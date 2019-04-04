using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using RestaurantScores.Models;

namespace RestaurantScoresUnitTests.Builders
{
	class ReviewBuilder
	{
		private string _url;
		private string _name;
		private int _numberOfReviews;
		private double? _overallScore;
		private double? _dishTheDirtScore;
		private List<Review> _reviews = new List<Review>();

		public ReviewBuilder WithUrl(string url)
		{
			_url = url;
			return this;
		}

		public ReviewBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public ReviewBuilder WithNumberOfReviews(int numberOfReviews)
		{
			_numberOfReviews = numberOfReviews;
			return this;
		}

		public ReviewBuilder WithOverallScore(double? overallScore)
		{
			_overallScore = overallScore;
			return this;
		}

		public ReviewBuilder WithDishTheDirtScore(double? dishTheDirtScore)
		{
			_dishTheDirtScore = dishTheDirtScore;
			return this;
		}

		public ReviewBuilder WithReviews(List<Review> reviews)
		{
			_reviews = reviews;
			return this;
		}

		public Review Build()
		{
			return new Review()
			{
				Url = _url,
				Name = _name,
				NumberOfReviews = _numberOfReviews,
				OverallScore =  _overallScore,
				DishTheDirtScore = _dishTheDirtScore
			};
		}

		public List<Review> BuildList()
		{
			return _reviews;
		}
	}
}
