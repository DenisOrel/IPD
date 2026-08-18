// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.XmlConfigEditorExtension
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal static class XmlConfigEditorExtension
{
  internal static T CastToType<T>(this object obj) where T : class
  {
    if (obj == null)
      throw new NullReferenceException($"При приведении к типу \"{typeof (T).Name}\" ссылка не содержит объекта");
    return obj is T obj1 ? obj1 : throw new InvalidCastException($"Ошибка приведения объекта \"{obj}\" к типу: {typeof (T).Name}");
  }

  internal static void SaveXmlDocument(XDocument doc, XmlImportBase item, XElement ownerXElement)
  {
    if (item.Name == string.Empty)
      return;
    XElement xelement = new XElement((XName) item.Name);
    if (item.attributes.Count > 0)
    {
      KeyValuePair<string, object>[] array = item.attributes.ToArray<KeyValuePair<string, object>>();
      Array.Sort<KeyValuePair<string, object>>(array, (IComparer<KeyValuePair<string, object>>) new XmlAttributeComparer());
      foreach (KeyValuePair<string, object> keyValuePair in array)
      {
        XAttribute content = new XAttribute((XName) keyValuePair.Key, keyValuePair.Value);
        xelement.Add((object) content);
      }
    }
    if (!string.IsNullOrEmpty(item.Value) && item.Value.Replace(" ", string.Empty).Replace(Environment.NewLine, string.Empty).Replace("\t", string.Empty).Replace("\n", string.Empty).Length > 0)
    {
      XCData content = new XCData(item.Value);
      xelement.Add((object) content);
    }
    if (item.Items != null && item.Items.Count > 0)
    {
      foreach (XmlImportBase xmlImportBase in item.Items)
        XmlConfigEditorExtension.SaveXmlDocument(doc, xmlImportBase, xelement);
    }
    if (ownerXElement != null)
      ownerXElement.Add((object) xelement);
    else
      doc.Add((object) xelement);
  }
}
