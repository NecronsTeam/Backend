using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmBackend.Database.Migrations
{
    /// <inheritdoc />
    public partial class AllChangesForPhotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Photos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AvatarId",
                table: "Accounts",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Photos_AvatarId",
                table: "Accounts",
                column: "AvatarId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Photos_AvatarId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AvatarId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Accounts");
        }
    }
}
