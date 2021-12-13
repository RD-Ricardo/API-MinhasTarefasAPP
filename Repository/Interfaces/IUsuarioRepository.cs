using System.Threading.Tasks;
using MyTaskApp_Api.Models;

namespace MyTaskApp_Api.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task Cadastrar(ApplicationUser usuario,  string senha);
        Task<ApplicationUser> Login(string email, string senha);
        Task<ApplicationUser> Obter(string id);

    }
}