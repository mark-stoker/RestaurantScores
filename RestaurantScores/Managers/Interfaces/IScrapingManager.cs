using System.Collections.Generic;
using RestaurantScores.Models;

namespace RestaurantScores.Managers.Interfaces
{
	public interface IScrapingManager
	{
		List<ResultsViewModel> ScrapeRestaurantReviewSites(List<Restaurant> restaurantSitesToScrape, List<Reviewer> reviewers);
	}
}
