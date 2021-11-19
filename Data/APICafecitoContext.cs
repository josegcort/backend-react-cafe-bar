using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APICafecito.Models;

namespace APICafecito.Data
{
    public class APICafecitoContext : DbContext
    {
        public APICafecitoContext (DbContextOptions<APICafecitoContext> options)
            : base(options)
        {
        }

        public DbSet<APICafecito.Models.Reserva> Reserva { get; set; }
    }
}
