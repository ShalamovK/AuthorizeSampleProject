using EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment.Base;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment {
    public class CreditCardPayment : PaymentBase {
        public ANetCardModel Card { get; set; }
        public ANetAddressModel BillAddress { get; set; }
    }
}
