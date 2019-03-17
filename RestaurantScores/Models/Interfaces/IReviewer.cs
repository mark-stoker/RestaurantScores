using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantScores.Models.Interfaces
{
    interface IReviewer
    {
		int Id { get; set; }
        string Name { get; set; }
        string WebAddress { get; set; }
		//bool ApiAvailable { get; set; }
		string NumberOfReviewsHtml { get; set; }
		string NumberOfReviews { get; set; }
		string OverallScoreHtml { get; set; }
		double? OverallScore { get; set; }
	    
		//string HtmlParentClass { get; set; }
		//string RatingPatternStart { get; set; }
		//string RatingPatternEnd { get; set; }
		//string NumberOfRattingsPatternStart { get; set; }
		//string NumberOfRatingsPatternEnd { get; set; }
	}
}
