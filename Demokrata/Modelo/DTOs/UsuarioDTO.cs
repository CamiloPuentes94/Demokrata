using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Demokrata.Modelo.DTOs
{
    public class UsuarioDTO
    {
       
        public int Id { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Sueldo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
