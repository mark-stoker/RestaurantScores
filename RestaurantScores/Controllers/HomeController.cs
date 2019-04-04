using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RestaurantScores.Models;
using RestaurantScores.Managers.Interfaces;

namespace RestaurantScores.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDatabaseManager _databaseManager;
		private readonly IScrapingManager _scrapingManager;
		private readonly IWebSearchManager _webSearchManager;

		public HomeController(IDatabaseManager databaseManager, IScrapingManager scrapingManager, IWebSearchManager webSearchManager)
		{
			_databaseManager = databaseManager;
			_scrapingManager = scrapingManager;
			_webSearchManager = webSearchManager;
		}

		public IActionResult Index()
        {
            return View();
        }

	    public IActionResult Search(Search search)
	    {
		    var webSearchResults = _webSearchManager.BingWebSearch(search.SearchString);
		    var reviewersScrapingData = _databaseManager.GetScrapingData();
		    var results = _scrapingManager.ScrapeRestaurantReviewSites(webSearchResults, reviewersScrapingData, search.SearchString);

			//TODO if result == null then show message asking user to expand their results
			
			return View("Results", results);
		}

		public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
