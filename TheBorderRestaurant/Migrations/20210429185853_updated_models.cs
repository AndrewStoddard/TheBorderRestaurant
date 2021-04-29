using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBorderRestaurant.Migrations
{
    public partial class updated_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodOrders_AspNetUsers_UserId1",
                table: "FoodOrders");

            migrationBuilder.DropIndex(
                name: "IX_FoodOrders_UserId1",
                table: "FoodOrders");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "FoodOrders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FoodOrders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrders_UserId",
                table: "FoodOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodOrders_AspNetUsers_UserId",
                table: "FoodOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodOrders_AspNetUsers_UserId",
                table: "FoodOrders");

            migrationBuilder.DropIndex(
                name: "IX_FoodOrders_UserId",
                table: "FoodOrders");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "FoodOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "FoodOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrders_UserId1",
                table: "FoodOrders",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodOrders_AspNetUsers_UserId1",
                table: "FoodOrders",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
