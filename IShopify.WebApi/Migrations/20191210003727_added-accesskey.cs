using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IShopify.WebApi.Migrations
{
    public partial class addedaccesskey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateUtc",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateUtc",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateUtc",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateUtc",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateUtc",
                table: "Departments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthProfile",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateUtc",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateUtc",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateUtc",
                table: "Attributes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateUtc",
                table: "Addresses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccessKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ExpirationUtc = table.Column<DateTime>(nullable: false),
                    Scopes = table.Column<string>(nullable: true),
                    ResourceIds = table.Column<Guid[]>(nullable: true),
                    MaxAllowedAttempts = table.Column<int>(nullable: false),
                    Attempts = table.Column<int>(nullable: false),
                    StartTimeUtc = table.Column<DateTime>(nullable: true),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false),
                    DeletedDateUtc = table.Column<DateTime>(nullable: true),
                    DeleteDateUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessKeys_Customers_UserId",
                        column: x => x.UserId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessKeys_UserId",
                table: "AccessKeys",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessKeys");

            migrationBuilder.DropColumn(
                name: "DeleteDateUtc",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "DeleteDateUtc",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeleteDateUtc",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeleteDateUtc",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DeleteDateUtc",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AuthProfile",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeleteDateUtc",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeleteDateUtc",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeleteDateUtc",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "DeleteDateUtc",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customers",
                type: "text",
                nullable: true);
        }
    }
}
