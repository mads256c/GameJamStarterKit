/**
 * Author: Julius Bendt
 * Author URI: www.juto.dk
 * Company: JutoGames
 * Company URI: www.juto.dk
 * Copyright 2015
 **/


using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;


namespace EasySave
{
    public class SaveManager
    {
        public static string filename = "Data.xml", path;
        public static bool Init = false;

        public static string version = "1.0.0";

        private static List<ValueClass> values = new List<ValueClass>();

        public static string Load(string key)
        {
            key = key.Replace(" ", "_");

            if (values.Count == 0)
            {
                LoadFile();
            }

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].key == key)
                {
                    return values[i].value;
                }
            }

            Debug.LogError("No values found with that key!");
            return null;
        }

        public static int LoadInt(string key)
        {
            return Convert.ToInt32(Load(key)); ;
        }

        public static float LoadFloat(string key)
        {
            return float.Parse(Load(key), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        }

        public static bool LoadBool(string key)
        {
            return Convert.ToBoolean(Load(key));
        }

        public static uint LoadUint(string key)
        {
            return Convert.ToUInt32(Load(key));
        }


        public static void Save(string key, string value)
        {

            if (!Init)
            {
                LoadFile();
            }

            if (KeyExist(key))
            {
                values[GetKeyIndex(key)].value = value;
            }
            else
            {
                values.Add(new ValueClass(key, value));
            }

        }

        public static void Save(string key, bool value)
        {
            Save(key, value.ToString());
        }

        public static void Save(string key, int value)
        {
            Save(key, value.ToString());
        }

        public static void Save(string key, float value)
        {
            Save(key, value.ToString());
        }

        public static void Save(string key, uint value)
        {
            Save(key, value.ToString());
        }

        public static void SaveFile()
        {
            if (values.Count > 0)
            {
                XmlDocument xdoc = new XmlDocument();

                xdoc.RemoveAll();
                XmlNode xRoot = xdoc.CreateNode(XmlNodeType.Element, "EasySave", "");

                XmlNode xBase = xdoc.CreateNode(XmlNodeType.Element, "Data", "");

                for (int i = 0; i < values.Count; i++)
                {
                    XmlNode newNode = xdoc.CreateNode(XmlNodeType.Element, values[i].key.ToString(), "");
                    xBase.AppendChild(newNode);
                }

                foreach (XmlNode node in xBase.ChildNodes)
                {
                    for (int i = 0; i < values.Count; i++)
                    {
                        if (node.Name == values[i].key)
                        {
                            node.InnerText = values[i].value;
                        }
                    }
                    xRoot.AppendChild(xBase);
                }
                xdoc.AppendChild(xRoot);
                xdoc.Save(path);
            }
            else
            {
                return;
            }
        }

        public static void LoadFile(string _filename = "Data.xml")
        {
            filename = _filename;
            path = Application.persistentDataPath + @"/" + filename;

            if (File.Exists(path))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);

                XmlNode xRoot = xDoc.FirstChild;
                XmlNode xData = xRoot.FirstChild;

                Init = true;

                foreach (XmlNode Node in xData.ChildNodes)
                {
                    Save(Node.Name, Node.InnerText);
                }
            }
            else
            {
                return;
            }
        }

        public static bool KeyExist(string key)
        {
            if (!Init)
            {
                return false;
            }

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].key == key)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>the index of the key in the "values" variable. return -1 if error is found.</returns>
        public static int GetKeyIndex(string key)
        {
            if (KeyExist(key))
            {
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i].key == key)
                    {
                        return i;
                    }
                }
            }


            return -1;
        }


        void OnApplicationQuit()
        {
            SaveFile();
        }

        void OnDestroy()
        {
            SaveFile();
        }
    }

    [System.Serializable]
    class ValueClass
    {
        public string key, value;

        public ValueClass(string _key, string _value)
        {
            key = _key.Replace(" ", "_");
            value = _value;
        }
    }
}


