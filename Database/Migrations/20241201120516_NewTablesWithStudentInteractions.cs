using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CrmBackend.Database.Migrations
{
    /// <inheritdoc />
    public partial class NewTablesWithStudentInteractions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrgChatLink",
                table: "Activities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ActivityTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Link = table.Column<string>(type: "text", nullable: false),
                    MaxScore = table.Column<double>(type: "double precision", nullable: false),
                    ActivityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityTests_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ActivityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competences_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ActivityId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StudentTestResultId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentActivities_Users_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentActivities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentActivityId = table.Column<int>(type: "integer", nullable: false),
                    ActivityTestId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentTestResults_ActivityTests_ActivityTestId",
                        column: x => x.ActivityTestId,
                        principalTable: "ActivityTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTestResults_StudentActivities_StudentActivityId",
                        column: x => x.StudentActivityId,
                        principalTable: "StudentActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTests_ActivityId",
                table: "ActivityTests",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Competences_ActivityId",
                table: "Competences",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentActivities_ActivityId",
                table: "StudentActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentActivities_StudentTestResultId",
                table: "StudentActivities",
                column: "StudentTestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentActivities_UserId",
                table: "StudentActivities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTestResults_ActivityTestId",
                table: "StudentTestResults",
                column: "ActivityTestId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTestResults_StudentActivityId",
                table: "StudentTestResults",
                column: "StudentActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentActivities_StudentTestResults_StudentTestResultId",
                table: "StudentActivities",
                column: "StudentTestResultId",
                principalTable: "StudentTestResults",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentActivities_StudentTestResults_StudentTestResultId",
                table: "StudentActivities");

            migrationBuilder.DropTable(
                name: "Competences");

            migrationBuilder.DropTable(
                name: "StudentTestResults");

            migrationBuilder.DropTable(
                name: "ActivityTests");

            migrationBuilder.DropTable(
                name: "StudentActivities");

            migrationBuilder.DropColumn(
                name: "OrgChatLink",
                table: "Activities");
        }
    }
}
