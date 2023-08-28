using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TpMovimento : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "IdVaga1",
                table: "Movimento",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdTpOperacao1",
                table: "Movimento",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<char>(
                name: "TpMovimento",
                table: "Movimento",
                type: "TEXT",
                nullable: false,
                defaultValue: 'E');

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Operacao_IdTpOperacao1",
                table: "Movimento");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimento_Vaga_IdVaga1",
                table: "Movimento");

            migrationBuilder.DropColumn(
                name: "TpMovimento",
                table: "Movimento");

            migrationBuilder.AlterColumn<int>(
                name: "IdVaga1",
                table: "Movimento",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "IdTpOperacao1",
                table: "Movimento",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Operacao_IdTpOperacao1",
                table: "Movimento",
                column: "IdTpOperacao1",
                principalTable: "Operacao",
                principalColumn: "IdTpOperacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimento_Vaga_IdVaga1",
                table: "Movimento",
                column: "IdVaga1",
                principalTable: "Vaga",
                principalColumn: "IdVaga");
        }
    }
}
