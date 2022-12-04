using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.Migrations
{
    public partial class Filegameupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameFile",
                table: "SoldGames",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameFile",
                table: "SoldGames");

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
    }
}
