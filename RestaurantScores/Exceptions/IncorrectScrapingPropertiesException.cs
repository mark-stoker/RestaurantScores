using System;

namespace RestaurantScores.Exceptions
{
	public class IncorrectScrapingAttributesException : Exception
	{
		public IncorrectScrapingAttributesException()
		{
		}

		public IncorrectScrapingAttributesException(string message) : base(message)
		{
		}
	}
}
