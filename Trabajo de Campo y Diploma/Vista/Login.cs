using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_de_Campo_y_Diploma.Vista
{
    public partial class Login : Form
    {
        private static Login instancia;
        Modelo.Usuarios usuario = new Modelo.Usuarios();
        Controladora.Usuario cUsuario = Controladora.Usuario.Obtener_instancia();

        public static Login Obtener_instancia(Modelo.Usuarios usuario)
        {
            if (instancia == null)
                instancia = new Login(usuario);

            if (instancia.IsDisposed)
                instancia = new Login(usuario);

            instancia.BringToFront();
            return instancia;
        }
        private Login(Modelo.Usuarios usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPass1.Text == txtPass2.Text)
            {
                usuario.clave = Comun.MetodosComunes.EncriptarPassBD(txtPass2.Text);
                cUsuario.modificarUsuario(usuario);
                MessageBox.Show("Contraseña guardada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
