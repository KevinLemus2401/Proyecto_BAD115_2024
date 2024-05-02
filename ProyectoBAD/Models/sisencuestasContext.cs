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
                entity.HasKey(e => e.IdEncuestado);

                entity.ToTable("ENCUESTADO");

                entity.Property(e => e.IdEncuestado)
                    .ValueGeneratedNever()
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
                entity.HasKey(e => e.IdEncuesta);

                entity.ToTable("ENCUESTA");

                entity.HasIndex(e => e.IdUsuario, "USUARIO_ENCUESTA_FK");

                entity.Property(e => e.IdEncuesta)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_ENCUESTA");

                entity.Property(e => e.EstadoEncuesta).HasColumnName("ESTADO_ENCUESTA");

                entity.Property(e => e.FechaEncuesta)
                    .HasColumnType("datetime")
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

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Encuesta)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_ENCUESTA_USUARIO_E_USUARIO");
            });

            modelBuilder.Entity<Opcionpreguntum>(entity =>
            {
                entity.HasKey(e => e.OpcionId);

                entity.ToTable("OPCIONPREGUNTA");

                entity.HasIndex(e => e.IdPregunta, "REL_PREGUNTA_OPCIONPREGUNTA_FK");

                entity.Property(e => e.OpcionId)
                    .ValueGeneratedNever()
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

                entity.HasOne(d => d.IdPreguntaNavigation)
                    .WithMany(p => p.Opcionpregunta)
                    .HasForeignKey(d => d.IdPregunta)
                    .HasConstraintName("FK_OPCIONPR_REL_PREGU_PREGUNTA");
            });

            modelBuilder.Entity<Preguntum>(entity =>
            {
                entity.HasKey(e => e.IdPregunta);

                entity.ToTable("PREGUNTA");

                entity.HasIndex(e => e.IdEncuesta, "REL_ENCUESTA_PREGUNTA_FK");

                entity.HasIndex(e => e.TipoPreguntaId, "REL_TIPOPREGUNTA_PREGUNTA_FK");

                entity.Property(e => e.IdPregunta)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_PREGUNTA");

                entity.Property(e => e.DescripcionPregunta)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_PREGUNTA");

                entity.Property(e => e.IdEncuesta).HasColumnName("ID_ENCUESTA");

                entity.Property(e => e.OrdenPregunta).HasColumnName("ORDEN_PREGUNTA");

                entity.Property(e => e.RequeridaPregunta).HasColumnName("REQUERIDA_PREGUNTA");

                entity.Property(e => e.TipoPreguntaId).HasColumnName("TIPO_PREGUNTA_ID");

                entity.HasOne(d => d.IdEncuestaNavigation)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.IdEncuesta)
                    .HasConstraintName("FK_PREGUNTA_REL_ENCUE_ENCUESTA");

                entity.HasOne(d => d.TipoPregunta)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.TipoPreguntaId)
                    .HasConstraintName("FK_PREGUNTA_REL_TIPOP_TIPOPREG");
            });

            modelBuilder.Entity<Respuestum>(entity =>
            {
                entity.HasKey(e => e.RespuestaId);

                entity.ToTable("RESPUESTA");

                entity.HasIndex(e => e.IdEncuesta, "RELATIONSHIP_7_FK");

                entity.HasIndex(e => e.IdEncuestado, "REL_ENCUESTADO_RESPUESTA_FK");

                entity.HasIndex(e => e.OpcionId, "REL_RESPUESTA_OPCIONPREGUNTA_FK");

                entity.HasIndex(e => e.IdPregunta, "REL_RESPUESTA_PREGUNTA_FK");

                entity.Property(e => e.RespuestaId)
                    .ValueGeneratedNever()
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

                entity.HasOne(d => d.IdEncuestaNavigation)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => d.IdEncuesta)
                    .HasConstraintName("FK_RESPUEST_RELATIONS_ENCUESTA");

                entity.HasOne(d => d.IdEncuestadoNavigation)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => d.IdEncuestado)
                    .HasConstraintName("FK_RESPUEST_REL_ENCUE_ENCUESTA");

                entity.HasOne(d => d.IdPreguntaNavigation)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => d.IdPregunta)
                    .HasConstraintName("FK_RESPUEST_REL_RESPU_PREGUNTA");

                entity.HasOne(d => d.Opcion)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => d.OpcionId)
                    .HasConstraintName("FK_RESPUEST_REL_RESPU_OPCIONPR");
            });

            modelBuilder.Entity<Tipopreguntum>(entity =>
            {
                entity.HasKey(e => e.TipoPreguntaId);

                entity.ToTable("TIPOPREGUNTA");

                entity.Property(e => e.TipoPreguntaId)
                    .ValueGeneratedNever()
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
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario)
                    .ValueGeneratedNever()
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
