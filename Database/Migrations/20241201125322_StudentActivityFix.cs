using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmBackend.Database.Migrations
{
    /// <inheritdoc />
    public partial class StudentActivityFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentActivities_Users_ActivityId",
                table: "StudentActivities");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentActivities_Activities_ActivityId",
                table: "StudentActivities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentActivities_Activities_ActivityId",
                table: "StudentActivities");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentActivities_Users_ActivityId",
                table: "StudentActivities",
                column: "ActivityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
