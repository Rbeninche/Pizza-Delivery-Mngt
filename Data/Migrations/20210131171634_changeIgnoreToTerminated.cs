using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaDeliveryManagement.Data.Migrations
{
    public partial class changeIgnoreToTerminated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ignore",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "Terminated",
                table: "Employees",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Terminated",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "Ignore",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
