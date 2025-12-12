namespace Shared.Exceptions
{
    public class BaseCustomException : Exception
    {
        public string ErrorCode { get; set; }
        public string? CustomMessage { get; set; }

        public BaseCustomException(string errorCode, string customMessage, params object[]? parameters)
        {
            ErrorCode = errorCode;
            CustomMessage = parameters != null ? string.Format(customMessage, parameters) : customMessage;
        }
    }

    public class NotFoundException : BaseCustomException
    {
        public string? FieldName { get; set; }
        public string? LookupValue { get; set; }

        public NotFoundException(string errorCode, string customMessage, params object[]? parameters)
            : base(errorCode, customMessage, parameters)
        {
        }

        public NotFoundException(string errorCode, string? fieldName, string? lookupValue) : base(errorCode, string.Empty)
        {
            FieldName = fieldName;
            LookupValue = lookupValue;
        }
    }

    public class UnauthorizedException : BaseCustomException
    {
        public UnauthorizedException(string errorCode, string customMessage, params object[]? parameters)
           : base(errorCode, customMessage, parameters)
        {
        }
    }

    public class SecurityException : BaseCustomException
    {
        public SecurityException(string errorCode, string customMessage, params object[]? parameters)
            : base(errorCode, customMessage, parameters)
        {
        }
    }

    public class BadRequestException : BaseCustomException
    {
        public BadRequestException(string errorCode, string customMessage, params object[]? parameters)
             : base(errorCode, customMessage, parameters)
        {
        }
    }
}
