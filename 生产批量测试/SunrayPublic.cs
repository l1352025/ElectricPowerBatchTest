using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Windows.Forms;
using System.IO;

namespace 桑锐公共库
{
    class ConfigClass
    {
        public static string ReadConfig(string strGroupName, string strName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList xmlNodes;
            XmlElement xmlSubEle;

            string ConfigFile = Application.ExecutablePath + ".cfg";
            if (File.Exists(ConfigFile) == false)
            {
                xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <configuration> <appSettings> </appSettings> </configuration>");
                xmlDoc.Save(ConfigFile);
            }
            xmlDoc.Load(ConfigFile);

            xmlNodes = xmlDoc.GetElementsByTagName(strGroupName);
            foreach (XmlNode xmlNode in xmlNodes)
            {
                foreach (XmlAttribute xmlAttr in xmlNode.Attributes)
                {
                    if (xmlAttr.Name == strName)
                    {
                        return xmlAttr.Value;
                    }
                }
                XmlAttribute xmlAttr1 = xmlDoc.CreateAttribute(strName);
                xmlAttr1.Value = "";
                xmlNode.Attributes.Append(xmlAttr1);
                xmlDoc.Save(ConfigFile);
                return "";
            }
            xmlNodes = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlElement xmlEle in xmlNodes)
            {
                if (xmlEle.Name.ToLower() == "appsettings")
                {
                    xmlSubEle = xmlDoc.CreateElement(strGroupName);
                    xmlEle.AppendChild(xmlSubEle);
                    xmlSubEle.SetAttribute(strName, "");
                    xmlDoc.Save(ConfigFile);
                    break;
                }
            }
            return "";
        }

        public static void WriteConfig(string strGroupName, string strName, string strValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList xmlNodes;
            XmlElement xmlSubEle;

            string ConfigFile = Application.ExecutablePath + ".cfg";
            if (File.Exists(ConfigFile) == false)
            {
                xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <configuration> <appSettings> </appSettings> </configuration>");
                xmlDoc.Save(ConfigFile);
            }
            xmlDoc.Load(ConfigFile);

            xmlNodes = xmlDoc.GetElementsByTagName(strGroupName);
            foreach (XmlNode xmlNode in xmlNodes)
            {
                foreach (XmlAttribute xmlAttr in xmlNode.Attributes)
                {
                    if (xmlAttr.Name == strName)
                    {
                        xmlAttr.Value = strValue;
                        xmlDoc.Save(ConfigFile);
                        return;
                    }
                }
                XmlAttribute xmlAttr1 = xmlDoc.CreateAttribute(strName);
                xmlAttr1.Value = strValue;
                xmlNode.Attributes.Append(xmlAttr1);
                xmlDoc.Save(ConfigFile);
                return;
            }
            xmlNodes = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlElement xmlEle in xmlNodes)
            {
                if (xmlEle.Name.ToLower() == "appsettings")
                {
                    xmlSubEle = xmlDoc.CreateElement(strGroupName);
                    xmlEle.AppendChild(xmlSubEle);
                    xmlSubEle.SetAttribute(strName, strValue);
                    xmlDoc.Save(ConfigFile);
                    break;
                }
            }
        }
    }

    class SunrayCommonLib
    {
        public static UInt16 GenCRC16(byte[] Buf, int iStart, int iLen, UInt16 uiSeed)
        {
            UInt16 uiCRC = 0xffff;

            for (int iLoop = 0; iLoop < iLen; iLoop++)
            {
                uiCRC ^= (UInt16)Buf[iStart + iLoop];
                for (int iLoop1 = 0; iLoop1 < 8; iLoop1++)
                {
                    if ((uiCRC & 1) == 1)
                    {
                        uiCRC >>= 1;
                        uiCRC ^= uiSeed;
                    }
                    else
                    {
                        uiCRC >>= 1;
                    }
                }
            }
            uiCRC ^= 0xffff;
            return uiCRC;
        }
    }
}
