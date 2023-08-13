using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarControl.Infrastructure.Migrations
{
    public partial class Incial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operacao",
                columns: table => new
                {
                    IdTpOperacao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nmOperacao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacao", x => x.IdTpOperacao);
                });

            migrationBuilder.CreateTable(
                name: "Vaga",
                columns: table => new
                {
                    IdVaga = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NmVaga = table.Column<string>(type: "TEXT", nullable: true),
                    flVaga = table.Column<char>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaga", x => x.IdVaga);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    IdVeiculo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Marca = table.Column<string>(type: "TEXT", nullable: true),
                    Modelo = table.Column<string>(type: "TEXT", nullable: true),
                    TpVeiculo = table.Column<string>(type: "TEXT", nullable: true),
                    Cor = table.Column<string>(type: "TEXT", nullable: true),
                    PlacaVeiculo = table.Column<string>(type: "TEXT", nullable: true),
                    NmCondutor = table.Column<string>(type: "TEXT", nullable: true),
                    CpfCondutor = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.IdVeiculo);
                });

            migrationBuilder.CreateTable(
                name: "Movimento",
                columns: table => new
                {
                    IdVeiculo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMovimento = table.Column<int>(type: "INTEGER", nullable: false),
                    DtEntrada = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DtSaida = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HrEntrada = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    HrSaida = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    IdVaga1 = table.Column<int>(type: "INTEGER", nullable: true),
                    IdTpOperacao1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimento", x => new { x.IdMovimento, x.IdVeiculo });
                    table.ForeignKey(
                        name: "FK_Movimento_Operacao_IdTpOperacao1",
                        column: x => x.IdTpOperacao1,
                        principalTable: "Operacao",
                        principalColumn: "IdTpOperacao",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimento_Vaga_IdVaga1",
                        column: x => x.IdVaga1,
                        principalTable: "Vaga",
                        principalColumn: "IdVaga",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdTpOperacao1",
                table: "Movimento",
                column: "IdTpOperacao1");

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdVaga1",
                table: "Movimento",
                column: "IdVaga1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimento");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "Operacao");

            migrationBuilder.DropTable(
                name: "Vaga");
        }
    }
}
