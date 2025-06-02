using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class TiposDeAranceles
    {
        //3 Atributos
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaVigencia { get; set; }
        
    }
}