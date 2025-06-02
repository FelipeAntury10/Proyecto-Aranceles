using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Aranceles
    {
        //4 Atributos
        [Key] public int Id { get; set; }
        public string? Cod { get; set; }
        public int? Id_Orden { get; set; }
        public int? Id_TipoDeArancel { get; set; }
        public decimal? PorcentajeDelArancel { get; set; }
        // Recibe 2
        [ForeignKey("Id_Orden")] public Ordenes? _Orden { get; set; }
        [ForeignKey("Id_TipoDeArancel")] public TiposDeAranceles? _TipoArancel { get; set; }

    }
}