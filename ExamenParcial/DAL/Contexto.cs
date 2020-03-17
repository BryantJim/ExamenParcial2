using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ExamenParcial.Entidades;

namespace ExamenParcial.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Llamadas> Llamadas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source=Llamadas.db");
        }
    }
}
