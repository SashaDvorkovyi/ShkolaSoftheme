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
    public static class DeSerializeClass
    {
        public static T ReadObjectXML<T>(string nameOfFile)
        {
            var newPerson = default(T);
            var ser = new DataContractSerializer(typeof(T));
            using (FileStream fs = new FileStream(nameOfFile, FileMode.OpenOrCreate))
            {
                var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                newPerson = (T)ser.ReadObject(reader);
            }
            return newPerson;
        }

        public static T ReadObjectJson<T>(string nameOfFile)
        {
            var newPerson = default(T);
            var ser = new DataContractJsonSerializer(typeof(T));
            using (FileStream fs = new FileStream(nameOfFile, FileMode.OpenOrCreate))
            {
                var reader = JsonReaderWriterFactory.CreateJsonReader(fs, new XmlDictionaryReaderQuotas());
                newPerson = (T)ser.ReadObject(reader);
            }
            return newPerson;
        }
    }
}
