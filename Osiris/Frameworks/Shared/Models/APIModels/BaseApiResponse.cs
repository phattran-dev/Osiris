namespace Shared.Models.APIModels
{
    public class BaseApiResponse<TData>
    {
        public TData? Data { get; set; }
        /// <summary>
        /// {ErrorCode, Message}
        /// </summary>
        public Dictionary<string, string?>? Errors { get; set; }
        public string? Messages { get; set; }

        public static BaseApiResponse<TData?> SuccessResponse(TData? data, string? messages = null)
        {
            return new BaseApiResponse<TData?>
            {
                Data = data,
                Messages = messages,
                Errors = null
            };
        }

        public static BaseApiResponse<TData?> FailureResponse(Dictionary<string, string?>? errors = null, TData? data = default)
        {
            return new BaseApiResponse<TData?>
            {
                Data = data,
                Errors = errors,
                Messages = null
            };
        }
    }
}
