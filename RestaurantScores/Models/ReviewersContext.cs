using System;
using Microsoft.EntityFrameworkCore;

namespace RestaurantScores.Models
{
    public partial class ReviewersContext : DbContext
    {
        public ReviewersContext()
        {
        }

        public ReviewersContext(DbContextOptions<ReviewersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Reviewer> Reviewer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
			{   
				//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
				optionsBuilder.UseSqlServer(System.Text.RegularExpressions.Regex.Unescape(Environment.GetEnvironmentVariable("RestaurantScoresDbConnectionString") ?? throw new InvalidOperationException()));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reviewer>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("1");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WebAddress)
                    .IsRequired()
                    .HasMaxLength(200);

	            entity.Property(e => e.NumberOfReviewsHtml)
		            .IsRequired()
		            .HasMaxLength(200);

	            entity.Property(e => e.NumberOfReviewsHtmlAttribute)
		            .IsRequired()
		            .HasMaxLength(200);

	            entity.Property(e => e.NumberOfReviews)
		            .IsRequired()
		            .HasMaxLength(200);

				entity.Property(e => e.OverallScoreHtml)
		            .IsRequired()
		            .HasMaxLength(200);

	            entity.Property(e => e.OverallScoreHtmlAttribute)
		            .IsRequired()
		            .HasMaxLength(200);

				entity.Property(e => e.OverallScore)
		            .IsRequired()
		            .HasMaxLength(200);

	            entity.Property(e => e.OverallMaxScore)
		            .IsRequired()
		            .HasMaxLength(200);

				entity.Property(e => e.DishTheDirtScore)
		            .IsRequired()
		            .HasMaxLength(200);
			});
        }
    }
}
