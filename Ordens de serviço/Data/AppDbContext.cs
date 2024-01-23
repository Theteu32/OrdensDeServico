using Microsoft.EntityFrameworkCore;
using Ordens_de_serviço.Ordens;

namespace Ordens_de_serviço.Data
{
    public class AppDbContext : DbContext
    {
       public DbSet<Ordem> Ordens {  get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite("Data Source=Banco.sqlite");
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
