// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportList`1
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Список базовых классов настроек</summary>
[Serializable]
public abstract class XmlExchangeExportList<T> : List<T> where T : XmlExchangeExportItem, new()
{
  /// <summary>Получение наименование секции элемента в XML</summary>
  /// <returns></returns>
  private string GetXmlTypeName()
  {
    return this.GetType().GetCustomAttributes<XmlRootAttribute>().FirstOrDefault<XmlRootAttribute>()?.ElementName ?? throw new Exception("XmlRootAttribute not found");
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  internal bool LoadData(XmlNode xmlNode)
  {
    if (xmlNode == null || xmlNode.ChildNodes.Count == 0 || string.Compare(xmlNode.Name, this.GetXmlTypeName(), StringComparison.OrdinalIgnoreCase) != 0)
      return false;
    foreach (XmlNode childNode in xmlNode.ChildNodes)
    {
      if (childNode != null)
      {
        T obj = new T();
        if (obj.LoadData(childNode))
          this.Add(obj);
      }
    }
    return true;
  }

  /// <summary>Сохранение данных в XML</summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  internal XmlNode SaveData(XmlDocument xmlDoc)
  {
    if (xmlDoc == null)
      return (XmlNode) null;
    XmlNode element = (XmlNode) xmlDoc.CreateElement(this.GetXmlTypeName());
    foreach (T obj in (List<T>) this)
    {
      XmlNode newChild = obj.SaveData(xmlDoc);
      if (newChild != null)
        element.AppendChild(newChild);
    }
    return element;
  }
}
