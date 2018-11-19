namespace AuthorizeNetSample.Domain.Models.Dtos {
    public class DecryptedVisaCheckoutDataDto {
        public ShippingInfoDto ShippingInfo { get; set; }
        public BillingInfoDto BillingInfo { get; set; }
        public CardInfoDto CardInfo { get; set; }
        public PaymentDetailsDto PaymentDetails { get; set; }
    }

    public class CardInfoDto {
        public string Number { get; set; }
        public string ExpDate { get; set; }
    }

    public class PaymentDetailsDto {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }

    public class ShippingInfoDto {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }

    public class BillingInfoDto : ShippingInfoDto {
        public string Email { get; set; }
    }
}
