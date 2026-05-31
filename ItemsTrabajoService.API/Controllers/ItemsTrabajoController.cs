using ItemsTrabajoService.API.Data;
using ItemsTrabajoService.API.Models;
using ItemsTrabajoService.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItemsTrabajoService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsTrabajoController : ControllerBase
    {
        private readonly ItemsDbContext _context;
        private readonly IDistribucionService _distribucionService;
        private readonly IHttpClientFactory _httpClientFactory;

        public ItemsTrabajoController(ItemsDbContext context, IDistribucionService distribucionService, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _distribucionService = distribucionService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("distribuir")]
        public async Task<IActionResult> RegistrarYDistribuir([FromBody] ItemTrabajo nuevoItem)
        {
            if(nuevoItem == null)
                return BadRequest("El item de trabajo no puede ser nulo.");

            List<string> usuariosDisponibles = new();
            List<UsuarioDto>? usuariosDto = null;

            try
            {
                var client = _httpClientFactory.CreateClient("UsuariosClient");
                usuariosDto = await client.GetFromJsonAsync<List<UsuarioDto>>("api/usuarios");

                if(usuariosDto != null)
                {
                    usuariosDisponibles = usuariosDto.Select(x => x.UserName).ToList();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener usuarios disponibles: {ex.Message}");
            }

            if (!usuariosDisponibles.Any())
                return BadRequest("No hay usuarios disponibles");

            var itemsTotales = await _context.ItemsTrabajo.ToListAsync();
            var resumenUsuarios = new List<UsuarioResumen>();

            if (usuariosDto != null)
            {
                foreach (var usuario in usuariosDto)
                {
                   
                    var tareasActivas = itemsTotales
                        .Where(x => x.UsernameAsignado == usuario.UserName && x.Estado == EstadoEnum.Pendiente)
                        .ToList();
                   
                    resumenUsuarios.Add(new UsuarioResumen
                    {
                        UserName = usuario.UserName,
                        ItemsPendientes = tareasActivas
                    });
                }
            }


            bool asignadoExitosamente = _distribucionService.AsignarItem(nuevoItem, resumenUsuarios);

            if (!asignadoExitosamente)
                return BadRequest("No se pudo asignar la tarea. Todos los usuarios se encuentran SATURADOS.");

            _context.ItemsTrabajo.Add(nuevoItem);   
            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Ítem distribuido con éxito.", AsignadoA = nuevoItem.UsernameAsignado, Item = nuevoItem });
        }

    }
}
