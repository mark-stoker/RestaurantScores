using System.Collections.Generic;
using RestaurantScores.Models;

namespace RestaurantScores.Managers.Interfaces
{
	public interface IWebSearchManager
	{
		List<Restaurant> BingWebSearch(string searchQuery);
	}
}
