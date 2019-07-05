using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RestaurantScores.Exceptions;
using RestaurantScores.Managers.Interfaces;
using RestaurantScores.Models;

namespace RestaurantScores.Managers
{
	public class ScrapingManager : IScrapingManager
	{
		public List<ResultsViewModel> ScrapeRestaurantReviewSites(List<Review> reviewSites, List<ReviewerScrapingDetails> reviewersScrapingDetails, string searchString)
		{
			var results = new List<ResultsViewModel>();
			foreach (var scrapingDetails in reviewersScrapingDetails)
			{
				var filteredReviewSitesToScrape = reviewSites.FirstOrDefault(x => x.Url.Contains(scrapingDetails.WebAddress.Trim()));

				if (filteredReviewSitesToScrape != null)
				{
					//Should this call be async using await>???
					var reviewerRating = GetReviewValuesFromHtml(filteredReviewSitesToScrape?.Url?.Trim(), scrapingDetails?.NumberOfReviewsHtml?.Trim(), scrapingDetails.NumberOfReviewsHtmlAttribute?.Trim(), scrapingDetails?.OverallScoreHtml?.Trim(), scrapingDetails?.OverallScoreHtmlAttribute?.Trim());

					results.Add(new ResultsViewModel()
					{
						Restaurant = new Restaurant()
						{
							Name = searchString,
							Url = ""
						},
						Review = new Review()
						{
							Name = filteredReviewSitesToScrape?.Name,
							Url = filteredReviewSitesToScrape?.Url,
							NumberOfReviews = int.Parse(reviewerRating?.Result[0], NumberStyles.AllowThousands),
							OverallScore = Convert.ToDouble(reviewerRating?.Result[1].Trim()),
						},
						ReviewerScrapingDetails = new ReviewerScrapingDetails()
						{
							Name = scrapingDetails?.Name,
							WebAddress = filteredReviewSitesToScrape?.Url, //TODO: change back to scrapingDetails.WebAddress,
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
			
			//if (uri != null)
			//{
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
			var web = new HtmlWeb
			{
				AutoDetectEncoding = true
			};
			//TODO: Is ths an async call???? If not it needs to be!!!
			//Todo: if Internet connection is lost here may need to handle it
			//Todo: Some sites seem to block this approach e.g. Zomato why? also need error handling
			//This may need to be abstracted out of here for mocking purposes

			try
			{
				var htmlDoc = await Task.Factory.StartNew(() => web.Load(uri));

				//What if there are no reviews available?? return 0
				string reviewCount = ExtractReviewCountFromHtml(numberOfRatingsHtmlTag, numberOfRatingsHtmlAttribute, htmlDoc);
				string ratingValue = ExtractRatingFromHtml(overallRatingHtmlTag, overallRatingHtmlAttribute, htmlDoc);

				var result = new List<string>
				{
					reviewCount,
					ratingValue
				};

				return result;
			}
			catch(IncorrectScrapingAttributesException exception)
			{
				throw new Exception("Scraping details for Company are incorrect.", exception);
				//TODO this needs to be refactored, causing a warning
			}
		}
		
		private static string ExtractReviewCountFromHtml(string numberOfRatingsHtmlTag, string numberOfRatingsHtmlAttribute, HtmlDocument htmlDoc)
		{
			if (numberOfRatingsHtmlAttribute == null)
			return ExtractFromHtmlTag(numberOfRatingsHtmlTag, htmlDoc);
			
				return ExtractFromHtmlAttribute(numberOfRatingsHtmlTag, numberOfRatingsHtmlAttribute, htmlDoc);
		}

		private static string ExtractRatingFromHtml(string overallRatingHtmlTag, string overallRatngHtmlAttribte, HtmlDocument htmlDoc)
		{
			if (overallRatngHtmlAttribte == null)
			return ExtractFromHtmlTag(overallRatingHtmlTag, htmlDoc);
			
				return ExtractFromHtmlAttribute(overallRatingHtmlTag, overallRatngHtmlAttribte, htmlDoc);
		}

		private static string ExtractFromHtmlTag(string numberOfRatingsHtmlTag, HtmlDocument htmlDoc)
		{
			string result;
			if (htmlDoc.DocumentNode.SelectSingleNode(numberOfRatingsHtmlTag) != null)
			{
				result = htmlDoc.DocumentNode.SelectSingleNode(numberOfRatingsHtmlTag).InnerText;
				return result.Trim().IndexOf(" ") >= 0 ? result.Trim().Substring(0, result.Trim().IndexOf(" ", StringComparison.Ordinal)) : result.Trim();
			}

			return "0";
		}

		private static string ExtractFromHtmlAttribute(string numberOfRatingsHtmlTag, string numberOfRatingsHtmlAttribute, HtmlDocument htmlDoc)
		{
			string result;
			if (htmlDoc.DocumentNode.SelectSingleNode(numberOfRatingsHtmlTag) != null)
			{
				result = htmlDoc.DocumentNode.SelectSingleNode(numberOfRatingsHtmlTag).Attributes[numberOfRatingsHtmlAttribute].Value;
				return result.Trim().IndexOf(" ") >= 0 ? result.Trim().Substring(0, result.Trim().IndexOf(" ", StringComparison.Ordinal)) : result.Trim();
			}

			return "0";
		}
	}
}
