
using Tecnicos.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_clean_architecture.Context;

public class TecnicosContext : DbContext
{
    public TecnicosContext(DbContextOptions<TecnicosContext> options) : base(options) { }
        public DbSet<Compras> Compras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Compras>(tb =>
        {
            tb.HasKey(col => col.CompraId);
            tb.Property(col => col.CompraId).UseIdentityColumn().ValueGeneratedOnAdd();
            tb.Property(col => col.Descripcion).HasMaxLength(50);
            tb.ToTable("Compras");
            tb.HasData(
                new Compras { CompraId = 1, Descripcion = "Router", Monto = 250 },
                new Compras { CompraId = 2, Descripcion = "Switch", Monto = 150 },
                new Compras { CompraId = 3, Descripcion = "Hub", Monto = 100 }
                );

        });
    }


}
