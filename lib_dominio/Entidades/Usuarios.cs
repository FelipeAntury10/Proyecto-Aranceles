using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Usuarios
    {
        // 3 Atributos
        [Key] public int Id { get; set; }
        public string? Cod { get; set; }
        public string? Usuario { get; set; }
        public string? Contraseña { get; set; }
        public int? Id_Rol { get; set; }

        //Recibe 1
        [ForeignKey("Id_Rol")] public Roles? _Rol { get; set; }

    }
}