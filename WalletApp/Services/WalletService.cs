using WalletApp.DTOs;
using WalletApp.DTOs.Requests;
using WalletApp.Models;
using WalletApp.Types.Enums;

namespace WalletApp.Services
{
	public class WalletService : IWalletService
	{
		private const int MaxLimit = 1500;
		private const int LatestTransactionsCount = 10;
		private const string DefaultBackground = "#B9B9B9";

		private readonly Random _random;
		private readonly WalletAppContext _context;

		public WalletService(WalletAppContext walletAppContext)
		{
			_random = new Random();
			_context = walletAppContext;
		}

		public int GetCardBalance()
		{
			int balance = _random.Next(MaxLimit);
			return balance;
		}

		public int GetAvailableBalance()
		{
			int available = MaxLimit - GetCardBalance();
			return available;
		}

		public string GetPaymentDueMessage()
		{
			return "You've paid your " + DateTime.Today.ToString("MMMM") + " balance.";
		}

		public TransactionDto GetTransactionById(int id)
		{
			Transaction transaction = _context.Transactions.FirstOrDefault(t => t.Id == id);
			if (transaction == null)
			{
				throw new Exception($"Transaction with ID {id} not found.");
			}
			return new TransactionDto()
			{
				Type = transaction.Type,
				Amount = transaction.Type == TransactionType.Payment ? $"+${transaction.Amount}" : $"${transaction.Amount}",
				Name = transaction.Name,
				Description = transaction.Description,
				Pending = transaction.Pending,
				Date = GetTransactionDate(transaction.Date),
				Icon = transaction.Icon,
				AuthorizedUser = transaction.AuthorizedUser,
				UserId = transaction.UserId
			};
		}

		public TransactionsDto GetLatestTransactions(int userId)
		{
			List<Transaction> allTransactions = _context.Transactions.Where(t => t.UserId == userId).ToList();
			List<Transaction> orderedTransactions = allTransactions.OrderByDescending(t => t.Date).ToList();
			List<Transaction> latestTransactions = orderedTransactions.Take(LatestTransactionsCount).ToList();

			return new TransactionsDto(latestTransactions.Select(t => new TransactionDto
			{
				Type = t.Type,
				Amount = t.Type == TransactionType.Payment ? $"+${t.Amount}" : $"${t.Amount}",
				Name = t.Name,
				Description = t.Description,
				Date = GetTransactionDate(t.Date),
				Pending = t.Pending,
				Icon = t.Icon,
				AuthorizedUser = t.AuthorizedUser,
				UserId = t.UserId
			}).ToList()); ;
		}

		public UsersDto GetUsers()
		{
			var users = _context.Users.ToList();
			return new UsersDto(users.Select(u => new UserDto(u.Name, u.CardBalance, u.CardLimit)).ToList());
		}

		public TransactionsDto GetUserTransactions(int userId)
		{
			var transactions = _context.Transactions.Where(t => t.UserId == userId).ToList();
			return new TransactionsDto(transactions.Select(t => new TransactionDto
			{
				Type = t.Type,
				Amount = t.Type == TransactionType.Payment ? $"+${t.Amount}" : $"${t.Amount}",
				Name = t.Name,
				Description = t.Description,
				Pending = t.Pending,
				Date = GetTransactionDate(t.Date),
				Icon = t.Icon,
				AuthorizedUser = t.AuthorizedUser,
				UserId = t.UserId
			}).ToList());
		}

		public UserDto CreateUser(UserDto request)
		{
			var user = new User
			{
				Name = request.Name,
				CardBalance = request.CardBalance,
				CardLimit = request.CardLimit
			};
			_context.Users.Add(user);
			_context.SaveChanges();
			return new UserDto(user.Name, user.CardBalance, user.CardLimit);
		}

		public TransactionDto CreateTransaction(CreateTransactionRequest request)
		{
			var user = _context.Users.FirstOrDefault(u => u.Id == request.UserId);
			if (user == null)
			{
				throw new Exception($"User with ID {request.UserId} not found.");
			}

			var transaction = new Transaction
			{
				Amount = request.Amount,
				Type = request.Type,
				Name = request.Name,
				Description = request.Pending ? "Pending " + request.Description : request.Description,
				Date = DateTime.Now,
				AuthorizedUser = request.AuthorizedUser,
				Pending = request.Pending,
				Icon = string.IsNullOrEmpty(request.Icon) ? DefaultBackground : request.Icon,
				User = user
			};
			_context.Transactions.Add(transaction);
			_context.SaveChanges();

			return new TransactionDto()
			{
				Type = transaction.Type,
				Amount = transaction.Type == TransactionType.Payment ? $"+${transaction.Amount}" : $"${transaction.Amount}",
				Name = transaction.Name,
				Description = transaction.Description,
				Pending = transaction.Pending,
				Icon = transaction.Icon,
				AuthorizedUser = transaction.AuthorizedUser,
				UserId = user.Id
			};
		}

		public bool DeleteTransaction(int id)
		{
			Transaction transaction = _context.Transactions.FirstOrDefault(t => t.Id == id);
			if (transaction == null)
			{
				return false;
			}
			_context.Transactions.Remove(transaction);
			_context.SaveChanges();
			return true;
		}

		private string GetTransactionDate(DateTime date)
		{
			string transactionDate;
			if ((DateTime.Now - date).TotalDays <= 7)
			{
				// Display day name for transactions that happened within the last week
				transactionDate = date.ToString("dddd");
			}
			else
			{
				// Display full date for transactions that happened more than a week ago
				transactionDate = date.ToShortDateString();
			}

			return transactionDate;
		}

		public int CalculateDailyPoints(DateTime date)
		{
			int seasonStartDay;
			switch (date.Month)
			{
				case 12:
				case 1:
				case 2:
					seasonStartDay = 1; // winter
					break;
				case 3:
				case 4:
				case 5:
					seasonStartDay = 61; // spring
					break;
				case 6:
				case 7:
				case 8:
					seasonStartDay = 152; // summer
					break;
				case 9:
				case 10:
				case 11:
				default:
					seasonStartDay = 244; // autumn
					break;
			}

			int daysSinceStart = (date - new DateTime(date.Year, 1, 1)).Days + 1;
			int daysSinceSeasonStart = daysSinceStart - seasonStartDay;

			if (daysSinceSeasonStart == 0)
			{
				return 2;
			}
			else if (daysSinceSeasonStart == 1)
			{
				return 3;
			}
			else
			{
				int previousPoints = CalculateDailyPoints(date.AddDays(-1));
				int points = previousPoints + (int)(previousPoints * 0.6);

				if (points >= 1000)
				{
					points = (points / 1000) * 1000;
				}

				return points;
			}
		}


	}
}
