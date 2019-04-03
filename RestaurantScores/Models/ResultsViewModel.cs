namespace RestaurantScores.Models
{
    public class ResultsViewModel
    {
        public Restaurant restaurant { get; set; }
		public Review Review { get; set; }
        public ReviewerScrapingDetails ReviewerScrapingDetails { get; set; }
    }
}
