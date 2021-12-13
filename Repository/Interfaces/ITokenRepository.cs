using System.Threading.Tasks;
using MyTaskApp_Api.Models;

namespace MyTaskApp_Api.Repository.Interfaces
{
    public interface ITokenRepository
    {
        Task Cadastrar(Token token);
        Task<Token> Obter(string refreshToken);
        Task Atualizar(Token token);
    }
}