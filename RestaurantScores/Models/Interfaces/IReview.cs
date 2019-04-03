using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantScores.Models.Interfaces
{
    interface IReview
    {
        string Name { get; set; }
        string Url { get; set; }
	    string NumberOfReviews { get; set; }
	    double? OverallScore { get; set; }
	    double? DishTheDirtScore { get; set; }
	}
}
