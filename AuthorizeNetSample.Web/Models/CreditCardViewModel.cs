using AuthorizeNetSample.Web.Models.Base;
using System;
using System.Collections.Generic;

namespace AuthorizeNetSample.Web.Models {
    public class CreditCardViewModel : BaseEntityViewModel<Guid> {
        public string LastFourDigits { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumHash { get; set; }
        public string ExpDate { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
        public List<AddressViewModel> BillingAddresses { get; set; }
    }
}