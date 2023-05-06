namespace WalletApp.DTOs
{
	public class UserDto
	{
		public string Name { get; set; }
		public decimal CardBalance { get; set; }
		public decimal CardLimit { get; set; }

		public UserDto(string name, decimal cardBalance, decimal cardLimit)
		{
			Name = name;
			CardBalance = cardBalance;
			CardLimit = cardLimit;
		}
	}
}
