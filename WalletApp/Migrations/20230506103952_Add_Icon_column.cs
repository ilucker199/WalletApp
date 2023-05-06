using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletApp.Migrations
{
    public partial class Add_Icon_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Transactions");
        }
    }
}
