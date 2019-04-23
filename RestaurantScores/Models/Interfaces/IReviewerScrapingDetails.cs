using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantScores.Models.Interfaces
{
    interface IReviewerScrapingDetails
    {
	    int Id { get; set; }
	    string Name { get; set; }
	    string WebAddress { get; set; }
	    string NumberOfReviewsHtml { get; set; }
	    string NumberOfReviewsHtmlAttribute { get; set; }
	    string OverallScoreHtml { get; set; }
		string OverallScoreHtmlAttribute { get; set; }
	    int OverallMaxScore { get; set; }
	}
}
