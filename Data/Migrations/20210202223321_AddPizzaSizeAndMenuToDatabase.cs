using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaDeliveryManagement.Data.Migrations
{
    public partial class AddPizzaSizeAndMenuToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PizzaSize",
                columns: table => new
                {
                    PizzaSizeId = table.Column<string>(nullable: false),
                    Size = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaSize", x => x.PizzaSizeId);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaName = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PizzaSizeId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_Menu_PizzaSize_PizzaSizeId",
                        column: x => x.PizzaSizeId,
                        principalTable: "PizzaSize",
                        principalColumn: "PizzaSizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PizzaSize",
                columns: new[] { "PizzaSizeId", "Size" },
                values: new object[] { "S", "Small" });

            migrationBuilder.InsertData(
                table: "PizzaSize",
                columns: new[] { "PizzaSizeId", "Size" },
                values: new object[] { "M", "Medium" });

            migrationBuilder.InsertData(
                table: "PizzaSize",
                columns: new[] { "PizzaSizeId", "Size" },
                values: new object[] { "L", "Large" });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "MenuId", "PizzaName", "PizzaSizeId", "Price" },
                values: new object[,]
                {
                    { 1, "Cheese Pizza", "S", 5m },
                    { 4, "Veggie Pizza", "S", 6m },
                    { 7, "Pepperoni Pizza", "S", 7m },
                    { 2, "Cheese Pizza", "M", 7m },
                    { 5, "Veggie Pizza", "M", 8m },
                    { 8, "Pepperoni Pizza", "M", 9m },
                    { 3, "Cheese Pizza", "L", 9m },
                    { 6, "Veggie Pizza", "L", 10m },
                    { 9, "Pepperoni Pizza", "L", 11m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_PizzaSizeId",
                table: "Menu",
                column: "PizzaSizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "PizzaSize");
        }
    }
}
