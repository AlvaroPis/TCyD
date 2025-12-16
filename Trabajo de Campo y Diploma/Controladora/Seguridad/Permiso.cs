using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_de_Campo_y_Diploma.Controladora.Seguridad
{
    public class Permiso
    {
        private static Permiso permiso;

        public static Permiso Obtener_instancia()
        {
            if (permiso == null)
            {
                permiso = new Permiso();
            }
            return permiso;
        }
        private Permiso() { }

        public List<Modelo.Permisos> getPermisos()
        {
            return Modelo.Contexto.Obtener_instancia().Permisos
           .AsNoTracking()
           .Include(p => p.Formularios) // Incluye relación si existe
           .ToList();
        }

        public List<Modelo.Permisos> getPermiso(int id)
        {
            // Agregar logging para diagnóstico
            Console.WriteLine("Obteniendo todos los permisos...");

            var permisos = Modelo.Contexto.Obtener_instancia().Permisos
                           .AsNoTracking()
                           .ToList();

            Console.WriteLine($"Total permisos obtenidos: {permisos.Count}");
            return permisos;
        }

        public List<Modelo.Permisos> getPermisosGrupo(int id)
        {
            var grupo = Modelo.Contexto.Obtener_instancia().Grupos.Include(g => g.Permisos).FirstOrDefault(g => g.id_grupo == id);
            if (grupo == null)
            {
                // Si no existe el grupo, devolver una lista vacía
                return new List<Modelo.Permisos>();
            }

            // Devolver los permisos asociados al grupo
            return grupo.Permisos.ToList();
        }

        public List<Modelo.Permisos> getPermisosUsuario(int id)
        {
            var grupo = Modelo.Contexto.Obtener_instancia().Usuarios.Include(g => g.Permisos).FirstOrDefault(g => g.id_usuario == id);
            if (grupo == null)
            {
                // Si no existe el grupo, devolver una lista vacía
                return new List<Modelo.Permisos>();
            }

            // Devolver los permisos asociados al grupo
            return grupo.Permisos.ToList();
        }

        public void agregarPermiso(Modelo.Permisos permiso)
        {

            Modelo.Contexto.Obtener_instancia().Permisos.Add(permiso);
            Modelo.Contexto.Obtener_instancia().SaveChanges();
        }

        public void modificarPermiso(Modelo.Permisos permiso)
        {
            var contexto = Modelo.Contexto.Obtener_instancia();
            var permisoExistente = contexto.Permisos.Find(permiso.id_permiso); // Busca en la BD

            if (permisoExistente != null)
            {
                // Copia las propiedades del objeto recibido al existente
                contexto.Entry(permisoExistente).CurrentValues.SetValues(permiso);
                contexto.SaveChanges();
            }
        }

        public void eliminarPermiso(Modelo.Permisos permiso)
        {
            permiso.estado = false; // Cambia el estado a inactivo
            modificarPermiso(permiso); // Reutiliza el método existente
        }
    }
}
