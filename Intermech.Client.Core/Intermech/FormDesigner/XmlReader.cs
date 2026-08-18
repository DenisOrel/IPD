
// Type: Intermech.FormDesigner.XmlReader
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.XML;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.FormDesigner;

/// <summary>Класс для чтения форм из потока.</summary>
public static class XmlReader
{
  /// <summary>Прочитать форму.</summary>
  /// <param name="stream"></param>
  /// <param name="Host"></param>
  /// <returns></returns>
  public static object Read(Stream stream, IDesignerHost Host)
  {
    object obj = (object) null;
    stream.Position = 0L;
    XmlDocument xmlDocument = new XmlDocument();
    using (System.Xml.XmlReader reader = (System.Xml.XmlReader) new XmlTextReader(stream))
      xmlDocument.Load(reader);
    try
    {
      if (XmlReader.ReadObject((XmlNode) xmlDocument.DocumentElement, Host) is Control control && (control.Location.X < 0 || control.Location.Y < 0))
        control.Location = new Point(15, 15);
      obj = (object) control;
    }
    catch
    {
    }
    return obj;
  }

  /// <summary>Прочитать значение</summary>
  /// <param name="obj"></param>
  /// <param name="home"></param>
  /// <param name="parent"></param>
  /// <param name="property"></param>
  /// <param name="converter"></param>
  /// <returns></returns>
  public static bool ReadValue(
    out object obj,
    object home,
    XmlNode parent,
    PropertyDescriptor property,
    TypeConverter converter)
  {
    bool flag = false;
    obj = (object) null;
    foreach (XmlNode childNode1 in parent.ChildNodes)
    {
      if (childNode1.NodeType == XmlNodeType.Text)
      {
        if (converter.CanConvertFrom(typeof (string)))
        {
          obj = converter.ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) childNode1.InnerText);
          flag = true;
          break;
        }
        break;
      }
      if (childNode1.Name == "Binary")
      {
        byte[] buffer = Convert.FromBase64String(childNode1.InnerText);
        if (ImXmlConsts.GetConversionSupported(converter, typeof (byte[])))
        {
          obj = converter.ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) buffer);
        }
        else
        {
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          using (MemoryStream serializationStream = new MemoryStream(buffer))
            obj = binaryFormatter.Deserialize((Stream) serializationStream);
        }
        flag = true;
        break;
      }
      if (childNode1.Name == "Item")
      {
        obj = property.GetValue(home);
        if (obj is IList)
        {
          foreach (XmlNode childNode2 in parent.ChildNodes)
          {
            System.Type type = System.Type.GetType(childNode2.Attributes["Type"].Value);
            object obj1;
            if (XmlReader.ReadValue(out obj1, home, childNode2, property, TypeDescriptor.GetConverter(type)))
              ((IList) obj).Add(obj1);
          }
        }
        flag = true;
        break;
      }
      int num = childNode1.Name == "InstanceDescriptor" ? 1 : 0;
    }
    return flag;
  }

  /// <summary>Прочитать объект.</summary>
  /// <param name="parent"></param>
  /// <param name="host"></param>
  /// <returns></returns>
  public static object ReadObject(XmlNode parent, IDesignerHost host)
  {
    object obj = (object) null;
    if (parent.Name == "Object")
    {
      System.Type type = System.Type.GetType(parent.Attributes["Type"].Value);
      XmlAttribute attribute1 = parent.Attributes["Name"];
      XmlAttribute attribute2 = parent.Attributes["Control"];
      bool isControl = attribute2 != null && attribute2.Value == "true";
      obj = !typeof (IComponent).IsAssignableFrom(type) || host == null ? Activator.CreateInstance(type) : (attribute1 != null ? (!(host.GetService(typeof (INameCreationService)) is INameCreationService service) ? (object) host.CreateComponent(type) : (!service.IsValidName(attribute1.Value) || (host as IContainer).Components[attribute1.Value] != null ? (object) host.CreateComponent(type) : (object) host.CreateComponent(type, attribute1.Value))) : (object) host.CreateComponent(type));
      switch (obj)
      {
        case IXmlSaveLoad xmlSaveLoad:
          xmlSaveLoad.Load(parent);
          goto label_6;
        case null:
          goto label_6;
        case Control control:
          control.SuspendLayout();
          break;
      }
      XmlReader.ReadProperties(parent, obj, isControl, host);
      control?.ResumeLayout(false);
    }
label_6:
    return obj;
  }

  /// <summary>Прочитать свойства.</summary>
  /// <param name="parent"></param>
  /// <param name="obj"></param>
  /// <param name="isControl"></param>
  /// <param name="host"></param>
  public static void ReadProperties(
    XmlNode parent,
    object obj,
    bool isControl,
    IDesignerHost host)
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
    foreach (XmlNode childNode in parent.ChildNodes)
    {
      if (isControl && childNode.Name.Equals("Object"))
      {
        if (XmlReader.ReadObject(childNode, host) is Control control)
          (obj as Control).Controls.Add(control);
      }
      else if (childNode.Name == "Property")
      {
        XmlAttribute attribute = childNode.Attributes["Name"];
        PropertyDescriptor property = properties[attribute.Value];
        if (property != null)
        {
          if (property.SerializationVisibility == DesignerSerializationVisibility.Content)
          {
            if (typeof (IList).IsAssignableFrom(property.PropertyType))
            {
              object obj1;
              if (XmlReader.ReadValue(out obj1, obj, childNode, property, property.Converter))
                property.SetValue(obj, obj1);
            }
            else
            {
              object obj2 = property.GetValue(obj);
              XmlReader.ReadProperties(childNode, obj2, false, host);
            }
          }
          else
          {
            object obj3;
            if (XmlReader.ReadValue(out obj3, obj, childNode, property, property.Converter))
              property.SetValue(obj, obj3);
          }
        }
      }
    }
  }
}
