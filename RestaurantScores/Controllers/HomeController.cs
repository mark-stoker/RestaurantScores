using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantScores.Models;
using HtmlAgilityPack;

namespace RestaurantScores.Controllers
{
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Restaurant(Restaurant restaurant)
        {
            return RedirectToAction("Results", restaurant);
        }

        public IActionResult Results(Restaurant restaurant)
        {
	        var context = new ReviewersContext();
	        var reviewers = context.Reviewer.ToList();					

            List<ResultsViewModel> results = new List<ResultsViewModel>();

	        foreach (var reviewer in reviewers)
	        {
		        var reviewerRating = GetReviewValuesFromHtml(reviewer.WebAddress.Trim(), reviewer.NumberOfReviewsHtml.Trim(), reviewer.NumberOfReviewsHtmlAttribute?.Trim(), reviewer.OverallScoreHtml.Trim(), reviewer.OverallScoreHtmlAttribute?.Trim());

		        results.Add(new ResultsViewModel()
		        {
			        restaurant = new Restaurant()
			        {
				        Name = restaurant.Name,
				        PostCode = restaurant.PostCode
			        },
			        reviewer = new Reviewer()
			        {
						Name = reviewer.Name,
						WebAddress = reviewer.WebAddress,
				        NumberOfReviews = reviewerRating.Result[0],
						OverallScore = Convert.ToDouble(reviewerRating.Result[1].Trim()),
						OverallMaxScore = reviewer.OverallMaxScore
					}
				});
			}

			return View("Results", results);
        }

        private static String BetweenStrings(String text, String start, String end)
        {
            int p1 = text.IndexOf(start) + start.Length;
            int p2 = text.IndexOf(end, p1);

            if (end == "") return (text.Substring(p1));
            else return text.Substring(p1, p2 - p1);
        }

		//TODO: Investigate if I need to dispose of connection??
		private static async Task<List<string>> GetReviewValuesFromHtml(string uri, string numberOfRatingsHtmlTag, string numberOfRatingsHtmlAttribute, string overallRatingHtmlTag, string overallRatingHtmlAttribute)
		{
			HtmlWeb web = new HtmlWeb();
			//TODO: Is ths an async call????
			//Todo: if Internet connection is lost here may need to handle it
			//Todo: Some sites seem to block this approach e.g. Zomato why? also need error handling
			var htmlDoc = await Task.Factory.StartNew(() => web.Load(uri));
			string reviewCount = ExtractReviewCountFromHtml(numberOfRatingsHtmlTag, numberOfRatingsHtmlAttribute, htmlDoc);
			string ratingValue = ExtractRatingFromHtml(overallRatingHtmlTag, overallRatingHtmlAttribute, htmlDoc);
			

			var result = new List<string>
			{
				reviewCount,
				ratingValue
				
			};

			return result;
		}

		private static string ExtractReviewCountFromHtml(string numberOfRatingsHtmlTag, string numberOfRatingsHtmlAttribute, HtmlDocument htmlDoc)
		{
			if (numberOfRatingsHtmlAttribute == null)
			{
				return htmlDoc.DocumentNode.SelectSingleNode(numberOfRatingsHtmlTag).InnerText;
			}
			else
			{
				return htmlDoc.DocumentNode.SelectSingleNode(numberOfRatingsHtmlTag).Attributes[numberOfRatingsHtmlAttribute].Value;
			}

		}

		private static string ExtractRatingFromHtml(string overallRatingHtmlTag, string overallRatngHtmlAttribte, HtmlDocument htmlDoc)
		{
			
			if (overallRatngHtmlAttribte == null)
			{
				return htmlDoc.DocumentNode.SelectSingleNode(overallRatingHtmlTag).InnerText;
			}
			else
			{
				return htmlDoc.DocumentNode.SelectSingleNode(overallRatingHtmlTag).Attributes[overallRatngHtmlAttribte].Value;
			}
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
