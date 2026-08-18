
// Type: Intermech.FormDesigner.ImXmlWriter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Client.Core.FormDesigner.XML;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.FormDesigner;

/// <summary>Клас сохранения данных об объекте.</summary>
public static class ImXmlWriter
{
  /// <summary>Сохранить IDesignerHost.</summary>
  /// <param name="stream"></param>
  /// <param name="host"></param>
  public static void Write(Stream stream, IDesignerHost host)
  {
    IComponent rootComponent = host.RootComponent;
    XmlDocument doc = new XmlDocument();
    XmlNode element = (XmlNode) doc.CreateElement("FormDesignerXMLRoot");
    XmlAttribute attribute = doc.CreateAttribute("Version");
    attribute.Value = "2.0";
    element.Attributes.Append(attribute);
    doc.AppendChild(element);
    XmlNode newChild = ImXmlWriter.WriteObject(doc, (object) rootComponent, host);
    element.AppendChild(newChild);
    XmlWriter w = (XmlWriter) new XmlTextWriter(stream, Encoding.UTF8);
    doc.Save(w);
    w.Flush();
  }

  /// <summary>Сохранить объект.</summary>
  /// <param name="doc"></param>
  /// <param name="obj"></param>
  /// <param name="host"></param>
  /// <returns></returns>
  public static XmlNode WriteObject(XmlDocument doc, object obj, IDesignerHost host)
  {
    return ImXmlWriter.WriteObject(doc, obj, host, false);
  }

  /// <summary>Сохранить объект.</summary>
  /// <param name="doc"></param>
  /// <param name="obj"></param>
  /// <param name="host"></param>
  /// <param name="isItem"></param>
  /// <returns></returns>
  public static XmlNode WriteObject(XmlDocument doc, object obj, IDesignerHost host, bool isItem)
  {
    XmlNode xmlNode = (XmlNode) null;
    if (obj != null)
    {
      ImXmlConsts.ObjectClass objectClass = ImXmlConsts.ObjectClass.Object;
      if (obj is IComponent)
        objectClass = ImXmlConsts.ObjectClass.Component;
      if (obj is Control)
        objectClass = ImXmlConsts.ObjectClass.Control;
      XmlAttribute attribute1 = doc.CreateAttribute("Name");
      switch (objectClass)
      {
        case ImXmlConsts.ObjectClass.Object:
          xmlNode = (XmlNode) doc.CreateElement("Object");
          break;
        case ImXmlConsts.ObjectClass.Component:
          xmlNode = (XmlNode) doc.CreateElement("Component");
          IComponent component1 = obj as IComponent;
          if (component1.Site != null)
          {
            attribute1.Value = component1.Site.Name;
            xmlNode.Attributes.Append(attribute1);
            break;
          }
          break;
        case ImXmlConsts.ObjectClass.Control:
          xmlNode = (XmlNode) doc.CreateElement("Control");
          Control control1 = obj as Control;
          if (control1.Site != null)
          {
            attribute1.Value = control1.Site.Name;
            xmlNode.Attributes.Append(attribute1);
            break;
          }
          break;
      }
      System.Type type = obj.GetType();
      XmlAttribute attribute2 = doc.CreateAttribute("Type");
      attribute2.Value = type.FullName;
      XmlAttribute attribute3 = doc.CreateAttribute("Assembly");
      attribute3.Value = type.Assembly.GetName().Name;
      xmlNode.Attributes.Append(attribute2);
      xmlNode.Attributes.Append(attribute3);
      if (obj is IXmlSaveLoad xmlSaveLoad)
      {
        xmlSaveLoad.Save(xmlNode);
      }
      else
      {
        PropertyDescriptorCollection properties;
        if (ImXmlConsts.PDCCache.ContainsKey(obj.GetType()))
        {
          properties = ImXmlConsts.PDCCache[obj.GetType()];
        }
        else
        {
          properties = TypeDescriptor.GetProperties(obj.GetType(), ImXmlConsts.FilterAttributes);
          ImXmlConsts.PDCCache[obj.GetType()] = properties;
        }
        ImXmlWriter.WriteProperties(xmlNode, obj, properties, host);
        if (objectClass == ImXmlConsts.ObjectClass.Object & isItem)
        {
          XmlNode element = (XmlNode) xmlNode.OwnerDocument.CreateElement("Item");
          if (ImXmlWriter.WriteValue(element, obj, (PropertyDescriptor) null, host))
            xmlNode.AppendChild(element);
        }
        else
        {
          switch (objectClass)
          {
            case ImXmlConsts.ObjectClass.Component:
              IComponent component2 = obj as IComponent;
              if (component2.Site == null || component2.Site.Container != host.Container)
              {
                xmlNode = (XmlNode) null;
                break;
              }
              break;
            case ImXmlConsts.ObjectClass.Control:
              Control control2 = obj as Control;
              if (control2.Site == null || control2.Site.Container != host.Container)
              {
                xmlNode = (XmlNode) null;
                break;
              }
              if ((control2 is IFormDesignerControl formDesignerControl ? (formDesignerControl.CanContainsChildren ? 1 : 0) : (!control2.HasChildren ? 0 : (!(control2 is AttrsControl) ? 1 : 0))) != 0)
              {
                IEnumerator enumerator = control2.Controls.GetEnumerator();
                try
                {
                  while (enumerator.MoveNext())
                  {
                    Control current = (Control) enumerator.Current;
                    XmlNode newChild = ImXmlWriter.WriteObject(xmlNode.OwnerDocument, (object) current, host);
                    if (newChild != null)
                      xmlNode.AppendChild(newChild);
                  }
                  break;
                }
                finally
                {
                  if (enumerator is IDisposable disposable)
                    disposable.Dispose();
                }
              }
              else
                break;
          }
        }
      }
    }
    return xmlNode;
  }

  /// <summary>Сохранить свойства.</summary>
  /// <param name="parent"></param>
  /// <param name="obj"></param>
  /// <param name="pdc"></param>
  /// <param name="host"></param>
  public static void WriteProperties(
    XmlNode parent,
    object obj,
    PropertyDescriptorCollection pdc,
    IDesignerHost host)
  {
    XmlNode element = (XmlNode) parent.OwnerDocument.CreateElement("Properties");
    foreach (PropertyDescriptor pd in pdc)
      ImXmlWriter.WriteProperty(element, obj, pd, host);
    parent.AppendChild(element);
  }

  /// <summary>Сохранить свойство.</summary>
  /// <param name="parent"></param>
  /// <param name="obj"></param>
  /// <param name="pd"></param>
  /// <param name="host"></param>
  public static void WriteProperty(
    XmlNode parent,
    object obj,
    PropertyDescriptor pd,
    IDesignerHost host)
  {
    XmlNode element = (XmlNode) parent.OwnerDocument.CreateElement("Property");
    bool flag = false;
    if (ImXmlConsts.SkipProperties.Contains(pd.Name))
      return;
    if (pd.ShouldSerializeValue(obj))
    {
      switch (pd.SerializationVisibility)
      {
        case DesignerSerializationVisibility.Visible:
        case DesignerSerializationVisibility.Content:
          flag = ImXmlWriter.WriteValue(element, obj, pd, host);
          break;
      }
    }
    if (!flag)
      return;
    parent.AppendChild(element);
  }

  /// <summary>Сохранить значение.</summary>
  /// <param name="parent"></param>
  /// <param name="obj"></param>
  /// <param name="pd"></param>
  /// <param name="host"></param>
  /// <returns></returns>
  public static bool WriteValue(
    XmlNode parent,
    object obj,
    PropertyDescriptor pd,
    IDesignerHost host)
  {
    bool flag1 = false;
    TypeConverter converter1 = (TypeConverter) null;
    object graph = (object) null;
    bool flag2 = false;
    if (pd != null)
    {
      converter1 = pd.Converter;
      flag2 = pd.IsReadOnly;
      graph = pd.GetValue(obj);
    }
    else if (obj != null)
    {
      converter1 = TypeDescriptor.GetConverter(obj);
      graph = obj;
    }
    if (graph != null)
    {
      System.Type type = graph.GetType();
      ConstructorInfo constructor = type.GetConstructor(new System.Type[0]);
      TypeConverter converter2 = TypeDescriptor.GetConverter(typeof (ImXmlConsts.PropertyFormat));
      XmlAttribute attribute1 = parent.OwnerDocument.CreateAttribute("PropertyFormat");
      parent.Attributes.Append(attribute1);
      if (pd != null)
      {
        XmlAttribute attribute2 = parent.OwnerDocument.CreateAttribute("Name");
        attribute2.Value = pd.Name;
        parent.Attributes.Append(attribute2);
      }
      if (!flag2 && type.IsClass && constructor != (ConstructorInfo) null && !ImXmlConsts.Types2String.Contains(type))
      {
        attribute1.Value = converter2.ConvertToInvariantString((object) ImXmlConsts.PropertyFormat.Object);
        XmlNode newChild = ImXmlWriter.WriteObject(parent.OwnerDocument, graph, host);
        if (newChild != null)
        {
          parent.AppendChild(newChild);
          flag1 = true;
        }
      }
      else if (!flag2 && (type.IsPrimitive || type == typeof (string) || type.IsEnum || ImXmlConsts.Types2String.Contains(type)) && ImXmlConsts.GetConversionSupported(converter1, typeof (string)))
      {
        attribute1.Value = converter2.ConvertToInvariantString((object) ImXmlConsts.PropertyFormat.Value);
        parent.InnerText = converter1.ConvertToInvariantString(graph);
        flag1 = true;
      }
      else if (!flag2 && type.IsSerializable)
      {
        attribute1.Value = converter2.ConvertToInvariantString((object) ImXmlConsts.PropertyFormat.Serialized);
        using (MemoryStream serializationStream = new MemoryStream())
        {
          new BinaryFormatter().Serialize((Stream) serializationStream, graph);
          parent.InnerText = Convert.ToBase64String(serializationStream.ToArray());
        }
        flag1 = true;
      }
      else if (graph is IList)
      {
        attribute1.Value = converter2.ConvertToInvariantString((object) ImXmlConsts.PropertyFormat.Collection);
        IList list = graph as IList;
        if (list.Count > 0)
        {
          foreach (object obj1 in (IEnumerable) list)
          {
            XmlNode newChild = ImXmlWriter.WriteObject(parent.OwnerDocument, obj1, host, true);
            if (newChild != null)
              parent.AppendChild(newChild);
          }
          flag1 = true;
        }
      }
      else if (!flag2 && ImXmlConsts.GetConversionSupported(converter1, typeof (byte[])))
      {
        attribute1.Value = converter2.ConvertToInvariantString((object) ImXmlConsts.PropertyFormat.Binary);
        byte[] inArray = converter1.ConvertTo(graph, typeof (byte[])) as byte[];
        parent.InnerText = Convert.ToBase64String(inArray);
        flag1 = true;
      }
      else if (!flag2 && ImXmlConsts.GetConversionSupported(converter1, typeof (string)))
      {
        attribute1.Value = converter2.ConvertToInvariantString((object) ImXmlConsts.PropertyFormat.Value);
        parent.InnerText = converter1.ConvertToInvariantString(graph);
        flag1 = true;
      }
    }
    return flag1;
  }
}
