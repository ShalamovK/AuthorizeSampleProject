using AuthorizeNetSample.Web.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AuthorizeNetSample.Web.Models {
    public class CreditCardViewModel : BaseEntityViewModel<Guid> {
        public string LastFourDigits { get; set; }
        [DisplayName("Card Number")]
        public string CardNum { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumHash { get; set; }
        [DisplayName("Expiration Date")]
        public string ExpDate { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
        public List<AddressViewModel> BillingAddresses { get; set; }
    }
}