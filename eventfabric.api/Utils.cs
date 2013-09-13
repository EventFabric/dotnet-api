using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace eventfabric.api
{
	public class Utils
	{
		public static T DeserializeJsonTo<T> (string jsonSerializado)
		{
			try {
				var obj = Activator.CreateInstance<T> ();
				var ms = new MemoryStream (Encoding.Unicode.GetBytes (jsonSerializado));
				var serializer = new DataContractJsonSerializer (obj.GetType ());
				obj = (T)serializer.ReadObject (ms);

				ms.Close ();
				ms.Dispose ();

				return obj;
			} catch {
				return default(T);
			}
		}

		public static string SerializeFromTToJson<T> (T data)
		{
			try {
				var ms = new MemoryStream ();
				var ser = new DataContractJsonSerializer (data.GetType ());
				ser.WriteObject (ms, data);
				var json = Encoding.Default.GetString (ms.ToArray ());
				return json;
			} catch {
				return string.Empty;
			}
		}
	}
}
