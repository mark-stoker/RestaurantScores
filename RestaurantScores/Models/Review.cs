using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using RestaurantScores.Models.Interfaces;

namespace RestaurantScores.Models
{
    public class Review : IReview
    {
        public string Name { get; set; }
        public float Score { get; set; }

        [DisplayName("Max Score")]
        public float MaxScore { get; set; }
    }
}
