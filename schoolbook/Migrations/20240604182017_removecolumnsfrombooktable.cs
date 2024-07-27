using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolbook.Migrations
{
    public partial class removecolumnsfrombooktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "count",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "totalsold",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "totalsold",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
