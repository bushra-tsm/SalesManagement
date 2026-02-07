using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagement.Data.Migrations
{
    public partial class addDetials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Products_ProductId",
                table: "SaleDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Sales_SaleId",
                table: "SaleDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDetail",
                table: "SaleDetail");

            migrationBuilder.RenameTable(
                name: "SaleDetail",
                newName: "SaleDetails");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetail_SaleId",
                table: "SaleDetails",
                newName: "IX_SaleDetails_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetail_ProductId",
                table: "SaleDetails",
                newName: "IX_SaleDetails_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDetails",
                table: "SaleDetails",
                column: "SaleDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Products_ProductId",
                table: "SaleDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Sales_SaleId",
                table: "SaleDetails",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Products_ProductId",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Sales_SaleId",
                table: "SaleDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDetails",
                table: "SaleDetails");

            migrationBuilder.RenameTable(
                name: "SaleDetails",
                newName: "SaleDetail");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetails_SaleId",
                table: "SaleDetail",
                newName: "IX_SaleDetail_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleDetails_ProductId",
                table: "SaleDetail",
                newName: "IX_SaleDetail_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDetail",
                table: "SaleDetail",
                column: "SaleDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Products_ProductId",
                table: "SaleDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Sales_SaleId",
                table: "SaleDetail",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
