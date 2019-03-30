using RestaurantScores.Models.Interfaces;

namespace RestaurantScores.Models
{
	public class Search : ISearch
	{
		public string SearchString { get; set; }
	}
}
