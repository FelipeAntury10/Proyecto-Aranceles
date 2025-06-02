using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Facturas
    {
        //4 Atributos
        [Key] public int Id { get; set; }
        public string? Cod { get; set; }
        public int? Id_Arancel { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? PagoTotalCop { get; set; }
        //Recibe 1
        [ForeignKey("Id_Arancel")] public Aranceles? _Arancel { get; set; }
    }
}