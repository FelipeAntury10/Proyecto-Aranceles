using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class TiposDeProductos
    {
        //3 Atributos
        [Key] public int Id { get; set; } // :) int? != int ; null : 0 
        public string? Nombre { get; set; }
        public string? EntidadRegulatoria { get; set; }
    }
}