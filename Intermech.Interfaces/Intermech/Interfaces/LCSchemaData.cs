
// Type: Intermech.Interfaces.LCSchemaData
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.IO;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для обработки DrawData для схемы жизненного цикла
    /// </summary>
    public class LCSchemaData
    {
      public static byte[] ReplaceGuidData(Hashtable hash, byte[] drawData)
      {
        if (drawData == null)
          return drawData;
        XmlDocument xmlDocument = new XmlDocument();
        using (MemoryStream inStream = new MemoryStream(drawData))
          xmlDocument.Load((Stream) inStream);
        XmlNode xmlNode = xmlDocument.SelectSingleNode("//schema//nodes");
        if (xmlNode == null || !xmlNode.HasChildNodes)
          return drawData;
        foreach (XmlNode childNode in xmlNode.ChildNodes)
        {
          if (childNode.Name == "node")
          {
            XmlAttribute attribute = childNode.Attributes["guid"];
            if (attribute != null)
            {
              Guid key = new Guid(attribute.Value.ToString());
              object obj = hash[(object) key];
              if (obj != null)
                attribute.Value = obj.ToString();
            }
          }
        }
        using (MemoryStream outStream = new MemoryStream())
        {
          xmlDocument.Save((Stream) outStream);
          return outStream.GetBuffer();
        }
      }
    }
}
