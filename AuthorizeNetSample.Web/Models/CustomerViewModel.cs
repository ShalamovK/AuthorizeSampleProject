using AuthorizeNetSample.Web.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AuthorizeNetSample.Web.Models {
    public class CustomerViewModel : BaseEntityViewModel<Guid> {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public List<PaymentViewModel> Payments { get; set; }
        public List<CreditCardViewModel> CreditCards { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
    }
}