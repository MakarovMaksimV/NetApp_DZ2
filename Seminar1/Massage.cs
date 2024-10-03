using System.Net;
using System.Text.Json;
namespace Seminar2
{
	public class Massage
	{
        public string FromName { get; set; }
        public string ToName { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public Massage(string FromName, DateTime Date, string Text)
        {
            this.FromName = FromName;
            this.Date = Date;
            this.Text = Text;
        }

        public Massage(string FromName, DateTime Date, string Text, string ToName)
        {
            this.FromName = FromName;
            this.Date = Date;
            this.Text = Text;
            this.ToName = ToName;
        }

        public Massage()
        {
        }

        public string ToJSON()
		{
			return JsonSerializer.Serialize(this);
		}


        public static Massage? FromJson(string massage)
        {
            return JsonSerializer.Deserialize<Massage>(massage);
        }

        public override string ToString()
        {
            return $"Получено от: {FromName} " +
                $"\nДата получения: {Date} " +
                $"\nСообщение: {Text}\n";
        }
        
    }
}

