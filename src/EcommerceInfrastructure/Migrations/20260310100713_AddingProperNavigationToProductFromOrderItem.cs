using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingProperNavigationToProductFromOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProductId1",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ProductId1",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "OrderItem");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 10, 7, 13, 70, DateTimeKind.Utc).AddTicks(7712));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 10, 7, 13, 70, DateTimeKind.Utc).AddTicks(7729));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 10, 7, 13, 70, DateTimeKind.Utc).AddTicks(8011));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 10, 7, 13, 70, DateTimeKind.Utc).AddTicks(8021));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 20, 11, 40, 367, DateTimeKind.Utc).AddTicks(7366));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 20, 11, 40, 367, DateTimeKind.Utc).AddTicks(7376));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 20, 11, 40, 367, DateTimeKind.Utc).AddTicks(7717));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 20, 11, 40, 367, DateTimeKind.Utc).AddTicks(7728));

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId1",
                table: "OrderItem",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProductId1",
                table: "OrderItem",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
