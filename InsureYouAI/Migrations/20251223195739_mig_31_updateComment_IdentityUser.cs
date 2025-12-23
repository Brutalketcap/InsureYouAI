using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureYouAI.Migrations
{
    /// <inheritdoc />
    public partial class mig_31_updateComment_IdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "appUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_appUserId",
                table: "Comments",
                column: "appUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_appUserId",
                table: "Comments",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_appUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_appUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "appUserId",
                table: "Comments");
        }
    }
}
