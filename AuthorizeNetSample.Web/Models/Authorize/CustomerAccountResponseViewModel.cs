using System.Collections.Generic;
using System.ComponentModel;

namespace AuthorizeNetSample.Web.Models.Authorize {
    public class CustomerAccountResponseViewModel {
        [DisplayName("Customer Id")]
        public string CustomerId { get; set; }
        [DisplayName("Customer Payment Profile Ids")]
        public List<string> PaymentProfiles { get; set; }
        [DisplayName("Customer Shipping Profile Ids")]
        public List<string> ShippingProfiles { get; set; }
    }
}