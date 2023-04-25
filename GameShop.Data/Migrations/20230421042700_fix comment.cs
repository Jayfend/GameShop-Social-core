using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.Migrations
{
    public partial class fixcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("103c402d-58e0-4f91-aa60-85306b44fe05"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("484da5f9-5869-4952-8139-f7aeaff36d46"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4b5357ea-cff4-4d3c-a062-176d32f0bfc2"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4c0db0ad-0b26-468b-b662-4b7574904401"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4cbec419-1bbd-4164-b39a-58ac427594b1"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("858dd4fa-29d5-4205-aa42-93972c576dad"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("90709602-827f-447a-9668-72d75e584e55"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a9b5bca0-2838-427a-92fe-28aeec2aa1e4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ce7e8e75-daf2-406b-bd21-2dafa54af919"));

            migrationBuilder.DeleteData(
                table: "UserAvatar",
                keyColumn: "Id",
                keyValue: new Guid("0a1362c7-a4b5-485a-89d5-62f8abe4a674"));

            migrationBuilder.DeleteData(
                table: "UserThumbnail",
                keyColumn: "Id",
                keyValue: new Guid("73f581c5-7a53-41ee-be61-56eaf6cd6715"));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "209c7b7c-8f00-4cb6-bbce-9d4b85dec41f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "71a0138b-dce1-4c96-bf0a-2a02cdc4dd11");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1a4914c9-5f34-4dab-9c56-c60025d89c12", "AQAAAAEAACcQAAAAED1PSW88JmsghxRvF44YsdVMIOj+xS+VGZy0M+3j6hIdJ50NbPxpp/SPi3ZSJZahDg==" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedDate", "GenreName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("9124ed1f-5647-4f1d-998c-e3252afb7bee"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9fb5b065-97dc-49b2-bcd6-001af98b1236"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open-World", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("19543b51-cd15-4f1f-8cfe-5d4bf2dd4203"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Multiplayer", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7ba0156a-f2c8-4e4f-909d-2d94007ba605"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action RPG", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b3512484-6aa4-4db1-ae74-640ae1be9c9c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simulation", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4ee9c55b-3384-4e6d-8689-8e498821795f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horror", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("0da988d5-13a4-4332-820a-7b9f49e05859"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports & Racing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("800bbef4-9044-4129-b3a7-ed15cbfc48ed"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Role-Playing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c14f62e6-ee7d-4188-8710-3bebea13029b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Visual Novel", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserAvatar",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("285c2a4a-5349-4b12-9f06-25f620892647"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "UserThumbnail",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("19a9746f-8896-4ba9-94a9-b14a8be25d29"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0da988d5-13a4-4332-820a-7b9f49e05859"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("19543b51-cd15-4f1f-8cfe-5d4bf2dd4203"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4ee9c55b-3384-4e6d-8689-8e498821795f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7ba0156a-f2c8-4e4f-909d-2d94007ba605"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("800bbef4-9044-4129-b3a7-ed15cbfc48ed"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("9124ed1f-5647-4f1d-998c-e3252afb7bee"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("9fb5b065-97dc-49b2-bcd6-001af98b1236"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b3512484-6aa4-4db1-ae74-640ae1be9c9c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("c14f62e6-ee7d-4188-8710-3bebea13029b"));

            migrationBuilder.DeleteData(
                table: "UserAvatar",
                keyColumn: "Id",
                keyValue: new Guid("285c2a4a-5349-4b12-9f06-25f620892647"));

            migrationBuilder.DeleteData(
                table: "UserThumbnail",
                keyColumn: "Id",
                keyValue: new Guid("19a9746f-8896-4ba9-94a9-b14a8be25d29"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Content",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "2b01e6f0-f8c9-492b-8f63-d69e81c53924");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "9e7b4b10-9ecc-4f81-855b-b3a462b1b132");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4111a9d8-8634-45cc-a693-63fd3e426419", "AQAAAAEAACcQAAAAELmoqkS+dbOgBKtOnyXU8iWSIAenJl0MhNdjRP7G9JtyJ46EODvYTFDs/Z54jkWZ6g==" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedDate", "GenreName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("484da5f9-5869-4952-8139-f7aeaff36d46"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4c0db0ad-0b26-468b-b662-4b7574904401"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open-World", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a9b5bca0-2838-427a-92fe-28aeec2aa1e4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Multiplayer", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("103c402d-58e0-4f91-aa60-85306b44fe05"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action RPG", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4cbec419-1bbd-4164-b39a-58ac427594b1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simulation", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("858dd4fa-29d5-4205-aa42-93972c576dad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horror", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4b5357ea-cff4-4d3c-a062-176d32f0bfc2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports & Racing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ce7e8e75-daf2-406b-bd21-2dafa54af919"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Role-Playing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("90709602-827f-447a-9668-72d75e584e55"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Visual Novel", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserAvatar",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("0a1362c7-a4b5-485a-89d5-62f8abe4a674"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "UserThumbnail",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("73f581c5-7a53-41ee-be61-56eaf6cd6715"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });
        }
    }
}
