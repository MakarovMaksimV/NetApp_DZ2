using System.Net;
using System.Net.Sockets;
using System.Text;
using Seminar2;

namespace Client;

class Program
{
    
    static void Main(string[] args)
    {
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        UdpClient ucl = new UdpClient();
        Console.WriteLine("Введите имя: ");
        string nikName = Console.ReadLine();

        bool work = true;
        while (work)
        {
            try
            {
                Console.WriteLine("Введите сообщение:");
                string massageText = Console.ReadLine();
                if(String.IsNullOrEmpty(massageText) || massageText.ToLower().Contains("отмена"))
                {
                    work = false;
                }
                Massage massage = new Massage(nikName,DateTime.Now,massageText);
                string js = massage.ToJSON();
                byte[] bytes = Encoding.UTF8.GetBytes(js);
                ucl.Send(bytes, iPEndPoint);

                byte[] buffer = ucl.Receive(ref iPEndPoint);
                string text = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(Massage.FromJson(text).ToString());

                Massage proofOfIncom = new Massage(nikName, DateTime.Now, "Сообщение доставлено");
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

