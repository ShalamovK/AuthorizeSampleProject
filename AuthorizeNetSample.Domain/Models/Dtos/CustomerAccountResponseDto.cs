using System.Collections.Generic;

namespace AuthorizeNetSample.Domain.Models.Dtos {
    public class CustomerAccountResponseDto {
        public string CustomerId { get; set; }
        public List<string> PaymentProfiles { get; set; }
        public List<string> ShippingProfiles { get; set; }
    }
}
