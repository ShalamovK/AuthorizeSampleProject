using AuthorizeNetSample.Web.Models.Base;
using System;

namespace AuthorizeNetSample.Web.Models {
    public class PaymentViewModel : BaseEntityViewModel<Guid> {
        public string TransactionId { get; set; }
        public string AuthKey { get; set; }
        public decimal Amount { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}