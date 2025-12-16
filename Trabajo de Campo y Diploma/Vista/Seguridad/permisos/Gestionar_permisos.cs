using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_de_Campo_y_Diploma.Vista.Seguridad.permisos
{
    public partial class Gestionar_permisos : Form
    {
        private static Gestionar_permisos instancia;
        Controladora.Seguridad.Permiso cPermisos = Controladora.Seguridad.Permiso.Obtener_instancia();
        private List<Modelo.Permisos> Permisos;
        private List<Modelo.Permisos> PermisosFiltrados;
        private Modelo.Permisos Usuario;
        private int id_permiso;
        private int index;
        Controladora.Seguridad_composite.PermisoGrupo cPermisoGrupo = Controladora.Seguridad_composite.PermisoGrupo.Obtener_instancia();

        public static Gestionar_permisos Obtener_instancia()
        {
            if (instancia == null)
            {
                instancia = new Gestionar_permisos();
            }
            if (instancia.IsDisposed)
            {
                instancia = new Gestionar_permisos();
            }
            instancia.BringToFront();
            return instancia;
        }
        private Gestionar_permisos()
        {
            InitializeComponent();
            Permisos = cPermisos.getPermisos();
            ConfigurarPermisosBotones();
            filtrar();
            comboBoxfiltro.Items.Add("Nombre");
            comboBoxfiltro.SelectedIndex = 0;
            buttonEliminar.Enabled = false;
            buttonModificar.Enabled = false;
            checkBoxSoloHabilitados.Checked = true;
            dataUsuarios.Columns[0].Visible = false;
            dataUsuarios.Columns[2].Visible = false;
            dataUsuarios.Columns[4].Visible = false;
            dataUsuarios.Columns[5].Visible = false;
            dataUsuarios.Columns[6].Visible = false;
        }

        private void ConfigurarPermisosBotones()
        {
            buttonAgregar.Visible = true;
            buttonModificar.Visible = true;
            buttonEliminar.Visible = true;
            //buttonAgregar.Visible = cPermisoGrupo.valiPermiso("Agregar permiso");
            //buttonModificar.Visible = cPermisoGrupo.valiPermiso("Modificar permiso");
            //buttonEliminar.Visible = cPermisoGrupo.valiPermiso("Eliminar permiso");
        }

        public void filtrar()
        {
            try
            {
                // 1. Obtener todos los permisos sin filtrar
                var todosPermisos = cPermisos.getPermisos();

                // Debug: Mostrar conteo en consola
                Debug.WriteLine($"Permisos obtenidos: {todosPermisos?.Count ?? 0}");

                // 2. Aplicar filtros solo si es necesario
                IEnumerable<Modelo.Permisos> resultados = todosPermisos;

                // Filtro por estado (si el checkbox está marcado)
                if (checkBoxSoloHabilitados.Checked)
                {
                    resultados = resultados.Where(p => p.estado == true || p.estado == null);
                    Debug.WriteLine($"Después de filtrar por estado: {resultados.Count()}");
                }

                // Filtro por texto (si hay búsqueda)
                if (!string.IsNullOrEmpty(textBoxNombre.Text))
                {
                    string filtro = textBoxNombre.Text.ToLower();
                    resultados = resultados.Where(p =>
                        p.nombre_permiso != null &&
                        p.nombre_permiso.ToLower().Contains(filtro));
                    Debug.WriteLine($"Después de filtrar por texto: {resultados.Count()}");
                }

                // 3. Convertir a lista y asignar
                PermisosFiltrados = resultados.ToList();
                dataUsuarios.DataSource = PermisosFiltrados;

                // 4. Debug adicional
                foreach (var p in PermisosFiltrados.Take(5))
                {
                    Debug.WriteLine($"Muestra: {p.id_permiso} - {p.nombre_permiso}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}");
                Debug.WriteLine($"ERROR: {ex.ToString()}");
            }
        }
        private void EjecutarDiagnosticoCompleto()
        {
            // 1. Verificar conexión directa a la base de datos
            var sb = new StringBuilder();
            var contexto = Modelo.Contexto.Obtener_instancia();

            try
            {
                // A. Conteo total de permisos
                var totalBD = contexto.Permisos.Count();
                sb.AppendLine($"Total de permisos en BD: {totalBD}");

                // B. Mostrar permisos "perdidos" (30-36)
                var permisosPerdidos = contexto.Permisos
                    .Where(p => p.id_permiso >= 30 && p.id_permiso <= 36)
                    .ToList();

                sb.AppendLine("\nPermisos especiales (30-36):");
                foreach (var p in permisosPerdidos)
                {
                    sb.AppendLine($"{p.id_permiso} | {p.nombre_permiso} | {p.estado} | {p.id_formulario}");
                }

                // C. Verificar configuración del DataGridView
                sb.AppendLine("\nConfiguración DataGridView:");
                sb.AppendLine($"- Columnas: {dataUsuarios.Columns.Count}");
                sb.AppendLine($"- Origen datos: {(dataUsuarios.DataSource == null ? "NULL" : "ASIGNADO")}");

                // Mostrar resultados
                MessageBox.Show(sb.ToString(), "Resultados Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // D. Alternativa: Mostrar en consola de depuración
                Debug.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en diagnóstico: {ex.Message}");
            }
        }

        private void Gestionar_permisos_Load(object sender, EventArgs e)
        {

        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            Form ventas = detalle_permisos.Obtener_instancia(0);
            ventas.Show();
            Permisos = cPermisos.getPermisos();
            filtrar();
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            Form form = detalle_permisos.Obtener_instancia(id_permiso);
            form.Show();
            filtrar();
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar el permiso seleccionado?", "Eliminar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Obtener el permiso (sin crear una nueva instancia innecesaria)
                Modelo.Permisos Permiso = Permisos.FirstOrDefault(p => p.id_permiso == id_permiso);

                // Validar si el permiso existe
                if (Permiso == null)
                {
                    MessageBox.Show("El permiso no existe o ya fue eliminado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Marcar como inactivo y guardar
                Permiso.estado = false;
                cPermisos.eliminarPermiso(Permiso); // Asegúrate de que este método actualice en BD
                filtrar();

                MessageBox.Show("Permiso deshabilitado correctamente.");
            }
        }

        private void dataUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            if (index != -1)
            {
                buttonAgregar.Enabled = true;
                buttonEliminar.Enabled = true;
                buttonModificar.Enabled = true;
                id_permiso = Convert.ToInt32(dataUsuarios.Rows[index].Cells[0].Value);
            }
            else
            {
                buttonAgregar.Enabled = false;
                buttonEliminar.Enabled = false;
                buttonModificar.Enabled = false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            textBoxNombre.Text = "";
            filtrar();
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void checkBoxSoloHabilitados_CheckedChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EjecutarDiagnosticoCompleto();
        }
    }
}
