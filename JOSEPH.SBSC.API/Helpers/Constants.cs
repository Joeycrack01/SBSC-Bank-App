using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOSEPH.SBSC.API.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string userName = "user", userId = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
                public const string UserClaim = "UserClaim";
                public const string CooperatePlan = "Cooperate";
                public const string EnterprisePlan = "Enterprise";
            }
        }
    }
}
