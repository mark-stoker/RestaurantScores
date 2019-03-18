using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RestaurantScores.Models;

namespace RestaurantScores.Managers
{
	public class ScrapingManager
	{
		public void RenderScrapingResults(Restaurant restaurant, ScrapingManager scrapingManager, List<Reviewer> reviewers, List<ResultsViewModel> results)
		{
			foreach (var reviewer in reviewers)
			{
				var reviewerRating = scrapingManager.GetReviewValuesFromHtml(reviewer.WebAddress.Trim(), reviewer.NumberOfReviewsHtml.Trim(), reviewer.NumberOfReviewsHtmlAttribute?.Trim(), reviewer.OverallScoreHtml.Trim(), reviewer.OverallScoreHtmlAttribute?.Trim());

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
		}

		//TODO: Investigate if I need to dispose of connection??
		private async Task<List<string>> GetReviewValuesFromHtml(string uri, string numberOfRatingsHtmlTag, string numberOfRatingsHtmlAttribute, string overallRatingHtmlTag, string overallRatingHtmlAttribute)
		{
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
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
	}
}
