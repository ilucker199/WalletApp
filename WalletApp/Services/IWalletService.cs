using WalletApp.DTOs;
using WalletApp.DTOs.Requests;

namespace WalletApp.Services
{
	public interface IWalletService
	{
		public int GetCardBalance();
		public int GetAvailableBalance();
		public string GetPaymentDueMessage();
		public TransactionDto GetTransactionById(int id);
		public TransactionsDto GetLatestTransactions(int userId);
		public UsersDto GetUsers();
		public TransactionsDto GetUserTransactions(int userId);
		public UserDto CreateUser(UserDto request);
		public TransactionDto CreateTransaction(CreateTransactionRequest request);
		public bool DeleteTransaction(int id);
		public int CalculateDailyPoints(DateTime date);
	}
}
