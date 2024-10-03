using System;
using System.Net;
using Seminar2;
using System.Net.Sockets;
using System.Text;

namespace Server
{
	public class Server
	{
        static Dictionary<string, IPEndPoint> clients = new Dictionary<string, IPEndPoint>();
        public static bool breakLoop = true;
        Massage msgServer = new Massage();
        Massage? msgClient;
        Send send = new Send();

        public void RunServer()
		{
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            UdpClient ucl = new UdpClient(12345);

            Console.WriteLine("Connecting");

            CancellationTokenSource cts = new CancellationTokenSource();
            Thread thread = new Thread(() =>
            {
                while (!cts.IsCancellationRequested)
                {
                    try
                    {
                        byte[] buffer = ucl.Receive(ref iPEndPoint);
                        Recive recive = new Recive();
                        msgClient = recive.ReciveMassage(buffer);
                        Registration reg = new Registration();
                        Massage serMassage = reg.ClientRegistration(msgClient, iPEndPoint, ref clients);

                        if (msgClient.ToName.ToLower().Equals("server"))
                        {
                            send.SendMassageRegistration(serMassage, iPEndPoint, ucl);
                        }
                        else if (msgClient.ToName.ToLower().Equals("all"))
                        {
                            send.SendMassageAll(msgClient, ucl, clients);
                        }

                        else if (clients.TryGetValue(msgClient.FromName, out IPEndPoint value))
                        {
                            send.SendMassageToClient(msgClient, ucl, value);
                        }
                        else
                        {
                            Console.WriteLine($"пользователь {msgClient.ToName} не найден");
                        }
                        if (msgClient.Text.ToLower().Contains("отмена"))
                        {
                            cts.Cancel();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            });
            thread.Start();
        }

	}
}

