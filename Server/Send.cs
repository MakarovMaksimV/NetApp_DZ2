using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Seminar2;
namespace Server
{
	public class Send
	{
		public Send()
		{
		}

		public void SendMassage(Massage msgServer,IPEndPoint iPEndPoint, UdpClient ucl)
		{
            string js = msgServer.ToJSON();
            byte[] bytes = Encoding.UTF8.GetBytes(js);
            ucl.Send(bytes, iPEndPoint);
        }

	}
}

