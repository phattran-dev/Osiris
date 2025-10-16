namespace Shared.ErrorCodes
{
    public static partial class ResponseCodes
    {
        public static class CommonCodes
        {
            #region Failure/Validation/Error Codes
            public const string SOMETHING_WENT_WRONG = "CM_00000";
            public const string INVALID_CREDENTIALS = "CM_00001";
            public const string RECORD_ALREADY_EXISTS = "CM_00002";
            public const string RECORD_ALREADY_DELETED = "CM_00003";
            public const string RECORD_NOT_FOUND = "CM_00004";
            public const string RECORD_CREATED_FAILED = "CM_00005";
            public const string RECORD_UPDATE_FAILED = "CM_00006";
            public const string RECORD_DELETE_FAILED = "CM_00007";
            public const string FIELD_REQUIRED = "CM_00008";
            public const string INVALID_REQUEST = "CM_00009";
            public const string INVALID_FILE_FORMAT = "CM_00010";
            public const string INVALID_FILE_SIZE = "CM_00011";
            public const string INVALID_FIELD_VALUE = "CM_00012";
            public const string INVALID_FIELD_LENGTH = "CM_00013";
            public const string INVALID_DATE_FORMAT = "CM_00014";
            public const string INVALID_DATE_RANGE = "CM_00015";
            public const string INVALID_NUMERIC_FORMAT = "CM_00016";
            public const string INVALID_NUMERIC_RANGE_VALUE = "CM_00017";
            public const string INVALID_SELECTED_OPTION = "CM_00018";
            #endregion Failure/Validation/Error Codes

            #region Success Codes
            public const string GET_RECORDS_SUCCESS = "SUCCESS_00000";
            public const string GET_RECORD_SUCCESS = "SUCCESS_00001";
            public const string CREATE_RECORDS_SUCCESS = "SUCCESS_00002";
            public const string CREATE_RECORD_SUCCESS = "SUCCESS_00003";
            public const string UPDATE_RECORDS_SUCCESS = "SUCCESS_00004";
            public const string UPDATE_RECORD_SUCCESS = "SUCCESS_00005";
            public const string DELETE_RECORDS_SUCCESS = "SUCCESS_00006";
            public const string DELETE_RECORD_SUCCESS = "SUCCESS_00007";
            public const string IMPORT_FILES_SUCCESS = "SUCCESS_00008";
            public const string IMPORT_FILE_SUCCESS = "SUCCESS_00009";
            public const string EXPORT_FILES_SUCCESS = "SUCCESS_00010";
            public const string EXPORT_FILE_SUCCESS = "SUCCESS_00011";
            #endregion Success Codes

            public static readonly Dictionary<string, string> Messages = new Dictionary<string, string>()
            {
                #region Failure/Validation/Error Codes
                { SOMETHING_WENT_WRONG, "Something went wrong, please try again. {0}" },
                { INVALID_CREDENTIALS, "Invalid credentials {0}, please try again." },
                { RECORD_ALREADY_EXISTS, "{0} Record already exists." },
                { RECORD_ALREADY_DELETED, "{0} Record already deleted." },
                { RECORD_NOT_FOUND, "{0} Record not found." },
                { RECORD_CREATED_FAILED, "Failed to create {0} record." },
                { RECORD_UPDATE_FAILED, "Failed to update {0} record." },
                { RECORD_DELETE_FAILED, "Failed to delete {0} record." },
                { FIELD_REQUIRED, "This {0} field is required." },
                { INVALID_REQUEST, "Invalid {0} request. {0}" },
                { INVALID_FILE_FORMAT, "Invalid file format. Expected format: {0}" },
                { INVALID_FILE_SIZE, "Invalid file size. The maximum limit of {0}" },
                { INVALID_FIELD_VALUE, "Invalid {0} field value." },
                { INVALID_FIELD_LENGTH, "Invalid {0} field length. The maximum limit of {1} characters." },
                { INVALID_DATE_FORMAT, "Invalid date format. Expected format: {0}" },
                { INVALID_DATE_RANGE, "Invalid date range. The date must be {0}" },
                { INVALID_NUMERIC_FORMAT, "Invalid numeric format. Expected format: {0}" },
                { INVALID_NUMERIC_RANGE_VALUE, "Invalid numeric range value. The value must be {0}" },
                { INVALID_SELECTED_OPTION, "Invalid selected option for {0}." },
                #endregion Failure/Validation/Error Codes

                #region Success Codes
                { GET_RECORDS_SUCCESS, "{0} Records retrieved successfully." },
                { GET_RECORD_SUCCESS, "{0} Record retrieved successfully." },
                { CREATE_RECORDS_SUCCESS, "{0} Records created successfully." },
                { CREATE_RECORD_SUCCESS, "{0} Record created successfully." },
                { UPDATE_RECORDS_SUCCESS, "{0} Records updated successfully." },
                { UPDATE_RECORD_SUCCESS, "{0} Record updated successfully." },
                { DELETE_RECORDS_SUCCESS, "{0} Records deleted successfully." },
                { DELETE_RECORD_SUCCESS, "{0} Record deleted successfully." },
                { IMPORT_FILES_SUCCESS, "Files imported successfully." },
                { IMPORT_FILE_SUCCESS, "File imported successfully." },
                { EXPORT_FILES_SUCCESS, "Files exported successfully." },
                { EXPORT_FILE_SUCCESS, "File exported successfully." }
                #endregion Success Codes
            };
        }
    }

}
