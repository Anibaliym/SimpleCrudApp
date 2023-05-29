namespace SimpleCrudWebApi.Modelo
{
    public class view_persona
    {
        public long personaId { get; set; }
        public String? rut { get; set; }
        public String? nombre { get; set; }
        public String? apellidoPaterno { get; set; }
        public String? apellidoMaterno { get; set; }
        public DateTime? nacimiento { get; set; }
        public String? sexo { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public long usuarioCreacionId { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public long usuarioMoodificacion { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
