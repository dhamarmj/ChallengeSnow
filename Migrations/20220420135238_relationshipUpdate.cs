using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeSnow.Migrations
{
    public partial class relationshipUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Items_Item_NumberId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "Item_NumberId",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Items_Item_NumberId",
                table: "Orders",
                column: "Item_NumberId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Items_Item_NumberId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "Item_NumberId",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Items_Item_NumberId",
                table: "Orders",
                column: "Item_NumberId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
