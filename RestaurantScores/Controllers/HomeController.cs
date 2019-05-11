using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestaurantScores.Managers;
using RestaurantScores.Models;
using RestaurantScores.Managers.Interfaces;

namespace RestaurantScores.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDatabaseManager _databaseManager;
		private readonly IScrapingManager _scrapingManager;
		private readonly IWebSearchManager _webSearchManager;
		private readonly IHostingEnvironment _hostingEnvironment;

		public HomeController(IDatabaseManager databaseManager, IScrapingManager scrapingManager, IWebSearchManager webSearchManager, IHostingEnvironment hostingEnvironment)
		{
			_databaseManager = databaseManager;
			_scrapingManager = scrapingManager;
			_webSearchManager = webSearchManager;
			_hostingEnvironment = hostingEnvironment;
		}

		public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Search(Search search)
	    {
		    if (ModelState.IsValid && !_hostingEnvironment.IsDevelopment())
		    {
			    var recaptchaManager = new RecaptchaManager();

				if (!recaptchaManager.ReCaptchaPassed(Request.Form["g-recaptcha-response"], Environment.GetEnvironmentVariable("GoogleRecaptcha-Secret")))
			    {
					ModelState.AddModelError(string.Empty, "Please tick the box below to search.");
					return View("Index");
			    }
		    }

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
