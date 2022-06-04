using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryCompilePepe;
using System.IO;

namespace SuperPepe
{
    public partial class Form1 : Form
    {
        string origen, destino;
        PCC compilador;
        public Form1()
        {
            InitializeComponent();
            compilador = new PCC();
        }

        private void button1_Click(object sender, EventArgs e)
        {//Abrir
            DialogResult re = openFileDialog1.ShowDialog();
            if (re == DialogResult.OK)
            {
                origen = openFileDialog1.FileName; // "C:\progs\programitA.pp"
                compilador.recibeTransformaArchivo(origen);
                lblCodigo.Text = origen;

                //cargando en el rich text box: tctContenido
                txtContenido.Text = File.ReadAllText(origen);
                colorear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            compilador.compilar();
            destino = $"{Path.GetDirectoryName(origen)}\\{Path.GetFileNameWithoutExtension(origen)}.slm";
            if (File.Exists(destino))
                File.Delete(destino);
            compilador.exportarSLM(destino);
            lblCompilado.Text = destino;
            compilador = null;
            compilador = new PCC();
            //cargando en richTectBox el compilado
            txtSLM.Text = File.ReadAllText(destino);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //"C:\miCarpeta\app.exe C:\Desktop\prog.slm"
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.Arguments = destino;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            psi.FileName = @"f:\tutoriales\.Net\clases\Examen\Entregar\EjecutorSuperPepe\EjecutorSuperPepe\bin\Debug\netcoreapp3.1\EjecutorSuperPepe.exe";
            System.Diagnostics.Process.Start(psi);
        }
        private void colorear()
        {
            //La palabra a identificar: input
            string palabra = "input";
            List<int> indices = new List<int>();
            for (int i = 0; i < txtContenido.Text.Length; i++)
                if (txtContenido.Text.IndexOf(palabra, i) != -1)
                    indices.Add(txtContenido.Text.IndexOf(palabra, i));
            for (int i = 0; i < indices.Count; i++)
            {
                txtContenido.Select(indices.ElementAt(i), palabra.Length);
                txtContenido.SelectionColor = Color.Blue;
                indices.RemoveAt(i);
            }
            txtContenido.Select(txtContenido.Text.Length, 0);
            txtContenido.SelectionColor = Color.Black;
        }
    }
}
