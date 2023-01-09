using Serilog;

namespace N5Challenge.Transverse.Logger
{
    public static class LoggerUtils
    {
        public static void WriteLogError(string message, Exception exception, params object?[]? propertyValues)
        {
            string errorMessage = GetErrorMessage(exception);

            Log.Fatal(message + errorMessage, propertyValues);
        }

        private static string GetErrorMessage(Exception exception) 
        {
            string errorMessage = string.Empty;
            
            if(exception != null) 
            {
                errorMessage += $" -- [{exception.Message}].";

                if(exception.InnerException != null) 
                {
                    errorMessage += GetErrorMessage(exception.InnerException);
                }
            }

            return errorMessage;
        }
    }
}
