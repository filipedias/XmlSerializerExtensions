using System;

namespace SerializerExtensions.Extensions
{
    public static class DeserializeExtensions
    {
        /// <summary>
        /// Serializes the specified XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T Deserialize<T>(this string xml) where T : class
        {
            return SerializerManager.Deserialize<T>(xml);
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool Deserialize<T>(this string xml, out T obj) where T : class
        {
            return SerializerManager.Deserialize<T>(xml, out obj);
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <param name="obj">The object.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static bool Deserialize<T>(this string xml, out T obj, out Exception exception) where T : class
        {
            return SerializerManager.Deserialize<T>(xml, out obj, out exception);
        }

        /// <summary>
        /// Deserializes the specified byte array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="byteArray">The byte array.</param>
        /// <returns></returns>
        public static T Deserialize<T>(this byte[] byteArray) where T : class
        {
            return SerializerManager.Deserialize<T>(byteArray);
        }
    }
}
