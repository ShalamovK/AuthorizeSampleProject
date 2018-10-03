namespace EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment.Base {
    public class PaymentBase {
        public string CustomerPO { get; set; }
        public string OrderNum { get; set; }
        public decimal Amount { get; set; }
        public ANetAddressModel ShipAddress { get; set; }
    }
}
