using System;
using Seminar2;

namespace Seminar1
{
	public class PrototypePattern 
	{
        public string constMassage = "с уважением ";
        public Massage Massage { get; set; }

		public PrototypePattern(Massage Massage)
		{
            this.Massage = Massage;
		}

        public object Clone()
        {
            Massage.Text += constMassage;
            return Massage;
        }
    }
}

