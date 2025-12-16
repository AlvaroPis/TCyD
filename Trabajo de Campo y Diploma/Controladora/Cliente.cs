using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Trabajo_de_Campo_y_Diploma.Modelo;

namespace Trabajo_de_Campo_y_Diploma.Controladora
{
    public class Cliente
    {
        private static Cliente cliente;

        public static Cliente Obtener_instancia()
        {
            if (cliente == null)
            {
                cliente = new Cliente();
            }
            return cliente;
        }

        private Cliente() { }

        public List<Trabajo_de_Campo_y_Diploma.Modelo.Clientes> GetCliente(int dni)
        {
            var clientesConDni = from cliente in Trabajo_de_Campo_y_Diploma.Modelo.Contexto.Obtener_instancia().Clientes
                                 where cliente.dni == dni
                                 select cliente;
            return clientesConDni.ToList();
        }

        public List<Trabajo_de_Campo_y_Diploma.Modelo.Clientes> GetClienteID(int id_cliente)
        {
            var clientes = from cliente in Trabajo_de_Campo_y_Diploma.Modelo.Contexto.Obtener_instancia().Clientes
                           where cliente.id_cliente == id_cliente
                           select cliente;
            return clientes.ToList();
        }

        public System.Collections.IList getClientes()
        {
            var clientesConDni = from cliente in Trabajo_de_Campo_y_Diploma.Modelo.Contexto.Obtener_instancia().Clientes
                                 select cliente;
            return clientesConDni.ToList();
        }

        public void agregarCliente(Trabajo_de_Campo_y_Diploma.Modelo.Clientes cliente)
        {
            Trabajo_de_Campo_y_Diploma.Modelo.Contexto.Obtener_instancia().Clientes.Add(cliente);
            Trabajo_de_Campo_y_Diploma.Modelo.Contexto.Obtener_instancia().SaveChanges();
        }

        public void modificarCliente(Trabajo_de_Campo_y_Diploma.Modelo.Clientes cliente)
        {
            Trabajo_de_Campo_y_Diploma.Modelo.Contexto.Obtener_instancia().Entry(cliente).State = System.Data.Entity.EntityState.Modified;
            Trabajo_de_Campo_y_Diploma.Modelo.Contexto.Obtener_instancia().SaveChanges();
        }

        public void eliminarCliente(Trabajo_de_Campo_y_Diploma.Modelo.Clientes cliente)
        {
            cliente.estado = false;
            Trabajo_de_Campo_y_Diploma.Modelo.Contexto.Obtener_instancia().Entry(cliente).State = System.Data.Entity.EntityState.Modified;
            Trabajo_de_Campo_y_Diploma.Modelo.Contexto.Obtener_instancia().SaveChanges();
        }

    }
}