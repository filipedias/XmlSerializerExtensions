using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SerializerExtensions
{
    public static class SerializerManager
    {
        #region Serialize
        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <returns></returns>
        public static string Serialize<T>(object obj, XmlSerializerNamespaces namespaces = null) where T : class
        {
            return Serialize(obj, typeof(T), namespaces);
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <returns></returns>
        public static string Serialize(object obj, XmlSerializerNamespaces namespaces = null)
        {
            var type = obj?.GetType();
            return Serialize(obj, type, namespaces);
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="t">The t.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <returns></returns>
        public static string Serialize(object obj, Type t, XmlSerializerNamespaces namespaces = null)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var serializer = new XmlSerializer(t);

                if (namespaces != null)
                    serializer.Serialize(memoryStream, obj, namespaces);
                else
                    serializer.Serialize(memoryStream, obj);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader streamReader = new StreamReader(memoryStream))
                    return streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// Serializes to file.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <returns></returns>
        public static bool SerializeToFile(object obj, string filePath, out Exception exception, XmlSerializerNamespaces namespaces = null)
        {
            exception = null;
            try
            {
                var xmlString = Serialize(obj, namespaces);
                return SaveToFile(filePath, xmlString);
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }
        
        /// <summary>
        /// Serializes to byte array.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static byte[] SerializeToByteArray(object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var t = obj?.GetType();
                var serializer = new XmlSerializer(t);
                serializer.Serialize(memoryStream, obj);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return memoryStream.ToArray();
            }
        }
        #endregion

        #region Deserialize 
        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <param name="obj">The object.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static bool Deserialize<T>(string xml, out T obj, out Exception exception) where T : class
        {
            exception = null;
            obj = default(T);
            try
            {
                obj = Deserialize<T>(xml);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool Deserialize<T>(string xml, out T obj) where T : class
        {
            Exception exception = null;
            return Deserialize<T>(xml, out obj, out exception);
        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using (StringReader stringReader = new StringReader(xml))
            using (XmlReader xmlReader = XmlReader.Create(stringReader))
            {
                object obj = serializer.Deserialize(xmlReader);
                return ((T)obj);
            }
        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        public static object Deserialize(string xml, Type t)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(t);
                return xmlSerializer.Deserialize(XmlReader.Create(stringReader));
            }
        }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static T Deserialize<T>(Stream stream) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            stream.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(stream);
        }

        /// <summary>
        /// Deserializes the specified byte array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="byteArray">The byte array.</param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] byteArray) where T : class
        {
            if (byteArray == null)
                return null;

            using (var memStream = new MemoryStream(byteArray))
            {
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = Deserialize<T>(memStream);
                return obj;
            }
        }

        /// <summary>
        /// Deserializes from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">The file path.</param>
        /// <param name="obj">The object.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static bool DeserializeFromFile<T>(string filePath, out T obj, out Exception exception) where T : class
        {
            exception = null;
            obj = default(T);
            try
            {
                obj = DeserializeFromFile<T>(filePath);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool DeserializeFromFile<T>(string filePath, out T obj) where T : class
        {
            Exception exception;
            return DeserializeFromFile(filePath, out obj, out exception);
        }

        public static T DeserializeFromFile<T>(string filePath) where T : class
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(file))
            {
                string xmlString = sr.ReadToEnd();
                return Deserialize<T>(xmlString);
            }
        }
        public static bool IsValidXml(string value)
        {
            try
            {
                // Check we actually have a value
                if (string.IsNullOrEmpty(value) == false)
                {
                    // Try to load the value into a document
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(value);

                    // If we managed with no exception then this is valid XML!
                    return true;
                }
                else
                {
                    // A blank value is not valid xml
                    return false;
                }
            }
            catch (XmlException)
            {
                return false;
            }
        }
        #endregion

        #region Private Methods
        private static bool SaveToFile(string filePath, string xmlString)
        {
            var rootDir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(rootDir))
                throw new DirectoryNotFoundException($"Directory \"{rootDir}\" does not exist");
            if (string.IsNullOrEmpty(xmlString))
                throw new ArgumentException("XmlString can not be null or empty");

            FileInfo xmlFile = new FileInfo(filePath);
            using (StreamWriter streamWriter = xmlFile.CreateText())
                streamWriter.WriteLine(xmlString);
            return true;
        }
        #endregion
    }
}
