using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RondaSegurancaBack.Migrations
{
    /// <inheritdoc />
    public partial class ajusteRondas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Rondas",
                newName: "UsuarioResponsavelId");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Rondas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "RondaId",
                table: "AparelhoLocalizacoes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Rondas");

            migrationBuilder.RenameColumn(
                name: "UsuarioResponsavelId",
                table: "Rondas",
                newName: "UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "RondaId",
                table: "AparelhoLocalizacoes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
