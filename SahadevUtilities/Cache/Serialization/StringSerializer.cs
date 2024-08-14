
using System;
using System.IO;


namespace SahadevUtilities.Cache.Serialization
{
    public class StringSerializer : ISerializer
    {
        public object Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(object value, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
