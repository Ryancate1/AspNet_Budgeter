using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace rcate_FinancialPortal.Models.Helpers
{
    public static class Extensions
    {
        public static int? GetHouseHoldId(this IIdentity user)
        {
            var claimsIdentity = (ClaimsIdentity)user;
            var HouseHoldClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "HouseHoldId");

            if (HouseHoldClaim != null)
            {
                return Int32.Parse(HouseHoldClaim.Value);
            }
            else
            {
                return null;
            }
        }

        public static bool IsInHouseHold(this IIdentity user)
        {
            var cUser = (ClaimsIdentity)user;
            var houseId = cUser.Claims.FirstOrDefault(c => c.Type == "HouseHoldId");
            return (houseId != null && !string.IsNullOrWhiteSpace(houseId.Value));
        }
    }
}