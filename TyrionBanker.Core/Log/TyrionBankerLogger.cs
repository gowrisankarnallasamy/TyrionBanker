using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyrionBanker.Core.Log
{
    public class TyrionBankerLogger
    {
        public ILog Log4NetLogger { get; }

        public ILogger Logger => this.Log4NetLogger.Logger;

        public TyrionBankerLogger(Type type)
        {
            this.Log4NetLogger = LogManager.GetLogger(type);
        }

        public TyrionBankerLogger(string name)
        {
            this.Log4NetLogger = LogManager.GetLogger(name);
        }

        public void Write(LogEntry logEntry)
        {
            var loggingEvent = new LoggingEvent(logEntry.BoundaryType ?? typeof(TyrionBankerLogger), Logger.Repository, new LoggingEventData()
            {
                Level = logEntry.Level,
                Message = logEntry.Message,
                //TimeStamp = logEntry.LocalDateTime,
                TimeStampUtc = logEntry.UtcDateTime,
                LoggerName = Logger.Name,
                Identity = logEntry.LogGuid.ToString(),
                ExceptionString = logEntry.Exception?.ToString().Replace("\r", " ").Replace("\n", " ")
            });

            foreach (var p in logEntry.Values)
            {
                loggingEvent.Properties[p.Key] = p.Value;
            }

            Logger.Log(loggingEvent);

        }
    }
}
