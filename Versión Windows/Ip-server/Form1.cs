using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Ip_server
{
    public partial class Form1 : Form
    {
        String host = "Sin host";
        String ip1 = "Sin ip";
        int lista = 0;
        string listaTexto;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ip1 != "Sin ip")
            {
                if (label5.Text == "Guardado 1")
                {
                    host = textBox1.Text;
                    label5.Text = host + " - " + ip1;
                }
                else if (label6.Text == "Guardado 2")
                {
                    host = textBox1.Text;
                    label6.Text = host + " - " + ip1;
                }
                else if (label12.Text == "Guardado 3")
                {
                    host = textBox1.Text;
                    label12.Text = host + " - " + ip1;
                }
            }
            else
            {
                MessageBox.Show(
                    "Necesitas poner un nombre de host",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    String url_address = textBox1.Text;

                    bool inicionossl = url_address.StartsWith("http://");
                    bool iniciosissl = url_address.StartsWith("https://");
                    bool finalurl = url_address.EndsWith("/");

                    if (inicionossl == true)
                    {
                        url_address = url_address.Replace("http://", "");
                    }
                    else if (iniciosissl == true)
                    {
                        url_address = url_address.Replace("https://", "");
                    }

                    if (finalurl == true)
                    {
                        url_address = url_address.Replace("/", "");
                    }

                    IPAddress[] addresslist = Dns.GetHostAddresses(url_address);

                    foreach (IPAddress thisaddress in addresslist)
                    {
                        textBox2.Enabled = true;
                        textBox2.Text = thisaddress.ToString();
                        ip1 = textBox2.Text;             
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Necesitas poner un nombre de host",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show(
                    "Host desconocido",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vivarsoft.es");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Desarrollador: https://vivarsoft.es \nVersión: 2.0",
                "Acerca de",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            save.FileName = "ip.txt";

            save.Filter = "Text File | *.txt";

            if (save.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(save.OpenFile());
                writer.WriteLine(label5.Text);
                writer.WriteLine(label6.Text);
                writer.WriteLine(label12.Text);
                writer.Dispose();
                writer.Close();

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label5.Text = "Guardado 1";
            label6.Text = "Guardado 2";
            label12.Text = "Guardado 3";
        }


        //posicion palabras clave en google
        private void button4_Click(object sender, EventArgs e)
        {
            string strUrl = textBox3.Text;
            string keywords = textBox4.Text;
            bool iniciohttp = strUrl.StartsWith("http://");
            bool iniciossl = strUrl.StartsWith("https://");

            if (iniciohttp == false && iniciossl == false)
            {
                strUrl = "http://" + strUrl;
            }

            try {
            Uri url = new Uri(strUrl);
            int position = GetPosition(url, keywords);
            string posicionTexto = position.ToString();
           
                if (position.ToString() == "0")
                {
                    posicionTexto = ">100";
                }
                listaTexto = lista.ToString();

                textBox5.Text = posicionTexto;
                lista++;

                listBox1.Items.Add(listaTexto + ") " + strUrl);
                listBox2.Items.Add(listaTexto + ") " + keywords);
                listBox3.Items.Add(listaTexto + ") " + posicionTexto);
                textBox5.Enabled = true;

            }
            catch(UriFormatException)
            {
                MessageBox.Show(
                   "Introduce una url",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );
            }
            catch
            {
                MessageBox.Show(
                   "Error desconocido",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );
            }
        }

        public static int GetPosition(Uri url, string searchTerm)
        {
            string raw = "http://www.google.es/search?num=100&q="+ searchTerm +"&btnG=Search";

            string search = string.Format(raw, HttpUtility.UrlEncode(searchTerm));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(search);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
                {
                    string html = reader.ReadToEnd();
                    return FindPosition(html, url);
                }
            }
        }

        private static int FindPosition(string html, Uri url)
        {
            string lookup = "(<h3 class=\"r\"><a href=\"/url\\?q=)(\\w+[a-zA-Z0-9.\\-?=/:]*)";


            MatchCollection matches = Regex.Matches(html, lookup);

            for (int i = 0; i < matches.Count; i++)
            {
                string match = matches[i].Groups[2].Value;
                if (match.Contains(url.Host))
                    return i + 1;
            }

            return 0;

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            lista = 0;
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string direccion;
            string palabra;
            string posicion;
            SaveFileDialog save = new SaveFileDialog();

            save.FileName = "export.csv";

            save.Filter = "Text File | *.csv";

            if (save.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(save.OpenFile());

                for (int i = 0; i < lista; i++)
                {
                    direccion = listBox1.Items[i].ToString();
                    palabra = listBox2.Items[i].ToString();
                    posicion = listBox3.Items[i].ToString();

                    if (direccion.StartsWith(i + ") ") == true)
                    {
                        direccion = direccion.Replace(i + ") ", "");
                    }
                    if (palabra.StartsWith(i + ") ") == true)
                    {
                        palabra = palabra.Replace(i + ") ", "");
                    }
                    if (posicion.StartsWith(i + ") ") == true)
                    {
                        posicion = posicion.Replace(i + ") ", "");
                    }

                    writer.WriteLine(direccion + "," + palabra + "," + posicion);
                }
                writer.Dispose();
                writer.Close();

            }
        }
    }
}
