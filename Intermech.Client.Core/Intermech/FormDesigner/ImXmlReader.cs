
// Type: Intermech.FormDesigner.ImXmlReader
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.FormDesigner;

/// <summary>Класс для загрузки данных об объекте.</summary>
public static class ImXmlReader
{
  /// <summary>Загрузить данные в IDesignerHost.</summary>
  /// <param name="stream"></param>
  /// <param name="host"></param>
  /// <returns></returns>
  public static object Read(Stream stream, IDesignerHost host)
  {
    stream.Position = 0L;
    XmlDocument xmlDocument = new XmlDocument();
    object obj = (object) null;
    try
    {
      using (System.Xml.XmlReader reader = (System.Xml.XmlReader) new XmlTextReader(stream))
        xmlDocument.Load(reader);
      XmlNode documentElement = (XmlNode) xmlDocument.DocumentElement;
      if (documentElement.Name == "FormDesignerXMLRoot")
      {
        if (documentElement.Attributes["Version"].Value == "2.0")
          obj = ImXmlReader.ReadObject(documentElement.FirstChild, host);
      }
      else if (documentElement.Name == "Object")
        obj = XmlReader.ReadObject(documentElement, host);
    }
    catch
    {
      throw;
    }
    if (obj is DesForm desForm)
    {
      Point location = desForm.Location;
      if (location.X >= 0)
      {
        location = desForm.Location;
        if (location.Y >= 0)
          goto label_16;
      }
      desForm.Location = new Point(16 /*0x10*/, 16 /*0x10*/);
    }
label_16:
    return obj;
  }

  /// <summary>Загрузить объект.</summary>
  /// <param name="parent"></param>
  /// <param name="host"></param>
  /// <returns></returns>
  public static object ReadObject(XmlNode parent, IDesignerHost host)
  {
    XmlAttribute attribute1 = parent.Attributes["Type"];
    XmlAttribute attribute2 = parent.Attributes["Assembly"];
    XmlAttribute attribute3 = parent.Attributes["Name"];
    if (attribute1.Value.CompareTo("System.Windows.Forms.Panel") == 0 || attribute1.Value.CompareTo("Panel") == 0)
    {
      attribute1.Value = "Intermech.Client.Core.FormDesigner.Controls.IMPanel";
      attribute2.Value = "Intermech.Client.Core";
    }
    if (attribute1.Value.CompareTo("System.Windows.Forms.Label") == 0 || attribute1.Value.CompareTo("Label") == 0)
    {
      attribute1.Value = "Intermech.Client.Core.FormDesigner.Controls.IMLabel";
      attribute2.Value = "Intermech.Client.Core";
    }
    if (attribute1.Value.CompareTo("System.Windows.Forms.PictureBox") == 0 || attribute1.Value.CompareTo("PictureBox") == 0)
    {
      attribute1.Value = "Intermech.Client.Core.FormDesigner.Controls.IMPictureBox";
      attribute2.Value = "Intermech.Client.Core";
    }
    if (attribute1.Value.CompareTo("System.Windows.Forms.GroupBox") == 0 || attribute1.Value.CompareTo("GroupBox") == 0)
    {
      attribute1.Value = "Intermech.Client.Core.FormDesigner.Controls.IMGroupBox";
      attribute2.Value = "Intermech.Client.Core";
    }
    if (attribute1.Value.CompareTo("System.Windows.Forms.TabControl") == 0 || attribute1.Value.CompareTo("TabControl") == 0)
    {
      attribute1.Value = "Intermech.Client.Core.FormDesigner.Controls.IMTabControl";
      attribute2.Value = "Intermech.Client.Core";
    }
    ImXmlConsts.ObjectClass objectClass = ImXmlConsts.ObjectClass.Object;
    if (parent.Name == "Component")
      objectClass = ImXmlConsts.ObjectClass.Component;
    else if (parent.Name == "Control")
      objectClass = ImXmlConsts.ObjectClass.Control;
    if (attribute1 == null || attribute2 == null)
      return (object) null;
    Assembly assembly = Assembly.Load(attribute2.Value);
    if (assembly != (Assembly) null)
    {
      System.Type type = assembly.GetType(attribute1.Value);
      if (type != (System.Type) null)
      {
        object obj1 = (object) null;
        if (host != null && (objectClass == ImXmlConsts.ObjectClass.Component || objectClass == ImXmlConsts.ObjectClass.Control))
        {
          INameCreationService service = host.GetService(typeof (INameCreationService)) as INameCreationService;
          obj1 = attribute3 == null || service == null || !service.IsValidName(attribute3.Value) || (host as IContainer).Components[attribute3.Value] != null ? (object) host.CreateComponent(type) : (object) host.CreateComponent(type, attribute3.Value);
        }
        else if (type.GetConstructor(new System.Type[0]) != (ConstructorInfo) null)
        {
          obj1 = Activator.CreateInstance(type);
          if (obj1 is Control control && !string.IsNullOrEmpty(attribute3.Value))
            control.Name = attribute3.Value;
        }
        if (obj1 is Control control1 && obj1 is Form)
          control1.SuspendLayout();
        foreach (XmlNode childNode in parent.ChildNodes)
        {
          if (obj1 != null && childNode.Name.Equals("Properties"))
            ImXmlReader.ReadProperties(childNode, obj1);
          else if (obj1 != null && childNode.Name == "Control" && objectClass == ImXmlConsts.ObjectClass.Control)
          {
            if (ImXmlReader.ReadObject(childNode, host) is Control control2)
              control2.Parent = control1;
          }
          else
          {
            object obj2;
            if (childNode.Name.Equals("Item") && ImXmlReader.ReadProperty(out obj2, childNode, (object) null, (PropertyDescriptor) null, TypeDescriptor.GetConverter(type)))
              return obj2;
          }
        }
        if (control1 != null && obj1 is Form)
          control1.ResumeLayout(false);
        return obj1;
      }
    }
    return (object) null;
  }

  /// <summary>Загрузить свойства.</summary>
  /// <param name="parent"></param>
  /// <param name="obj"></param>
  public static void ReadProperties(XmlNode parent, object obj)
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
      if (childNode.Name == "Property")
      {
        XmlAttribute attribute = childNode.Attributes["Name"];
        if (attribute != null)
        {
          PropertyDescriptor pd = properties[attribute.Value];
          object obj1;
          if (pd != null && ImXmlReader.ReadProperty(out obj1, childNode, obj, pd, pd.Converter))
            pd.SetValue(obj, obj1);
        }
      }
    }
  }

  /// <summary>Загрузить свойство.</summary>
  /// <param name="value"></param>
  /// <param name="parent"></param>
  /// <param name="obj"></param>
  /// <param name="pd"></param>
  /// <param name="propertyConverter"></param>
  /// <returns></returns>
  public static bool ReadProperty(
    out object value,
    XmlNode parent,
    object obj,
    PropertyDescriptor pd,
    TypeConverter propertyConverter)
  {
    XmlAttribute attribute = parent.Attributes["PropertyFormat"];
    ImXmlConsts.PropertyFormat propertyFormat = (ImXmlConsts.PropertyFormat) TypeDescriptor.GetConverter(typeof (ImXmlConsts.PropertyFormat)).ConvertFromInvariantString(attribute.Value);
    value = (object) null;
    switch (propertyFormat)
    {
      case ImXmlConsts.PropertyFormat.Value:
        value = propertyConverter.ConvertFromInvariantString(parent.InnerText);
        break;
      case ImXmlConsts.PropertyFormat.Binary:
        value = propertyConverter.ConvertFrom((object) Convert.FromBase64String(parent.InnerText));
        break;
      case ImXmlConsts.PropertyFormat.Collection:
        if (obj != null && pd != null)
        {
          ImXmlReader.ReadCollection(parent, obj, pd);
          break;
        }
        break;
      case ImXmlConsts.PropertyFormat.Serialized:
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(parent.InnerText)))
        {
          value = binaryFormatter.Deserialize((Stream) serializationStream);
          break;
        }
      case ImXmlConsts.PropertyFormat.Object:
        value = ImXmlReader.ReadObject(parent.FirstChild, (IDesignerHost) null);
        break;
    }
    return value != null;
  }

  /// <summary>Загрузить коллекцию.</summary>
  /// <param name="parent"></param>
  /// <param name="obj"></param>
  /// <param name="pd"></param>
  public static void ReadCollection(XmlNode parent, object obj, PropertyDescriptor pd)
  {
    IList list = pd.GetValue(obj) as IList;
    foreach (XmlNode childNode in parent.ChildNodes)
    {
      object obj1 = ImXmlReader.ReadObject(childNode, (IDesignerHost) null);
      if (obj1 != null)
        list.Add(obj1);
    }
  }
}
