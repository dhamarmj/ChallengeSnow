using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeSnow.Migrations
{
    public partial class inheritanceStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Items_Item_NumberId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "AllItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllItems",
                table: "AllItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AllItems_Item_NumberId",
                table: "Orders",
                column: "Item_NumberId",
                principalTable: "AllItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AllItems_Item_NumberId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllItems",
                table: "AllItems");

            migrationBuilder.RenameTable(
                name: "AllItems",
                newName: "Items");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Items_Item_NumberId",
                table: "Orders",
                column: "Item_NumberId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
