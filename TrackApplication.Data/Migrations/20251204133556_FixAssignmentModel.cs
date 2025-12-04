using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackApplicationData.Migrations
{
    /// <inheritdoc />
    public partial class FixAssignmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Tickets_ITSupportId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "TicktId",
                table: "Assignments",
                newName: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_TicketId",
                table: "Assignments",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Tickets_TicketId",
                table: "Assignments",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Tickets_TicketId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_TicketId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Assignments",
                newName: "TicktId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Tickets_ITSupportId",
                table: "Assignments",
                column: "ITSupportId",
                principalTable: "Tickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
