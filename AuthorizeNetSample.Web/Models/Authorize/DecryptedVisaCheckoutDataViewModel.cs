using System.ComponentModel;

namespace AuthorizeNetSample.Web.Models.Authorize {
    public class DecryptedVisaCheckoutDataViewModel {
        public ShippingInfoViewModel ShippingInfo { get; set; }
        public BillingInfoViewModel BillingInfo { get; set; }
        public CardInfoViewModel CardInfo { get; set; }
        public PaymentDetailsViewModel PaymentDetails { get; set; }
    }

    public class CardInfoViewModel {
        [DisplayName("Number")]
        public string Number { get; set; }
        [DisplayName("Expiration Date")]
        public string ExpDate { get; set; }
    }

    public class PaymentDetailsViewModel {
        [DisplayName("Amount")]
        public decimal Amount { get; set; }
        [DisplayName("Currency")]
        public string Currency { get; set; }
    }

    public class ShippingInfoViewModel {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("State")]
        public string State { get; set; }
        [DisplayName("ZIP")]
        public string Zip { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
    }

    public class BillingInfoViewModel : ShippingInfoViewModel {
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}