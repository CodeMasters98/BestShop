using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Products_ProductId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ProductId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Tags");

            migrationBuilder.EnsureSchema(
                name: "BASE");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tags",
                newSchema: "BASE");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products",
                newSchema: "BASE");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories",
                newSchema: "BASE");

            migrationBuilder.CreateTable(
                name: "ProductTag",
                schema: "BASE",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => new { x.ProductsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ProductTag_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalSchema: "BASE",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalSchema: "BASE",
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "BASE",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_TagsId",
                schema: "BASE",
                table: "ProductTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTag",
                schema: "BASE");

            migrationBuilder.DropTable(
                name: "User",
                schema: "BASE");

            migrationBuilder.RenameTable(
                name: "Tags",
                schema: "BASE",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "BASE",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "BASE",
                newName: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ProductId",
                table: "Tags",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Products_ProductId",
                table: "Tags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
