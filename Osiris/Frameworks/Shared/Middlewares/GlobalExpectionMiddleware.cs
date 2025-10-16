using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.ErrorCodes;
using Shared.Exceptions;
using Shared.Models.APIModels;
using System.Text;
using System.Text.Json;


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
                _httpContext.Response.ContentType = "application/json";
                var jsonResponse = GetStringJsonResponse(ex);

                switch (ex)
                {
                    case UnauthorizedException _:
                        _httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        break;
                    case SecurityException _:
                        _httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        break;
                    case NotFoundException _:
                        _httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                }

                _logger.LogError(ex, $"An unhandled exception occurred. Something went wrong! StatusCode: {_httpContext.Response.StatusCode} \n Message:{ex.Message}; \n StackTrace: {ex.StackTrace}");

                var jsonBytes = Encoding.UTF8.GetBytes(jsonResponse);
                await _httpContext.Response.Body.WriteAsync(jsonBytes, 0, jsonBytes.Length);
            }
        }


        private string GetStringJsonResponse(Exception ex)
        {
            if (IsMessageFormattedAsApiResponse(ex.Message))
                return ex.Message;

            var buildAdditionalInfo = new StringBuilder();
            buildAdditionalInfo.AppendLine($"\nException Type: {ex.GetType().FullName}");
            buildAdditionalInfo.AppendLine($"\nMessage: {ex.Message}");
            buildAdditionalInfo.AppendLine($"\nStackTrace: {ex.StackTrace}");
            var additionalInfo = buildAdditionalInfo.ToString();

            var errors = ResponseCodes.GetMessageDictionary(ResponseCodes.CommonCodes.SOMETHING_WENT_WRONG, additionalInfo);
            return JsonSerializer.Serialize(BaseApiResponse<string>.FailureResponse(errors));
        }

        /// <summary>
        /// Check message is already formatted as API response model
        /// </summary>
        private bool IsMessageFormattedAsApiResponse(string errorMessages)
        {
            if (string.IsNullOrWhiteSpace(errorMessages))
                return false;

            try
            {
                using var document = JsonDocument.Parse(errorMessages);
                var root = document.RootElement;

                return root.ValueKind == JsonValueKind.Object &&
                       (root.TryGetProperty($"{nameof(BaseApiResponse<object>.Data)}", out _) || root.TryGetProperty($"{nameof(BaseApiResponse<object>.Messages)}", out _));
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
