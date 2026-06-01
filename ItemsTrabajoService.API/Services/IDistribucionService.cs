using ItemsTrabajoService.API.Models;

namespace ItemsTrabajoService.API.Services
{
    public interface IDistribucionService
    {
        public UsuarioResumen AsignarItem(ItemTrabajo item, List<UsuarioResumen> usuarios);
    }
}
