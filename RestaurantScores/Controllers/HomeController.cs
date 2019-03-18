using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantScores.Models;
using HtmlAgilityPack;
using RestaurantScores.Managers;

namespace RestaurantScores.Controllers
{
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		
		[HttpPost]
		public IActionResult Results(Restaurant restaurant)
        {
	        var databaseManager = new DatabaseManager();
			var reviewers = databaseManager.GetSearchData();
			var scrapingManager = new ScrapingManager();
			var results = new List<ResultsViewModel>();

			scrapingManager.RenderScrapingResults(restaurant, scrapingManager, reviewers, results);

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
