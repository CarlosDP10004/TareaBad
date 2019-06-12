using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace gestioncomprasAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmpresaProveedora",
                columns: table => new
                {
                    IdEmpresaProveedora = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreEmpresa = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    DireccionEmpresa = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Responsable = table.Column<int>(nullable: false),
                    NITEmpresa = table.Column<string>(unicode: false, maxLength: 16, nullable: false),
                    LogotipoEmpresa = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    MontoPermitido = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaProveedora", x => x.IdEmpresaProveedora);
                });

            migrationBuilder.CreateTable(
                name: "Lista",
                columns: table => new
                {
                    IdLista = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoLista = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    NombreLista = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lista", x => x.IdLista);
                });

            migrationBuilder.CreateTable(
                name: "Permiso",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombrePermiso = table.Column<string>(unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permiso", x => x.IdPermiso);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    IdPersona = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombrePersona = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    ApellidoPersona = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    DUI = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    NIT = table.Column<string>(unicode: false, maxLength: 16, nullable: false),
                    ISSS = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    FotoPerfil = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.IdPersona);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreRol = table.Column<string>(unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Autorizacion",
                columns: table => new
                {
                    IdAutorizacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpresaProveedora = table.Column<int>(nullable: false),
                    AutorizacionVenta = table.Column<bool>(nullable: true),
                    AutorizacionInstalacion = table.Column<bool>(nullable: true),
                    AutorizacionMantenimiento = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autorizacion", x => x.IdAutorizacion);
                    table.ForeignKey(
                        name: "FK_Autorizacion_EmpresaProveedora",
                        column: x => x.IdEmpresaProveedora,
                        principalTable: "EmpresaProveedora",
                        principalColumn: "IdEmpresaProveedora",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProducto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpresaProveedora = table.Column<int>(nullable: false),
                    Marca = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    AnioFabricacion = table.Column<int>(nullable: false),
                    CapacidadBTU = table.Column<double>(nullable: false),
                    PrecioUnidad = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_Producto_EmpresaProveedora",
                        column: x => x.IdEmpresaProveedora,
                        principalTable: "EmpresaProveedora",
                        principalColumn: "IdEmpresaProveedora",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactoEmpresa",
                columns: table => new
                {
                    IdContactoEmpresa = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpresaProveedora = table.Column<int>(nullable: false),
                    TipoContacto = table.Column<int>(nullable: false),
                    DescripcionContacto = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactoEmpresa", x => x.IdContactoEmpresa);
                    table.ForeignKey(
                        name: "FK_ContactoEmpresa_EmpresaProveedora",
                        column: x => x.IdEmpresaProveedora,
                        principalTable: "EmpresaProveedora",
                        principalColumn: "IdEmpresaProveedora",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactoEmpresa_Lista",
                        column: x => x.TipoContacto,
                        principalTable: "Lista",
                        principalColumn: "IdLista",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactoPersona",
                columns: table => new
                {
                    IdContactoPersona = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPersona = table.Column<int>(nullable: false),
                    IdTipoContacto = table.Column<int>(nullable: false),
                    DescripcionContacto = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactoPersona", x => x.IdContactoPersona);
                    table.ForeignKey(
                        name: "FK_ContactoPersona_Persona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactoPersona_Lista",
                        column: x => x.IdTipoContacto,
                        principalTable: "Lista",
                        principalColumn: "IdLista",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpresa = table.Column<int>(nullable: false),
                    IdPersona = table.Column<int>(nullable: false),
                    Puesto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleado_EmpresaProveedora",
                        column: x => x.IdEmpresa,
                        principalTable: "EmpresaProveedora",
                        principalColumn: "IdEmpresaProveedora",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleado_Persona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleado_Lista",
                        column: x => x.Puesto,
                        principalTable: "Lista",
                        principalColumn: "IdLista",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Idusuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CorreoElectronico = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Clave = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    IdPersona = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    Intento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Idusuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Persona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolPermiso",
                columns: table => new
                {
                    IdRolPermiso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdRol = table.Column<int>(nullable: false),
                    IdPermiso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermiso", x => x.IdRolPermiso);
                    table.ForeignKey(
                        name: "FK_RolPermiso_Permiso",
                        column: x => x.IdPermiso,
                        principalTable: "Permiso",
                        principalColumn: "IdPermiso",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolPermiso_Rol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Instalacion",
                columns: table => new
                {
                    IdInstalacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProducto = table.Column<int>(nullable: false),
                    IdEmpresa = table.Column<int>(nullable: false),
                    EncargadoInstalacion = table.Column<int>(nullable: false),
                    FechaInicioInstalacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaFinInstalacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Observaciones = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instalacion", x => x.IdInstalacion);
                    table.ForeignKey(
                        name: "FK_Instalacion_Empleado",
                        column: x => x.EncargadoInstalacion,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Instalacion_EmpresaProveedora",
                        column: x => x.IdEmpresa,
                        principalTable: "EmpresaProveedora",
                        principalColumn: "IdEmpresaProveedora",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Instalacion_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimiento",
                columns: table => new
                {
                    IdMantenimiento = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdProducto = table.Column<int>(nullable: false),
                    IdEmpleado = table.Column<int>(nullable: false),
                    FechaInicioMantenimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaFinMantenimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoInicial = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    EstadoFinal = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Observaciones = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimiento", x => x.IdMantenimiento);
                    table.ForeignKey(
                        name: "FK_Mantenimiento_Empleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mantenimiento_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bitacora",
                columns: table => new
                {
                    IdRegistroBitacora = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(nullable: false),
                    Mantenimiento = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Accion = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bitacora", x => x.IdRegistroBitacora);
                    table.ForeignKey(
                        name: "FK_Bitacora_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Idusuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstitucionGubernamental",
                columns: table => new
                {
                    IdInstitucionG = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreInstitucionG = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    EncargadoUACI = table.Column<int>(nullable: false),
                    LogotipoInstitucionG = table.Column<string>(unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitucionGubernamental", x => x.IdInstitucionG);
                    table.ForeignKey(
                        name: "FK_InstitucionGubernamental_Usuario",
                        column: x => x.EncargadoUACI,
                        principalTable: "Usuario",
                        principalColumn: "Idusuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    IdUsuarioRol = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(nullable: false),
                    IdRol = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => x.IdUsuarioRol);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Rol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Idusuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    IdCompra = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdInstitucionG = table.Column<int>(nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime", nullable: false),
                    TipoContratacion = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    EmpresaProveedora = table.Column<int>(nullable: false),
                    TotalCompra = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.IdCompra);
                    table.ForeignKey(
                        name: "FK_Compra_EmpresaProveedora",
                        column: x => x.EmpresaProveedora,
                        principalTable: "EmpresaProveedora",
                        principalColumn: "IdEmpresaProveedora",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compra_InstitucionGubernamental",
                        column: x => x.IdInstitucionG,
                        principalTable: "InstitucionGubernamental",
                        principalColumn: "IdInstitucionG",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactoInstitucion",
                columns: table => new
                {
                    IdContactoInstitucion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdInstitucionG = table.Column<int>(nullable: false),
                    TipoContacto = table.Column<int>(nullable: false),
                    DescripcionContacto = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactoInstitucion", x => x.IdContactoInstitucion);
                    table.ForeignKey(
                        name: "FK_ContactoInstitucion_InstitucionGubernamental",
                        column: x => x.IdInstitucionG,
                        principalTable: "InstitucionGubernamental",
                        principalColumn: "IdInstitucionG",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactoInstitucion_Lista",
                        column: x => x.TipoContacto,
                        principalTable: "Lista",
                        principalColumn: "IdLista",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompra",
                columns: table => new
                {
                    IdDetalleCompra = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCompra = table.Column<int>(nullable: false),
                    IdProducto = table.Column<int>(nullable: false),
                    GarantiaMeses = table.Column<int>(nullable: false),
                    Iva = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompra", x => x.IdDetalleCompra);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Compra",
                        column: x => x.IdCompra,
                        principalTable: "Compra",
                        principalColumn: "IdCompra",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autorizacion_IdEmpresaProveedora",
                table: "Autorizacion",
                column: "IdEmpresaProveedora");

            migrationBuilder.CreateIndex(
                name: "IX_Bitacora_IdUsuario",
                table: "Bitacora",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_EmpresaProveedora",
                table: "Compra",
                column: "EmpresaProveedora");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdInstitucionG",
                table: "Compra",
                column: "IdInstitucionG");

            migrationBuilder.CreateIndex(
                name: "IX_ContactoEmpresa_IdEmpresaProveedora",
                table: "ContactoEmpresa",
                column: "IdEmpresaProveedora");

            migrationBuilder.CreateIndex(
                name: "IX_ContactoEmpresa_TipoContacto",
                table: "ContactoEmpresa",
                column: "TipoContacto");

            migrationBuilder.CreateIndex(
                name: "IX_ContactoInstitucion_IdInstitucionG",
                table: "ContactoInstitucion",
                column: "IdInstitucionG");

            migrationBuilder.CreateIndex(
                name: "IX_ContactoInstitucion_TipoContacto",
                table: "ContactoInstitucion",
                column: "TipoContacto");

            migrationBuilder.CreateIndex(
                name: "IX_ContactoPersona_IdPersona",
                table: "ContactoPersona",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_ContactoPersona_IdTipoContacto",
                table: "ContactoPersona",
                column: "IdTipoContacto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_IdCompra",
                table: "DetalleCompra",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_IdProducto",
                table: "DetalleCompra",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdEmpresa",
                table: "Empleado",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdPersona",
                table: "Empleado",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_Puesto",
                table: "Empleado",
                column: "Puesto");

            migrationBuilder.CreateIndex(
                name: "IX_Instalacion_EncargadoInstalacion",
                table: "Instalacion",
                column: "EncargadoInstalacion");

            migrationBuilder.CreateIndex(
                name: "IX_Instalacion_IdEmpresa",
                table: "Instalacion",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Instalacion_IdProducto",
                table: "Instalacion",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_InstitucionGubernamental_EncargadoUACI",
                table: "InstitucionGubernamental",
                column: "EncargadoUACI");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_IdEmpleado",
                table: "Mantenimiento",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_IdProducto",
                table: "Mantenimiento",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdEmpresaProveedora",
                table: "Producto",
                column: "IdEmpresaProveedora");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_IdPermiso",
                table: "RolPermiso",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_IdRol",
                table: "RolPermiso",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdPersona",
                table: "Usuario",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_IdRol",
                table: "UsuarioRol",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_IdUsuario",
                table: "UsuarioRol",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autorizacion");

            migrationBuilder.DropTable(
                name: "Bitacora");

            migrationBuilder.DropTable(
                name: "ContactoEmpresa");

            migrationBuilder.DropTable(
                name: "ContactoInstitucion");

            migrationBuilder.DropTable(
                name: "ContactoPersona");

            migrationBuilder.DropTable(
                name: "DetalleCompra");

            migrationBuilder.DropTable(
                name: "Instalacion");

            migrationBuilder.DropTable(
                name: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "RolPermiso");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Permiso");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "InstitucionGubernamental");

            migrationBuilder.DropTable(
                name: "Lista");

            migrationBuilder.DropTable(
                name: "EmpresaProveedora");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
