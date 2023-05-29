namespace SimpleCrudWebApi.Modelo.system
{
    public class view_respuestasSistema
    {
        public int errorCode { get; set; }
        public string? detalles { get; set; }
        public int registrosCreados { get; set; }
        public int registrosActualizados { get; set; }
        public int registrosEliminados { get; set; }
        public List<object> lista { get; set; }
    }
}
