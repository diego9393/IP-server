using System;
using Gtk;
using System.Net;

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
					label1.LabelProp = thisaddress.ToString();
					ip1 = label1.LabelProp;
				}
			}
			else
			{	
				label1.LabelProp = "Introduce un nombre de host";
			}
		}
		catch (System.Net.Sockets.SocketException)
		{
			label1.LabelProp = "Host desconocido";
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
			label1.LabelProp = "Host desconocido";
		}
	}

	protected void borraronclick (object sender, EventArgs e)
	{
		label3.LabelProp = "Guardado 1";
		label4.LabelProp = "Guardado 2";
	}
}
