using System.Collections.Generic;
using RestaurantScores.Models;

namespace RestaurantScores.Managers.Interfaces
{
	public interface IWebSearchManager
	{
		List<Review> BingWebSearch(string searchQuery);
	}
}
