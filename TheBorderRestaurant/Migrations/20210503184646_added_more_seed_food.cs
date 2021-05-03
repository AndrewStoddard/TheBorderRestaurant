using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBorderRestaurant.Migrations
{
    public partial class added_more_seed_food : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "Id", "Description", "ImageName", "Name", "Price" },
                values: new object[,]
                {
                    { 5, "Some fried chicken for ya belly.", "Chicken.jpg", "Chicken Tendies", 9.0 },
                    { 6, "Seafood dish consisting of shelled, cooked prawns in a Marie Rose sauce or cocktail sauce, served in a glass.", "ShrimpCocktail.jpg", "Shrimp Cocktail", 18.0 },
                    { 7, "A flour tortilla wrapped into a sealed cylindrical shape around various ingredients.", "Burrito.jpg", "Burrito", 4.0 },
                    { 8, "The term originally referred to skirt steak, the cut of beef first used in the dish.", "Fajitas.jpg", "Fajitas", 7.0 },
                    { 9, "Just Tilapia but Loco.", "Tilapia.jpg", "Tilapia Loco", 12.0 },
                    { 10, "Fries with some Mexican Flair.", "Fries.jpg", "Mexican Fries", 2.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
