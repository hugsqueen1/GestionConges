using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionConges.Migrations
{
    /// <inheritdoc />
    public partial class AjoutChampsEmployeEtSuivi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeSuivi",
                table: "Conges",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomEmploye",
                table: "Conges",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PosteEmploye",
                table: "Conges",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Statut",
                table: "Conges",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeSuivi",
                table: "Conges");

            migrationBuilder.DropColumn(
                name: "NomEmploye",
                table: "Conges");

            migrationBuilder.DropColumn(
                name: "PosteEmploye",
                table: "Conges");

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Conges");
        }
    }
}
