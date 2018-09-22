using System;
using System.Collections.Generic;

namespace AuthorizeNetSample.Main.Test
{
	public static class Extensions
	{
		public static IEnumerable<Target> CustomSelect<Source, Target>(this IEnumerable<Source> target, Func<Source, Target> selector)
		{
			foreach (Source element in target)
			{
				yield return selector(element);
			}
		}
	}
}
