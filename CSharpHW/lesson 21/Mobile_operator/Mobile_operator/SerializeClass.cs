using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Json;

namespace Mobile_operator
{
    public static class SerializeClass
    {
        public static void XMLSerialize<T>(T obj, string nameOfFile)
        {
            var xmlSerialize = new DataContractSerializer(typeof(T));
            using(var fs= new FileStream(nameOfFile, FileMode.OpenOrCreate))
            {
                xmlSerialize.WriteObject(fs, obj);
            }
        }

        public static void JsonSerialize<T>(T obj, string nameOfFile)
        {
            var xmlSerialize = new DataContractJsonSerializer(typeof(T));
            using (var fs = new FileStream(nameOfFile, FileMode.OpenOrCreate))
            {
                xmlSerialize.WriteObject(fs, obj);
            }
        }
    }
}
