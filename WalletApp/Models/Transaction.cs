using WalletApp.Types.Enums;

namespace WalletApp.Models
{
	public class Transaction
	{
		public int Id { get; set; }
		public TransactionType Type { get; set; }
		public decimal Amount { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public bool Pending { get; set; }
		public string Icon { get; set; }
		public string AuthorizedUser { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
