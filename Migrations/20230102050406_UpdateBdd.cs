using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetelearebeccagr3.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Films");

            migrationBuilder.AddColumn<int>(
                name: "CinemaId",
                table: "Seances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Realisateur",
                table: "Films",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Seances_CinemaId",
                table: "Seances",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seances_Cinemas_CinemaId",
                table: "Seances",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seances_Cinemas_CinemaId",
                table: "Seances");

            migrationBuilder.DropIndex(
                name: "IX_Seances_CinemaId",
                table: "Seances");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "Seances");

            migrationBuilder.AlterColumn<string>(
                name: "Realisateur",
                table: "Films",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Statut",
                table: "Films",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
