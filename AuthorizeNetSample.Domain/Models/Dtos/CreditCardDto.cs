using AuthorizeNetSample.Domain.Models.Dtos.Base;
using System;
using System.Collections.Generic;

namespace AuthorizeNetSample.Domain.Models.Dtos {
    public class CreditCardDto : BaseEntityDto<Guid> {
        public string LastFourDigits { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumHash { get; set; }
        public string ExpDate { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public List<AddressDto> BillingAddresses { get; set; }
    }
}
