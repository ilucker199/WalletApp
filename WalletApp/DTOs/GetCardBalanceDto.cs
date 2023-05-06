using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApp.DTOs
{
	public class GetCardBalanceDto
	{
		public int CardBalance { get; set; }

		public GetCardBalanceDto(int cardBalance)
		{
			CardBalance = cardBalance;
		}
	}
}
