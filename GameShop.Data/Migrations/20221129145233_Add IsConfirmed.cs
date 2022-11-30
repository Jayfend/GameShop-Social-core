using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.Migrations
{
    public partial class AddIsConfirmed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isConfirmed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isConfirmed",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "1cecde01-f31e-4f2e-a8a1-7d844d7e507a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "cd25b051-b64a-45f7-916c-324c2c69e657");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9dcadf41-c755-4a63-b562-e20a36fd245b", "AQAAAAEAACcQAAAAEGZcVPc6Kj8RauaBvC1/5W0XKNzePu19vKzpADeOwamzeImckUvVszz7FkbyVNDIzQ==" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 28, 17, 27, 48, 364, DateTimeKind.Local).AddTicks(8223), new DateTime(2022, 11, 28, 17, 27, 48, 366, DateTimeKind.Local).AddTicks(246) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 11, 28, 17, 27, 48, 366, DateTimeKind.Local).AddTicks(758), new DateTime(2022, 11, 28, 17, 27, 48, 366, DateTimeKind.Local).AddTicks(781) });
        }
    }
}
