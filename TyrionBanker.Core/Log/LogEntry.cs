using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyrionBanker.Core.Log
{
    public class LogEntry
    {
        public Dictionary<string, object> Values { get; } = new Dictionary<string, object>();
        public Type BoundaryType { get; set; }
        public Guid LogGuid { get; } = Guid.NewGuid();
        public DateTime UtcDateTime { get; } = DateTime.UtcNow;
        public DateTime LocalDateTime { get; } = DateTime.Now;
        public Level Level { get; }

        public LogEntry(Level level)
        {
            this.Level = level;
        }

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public string Method { get; set; }
        public int Line { get; set; }

    }
}
