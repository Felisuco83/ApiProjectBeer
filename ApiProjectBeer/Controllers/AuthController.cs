using ApiProjectBeer.Models;
using ApiProjectBeer.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProjectBeer.Models;
using ProjectBeer.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiProjectBeer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        RepositoryBeer repo;
        HelperToken helper;

        public AuthController(RepositoryBeer repo
            , IConfiguration configuration)
        {
            this.helper = new HelperToken(configuration);
            this.repo = repo;
        }

        //NECESITAMOS UN PUNTO DE ENTRADA (ENDPOINT) PARA QUE EL 
        //USUARIO NOS ENVIE LOS DATOS DE SU VALIDACION
        //LOS ENDPOINT AUTH SON POST
        //LO QUE RECIBIREMOS SERA UserName y Password
        //QUE NOSOTROS LO HEMOS INCLUIDO CON LoginModel
        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(LoginModel model)
        {
            User user =
                this.repo.ExisteUsuario(model.UserName
                , model.Password);
            if (user != null)
            {
                //ASI NO FUNCIONABA EL [AUTHORIZE(Roles="aaaa")]
                //Claim[] claims = new[]
                //{
                //    new Claim("UserData",
                //    JsonConvert.SerializeObject(user))
                //};
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Role,user.Role)
                };

                JwtSecurityToken token = new JwtSecurityToken
                    (
                     issuer: helper.Issuer
                     , audience: helper.Audience
                     , claims: claims
                     , expires: DateTime.UtcNow.AddMinutes(10)
                     , notBefore: DateTime.UtcNow
                     , signingCredentials:
                    new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256)
                    );
                //DEVOLVEMOS UNA RESPUESTA AFIRMATIVA
                //CON SU TOKEN
                return Ok(
                    new
                    {
                        response =
                        new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}

