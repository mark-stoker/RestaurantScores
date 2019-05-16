using System;
using Microsoft.EntityFrameworkCore;

namespace RestaurantScores.Models
{

	public class ReviewerScrpaingDetailsContext : DbContext
	{
		public DbSet<ReviewerScrapingDetails> ReviewerScrapingDetails { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(Environment.GetEnvironmentVariable("RestaurantScoresDbConnectionString") ?? throw new InvalidOperationException());
		}
	}
}
