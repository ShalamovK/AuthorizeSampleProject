using AuthorizeNetSample.Web.Models.Base;
using System;

namespace AuthorizeNetSample.Web.Models {
    public class AddressViewModel : BaseEntityViewModel<Guid> {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual CustomerViewModel Customer { get; set; }
        public Guid? CreditCardId { get; set; }
        public virtual CreditCardViewModel CreditCard { get; set; }
    }
}