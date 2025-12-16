using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_de_Campo_y_Diploma.Controladora.Seguridad
{
    public class SesionManager
    {
        private static SesionManager sesionManager;

        public static SesionManager Obtener_instancia()
        {
            if (sesionManager == null)
            {
                sesionManager = new SesionManager();
            }
            return sesionManager;
        }
        private SesionManager() { }

        public Modelo.Usuarios GetUsuario(string usuario)
        {
            var user = Modelo.Contexto.Obtener_instancia().Usuarios.FirstOrDefault(u => u.usuario == usuario && u.estado == true);
            return user;
        }

        public bool LoginUser(string usuario, string contrasena)
        {
            string contrasenaEncriptada = Comun.MetodosComunes.Encriptar(contrasena);
            var user = Modelo.Contexto.Obtener_instancia().Usuarios
                      .FirstOrDefault(u => u.usuario == usuario &&
                                          u.clave == contrasenaEncriptada &&
                                          u.estado == true);
            return user != null;
        }

    }
}
