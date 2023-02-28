using Microsoft.EntityFrameworkCore.Migrations;

namespace BarDatabase.Migrations
{
    public partial class UpdateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_Ingredients_IngredientId",
                table: "ProductIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_Products_ProductId",
                table: "ProductIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Orders_OrderId",
                table: "ProductOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Products_ProductId",
                table: "ProductOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrders",
                table: "ProductOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductIngredients",
                table: "ProductIngredients");

            migrationBuilder.RenameTable(
                name: "ProductOrders",
                newName: "ProductOrder");

            migrationBuilder.RenameTable(
                name: "ProductIngredients",
                newName: "ProductIngredient");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductOrder",
                newName: "IX_ProductOrder_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductIngredients_ProductId",
                table: "ProductIngredient",
                newName: "IX_ProductIngredient_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductIngredient",
                table: "ProductIngredient",
                columns: new[] { "IngredientId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredient_Ingredients_IngredientId",
                table: "ProductIngredient",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredient_Products_ProductId",
                table: "ProductIngredient",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Orders_OrderId",
                table: "ProductOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Products_ProductId",
                table: "ProductOrder",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredient_Ingredients_IngredientId",
                table: "ProductIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredient_Products_ProductId",
                table: "ProductIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Orders_OrderId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Products_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductIngredient",
                table: "ProductIngredient");

            migrationBuilder.RenameTable(
                name: "ProductOrder",
                newName: "ProductOrders");

            migrationBuilder.RenameTable(
                name: "ProductIngredient",
                newName: "ProductIngredients");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrders",
                newName: "IX_ProductOrders_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductIngredient_ProductId",
                table: "ProductIngredients",
                newName: "IX_ProductIngredients_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrders",
                table: "ProductOrders",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductIngredients",
                table: "ProductIngredients",
                columns: new[] { "IngredientId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_Ingredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_Products_ProductId",
                table: "ProductIngredients",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Orders_OrderId",
                table: "ProductOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Products_ProductId",
                table: "ProductOrders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
