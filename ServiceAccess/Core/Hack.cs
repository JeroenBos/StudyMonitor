using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBSnorro
{
	/// <summary> This file contains memebers that I usually have in a neat form, but I didn't want to include all those .cs files. </summary>
	static class Extensions
	{
		/// <summary> Gets the (boxed) default instance of the specified type. </summary>
		public static object GetDefault(this Type type)
		{
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}
	}
	class Contract
	{
		public static void Requires(bool requirement, string message = "Requirement not met")
		{
			if (!requirement)
			{
				throw new Exception(message);
			}
		}
		public static void Assert(bool requirement, string message = "Requirement not met")
		{
			if (!requirement)
			{
				throw new Exception(message);
			}
		}
	}
}
