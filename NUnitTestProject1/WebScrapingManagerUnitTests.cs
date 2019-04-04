using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
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
			//Question should this be the Interface?? i.e. test by interface
			//_scrapingManager = new ScrapingManager();
			//_reviewSitestToScrape = new List<Review>();
			//_reviewersScrapingDetails = new List<ReviewerScrapingDetails>();

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
					new ReviewerScrapingDetailsBuilder()
						.WithId(4)
						.WithName("TripExpert")
						.WithWebAddress("www.tripexpert.com")
						.WithNumberOfReviewsHtml("//meta[contains(@itemprop, \'reviewCount\')]")
						.WithNumberOfReviewsHtmlAttribute("content")
						.WithOverallScoreHtml("//span[contains(@class, \'score fleft\')]")
						.WithOverallScoreHtmlAttribute(null)
						.WithOverallMaxScore(5)
						.Build(),
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
		}

		[Test] 
		public void SearchForRatingsAndReviewCounts_ReviewAndScrapingDataIsCorrect_ScrapingDetailsReturned()
		{
			//Arrange
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

			//Act
			var result = _scrapingManager.ScrapeRestaurantReviewSites(_reviewSitestToScrape, _reviewersScrapingDetails, "The Ledbury notting hill london");

			//Assert
			Assert.AreEqual(4, result.Count);

			Assert.IsTrue(result[0].Review.NumberOfReviews > 0);
			Assert.IsTrue(result[0].Review.OverallScore > 0);

			Assert.IsTrue(result[1].Review.NumberOfReviews > 0);
			Assert.IsTrue(result[1].Review.OverallScore > 0);

			Assert.IsTrue(result[2].Review.NumberOfReviews > 0);
			Assert.IsTrue(result[2].Review.OverallScore > 0);

			Assert.IsTrue(result[3].Review.NumberOfReviews > 0);
			Assert.IsTrue(result[3].Review.OverallScore > 0);
		}

		[Test]
		public void SearchForRatingsAndReviewCounts_OpenTableReviewUrlIncorrect_ZeroValuesReturnedForThatReview()
		{
			//Arrange
			_reviewSitestToScrape = new ReviewBuilder()
				.WithReviews(new List<Review>
				{
					new ReviewBuilder()
						.WithName("The Ledbury, London - Updated 2019 Restaurant Reviews ..")
						.WithUrl("https://www.tripadvisor.co.uk/Restaurant_Review-g186338-d720761-Reviews-The_Ledbury-London_England.html")
						.Build(),
					new ReviewBuilder()
						.WithName("The Ledbury - London, | OpenTable")
						.WithUrl("https://www.opentable.co.uk/the-ledburyy") //Incorrect url
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

			//Act
			var result = _scrapingManager.ScrapeRestaurantReviewSites(_reviewSitestToScrape, _reviewersScrapingDetails,"The Ledbury notting hill london");

			//Assert
			Assert.AreEqual(result[1].Review.NumberOfReviews, 0);
			Assert.AreEqual(result[1].Review.OverallScore, 0.0);

		}

		[Test]
		public void SearchForRatingsAndReviewCounts_TripAdvisorScrapingDataIncorrectForTotalNumberOfReivews_ZeroReturnedForTotalNmberOfReviews()
		{
			//Arrange
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

			_reviewersScrapingDetails[0].NumberOfReviewsHtml = "";

			//Act
			var result = _scrapingManager.ScrapeRestaurantReviewSites(_reviewSitestToScrape, _reviewersScrapingDetails, "The Ledbury notting hill london");

			//Assert
			Assert.AreEqual(result[1].Review.NumberOfReviews, 0);
			Assert.AreEqual(result[1].Review.OverallScore, 0.0);
		}
	}
}