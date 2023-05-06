namespace WalletApp.DTOs
{
	public class UsersDto
	{
		public List<UserDto> Users { get; set; }

		public UsersDto(List<UserDto> users)
		{
			Users = users;
		}
	}
}
