using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoLibreriadeClases
{
    public class Util
    {
        //Metodo para darle formato al control ListView
        public static void FormatoListView(ListView lv)
        {
            lv.View = View.Details; //vista con columnas
            lv.CheckBoxes = true;//para mostrar cuadros de marcacion
            lv.GridLines = true;//para mostrar lineas en el control
            //agregar las columnas
            lv.Columns.Add("Nro", 70, HorizontalAlignment.Left);
            lv.Columns.Add("Producto", 210, HorizontalAlignment.Left);
            lv.Columns.Add("Precio", 80, HorizontalAlignment.Left);
            lv.Columns.Add("Cantidad", 100, HorizontalAlignment.Center);
            lv.Columns.Add("Total", 100, HorizontalAlignment.Left);
        }
        public static string getFecha()
        {
            return DateTime.Now.ToShortDateString();
        }
        public static string getHora()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }
}
