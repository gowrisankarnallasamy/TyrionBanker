using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyrionBanker.Core
{
    public abstract class TrionBankerExceptions : Exception
    {
        public TrionBankerExceptions(string exceptionMessage) : base(exceptionMessage)
        {

        }
    }

    public class UnabletoDeleteExceptions : TrionBankerExceptions
    {
        public UnabletoDeleteExceptions(string exceptionMessage) :base(exceptionMessage)
        {

        }
    }
}
