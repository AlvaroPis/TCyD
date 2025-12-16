using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo_de_Campo_y_Diploma.Modelo;
using System.Data.Entity;

namespace Trabajo_de_Campo_y_Diploma.Controladora
{
    public class Presupuesto
    {
        private static Presupuesto instancia;

        public static Presupuesto Obtener_instancia()
        {
            if (instancia == null)
                instancia = new Presupuesto();
            return instancia;
        }

        public int GuardarPresupuesto(Presupuestos presupuesto)
        {
            using (var context = new Contexto())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // 1. Validar stock primero
                        foreach (var item in presupuesto.Linea_Presupuestos)
                        {
                            var producto = context.Productos.Find(item.ProductoId);
                            if (producto == null)
                                throw new Exception($"Producto con ID {item.ProductoId} no encontrado");

                            if (producto.stock < item.Cantidad)
                                throw new Exception($"Stock insuficiente para {producto.descripcion}. Disponible: {producto.stock}, Requerido: {item.Cantidad}");
                        }

                        // 2. Actualizar stocks
                        foreach (var item in presupuesto.Linea_Presupuestos)
                        {
                            var producto = context.Productos.Find(item.ProductoId);
                            producto.stock -= (int)item.Cantidad;
                            context.Entry(producto).State = EntityState.Modified;
                        }

                        // 3. Guardar el presupuesto principal
                        context.Presupuestos.Add(presupuesto);
                        context.SaveChanges(); // Guarda primero el presupuesto para obtener el ID

                        // 4. Asignar IDs y guardar líneas
                        foreach (var item in presupuesto.Linea_Presupuestos)
                        {
                            item.PresupuestoId = presupuesto.PresupuestoId;
                            context.Linea_Presupuestos.Add(item);
                        }

                        context.SaveChanges();
                        dbContextTransaction.Commit();

                        return presupuesto.PresupuestoId;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        // Loggear el error (puedes usar tu sistema de logging)
                        throw new Exception("Error al guardar presupuesto. Cambios revertidos. " + ex.Message);
                    }
                }
            }
        }

        // Función para obtener productos disponibles con stock
        public List<Productos> ObtenerProductosDisponibles()
        {
            using (var context = new Contexto())
            {
                try
                {
                    return context.Productos
                        .Where(p => p.stock > 0)        // Solo productos con stock
                        .OrderBy(p => p.descripcion)    // Ordenados alfabéticamente
                        .ToList();
                }
                catch (Exception ex)
                {
                    // Puedes loggear el error aquí
                    throw new Exception("Error al obtener productos: " + ex.Message);
                }
            }
        }

        // Función para obtener un producto específico por ID
        public Productos ObtenerProductoPorId(int productoId)
        {
            using (var context = new Contexto())
            {
                return context.Productos
                    .FirstOrDefault(p => p.ProductoId == productoId);
            }
        }

        // Resto de tus métodos (GuardarPresupuesto, etc.)
    }
}
