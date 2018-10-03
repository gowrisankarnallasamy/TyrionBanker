using log4net.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TyrionBanker.Core.Log
{
    public static class TyrionBankerLoggerEx
    {
        private static LogEntry CreateLogEntry(Level level, string message, Exception exception, object properties, string caller, int line)
        {
            var entity = new LogEntry(level)
            {
                BoundaryType = typeof(TyrionBankerLoggerEx),
                Message = message,
                Line = line,
                Method = caller,
                Exception = exception,
            };

            //entity.Values.Add("_Message", message);
            entity.Values.Add("_Line", line);
            entity.Values.Add("_Method", caller);
            //entity.Values.Add("_Exception", exception?.ToString());

            var jsonProperties = JsonConvert.SerializeObject(properties, Formatting.None);
            entity.Values["_Values"] = jsonProperties;

            return entity;
        }


        #region like "Implementation of ILog"

        public static void Trace(this TyrionBankerLogger me,
            object message,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Trace, message?.ToString(), null, properties, caller, line));

        public static void Trace(this TyrionBankerLogger me,
            object message, Exception exception,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Trace, message?.ToString(), exception, properties, caller, line));

        public static void TraceFormat(this TyrionBankerLogger me,
            string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Trace, string.Format(format, args), null, properties, caller, line));

        public static void TraceFormat(this TyrionBankerLogger me,
            string format, object arg0,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Trace, string.Format(format, arg0), null, properties, caller, line));

        public static void TraceFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Trace, string.Format(format, arg0, arg1), null, properties, caller, line));

        public static void TraceFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1, object arg2,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Trace, string.Format(format, arg0, arg1, arg2), null, properties, caller, line));

        public static void TraceFormat(this TyrionBankerLogger me,
            IFormatProvider provider, string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Trace, string.Format(format, args, provider), null, properties, caller, line));

        public static void Debug(this TyrionBankerLogger me,
            object message,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Debug, message?.ToString(), null, properties, caller, line));

        public static void Debug(this TyrionBankerLogger me,
            object message, Exception exception,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Debug, message?.ToString(), exception, properties, caller, line));

        public static void DebugFormat(this TyrionBankerLogger me,
            string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Debug, string.Format(format, args), null, properties, caller, line));

        public static void DebugFormat(this TyrionBankerLogger me,
            string format, object arg0,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Debug, string.Format(format, arg0), null, properties, caller, line));

        public static void DebugFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Debug, string.Format(format, arg0, arg1), null, properties, caller, line));

        public static void DebugFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1, object arg2,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Debug, string.Format(format, arg0, arg1, arg2), null, properties, caller, line));

        public static void DebugFormat(this TyrionBankerLogger me,
            IFormatProvider provider, string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Debug, string.Format(format, args, provider), null, properties, caller, line));

        public static void Info(this TyrionBankerLogger me,
              object message,
              object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Info, message?.ToString(), null, properties, caller, line));

        public static void Info(this TyrionBankerLogger me,
            object message, Exception exception,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Info, message?.ToString(), exception, properties, caller, line));

        public static void InfoFormat(this TyrionBankerLogger me,
            string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Info, string.Format(format, args), null, properties, caller, line));

        public static void InfoFormat(this TyrionBankerLogger me,
            string format, object arg0,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Info, string.Format(format, arg0), null, properties, caller, line));

        public static void InfoFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Info, string.Format(format, arg0, arg1), null, properties, caller, line));

        public static void InfoFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1, object arg2,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Info, string.Format(format, arg0, arg1, arg2), null, properties, caller, line));

        public static void InfoFormat(this TyrionBankerLogger me,
            IFormatProvider provider, string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Info, string.Format(format, args, provider), null, properties, caller, line));

        public static void Warn(this TyrionBankerLogger me,
            object message,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Warn, message?.ToString(), null, properties, caller, line));

        public static void Warn(this TyrionBankerLogger me,
            object message, Exception exception,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Warn, message?.ToString(), exception, properties, caller, line));

        public static void WarnFormat(this TyrionBankerLogger me,
            string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Warn, string.Format(format, args), null, properties, caller, line));

        public static void WarnFormat(this TyrionBankerLogger me,
            string format, object arg0,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Warn, string.Format(format, arg0), null, properties, caller, line));

        public static void WarnFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Warn, string.Format(format, arg0, arg1), null, properties, caller, line));

        public static void WarnFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1, object arg2,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Warn, string.Format(format, arg0, arg1, arg2), null, properties, caller, line));

        public static void WarnFormat(this TyrionBankerLogger me,
            IFormatProvider provider, string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Warn, string.Format(format, args, provider), null, properties, caller, line));

        public static void Error(this TyrionBankerLogger me,
            object message,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Error, message?.ToString(), null, properties, caller, line));

        public static void Error(this TyrionBankerLogger me,
            object message, Exception exception,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Error, message?.ToString(), exception, properties, caller, line));

        public static void ErrorFormat(this TyrionBankerLogger me,
            string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Error, string.Format(format, args), null, properties, caller, line));

        public static void ErrorFormat(this TyrionBankerLogger me,
            string format, object arg0,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Error, string.Format(format, arg0), null, properties, caller, line));

        public static void ErrorFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Error, string.Format(format, arg0, arg1), null, properties, caller, line));

        public static void ErrorFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1, object arg2,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Error, string.Format(format, arg0, arg1, arg2), null, properties, caller, line));

        public static void ErrorFormat(this TyrionBankerLogger me,
            IFormatProvider provider, string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Error, string.Format(format, args, provider), null, properties, caller, line));

        public static void Fatal(this TyrionBankerLogger me,
            object message,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Fatal, message?.ToString(), null, properties, caller, line));

        public static void Fatal(this TyrionBankerLogger me,
            object message, Exception exception,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Fatal, message?.ToString(), exception, properties, caller, line));

        public static void FatalFormat(this TyrionBankerLogger me,
            string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Fatal, string.Format(format, args), null, properties, caller, line));

        public static void FatalFormat(this TyrionBankerLogger me,
            string format, object arg0,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Fatal, string.Format(format, arg0), null, properties, caller, line));

        public static void FatalFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Fatal, string.Format(format, arg0, arg1), null, properties, caller, line));

        public static void FatalFormat(this TyrionBankerLogger me,
            string format, object arg0, object arg1, object arg2,
            object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0) => me.Write(CreateLogEntry(Level.Fatal, string.Format(format, arg0, arg1, arg2), null, properties, caller, line));

        public static void FatalFormat(this TyrionBankerLogger me,
            IFormatProvider provider, string format, object properties = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, params object[] args) => me.Write(CreateLogEntry(Level.Fatal, string.Format(format, args, provider), null, properties, caller, line));

        #endregion
    }
}
