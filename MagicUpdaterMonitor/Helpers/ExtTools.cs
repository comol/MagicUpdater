using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterMonitor.Helpers
{
	public enum TypesFind
	{
		_bool,
		_int,
		_string,
		_DateTime,
		_unnone
	}

	public static class ExtTools
	{
		public static object ConvertStringToType(string value, Type type)
		{
			if(type == typeof(int))
			{
				return Convert.ToInt32(value);
			}

			if (type == typeof(double))
			{
				return Convert.ToDouble(value);
			}

			if (type == typeof(string))
			{
				return Convert.ToString(value);
			}

			if (type == typeof(bool))
			{
				return Convert.ToBoolean(value.ToLower());
			}

			return null;
		}

		public static string GetRussianTypeDisplayValue(Type type)
		{
			if (type == typeof(int))
			{
				return "целое";
			}

			if (type == typeof(double))
			{
				return "дробное";
			}

			if (type == typeof(string))
			{
				return "строка";
			}

			return null;
		}

		public static bool IsTypeValid(string value, Type type)
		{
			if (type == typeof(int))
			{
				int outVal;
				return int.TryParse(value, out outVal);
			}

			if (type == typeof(double))
			{
				double outVal;
				return double.TryParse(value, out outVal);
			}

			if (type == typeof(string))
			{
				return true;
			}

			return false;
		}

		public static TypesFind FindType(Type type)
		{
			if (type == typeof(bool))
			{
				return TypesFind._bool;
			}
			else if (type == typeof(int))
			{
				return TypesFind._int;
			}
			else if (type == typeof(string))
			{
				return TypesFind._string;
			}
			else if (type == typeof(DateTime))
			{
				return TypesFind._DateTime;
			}

			return TypesFind._unnone;
		}
	}
}
