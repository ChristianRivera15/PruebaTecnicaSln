using ItemsTrabajoService.API.Models;

namespace ItemsTrabajoService.API.Services
{
    public class DistribucionService : IDistribucionService
    {
        public UsuarioResumen AsignarItem(ItemTrabajo item, List<UsuarioResumen> usuarios)
        {
            var diasRestantes = (item.FechaEntrega - DateTime.Today).Days;
            UsuarioResumen usuarioElegido;

            if(diasRestantes < 3)
            {
                // validacion 1: cerca de vencer menos items de trabajo independientemente de su relevancia 
                usuarioElegido = usuarios.OrderBy(u => u.ItemsPendientesCount).FirstOrDefault();
            }
            else
            {
                // validacion 2: no vence pronto items relevantes se asigna primero a menos lista de pendientes
                usuarioElegido = usuarios.OrderByDescending(u => item.Relevancia == RelevanciaEnum.Alta)
                    .ThenBy(u => u.ItemsPendientesCount)
                    .FirstOrDefault();
            }

            usuarioElegido.ItemsPendientes.Add(item);
            
            //Ordenamos su lista interna de pendientes priorizando por relevancia Alta y luego por Fecha de Entrega
            usuarioElegido.ItemsPendientes = usuarioElegido.ItemsPendientes
                .OrderByDescending(x => x.Relevancia == RelevanciaEnum.Alta)
                .ThenBy(x => x.FechaEntrega)
                .ToList();

            item.UsernameAsignado = usuarioElegido.UserName;



            return usuarioElegido;

        }
    }
}
