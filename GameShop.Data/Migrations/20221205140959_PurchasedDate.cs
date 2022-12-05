using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.Migrations
{
    public partial class PurchasedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PurcharsedDate",
                table: "SoldGames",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "7fb0534e-259d-4939-8123-7293f8499770");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6ce5967c-0046-4648-bd05-ccb414d8aaf7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "601ed354-555e-433c-b79e-b252a1946eaa", "AQAAAAEAACcQAAAAEIJO2CY2eHLbhpVj8hmTH43xwpTQp6EwRlNUP/0KcG7CenYPxESvhmUazdkpYn8T8Q==" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 12, 5, 21, 9, 58, 740, DateTimeKind.Local).AddTicks(1768), new DateTime(2022, 12, 5, 21, 9, 58, 740, DateTimeKind.Local).AddTicks(9616) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 12, 5, 21, 9, 58, 741, DateTimeKind.Local).AddTicks(216), new DateTime(2022, 12, 5, 21, 9, 58, 741, DateTimeKind.Local).AddTicks(237) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurcharsedDate",
                table: "SoldGames");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "794b1efc-f694-4033-a6d2-acf6794b1a36");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e05322d4-c0b4-435d-a756-0d5f1035036e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a51df445-cbb9-4ba1-9a57-8f65a0bb3e32", "AQAAAAEAACcQAAAAEIBZbLrLqc3VqcOFuYYm/4PbcABoMwtqhRgq14+nEPwbzKIuHtnbJfgJr1P2F6+czA==" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 12, 4, 21, 20, 57, 699, DateTimeKind.Local).AddTicks(4019), new DateTime(2022, 12, 4, 21, 20, 57, 700, DateTimeKind.Local).AddTicks(2129) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 12, 4, 21, 20, 57, 700, DateTimeKind.Local).AddTicks(2664), new DateTime(2022, 12, 4, 21, 20, 57, 700, DateTimeKind.Local).AddTicks(2683) });
        }
    }
}