﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryOrtizAccionDeInventario
{
    public partial class FrmAgregar : Form
    {
        public FrmAgregar()
        {
            InitializeComponent();
        }
        ClsConexionBD objConectarBD = new ClsConexionBD();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmAgregar_Load(object sender, EventArgs e)
        {
                
            objConectarBD.ConectarBD();
            objConectarBD.Mostrar(dgvDato);

            DataTable categorias = objConectarBD.ObtenerCategorias();

            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nombre";  // Lo que se muestra
            cmbCategoria.ValueMember = "Id";        // El valor real (por ejemplo, para guardar en DB)
            cmbCategoria.SelectedIndex = -1;        // Opcional, para que no haya nada seleccionado al inicio
            int idCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);

            btnAceptar.Enabled = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            decimal precio = Convert.ToInt32(txtPrecio.Text);
            int stock = Convert.ToInt32(txtStock.Text);
            string categoriaId = cmbCategoria.SelectedValue.ToString();

            objConectarBD.InsertarContacto(
                nombre,
                descripcion,
                precio,
                stock,
                categoriaId
            );

            dgvDato.DataSource = objConectarBD.ObtenerDatosTabla("Contactos");
        }
        private void VerificarCampos()
        {
            bool camposValidos =
                !string.IsNullOrWhiteSpace(txtNombre.Text) &&
                !string.IsNullOrWhiteSpace(txtDescripcion.Text) &&
                decimal.TryParse(txtPrecio.Text, out _) &&
                int.TryParse(txtStock.Text, out _) &&
                cmbCategoria.SelectedIndex >= 0;

            btnAceptar.Enabled = camposValidos;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            VerificarCampos();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            VerificarCampos();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            VerificarCampos();
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            VerificarCampos();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificarCampos();
        }
    }
}
