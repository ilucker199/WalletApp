namespace WalletApp.DTOs
{
	public class GetPaymentDueDto
	{
		public string PaymentDue { get; set; }

		public GetPaymentDueDto(string paymentDue)
		{
			PaymentDue = paymentDue;
		}
	}
}
