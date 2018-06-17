using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StoreWebsite.Data.Migrations
{
    public partial class CartItemsCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Orders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CartItems",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrderDetails",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CartItems",
                nullable: false,
                oldClrType: typeof(Guid));
        }
    }
}
