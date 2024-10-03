using Seminar2;
using System.Net;
using System.Text;

namespace Server
{
	public class Registration
	{
        Massage serMassage = new Massage();

        public Registration()
		{
		}

		public Massage ClientRegistration(Massage msgClient, IPEndPoint iPEndPoint, Dictionary<string,IPEndPoint> clients)
		{
            if (msgClient.Text.ToLower().Equals("reg"))
            {
                if (clients.TryAdd(msgClient.FromName, iPEndPoint))
                {
                    serMassage = new Massage("Server", DateTime.Now, $"добавлен пользователь: {msgClient.FromName} ");
                }
            }
            else if (msgClient.Text.ToLower().Equals("delete"))
            {
                clients.Remove(msgClient.FromName);
                serMassage = new Massage("Server", DateTime.Now, $"удалён пользователь: {msgClient.FromName} ");
            }
            else if (msgClient.Text.ToLower().Equals("list"))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var client in clients)
                {
                    sb.Append(client.Key + "\n");
                    serMassage = new Massage("Server", DateTime.Now, $"список клиентов:\n {sb}");
                }
            }
            return serMassage;
        }
	}
}

