using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketingSystemBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketCategories_CategoryId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Tickets",
                newName: "TicketCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_CategoryId",
                table: "Tickets",
                newName: "IX_Tickets_TicketCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketCategories_TicketCategoryId",
                table: "Tickets",
                column: "TicketCategoryId",
                principalTable: "TicketCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketCategories_TicketCategoryId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "TicketCategoryId",
                table: "Tickets",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_TicketCategoryId",
                table: "Tickets",
                newName: "IX_Tickets_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketCategories_CategoryId",
                table: "Tickets",
                column: "CategoryId",
                principalTable: "TicketCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
