using ItemsTrabajoService.API.Models;

namespace ItemsTrabajoService.API.Services
{
    public interface IDistribucionService
    {
        bool AsignarItem(ItemTrabajo item, List<UsuarioResumen> usuarios);
    }
}
