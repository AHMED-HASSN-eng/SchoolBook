using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace schoolbook.Migrations
{
    public partial class updaterealationsv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Users_UserId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_BookId",
                table: "Operations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Operations",
                newName: "buyerrId");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Operations",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Operations_UserId",
                table: "Operations",
                newName: "IX_Operations_buyerrId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_BookId",
                table: "Operations",
                column: "BookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Users_buyerrId",
                table: "Operations",
                column: "buyerrId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Users_buyerrId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_BookId",
                table: "Operations");

            migrationBuilder.RenameColumn(
                name: "buyerrId",
                table: "Operations",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Operations",
                newName: "State");

            migrationBuilder.RenameIndex(
                name: "IX_Operations_buyerrId",
                table: "Operations",
                newName: "IX_Operations_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_BookId",
                table: "Operations",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Users_UserId",
                table: "Operations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
