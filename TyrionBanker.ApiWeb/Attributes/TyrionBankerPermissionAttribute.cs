using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Web;

namespace TyrionBanker.ApiWeb.Attributes
{
    public class TyrionBankerPermissionAttribute : CodeAccessSecurityAttribute
    {
        public string Function { get; set; }

        public TyrionBankerPermissionAttribute(SecurityAction action = SecurityAction.Demand)
            : base(action)
        {
        }

        public override IPermission CreatePermission()
        {
            string function = this.Function;

            return new CustomClaimsPrincipalPermission("function", function);
        }
    }

    public class CustomClaimsPrincipalPermission : IPermission, ISecurityEncodable, IUnrestrictedPermission
    {
        private string name;
        private string[] values;
        public CustomClaimsPrincipalPermission(string name, string value)
        {
            this.name = name;
            values = value.Split(',').Select(v => v.Trim()).ToArray();
        }

        public IPermission Copy()
        {
            return (CustomClaimsPrincipalPermission)this.MemberwiseClone();
        }

        public void Demand()
        {
            CheckClaims();
        }

        public void FromXml(SecurityElement e)
        {

        }

        public IPermission Intersect(IPermission target)
        {
            return null;
        }

        public bool IsSubsetOf(IPermission target)
        {
            return target != null;
        }

        public bool IsUnrestricted()
        {
            var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.Current.User.Identity;
            return values.Any(v => identity.HasClaim(name, v));
        }

        public SecurityElement ToXml()
        {
            return null;
        }

        public IPermission Union(IPermission target)
        {
            return null;
        }

        private void CheckClaims()
        {
            if (!IsUnrestricted())
            {
                throw new SecurityException("Access is denied. Security principal does not satisfy required claims.");
            }
        }
    }
}