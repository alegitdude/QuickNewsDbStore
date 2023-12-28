using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurlAndStoreApp.Migrations
{
    /// <inheritdoc />
    public partial class IntialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "NewsDb");

            migrationBuilder.CreateTable(
                name: "Articles",
                schema: "NewsDb",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 500, nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Snippet = table.Column<string>(type: "nvarchar(3000)", maxLength: 1000, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(1000)", maxLength: 100, nullable: false),
                    Image_Url = table.Column<string>(type: "nvarchar(1000)", maxLength: 200, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Published_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(1000)", maxLength: 100, nullable: false),
                    Category1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Relevance_Score = table.Column<double>(type: "float", nullable: true),
                    Locale = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Uuid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles",
                schema: "NewsDb");
        }
    }
}
