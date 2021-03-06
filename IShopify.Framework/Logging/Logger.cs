﻿using IShopify.Core.Framework.Logging;
using IShopify.Core.Helpers;
using IShopify.Core.Security;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IShopify.Framework.Logging
{
    public class Logger : ILogger
    {
        private readonly ILogProvider _logProvider;
        private readonly IUserContext _userContext;

        public Logger(ILogProvider logProvider, IUserContext userContext)
        {
            _logProvider = logProvider;
            _userContext = userContext;
        }

        public Guid Debug(string message, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            return LogEvent(LogLevel.Debug, null, message);
        }

        public Guid Error(string message, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            return LogEvent(LogLevel.Error, null, message);
        }

        public Guid Error(Exception exception, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            return LogEvent(LogLevel.Error, exception, exception.Message);
        }

        public Guid Fatal(string message, Exception exception = null, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            return LogEvent(LogLevel.Fatal, exception, exception.Message);
        }

        public Guid Info(string message, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            return LogEvent(LogLevel.Information, null, message);
        }

        public Guid Trace(string message, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            return LogEvent(LogLevel.Default, null, message);
        }

        public Guid Warn(string message, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            return LogEvent(LogLevel.Warning, null, message);
        }

        private Guid LogEvent(LogLevel logLevel, Exception exception, string message)
        {
            
            var logEntryValues = new LogEntryValues
            {
                LogId = Guid.NewGuid(),
                Message = message,
                BaseExceptionType = exception?.GetBaseException().GetType()?.Name,
                ExceptionType = exception?.GetType()?.Name,
                Exception = exception?.ToJson(camelCasing: true),
                Logger = string.Empty, // TODO update
                User = $"{_userContext?.Email} <{_userContext?.DisplayName}>"
            };

            return _logProvider.LogEvent(logLevel, logEntryValues);
        }
    }
}
