namespace Trabajo_de_Campo_y_Diploma.Vista
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.moduloSeguridad = new System.Windows.Forms.ToolStripMenuItem();
            this.formularioUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.formularioGrupos = new System.Windows.Forms.ToolStripMenuItem();
            this.formularioPermiso = new System.Windows.Forms.ToolStripMenuItem();
            this.moduloVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.formularioGestionarClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.formularioGenerarPresupuesto = new System.Windows.Forms.ToolStripMenuItem();
            this.moduloReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.formularioReportePresupuestos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moduloSeguridad,
            this.moduloVentas,
            this.moduloReportes});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(800, 28);
            this.menuStrip2.TabIndex = 0;
            this.menuStrip2.Text = "menuStrip2";
            this.menuStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip2_ItemClicked);
            // 
            // moduloSeguridad
            // 
            this.moduloSeguridad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formularioUsuarios,
            this.formularioGrupos,
            this.formularioPermiso});
            this.moduloSeguridad.Name = "moduloSeguridad";
            this.moduloSeguridad.Size = new System.Drawing.Size(91, 24);
            this.moduloSeguridad.Text = "Seguridad";
            this.moduloSeguridad.Click += new System.EventHandler(this.moduloSeguridad_Click);
            // 
            // formularioUsuarios
            // 
            this.formularioUsuarios.Name = "formularioUsuarios";
            this.formularioUsuarios.Size = new System.Drawing.Size(224, 26);
            this.formularioUsuarios.Text = "Usuarios";
            this.formularioUsuarios.Click += new System.EventHandler(this.formularioUsuarios_Click);
            // 
            // formularioGrupos
            // 
            this.formularioGrupos.Name = "formularioGrupos";
            this.formularioGrupos.Size = new System.Drawing.Size(224, 26);
            this.formularioGrupos.Text = "Grupos";
            this.formularioGrupos.Click += new System.EventHandler(this.formularioGrupos_Click);
            // 
            // formularioPermiso
            // 
            this.formularioPermiso.Name = "formularioPermiso";
            this.formularioPermiso.Size = new System.Drawing.Size(224, 26);
            this.formularioPermiso.Text = "Permisos";
            this.formularioPermiso.Click += new System.EventHandler(this.formularioPermiso_Click);
            // 
            // moduloVentas
            // 
            this.moduloVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formularioGestionarClientes,
            this.formularioGenerarPresupuesto});
            this.moduloVentas.Name = "moduloVentas";
            this.moduloVentas.Size = new System.Drawing.Size(66, 24);
            this.moduloVentas.Text = "Ventas";
            // 
            // formularioGestionarClientes
            // 
            this.formularioGestionarClientes.Name = "formularioGestionarClientes";
            this.formularioGestionarClientes.Size = new System.Drawing.Size(228, 26);
            this.formularioGestionarClientes.Text = "Gestionar Clientes";
            this.formularioGestionarClientes.Click += new System.EventHandler(this.formularioGestionarClientes_Click);
            // 
            // formularioGenerarPresupuesto
            // 
            this.formularioGenerarPresupuesto.Name = "formularioGenerarPresupuesto";
            this.formularioGenerarPresupuesto.Size = new System.Drawing.Size(228, 26);
            this.formularioGenerarPresupuesto.Text = "Generar Presupuesto";
            this.formularioGenerarPresupuesto.Click += new System.EventHandler(this.generarPresupuestoToolStripMenuItem_Click);
            // 
            // moduloReportes
            // 
            this.moduloReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formularioReportePresupuestos});
            this.moduloReportes.Name = "moduloReportes";
            this.moduloReportes.Size = new System.Drawing.Size(82, 24);
            this.moduloReportes.Text = "Reportes";
            // 
            // formularioReportePresupuestos
            // 
            this.formularioReportePresupuestos.Name = "formularioReportePresupuestos";
            this.formularioReportePresupuestos.Size = new System.Drawing.Size(218, 26);
            this.formularioReportePresupuestos.Text = "Gestionar Reportes";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "Menu";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem moduloSeguridad;
        private System.Windows.Forms.ToolStripMenuItem formularioUsuarios;
        private System.Windows.Forms.ToolStripMenuItem formularioGrupos;
        private System.Windows.Forms.ToolStripMenuItem formularioPermiso;
        private System.Windows.Forms.ToolStripMenuItem moduloVentas;
        private System.Windows.Forms.ToolStripMenuItem formularioGestionarClientes;
        private System.Windows.Forms.ToolStripMenuItem moduloReportes;
        private System.Windows.Forms.ToolStripMenuItem formularioReportePresupuestos;
        private System.Windows.Forms.ToolStripMenuItem formularioGenerarPresupuesto;
    }
}