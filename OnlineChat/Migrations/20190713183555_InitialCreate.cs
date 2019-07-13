using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OnlineChat.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    ChatName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Chats_ChatName",
                        column: x => x.ChatName,
                        principalTable: "Chats",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(nullable: true),
                    CTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ChatName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatName",
                        column: x => x.ChatName,
                        principalTable: "Chats",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Chats",
                column: "Name",
                values: new object[]
                {
                    "Developers",
                    "Designers"
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ChatName", "Name" },
                values: new object[,]
                {
                    { 1, "Developers", "Valera" },
                    { 2, "Developers", "Max" },
                    { 3, "Developers", "Tom" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "CTime", "ChatName", "Text", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 7, 13, 20, 35, 55, 13, DateTimeKind.Local).AddTicks(4860), "Developers", "Hey there", 1 },
                    { 2, new DateTime(2019, 7, 13, 20, 35, 55, 19, DateTimeKind.Local).AddTicks(990), "Developers", "What's up", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatName",
                table: "Messages",
                column: "ChatName");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatName",
                table: "Users",
                column: "ChatName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Chats");
        }
    }
}
