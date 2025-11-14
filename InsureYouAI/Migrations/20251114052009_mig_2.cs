using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureYouAI.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorys_Categorys_CategoryId1",
                table: "Categorys");

            migrationBuilder.DropIndex(
                name: "IX_Categorys_CategoryId1",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Categorys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Categorys",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorys_CategoryId1",
                table: "Categorys",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorys_Categorys_CategoryId1",
                table: "Categorys",
                column: "CategoryId1",
                principalTable: "Categorys",
                principalColumn: "CategoryId");
        }
    }
}
