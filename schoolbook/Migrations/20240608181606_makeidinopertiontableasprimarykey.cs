using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolbook.Migrations
{
    public partial class makeidinopertiontableasprimarykey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Operations_Id",
                table: "Operations",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operations_Id",
                table: "Operations");
        }
    }
}
