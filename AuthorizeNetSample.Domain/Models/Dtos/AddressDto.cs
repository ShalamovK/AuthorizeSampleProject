using AuthorizeNetSample.Domain.Models.Dtos.Base;
using System;

namespace AuthorizeNetSample.Domain.Models.Dtos {
    public class AddressDto : BaseEntityDto<Guid> {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual CustomerDto Customer { get; set; }
        public Guid? CreditCardId { get; set; }
        public virtual CreditCardDto CreditCard { get; set; }
    }
}
