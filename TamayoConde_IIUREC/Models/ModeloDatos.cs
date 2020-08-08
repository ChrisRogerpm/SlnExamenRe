namespace TamayoConde_IIUREC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModeloDatos : DbContext
    {
        public ModeloDatos()
            : base("name=ModeloDatos")
        {
        }

        public virtual DbSet<Aviso> Aviso { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aviso>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Aviso>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Aviso>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Aviso>()
                .Property(e => e.imagen)
                .IsUnicode(false);

            modelBuilder.Entity<Aviso>()
                .Property(e => e.thumbail)
                .IsUnicode(false);

            modelBuilder.Entity<Aviso>()
                .Property(e => e.urlvideo)
                .IsUnicode(false);

            modelBuilder.Entity<Aviso>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            //modelBuilder.Entity<Categoria>()
            //    .HasMany(e => e.Aviso)
            //    .WithRequired(e => e.Categoria)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nombreusuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.clave)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.nivel)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.avatar)
                .IsUnicode(false);
        }
    }
}
