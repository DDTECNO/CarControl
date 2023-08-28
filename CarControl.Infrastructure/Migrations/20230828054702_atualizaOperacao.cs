using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class atualizaOperacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Operacao_IdTpOperacao1",
                table: "Movimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Vaga_IdVaga1",
                table: "Movimento");

            migrationBuilder.RenameColumn(
                name: "IdVaga1",
                table: "Movimento",
                newName: "IdVaga");

            migrationBuilder.RenameColumn(
                name: "IdTpOperacao1",
                table: "Movimento",
                newName: "IdTpOperacao");

            migrationBuilder.RenameIndex(
                name: "IX_Movimento_IdVaga1",
                table: "Movimento",
                newName: "IX_Movimento_IdVaga");

            migrationBuilder.RenameIndex(
                name: "IX_Movimento_IdTpOperacao1",
                table: "Movimento",
                newName: "IX_Movimento_IdTpOperacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Operacao_IdTpOperacao",
                table: "Movimento",
                column: "IdTpOperacao",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Operacao_IdTpOperacao",
                table: "Movimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Vaga_IdVaga",
                table: "Movimento");

            migrationBuilder.RenameColumn(
                name: "IdVaga",
                table: "Movimento",
                newName: "IdVaga1");

            migrationBuilder.RenameColumn(
                name: "IdTpOperacao",
                table: "Movimento",
                newName: "IdTpOperacao1");

            migrationBuilder.RenameIndex(
                name: "IX_Movimento_IdVaga",
                table: "Movimento",
                newName: "IX_Movimento_IdVaga1");

            migrationBuilder.RenameIndex(
                name: "IX_Movimento_IdTpOperacao",
                table: "Movimento",
                newName: "IX_Movimento_IdTpOperacao1");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Operacao_IdTpOperacao1",
                table: "Movimento",
                column: "IdTpOperacao1",
                principalTable: "Operacao",
                principalColumn: "IdTpOperacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Vaga_IdVaga1",
                table: "Movimento",
                column: "IdVaga1",
                principalTable: "Vaga",
                principalColumn: "IdVaga",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
