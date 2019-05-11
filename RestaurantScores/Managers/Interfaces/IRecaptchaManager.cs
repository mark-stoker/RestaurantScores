namespace RestaurantScores.Managers.Interfaces
{
	public interface IRecaptchaManager
	{
		bool ReCaptchaPassed(string gRecaptchaResponse, string secret);
	}
}
