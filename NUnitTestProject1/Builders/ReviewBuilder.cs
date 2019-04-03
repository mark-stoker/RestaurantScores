using System;
using System.Collections.Generic;
using System.Text;
using RestaurantScores.Models;

namespace RestaurantScoresUnitTests.Builders
{
	class ReviewBuilder
	{
		private string _url;
		private string _name;

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

		public Review Build()
		{
			return new Review()
			{
				Url = _url,
				Name = _name
			};
		}
	}
}
