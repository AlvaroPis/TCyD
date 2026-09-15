using System;
using System.Collections.Generic;
using System.Linq;
using Trabajo_de_Campo_y_Diploma.Modelo;

namespace Trabajo_de_Campo_y_Diploma.Controladora.Seguridad_composite
{
    public class PermisoGrupo : PermisoComponent
    {
        private static PermisoGrupo instancia;

        public static PermisoGrupo Obtener_instancia()
        {
            if (instancia == null)
            {
                instancia = new PermisoGrupo();
            }
            return instancia;
        }
        private PermisoGrupo() { }

        private readonly List<PermisoComponent> _components = new List<PermisoComponent>();
        private List<Permisos> todosLosPermisos;

        public override void Add(PermisoComponent component)
        {
            _components.Add(component);
        }

        public override void Remove(PermisoComponent component)
        {
            _components.Remove(component);
        }

        public override bool valiPermiso(string permissionName)
        {
            return todosLosPermisos?.Any(p => p.nombre_permiso == permissionName) ?? false;
        }

        public List<Permisos> GetPermisosUsuario(int idUsuario)
        {
            Console.WriteLine($"   GetPermisosUsuario para ID: {idUsuario}");

            try
            {
                var context = new Contexto();

                context.Database.Connection.Open();

                var permisosUsuario = context.Permisos
                    .Where(p => p.estado != false) // habilitado = true o sin definir, igual que en la grilla de admin
                    .Where(p => p.Usuarios.Any(u => u.id_usuario == idUsuario))
                    .ToList();

                context.Database.Connection.Close();

                Console.WriteLine($"     Encontrados: {permisosUsuario.Count}");
                return permisosUsuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"     ERROR: {ex.Message}");
                return new List<Permisos>();
            }
        }

        public List<Permisos> GetPermisosGrupo(int idUsuario)
        {
            Console.WriteLine($"   GetPermisosGrupo para ID: {idUsuario}");

            try
            {
                var context = new Contexto();
                context.Database.Connection.Open();

                var gruposUsuario = context.Grupos
                    .Where(g => g.estado == true)
                    .Where(g => g.Usuarios.Any(u => u.id_usuario == idUsuario))
                    .ToList();

                Console.WriteLine($"     Grupos del usuario: {gruposUsuario.Count}");

                var permisosGrupos = new List<Permisos>();
                foreach (var grupo in gruposUsuario)
                {
                    var permisosDelGrupo = context.Permisos
                        .Where(p => p.estado != false) // habilitado = true o sin definir, igual que en la grilla de admin
                        .Where(p => p.Grupos.Any(g => g.id_grupo == grupo.id_grupo))
                        .ToList();

                    permisosGrupos.AddRange(permisosDelGrupo);
                    Console.WriteLine($"     Grupo '{grupo.grupo_nombre}': {permisosDelGrupo.Count} permisos");
                }

                context.Database.Connection.Close();

                return permisosGrupos.Distinct().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"     ERROR: {ex.Message}");
                return new List<Permisos>();
            }
        }

        public List<Permisos> GetPermisosLogin(int idUsuario)
        {
            Console.WriteLine($"\n=== GET PERMISOS LOGIN (Usuario: {idUsuario}) ===");

            try
            {
                var permisosUsuario = GetPermisosUsuario(idUsuario);
                var permisosGrupos = GetPermisosGrupo(idUsuario);

                todosLosPermisos = permisosUsuario
                    .Union(permisosGrupos)
                    .Distinct()
                    .ToList();

                Console.WriteLine($"Permisos directos: {permisosUsuario.Count}");
                Console.WriteLine($"Permisos por grupos: {permisosGrupos.Count}");
                Console.WriteLine($"Total único: {todosLosPermisos.Count}");

                if (todosLosPermisos.Count > 0)
                {
                    Console.WriteLine("Lista de permisos:");
                    foreach (var permiso in todosLosPermisos)
                    {
                        Console.WriteLine($"  - {permiso.nombre_permiso}");
                    }
                }
                else
                {
                    Console.WriteLine("⚠ No se encontraron permisos");
                }

                return todosLosPermisos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR GetPermisosLogin: {ex.Message}");
                return new List<Permisos>();
            }
        }

        public List<Formularios> GetFormulariosUsuario(int idUsuario)
        {
            Console.WriteLine($"\n=== GET FORMULARIOS USUARIO (ID: {idUsuario}) ===");

            try
            {
                var context = new Contexto();
                context.Database.Connection.Open();

                var todosFormularios = context.Formularios.ToList();
                Console.WriteLine($"Formularios en BD: {todosFormularios.Count}");
                foreach (var f in todosFormularios)
                {
                    Console.WriteLine($"  - {f.nombre} (ID: {f.id_formulario})");
                }

                var permisos = GetPermisosLogin(idUsuario);

                var formulariosUsuario = permisos
                    .Where(p => p.Formularios != null && p.estado != false)
                    .Select(p => p.Formularios)
                    .Distinct()
                    .ToList();

                context.Database.Connection.Close();

                Console.WriteLine($"Formularios del usuario: {formulariosUsuario.Count}");

                return formulariosUsuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR GetFormulariosUsuario: {ex.Message}");

                // ponytail: fail-closed, sin permisos válidos no se otorga acceso por defecto
                return new List<Formularios>();
            }
        }

        public List<Modulos> GetModulosUsuario(int idUsuario)
        {
            Console.WriteLine($"\n=== GET MÓDULOS USUARIO (ID: {idUsuario}) ===");

            try
            {
                var formularios = GetFormulariosUsuario(idUsuario);

                var modulos = formularios
                    .Where(f => f.Modulos != null)
                    .Select(f => f.Modulos)
                    .Distinct()
                    .ToList();

                Console.WriteLine($"Módulos encontrados: {modulos.Count}");

                return modulos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR GetModulosUsuario: {ex.Message}");
                // ponytail: fail-closed, sin permisos válidos no se otorga acceso por defecto
                return new List<Modulos>();
            }
        }
        public void DebugBaseDeDatos(int idUsuario)
        {

            var context = Modelo.Contexto.Obtener_instancia();

            try
            {
                // 1. Usuario
                var usuario = context.Usuarios.FirstOrDefault(u => u.id_usuario == idUsuario);
                Console.WriteLine($"1. USUARIO: {(usuario != null ? usuario.nombre : "NO ENCONTRADO")}");

                // 2. Todos los formularios
                Console.WriteLine("\n2. TODOS LOS FORMULARIOS EN BD:");
                var formulariosBD = context.Formularios.ToList();
                foreach (var f in formulariosBD)
                {
                    Console.WriteLine($"   - {f.nombre} (ID: {f.id_formulario})");
                }

                // 3. Buscar específicamente "Generar presupuesto"
                var presupuestoForm = formulariosBD.FirstOrDefault(f =>
                    f.nombre.Equals("Generar presupuesto", StringComparison.OrdinalIgnoreCase));

                if (presupuestoForm != null)
                {
                    Console.WriteLine($"\n3. FORMULARIO 'Generar presupuesto': ENCONTRADO (ID: {presupuestoForm.id_formulario})");

                    // Verificar si hay permiso para este formulario
                    var permisoPresupuesto = context.Permisos
                        .FirstOrDefault(p => p.id_formulario == presupuestoForm.id_formulario);

                    if (permisoPresupuesto != null)
                    {
                        Console.WriteLine($"   Permiso asociado: {permisoPresupuesto.nombre_permiso} (ID: {permisoPresupuesto.id_permiso})");
                    }
                    else
                    {
                        Console.WriteLine($"   ⚠ NO hay permiso asociado a este formulario");
                    }
                }
                else
                {
                    Console.WriteLine($"\n3. FORMULARIO 'Generar presupuesto': NO ENCONTRADO");
                    Console.WriteLine($"   Buscando similares...");
                    var similares = formulariosBD
                        .Where(f => f.nombre != null &&
                                    f.nombre.ToLower().Contains("presupuesto"))
                        .ToList();
                    if (similares.Count > 0)
                    {
                        foreach (var s in similares)
                        {
                            Console.WriteLine($"   - {s.nombre} (ID: {s.id_formulario})");
                        }
                    }
                }

                // 4. Grupos del usuario
                Console.WriteLine("\n4. GRUPOS DEL USUARIO:");
                var grupos = context.Grupos
                    .Where(g => g.Usuarios.Any(u => u.id_usuario == idUsuario))
                    .ToList();

                if (grupos.Count > 0)
                {
                    foreach (var g in grupos)
                    {
                        Console.WriteLine($"   - {g.grupo_nombre} (ID: {g.id_grupo})");
                    }
                }
                else
                {
                    Console.WriteLine($"   ⚠ El usuario no tiene grupos asignados");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en DebugBaseDeDatos: {ex.Message}");
            }
        }
    }
}