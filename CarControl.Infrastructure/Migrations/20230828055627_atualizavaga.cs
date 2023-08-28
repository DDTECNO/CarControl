using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class atualizavaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movimento_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.DropIndex(
                name: "IX_Movimento_IdVaga",
                table: "Movimento");

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdVaga",
                table: "Movimento",
                column: "IdVaga",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movimento_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.DropIndex(
                name: "IX_Movimento_IdVaga",
                table: "Movimento");

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao");

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdVaga",
                table: "Movimento",
                column: "IdVaga");
        }
    }
}
