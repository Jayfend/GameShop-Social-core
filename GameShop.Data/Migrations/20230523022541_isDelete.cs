using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.Migrations
{
    public partial class isDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("007b2916-6b6f-4215-8c2a-37c31abec623"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("02950d04-ce41-4da9-8e6b-fe2ed6a9a580"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("278b7ef1-5f99-4a29-b375-0a1b7ea4a4ae"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2a88842c-aa81-4010-b369-6598b701d99e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3fd21156-b2af-4741-bcad-34f93aac450b"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("5ecdc71d-d7ba-4176-a546-7db1ace8b9a9"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("763375ea-ec89-4e22-bb67-83a162102971"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8384f671-a501-4a3c-99dd-2333d4e9efbc"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("cab51e15-a8ba-472a-8709-fda6594664b2"));

            migrationBuilder.DeleteData(
                table: "UserAvatar",
                keyColumn: "Id",
                keyValue: new Guid("18238aa7-dc41-4995-9ed5-8ddc6c006fad"));

            migrationBuilder.DeleteData(
                table: "UserThumbnail",
                keyColumn: "Id",
                keyValue: new Guid("e72c36ce-5b16-4acd-a4e6-f12096b2fce9"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Games",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "38452b1d-07b0-41cc-a8cc-b8d36c499cd0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "fd5aaccd-c7ab-4353-baa5-03a109d29a51");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "Creationtime", "PasswordHash" },
                values: new object[] { "f04cd73d-cad2-4187-a4cd-f04ecb86616d", new DateTime(2023, 5, 23, 9, 25, 40, 939, DateTimeKind.Local).AddTicks(9032), "AQAAAAEAACcQAAAAEInwlZsDsUFGcj87VaHtjheARUtFrlGxXqg5mtn6WPbQL9g1oIF1RG0epddKKI6h6A==" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedDate", "GenreName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("319a582c-72db-4a41-8152-b2c6bfca1ab7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fe26ad38-56ac-415f-97c1-15cb4f206a68"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open-World", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fc19f241-d6c1-4702-bd4d-7c50587e4330"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Multiplayer", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8ae44a40-964d-4918-8216-b0e357da3695"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action RPG", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fd929a7a-b291-49e1-9462-241fe8cf7e17"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simulation", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("73ed73e7-e1b5-4bdd-becd-f0b9c3cd0356"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horror", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("69d8773a-6946-41e4-9b6a-ff764739cae3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports & Racing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f4769da3-a969-4591-bd73-b0ed8369a156"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Role-Playing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("14211f7d-0628-4ed5-b3cf-3d427017aeb9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Visual Novel", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserAvatar",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("c7506650-2bed-4ff6-8ae5-7d97eda77644"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "UserThumbnail",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("bcaa6674-7b0a-411d-97b4-f9790b98830b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("14211f7d-0628-4ed5-b3cf-3d427017aeb9"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("319a582c-72db-4a41-8152-b2c6bfca1ab7"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("69d8773a-6946-41e4-9b6a-ff764739cae3"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("73ed73e7-e1b5-4bdd-becd-f0b9c3cd0356"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8ae44a40-964d-4918-8216-b0e357da3695"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f4769da3-a969-4591-bd73-b0ed8369a156"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("fc19f241-d6c1-4702-bd4d-7c50587e4330"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("fd929a7a-b291-49e1-9462-241fe8cf7e17"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("fe26ad38-56ac-415f-97c1-15cb4f206a68"));

            migrationBuilder.DeleteData(
                table: "UserAvatar",
                keyColumn: "Id",
                keyValue: new Guid("c7506650-2bed-4ff6-8ae5-7d97eda77644"));

            migrationBuilder.DeleteData(
                table: "UserThumbnail",
                keyColumn: "Id",
                keyValue: new Guid("bcaa6674-7b0a-411d-97b4-f9790b98830b"));

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Games");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "a2215fb8-689f-455c-969d-1717f17db1a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "fee0831a-bba2-4e62-a83b-6be0384cc030");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "Creationtime", "PasswordHash" },
                values: new object[] { "b9ea220a-3ce3-45b5-8091-8a0b7694c158", new DateTime(2023, 5, 18, 11, 0, 30, 139, DateTimeKind.Local).AddTicks(4587), "AQAAAAEAACcQAAAAED8KW3JaxMgBIdDLsA9YhoMPs1fx1vDpBBRWeYEed5dd9V7dPEutuC34BJUBPSZRDA==" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedDate", "GenreName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("5ecdc71d-d7ba-4176-a546-7db1ace8b9a9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("02950d04-ce41-4da9-8e6b-fe2ed6a9a580"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open-World", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("278b7ef1-5f99-4a29-b375-0a1b7ea4a4ae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Multiplayer", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8384f671-a501-4a3c-99dd-2333d4e9efbc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action RPG", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cab51e15-a8ba-472a-8709-fda6594664b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simulation", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("763375ea-ec89-4e22-bb67-83a162102971"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horror", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2a88842c-aa81-4010-b369-6598b701d99e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports & Racing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3fd21156-b2af-4741-bcad-34f93aac450b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Role-Playing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("007b2916-6b6f-4215-8c2a-37c31abec623"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Visual Novel", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserAvatar",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("18238aa7-dc41-4995-9ed5-8ddc6c006fad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "UserThumbnail",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("e72c36ce-5b16-4acd-a4e6-f12096b2fce9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });
        }
    }
}
