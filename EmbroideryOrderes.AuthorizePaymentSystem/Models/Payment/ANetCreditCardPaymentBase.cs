namespace EmbroideryOrderes.AuthorizePaymentSystem.Models.Payment {
    public class ANetCreditCardPaymentBase {
        public decimal Amount { get; set; }
        public ANetCardModel Card { get; set; }
        public ANetAddressModel BillAddress { get; set; }
    }
}
