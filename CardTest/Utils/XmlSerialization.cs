using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MT.Library.Parameter
{
    /// <summary>
    /// Xml序列化器
    /// </summary>
    public class XmlSerialization
    {
        /// <summary>
        /// 将传入的对象列化成XML
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string Object2Xml<T>(T obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter textWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            xmlSerializer.Serialize(textWriter, obj, null);
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
        /// <summary>
        /// 将XML序列成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Xml2Object<T>(string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            return (T)((object)xmlSerializer.Deserialize(stream));
        }
    }
}
