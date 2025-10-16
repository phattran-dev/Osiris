namespace Shared.ErrorCodes
{
    public static partial class ResponseCodes
    {
        public static string GetMessage(string code, params object[]? parameters)
        {
            var message = GetRawMessageByCode(code);
            if (parameters == null || parameters.Length == 0)
                return CleanMessage(message);

            try
            {
                return CleanMessage(string.Format(message, parameters));
            }
            catch (FormatException)
            {
                // If formatting fails, return the original message
                return CleanMessage(message);
            }
        }


        /// <summary>
        /// Add more information for error message
        /// </summary>
        /// <returns>Error Message - [Additional Information]</returns>
        public static Dictionary<string, string> GetMessageDictionary(string errorCode, string? additionalInformation = null, params object[]? parameters)
        {
            var errroMessage = string.IsNullOrWhiteSpace(additionalInformation) ?
                GetMessage(errorCode, parameters) :
                $"{GetMessage(errorCode, parameters)} \n ###Details: {additionalInformation}";

            return new Dictionary<string, string> { { errorCode, errroMessage } };
        }

        #region Private Methods
        private static string GetRawMessageByCode(string errorCode)
        {
            if (CommonCodes.Messages.TryGetValue(errorCode, out string? message))
                return message;

            return CommonCodes.Messages[CommonCodes.SOMETHING_WENT_WRONG];
        }

        private static string CleanMessage(string message)
        {
            if(string.IsNullOrWhiteSpace(message))
                return string.Empty;

            return message.Replace("  ", " ").Trim();
        }

        #endregion Private Methods
    }
}
