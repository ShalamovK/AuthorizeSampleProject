using System.ComponentModel;

namespace AuthorizeNetSample.Web.Models.Authorize {
    public class EncryptVisaCheckoutDataViewModel {
        [DisplayName("Data Descriptor")]
        public string DataDescriptor { get; set; }
        [DisplayName("Data Value")]
        public string DataValue { get; set; }
        [DisplayName("Data Key")]
        public string DataKey { get; set; }
        [DisplayName("Call Id")]
        public string CallId { get; set; }
    }
}