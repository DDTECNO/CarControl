using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ATULIAZAChaveEstrangeira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Operacao_IdMovimento",
                table: "Movimento");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Operacao_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao",
                principalTable: "Operacao",
                principalColumn: "IdTpOperacao",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Operacao_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Operacao_IdMovimento",
                table: "Movimento",
                column: "IdMovimento",
                principalTable: "Operacao",
                principalColumn: "IdTpOperacao",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
