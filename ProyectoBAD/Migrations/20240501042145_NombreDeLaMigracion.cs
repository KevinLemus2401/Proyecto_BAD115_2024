using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoBAD.Migrations
{
    public partial class NombreDeLaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ENCUESTADO",
                columns: table => new
                {
                    ID_ENCUESTADO = table.Column<int>(type: "int", nullable: false),
                    EMAIL_ENCUESTADO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FECHA_NAC_ENCUESTA = table.Column<DateTime>(type: "datetime", nullable: false),
                    GEN_ENCUESTADO = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENCUESTADO", x => x.ID_ENCUESTADO);
                });

            migrationBuilder.CreateTable(
                name: "TIPOPREGUNTA",
                columns: table => new
                {
                    TIPO_PREGUNTA_ID = table.Column<int>(type: "int", nullable: false),
                    NOMBRE_TIPO_PREGUNTA = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DESCRIPCION_TIPO_PREGUNTA = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPOPREGUNTA", x => x.TIPO_PREGUNTA_ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    EMAIL_USUARIO = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TELEFONO_USUARIO = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    PRIMER_NOMBRE_USUARIO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SEGUNDO_NOMBRE_USUARIO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PRIMER_APELLIDO_USUARIO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SEGUNDO_APELLIDO_USUARIO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GEN_USUARIO = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "ENCUESTA",
                columns: table => new
                {
                    ID_ENCUESTA = table.Column<int>(type: "int", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: true),
                    TITULO_ENCUESTA = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    OBJETIVO_ENCUESTA = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    GRUPOMETA_ENCUESTA = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    INDICACIONES_ENCUESTA = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    FECHA_ENCUESTA = table.Column<DateTime>(type: "datetime2", rowVersion: true, nullable: false),
                    ESTADO_ENCUESTA = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENCUESTA", x => x.ID_ENCUESTA);
                    table.ForeignKey(
                        name: "FK_ENCUESTA_USUARIO_E_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "PREGUNTA",
                columns: table => new
                {
                    ID_PREGUNTA = table.Column<int>(type: "int", nullable: false),
                    ID_ENCUESTA = table.Column<int>(type: "int", nullable: true),
                    TIPO_PREGUNTA_ID = table.Column<int>(type: "int", nullable: true),
                    DESCRIPCION_PREGUNTA = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    REQUERIDA_PREGUNTA = table.Column<bool>(type: "bit", nullable: true),
                    ORDEN_PREGUNTA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PREGUNTA", x => x.ID_PREGUNTA);
                    table.ForeignKey(
                        name: "FK_PREGUNTA_REL_ENCUE_ENCUESTA",
                        column: x => x.ID_ENCUESTA,
                        principalTable: "ENCUESTA",
                        principalColumn: "ID_ENCUESTA");
                    table.ForeignKey(
                        name: "FK_PREGUNTA_REL_TIPOP_TIPOPREG",
                        column: x => x.TIPO_PREGUNTA_ID,
                        principalTable: "TIPOPREGUNTA",
                        principalColumn: "TIPO_PREGUNTA_ID");
                });

            migrationBuilder.CreateTable(
                name: "OPCIONPREGUNTA",
                columns: table => new
                {
                    OPCION_ID = table.Column<int>(type: "int", nullable: false),
                    ID_PREGUNTA = table.Column<int>(type: "int", nullable: true),
                    VALOR_OPCION = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    DESCRIPCION_OPCION = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    ESTADO_OPCION = table.Column<bool>(type: "bit", nullable: true),
                    ORDEN_OPCION = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPCIONPREGUNTA", x => x.OPCION_ID);
                    table.ForeignKey(
                        name: "FK_OPCIONPR_REL_PREGU_PREGUNTA",
                        column: x => x.ID_PREGUNTA,
                        principalTable: "PREGUNTA",
                        principalColumn: "ID_PREGUNTA");
                });

            migrationBuilder.CreateTable(
                name: "RESPUESTA",
                columns: table => new
                {
                    RESPUESTA_ID = table.Column<int>(type: "int", nullable: false),
                    OPCION_ID = table.Column<int>(type: "int", nullable: true),
                    ID_PREGUNTA = table.Column<int>(type: "int", nullable: true),
                    ID_ENCUESTADO = table.Column<int>(type: "int", nullable: true),
                    ID_ENCUESTA = table.Column<int>(type: "int", nullable: true),
                    FECHA_RESPUESTA = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    TEXTO_RESPUESTA = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESPUESTA", x => x.RESPUESTA_ID);
                    table.ForeignKey(
                        name: "FK_RESPUEST_REL_ENCUE_ENCUESTA",
                        column: x => x.ID_ENCUESTADO,
                        principalTable: "ENCUESTADO",
                        principalColumn: "ID_ENCUESTADO");
                    table.ForeignKey(
                        name: "FK_RESPUEST_REL_RESPU_OPCIONPR",
                        column: x => x.OPCION_ID,
                        principalTable: "OPCIONPREGUNTA",
                        principalColumn: "OPCION_ID");
                    table.ForeignKey(
                        name: "FK_RESPUEST_REL_RESPU_PREGUNTA",
                        column: x => x.ID_PREGUNTA,
                        principalTable: "PREGUNTA",
                        principalColumn: "ID_PREGUNTA");
                    table.ForeignKey(
                        name: "FK_RESPUEST_RELATIONS_ENCUESTA",
                        column: x => x.ID_ENCUESTA,
                        principalTable: "ENCUESTA",
                        principalColumn: "ID_ENCUESTA");
                });

            migrationBuilder.CreateIndex(
                name: "USUARIO_ENCUESTA_FK",
                table: "ENCUESTA",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "REL_PREGUNTA_OPCIONPREGUNTA_FK",
                table: "OPCIONPREGUNTA",
                column: "ID_PREGUNTA");

            migrationBuilder.CreateIndex(
                name: "REL_ENCUESTA_PREGUNTA_FK",
                table: "PREGUNTA",
                column: "ID_ENCUESTA");

            migrationBuilder.CreateIndex(
                name: "REL_TIPOPREGUNTA_PREGUNTA_FK",
                table: "PREGUNTA",
                column: "TIPO_PREGUNTA_ID");

            migrationBuilder.CreateIndex(
                name: "REL_ENCUESTADO_RESPUESTA_FK",
                table: "RESPUESTA",
                column: "ID_ENCUESTADO");

            migrationBuilder.CreateIndex(
                name: "REL_RESPUESTA_OPCIONPREGUNTA_FK",
                table: "RESPUESTA",
                column: "OPCION_ID");

            migrationBuilder.CreateIndex(
                name: "REL_RESPUESTA_PREGUNTA_FK",
                table: "RESPUESTA",
                column: "ID_PREGUNTA");

            migrationBuilder.CreateIndex(
                name: "RELATIONSHIP_7_FK",
                table: "RESPUESTA",
                column: "ID_ENCUESTA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RESPUESTA");

            migrationBuilder.DropTable(
                name: "ENCUESTADO");

            migrationBuilder.DropTable(
                name: "OPCIONPREGUNTA");

            migrationBuilder.DropTable(
                name: "PREGUNTA");

            migrationBuilder.DropTable(
                name: "ENCUESTA");

            migrationBuilder.DropTable(
                name: "TIPOPREGUNTA");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
