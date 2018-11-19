namespace AuthorizeNetSample.Domain.Models.Authorize {
    public class EncryptVisaCheckoutDataDto {
        public string DataDescriptor { get; set; }
        public string DataValue { get; set; }
        public string DataKey { get; set; }
        public string CallId { get; set; }
    }
}
