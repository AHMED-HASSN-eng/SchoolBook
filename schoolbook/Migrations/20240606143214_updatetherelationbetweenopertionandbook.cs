using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolbook.Migrations
{
    public partial class updatetherelationbetweenopertionandbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operations_BookId",
                table: "Operations");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_BookId",
                table: "Operations",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operations_BookId",
                table: "Operations");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_BookId",
                table: "Operations",
                column: "BookId",
                unique: true);
        }
    }
}
