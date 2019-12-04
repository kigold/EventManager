using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Identity
{
    public static class ClaimTypesHelper
    {
        public const string AccountId = nameof(AccountId);
        public const string FirstName = nameof(FirstName);
        public const string LastName = nameof(LastName);
        public const string LastLogin = nameof(LastLogin);
        public const string UserType = nameof(UserType);
    }
}
