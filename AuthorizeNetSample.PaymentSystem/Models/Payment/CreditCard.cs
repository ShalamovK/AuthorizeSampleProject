namespace AuthorizeNetSample.PaymentSystem.Models.Payment
{
	public class CreditCard
	{
		public string CardNumber { get; set; }
		public string CVC { get; set; }
		public string ExpDate { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
