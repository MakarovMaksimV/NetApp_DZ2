using System.Text.Json;
namespace Seminar1
{
	public class Massage
	{
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public Massage(string Name, DateTime Date, string Text)
		{
            this.Name = Name;
            this.Date = Date;
            this.Text = Text;
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
    }
}

