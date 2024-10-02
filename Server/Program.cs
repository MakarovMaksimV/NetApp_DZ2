using System.Net;
using System.Net.Sockets;
using System.Text;
using Seminar2;
using Xamarin.Forms;


namespace Server;

class Program
{
    public static bool breakLoop = true;
    static void Main(string[] args)
    {
        
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        UdpClient ucl = new UdpClient(12345);
        Console.WriteLine("Connecting");

        CancellationTokenSource cts = new CancellationTokenSource();
        while (!cts.IsCancellationRequested)
            {
            try
                {
                byte[] buffer = ucl.Receive(ref iPEndPoint);
                string text = Encoding.UTF8.GetString(buffer);

                Thread thread = new Thread(() =>
                {
                    Massage? msgClient = Massage.FromJson(text);

                    if (msgClient.Text.ToLower().Contains("отмена"))
                    {
                        cts.Cancel();
                    }
                        Console.WriteLine(Massage.FromJson(text).ToString());
                        Console.WriteLine();

                        Massage msgServer = new Massage("Server", DateTime.Now, "Сообщение доставлено");
                        string js = msgServer.ToJSON();
                        byte[] bytes = Encoding.UTF8.GetBytes(js);
                        ucl.Send(bytes, iPEndPoint);
                });
                thread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}