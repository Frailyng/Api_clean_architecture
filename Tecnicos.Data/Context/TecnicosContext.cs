
using Tecnicos.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_clean_architecture.Context;

public class TecnicosContext : DbContext
{
    public TecnicosContext(DbContextOptions<TecnicosContext> options) : base(options) { }
        public DbSet<Clientes> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Clientes>(tb =>
        {
            tb.HasKey(col => col.CompraId);
            tb.Property(col => col.CompraId).UseIdentityColumn().ValueGeneratedOnAdd();
            tb.Property(col => col.Descripcion).HasMaxLength(50);
            tb.ToTable("Clientes");
            tb.HasData(
                new Clientes { CompraId = 1, Descripcion = "Router", Monto = 250 },
                new Clientes { CompraId = 2, Descripcion = "Switch", Monto = 150 },
                new Clientes { CompraId = 3, Descripcion = "Hub", Monto = 100 }
                );

        });
    }


}
