using System;
using System.Collections.Generic;
using System.Text;
using RestaurantScores.Models;

namespace RestaurantScoresUnitTests.Builders
{
	class ReviewerScrapingDetailsBuilder
	{
		private int _id;
		private string _name;
		private string _webAddress;
		private string _numberOfReviewsHtml;
		private string _numberOfReviewsHtmlAttribute;
		private string _overallScoreHtmlAttribute;
		private string _overallScoreHtml;
		private int _overallMaxScore;
		private List<ReviewerScrapingDetails> _reviewerScrapingDetails = new List<ReviewerScrapingDetails>();

		public ReviewerScrapingDetailsBuilder WithId(int id)
		{
			_id = id;
			return this;
		}

		public ReviewerScrapingDetailsBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public ReviewerScrapingDetailsBuilder WithWebAddress(string webAddress)
		{
			_webAddress = webAddress;
			return this;
		}

		public ReviewerScrapingDetailsBuilder WithNumberOfReviewsHtml(string numberOfReviewsHtml)
		{
			_numberOfReviewsHtml = numberOfReviewsHtml;
			return this;
		}

		public ReviewerScrapingDetailsBuilder WithNumberOfReviewsHtmlAttribute(string numberOfReviewsHtmlAttribute)
		{
			_numberOfReviewsHtmlAttribute = numberOfReviewsHtmlAttribute;
			return this;
		}

		public ReviewerScrapingDetailsBuilder WithOverallScoreHtmlAttribute(string overallScoreHtmlAttribute)
		{
			_overallScoreHtmlAttribute = overallScoreHtmlAttribute;
			return this;
		}

		public ReviewerScrapingDetailsBuilder WithOverallScoreHtml(string overallScoreHtml)
		{
			_overallScoreHtml = overallScoreHtml;
			return this;
		}

		public ReviewerScrapingDetailsBuilder WithOverallMaxScore(int overallMaxScore)
		{
			_overallMaxScore = overallMaxScore;
			return this;
		}

		public ReviewerScrapingDetailsBuilder WithReviewersScrapingDetails(List<ReviewerScrapingDetails> reviewerScrapingDetails)
		{
			_reviewerScrapingDetails = reviewerScrapingDetails;
			return this;
		}

		public ReviewerScrapingDetails Build()
		{
			return new ReviewerScrapingDetails()
			{
				Id = _id,
				Name = _name,
				WebAddress = _webAddress,
				NumberOfReviewsHtml = _numberOfReviewsHtml,
				NumberOfReviewsHtmlAttribute = _numberOfReviewsHtmlAttribute,
				OverallScoreHtmlAttribute = _overallScoreHtmlAttribute,
				OverallScoreHtml = _overallScoreHtml,
				OverallMaxScore = _overallMaxScore
			};
		}

		public List<ReviewerScrapingDetails> BuildList()
		{
			return _reviewerScrapingDetails;
		}
	}
}
