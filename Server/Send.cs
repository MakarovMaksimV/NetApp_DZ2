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

        public void SendMassageAll(Massage msgClient, UdpClient ucl, Dictionary<string, IPEndPoint> clients )
        {
            
            foreach (var client in clients)
            {
                msgClient.ToName = client.Key;
                msgClient = new Massage(msgClient.FromName, DateTime.Now, $": {msgClient.Text},отправлено всем клиентам");
                string js = msgClient.ToJSON();
                byte[] bytes = Encoding.UTF8.GetBytes(js);
                ucl.Send(bytes, client.Value);
                Console.WriteLine("Клиенту" + msgClient.ToName.ToString());
            }
            
        }

        public void SendMassageToClient(Massage msgClient, UdpClient ucl, IPEndPoint value)
        {
            msgClient = new Massage(msgClient.FromName, DateTime.Now, $": {msgClient.Text}, отправлено клиенту {msgClient.ToName} ");
            string js = msgClient.ToJSON();
            byte[] bytes = Encoding.UTF8.GetBytes(js);
            ucl.Send(bytes, value);
        }
    }
}

