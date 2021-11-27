using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTaskApp_Api.Models;

namespace MyTaskApp_Api.Database
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }


        public DbSet<Tarefa> Tarefas { get; set; }
    }
}