using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTaskApp_Api.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        [ForeignKey("Usuario")]
        public string  UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
        public bool Utilizado { get; set; }
        public DateTime ExpirationToken { get; set; }
        public DateTime ExpirationRefreshToken{ get; set; }
        public DateTime DataCriado { get; set; }
        public DateTime? DataAtualizado { get; set; }
    }
}