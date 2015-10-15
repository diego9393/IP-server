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

namespace Ip_server
{
    public partial class Form1 : Form
    {
        String host = "Sin host";
        String ip1 = "Sin ip";
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
            }
            else
            {
                MessageBox.Show(
                    "Necesitas poner un nombre de host",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error //For Info Asterisk
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
                    IPAddress[] addresslist = Dns.GetHostAddresses(url_address);

                    foreach (IPAddress thisaddress in addresslist)
                    {
                        label1.Text = thisaddress.ToString();
                        ip1 = label1.Text;
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
                "Desarrollador: https://vivarsoft.es \nVersión: 1.0.1",
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
        }
    }
}
