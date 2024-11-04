using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class addRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Recipe",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Recipe");
        }
    }
}
