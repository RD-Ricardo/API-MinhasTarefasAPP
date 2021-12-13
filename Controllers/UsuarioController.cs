using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTaskApp_Api.Dto;
using MyTaskApp_Api.Models;
using MyTaskApp_Api.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace MyTaskApp_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {   
        private readonly IUsuarioRepository _repository;
        private readonly ITokenRepository _tokenRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsuarioController(IUsuarioRepository repository, SignInManager<ApplicationUser> signInManager,  UserManager<ApplicationUser> userManager,  ITokenRepository tokenRepository)
        {
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO usuario)
        {
            
            ModelState.Remove("Name");
            ModelState.Remove("ConfirmacaoSenha");

            if(ModelState.IsValid)
            {

                ApplicationUser usuarioLogin = await _repository.Login(usuario.Email, usuario.Senha);

                if(usuarioLogin != null)
                {

                    //Identity Cookie 

                    //JWT (String, Intecridade)
                   //await _signInManager.SignInAsync(usuarioLogin, false);
                    var token = BuildToken(usuarioLogin);
                    var tokenModel = new Token()
                    {
                        RefreshToken = token.RefreshToken,
                        ExpirationRefreshToken = token.ExpirationRefreshtoken,
                        ExpirationToken = token.Expiration,
                        Usuario = usuarioLogin,
                        DataCriado = DateTime.Now,
                        Utilizado = false
                    };


                    await _tokenRepository.Cadastrar(tokenModel);
                    return Ok(token);
                }
                return NotFound("Usuario não localizado");
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Renovar([FromBody] TokenDTO tokenDTO)
        {
           var refreshTokenDB = await  _tokenRepository.Obter(tokenDTO.RefreshToken);
           if(refreshTokenDB == null)
           {
               return BadRequest();
            }

            refreshTokenDB.DataAtualizado = DateTime.Now;
            refreshTokenDB.Utilizado = true;

            await _tokenRepository.Atualizar(refreshTokenDB);


             //Gerar um novo Token 

            var usuario = await _repository.Obter(refreshTokenDB.UsuarioId);

              var token = BuildToken(usuario);
                    var tokenModel = new Token()
                    {
                        RefreshToken = token.RefreshToken,
                        ExpirationRefreshToken = token.ExpirationRefreshtoken,
                        ExpirationToken = token.Expiration,
                        Usuario = usuario,
                        DataCriado = DateTime.Now,
                        Utilizado = false
                    };

                await _tokenRepository.Cadastrar(tokenModel);
                return Ok(token);
           
            //Refresh Token
            //Salvar no Banco de Dados 
        }
        public TokenDTO BuildToken(ApplicationUser usuario)
        {
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes ("cahve-api-jwt-minhastarefas"));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer:null,
                audience: null,
                claims:claims,
                expires: exp,
                signingCredentials: sign
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = Guid.NewGuid().ToString().Replace("-","");

            var expRefreshToken = DateTime.UtcNow.AddHours(2);

            var TokenDto = new TokenDTO { Token = tokenString , Expiration = exp, RefreshToken = refreshToken, ExpirationRefreshtoken = expRefreshToken};

            return TokenDto;
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