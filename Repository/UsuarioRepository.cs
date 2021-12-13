using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyTaskApp_Api.Models;
using MyTaskApp_Api.Repository.Interfaces;

namespace MyTaskApp_Api.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Cadastrar(ApplicationUser usuario, string senha)
        {
            var result =  await _userManager.CreateAsync(usuario, senha);

            if(!result.Succeeded)
            {

                StringBuilder sb = new StringBuilder();

                foreach(var erro in result.Errors)
                {
                    sb.Append(erro.Description);
                }
                throw new System.Exception("Usuario não cadastrado" + sb.ToString() );
            }
        }

        public async Task<ApplicationUser> Login(string email, string senha)
        {
            var usuario  = await _userManager.FindByEmailAsync(email);
            if(await _userManager.CheckPasswordAsync(usuario, senha))
            {
                return usuario;
            }
            else
            {
                return null;
            }

        }

        public async Task<ApplicationUser> Obter(string id)
        {
          var usuario = await   _userManager.FindByIdAsync(id);
          return usuario;
        }
    }
}