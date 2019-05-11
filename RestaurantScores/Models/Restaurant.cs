using RestaurantScores.Models.Interfaces;

namespace RestaurantScores.Models
{
	public class Restaurant : IRestaurantReview
    {
        public string Name { get; set; }
	    public string Url { get; set; }
    }
}
