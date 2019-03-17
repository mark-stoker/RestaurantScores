using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantScores.Models.Interfaces
{
    interface IReview
    {
        string Name { get; set; }
        float Score { get; set; }
        float MaxScore { get; set; }
    }
}
