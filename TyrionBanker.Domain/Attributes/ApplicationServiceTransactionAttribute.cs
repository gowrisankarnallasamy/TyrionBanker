using System;
using System.Diagnostics;
using System.Reflection;
using System.Transactions;
using TyrionBanker.Core.Log;
using TyrionBanker.Domain.Database;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;

namespace TyrionBanker.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ApplicationServiceTransactionAttribute : HandlerAttribute
    {
        public TransactionScopeOption Option { get; set; } = TransactionScopeOption.Required;

        public int Timeout { get; set; }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new TransactionHandler();
        }

        [DebuggerStepThrough]
        public class TransactionHandler : ICallHandler
        {
            public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
            {
                var classAttr = input.MethodBase.DeclaringType.GetCustomAttribute<ApplicationServiceTransactionAttribute>();
                var methodAttr = input.MethodBase.GetCustomAttribute<ApplicationServiceTransactionAttribute>();

                var option = methodAttr?.Option ?? classAttr?.Option ?? TransactionScopeOption.Required;
                var timeout = methodAttr?.Timeout ?? classAttr?.Timeout ?? 0;

                IMethodReturn result = null;
                try
                {
                    if (classAttr != null || methodAttr != null)
                    {
                        using (var tran = new TransactionScope(option, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(timeout) }))
                        {
                            result = getNext()(input, getNext);
                            DomainUnityContainer.Resolve<ITyrionBankerDbConnection>("TyrionBankerDB").Close();

                            if (result.Exception == null)
                            {
                                tran.Complete();
                            }
                        }
                    }
                    else result = getNext()(input, getNext);
                }
                catch (Exception e)
                {
                    new TyrionBankerLogger(input.MethodBase.DeclaringType.FullName).Error(e);
                    throw;
                }
                return result;
            }

            public int Order
            {
                get { return 1; }
                set { }
            }
        }
    }
}
