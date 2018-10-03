using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Core.Log;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;

namespace TyrionBanker.Core.Unity.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public class LogAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LogHandler();
        }

        public class LogHandler : ICallHandler
        {
            public int Order
            {
                get
                {
                    return 1;
                }

                set
                {

                }
            }

            public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
            {
                var log = new TyrionBankerLogger(input.MethodBase.DeclaringType.Name);
                IMethodReturn result = null;
                try
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < input.Arguments.Count; i++)
                    {
                        var arg = input.Arguments[i];
                        if (sb.Length > 0) sb.Append(", ");
                        if (input.Arguments.ParameterName(i).ToLower() == "password")
                        {
                            sb.AppendFormat("{0}:(XXXX)", input.Arguments.ParameterName(i));
                        }
                        else
                        {
                            sb.AppendFormat("{0}:{1}", input.Arguments.ParameterName(i), arg);
                        }

                        var start = DateTime.UtcNow;
                        log.Trace("Start " + sb, null, input.MethodBase.Name, new StackFrame(5, true)?.GetFileLineNumber() ?? 0);
                        result = getNext()(input, getNext);
                        var end = DateTime.UtcNow;
                        if (result.Exception != null)
                        {
                            log.Error(result.Exception.Message, result.Exception, null, input.MethodBase.Name, new StackFrame(5, true)?.GetFileLineNumber() ?? 0);
                        }
                        else
                        {
                            log.Trace($"End({(end - start):mm\\:ss\\.fff}) result:{result.ReturnValue}", null, input.MethodBase.Name, new StackFrame(5, true)?.GetFileLineNumber() ?? 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    throw;
                }
                return result;
            }
        }
    }
}
