using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeSnow.Migrations
{
    public partial class desctiptionItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AllItems",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AllItems");
        }
    }
}
