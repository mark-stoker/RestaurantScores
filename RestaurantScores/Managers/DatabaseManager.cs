using System.Collections.Generic;
using System.Linq;
using RestaurantScores.Managers.Interfaces;
using RestaurantScores.Models;

namespace RestaurantScores.Managers
{
	public class DatabaseManager : IDatabaseManager
	{
		public List<ReviewerScrapingDetails> GetScrapingData()
		{
			var context = new ReviewerScrpaingDetailsContext();
			var reviewers = context.ReviewerScrapingDetails.ToList();
			return reviewers;
		}
	}
}
