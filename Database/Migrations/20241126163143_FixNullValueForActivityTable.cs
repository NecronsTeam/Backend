using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmBackend.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixNullValueForActivityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Photos_PreviewPhotoId",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "PreviewPhotoId",
                table: "Activities",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Photos_PreviewPhotoId",
                table: "Activities",
                column: "PreviewPhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Photos_PreviewPhotoId",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "PreviewPhotoId",
                table: "Activities",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Photos_PreviewPhotoId",
                table: "Activities",
                column: "PreviewPhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
