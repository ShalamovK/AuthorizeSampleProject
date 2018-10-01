using AuthorizeNetSample.Main.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeNetSample
{
	public abstract class Building
	{
		public virtual int GetFloors()
		{
			return 0;
		}
	}

	public class House : Building
	{
		public override int GetFloors()
		{
			return 1;
		}
	}

	public class SkyScraper : House
	{
		public override int GetFloors()
		{
			return 30;
		}
	}

	public class OrdinaryHouse : House
	{
		public new int GetFloors()
		{
			return 5;
		}
	}


	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Press Enter to start");
			Console.ReadLine();

			//List<TestClass> list = new List<TestClass>
			//{
			//	new TestClass
			//	{
			//		Name = "Kirill",
			//		Rate = 4,
			//		Nick = "Keldor"
			//	},
			//	new TestClass
			//	{
			//		Name = "Valya",
			//		Rate = 10,
			//		Nick = "Covalka"
			//	}
			//};

			//var result = list.CustomSelect(t => new { name = t.Nick, level = t.Rate });

			//foreach (var elem in result)
			//{
			//	Console.WriteLine(elem.name + " " + elem.level);
			//}
			Building house1 = new SkyScraper();
			Console.WriteLine(house1.GetFloors());

			House house2 = new SkyScraper();
			Console.WriteLine(house2.GetFloors());

			House house3 = new OrdinaryHouse();
			Console.WriteLine(house3.GetFloors());

			//OrdinaryHouse house4 = new House();
			//Console.WriteLine(house4.GetFloors());

			Console.ReadLine();
		}
	}
}
