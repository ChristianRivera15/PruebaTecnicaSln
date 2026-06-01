using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuariosService.API.Data;
using UsuariosService.API.Models;

namespace UsuariosService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private static readonly List<Usuario> _usuariosExistentes = new()
        {
            new Usuario { Id = 1, UserName = "Usuario A" },
            new Usuario { Id = 2, UserName = "Usuario B" },
            new Usuario { Id = 2, UserName = "Usuario C" }
        };


        /// <summary>
        /// en este método se obtiene la lista de usuarios existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            return Ok(_usuariosExistentes);
        }

    }
}
