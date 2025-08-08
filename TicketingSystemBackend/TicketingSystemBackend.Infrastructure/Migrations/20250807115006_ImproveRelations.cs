using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketingSystemBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImproveRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Files_FileId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_FileId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "TicketCategories",
                newName: "CategoryName");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArticleFile",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "int", nullable: false),
                    FilesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleFile", x => new { x.ArticlesId, x.FilesId });
                    table.ForeignKey(
                        name: "FK_ArticleFile_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleFile_Files_FilesId",
                        column: x => x.FilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleCategoryId",
                table: "Articles",
                column: "ArticleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFile_FilesId",
                table: "ArticleFile",
                column: "FilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleCategories_ArticleCategoryId",
                table: "Articles",
                column: "ArticleCategoryId",
                principalTable: "ArticleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleCategories_ArticleCategoryId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleFile");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleCategoryId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "TicketCategories",
                newName: "Category");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_FileId",
                table: "Articles",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Files_FileId",
                table: "Articles",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id");
        }
    }
}
