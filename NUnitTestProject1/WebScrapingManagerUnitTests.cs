using System.Collections.Generic;
using NUnit.Framework;
using RestaurantScores.Managers;
using RestaurantScores.Models;
using RestaurantScoresUnitTests.Builders;

namespace Tests
{
	public class WebScrapingManagerUnitTests
	{
		private ScrapingManager _scrapingManager;
		private List<Review> _reviewSitestToScrape;
		private List<ReviewerScrapingDetails> _reviewers;

		[SetUp]
		public void Setup()
		{
			//Question should this be the Interface?? i.e. test by interface
			var webScrapingManager = new ScrapingManager();
			var reviewSitesToScrape = new List<Review>();
			var reviewers = new List<ReviewerScrapingDetails>();
		}

		[Test]
		public void Test1()
		{
			//Arrange
			var review1 = new ReviewBuilder()
				.WithName("The Ledbury, London - Updated 2019 Restaurant Reviews ..")
				.WithUrl("https://www.tripadvisor.co.uk/Restaurant_Review-g186338-d720761-Reviews-The_Ledbury-London_England.html")
				.Build();

			var review2 = new ReviewBuilder()
				.WithName("The Ledbury, London - Updated 2019 Restaurant Reviews ..")
				.WithUrl("https://www.tripadvisor.co.uk/Restaurant_Review-g186338-d720761-Reviews-The_Ledbury-London_England.html")
				.Build();

			_reviewSitestToScrape.Add(review1);
			_reviewSitestToScrape.Add(review1);

			//Act


			//Assert
			Assert.Pass();
		}
	}
}