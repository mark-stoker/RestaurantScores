using System.Collections.Generic;
using System.Linq;
using RestaurantScores.Managers.Interfaces;
using RestaurantScores.Models;

namespace RestaurantScores.Managers
{
	public class DatabaseManager : IDatabaseManager
	{
		public List<Reviewer> GetScrapingData()
		{
			var context = new ReviewersContext();
			var reviewers = context.Reviewer.ToList();
			return reviewers;
		}
	}
}
