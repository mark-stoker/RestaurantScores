using System.ComponentModel;
using RestaurantScores.Models.Interfaces;

namespace RestaurantScores.Models
{
    public class Review : IReview
    {
        public string Name { get; set; }

        public string Url { get; set; }

	    [DisplayName("Number of Reviews")]
	    public int NumberOfReviews { get; set; } = 0;

		[DisplayName("Site Average Rating")]
	    public double? OverallScore { get; set; } = 0.00;

	    [DisplayName("Percentage Equivalent")]
	    public double? DishTheDirtScore { get; set; }

	}
}
