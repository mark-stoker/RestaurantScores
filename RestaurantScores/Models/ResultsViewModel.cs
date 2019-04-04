namespace RestaurantScores.Models
{
    public class ResultsViewModel
    {
        public Restaurant Restaurant { get; set; }
		public Review Review { get; set; }
        public ReviewerScrapingDetails ReviewerScrapingDetails { get; set; }
    }
}
