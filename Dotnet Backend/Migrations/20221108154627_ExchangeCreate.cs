using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace users_items_backend.Migrations
{
    public partial class ExchangeCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    senderId = table.Column<int>(type: "INTEGER", nullable: false),
                    recieverId = table.Column<int>(type: "INTEGER", nullable: false),
                    itemName = table.Column<string>(type: "TEXT", nullable: false),
                    itemDescription = table.Column<string>(type: "TEXT", nullable: false),
                    itemQuantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exchanges_Users_recieverId",
                        column: x => x.recieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exchanges_Users_senderId",
                        column: x => x.senderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_recieverId",
                table: "Exchanges",
                column: "recieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_senderId",
                table: "Exchanges",
                column: "senderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exchanges");
        }
    }
}
