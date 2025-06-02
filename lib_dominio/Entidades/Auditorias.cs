using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Auditorias
    {
        // 3 Atributos
        [Key] public int Id { get; set; }
        public string? Cod { get; set; }
        public string? Accion { get; set; }
        public string? Entidad { get; set; }
        public string? Informacion { get; set; }
        public int? Id_Usuario { get; set; }
        public DateTime? Fecha { get; set; }


        //Recibe 1
        [ForeignKey("Id_Usuario")] public Usuarios? _Usuario { get; set; }

    }
}