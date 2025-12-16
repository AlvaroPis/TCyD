using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Trabajo_de_Campo_y_Diploma.Modelo
{
    public class Contexto : DbContext
    {
        private static Contexto instancia;

        public static Contexto Obtener_instancia()
        {
            {
                if (instancia == null)
                {
                    instancia = new Contexto();
                }
                return instancia;
            }
        }
        public Contexto() : base("name=Contexto")
        {
            Database.SetInitializer<Contexto>(null);
        }
        public virtual DbSet<Clientes> Clientes { get; set; }

        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Pedidos> Pedidos { get; set; }

        public virtual DbSet<Linea_Presupuestos> Linea_Presupuestos { get; set; }

        public virtual DbSet<Presupuestos> Presupuestos { get; set; }

        public virtual DbSet<Linea_Pedidos> Linea_Pedidos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Grupos> Grupos { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }   
        public virtual DbSet<Formularios> Formularios { get; set; } 
        public virtual DbSet<Modulos> Modulos { get; set; } 



    }
} 

