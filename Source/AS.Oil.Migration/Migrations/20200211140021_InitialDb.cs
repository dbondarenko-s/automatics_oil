using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AS.Oil.Migration.Migrations
{
    public partial class InitialDb : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "category",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "storage",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_id = table.Column<int>(nullable: false),
                    max_volume = table.Column<double>(nullable: false),
                    min_volume = table.Column<double>(nullable: false),
                    volume = table.Column<double>(nullable: false),
                    name = table.Column<string>(maxLength: 1024, nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    create_dt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storage", x => x.id);
                    table.ForeignKey(
                        name: "FK_storage_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "dbo",
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_storage_category_id",
                schema: "dbo",
                table: "storage",
                column: "category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "storage",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "category",
                schema: "dbo");
        }
    }
}
