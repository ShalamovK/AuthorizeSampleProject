using AuthorizeNetSample.Main.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeNetSample
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Press Enter to start");
			Console.ReadLine();

			List<TestClass> list = new List<TestClass>
			{
				new TestClass
				{
					Name = "Kirill",
					Rate = 4,
					Nick = "Keldor"
				},
				new TestClass
				{
					Name = "Valya",
					Rate = 10,
					Nick = "Covalka"
				}
			};

			var result = list.CustomSelect(t => new { name = t.Nick, level = t.Rate });

			foreach (var elem in result)
			{
				Console.WriteLine(elem.name + " " + elem.level);
			}

			Console.ReadLine();
		}
	}
}
