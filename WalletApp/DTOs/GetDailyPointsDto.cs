namespace WalletApp.DTOs
{
	public class GetDailyPointsDto
	{
		public int DailyPoints { get; set; }

		public GetDailyPointsDto(int dailyPoints)
		{
			DailyPoints = dailyPoints;
		}
	}
}
