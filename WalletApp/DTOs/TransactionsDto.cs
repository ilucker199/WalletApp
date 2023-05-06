namespace WalletApp.DTOs
{
	public class TransactionsDto
	{
		public List<TransactionDto> Transactions { get; set; }

		public TransactionsDto(List<TransactionDto> transactions)
		{
			Transactions = transactions;
		}
	}
}
