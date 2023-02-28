using Microsoft.EntityFrameworkCore.Migrations;

namespace BarDatabase.Migrations
{
    public partial class RecepieInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOrderId",
                table: "ProductOrders");

            migrationBuilder.DropColumn(
                name: "ProductIngredientId",
                table: "ProductIngredients");

            migrationBuilder.AddColumn<string>(
                name: "Recipe",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recipe",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductOrderId",
                table: "ProductOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductIngredientId",
                table: "ProductIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
