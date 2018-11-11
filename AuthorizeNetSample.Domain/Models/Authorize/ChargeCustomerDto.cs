using AuthorizeNetSample.Domain.Enums;
using System;

namespace AuthorizeNetSample.Domain.Models.Authorize {
    public class ChargeCustomerDto {
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentNote { get; set; }
        public Guid CreditCardId { get; set; }
        public MerchantAuthenticationType AuthenticationType { get; set; }
    }
}
