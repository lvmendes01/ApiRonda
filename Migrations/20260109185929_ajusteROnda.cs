using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RondaSegurancaBack.Migrations
{
    /// <inheritdoc />
    public partial class ajusteROnda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocorrencias_Rondas_RondaId",
                table: "Ocorrencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Rondas_AspNetUsers_UsuarioId",
                table: "Rondas");

            migrationBuilder.DropIndex(
                name: "IX_Rondas_UsuarioId",
                table: "Rondas");

            migrationBuilder.DropIndex(
                name: "IX_Ocorrencias_RondaId",
                table: "Ocorrencias");

            migrationBuilder.DropColumn(
                name: "RondaId",
                table: "Ocorrencias");

            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "Rondas",
                newName: "DataHoraInicioRealizada");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Rondas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraCriacao",
                table: "Rondas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraFimPlanejada",
                table: "Rondas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraFimRealizada",
                table: "Rondas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraInicioPlanejada",
                table: "Rondas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCriacaoId",
                table: "Rondas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RondaId",
                table: "AparelhoLocalizacoes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHoraCriacao",
                table: "Rondas");

            migrationBuilder.DropColumn(
                name: "DataHoraFimPlanejada",
                table: "Rondas");

            migrationBuilder.DropColumn(
                name: "DataHoraFimRealizada",
                table: "Rondas");

            migrationBuilder.DropColumn(
                name: "DataHoraInicioPlanejada",
                table: "Rondas");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Rondas");

            migrationBuilder.DropColumn(
                name: "RondaId",
                table: "AparelhoLocalizacoes");

            migrationBuilder.RenameColumn(
                name: "DataHoraInicioRealizada",
                table: "Rondas",
                newName: "DataHora");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Rondas",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "RondaId",
                table: "Ocorrencias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rondas_UsuarioId",
                table: "Rondas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_RondaId",
                table: "Ocorrencias",
                column: "RondaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencias_Rondas_RondaId",
                table: "Ocorrencias",
                column: "RondaId",
                principalTable: "Rondas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rondas_AspNetUsers_UsuarioId",
                table: "Rondas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
