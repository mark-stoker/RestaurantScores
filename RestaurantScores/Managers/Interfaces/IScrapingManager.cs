using System.Collections.Generic;
using RestaurantScores.Models;

namespace RestaurantScores.Managers.Interfaces
{
	public interface IScrapingManager
	{
		List<ResultsViewModel> ScrapeRestaurantReviewSites(List<Review> revieweSitesToScrape, List<ReviewerScrapingDetails> reviewers);
	}
}
