using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoBAD.Models
{
    public partial class sisencuestasContext : DbContext
    {
        public sisencuestasContext()
        {
        }

        public sisencuestasContext(DbContextOptions<sisencuestasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Encuestado> Encuestados { get; set; } = null!;
        public virtual DbSet<Encuestum> Encuesta { get; set; } = null!;
        public virtual DbSet<Opcionpreguntum> Opcionpregunta { get; set; } = null!;
        public virtual DbSet<Preguntum> Pregunta { get; set; } = null!;
        public virtual DbSet<Respuestum> Respuesta { get; set; } = null!;
        public virtual DbSet<Tipopreguntum> Tipopregunta { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=bad-encuestas.database.windows.net; Database=sis-encuestas; User Id=lf18010; Password=Nueva2023!.;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Encuestado>(entity =>
            {
                entity.HasKey(e => e.IdEncuestado)
                    .IsClustered(false);

                entity.ToTable("ENCUESTADO");

                entity.Property(e => e.IdEncuestado)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_ENCUESTADO");

                entity.Property(e => e.EmailEncuestado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL_ENCUESTADO");

                entity.Property(e => e.FechaNacEncuesta)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_NAC_ENCUESTA");

                entity.Property(e => e.GenEncuestado)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("GEN_ENCUESTADO")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Encuestum>(entity =>
            {
                entity.HasKey(e => e.IdEncuesta)
                    .IsClustered(false);

                entity.ToTable("ENCUESTA");

                entity.Property(e => e.IdEncuesta)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_ENCUESTA");

                entity.Property(e => e.EstadoEncuesta).HasColumnName("ESTADO_ENCUESTA");

                entity.Property(e => e.FechaEncuesta)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("FECHA_ENCUESTA");

                entity.Property(e => e.GrupometaEncuesta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("GRUPOMETA_ENCUESTA");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.IndicacionesEncuesta)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("INDICACIONES_ENCUESTA");

                entity.Property(e => e.ObjetivoEncuesta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("OBJETIVO_ENCUESTA");

                entity.Property(e => e.TituloEncuesta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TITULO_ENCUESTA");
            });

            modelBuilder.Entity<Opcionpreguntum>(entity =>
            {
                entity.HasKey(e => e.OpcionId)
                    .IsClustered(false);

                entity.ToTable("OPCIONPREGUNTA");

                entity.Property(e => e.OpcionId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OPCION_ID");

                entity.Property(e => e.DescripcionOpcion)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_OPCION");

                entity.Property(e => e.EstadoOpcion).HasColumnName("ESTADO_OPCION");

                entity.Property(e => e.IdPregunta).HasColumnName("ID_PREGUNTA");

                entity.Property(e => e.OrdenOpcion).HasColumnName("ORDEN_OPCION");

                entity.Property(e => e.ValorOpcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("VALOR_OPCION");
            });

            modelBuilder.Entity<Preguntum>(entity =>
            {
                entity.HasKey(e => e.IdPregunta)
                    .IsClustered(false);

                entity.ToTable("PREGUNTA");

                entity.Property(e => e.IdPregunta)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_PREGUNTA");

                entity.Property(e => e.DescripcionPregunta)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_PREGUNTA");

                entity.Property(e => e.IdEncuesta).HasColumnName("ID_ENCUESTA");

                entity.Property(e => e.OrdenPregunta).HasColumnName("ORDEN_PREGUNTA");

                entity.Property(e => e.RequeridaPregunta).HasColumnName("REQUERIDA_PREGUNTA");

                entity.Property(e => e.TipoPreguntaId).HasColumnName("TIPO_PREGUNTA_ID");
            });

            modelBuilder.Entity<Respuestum>(entity =>
            {
                entity.HasKey(e => e.RespuestaId)
                    .IsClustered(false);

                entity.ToTable("RESPUESTA");

                entity.Property(e => e.RespuestaId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RESPUESTA_ID");

                entity.Property(e => e.FechaRespuesta)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("FECHA_RESPUESTA");

                entity.Property(e => e.IdEncuesta).HasColumnName("ID_ENCUESTA");

                entity.Property(e => e.IdEncuestado).HasColumnName("ID_ENCUESTADO");

                entity.Property(e => e.IdPregunta).HasColumnName("ID_PREGUNTA");

                entity.Property(e => e.OpcionId).HasColumnName("OPCION_ID");

                entity.Property(e => e.TextoRespuesta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXTO_RESPUESTA");
            });

            modelBuilder.Entity<Tipopreguntum>(entity =>
            {
                entity.HasKey(e => e.TipoPreguntaId)
                    .IsClustered(false);

                entity.ToTable("TIPOPREGUNTA");

                entity.Property(e => e.TipoPreguntaId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TIPO_PREGUNTA_ID");

                entity.Property(e => e.DescripcionTipoPregunta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_TIPO_PREGUNTA");

                entity.Property(e => e.NombreTipoPregunta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_TIPO_PREGUNTA");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .IsClustered(false);

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_USUARIO");

                entity.Property(e => e.EmailUsuario)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL_USUARIO");

                entity.Property(e => e.GenUsuario)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("GEN_USUARIO")
                    .IsFixedLength();

                entity.Property(e => e.PrimerApellidoUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRIMER_APELLIDO_USUARIO");

                entity.Property(e => e.PrimerNombreUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRIMER_NOMBRE_USUARIO");

                entity.Property(e => e.SegundoApellidoUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SEGUNDO_APELLIDO_USUARIO");

                entity.Property(e => e.SegundoNombreUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SEGUNDO_NOMBRE_USUARIO");

                entity.Property(e => e.TelefonoUsuario)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO_USUARIO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
