using System;
using gestioncomprasAPI.Models.Model.StoredProcedures;
using gestioncomprasAPI.Models.Model.StoredProcedures.EmpresaProveedora;
using gestioncomprasAPI.Models.Model.StoredProcedures.InstitucionGubernamental;
using gestioncomprasAPI.Models.Model.StoredProcedures.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace gestioncomprasAPI.Models
{
    public partial class GestionComprasContext : DbContext
    {
        public GestionComprasContext()
        {
        }

        public GestionComprasContext(DbContextOptions<GestionComprasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autorizacion> Autorizacion { get; set; }
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<ContactoEmpresa> ContactoEmpresa { get; set; }
        public virtual DbSet<ContactoInstitucion> ContactoInstitucion { get; set; }
        public virtual DbSet<ContactoPersona> ContactoPersona { get; set; }
        public virtual DbSet<DetalleCompra> DetalleCompra { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EmpresaProveedora> EmpresaProveedora { get; set; }
        public virtual DbSet<Instalacion> Instalacion { get; set; }
        public virtual DbSet<InstitucionGubernamental> InstitucionGubernamental { get; set; }
        public virtual DbSet<Lista> Lista { get; set; }
        public virtual DbSet<Mantenimiento> Mantenimiento { get; set; }
        public virtual DbSet<Permiso> Permiso { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolPermiso> RolPermiso { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }
        public virtual DbSet<SPSLTBContactoPersonaIdResult> SPSLTBContactoPersonaId { get; set; }
        public virtual DbSet<SPINTBPersonaResult> SPINTBPersona { get; set; }
        public virtual DbSet<SPUPTBPersonaResult> SPUPTBPersona { get; set; }

        // Usuarios
        public virtual DbSet<SPSLTBUsuarioResult> SPSLTBUsuario { get; set; }
        public virtual DbSet<SPSLTBPermisoxRolResult> SPSLTBPermisoxRol { get; set; }
        public virtual DbSet<SPSLTBRolxUsuarioResult> SPSLTBRolxUsuario { get; set; }
        public virtual DbSet<SPINTBUsuarioResult> SPINTBUsuario { get; set; }
        public virtual DbSet<SPUPTBUsuarioInactivarResult> SPUPTBUsuarioInactivar { get; set; }
        public virtual DbSet<SPUPTBUsuarioActivarResult> SPUPTBUsuarioActivar { get; set; }


        // Empresa Proveedora
        public virtual DbSet<SPSLTBEmpresaProveedoraResult> SPSLTBEmpresaProveedora { get; set; }
        public virtual DbSet<SPSLTBEmpresaProveedoraIdResult> SPSLTBEmpresaProveedoraId { get; set; }
        public virtual DbSet<SPINTBEmpresaProveedoraResult> SPINTBEmpresaProveedora { get; set; }
        public virtual DbSet<SPSLTBContactoEmpresaIdResult> SPSLTBContactoEmpresaId { get; set; }
        public virtual DbSet<SPUPTBEmpresaProveedoraResult> SPUPTBEmpresaProveedora { get; set; }
        public virtual DbSet<SPUPTBAutorizacionEmpresaResult> SPUPTBAutorizacionEmpresa { get; set; }

        // Institución Gubernamental
        public virtual DbSet<SPSLTBInstitucionGubernamentalResult> SPSLTBInstitucionGubernamental { get; set; }
        public virtual DbSet<SPSLTBInstitucionGubernamentalIdResult> SPSLTBInstitucionGubernamentalId { get; set; }
        public virtual DbSet<SPINTBInstitucionGubernamentalResult> SPINTBInstitucionGubernamental { get; set; }
        public virtual DbSet<SPUPTBInstitucionGubernamentalResult> SPUPTBInstitucionGubernamental { get; set; }
        public virtual DbSet<SPSLTBContactoInstitucionIdResult> SPSLTBContactoInstitucionId { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("GestionComprasDatabase");                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autorizacion>(entity =>
            {
                entity.HasKey(e => e.IdAutorizacion);

                entity.HasOne(d => d.IdEmpresaProveedoraNavigation)
                    .WithMany(p => p.Autorizacion)
                    .HasForeignKey(d => d.IdEmpresaProveedora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autorizacion_EmpresaProveedora");
            });

            modelBuilder.Entity<Bitacora>(entity =>
            {
                entity.HasKey(e => e.IdRegistroBitacora);

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FechaHora).HasColumnType("datetime");

                entity.Property(e => e.Mantenimiento)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Bitacora)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bitacora_Usuario");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra);

                entity.Property(e => e.FechaCompra).HasColumnType("datetime");

                entity.Property(e => e.TipoContratacion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalCompra).HasColumnType("money");

                entity.HasOne(d => d.EmpresaProveedoraNavigation)
                    .WithMany(p => p.Compra)
                    .HasForeignKey(d => d.EmpresaProveedora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compra_EmpresaProveedora");

                entity.HasOne(d => d.IdInstitucionGNavigation)
                    .WithMany(p => p.Compra)
                    .HasForeignKey(d => d.IdInstitucionG)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compra_InstitucionGubernamental");
            });

            modelBuilder.Entity<ContactoEmpresa>(entity =>
            {
                entity.HasKey(e => e.IdContactoEmpresa);

                entity.Property(e => e.DescripcionContacto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaProveedoraNavigation)
                    .WithMany(p => p.ContactoEmpresa)
                    .HasForeignKey(d => d.IdEmpresaProveedora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactoEmpresa_EmpresaProveedora");

                entity.HasOne(d => d.TipoContactoNavigation)
                    .WithMany(p => p.ContactoEmpresa)
                    .HasForeignKey(d => d.TipoContacto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactoEmpresa_Lista");
            });

            modelBuilder.Entity<ContactoInstitucion>(entity =>
            {
                entity.HasKey(e => e.IdContactoInstitucion);

                entity.Property(e => e.DescripcionContacto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdInstitucionGNavigation)
                    .WithMany(p => p.ContactoInstitucion)
                    .HasForeignKey(d => d.IdInstitucionG)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactoInstitucion_InstitucionGubernamental");

                entity.HasOne(d => d.TipoContactoNavigation)
                    .WithMany(p => p.ContactoInstitucion)
                    .HasForeignKey(d => d.TipoContacto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactoInstitucion_Lista");
            });

            modelBuilder.Entity<ContactoPersona>(entity =>
            {
                entity.HasKey(e => e.IdContactoPersona);

                entity.Property(e => e.DescripcionContacto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.ContactoPersona)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactoPersona_Persona");

                entity.HasOne(d => d.IdTipoContactoNavigation)
                    .WithMany(p => p.ContactoPersona)
                    .HasForeignKey(d => d.IdTipoContacto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactoPersona_Lista");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(e => e.IdDetalleCompra);

                entity.Property(e => e.Iva).HasColumnType("money");

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany(p => p.DetalleCompra)
                    .HasForeignKey(d => d.IdCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleCompra_Compra");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleCompra)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleCompra_Producto");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleado_EmpresaProveedora");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleado_Persona");

                entity.HasOne(d => d.PuestoNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.Puesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleado_Lista");
            });

            modelBuilder.Entity<EmpresaProveedora>(entity =>
            {
                entity.HasKey(e => e.IdEmpresaProveedora);

                entity.Property(e => e.DireccionEmpresa)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LogotipoEmpresa)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MontoPermitido).HasColumnType("money");

                entity.Property(e => e.Nitempresa)
                    .IsRequired()
                    .HasColumnName("NITEmpresa")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Instalacion>(entity =>
            {
                entity.HasKey(e => e.IdInstalacion);

                entity.Property(e => e.FechaFinInstalacion).HasColumnType("datetime");

                entity.Property(e => e.FechaInicioInstalacion).HasColumnType("datetime");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.EncargadoInstalacionNavigation)
                    .WithMany(p => p.Instalacion)
                    .HasForeignKey(d => d.EncargadoInstalacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Instalacion_Empleado");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Instalacion)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Instalacion_EmpresaProveedora");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Instalacion)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Instalacion_Producto");
            });

            modelBuilder.Entity<InstitucionGubernamental>(entity =>
            {
                entity.HasKey(e => e.IdInstitucionG);

                entity.Property(e => e.EncargadoUaci).HasColumnName("EncargadoUACI");

                entity.Property(e => e.LogotipoInstitucionG)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombreInstitucionG)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.EncargadoUaciNavigation)
                    .WithMany(p => p.InstitucionGubernamental)
                    .HasForeignKey(d => d.EncargadoUaci)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstitucionGubernamental_Usuario");
            });

            modelBuilder.Entity<Lista>(entity =>
            {
                entity.HasKey(e => e.IdLista);

                entity.Property(e => e.NombreLista)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoLista)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mantenimiento>(entity =>
            {
                entity.HasKey(e => e.IdMantenimiento);

                entity.Property(e => e.EstadoFinal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoInicial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaFinMantenimiento).HasColumnType("datetime");

                entity.Property(e => e.FechaInicioMantenimiento).HasColumnType("datetime");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Mantenimiento)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mantenimiento_Empleado");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Mantenimiento)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mantenimiento_Producto");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso);

                entity.Property(e => e.NombrePermiso)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.Property(e => e.ApellidoPersona)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Dui)
                    .IsRequired()
                    .HasColumnName("DUI")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FotoPerfil)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Isss)
                    .IsRequired()
                    .HasColumnName("ISSS")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nit)
                    .IsRequired()
                    .HasColumnName("NIT")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.NombrePersona)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.Property(e => e.CapacidadBtu).HasColumnName("CapacidadBTU");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnidad).HasColumnType("money");

                entity.HasOne(d => d.IdEmpresaProveedoraNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdEmpresaProveedora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Producto_EmpresaProveedora");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.Property(e => e.NombreRol)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolPermiso>(entity =>
            {
                entity.HasKey(e => e.IdRolPermiso);

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.RolPermiso)
                    .HasForeignKey(d => d.IdPermiso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolPermiso_Permiso");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolPermiso)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolPermiso_Rol");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario);

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoElectronico)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Persona");
            });

            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioRol);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioRol_Rol");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioRol)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioRol_Usuario");
            });
        }
    }
}
