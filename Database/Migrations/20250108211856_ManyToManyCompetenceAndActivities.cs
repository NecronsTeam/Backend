using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmBackend.Database.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyCompetenceAndActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competences_Activities_ActivityId",
                table: "Competences");

            migrationBuilder.DropIndex(
                name: "IX_Competences_ActivityId",
                table: "Competences");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Competences");

            migrationBuilder.CreateTable(
                name: "ActivityCompetence",
                columns: table => new
                {
                    ActivitiesId = table.Column<int>(type: "integer", nullable: false),
                    CompetencesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityCompetence", x => new { x.ActivitiesId, x.CompetencesId });
                    table.ForeignKey(
                        name: "FK_ActivityCompetence_Activities_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityCompetence_Competences_CompetencesId",
                        column: x => x.CompetencesId,
                        principalTable: "Competences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityCompetence_CompetencesId",
                table: "ActivityCompetence",
                column: "CompetencesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityCompetence");

            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Competences",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Competences_ActivityId",
                table: "Competences",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competences_Activities_ActivityId",
                table: "Competences",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }
    }
}
