using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabajo_de_Campo_y_Diploma.Controladora;
using Trabajo_de_Campo_y_Diploma.Controladora.Seguridad;
using Trabajo_de_Campo_y_Diploma.Controladora.Seguridad_composite;
using Trabajo_de_Campo_y_Diploma.Modelo;
using Trabajo_de_Campo_y_Diploma.Vista;
using Trabajo_de_Campo_y_Diploma.Vista.Clientes;
using Trabajo_de_Campo_y_Diploma.Vista.Presupuestos;
using Trabajo_de_Campo_y_Diploma.Vista.Seguridad.grupo;
using Trabajo_de_Campo_y_Diploma.Vista.Seguridad.permisos;
using Trabajo_de_Campo_y_Diploma.Vista.Seguridad.Usuarios;

namespace Trabajo_de_Campo_y_Diploma.Vista 
{
    public partial class Menu : Form
    {
        private static Menu instancia;
        private Modelo.Usuarios usuario;
        private List<Modelo.Formularios> formularios = new List<Modelo.Formularios>();
        private List<Modelo.Modulos> modulos = new List<Modelo.Modulos>();
        Controladora.Seguridad_composite.PermisoGrupo cPermisoGrupo = Controladora.Seguridad_composite.PermisoGrupo.Obtener_instancia();
        public static Menu Obtener_instancia(Modelo.Usuarios usuario)
        {
            if (instancia == null)
                instancia = new Menu(usuario);
            if (instancia.IsDisposed)
                instancia = new Menu(usuario);

            instancia.BringToFront();
            return instancia;
        }
        private Menu(Trabajo_de_Campo_y_Diploma.Modelo.Usuarios usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            cPermisoGrupo.GetPermisosLogin(usuario.id_usuario);
            formularios = cPermisoGrupo.GetFormulariosUsuario(usuario.id_usuario);
            modulos = cPermisoGrupo.GetModulosUsuario(usuario.id_usuario);
            ConfigurarModulos();
            ConfigurarFormularios();
        }

        private void ConfigurarFormularios()
        {
            //Seguridad
            formularioUsuarios.Visible = formularios.Any(f => f.nombre == "Usuarios");
            formularioGrupos.Visible = formularios.Any(f => f.nombre == "Grupos");
            formularioPermiso.Visible = formularios.Any(f => f.nombre == "Permisos");
            //Ventas
            formularioGenerarPresupuesto.Visible = formularios.Any(f => f.nombre == "Generar presupuesto");
            formularioGestionarClientes.Visible = formularios.Any(f => f.nombre == "Gestionar clientes");
        }

        private void ConfigurarModulos()
        {
            moduloSeguridad.Visible = modulos.Any(m => m.nombre == "Seguridad");
            moduloVentas.Visible = modulos.Any(m => m.nombre == "Ventas");
            moduloReportes.Visible = modulos.Any(m => m.nombre == "Reportes");
        }
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Trabajo_de_Campo_y_Diploma.Vista.Seguridad.Usuarios.Gestionar_usuarios.Obtener_instancia();
            form.Show();
        }

        private void gestionarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Gestionar_clientes.Obtener_instancia();
            form.ShowDialog();
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Trabajo_de_Campo_y_Diploma.Vista.Seguridad.grupo.Gestionar_grupos.Obtener_instancia();
            form.ShowDialog();
        }

        private void permisoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Trabajo_de_Campo_y_Diploma.Vista.Seguridad.permisos.Gestionar_permisos.Obtener_instancia();
            form.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void formularioUsuarios_Click(object sender, EventArgs e)
        {
            Form form = Gestionar_usuarios.Obtener_instancia();
            form.Show();
        }

        private void formularioGrupos_Click(object sender, EventArgs e)
        {
            Form form = Gestionar_grupos.Obtener_instancia();
            form.ShowDialog();
        }

        private void formularioPermiso_Click(object sender, EventArgs e)
        {
            Form form = Gestionar_permisos.Obtener_instancia();
            form.ShowDialog();
        }

        private void formularioGestionarClientes_Click(object sender, EventArgs e)
        {
            Form form = Gestionar_clientes.Obtener_instancia();
            form.ShowDialog();
        }

        private void generarPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Generar_presupuestos.Obtener_instancia();
            form.ShowDialog();
        }

        private void moduloSeguridad_Click(object sender, EventArgs e)
        {

        }
    }
}
