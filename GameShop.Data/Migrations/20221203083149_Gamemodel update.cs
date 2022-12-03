using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.Migrations
{
    public partial class Gamemodelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Games",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "155163c0-f406-4fb7-855c-df7224c47859");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "46d8fa8e-b8ee-472e-a47c-7667119b6301");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6bd555af-6a2b-4c1d-9a81-04bb64c72efb", "AQAAAAEAACcQAAAAEMsQM28aPS5hpUSY4EUxjVgG0DiRHyYpIrtjzP9+zCeksOZKgUu92zSS0xRTLeRNVw==" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 12, 3, 15, 31, 48, 926, DateTimeKind.Local).AddTicks(6040), new DateTime(2022, 12, 3, 15, 31, 48, 927, DateTimeKind.Local).AddTicks(9177) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 12, 3, 15, 31, 48, 927, DateTimeKind.Local).AddTicks(9771), new DateTime(2022, 12, 3, 15, 31, 48, 927, DateTimeKind.Local).AddTicks(9795) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Games");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "8e3df23a-3e8e-45f2-9351-261dbc992336");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c589b17c-cbf1-4e70-991c-7de84291cbf1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "14b8578c-2d30-4a5b-b480-716ea073b57d", "AQAAAAEAACcQAAAAEA++vCBsr579JJnE836VYrOhI0vAjGZglbcREoofrdUsFGQRc+LwM/vR8sxiwObKUA==" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 29, 22, 28, 7, 836, DateTimeKind.Local).AddTicks(6062), new DateTime(2022, 11, 29, 22, 28, 7, 837, DateTimeKind.Local).AddTicks(5223) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 29, 22, 28, 7, 837, DateTimeKind.Local).AddTicks(5726), new DateTime(2022, 11, 29, 22, 28, 7, 837, DateTimeKind.Local).AddTicks(5751) });
        }
    }
}
