using AuthorizeNetSample.Domain.Enums;
using AuthorizeNetSample.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AuthorizeNetSample.Web.Helpers {
    public static class DropdownHelper {
        public static List<SelectListItem> GetMerchantAuthenticationTypes() {
            return Enum.GetValues(typeof(MerchantAuthenticationType)).Cast<MerchantAuthenticationType>()
                 .Select(x => new SelectListItem {
                     Text = x.GetDescription(),
                     Value = ((int)x).ToString(),
                 }).ToList();
        }
    }
}