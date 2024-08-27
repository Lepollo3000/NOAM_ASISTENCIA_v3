using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NOAM_ASISTENCIA_v3.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "aplicacion");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Lockout = table.Column<bool>(type: "bit", nullable: false),
                    ForgotPassword = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                schema: "aplicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaUtcAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaUtcEdita = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaUtcElimina = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioAltaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioEditaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioEliminaId = table.Column<int>(type: "int", nullable: true),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sucursal_AspNetUsers_UsuarioAltaId",
                        column: x => x.UsuarioAltaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sucursal_AspNetUsers_UsuarioEditaId",
                        column: x => x.UsuarioEditaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sucursal_AspNetUsers_UsuarioEliminaId",
                        column: x => x.UsuarioEliminaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                schema: "aplicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time", nullable: false),
                    FechaUtcAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaUtcEdita = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaUtcElimina = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioAltaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioEditaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioEliminaId = table.Column<int>(type: "int", nullable: true),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turno_AspNetUsers_UsuarioAltaId",
                        column: x => x.UsuarioAltaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turno_AspNetUsers_UsuarioEditaId",
                        column: x => x.UsuarioEditaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Turno_AspNetUsers_UsuarioEliminaId",
                        column: x => x.UsuarioEliminaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Asistencia",
                schema: "aplicacion",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    FechaEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaUtcAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaUtcEdita = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaUtcElimina = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioAltaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioEditaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioEliminaId = table.Column<int>(type: "int", nullable: true),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia", x => new { x.UsuarioId, x.SucursalId, x.FechaEntrada });
                    table.ForeignKey(
                        name: "FK_Asistencia_AspNetUsers_UsuarioAltaId",
                        column: x => x.UsuarioAltaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistencia_AspNetUsers_UsuarioEditaId",
                        column: x => x.UsuarioEditaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asistencia_AspNetUsers_UsuarioEliminaId",
                        column: x => x.UsuarioEliminaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asistencia_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistencia_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalSchema: "aplicacion",
                        principalTable: "Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TurnoDia",
                schema: "aplicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    Dia = table.Column<int>(type: "int", nullable: false),
                    FechaUtcAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaUtcEdita = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaUtcElimina = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioAltaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioEditaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioEliminaId = table.Column<int>(type: "int", nullable: true),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnoDia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurnoDia_AspNetUsers_UsuarioAltaId",
                        column: x => x.UsuarioAltaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TurnoDia_AspNetUsers_UsuarioEditaId",
                        column: x => x.UsuarioEditaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TurnoDia_AspNetUsers_UsuarioEliminaId",
                        column: x => x.UsuarioEliminaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TurnoDia_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalSchema: "aplicacion",
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioTurno",
                schema: "aplicacion",
                columns: table => new
                {
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaUtcAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaUtcEdita = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaUtcElimina = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioAltaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioEditaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioEliminaId = table.Column<int>(type: "int", nullable: true),
                    EstaEliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTurno", x => new { x.TurnoId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuarioTurno_AspNetUsers_UsuarioAltaId",
                        column: x => x.UsuarioAltaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioTurno_AspNetUsers_UsuarioEditaId",
                        column: x => x.UsuarioEditaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioTurno_AspNetUsers_UsuarioEliminaId",
                        column: x => x.UsuarioEliminaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioTurno_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioTurno_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalSchema: "aplicacion",
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_SucursalId",
                schema: "aplicacion",
                table: "Asistencia",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_UsuarioAltaId",
                schema: "aplicacion",
                table: "Asistencia",
                column: "UsuarioAltaId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_UsuarioEditaId",
                schema: "aplicacion",
                table: "Asistencia",
                column: "UsuarioEditaId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_UsuarioEliminaId",
                schema: "aplicacion",
                table: "Asistencia",
                column: "UsuarioEliminaId");

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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_UsuarioAltaId",
                schema: "aplicacion",
                table: "Sucursal",
                column: "UsuarioAltaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_UsuarioEditaId",
                schema: "aplicacion",
                table: "Sucursal",
                column: "UsuarioEditaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_UsuarioEliminaId",
                schema: "aplicacion",
                table: "Sucursal",
                column: "UsuarioEliminaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turno_UsuarioAltaId",
                schema: "aplicacion",
                table: "Turno",
                column: "UsuarioAltaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turno_UsuarioEditaId",
                schema: "aplicacion",
                table: "Turno",
                column: "UsuarioEditaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turno_UsuarioEliminaId",
                schema: "aplicacion",
                table: "Turno",
                column: "UsuarioEliminaId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoDia_TurnoId",
                schema: "aplicacion",
                table: "TurnoDia",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoDia_UsuarioAltaId",
                schema: "aplicacion",
                table: "TurnoDia",
                column: "UsuarioAltaId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoDia_UsuarioEditaId",
                schema: "aplicacion",
                table: "TurnoDia",
                column: "UsuarioEditaId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoDia_UsuarioEliminaId",
                schema: "aplicacion",
                table: "TurnoDia",
                column: "UsuarioEliminaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTurno_UsuarioAltaId",
                schema: "aplicacion",
                table: "UsuarioTurno",
                column: "UsuarioAltaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTurno_UsuarioEditaId",
                schema: "aplicacion",
                table: "UsuarioTurno",
                column: "UsuarioEditaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTurno_UsuarioEliminaId",
                schema: "aplicacion",
                table: "UsuarioTurno",
                column: "UsuarioEliminaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTurno_UsuarioId",
                schema: "aplicacion",
                table: "UsuarioTurno",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencia",
                schema: "aplicacion");

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
                name: "TurnoDia",
                schema: "aplicacion");

            migrationBuilder.DropTable(
                name: "UsuarioTurno",
                schema: "aplicacion");

            migrationBuilder.DropTable(
                name: "Sucursal",
                schema: "aplicacion");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Turno",
                schema: "aplicacion");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
