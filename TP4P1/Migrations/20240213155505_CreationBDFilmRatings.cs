using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TP4P1.Migrations
{
    public partial class CreationBDFilmRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_film_flm",
                columns: table => new
                {
                    flm_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    flm_titre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    flm_resume = table.Column<string>(type: "text", nullable: false),
                    flm_datesortie = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    flm_duree = table.Column<decimal>(type: "numeric(3,0)", nullable: false),
                    flm_genre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_film_flm", x => x.flm_id);
                });

            migrationBuilder.CreateTable(
                name: "T_E_UTILISATEUR_UTL",
                columns: table => new
                {
                    utl_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    utl_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    utl_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    utl_mobile = table.Column<string>(type: "char(10)", nullable: false),
                    utl_mail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    utl_pwd = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    utl_rue = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    utl_cp = table.Column<string>(type: "char(100)", maxLength: 100, nullable: false),
                    utl_ville = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    utl_pays = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    utl_latitude = table.Column<float>(type: "real", nullable: false),
                    utl_longitude = table.Column<float>(type: "real", nullable: false),
                    utl_datecreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_E_UTILISATEUR_UTL", x => x.utl_id);
                });

            migrationBuilder.CreateTable(
                name: "t_j_notation_not",
                columns: table => new
                {
                    utl_id = table.Column<int>(type: "integer", nullable: false),
                    flm_id = table.Column<int>(type: "integer", nullable: false),
                    not_note = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_notation_not", x => new { x.utl_id, x.flm_id });
                    table.ForeignKey(
                        name: "FK_t_j_notation_not_t_e_film_flm_flm_id",
                        column: x => x.flm_id,
                        principalTable: "t_e_film_flm",
                        principalColumn: "flm_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_notation_not_T_E_UTILISATEUR_UTL_utl_id",
                        column: x => x.utl_id,
                        principalTable: "T_E_UTILISATEUR_UTL",
                        principalColumn: "utl_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_j_notation_not_flm_id",
                table: "t_j_notation_not",
                column: "flm_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_j_notation_not");

            migrationBuilder.DropTable(
                name: "t_e_film_flm");

            migrationBuilder.DropTable(
                name: "T_E_UTILISATEUR_UTL");
        }
    }
}
