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
            if (new FileInfo(nameOfFile).Exists)
            {
                var ser = new DataContractSerializer(typeof(T));

                using (FileStream fs = new FileStream(nameOfFile, FileMode.Open))
                {
                    if (fs.CanRead)
                    {
                        var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                        var readItem = ser.ReadObject(reader);

                        if (readItem is T)
                        {
                            return (T)readItem;
                        }
                    }
                }
            }
            return default(T);
        }

        public static T ReadObjectJson<T>(string nameOfFile)
        {
            if (new FileInfo(nameOfFile).Exists)
            { 
                var ser = new DataContractJsonSerializer(typeof(T));

                using (FileStream fs = new FileStream(nameOfFile, FileMode.Open))
                {
                    if (fs.CanRead)
                    {
                        var reader = JsonReaderWriterFactory.CreateJsonReader(fs, new XmlDictionaryReaderQuotas());
                        var readItem = ser.ReadObject(reader);

                        if (readItem is T)
                        {
                            return (T)readItem;
                        }
                    }
                }
            }
            return default(T);
        }
    }
}
