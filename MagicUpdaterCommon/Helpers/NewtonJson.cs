using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace MagicUpdaterCommon.Helpers
{
	public static class NewtonJson
	{
		public static T ReadJsonFile<T>(string filename) where T : new()
		{
			if (!File.Exists(filename))
				return new T();

			return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename));
		}

		public static void WriteJsonFile<T>(T data, string filename)
		{
			File.WriteAllText(filename, JsonConvert.SerializeObject(data, Formatting.Indented));
		}

		public static T ReadJsonString<T>(string _string)
		{
			if (_string.Length > 0)
			{
				return JsonConvert.DeserializeObject<T>(_string);
			}

			return default(T);
		}

		public static string GetJsonFromModel(object model)
		{
			if (model != null)
			{
				return JsonConvert.SerializeObject(model);
			}

			return null;
		}

		public static object GetModelFromJson(string json)
		{
			if (!string.IsNullOrEmpty(json))
			{
				return JsonConvert.DeserializeObject(json);
			}
			else
			{
				return null;
			}
		}
	}
}
