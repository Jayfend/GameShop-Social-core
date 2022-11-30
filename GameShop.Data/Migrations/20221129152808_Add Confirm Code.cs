using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.Migrations
{
    public partial class AddConfirmCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmCode",
                table: "AspNetUsers",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmCode",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "d94e70e4-cb3c-48e7-8c74-02e636254037");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "04752fdc-e2ee-4b78-8198-a54b23a8849d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "846407eb-df09-41dd-a095-bb6e169c32dd", "AQAAAAEAACcQAAAAEHBR0R4tpegdxN7qHwyuMyJhS2BYiVLtAwu0kKkX3HzqIYFoaF6fWrykWsc0tPE8og==" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 29, 21, 52, 32, 473, DateTimeKind.Local).AddTicks(3182), new DateTime(2022, 11, 29, 21, 52, 32, 474, DateTimeKind.Local).AddTicks(1867) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 29, 21, 52, 32, 474, DateTimeKind.Local).AddTicks(2382), new DateTime(2022, 11, 29, 21, 52, 32, 474, DateTimeKind.Local).AddTicks(2405) });
        }
    }
}
