using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoBAD.Migrations
{
    public partial class Cambio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ENCUESTADO",
                columns: table => new
                {
                    ID_ENCUESTADO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    TIPO_PREGUNTA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    ID_USUARIO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    EMAIL_USUARIO = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TELEFONO_USUARIO = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    PRIMER_NOMBRE_USUARIO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SEGUNDO_NOMBRE_USUARIO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PRIMER_APELLIDO_USUARIO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SEGUNDO_APELLIDO_USUARIO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GEN_USUARIO = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_USUARIO_UserId",
                        column: x => x.UserId,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_USUARIO_UserId",
                        column: x => x.UserId,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_USUARIO_UserId",
                        column: x => x.UserId,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_USUARIO_UserId",
                        column: x => x.UserId,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ENCUESTA",
                columns: table => new
                {
                    ID_ENCUESTA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USUARIO = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TITULO_ENCUESTA = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    OBJETIVO_ENCUESTA = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    GRUPOMETA_ENCUESTA = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    INDICACIONES_ENCUESTA = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    FECHA_ENCUESTA = table.Column<DateTime>(type: "datetime", nullable: true),
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
                    ID_PREGUNTA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    OPCION_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    RESPUESTA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OPCION_ID = table.Column<int>(type: "int", nullable: true),
                    ID_PREGUNTA = table.Column<int>(type: "int", nullable: true),
                    ID_ENCUESTADO = table.Column<int>(type: "int", nullable: true),
                    ID_ENCUESTA = table.Column<int>(type: "int", nullable: true),
                    FECHA_RESPUESTA = table.Column<DateTime>(type: "datetime", nullable: true),
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

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

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "USUARIO",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "USUARIO",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "RESPUESTA");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

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
