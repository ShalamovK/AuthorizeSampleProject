using AuthorizeNetSample.Domain.Models.Dtos.Base;
using System;

namespace AuthorizeNetSample.Domain.Models.Dtos {
    public class PaymentDto : BaseEntityDto<Guid> {
        public string TransactionId { get; set; }
        public string AuthKey { get; set; }
        public decimal Amount { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
