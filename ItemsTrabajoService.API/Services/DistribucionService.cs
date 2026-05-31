using ItemsTrabajoService.API.Models;

namespace ItemsTrabajoService.API.Services
{
    public class DistribucionService : IDistribucionService
    {
        public bool AsignarItem(ItemTrabajo item, List<UsuarioResumen> usuarios)
        {
            var usuariosDisponibles = usuarios.Where(u => !u.EstaSaturado).ToList();
            
            if (!usuariosDisponibles.Any())
            {
                return false;
            }
            
            UsuarioResumen? usuarioElegido = null;
            DateTime fechaActual = DateTime.Today;
            int diasRestantes = (item.FechaEntrega.Date - fechaActual).Days;
            
            if (diasRestantes < 3)
            {
                usuarioElegido = usuariosDisponibles
                    .OrderBy(u => u.ItemsPendientesCount)
                    .FirstOrDefault();
            }
            else
            {
                usuarioElegido = usuariosDisponibles
                    .OrderBy(u => u.ItemsPendientesCount)
                    .FirstOrDefault();
            }

            if (usuarioElegido != null)
            {
                item.UsernameAsignado = usuarioElegido.UserName;
                usuarioElegido.ItemsPendientes.Add(item);
                
                usuarioElegido.ItemsPendientes = usuarioElegido.ItemsPendientes
                    .OrderBy(i => i.FechaEntrega)
                    .ThenByDescending(i => i.Relevancia)
                    .ToList();

                return true;
            }

            return false;
        }
    }
}
