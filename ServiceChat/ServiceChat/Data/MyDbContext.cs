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
        public DbSet<Chat_Grupos> Sala { get; set; }
        public DbSet<Chat_Membros> Grupo { get; set; }
        public DbSet<Chat_Mensagens> Mensagem { get; set; }
    }
}
