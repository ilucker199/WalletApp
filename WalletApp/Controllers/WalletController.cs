using Microsoft.AspNetCore.Mvc;
using WalletApp.DTOs;
using WalletApp.DTOs.Requests;
using WalletApp.Services;

namespace WalletApp.Controllers
{
	[ApiController]
	[Route("api")]
	public class WalletController : ControllerBase
	{
		private readonly IWalletService _walletService;

		public WalletController(IWalletService walletService)
		{
			_walletService = walletService;
		}

		[HttpGet]
		[Route("card-balance")]
		public ActionResult<GetCardBalanceDto> GetCardBalance()
		{
			var cardBalance = _walletService.GetCardBalance();
			return Ok(new GetCardBalanceDto(cardBalance));
		}

		[HttpGet]
		[Route("payment-due")]
		public ActionResult<GetPaymentDueDto> GetPaymentDue()
		{
			var paymentDue = _walletService.GetPaymentDueMessage();
			return Ok(new GetPaymentDueDto(paymentDue));
		}

		[HttpGet]
		[Route("daily-points")]
		public ActionResult<int> GetDailyPoints()
		{
			var dailyPoints = _walletService.CalculateDailyPoints(DateTime.Now);
			return Ok(new GetDailyPointsDto(dailyPoints));
		}

		[HttpGet]
		[Route("latest-transactions")]
		public ActionResult<TransactionsDto> GetLatestTransactions(int userId)
		{
			var latestTransactions = _walletService.GetLatestTransactions(userId);
			return Ok(latestTransactions);
		}

		[HttpGet]
		[Route("transactions/{id}")]
		public ActionResult<TransactionDto> GetTransactionById(int id)
		{
			var transaction = _walletService.GetTransactionById(id);
			if (transaction == null)
			{
				return NotFound();
			}
			return Ok(transaction);
		}

		[HttpGet]
		[Route("users")]
		public ActionResult<UsersDto> GetUsers()
		{
			var users = _walletService.GetUsers();
			return Ok(users);
		}

		[HttpGet]
		[Route("users/{id}/transactions")]
		public ActionResult<TransactionsDto> GetUserTransactions(int id)
		{
			var userTransactions = _walletService.GetUserTransactions(id);
			return Ok(userTransactions);
		}

		[HttpPost]
		[Route("users/create")]
		public ActionResult<UserDto> CreateUser([FromBody] UserDto request)
		{
			var user = _walletService.CreateUser(request);
			return Ok(user);
		}

		[HttpPost]
		[Route("transactions/create")]
		public ActionResult<TransactionDto> CreateTransaction([FromBody] CreateTransactionRequest request)
		{
			var transaction = _walletService.CreateTransaction(request);
			return Ok(transaction);
		}

		[HttpDelete]
		[Route("transactions/delete/{id}")]
		public ActionResult DeleteTransaction(int id)
		{
			if (!_walletService.DeleteTransaction(id))
			{
				return NotFound();
			}
			return NoContent();
		}
	}

}
