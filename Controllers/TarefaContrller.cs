using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTaskApp_Api.Models;
using MyTaskApp_Api.Repository.Interfaces;

namespace MyTaskApp_Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TarefaContrller :Controller
    {
        private readonly ITarefaRepository _repository;
        private readonly UserManager<ApplicationUser> _usermanager;
        public TarefaContrller(ITarefaRepository repository,
        UserManager<ApplicationUser> usermanager)
        {
            _repository = repository;
            _usermanager = usermanager;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Restautar([FromQuery] DateTime datetime)
        {
            var usuario =  await _usermanager.GetUserAsync(HttpContext.User);

            return Ok( await _repository.Restauracao(usuario,datetime));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Sincronizar ([FromBody] List<Tarefa> tarefas)
        {
            return Ok( await _repository.Sincronizacao(tarefas));
        }

    }
}