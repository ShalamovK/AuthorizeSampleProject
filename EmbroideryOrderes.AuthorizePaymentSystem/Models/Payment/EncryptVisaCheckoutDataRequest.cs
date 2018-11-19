namespace EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment {
    public class EncryptVisaCheckoutDataRequest {
        public string DataDescriptor { get; set; }
        public string DataValue { get; set; }
        public string DataKey { get; set; }
        public string CallId { get; set; }
    }
}
