using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeBlogBackEnd.Migrations
{
    public partial class refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bloggers_Email",
                table: "Bloggers",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bloggers_Email",
                table: "Bloggers");
        }
    }
}
