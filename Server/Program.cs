using System.Net;
using System.Net.Sockets;
using System.Text;
using Seminar1;

namespace Server;

class Program
{
    static void Main(string[] args)
    {
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        UdpClient ucl = new UdpClient(12345);
        Console.WriteLine("Connecting");

        while(true)
        {
            try
            {
                byte[] buffer = ucl.Receive(ref iPEndPoint);
                string text = Encoding.UTF8.GetString(buffer);
                Console.WriteLine($"Имя: {Massage.FromJson(text).Name}" +
                    $"\nДата получения: {Massage.FromJson(text).Date}" +
                    $"\nСообщение: {Massage.FromJson(text).Text}");
                Console.WriteLine();

                Massage massage = new Massage("Server", DateTime.Now, "Сообщение доставлено");
                string js = massage.ToJSON();
                byte[] bytes = Encoding.UTF8.GetBytes(js);
                ucl.Send(bytes, iPEndPoint);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}