using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioMedicamentos.usuarios
{
    public static class UsuarioActual
    {
        public static int IdUsuario { get; set; } = 0;
        public static string RolUsuario { get; set; } = string.Empty;
        public static string correo { get; set; } = string.Empty;
        public static string contrasena { get; set; } = string.Empty;
        public static void EstablecerUsuario(int id, string rol)
        {
            IdUsuario = id;
            RolUsuario = rol;

        }
    }
}
