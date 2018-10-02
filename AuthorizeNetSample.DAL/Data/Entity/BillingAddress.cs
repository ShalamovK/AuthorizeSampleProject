namespace AuthorizeNetSample.DAL.Data.Entity {
    public class BillingAddress : Address {
        public virtual CreditCard CreditCard { get; set; }
    }
}
