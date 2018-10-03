using EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment.Base;
using EmbroideryOrderes.AuthorizePaymentSystem.Models.Profile;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment {
    public class ProfilePayment : PaymentBase {
        public ANetCustomerProfileModel CustomerProfileModel { get; set; }
    }
}
