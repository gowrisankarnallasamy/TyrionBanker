using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Unity;

namespace TyrionBanker.ApiWeb.Controllers
{
    public abstract class AbstractController : ApiController
    {
        /// <summary>
        /// DIコンテナ
        /// </summary>
        public IUnityContainer UnityContainer { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="unityContainer"></param>
        public AbstractController(IUnityContainer unityContainer)
        {
            this.UnityContainer = unityContainer;
        }

        protected bool ModelValidationCheck(object model, out string errorMessage)
        {
            errorMessage = null;

            if (model == null)
            {
                return true;
            }
            Type type = model.GetType();
            MethodInfo method = type.GetMethod("Validate");
            if (method == null)
            {
                return true;
            }
            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();
            method.Invoke(model, new object[] { modelStateDictionary });

            List<string> errors = new List<string>();
            if (modelStateDictionary.Keys.Count > 0)
            {
                foreach (string key in modelStateDictionary.Keys)
                {
                    ModelState modelState = modelStateDictionary[key];
                    errors.AddRange(modelState.Errors.Select(x => x.ErrorMessage).ToList());
                }
                if (errors.Count > 0)
                {
                    errorMessage = string.Join("\r\n", errors);
                    return false;
                }
            }

            return true;
        }
    }
}