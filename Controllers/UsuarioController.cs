using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTaskApp_Api.Dto;
using MyTaskApp_Api.Models;
using MyTaskApp_Api.Repository.Interfaces;

namespace MyTaskApp_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {   
        private readonly IUsuarioRepository _repository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsuarioController(IUsuarioRepository repository, SignInManager<ApplicationUser> signInManager,  UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO usuario)
        {
            
            ModelState.Remove("Name");
            ModelState.Remove("ConfirmacaoSenha");

            if(ModelState.IsValid)
            {

                var usuarioLogin = await _repository.Login(usuario.Email, usuario.Senha);

                if(usuarioLogin != null)
                {
                   await _signInManager.SignInAsync(usuarioLogin, false);

                   return Ok();
                }
                return NotFound("Usuario não localizado");
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        [HttpPost("/Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO usuarioDTO)
        {
            if(ModelState.IsValid)
            {
                var usuario = new ApplicationUser()
                {
                    UserName = usuarioDTO.Email,
                    Email = usuarioDTO.Email,
                    FullName = usuarioDTO.Name
                };

                var result =  await _userManager.CreateAsync(usuario, usuarioDTO.Senha);

                if(!result.Succeeded)
                {
                    List<string> sb = new List<string>();

                    foreach(var erro in result.Errors)
                    {
                        sb.Add(erro.Description);
                    }
                    return UnprocessableEntity(sb);
                }

                return Ok(usuario);
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

    }
}