namespace WalletApp.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal CardBalance { get; set; }
		public decimal CardLimit { get; set; }
		public ICollection<Transaction> Transactions { get; set; }
		public ICollection<DailyPoints> DailyPoints { get; set; }
	}
}
