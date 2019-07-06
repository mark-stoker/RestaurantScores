using System;

namespace RestaurantScores.Exceptions
{
	public class IncorrectScrapingPropertiesException : Exception
	{
		public IncorrectScrapingPropertiesException()
		{
		}

		public IncorrectScrapingPropertiesException(string message) : base(message)
		{
		}
	}
}
