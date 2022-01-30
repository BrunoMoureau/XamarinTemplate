using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xamarin.Basics.Services.Loggers;

namespace XamarinTemplate.Services.Loggers
{
    public class LoggerService : ILoggerService
    {
        public void Initialize()
        {
        }

        public void Debugging(string message, Dictionary<string, string> properties = null,
            [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "")
        {
            Debug.WriteLine($@"
[Debugging]
--- {message}
--- [File: {filePath}]
--- [Called from: {callerName}]
");
        }

        public void Debugging(Exception exception, Dictionary<string, string> properties = null,
            [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "")
        {
            Debug.WriteLine($@"
[Debugging]
--- [Exception: {exception.Message}]
--- [StackTrace: {exception.StackTrace}]
------ [Inner exception: {exception.InnerException?.Message ?? "/"}]
------ [StackTrace: {exception.InnerException?.StackTrace ?? "/"}]
--- [File: {filePath}]
--- [Called from: {callerName}]
");
        }

        public void Log(string message, Dictionary<string, string> properties = null,
            [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "")
        {
            Debugging(message, properties, callerName, filePath);
            //write any external log service call below
        }

        public void Log(Exception exception, Dictionary<string, string> properties = null,
            [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "")
        {
            Debugging(exception, properties, callerName, filePath);
            //write any external log service call below
        }
    }
}