using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyTaskApp_Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        
        [ForeignKey("UsuarioId")]
        public ICollection<Tarefa> Tarefas { get; set; }

        [ForeignKey("UsuarioId")]
        public ICollection<Token> Tokens { get; set; }
    }
}