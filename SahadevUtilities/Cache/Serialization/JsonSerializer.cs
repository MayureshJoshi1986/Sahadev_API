﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace SahadevUtilities.Cache.Serialization
{
    public class JsonSerializer : ISerializer
    {
        private readonly Encoding encoding;

        /// <summary>
        /// Including object type in the final serialized value.
        /// By setting this value to true the untyped Get() methods will be worked but it makes serialized value bigger.
        /// </summary>
        public bool SerializeTypeName { get; private set; }
     
        public JsonSerializer(Encoding encoding = null, bool serializeTypeName = false)
        {
            if (encoding != null)
                this.encoding = encoding;
            else
                this.encoding = Encoding.Default;

            SerializeTypeName = serializeTypeName;
        }

        public void Serialize(object value, Stream stream)
        {
            var settings = new JsonSerializerSettings()
            {                
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
            };
            settings.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());

            using (StreamWriter sw = new StreamWriter(stream, encoding))
            {
                if (SerializeTypeName)
                    sw.WriteLine(value.GetType().AssemblyQualifiedName);

                sw.Write(JsonConvert.SerializeObject(value, settings));
            }
        }

        public object Deserialize(Stream stream)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };

            using (StreamReader sr = new StreamReader(stream, encoding))
            {
                if (SerializeTypeName)
                {
                    string className = sr.ReadLine();
                    Type objectType = Util.GetType(className);

                    return JsonConvert.DeserializeObject(sr.ReadToEnd(), objectType, settings);
                }
                else
                    return JsonConvert.DeserializeObject(sr.ReadToEnd(), settings);
            }
        }

        public T Deserialize<T>(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream, encoding))
            {
                if (SerializeTypeName)
                {
                    string className = sr.ReadLine();
                    //Type objectType = GetType(className);
                }

                return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
            }
        }
       
    }
}
