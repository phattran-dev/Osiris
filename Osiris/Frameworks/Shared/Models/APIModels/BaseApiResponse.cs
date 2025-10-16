namespace Shared.Models.APIModels
{
    public class BaseApiResponse<TData>
    {
        public TData? Data { get; set; }
        public Dictionary<string, string>? Errors { get; set; }
        public Dictionary<string, string>? Messages { get; set; }

        public static BaseApiResponse<TData?> SuccessResponse(TData? data, Dictionary<string, string>? messages = null)
        {
            return new BaseApiResponse<TData?>
            {
                Data = data,
                Messages = messages,
                Errors = null
            };
        }

        public static BaseApiResponse<TData?> FailureResponse(Dictionary<string, string>? errors = null, TData? data = null)
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
