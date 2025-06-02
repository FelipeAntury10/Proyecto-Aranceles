using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Productos
    {
        // 5 Atributos
        [Key] public int Id { get; set; }
        public int? Id_Empresa { get; set; }
        public int? Id_TipoProducto { get; set; }
        public string? Nombre { get; set; }
        public decimal? PrecioUnitario { get; set; }
        //Recibe 2

        [ForeignKey("Id_Empresa")] public Empresas? _Empresa { get; set; }

        [ForeignKey("Id_TipoProducto")] public TiposDeProductos? _TipoProducto { get; set; }

        //Envia 1

        // public List<Ordenes>? Ordenes { get; set; }

    }
}