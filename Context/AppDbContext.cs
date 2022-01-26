using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountRepeatedWords.Context
{
    public class AppDbContext : IdentityDbContext 
    {
        //IdentityDbContext - Contien
        //Contructor para pasar configuraciones -Ejm - Conexión a base de datos
        //Podemos cofigurar las tablas atraves de los comandosp
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
