using WalletApp.Types.Enums;

namespace WalletApp.DTOs
{
	public class TransactionDto
	{
		public TransactionType Type { get; set; }
		public string Amount { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Date { get; set; }
		public bool Pending { get; set; }
		public string Icon { get; set; }
		public string AuthorizedUser { get; set; }
		public int UserId { get; set; }
	}
}
