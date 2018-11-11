using System;
using System.ComponentModel;
using System.Reflection;

namespace AuthorizeNetSample.Domain.Helpers
{
	public static class EnumHelper
	{
		public static string GetDescription<T>(this T Enum) where T : IConvertible
		{
			Type type = Enum.GetType();
			MemberInfo[] info = type.GetMember(Enum.ToString());

			if (info != null && info.Length > 0)
			{
				object[] attributes = (object[])info[0].GetCustomAttributes(typeof(DescriptionAttribute));

				if (attributes != null && attributes.Length > 0)
				{
					return ((DescriptionAttribute)attributes[0]).Description;
				}
			}

			return type.ToString();
		}
	}
}
