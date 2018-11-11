namespace AuthorizeNetSample.Domain.Models.Authorize {
    public class AuthorizePaymentResponse {
        public string TransactionId { get; set; }
        public string AuthCode { get; set; }
        public string PaddedCardNum { get; set; }
    }
}
