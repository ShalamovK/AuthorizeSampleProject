namespace AuthorizeNetSample.Web.Models.Authorize {
    public class CustomerPaymentProfileInfoViewModel {
        public bool Default { get; set; }
        public bool DefaultSpecified { get; set; }
        public string PaymentProfileId { get; set; }
        public string ProfileId { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNum { get; set; }
    }
}