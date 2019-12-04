using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventManagement.Identity
{
    public class UserPrincipal : ClaimsPrincipal
    {
        public UserPrincipal(ClaimsPrincipal principal) : base(principal)
        {
        }

        private string GetClaimValue(string key)
        {
            var identity = this.Identity as ClaimsIdentity;

            if (identity == null)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }

        public String UserName
        {
            get
            {
                if (this.FindFirst(JwtRegisteredClaimNames.Sub) == null)
                    return String.Empty;

                return GetClaimValue(JwtRegisteredClaimNames.Sub);
            }
        }

        public Guid UserId
        {
            get
            {
                if (this.FindFirst(JwtRegisteredClaimNames.Jti) == null)
                    return Guid.Empty;

                return Guid.Parse(GetClaimValue(JwtRegisteredClaimNames.Jti));
            }
        }

        public Guid? AccountId
        {
            get
            {
                if (this.FindFirst(ClaimTypesHelper.AccountId) == null)
                    return null;

                return Guid.Parse(GetClaimValue(ClaimTypesHelper.AccountId));
            }
        }

        public string Role
        {
            get
            {
                if (this.FindFirst(ClaimTypes.Role) == null)
                    return string.Empty;

                return GetClaimValue(ClaimTypes.Role);
            }
        }

        public string FirstName
        {
            get
            {
                if (this.FindFirst(ClaimTypesHelper.FirstName) == null)
                    return string.Empty;

                return GetClaimValue(ClaimTypesHelper.FirstName);
            }
        }

        public string LastName
        {
            get
            {
                if (this.FindFirst(ClaimTypesHelper.LastName) == null)
                    return string.Empty;

                return GetClaimValue(ClaimTypesHelper.LastName);
            }
        }

        public string FullName => string.Concat(FirstName, " ", LastName);

        public string LastLogin
        {
            get
            {
                if (this.FindFirst(ClaimTypesHelper.LastLogin) == null)
                    return string.Empty;

                return GetClaimValue(ClaimTypesHelper.LastLogin);
            }
        }

        public int UserType
        {
            get
            {
                if (this.FindFirst(ClaimTypesHelper.UserType) == null)
                    return 0;

                return int.Parse(GetClaimValue(ClaimTypesHelper.UserType));
            }
        }

    }
}
