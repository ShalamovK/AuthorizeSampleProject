namespace AuthorizeNetSample.PaymentSystem.Models.Payment
{
	public class InvoiceLine
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
