using System;

namespace EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment {
    public class ANetPaymentCharge {
        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
