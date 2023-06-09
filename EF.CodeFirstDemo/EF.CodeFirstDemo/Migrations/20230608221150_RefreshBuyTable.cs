using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF.CodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class RefreshBuyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemoBuy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoBuy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DemoBuyItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DemoBuyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoBuyItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemoBuyItem_DemoBuy_DemoBuyId",
                        column: x => x.DemoBuyId,
                        principalTable: "DemoBuy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemoBuyItem_DemoProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "DemoProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemoBuyItem_DemoBuyId",
                table: "DemoBuyItem",
                column: "DemoBuyId");

            migrationBuilder.CreateIndex(
                name: "IX_DemoBuyItem_ProductId",
                table: "DemoBuyItem",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemoBuyItem");

            migrationBuilder.DropTable(
                name: "DemoBuy");
        }
    }
}
