using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Empresas
    {
        // 3 Atributos
        [Key] public int Id { get; set; }
        public int? Id_Pais { get; set; }
        public string? Nombre { get; set; }

        //Recibe 1
        [ForeignKey("Id_Pais")] public Paises? _Pais { get; set; }

    }
}