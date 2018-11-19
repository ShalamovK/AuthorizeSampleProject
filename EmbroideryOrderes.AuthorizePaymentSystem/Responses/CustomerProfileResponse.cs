using System.Collections.Generic;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Responses {
    public class CustomerProfileResponse {
        public string CustomerProfileId { get; set; }
        public List<string> CustomerPaymentProfileIds { get; set; }
        public List<string> CustomerShippingProfileIds { get; set; }
    }
}
