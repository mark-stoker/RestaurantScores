using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
