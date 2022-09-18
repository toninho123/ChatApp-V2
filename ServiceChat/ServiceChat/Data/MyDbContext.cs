using Microsoft.EntityFrameworkCore;
using ServiceChat.Model;

namespace ServiceChat.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }
    }
}
