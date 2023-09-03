using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ATULIAZACAMPOINDEX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdVaga",
                table: "Movimento",
                column: "IdVaga");

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdVeiculo",
                table: "Movimento",
                column: "IdVeiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movimento_IdVaga",
                table: "Movimento");

            migrationBuilder.DropIndex(
                name: "IX_Movimento_IdVeiculo",
                table: "Movimento");
        }
    }
}
