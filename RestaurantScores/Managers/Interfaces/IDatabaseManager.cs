using System.Collections.Generic;
using RestaurantScores.Models;

namespace RestaurantScores.Managers.Interfaces
{
	public interface IDatabaseManager
	{
		List<ReviewerScrapingDetails> GetScrapingData();
	}
}
