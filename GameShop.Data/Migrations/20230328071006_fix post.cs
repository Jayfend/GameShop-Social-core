using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.Migrations
{
    public partial class fixpost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("032c6796-8097-4577-9b96-74925e589d5b"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("22b1610c-39b0-41eb-98c4-ac6fb91ea934"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("5ea0ef3d-7445-4bde-b738-dfb020e06e58"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7c5c7b4c-29d7-477a-b92e-319bd763d8fc"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ceb68203-1c09-4980-8072-755d8cc2d522"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("dcfa4907-09c6-4e48-86cd-25d1cc146b86"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ded48134-97e3-4509-a000-91d34d0e1005"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("e52937c8-308e-4735-8653-db791a0b54b8"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("fd8c6d07-5842-41e1-8aa9-194da61dda1d"));

            migrationBuilder.DeleteData(
                table: "UserAvatar",
                keyColumn: "Id",
                keyValue: new Guid("29e6e928-d295-417e-a92d-2d3a755f108b"));

            migrationBuilder.DeleteData(
                table: "UserThumbnail",
                keyColumn: "Id",
                keyValue: new Guid("ba6ea26f-062f-4422-8fea-e2ab157460e0"));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Posts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "8e5aa211-d1d3-497a-a5fc-cd50a4fcac37");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "55ea0960-2e6b-4aa0-a8d7-981430128d7d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8db83eaa-e44a-4805-a120-4acf4c97a425", "AQAAAAEAACcQAAAAEH7teBg3rONttuIx6Kah3FV9jzPlDLTx3asJjl8fisi77OhdARqCcUjVw5nf001FGw==" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedDate", "GenreName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0a6bb5e9-af1f-40f1-940a-81c0be6c8146"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4a7d27a6-4425-4dda-9878-bd7b4a2e6b47"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open-World", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f9119a92-bda6-47a0-9e07-9a940907974d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Multiplayer", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4a12d8e7-b6b1-44b7-a340-7da999e298af"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action RPG", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7b3b8f50-47a9-4b8b-b2e9-237abb54ee9c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simulation", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d74753d0-73f0-4049-8298-dcccbf83520e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horror", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2db9e1b6-c832-49a0-801a-4124d7bf293c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports & Racing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("48953ac1-fa15-4696-b7fa-5f60ce79e8eb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Role-Playing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5d75c232-24ce-41f2-ad32-a2f6c0ca6e91"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Visual Novel", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserAvatar",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("ad3af56a-cb1e-4809-b2cd-13fcdbe606ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "UserThumbnail",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("9c8f3548-c709-46c0-936d-6b6dfb387d4e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0a6bb5e9-af1f-40f1-940a-81c0be6c8146"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2db9e1b6-c832-49a0-801a-4124d7bf293c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("48953ac1-fa15-4696-b7fa-5f60ce79e8eb"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4a12d8e7-b6b1-44b7-a340-7da999e298af"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4a7d27a6-4425-4dda-9878-bd7b4a2e6b47"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("5d75c232-24ce-41f2-ad32-a2f6c0ca6e91"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7b3b8f50-47a9-4b8b-b2e9-237abb54ee9c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d74753d0-73f0-4049-8298-dcccbf83520e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f9119a92-bda6-47a0-9e07-9a940907974d"));

            migrationBuilder.DeleteData(
                table: "UserAvatar",
                keyColumn: "Id",
                keyValue: new Guid("ad3af56a-cb1e-4809-b2cd-13fcdbe606ac"));

            migrationBuilder.DeleteData(
                table: "UserThumbnail",
                keyColumn: "Id",
                keyValue: new Guid("9c8f3548-c709-46c0-936d-6b6dfb387d4e"));

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52503f03-bdea-4bf8-8a1a-d21ae2646483"),
                column: "ConcurrencyStamp",
                value: "6cf7d56b-b185-4962-8f6c-75ca382d28bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0d8bab81-4feb-419a-988e-d1b2cd764736");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f2267653-6ab1-4369-ab4d-f17d4b486794", "AQAAAAEAACcQAAAAEIAoUNAJH4iGwx/jupYqyWy88f4l3/2i4DeTx2I6w96NzzHe7v2CT/Ki7kSsXH2TKw==" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedDate", "GenreName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("22b1610c-39b0-41eb-98c4-ac6fb91ea934"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7c5c7b4c-29d7-477a-b92e-319bd763d8fc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open-World", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ceb68203-1c09-4980-8072-755d8cc2d522"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Multiplayer", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("032c6796-8097-4577-9b96-74925e589d5b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action RPG", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5ea0ef3d-7445-4bde-b738-dfb020e06e58"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simulation", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ded48134-97e3-4509-a000-91d34d0e1005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horror", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e52937c8-308e-4735-8653-db791a0b54b8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports & Racing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fd8c6d07-5842-41e1-8aa9-194da61dda1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Role-Playing", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("dcfa4907-09c6-4e48-86cd-25d1cc146b86"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Visual Novel", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserAvatar",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("29e6e928-d295-417e-a92d-2d3a755f108b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "UserThumbnail",
                columns: new[] { "Id", "CreatedDate", "ImagePath", "Status", "UpdateDate", "UpdatedDate", "UserID" },
                values: new object[] { new Guid("ba6ea26f-062f-4422-8fea-e2ab157460e0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imgnotfound.jpg", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });
        }
    }
}
