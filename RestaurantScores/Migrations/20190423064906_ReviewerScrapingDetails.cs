using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantScores.Migrations
{
    public partial class ReviewerScrapingDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReviewerScrapingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    WebAddress = table.Column<string>(nullable: true),
                    NumberOfReviewsHtml = table.Column<string>(nullable: true),
                    NumberOfReviewsHtmlAttribute = table.Column<string>(nullable: true),
                    OverallScoreHtml = table.Column<string>(nullable: true),
                    OverallScoreHtmlAttribute = table.Column<string>(nullable: true),
                    OverallMaxScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewerScrapingDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewerScrapingDetails");
        }
    }
}
