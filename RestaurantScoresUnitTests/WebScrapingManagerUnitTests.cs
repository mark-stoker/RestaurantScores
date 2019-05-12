using System.Collections.Generic;
using NUnit.Framework;
using RestaurantScores.Managers;
using RestaurantScores.Models;
using RestaurantScoresUnitTests.Builders;

namespace Tests
{
	public class WebScrapingManagerUnitTests
	{
		private ScrapingManager _scrapingManager = new ScrapingManager();
		private List<Review> _reviewSitestToScrape = new List<Review>();
		private List<ReviewerScrapingDetails> _reviewersScrapingDetails = new List<ReviewerScrapingDetails>();

		[SetUp]
		public void Setup()
		{
			_reviewersScrapingDetails = new ReviewerScrapingDetailsBuilder()
				.WithReviewersScrapingDetails(new List<ReviewerScrapingDetails>
				{
					new ReviewerScrapingDetailsBuilder()
						.WithId(1)
						.WithName("TripAdvisor")
						.WithWebAddress("www.tripadvisor.co.uk")
						.WithNumberOfReviewsHtml("//span[contains(@class, \'reviewCount\')]")
						.WithNumberOfReviewsHtmlAttribute(null)
						.WithOverallScoreHtml("//span[contains(@class, \'restaurants-detail-overview-cards\')]  ")
						.WithOverallScoreHtmlAttribute(null)
						.WithOverallMaxScore(5)
						.Build(),
					new ReviewerScrapingDetailsBuilder()
						.WithId(2)
						.WithName("Open Table")
						.WithWebAddress("www.opentable.co.uk")
						.WithNumberOfReviewsHtml("//span[contains(@itemprop, \'reviewCount\')]")
						.WithNumberOfReviewsHtmlAttribute(null)
						.WithOverallScoreHtml("//div[contains(@class, \'oc-reviews-491257d8\')]")
						.WithOverallScoreHtmlAttribute(null)
						.WithOverallMaxScore(5)
						.Build(),
					new ReviewerScrapingDetailsBuilder()
						.WithId(3)
						.WithName("Timeout")
						.WithWebAddress("www.timeout.com")
						.WithNumberOfReviewsHtml("//span[contains(@class, \'js-comment_count\')]")
						.WithNumberOfReviewsHtmlAttribute(null)
						.WithOverallScoreHtml("//span[contains(@class, \'fyre-reviews-average\')] ")
						.WithOverallScoreHtmlAttribute(null)
						.WithOverallMaxScore(5)
						.Build(),
					//new ReviewerScrapingDetailsBuilder()
					//	.WithId(4)
					//	.WithName("TripExpert")
					//	.WithWebAddress("www.tripexpert.com")
					//	.WithNumberOfReviewsHtml("//meta[contains(@itemprop, \'reviewCount\')]")
					//	.WithNumberOfReviewsHtmlAttribute("content")
					//	.WithOverallScoreHtml("//span[contains(@class, \'score fleft\')]")
					//	.WithOverallScoreHtmlAttribute(null)
					//	.WithOverallMaxScore(5)
					//	.Build(),
					new ReviewerScrapingDetailsBuilder()
						.WithId(4)
						.WithName("Yelp")
						.WithWebAddress("www.yelp.com")
						.WithNumberOfReviewsHtml("//span[contains(@class, \'review-count rating-qualifier\')]")
						.WithNumberOfReviewsHtmlAttribute(null)
						.WithOverallScoreHtml("//meta[contains(@itemprop, \'ratingValue\')]")
						.WithOverallScoreHtmlAttribute("content")
						.WithOverallMaxScore(5)
						.Build(),
				}).BuildList();

			_reviewSitestToScrape = new ReviewBuilder()
				.WithReviews(new List<Review>
				{
					new ReviewBuilder()
						.WithName("The Ledbury, London - Updated 2019 Restaurant Reviews ..")
						.WithUrl("https://www.tripadvisor.co.uk/Restaurant_Review-g186338-d720761-Reviews-The_Ledbury-London_England.html")
						.Build(),
					new ReviewBuilder()
						.WithName("The Ledbury - London, | OpenTable")
						.WithUrl("https://www.opentable.co.uk/the-ledbury")
						.Build(),
					new ReviewBuilder()
						.WithName("The Ledbury | Restaurants in Westbourne Grove, London")
						.WithUrl("https://www.timeout.com/london/restaurants/the-ledbury")
						.Build(),
					new ReviewBuilder()
						.WithName("The Ledbury - Notting Hill - London, United Kingdom - Yelp")
						.WithUrl("https://www.yelp.com/biz/the-ledbury-london")
						.Build()
				}).BuildList();
		}

		[Test]
		public void SearchForRatingsAndReviewCounts_ReviewAndScrapingDataIsCorrect_ScrapingDetailsReturned()
		{
			//Arrange
			//See Setup()

			//Act
			var result = _scrapingManager.ScrapeRestaurantReviewSites(_reviewSitestToScrape, _reviewersScrapingDetails, "The Ledbury notting hill london");

			//Assert
			Assert.AreEqual(4, result.Count);

			Assert.AreEqual(result[0].Review.NumberOfReviews, 9999);
			Assert.AreEqual(result[0].Review.OverallScore, 9999);

			Assert.AreEqual(result[1].Review.NumberOfReviews, 9999);
			Assert.AreEqual(result[1].Review.OverallScore, 9999);

			Assert.AreEqual(result[2].Review.NumberOfReviews, 9999);
			Assert.AreEqual(result[2].Review.OverallScore, 9999);

			Assert.AreEqual(result[3].Review.NumberOfReviews, 9999);
			Assert.AreEqual(result[3].Review.OverallScore, 9999);
		}

		[Test]
		public void SearchForRatingsAndReviewCounts_OpenTableReviewUrlIncorrect_ZeroValuesReturnedForThatReview()
		{
			//Arrange
			_reviewSitestToScrape[1].Url = "https://www.opentable.co.uk/the-ledbury//dffgdffgsdfg////";

			//Act
			var result = _scrapingManager.ScrapeRestaurantReviewSites(_reviewSitestToScrape, _reviewersScrapingDetails,"The Ledbury notting hill london");

			//Assert
			Assert.AreEqual(result[1].Review.NumberOfReviews, 0);
			Assert.AreEqual(result[1].Review.OverallScore, 0.0);
		}

		//[Test]
		//public void SearchForRatingsAndReviewCounts_TripAdvisorReviewSetToNull_SystemNullRerenceException()
		//{
		//	//Arrange
		//	_reviewSitestToScrape[0].Url = null;

		//	//Act
		//	var result = _scrapingManager.ScrapeRestaurantReviewSites(_reviewSitestToScrape, _reviewersScrapingDetails, "The Ledbury notting hill london");

		//	//Assert
		//	Assert.AreEqual(result[0].Review.NumberOfReviews, 0);
		//	Assert.AreEqual(result[0].Review.OverallScore, 0.0);
		//}

		[Test]
		public void SearchForRatingsAndReviewCounts_TripAdvisorScrapingDataIncorrectForTotalNumberOfReivews_ZeroReturnedForTotalNmberOfReviews()
		{
			//Arrange
			_reviewersScrapingDetails[0].OverallScoreHtml = "//span[contains(@class, \'reviewCount\')]]]]]]]";

			//Act
			var result = _scrapingManager.ScrapeRestaurantReviewSites(_reviewSitestToScrape, _reviewersScrapingDetails, "The Ledbury notting hill london");

			//Assert
			Assert.AreEqual(result[0].Review.NumberOfReviews, 0);
			Assert.AreEqual(result[0].Review.OverallScore, 0.0);
		}

		[Test]
		public void SearchForRatingsAndReviewCounts_YelpScrapingDataIncorrectForOveralScore_ZeroReturnedForTotalNmberOfReviews()
		{
			//Arrange
			_reviewersScrapingDetails[3].NumberOfReviewsHtml = "///meta[contains(@itemprop, \'ratingValue\')]////";

			//Act
			var result = _scrapingManager.ScrapeRestaurantReviewSites(_reviewSitestToScrape, _reviewersScrapingDetails, "The Ledbury notting hill london");

			//Assert
			//The index here is 3 not 4 becaise TripExpert does not return a result
			Assert.AreEqual(result[3].Review.NumberOfReviews, 0);
			Assert.AreEqual(result[3].Review.OverallScore, 0.0);
		}

		[Test]
		public void SearchForRatingsAndReviewCounts_TripAdvisorReviewCountHtmlScrapingDataSetToNull_ZeroReturnedForTotalNmberOfReviews()
		{
			//Arrange
			_reviewersScrapingDetails[0].NumberOfReviewsHtml = null;

			//Act
			var result = _scrapingManager.ScrapeRestaurantReviewSites(_reviewSitestToScrape, _reviewersScrapingDetails, "The Ledbury notting hill london");

			//Assert
			Assert.AreEqual(result[0].Review.NumberOfReviews, 0);
			Assert.AreEqual(result[0].Review.OverallScore, 0.0);
		}

		[Test]
		public void SearchForRatingsAndReviewCounts_TimeoutOveralScoreHtmlScrapingDataSetToNull_ZeroReturnedForTotalNmberOfReviews()
		{
			//Arrange
			_reviewersScrapingDetails[2].OverallScoreHtml = null;

			//Act
			var result = _scrapingManager.ScrapeRestaurantReviewSites(_reviewSitestToScrape, _reviewersScrapingDetails, "The Ledbury notting hill london");

			//Assert
			Assert.AreEqual(result[2].Review.NumberOfReviews, 0);
			Assert.AreEqual(result[2].Review.OverallScore, 0.0);
		}
		
		//Todo: Serach Url is null
	}
}