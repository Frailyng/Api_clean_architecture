
using Api_clean_architecture.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Api_clean_architecture.Context;

public class TecnicosContext : DbContext
{
    public TecnicosContext(DbContextOptions<TecnicosContext> options) : base(options) { }
        public DbSet<Clientes> Clientes { get; set; }
}
