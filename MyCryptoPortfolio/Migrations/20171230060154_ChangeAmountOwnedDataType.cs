using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyCryptoPortfolio.Migrations
{
    public partial class ChangeAmountOwnedDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "AmountOwned",
                table: "Holdings",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AmountOwned",
                table: "Holdings",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
