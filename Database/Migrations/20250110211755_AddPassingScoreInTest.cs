using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmBackend.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddPassingScoreInTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "ActivityTests");

            migrationBuilder.AddColumn<double>(
                name: "PassingScore",
                table: "ActivityTests",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassingScore",
                table: "ActivityTests");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "ActivityTests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
