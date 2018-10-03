using EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Models.Profile {
    public class PaymentProfileModel {
        public string Id { get; set; }
        public bool Default { get; set; }
        public ANetAddressModel BillTo { get; set; }
        public ANetCardModel CreditCard { get; set; }
    }
}
