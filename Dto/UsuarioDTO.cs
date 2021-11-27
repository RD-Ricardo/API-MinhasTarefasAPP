using System.ComponentModel.DataAnnotations;

namespace MyTaskApp_Api.Dto
{
    public class UsuarioDTO
    {

        [Required]
        public string  Name { get; set; }
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        [Required]
        public string  Senha { get; set; }
        [Required]
        [Compare("Senha")]
        public string ConfirmacaoSenha { get; set; }
    }
}