using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.Migrations
{
    public partial class ImageConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Filesize",
                table: "GameImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 7, 17, 22, 29, 41, 692, DateTimeKind.Local).AddTicks(3585), new DateTime(2022, 7, 17, 22, 29, 41, 693, DateTimeKind.Local).AddTicks(808) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Filesize",
                table: "GameImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 7, 16, 15, 45, 3, 802, DateTimeKind.Local).AddTicks(8251), new DateTime(2022, 7, 16, 15, 45, 3, 803, DateTimeKind.Local).AddTicks(8628) });
        }
    }
}
