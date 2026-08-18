// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelEnumUtils
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client;

public static class AutoSelEnumUtils
{
  public static XmlNode Save(string name, int id, string caption, XmlDocument doc)
  {
    XmlElement element = doc.CreateElement(name);
    XmlAttribute attribute1 = doc.CreateAttribute("ID");
    attribute1.Value = id.ToString();
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = doc.CreateAttribute("Caption");
    attribute2.Value = caption;
    element.Attributes.Append(attribute2);
    return (XmlNode) element;
  }

  public static bool Load(string name, XmlNode node, out int id)
  {
    id = 0;
    if (node == null)
      return false;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name.Equals(name) && childNode.Attributes != null)
      {
        XmlAttribute attribute = childNode.Attributes["ID"];
        if (attribute != null)
        {
          id = Convert.ToInt32(attribute.Value);
          return true;
        }
      }
    }
    return false;
  }
}
