using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyTaskApp_Api.Database;
using MyTaskApp_Api.Models;
using MyTaskApp_Api.Repository.Interfaces;

namespace MyTaskApp_Api.Repository
{
    
    public class TarefaRepository : ITarefaRepository
    {
        private readonly Context _db;
        public TarefaRepository(Context db)
        {
            _db = db;
        }
        public  async Task<List<Tarefa>> Restauracao(ApplicationUser usuario, DateTime? dataUltimaSincronizacao)
        {
           if(dataUltimaSincronizacao == null )
           {    
              var result = await _db.Tarefas.ToListAsync();
              return result;
           }

          return await _db.Tarefas.Where(c => c.UsuarioId == usuario.Id).ToListAsync();
        }

        public async Task<List<Tarefa>> Sincronizacao(List<Tarefa> tarefas)
        {
            var novasTarefas = tarefas.Where(a => a.IdTarefaApi == 0).ToList();
            var tarefasAtualizada =  tarefas.Where(c => c.IdTarefaApi != 0).ToList();

            if(novasTarefas.Count() > 0 )
            {
                foreach(var tarefa in novasTarefas)
                {
                    _db.Tarefas.Add(tarefa);
                }

                await _db.SaveChangesAsync();
            }

            
            if(tarefasAtualizada.Count() > 0 )
            {
                foreach(var tarefa in tarefasAtualizada)
                {
                    _db.Tarefas.Update(tarefa);
                }

                await _db.SaveChangesAsync();
            }

            return novasTarefas.ToList();

                    
        }
    }
}