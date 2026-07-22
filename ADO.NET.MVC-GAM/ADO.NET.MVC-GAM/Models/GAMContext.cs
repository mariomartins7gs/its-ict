using Microsoft.EntityFrameworkCore;

namespace ADO.NET.MVC_GAM.Models;

public partial class GAMContext : DbContext
{
    public GAMContext()
    {
    }

    public GAMContext(DbContextOptions<GAMContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autori { get; set; }

    public virtual DbSet<Materiale> Materiali { get; set; }

    public virtual DbSet<Opera> Opere { get; set; }

    public virtual DbSet<OperaMateriale> OperaMateriali { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.ToTable("Autore");
            entity.Property(e => e.Nominativo).HasColumnType("nvarchar(max)");
        });

        modelBuilder.Entity<Materiale>(entity =>
        {
            entity.ToTable("Materiale");
            entity.Property(e => e.Denominazione).HasColumnType("nvarchar(max)");
        });

        modelBuilder.Entity<Opera>(entity =>
        {
            entity.ToTable("Opera");
            entity.Property(e => e.Ambito_culturale).HasColumnType("nvarchar(max)");
            entity.Property(e => e.Datazione).HasColumnType("nvarchar(max)");
            entity.Property(e => e.Immagine).HasColumnType("nvarchar(max)");
            entity.Property(e => e.Inventario).HasColumnType("nvarchar(max)");
            entity.Property(e => e.lsreferenceby).HasColumnType("nvarchar(max)");
            entity.Property(e => e.Titolo_soggetto).HasColumnType("nvarchar(max)");

            entity.HasOne(d => d.IdAutoreNavigation).WithMany(p => p.Opere)
                .HasForeignKey(d => d.IdAutore)
                .HasConstraintName("FK_Opera_Autore");
        });

        modelBuilder.Entity<OperaMateriale>(entity =>
        {
            entity.HasKey(e => new { e.IdOpera, e.IdMateriale });
            entity.ToTable("OperaMateriale");

            entity.HasOne(d => d.IdMaterialeNavigation).WithMany(p => p.OperaMateriali)
                .HasForeignKey(d => d.IdMateriale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperaMateriale_Materiale");

            entity.HasOne(d => d.IdOperaNavigation).WithMany(p => p.OperaMateriali)
                .HasForeignKey(d => d.IdOpera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperaMateriale_Opera");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
