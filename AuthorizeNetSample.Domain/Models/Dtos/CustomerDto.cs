using AuthorizeNetSample.Domain.Models.Dtos.Base;
using System;
using System.Collections.Generic;

namespace AuthorizeNetSample.Domain.Models.Dtos {
    public class CustomerDto : BaseEntityDto<Guid> {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AuthorizeId { get; set; }
        public List<PaymentDto> Payments { get; set; }
        public List<CreditCardDto> CreditCards { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
