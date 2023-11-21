using Entidades.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Configuracoes
{
    public class Contexto: IdentityDbContext<ApplicationUser>
    {
        public Contexto(DbContextOptions<Contexto> opcoes):base(opcoes)
        {

        }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("Users").HasKey(t => t.Id);
            builder.Entity<Solicitacao>().ToTable("Solicitacoes").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }

        private string ObterStringConexao()
        {
            string strcon = @"Data Source=profilecaio.database.windows.net;Initial Catalog=ProfileDb;User Id=caio;Password=zxcasd384!A";
            return strcon;
        }
    }
}
