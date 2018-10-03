namespace EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment {
    public class ANetRefundModel : ANetCreditCardPaymentBase {
        public string TransactionId { get; set; }
        public string AuthKey { get; set; }
    }
}
