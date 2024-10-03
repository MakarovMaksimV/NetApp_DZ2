using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Seminar1;
using Seminar2;
namespace Server
{
	public class Send
	{
		public Send()
		{
		}

		public void SendMassageRegistration(Massage msgServer,IPEndPoint iPEndPoint, UdpClient ucl)
		{
			PrototypePattern prototype = new PrototypePattern(msgServer);
            string js = msgServer.ToJSON();
            byte[] bytes = Encoding.UTF8.GetBytes(js);
            ucl.Send(bytes, iPEndPoint);
        }

        public void SendMassageAll(Massage msgServer, Massage msgClient, UdpClient ucl, Dictionary<string, IPEndPoint> clients )
        {
            foreach (var client in clients)
            {
                msgClient.ToName = client.Key;
                string js1 = msgClient.ToJSON();
                byte[] bytes1 = Encoding.UTF8.GetBytes(js1);
                ucl.Send(bytes1, client.Value);
            }
            msgServer = new Massage("Server", DateTime.Now, $"отправлено всем клиентам ");
        }

        public void SendMassageToClient(Massage msgClient, UdpClient ucl, Massage msgServer, IPEndPoint value)
        {
            string js1 = msgClient.ToJSON();
            byte[] bytes1 = Encoding.UTF8.GetBytes(js1);
            ucl.Send(bytes1, value);
            msgClient = new Massage("Server", DateTime.Now, $"отправлено клиенту {msgServer.ToName} ");
        }
    }
}

