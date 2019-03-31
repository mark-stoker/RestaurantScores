using System.ComponentModel.DataAnnotations;
using RestaurantScores.Models.Interfaces;

namespace RestaurantScores.Models
{
    public class Restaurant : IRestaurantReview
    {
        [Required]
        public string Name { get; set; }
	    public string Url { get; set; }
    }
}
