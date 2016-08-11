using System;
using System.IO;
using System.Xml.Serialization;

namespace SerializerExtensions.Extensions
{
    public static class SerializeExtensions
    {
        /// <summary>
        /// Serializes the specified namespaces.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <returns></returns>
        public static string Serialize<T>(this object obj, XmlSerializerNamespaces namespaces = null)
            where T : class
        {
            return SerializerManager.Serialize<T>(obj, namespaces);
        }

        /// <summary>
        /// Serializes the specified namespaces.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <returns></returns>
        public static string Serialize(this object obj, XmlSerializerNamespaces namespaces = null)
        {
            return SerializerManager.Serialize(obj, namespaces);
        }

        /// <summary>
        /// Serializes the specified namespaces.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="type">The type.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <returns></returns>
        public static string Serialize(this object obj, Type type, XmlSerializerNamespaces namespaces = null)
        {
            return SerializerManager.Serialize(obj, type, namespaces);
        }

        /// <summary>
        /// Serializes to file.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <returns></returns>
        public static bool SerializeToFile(this object obj, string filePath, out Exception exception, XmlSerializerNamespaces namespaces = null)
        {
            return SerializerManager.SerializeToFile(obj, filePath, out exception, namespaces);
        }

        /// <summary>
        /// Serializes to byte array.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static byte[] SerializeToByteArray(this object obj)
        {
            return SerializerManager.SerializeToByteArray(obj);
        }
    }
}
