using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyTaskApp_Api.Database;
using MyTaskApp_Api.Models;
using MyTaskApp_Api.Repository.Interfaces;

namespace MyTaskApp_Api.Repository
{
    public class TokeRepository : ITokenRepository
    {   
        private readonly Context _db;
        public TokeRepository(Context contex)
        {
            _db = contex;
        }
        public async Task<Token> Obter(string refreshToken)
        {
            return await _db.Token.FirstOrDefaultAsync(c => c.RefreshToken == refreshToken && c.Utilizado == false);
        }
        public async Task Atualizar(Token token)
        {
            _db.Token.Update(token);
            await _db.SaveChangesAsync();
        }

        public async Task Cadastrar(Token token)
        {
            _db.Token.Add(token);
            await _db.SaveChangesAsync();
        }

       
    }
}