using AuthorizeNetSample.Domain.Enums;
using System;
using System.ComponentModel;

namespace AuthorizeNetSample.Web.Models {
    public class ChargeCustomerViewModel {
        public CustomerViewModel Customer { get; set; }
        public decimal Amount { get; set; }
        public string PaymentNote { get; set; }
        public Guid CreditCardId { get; set; }
        [DisplayName("Authentication Type")]
        public MerchantAuthenticationType AuthenticationType { get; set; }
    }
}