using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SahadevUtilities.Cache.Serialization
{
    public interface ISerializer
    {
        //string Serialize(object value);

        //object Deserialize(string value);
        //T Deserialize<T>(string value);

        void Serialize(object value, Stream stream);
        object Deserialize(Stream stream);
        T Deserialize<T>(Stream stream);
    }
}
