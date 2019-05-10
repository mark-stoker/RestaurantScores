using RestaurantScores.Models.Interfaces;

namespace RestaurantScores.Models
{
	public class Search : ISearch
	{
		public string SearchString { get; set; }
		//This is a Honeypot field to stop spam
		public string Name { get; set; }
	}
}
