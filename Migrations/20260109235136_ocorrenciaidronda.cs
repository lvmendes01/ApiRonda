using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RondaSegurancaBack.Migrations
{
    /// <inheritdoc />
    public partial class ocorrenciaidronda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RondaId",
                table: "Ocorrencias",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RondaId",
                table: "Ocorrencias");
        }
    }
}
