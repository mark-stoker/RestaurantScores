using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace RestaurantScores.Models
{

	public class ReviewerScrpaingDetailsContext : DbContext
	{
		public DbSet<ReviewerScrapingDetails> ReviewerScrapingDetails { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//var connectionString = "Data Source=" + HttpContext.Current.Server.MapPath(@"\App_Data\database.db");
			optionsBuilder.UseSqlite("Data Source=ReviewerScrapingDetails.db");
			//optionsBuilder.UseSqlite("Data Source=" +Path.Combine("ReviewerScrapingDetails.db"));
			//Server.MapPath(@"~\App_Data\Your.db");
			//optionsBuilder.UseSqlite(System.Text.RegularExpressions.Regex.Unescape(Environment.GetEnvironmentVariable("RestaurantScoresDbConnectionString") ?? throw new InvalidOperationException()));
		}
	}
}
