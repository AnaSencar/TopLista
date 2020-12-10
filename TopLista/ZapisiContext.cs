using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopLista
{
    public class ZapisiContext : DbContext
    {
        public ZapisiContext(DbContextOptions<ZapisiContext> options) : base(options)
        {

        }

        public DbSet<Zapis> Zapisi { get; set; }
    }
}
