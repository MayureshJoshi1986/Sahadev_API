﻿using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SahadevUtilities.Cache.Serialization
{
    public class BinarySerializer : ISerializer
    {
        private readonly IFormatter serializer;

        public BinarySerializer()
        {
            serializer = new BinaryFormatter();
        }
        
        public void Serialize(object value, Stream stream)
        {
            serializer.Serialize(stream, value);
        }
                
        public object Deserialize(Stream stream)
        {
            return serializer.Deserialize(stream);
        }

        public T Deserialize<T>(Stream stream)
        {
            return (T)serializer.Deserialize(stream);
        }

        
    }
}
