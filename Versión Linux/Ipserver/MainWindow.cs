using System;
using Gtk;
using System.Net;
using System.IO;

public partial class MainWindow: Gtk.Window
{	
	String ip1 = "Sin ip";
	String host;
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void iponclick (object sender, EventArgs e)
	{
		try
		{
			if (entry1.Text != "")
			{
				String url_address = entry1.Text;
				IPAddress[] addresslist = Dns.GetHostAddresses(url_address);

				foreach (IPAddress thisaddress in addresslist)
				{			
					entry2.Text = thisaddress.ToString();
					ip1 = entry2.Text;
				}
			}
			else
			{	
				entry2.Text = "Introduce un nombre de host";
			}
		}
		catch (System.Net.Sockets.SocketException)
		{
			entry2.Text = "Host desconocido";
		}
	}

	protected void anotaronclick (object sender, EventArgs e)
	{
		if (ip1 != "Sin ip")
		{
			if (label3.LabelProp == "Guardado 1")
			{
				host = entry1.Text;
				label3.LabelProp = host + " - " + ip1;
			}
			else if (label4.LabelProp == "Guardado 2")
			{
				host = entry1.Text;
				label4.LabelProp = host + " - " + ip1;
			}
		}
		else
		{
			entry2.Text = "Host desconocido";
		}
	}

	protected void borraronclick (object sender, EventArgs e)
	{
		label3.LabelProp = "Guardado 1";
		label4.LabelProp = "Guardado 2";
	}
	protected void saveasclick (object sender, EventArgs e)
	{
		String usuario = Environment.UserName;
		string[] lines = {label3.LabelProp, label4.LabelProp};
		File.WriteAllLines(@"/home/"+ Environment.UserName + "/ip-server.txt", lines);
		//Application.Quit ();
	}
	protected void salirclick (object sender, EventArgs e)
	{
		Application.Quit();
	}
}
