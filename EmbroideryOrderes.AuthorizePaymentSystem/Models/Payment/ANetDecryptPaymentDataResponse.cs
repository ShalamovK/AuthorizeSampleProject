namespace EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment {
    public class ANetDecryptPaymentDataResponse {
        public ShippingInfo ShippingInfo { get; set; }
        public BillingInfo BillingInfo { get; set; }
        public ANetCardModel CardInfo { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
    }

    public class PaymentDetails {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }

    public class ShippingInfo {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }

    public class BillingInfo : ShippingInfo {
        public string Email { get; set; }
    }
}
