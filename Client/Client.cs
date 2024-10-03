using System;
using Seminar2;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
	public class Client
	{
		public Client()
		{
		}

		public void RunClient()
		{
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
            UdpClient ucl = new UdpClient();

            bool work = true;
            while (work)
            {
                try
                {
                    Console.WriteLine("Введите имя: ");
                    string nikName = Console.ReadLine();
                    Console.WriteLine("Введите получателя");
                    string toName = Console.ReadLine();
                    if (String.IsNullOrEmpty(toName))
                    {
                        Console.WriteLine("Вы не ввели имя пользователя");
                        continue;
                    }
                    Console.WriteLine("Введите сообщение:");
                    string massageText = Console.ReadLine();

                    if (String.IsNullOrEmpty(massageText) || massageText.ToLower().Contains("отмена"))
                    {
                        work = false;
                    }

                    Massage massage = new Massage(nikName, DateTime.Now, massageText);
                    massage.ToName = toName;
                    string js = massage.ToJSON();
                    byte[] bytes = Encoding.UTF8.GetBytes(js);
                    ucl.Send(bytes, iPEndPoint);

                    byte[] buffer = ucl.Receive(ref iPEndPoint);
                    string text = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(Massage.FromJson(text).ToString());

                    Massage proofOfIncom = new Massage(nikName, DateTime.Now, "Сообщение доставлено ");
                    string js1 = proofOfIncom.ToJSON();
                    byte[] bytes1 = Encoding.UTF8.GetBytes(js1);
                    ucl.Send(bytes1, iPEndPoint);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            ucl.Close();
        }
	}
}

