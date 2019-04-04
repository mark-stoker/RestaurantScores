using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RestaurantScores.Models;

namespace RestaurantScores.Managers.Interfaces
{
	public interface IScrapingManager
	{
		List<ResultsViewModel> ScrapeRestaurantReviewSites(List<Review> reviewSites,
			List<ReviewerScrapingDetails> reviewersScrapingDetails, string searchString);
	}
}
