using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyrionBanker.Domain.Database
{
	public interface ITyrionBankerDbConnection
	{
		DbConnection Connection { get; }
		void Open();
		void Close();
	}
}
