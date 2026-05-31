using System.ComponentModel.DataAnnotations;

namespace ItemsTrabajoService.API.Models
{
    public class ItemTrabajo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public DateTime FechaEntrega { get; set; }
        public RelevanciaEnum Relevancia { get; set; }
        public EstadoEnum Estado { get; set; } = EstadoEnum.Pendiente;
        public string? UsernameAsignado { get; set; }
    }
}
