using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Seminar2;
namespace Server
{
	public class Recive
	{
		Massage msgClient = new Massage();

		public Recive()
		{
		}
        public Massage ReciveMassage(byte[] buffer)
        {
			
			string text = Encoding.UTF8.GetString(buffer);
			msgClient = Massage.FromJson(text);

			return msgClient;
		}
	}
}

