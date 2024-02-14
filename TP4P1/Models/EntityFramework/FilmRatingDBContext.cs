using Microsoft.EntityFrameworkCore;

namespace TP4P1.Models.EntityFramework
{
    public partial class FilmRatingDBContext:DbContext
    {
        public FilmRatingDBContext()
        {
        }

        public FilmRatingDBContext(DbContextOptions<FilmRatingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Notation> Notations { get; set; } = null!;
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=TP4; uid=postgres; \npassword=postgres;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notation>()
                           .HasKey(n => new { n.UtilisateurId, n.FilmId});
            modelBuilder.Entity<Utilisateur>()
       .HasIndex(u => u.Mail)
       .IsUnique().HasName("uq_utl_mail");
            modelBuilder.Entity<Notation>(entity => entity.HasCheckConstraint("ck_not_note", "not_note between 0 and 5"));


            modelBuilder.Entity<Film>().HasKey(d => d.FilmId).HasName("pk_film");




            modelBuilder.Entity<Notation>(entity => entity.HasOne(d => d.FilmNote).WithMany(p => p.NotesFilm)
            .HasForeignKey(d => d.FilmId).OnDelete(DeleteBehavior.Restrict).HasConstraintName("fk_not_flm"));

            modelBuilder.Entity<Notation>(entity => entity.HasOne(d => d.UtilisateurNotant).WithMany(p => p.NotesUtilisateur).HasForeignKey(s => s.UtilisateurId)
            .OnDelete(DeleteBehavior.Restrict).HasConstraintName("fk_not_utl"));

            modelBuilder.Entity<Utilisateur>(entity => entity.Property(i => i.Pays).HasDefaultValue("France"));
            modelBuilder.Entity<Utilisateur>(entity => entity.Property(i => i.DateCreation).HasDefaultValueSql("now()"));




        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

