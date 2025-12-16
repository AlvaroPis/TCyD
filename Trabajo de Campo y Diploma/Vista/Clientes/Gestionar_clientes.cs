using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabajo_de_Campo_y_Diploma.Controladora;
using Trabajo_de_Campo_y_Diploma.Controladora.Seguridad_composite;

namespace Trabajo_de_Campo_y_Diploma.Vista.Clientes
{
    public partial class Gestionar_clientes : Form
    {
        private static Gestionar_clientes instancia;
        Controladora.Cliente cCliente = Controladora.Cliente.Obtener_instancia();
        Controladora.Seguridad_composite.PermisoGrupo cPermisoGrupo = Controladora.Seguridad_composite.PermisoGrupo.Obtener_instancia();
        private List<Modelo.Clientes> clientes;
        private List<Modelo.Clientes> clientesFiltrados;
        private Modelo.Clientes cliente;
        private int dni;
        private int index;
        public static Gestionar_clientes Obtener_instancia()
        {
            if (instancia == null)
            {
                instancia = new Gestionar_clientes();
            }
            if (instancia.IsDisposed)
            {
                instancia = new Gestionar_clientes();
            }
            instancia.BringToFront();
            return instancia;
        }
        private Gestionar_clientes()
        {
            InitializeComponent();
            // Primero agregar los ítems
            comboBoxfiltro.Items.Add("DNI");
            comboBoxfiltro.Items.Add("Nombre");

            // Luego seleccionar (solo si hay elementos)
            if (comboBoxfiltro.Items.Count > 0)
            {
                comboBoxfiltro.SelectedIndex = 0;
            }
            else
            {
                comboBoxfiltro.Text = "Seleccione filtro"; // Mensaje alternativo
            }

            // Resto de tu inicialización...
            ConfigurarPermisosBotones();
            clientes = (List<Modelo.Clientes>)cCliente.getClientes();
            filtrar();
            buttonEliminar.Enabled = false;
            buttonModificar.Enabled = false;
            dataClientes.Columns[0].Visible = false;
            dataClientes.Columns[4].Visible = false;
            checkBoxSoloHabilitados.Checked = true;
        }

        private void ConfigurarPermisosBotones()
        {
            buttonAgregar.Show();
            buttonModificar.Show();
            buttonEliminar.Show();
            //buttonAgregar.Visible = cPermisoGrupo.valiPermiso("Agregar cliente");
            //buttonModificar.Visible = cPermisoGrupo.valiPermiso("Modificar cliente");
            //buttonEliminar.Visible = cPermisoGrupo.valiPermiso("Eliminar cliente");

        }

        public void filtrar()
        {
            clientes = (List<Modelo.Clientes>)cCliente.getClientes();
            clientesFiltrados = clientes;
            if (comboBoxfiltro.Text == "Nombre" && textBoxNombre.Text != "")
            {
                clientesFiltrados = clientesFiltrados.Where(cliente => cliente.nombre.ToLower().Contains(textBoxNombre.Text.ToLower()) && cliente.estado == checkBoxSoloHabilitados.Checked).ToList();
                dataClientes.DataSource = clientesFiltrados;
            }
            else if (comboBoxfiltro.Text == "DNI" && textBoxNombre.Text != "")
            {
                clientesFiltrados = clientesFiltrados.Where(cliente => cliente.dni.ToString().ToLower().Contains(textBoxNombre.Text.ToLower()) && cliente.estado == checkBoxSoloHabilitados.Checked).ToList();
                dataClientes.DataSource = clientesFiltrados;
            }
            else
            {
                dataClientes.DataSource = clientesFiltrados.Where(cliente => cliente.estado == checkBoxSoloHabilitados.Checked).ToList();
            }

        }
        private void Gestionar_clientes_Load(object sender, EventArgs e)
        {
            buttonAgregar.BringToFront();
            buttonModificar.BringToFront();
            buttonEliminar.BringToFront();

            // Verificar si están dentro de un panel/container que no es visible
            if (buttonAgregar.Parent != null)
            {
                buttonAgregar.Parent.Visible = true;
                buttonAgregar.Parent.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            if (index != -1)
            {
                buttonAgregar.Enabled = true;
                buttonEliminar.Enabled = true;
                buttonModificar.Enabled = true;
                dni = Convert.ToInt32(dataClientes.Rows[index].Cells[0].Value);
            }
            else
            {
                buttonAgregar.Enabled = false;
                buttonEliminar.Enabled = false;
                buttonModificar.Enabled = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            Form ventas = modal_clientes.Obtener_instancia(0);
            ventas.Show();
            filtrar();
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            Form form = modal_clientes.Obtener_instancia(dni);
            form.Show();
            filtrar();
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar el cliente?", "Eliminar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clientesFiltrados = clientes;
                cliente = new Modelo.Clientes();
                cliente = clientesFiltrados.Where(cliente => cliente.id_cliente == dni).FirstOrDefault();
                cCliente.eliminarCliente(cliente);
                filtrar();
            }
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            textBoxNombre.Text = "";
            filtrar();
        }

        private void checkBoxSoloHabilitados_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBoxSoloHabilitados.Checked)
            {
                filtrar();
            }
            else
            {
                filtrar();
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
