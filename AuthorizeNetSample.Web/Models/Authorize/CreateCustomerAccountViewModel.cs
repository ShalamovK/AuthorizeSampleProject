using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorizeNetSample.Web.Models.Authorize {
    public class CreateCustomerAccountViewModel {
        public CreditCardViewModel CreditCard { get; set; }
        public ShippingInfoViewModel ShippingAddress { get; set; }
        public BillingInfoViewModel BillingAddress { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}