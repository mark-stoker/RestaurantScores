using System.ComponentModel;
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
