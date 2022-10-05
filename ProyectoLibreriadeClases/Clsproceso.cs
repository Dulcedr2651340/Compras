using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace ProyectoLibreriadeClases
{
    public class Clsproceso
    {
        public static string getRutaImagen()
        {
            //recuperando la posicion de la carpeta bin
            int indice = Application.StartupPath.IndexOf("bin");
            return Application.StartupPath.Substring(0,
                   Application.StartupPath.Length -
                   (Application.StartupPath.Length - indice)) + @"fotos\";
        }
        public static void cargarImagenes(ComboBox cboProductos, PictureBox pictureBox1)
        {
            string[] rutadeArchivos = Directory.GetFiles(getRutaImagen());
            cboProductos.Items.Add("--Seleccionar--");
            //Leer el contenido del arreglo
            for (int i = 0; i < rutadeArchivos.Length; i++)
            {
                //recuperar el nombre del archivo sin la extension
                string nombre = Path.GetFileNameWithoutExtension(rutadeArchivos[i]);
                if (nombre != "Electrodomesticos" && nombre != "Thumbs")
                {
                    cboProductos.Items.Add(nombre);
                }
            }
            cboProductos.SelectedIndex = 0;
            pictureBox1.Image = Image.FromFile(rutadeArchivos[1]);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public static double PreciodelProducto(int indice)
        {
            double[] precios = new double[] { 0, 2500, 1849.99, 350, 79.99, 3500 };
            return precios[indice];
        }
        static int n = 0;
        public static void AgregarProducto(ComboBox cboproductos, ListView lv, TextBox txtprecio,
            NumericUpDown npcantidad)
        {
            if (cboproductos.SelectedIndex == 0) //si no se ha seleccionado un producto
            {
                MessageBox.Show("No ha seleccionado un Producto");
            } else if (lv.Items.Count > 0)//Preguntando si hay productos en el control ListView
            { //recorriendo el control listview Items por items
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    if (lv.Items[i].SubItems[1].Text == cboproductos.Text)
                    {
                        if (MessageBox.Show("Producto Existente", "Desea Actualizar la cantidad del Producto",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            lv.Items[i].SubItems[3].Text = npcantidad.Value.ToString();
                            double precio = double.Parse(lv.Items[i].SubItems[2].Text);
                            lv.Items[i].SubItems[4].Text = ((int)npcantidad.Value * precio).ToString();
                            CalcularTotales(lv);
                            return; //Salir del metodo
                        }
                        else { return; }
                    }
                }
            }
            n++; //incrementando la variable n
            //Cargando los items en el control ListView
            ListViewItem lvitem = new ListViewItem();
            lvitem = lv.Items.Add(n.ToString());
            lvitem.SubItems.Add(cboproductos.Text);
            lvitem.SubItems.Add(txtprecio.Text);
            lvitem.SubItems.Add(npcantidad.Value.ToString());
            lvitem.SubItems.Add((double.Parse(txtprecio.Text) *
                            Convert.ToDouble(npcantidad.Value)).ToString());
            CalcularTotales(lv);
        }
        public static double subtotal = 0;
        public static double igv = 0;
        public static double totalventa = 0;
        static void CalcularTotales(ListView lv)
        {
            subtotal = 0;
            for (int i = 0; i < lv.Items.Count; i++)
            {
                subtotal += Convert.ToDouble(lv.Items[i].SubItems[4].Text);
            }
            igv = subtotal * 0.18;
            totalventa = subtotal + igv;
        }
        public static void CancelarProducto(ListView lv) 
        {
            lv.Items.Clear();
            CalcularTotales(lv);
            n = 0;
        }
        public static void EliminarProducto(ListView lv) {
            if (lv.CheckedItems.Count == 0)
            {
                MessageBox.Show("No ha seleccionado ningun producto para eliminarlo");
            }
            else
            {
                int i = 0;
                while (i < lv.Items.Count)
                {
                    if (lv.Items[i].Checked)//si el items esta marcado o seleccionado
                        lv.Items.RemoveAt(i); //elimina el items seleccionado
                    else { i++; }
                }
            }
            for (int i = 0; i < lv.Items.Count; i++)
            {
                lv.Items[i].Text = (i + 1).ToString();
            }
            CalcularTotales(lv);
        }               
    }
}
