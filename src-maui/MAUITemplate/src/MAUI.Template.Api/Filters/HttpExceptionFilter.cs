namespace MAUI.Template.Api.Filters
{
    public class HttpExceptionFilter
    {
        public static bool NoConnection(Exception exception)
        {
            var exceptionFullName = exception.GetType().FullName;
            return exception is HttpRequestException 
                   || string.Equals(exceptionFullName, "Java.Net.UnknownHostException");
        }
        
        public static bool LostConnection(Exception exception)
        {
            var exceptionFullName = exception.GetType().FullName;
            return exception is InvalidOperationException 
                   || string.Equals(exceptionFullName, "Javax.Net.Ssl.SSLException") 
                   || string.Equals(exceptionFullName, "Java.Net.SocketException")
                   || string.Equals(exceptionFullName, "Java.IO.IOException");
        }
    }
}