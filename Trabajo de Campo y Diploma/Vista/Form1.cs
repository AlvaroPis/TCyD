using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabajo_de_Campo_y_Diploma.Modelo;
using Trabajo_de_Campo_y_Diploma.Vista;

namespace Trabajo_de_Campo_y_Diploma
{
    public partial class Form1 : Form
    {

        private static Form1 instancia;
        Modelo.Usuarios usuario = new Modelo.Usuarios();
        Controladora.Seguridad.SesionManager cSesionManager = Controladora.Seguridad.SesionManager.Obtener_instancia();

        public static Form1 Obtener_instancia()
        {
            if (instancia == null)
                instancia = new Form1();

            if (instancia.IsDisposed)
                instancia = new Form1();

            instancia.BringToFront();
            return instancia;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // DEBUG
            Console.WriteLine($"\n=== INTENTO DE LOGIN ===");
            Console.WriteLine($"Usuario ingresado: {user.Text}");

            usuario = cSesionManager.GetUsuario(user.Text);
            if (usuario == null)
            {
                Console.WriteLine("❌ Usuario no encontrado");
                MessageBox.Show("Usuario y/o contraseña incorrecto", "LogIn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Console.WriteLine($"Usuario encontrado: {usuario.nombre} (ID: {usuario.id_usuario})");

            if (usuario != null && usuario.clave == "")
            {
                this.Hide();
                Form form = Vista.Login.Obtener_instancia(usuario);
                form.ShowDialog();
                this.Show();
                return;
            }

            if (usuario.estado == false)
            {
                Console.WriteLine("❌ Usuario inactivo");
                MessageBox.Show("Usuario inactivo contactarse con el administrador", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cSesionManager.LoginUser(user.Text, password.Text))
            {
                Console.WriteLine($"✅ Login exitoso. Creando menú...");

                this.Hide();
                Form form = Vista.Menu.Obtener_instancia(usuario);
                form.ShowDialog();
                this.Show();
                this.Focus();

                user.Text = "";
                password.Text = "";
                user.Focus();
            }
            else
            {
                Console.WriteLine("❌ Contraseña incorrecta");
                MessageBox.Show("Usuario y/o contraseña incorrecto", "LogIn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
