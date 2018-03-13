using System;
using System.Collections.Generic;
using System.Xml;

namespace Game.Shared.Components.Map
{
    public static class TiledParserReaderExtension
    {
                //XmlReader Read without all the garbage.
        public static bool ReadNext(this XmlReader rdr)
        {
            //If it's whitespace or garbage keep reading.
            XmlNodeType n;
            bool hasNext;
            do
            {
                hasNext = rdr.Read();
                n = rdr.NodeType;
            } while (hasNext && n != XmlNodeType.Element && n != XmlNodeType.EndElement && n != XmlNodeType.Attribute);

            return hasNext;
        }

        /// <summary>
        ///     Reads XmlReader to the next property.
        /// </summary>
        /// <param name="rdr"></param>
        /// <param name="bufferRead">Number of reads at end element to pass on to the next element. Default: 1</param>
        /// <returns>False if no properties are left for the current object</returns>
        public static bool ReadToNextProperty(this XmlReader rdr, int bufferRead = 1)
        {
            while (rdr.Name != "property")
            {
                rdr.Read();

                if (rdr.Name != "properties") continue;
                for(var i=0;i<bufferRead;i++) rdr.Read();
                return false; //End element, no more properties, Read past it and return false;           
            }

            return true;
        }

        /// <summary>
        ///     Iterate over everything within a node including data.
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        public static IEnumerable<XmlReader> IterateNodeEnumerable(this XmlReader rdr)
        {
            while (rdr.NodeType != XmlNodeType.EndElement)
            {
                if (!rdr.MoveToNextAttribute()) ReadNext(rdr); //Move to attribute if applicable, else read next line.
                yield return rdr;
            }
        }
        
        /// <summary>
        ///     Iterate over everything within a node not including data.
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns></returns>
        public static IEnumerable<XmlReader> IterateNodeEnumerableShort(this XmlReader rdr)
        {
            while (rdr.MoveToNextAttribute()) yield return rdr;
        }

        /// <summary>
        ///     Reads a namevalue  pair to a Dictionary String,String
        /// </summary>
        /// <param name="rdr"></param>
        /// <param name="dictionaryToCopy">Dictionary to write the keyvalue pair to, or null for a new instance</param>
        public static Dictionary<string, string> ReadKeyValuePairToDictionary(this XmlReader rdr,
            Dictionary<string, string> dictionaryToCopy = null)
        {
            var dictionary = dictionaryToCopy ?? new Dictionary<string, string>();

            var propertyName = string.Empty;
            while (rdr.MoveToNextAttribute())
            {
                switch (rdr.Name)
                {
                    case "name":
                        propertyName = rdr.Value;
                        break;
                    case "value":
                        dictionary.Add(propertyName, rdr.Value);
                        break;
                }
            }

            return dictionary;
        }
        
        /// <summary>
        ///     Iterates the node, invokes mapping and processes on inVal;
        /// </summary>
        /// <param name="rdr"></param>
        /// <param name="map"></param>
        /// <param name="inVal">The value to be processed by the map</param>
        /// <param name="includeData">If false only the parameters will processed, not the data. Default includes data.</param>
        /// <typeparam name="T">The Type used for inVal and the mapping type</typeparam>
        /// <returns>processed inVal</returns>
        public static T ProcessNodeMap<T>(this XmlReader rdr, IReadOnlyDictionary<string, Action<XmlReader, T>> map, T inVal,
            bool includeData = true)
        {
            var enumerable = includeData ? rdr.IterateNodeEnumerable() : rdr.IterateNodeEnumerableShort();
            foreach (var r in enumerable)
            {
                map.TryGetValue(r.Name, out var value);
                value?.Invoke(r, inVal);
            }

            return inVal;
        }
    }
}