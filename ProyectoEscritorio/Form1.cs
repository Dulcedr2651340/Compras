using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //Lectura de archivos de Entrada(input) y Salida(output)
using ProyectoLibreriadeClases;
using System.Globalization;

namespace ProyectoEscritorio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Clsproceso.cargarImagenes(cboProductos, pictureBox1);
            Util.FormatoListView(listView1);
            txtFecha.Text = Util.getFecha();
            txtHora.Text = Util.getHora();
        }
        
        private void cboProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ruta = string.Empty;
            if (cboProductos.SelectedIndex == 0) //Item => Seleccionar
            {
                ruta = Clsproceso.getRutaImagen() + "Electrodomesticos.jpg";
            }
            else //si seleccionamos un producto
            {
                ruta = Clsproceso.getRutaImagen() + cboProductos.Text + ".jpg";
            }
            pictureBox1.Image = Image.FromFile(ruta);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            txtPrecio.Text = Clsproceso.
                PreciodelProducto(cboProductos.SelectedIndex).ToString();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Clsproceso.AgregarProducto(cboProductos, listView1, txtPrecio,NCantidad);
            verCalculos();
        }
        void verCalculos()
        {
            //DateTimeFormatInfo f = CultureInfo.CurrentCulture.DateTimeFormat;
            NumberFormatInfo fm = new NumberFormatInfo();
            fm.CurrencySymbol = "S/.";
            txtSubTotal.Text = Clsproceso.subtotal.ToString("c", fm);
            txtIgv.Text = Clsproceso.igv.ToString("c", fm);
            txtTotalVenta.Text = Clsproceso.totalventa.ToString("c", fm);
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            Clsproceso.EliminarProducto(listView1);
            verCalculos();
        }

        private void btnCancelarProducto_Click(object sender, EventArgs e)
        {
            Clsproceso.CancelarProducto(listView1);
            verCalculos();
        }
    }
}
