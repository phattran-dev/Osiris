using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.ErrorCodes;
using Shared.Exceptions;
using Shared.Models.APIModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Middlewares
{
    public sealed class GlobalExpectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExpectionMiddleware> _logger;

        public GlobalExpectionMiddleware(RequestDelegate next, ILogger<GlobalExpectionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext _httpContext)
        {
            try
            {
                await _next(_httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(_httpContext, ex);
            }
        }

        #region Private Helper Methods
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new BaseApiResponse<object>();

            switch (exception)
            {
                case UnauthorizedException:
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                case SecurityException:
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    break;
                case NotFoundException notFoundException:
                    {
                        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        response.Errors = new Dictionary<string, string?>();

                        bool isUseCustomMessage = !string.IsNullOrWhiteSpace(notFoundException.CustomMessage);
                        if (isUseCustomMessage)
                        {
                            response.Errors.Add(notFoundException.ErrorCode, notFoundException.CustomMessage);
                        }
                        else
                        {
                            var message = ResponseCodes.GetMessage(notFoundException.ErrorCode);
                            var parameters = new List<object>()
                            {
                                !string.IsNullOrWhiteSpace(notFoundException.FieldName) ? notFoundException.FieldName : string.Empty,
                                !string.IsNullOrWhiteSpace(notFoundException.LookupValue) ? notFoundException.LookupValue : string.Empty,
                            };
                            message = string.Format(message, parameters.ToArray());
                            response.Errors.Add(notFoundException.ErrorCode, message);
                        }
                    }
                    break;
                default:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            _logger.LogError(exception, $"An unhandled exception occurred. Something went wrong! StatusCode: {httpContext.Response.StatusCode} \n Message:{exception.Message}; \n StackTrace: {exception.StackTrace}");

            try
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };

                var result = JsonSerializer.Serialize(response, jsonOptions);
                await httpContext.Response.WriteAsync(result);

            } catch (Exception logEx)
            {
                _logger.LogError(logEx, "An error occurred while logging exception details.");
                httpContext.Response.ContentType = "text/plain";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync($"An internal error server occurred: {logEx.Message}");
            }
        }

        //private string GetStringJsonResponse(Exception ex)
        //{
        //    if (IsMessageFormattedAsApiResponse(ex.Message))
        //        return ex.Message;

        //    var buildAdditionalInfo = new StringBuilder();
        //    buildAdditionalInfo.AppendLine($"\nException Type: {ex.GetType().FullName}");
        //    buildAdditionalInfo.AppendLine($"\nMessage: {ex.Message}");
        //    buildAdditionalInfo.AppendLine($"\nStackTrace: {ex.StackTrace}");
        //    var additionalInfo = buildAdditionalInfo.ToString();

        //    var errors = ResponseCodes.GetMessageDictionary(ResponseCodes.CommonCodes.SOMETHING_WENT_WRONG, additionalInfo);
        //    return JsonSerializer.Serialize(BaseApiResponse<string>.FailureResponse(errors));
        //}

        /// <summary>
        /// Check message is already formatted as API response model
        /// </summary>
        //private bool IsMessageFormattedAsApiResponse(string errorMessages)
        //{
        //    if (string.IsNullOrWhiteSpace(errorMessages))
        //        return false;

        //    try
        //    {
        //        using var document = JsonDocument.Parse(errorMessages);
        //        var root = document.RootElement;

        //        return root.ValueKind == JsonValueKind.Object &&
        //               (root.TryGetProperty($"{nameof(BaseApiResponse<object>.Data)}", out _) || root.TryGetProperty($"{nameof(BaseApiResponse<object>.Messages)}", out _));
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        #endregion Private Helper Methods
    }
}
