using StackExchange.Profiling.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Domain.Database;

namespace TyrionBanker.Infrastructure.Database
{
    public class TyrionBankerDbConnection : ITyrionBankerDbConnection, IDisposable
    {
        private readonly object obj = new object();
        private DbConnection dbConnection;

        public DbConnection Connection
        {
            get
            {
                return dbConnection;
            }
        }

        public TyrionBankerDbConnection(string connectionString)
        {
            List<IDbProfiler> profilerParam = new List<IDbProfiler>();

            var flag = ConfigurationManager.AppSettings["UseProfiler"] ?? "false";
            if (flag.ToLower() != "false")
            {
                profilerParam.Add(StackExchange.Profiling.MiniProfiler.Current);
            }
            flag = ConfigurationManager.AppSettings["OutPutSqlLog"] ?? "false";
            if (flag.ToLower() != "false")
            {
                profilerParam.Add(new TraceDbLogger());
            }
            if (profilerParam.Count > 0)
            {
                var profiler = new CompositeDbProfiler(profilerParam.ToArray());
                dbConnection = new ProfiledDbConnection(new SqlConnection(connectionString), profiler);
            }
            else
            {
                dbConnection = new SqlConnection(connectionString);
            }
        }

        public void Close()
        {
            if (dbConnection == null || dbConnection.State == ConnectionState.Closed) return;

            dbConnection.Close();
        }

        public void Open()
        {
            lock (obj)
            {
                if (dbConnection.State == ConnectionState.Closed || dbConnection.State == ConnectionState.Broken)
                {
                    dbConnection.Open();
                }
            }
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Dispose Resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~TyrionBankerDbConnection()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose Resources.
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbConnection != null)
                {
                    Close();
                    dbConnection.Dispose();
                    dbConnection = null;
                }
            }
        }

        #endregion
    }
}
