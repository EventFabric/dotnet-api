using Newtonsoft.Json;

namespace eventfabric.api
{
	public class Utils
	{
		public static T DeserializeJsonTo<T> (string jsonSerializado)
		{
			try {
                return JsonConvert.DeserializeObject<T>(jsonSerializado);
			} catch {
				return default(T);
			}
		}

		public static string SerializeFromTToJson<T> (T data)
		{
            return JsonConvert.SerializeObject(data);
		}
	}
}
