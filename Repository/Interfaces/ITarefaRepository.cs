using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyTaskApp_Api.Models;

namespace MyTaskApp_Api.Repository.Interfaces
{
    public interface ITarefaRepository
    {
        Task<List<Tarefa>> Sincronizacao(List<Tarefa> tarefas);
        Task<List<Tarefa>> Restauracao(ApplicationUser  usuario, DateTime? dataUltimaSincronizacao);
    }
}