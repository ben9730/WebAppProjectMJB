using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppProjectMJB.Migrations
{
    public partial class fix02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accessories_GameConsoleId",
                table: "Accessories");

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_GameConsoleId",
                table: "Accessories",
                column: "GameConsoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accessories_GameConsoleId",
                table: "Accessories");

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_GameConsoleId",
                table: "Accessories",
                column: "GameConsoleId",
                unique: true);
        }
    }
}
