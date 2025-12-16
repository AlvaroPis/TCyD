using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabajo_de_Campo_y_Diploma.Vista.Seguridad.permisos;

namespace Trabajo_de_Campo_y_Diploma.Vista.Seguridad.permisos
{
    public partial class detalle_permisos : Form
    {
        private static detalle_permisos instancia;
        //Permiso
        private Controladora.Seguridad.Permiso cPermiso = Controladora.Seguridad.Permiso.Obtener_instancia();
        private Modelo.Permisos permiso;
        //Formulario
        private Controladora.Seguridad.Formulario cFormulario = Controladora.Seguridad.Formulario.Obtener_instancia();

        private int id_permiso;

        public static detalle_permisos Obtener_instancia(int id_permiso)
        {
            if (instancia == null)
                instancia = new detalle_permisos(id_permiso);

            if (instancia.IsDisposed)
                instancia = new detalle_permisos(id_permiso);

            instancia.BringToFront();
            return instancia;
        }
        private detalle_permisos(int id_permiso)
        {
            InitializeComponent();
            drpFormulario.DataSource = cFormulario.getFormularios();
            drpFormulario.DisplayMember = "nombre"; // Nombre del campo a mostrar
            drpFormulario.ValueMember = "id_formulario";       // Nombre del campo que representa el valor
            this.id_permiso = id_permiso;
            if (id_permiso != 0)
            {
                permiso = cPermiso.getPermiso(id_permiso).FirstOrDefault();
                txtnombre.Text = permiso.nombre_permiso;
                drpFormulario.SelectedValue = permiso.id_formulario;
                checkEstado.Checked = (bool)permiso.estado;
            }
        }

        private void detalle_permisos_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id_permiso == 0)
            {
                permiso = new Modelo.Permisos();
                permiso.nombre_permiso = txtnombre.Text;
                permiso.estado = checkEstado.Checked;
                permiso.id_formulario = (int)drpFormulario.SelectedValue;
                cPermiso.agregarPermiso(permiso);
            }
            else
            {
                permiso.nombre_permiso = txtnombre.Text;
                permiso.estado = checkEstado.Checked;
                permiso.id_formulario = (int)drpFormulario.SelectedValue;
                cPermiso.modificarPermiso(permiso);
            }
            MessageBox.Show("Permiso guardado con exito");
            Vista.Seguridad.permisos.Gestionar_permisos.Obtener_instancia().filtrar();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void drpFormulario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
