﻿using System.ComponentModel;
using RestaurantScores.Models.Interfaces;

namespace RestaurantScores.Models
{
    public class Reviewer : IReviewer
    {
		public int Id { get; set; }
        public string Name { get; set; }
        public string WebAddress { get; set; }

		public string NumberOfReviewsHtml { get; set; }
		public string NumberOfReviewsHtmlAttribute { get; set; }
        [DisplayName("Number of Reviews")]
        public string NumberOfReviews { get; set; }

		public string OverallScoreHtmlAttribute { get; set; }
		public string OverallScoreHtml { get; set; }

	    [DisplayName("Site Average Rating")]
		public double? OverallScore { get; set; }

	    [DisplayName("Site Max Rating")]
		public int OverallMaxScore { get; set; }

		[DisplayName("Percentage Equivalent")]
		public double? DishTheDirtScore { get; set; }
    }
}
