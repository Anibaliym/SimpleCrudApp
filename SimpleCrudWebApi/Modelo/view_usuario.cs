using System.Runtime.InteropServices;

namespace SimpleCrudWebApi.Modelo
{
    public class view_usuario
    {
        public long usuarioId { get; set; }
        public long personaId { get; set; }
        public String email { get; set; }
        public String contrasena { get; set; }
        public int tipoUsuario { get; set; }
        public DateTime fechaCreacion { get; set; }
        public long usuarioCreacionId { get; set; }
        public DateTime fechaModificacion { get; set; }
        public long usuarioMoodificacion { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
