using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSalleDBSET : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salle_Filiales_FilialeId",
                table: "Salle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salle",
                table: "Salle");

            migrationBuilder.RenameTable(
                name: "Salle",
                newName: "Salles");

            migrationBuilder.RenameIndex(
                name: "IX_Salle_FilialeId",
                table: "Salles",
                newName: "IX_Salles_FilialeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salles",
                table: "Salles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Salles_Filiales_FilialeId",
                table: "Salles",
                column: "FilialeId",
                principalTable: "Filiales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salles_Filiales_FilialeId",
                table: "Salles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salles",
                table: "Salles");

            migrationBuilder.RenameTable(
                name: "Salles",
                newName: "Salle");

            migrationBuilder.RenameIndex(
                name: "IX_Salles_FilialeId",
                table: "Salle",
                newName: "IX_Salle_FilialeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salle",
                table: "Salle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Salle_Filiales_FilialeId",
                table: "Salle",
                column: "FilialeId",
                principalTable: "Filiales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
