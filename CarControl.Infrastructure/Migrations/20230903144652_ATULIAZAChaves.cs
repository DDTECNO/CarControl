using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ATULIAZAChaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Operacao_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Vaga_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Veiculo_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Operacao_IdMovimento",
                table: "Movimento",
                column: "IdMovimento",
                principalTable: "Operacao",
                principalColumn: "IdTpOperacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Vaga_IdVaga",
                table: "Movimento",
                column: "IdVaga",
                principalTable: "Vaga",
                principalColumn: "IdVaga",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Veiculo_IdVeiculo",
                table: "Movimento",
                column: "IdVeiculo",
                principalTable: "Veiculo",
                principalColumn: "IdVeiculo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Operacao_IdMovimento",
                table: "Movimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Vaga_IdVaga",
                table: "Movimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Veiculo_IdVeiculo",
                table: "Movimento");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Operacao_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao",
                principalTable: "Operacao",
                principalColumn: "IdTpOperacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Vaga_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao",
                principalTable: "Vaga",
                principalColumn: "IdVaga",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Veiculo_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao",
                principalTable: "Veiculo",
                principalColumn: "IdVeiculo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
