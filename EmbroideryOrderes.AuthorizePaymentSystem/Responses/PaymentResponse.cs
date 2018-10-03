namespace EmbroideryOrderes.AuthorizePaymentSystem.Responses {
    public class PaymentResponse {
        public string TransactionId { get; set; }
        public string AuthCode { get; set; }
        public string PaddedCardNum { get; set; }
    }
}
