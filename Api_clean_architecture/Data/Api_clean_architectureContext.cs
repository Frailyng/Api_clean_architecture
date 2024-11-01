using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api_clean_architecture.Modelos;

namespace Api_clean_architecture.Data
{
    public class Api_clean_architectureContext : DbContext
    {
        public Api_clean_architectureContext (DbContextOptions<Api_clean_architectureContext> options)
            : base(options)
        {
        }

        public DbSet<Api_clean_architecture.Modelos.Clientes> Clientes { get; set; } = default!;
    }
}
