using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabajo_de_Campo_y_Diploma.Controladora;
using ModeloPresupuesto = Trabajo_de_Campo_y_Diploma.Modelo.Presupuestos;
using ModeloLineaPresupuesto = Trabajo_de_Campo_y_Diploma.Modelo.Linea_Presupuestos;
using ModeloProducto = Trabajo_de_Campo_y_Diploma.Modelo.Productos;

namespace Trabajo_de_Campo_y_Diploma.Vista.Presupuestos
{
    public partial class Generar_presupuestos : Form
    {
        private static Generar_presupuestos instancia;
        private Controladora.Presupuesto cPresupuesto = Controladora.Presupuesto.Obtener_instancia();
        private List<ModeloProducto> productosDisponibles = new List<ModeloProducto>();
        private List<ModeloLineaPresupuesto> itemsPresupuesto = new List<ModeloLineaPresupuesto>();
        private decimal total = 0;

        public static Generar_presupuestos Obtener_instancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new Generar_presupuestos();
            }
            instancia.BringToFront();
            return instancia;
        }

        public Generar_presupuestos()
        {
            InitializeComponent();
            CargarProductos();
        }

        private void CargarProductos()
        {
            productosDisponibles = cPresupuesto.ObtenerProductosDisponibles()
                .Where(p => p.stock > 0)
                .OrderBy(p => p.descripcion)
                .ToList();

            // Configurar ComboBox
            comboBoxProductos.DataSource = productosDisponibles;
            comboBoxProductos.DisplayMember = "descripcion";
            comboBoxProductos.ValueMember = "ProductoId";

            // DataGridView de productos (si existe)
            if (dataGridViewProductos != null)
                dataGridViewProductos.DataSource = productosDisponibles;
        }

        private void Generar_presupuestos_Load(object sender, EventArgs e)
        {
            // Código de carga si es necesario
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (comboBoxProductos.SelectedItem == null || numericUpDownCantidad.Value <= 0)
            {
                MessageBox.Show("Seleccione un producto y cantidad válida");
                return;
            }

            var producto = (ModeloProducto)comboBoxProductos.SelectedItem;
            int cantidad = (int)numericUpDownCantidad.Value;

            // Verificar stock
            if (producto.stock < cantidad)
            {
                MessageBox.Show($"Stock insuficiente. Disponible: {producto.stock}");
                return;
            }

            // Crear línea de presupuesto
            var lineaPresupuesto = new ModeloLineaPresupuesto
            {
                ProductoId = producto.ProductoId,
                Producto = producto,
                Cantidad = cantidad,
                PrecioUnitario = producto.costo_producto ?? 0,
                PrecioTotal = (producto.costo_producto ?? 0) * cantidad
            };

            // Agregar a la lista
            itemsPresupuesto.Add(lineaPresupuesto);

            // Agregar al DataGridView
            dataGridViewPresupuesto.Rows.Add(
                producto.ProductoId,
                producto.descripcion,
                cantidad,
                producto.costo_producto,
                (decimal)producto.costo_producto * cantidad
            );

            // Actualizar stock visualmente
            producto.stock -= cantidad;
            CargarProductos(); // Refrescar lista
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            total = itemsPresupuesto.Sum(item => item.PrecioTotal);
            lblTotal.Text = total.ToString("C2");
        }

        private void btnGuardarPresupuesto_Click(object sender, EventArgs e)
        {
            if (itemsPresupuesto.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto al presupuesto");
                return;
            }

            try
            {
                // Usar el alias ModeloPresupuesto
                var nuevoPresupuesto = new ModeloPresupuesto
                {
                    Fecha = DateTime.Now,
                    ClienteId = 1, // Cambiar por cliente seleccionado
                    UsuarioId = 1, // Cambiar por usuario actual
                    Total = total,
                    Linea_Presupuestos = new System.Collections.Generic.HashSet<ModeloLineaPresupuesto>(itemsPresupuesto)
                };

                var presupuestoId = cPresupuesto.GuardarPresupuesto(nuevoPresupuesto);
                MessageBox.Show($"Presupuesto #{presupuestoId} creado", "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            dataGridViewPresupuesto.Rows.Clear();
            itemsPresupuesto.Clear();
            total = 0;
            lblTotal.Text = "$0.00";
            CargarProductos();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewPresupuesto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewPresupuesto_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
