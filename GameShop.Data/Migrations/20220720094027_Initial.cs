using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseEntity",
                columns: table => new
                {
                    BaseEntityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.BaseEntityID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseEntityID = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    GameName = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Discount = table.Column<int>(nullable: false, defaultValue: 0),
                    Description = table.Column<string>(nullable: false),
                    Gameplay = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameID);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "GameImages",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: false),
                    isDefault = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Filesize = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameImages", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_GameImages_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemRequirementMin",
                columns: table => new
                {
                    SRMID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OS = table.Column<string>(nullable: true),
                    Processor = table.Column<string>(nullable: true),
                    Memory = table.Column<string>(nullable: true),
                    Graphics = table.Column<string>(nullable: true),
                    Storage = table.Column<string>(nullable: true),
                    AdditionalNotes = table.Column<string>(nullable: true),
                    GameID = table.Column<int>(nullable: false),
                    Soundcard = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRequirementMin", x => x.SRMID);
                    table.ForeignKey(
                        name: "FK_SystemRequirementMin_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemRequirementRecommended",
                columns: table => new
                {
                    SRRID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OS = table.Column<string>(nullable: true),
                    Processor = table.Column<string>(nullable: true),
                    Memory = table.Column<string>(nullable: true),
                    Graphics = table.Column<string>(nullable: true),
                    Storage = table.Column<string>(nullable: true),
                    AdditionalNotes = table.Column<string>(nullable: true),
                    GameID = table.Column<int>(nullable: false),
                    Soundcard = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRequirementRecommended", x => x.SRRID);
                    table.ForeignKey(
                        name: "FK_SystemRequirementRecommended_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameinGenre",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false),
                    GenreID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameinGenre", x => new { x.GenreID, x.GameID });
                    table.ForeignKey(
                        name: "FK_GameinGenre_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameinGenre_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameID", "BaseEntityID", "CreatedDate", "Description", "GameName", "Gameplay", "Price", "Status", "UpdatedDate" },
                values: new object[] { 1, 0, new DateTime(2022, 7, 20, 16, 40, 27, 266, DateTimeKind.Local).AddTicks(5169), "The best game in the world", "Grand Theft Auto V", "Destroy the city", 250000m, 1, new DateTime(2022, 7, 20, 16, 40, 27, 267, DateTimeKind.Local).AddTicks(1905) });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameID", "BaseEntityID", "CreatedDate", "Description", "Discount", "GameName", "Gameplay", "Price", "Status", "UpdatedDate" },
                values: new object[] { 2, 0, new DateTime(2022, 7, 20, 16, 40, 27, 267, DateTimeKind.Local).AddTicks(2396), "Back to the cowboy town", 20, "Red Dead Redemption 2", "Discover the cowboy world", 250000m, 1, new DateTime(2022, 7, 20, 16, 40, 27, 267, DateTimeKind.Local).AddTicks(2419) });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreID", "GenreName" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Open-World" },
                    { 3, "Multiplayer" }
                });

            migrationBuilder.InsertData(
                table: "GameinGenre",
                columns: new[] { "GenreID", "GameID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "SystemRequirementMin",
                columns: new[] { "SRMID", "AdditionalNotes", "GameID", "Graphics", "Memory", "OS", "Processor", "Soundcard", "Storage" },
                values: new object[,]
                {
                    { 1, "", 1, "NVIDIA 9800 GT 1GB / AMD HD 4870 1GB (DX 10, 10.1, 11)", "4 GB RAM", "Windows 10 64 Bit, Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1", "Intel Core 2 Quad CPU Q6600 @ 2.40GHz (4 CPUs) / AMD Phenom 9850 Quad-Core Processor (4 CPUs) @ 2.5GHz", "100% DirectX 10 compatible", "72 GB available space" },
                    { 2, "", 2, "NVIDIA 9800 GT 1GB / AMD HD 4870 1GB (DX 10, 10.1, 11)", "4 GB RAM", "Windows 10 64 Bit, Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1", "Intel Core 2 Quad CPU Q6600 @ 2.40GHz (4 CPUs) / AMD Phenom 9850 Quad-Core Processor (4 CPUs) @ 2.5GHz", "100% DirectX 10 compatible", "72 GB available space" }
                });

            migrationBuilder.InsertData(
                table: "SystemRequirementRecommended",
                columns: new[] { "SRRID", "AdditionalNotes", "GameID", "Graphics", "Memory", "OS", "Processor", "Soundcard", "Storage" },
                values: new object[,]
                {
                    { 1, "", 1, "NVIDIA GTX 660 2GB / AMD HD 7870 2GB", "8 GB RAM", "Windows 10 64 Bit, Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1", " Intel Core i5 3470 @ 3.2GHz (4 CPUs) / AMD X8 FX-8350 @ 4GHz (8 CPUs)", "100% DirectX 10 compatible", "72 GB available space" },
                    { 2, "", 2, "NVIDIA GTX 660 2GB / AMD HD 7870 2GB", "8 GB RAM", "Windows 10 64 Bit, Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1", " Intel Core i5 3470 @ 3.2GHz (4 CPUs) / AMD X8 FX-8350 @ 4GHz (8 CPUs)", "100% DirectX 10 compatible", "72 GB available space" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameImages_GameID",
                table: "GameImages",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_GameinGenre_GameID",
                table: "GameinGenre",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRequirementMin_GameID",
                table: "SystemRequirementMin",
                column: "GameID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemRequirementRecommended_GameID",
                table: "SystemRequirementRecommended",
                column: "GameID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseEntity");

            migrationBuilder.DropTable(
                name: "GameImages");

            migrationBuilder.DropTable(
                name: "GameinGenre");

            migrationBuilder.DropTable(
                name: "SystemRequirementMin");

            migrationBuilder.DropTable(
                name: "SystemRequirementRecommended");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
