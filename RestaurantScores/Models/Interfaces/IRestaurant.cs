using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantScores.Models.Interfaces
{
    interface IRestaurant
    {
        string Name { get; set; }
		string Url { get; set; }
    }
}
