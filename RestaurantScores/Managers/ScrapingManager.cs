using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RestaurantScores.Managers.Interfaces;
using RestaurantScores.Models;

namespace RestaurantScores.Managers
{
	public class ScrapingManager : IScrapingManager
	{
		public List<ResultsViewModel> ScrapeRestaurantReviewSites(List<Review> reviewSitesToScrape, List<ReviewerScrapingDetails> reviewers)
		{
			var results = new List<ResultsViewModel>();
			foreach (var scrapingDetails in reviewers)
			{
				var filteredReviewSitesToScrape = reviewSitesToScrape.FirstOrDefault(x => x.Url.Contains(scrapingDetails.WebAddress.Trim()));

				if (filteredReviewSitesToScrape != null)
				{
					//Should this call be async using await>???
					var reviewerRating = GetReviewValuesFromHtml(filteredReviewSitesToScrape.Url.Trim(), scrapingDetails.NumberOfReviewsHtml.Trim(), scrapingDetails.NumberOfReviewsHtmlAttribute?.Trim(), scrapingDetails.OverallScoreHtml.Trim(), scrapingDetails.OverallScoreHtmlAttribute?.Trim());

					results.Add(new ResultsViewModel()
					{
						restaurant = new Restaurant()
						{
							Name = "",
							Url = ""
						},
						Review = new Review()
						{
							Name = filteredReviewSitesToScrape.Name,
							Url = filteredReviewSitesToScrape.Url,
							NumberOfReviews = reviewerRating.Result[0],
							OverallScore = Convert.ToDouble(reviewerRating.Result[1].Trim()),
						},
						ReviewerScrapingDetails = new ReviewerScrapingDetails()
						{
							Name = scrapingDetails.Name,
							WebAddress = scrapingDetails.WebAddress,
							OverallMaxScore = scrapingDetails.OverallMaxScore
						}
					});
				}
			}

			return results;
		}

		//TODO: Investigate if I need to dispose of connection??
		private async Task<List<string>> GetReviewValuesFromHtml(string uri, string numberOfRatingsHtmlTag, string numberOfRatingsHtmlAttribute, string overallRatingHtmlTag, string overallRatingHtmlAttribute)
		{
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
			HtmlWeb web = new HtmlWeb();
			//TODO: Is ths an async call???? If not it needs to be!!!
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
