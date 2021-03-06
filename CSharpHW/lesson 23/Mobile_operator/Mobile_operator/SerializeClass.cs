﻿using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Mobile_operator
{
    public static class SerializeClass
    {
        public static void XMLSerialize<T>(T obj, string nameOfFile)
        {
            var xmlSerialize = new DataContractSerializer(typeof(T));
            using(var fs= new FileStream(nameOfFile, FileMode.Open))
            {
                xmlSerialize.WriteObject(fs, obj);
            }
        }

        public static void JsonSerialize<T>(T obj, string nameOfFile)
        {
            var xmlSerialize = new DataContractJsonSerializer(typeof(T));
            using (var fs = new FileStream(nameOfFile, FileMode.Open))
            {
                xmlSerialize.WriteObject(fs, obj);
            }
        }
    }
}
