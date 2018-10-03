using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace TyrionBanker.Core
{
    public static class CoreUnityContainer
    {
        internal static IUnityContainer UnityContainer;
        public static void BuildUp(IUnityContainer container)
        {
            if (UnityContainer != null)
            {
                throw new InvalidOperationException("CoreUnityContainer already configured");
            }
        }
    }
}
