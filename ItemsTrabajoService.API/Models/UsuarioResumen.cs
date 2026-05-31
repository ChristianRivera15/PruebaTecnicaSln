namespace ItemsTrabajoService.API.Models
{
    public class UsuarioResumen
    {
        public string UserName { get; set; } = string.Empty;
        public int ItemsCompletadosCount { get; set; }
        public List<ItemTrabajo> ItemsPendientes { get; set; } = new List<ItemTrabajo>();
        public int ItemsPendientesCount => ItemsPendientes.Count;
        public int ItemsAltaRelevanciaCount => ItemsPendientes.Count(i => i.Relevancia == RelevanciaEnum.Alta);
        public bool EstaSaturado => ItemsAltaRelevanciaCount > 3;
    }
}
