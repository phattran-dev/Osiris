using Shared.Models.APIModels;
using System.Text.Json;

namespace Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Dictionary<string, string> errors) : base(JsonSerializer.Serialize(BaseApiResponse<object>.FailureResponse(errors))) { }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(Dictionary<string, string> errors) : base(JsonSerializer.Serialize(BaseApiResponse<object>.FailureResponse(errors))) { }
    }

    public class SecurityException : Exception
    {
        public SecurityException(Dictionary<string, string> errors) : base(JsonSerializer.Serialize(BaseApiResponse<object>.FailureResponse(errors))) { }
    }
}
