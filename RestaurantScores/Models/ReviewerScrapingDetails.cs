using System.ComponentModel;
using RestaurantScores.Models.Interfaces;

namespace RestaurantScores.Models
{
    public class ReviewerScrapingDetails : IReviewerScrapingDetails
    {
		public int Id { get; set; }
        public string Name { get; set; }
        public string WebAddress { get; set; }
		public string NumberOfReviewsHtml { get; set; }
		public string NumberOfReviewsHtmlAttribute { get; set; }
	    public string OverallScoreHtml { get; set; }
		public string OverallScoreHtmlAttribute { get; set; }
	    public int OverallMaxScore { get; set; }
	}
}
