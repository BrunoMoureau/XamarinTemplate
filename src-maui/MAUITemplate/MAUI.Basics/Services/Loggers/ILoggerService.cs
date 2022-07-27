﻿using System.Runtime.CompilerServices;

namespace MAUI.Basics.Services.Loggers
{
    public interface ILoggerService
    {
        void Initialize();

        void Debugging(string message, Dictionary<string, string> properties = null,
            [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "");

        void Debugging(Exception exception, Dictionary<string, string> properties = null,
            [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "");

        void Log(string message, Dictionary<string, string> properties = null,
            [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "");

        void Log(Exception exception, Dictionary<string, string> properties = null,
            [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "");
    }
}