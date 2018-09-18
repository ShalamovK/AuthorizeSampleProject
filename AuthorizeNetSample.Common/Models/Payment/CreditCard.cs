namespace AuthorizeNetSample.Common.Models.Payment
{
	public class CreditCard
	{
		public string CardNumber { get; set; }
		public string CVC { get; set; }
		public string ExpDate { get; set; }
		public string CardHolderFirstName { get; set; }
		public string CardHolderLastName { get; set; }
		public Address BillAddress { get; set; }
		public Address ShipAddress { get; set; }
	}
}
