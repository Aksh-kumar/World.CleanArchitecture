namespace World.WebApi.common
{
    public class AutherizationConstant
    {
        #region IMSURL
        public const string HTTP_ALLOW_METHOD_PROPERTY = "allowedHttpMethods";

        #endregion
    }

    public class ACTION
    {
        public const string READ = "READ";
        public const string INSERT = "INSERT";
        public const string UPDATE = "UPDATE";
        public const string DELETE = "DELETE";
    }

    public class MessageConstants
    {
        #region Log Message
        public const string StartMethodExecution = "--- Start Execution Of {0} ---";
        public const string EndMethodExecution = "--- End Execution Of {0} ---";
        #endregion

        #region Exception Messages
        public const string UnauthorizedAccessException = "You are not authorized user, Please contact site admin";
        public const string InvalidAccessTokenException = "Invalid Token Details";
        public const string DefaultServerErrorMessage = "A server error occurred.";
        public const string NotFoundMessage = "Record not found.";
        #endregion

        #region ExceptionType
        public const string UnauthorizedAccess = "UnauthorizedAccess";
        public const string InvalidAccessToken = "InvalidAccessToken";
        #endregion
    }
}
