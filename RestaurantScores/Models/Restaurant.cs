using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RestaurantScores.Models.Interfaces;

namespace RestaurantScores.Models
{
    public class Restaurant : IRestaurant
    {
        //[Required]
        public string Name { get; set; }

        //[Required]
        [DisplayName("Post Code")]
        public string PostCode { get; set; }
    }
}
