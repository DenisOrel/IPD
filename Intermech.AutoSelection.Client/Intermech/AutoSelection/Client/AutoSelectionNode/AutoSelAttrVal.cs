// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelAttrVal
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Interfaces;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelAttrVal : AutoSelAttr, ICustomTypeDescriptor
{
  private object _attrVal;
  private AutoSelectionAttrMode _attrMode;

  public AutoSelAttrVal(IAutoSelAttrType attrType)
    : this(Guid.Empty, (object) null, AutoSelectionAttrMode.SkipExists, attrType)
  {
  }

  public AutoSelAttrVal(
    Guid attr,
    object value,
    AutoSelectionAttrMode mode,
    IAutoSelAttrType attrType)
    : base(attr, attrType)
  {
    this._attrVal = value;
    this._attrMode = mode;
  }

  [CustomCategory("Attribute.AutoSelection.Client_88")]
  [CustomDisplayName("Attribute.AutoSelection.Client_18")]
  [CustomDescription("Attribute.AutoSelection.Client_19")]
  [TypeConverter(typeof (System.ComponentModel.StringConverter))]
  public object AttrValue
  {
    get => this._attrVal;
    set => this._attrVal = value;
  }

  [CustomCategory("Attribute.AutoSelection.Client_88")]
  [CustomDisplayName("Attribute.AutoSelection.Client_20")]
  [CustomDescription("Attribute.AutoSelection.Client_21")]
  [TypeConverter(typeof (EnumDescConverter))]
  public AutoSelectionAttrMode AttrMode
  {
    get => this._attrMode;
    set => this._attrMode = value;
  }

  public override XmlNode Save(XmlDocument doc)
  {
    XmlNode element = (XmlNode) doc.CreateElement("AutoSelAttr");
    XmlAttribute attribute1 = doc.CreateAttribute("AttrType");
    attribute1.Value = this._attrGuid.ToString();
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = doc.CreateAttribute("AttrMode");
    attribute2.Value = this._attrMode.ToString();
    element.Attributes.Append(attribute2);
    this.SaveAttrValue(doc, element);
    return element;
  }

  public virtual void SaveAttrValue(XmlDocument doc, XmlNode nodeAttr)
  {
    if (nodeAttr?.Attributes == null)
      return;
    XmlAttribute attribute1 = doc.CreateAttribute("AttrVal");
    attribute1.Value = this._attrVal != null ? this._attrVal.ToString() : string.Empty;
    nodeAttr.Attributes.Append(attribute1);
    if (!AutoSelectionUtils.ObjectType.IsObjectSerializable(this._attrVal))
      return;
    MemoryStream memoryStream = new MemoryStream();
    new BinaryFormatter().Serialize((Stream) memoryStream, this._attrVal);
    MemoryStream outStream = new MemoryStream();
    ZLibStreamHelper.PackStream((Stream) memoryStream, ZLibCompressLevels.LevelNormal, (Stream) outStream);
    string base64String = Convert.ToBase64String(outStream.ToArray());
    XmlNode element = (XmlNode) doc.CreateElement("Value");
    XmlAttribute attribute2 = doc.CreateAttribute("Data");
    attribute2.Value = base64String;
    element.Attributes.Append(attribute2);
    nodeAttr.AppendChild(element);
  }

  public static AutoSelAttrVal Load(XmlNode node, IAutoSelAttrType attrType)
  {
    if (node?.Attributes == null)
      return (AutoSelAttrVal) null;
    if (!node.Name.Equals("AutoSelAttr"))
      return (AutoSelAttrVal) null;
    XmlAttribute attribute1 = node.Attributes["AttrType"];
    XmlAttribute attribute2 = node.Attributes["AttrMode"];
    AutoSelectionAttrMode mode = AutoSelectionAttrMode.SkipExists;
    if (attribute2 != null)
      mode = (AutoSelectionAttrMode) EnumTypeHelper.GetEnumValue(typeof (AutoSelectionAttrMode), attribute2.Value, (object) AutoSelectionAttrMode.SkipExists);
    object obj = AutoSelAttrVal.LoadAttrValue(node);
    return new AutoSelAttrVal(new Guid(attribute1.Value), obj, mode, attrType);
  }

  public static object LoadAttrValue(XmlNode nodeAttr)
  {
    if (nodeAttr?.Attributes == null)
      return (object) null;
    if (!nodeAttr.Name.Equals("AutoSelAttr"))
      return (object) null;
    XmlNode xmlNode = (XmlNode) null;
    foreach (XmlNode childNode in nodeAttr.ChildNodes)
    {
      if (childNode.Name == "Value")
      {
        xmlNode = childNode;
        break;
      }
    }
    object obj = (object) null;
    XmlAttribute attribute = xmlNode?.Attributes?["Data"];
    if (attribute != null)
    {
      MemoryStream inStream = new MemoryStream(Convert.FromBase64String(attribute.Value));
      MemoryStream serializationStream = new MemoryStream();
      MemoryStream outStream = serializationStream;
      ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) outStream);
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      try
      {
        obj = binaryFormatter.Deserialize((Stream) serializationStream);
      }
      catch
      {
        obj = (object) null;
      }
    }
    if (obj == null)
      obj = (object) nodeAttr.Attributes["AttrVal"].Value;
    return obj;
  }

  public override object Clone()
  {
    return (object) new AutoSelAttrVal(this._attrGuid, this._attrVal, this._attrMode, this._attrType);
  }

  public PropertyDescriptorCollection GetProperties() => this.GetProperties(new Attribute[0]);

  public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties((object) this, attributes, true);
    PropertyDescriptorCollection properties2 = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    foreach (PropertyDescriptor propertyDescriptor in properties1)
    {
      bool flag = false;
      PropertyDescriptorFx propertyDescriptorFx = new PropertyDescriptorFx(propertyDescriptor, propertyDescriptor.GetValue((object) this));
      if (propertyDescriptor.Name == "AttrValue")
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this.AttrGuid);
        if (attributeType != null)
        {
          flag = true;
          Type editorBaseType = (Type) null;
          switch (attributeType.FieldType)
          {
            case FieldTypes.ftString:
            case FieldTypes.ftPassword:
              if (editorBaseType != (Type) null)
              {
                propertyDescriptorFx.SetEditor(editorBaseType);
                break;
              }
              break;
            case FieldTypes.ftInteger:
            case FieldTypes.ftAutoInc:
              propertyDescriptorFx.SetConverter(typeof (Int32Converter));
              goto case FieldTypes.ftString;
            case FieldTypes.ftDouble:
              propertyDescriptorFx.SetConverter(typeof (System.ComponentModel.DoubleConverter));
              goto case FieldTypes.ftString;
            case FieldTypes.ftDateTime:
              propertyDescriptorFx.SetConverter(typeof (DateTimeConverter));
              editorBaseType = typeof (DateTimeFixedEditor);
              goto case FieldTypes.ftString;
            case FieldTypes.ftObjectLink:
            case FieldTypes.ftObjectLinkByID:
              propertyDescriptorFx.SetEditor((object) new ObjectEditor(attributeType.AttributeID, attributeType.FieldType));
              goto case FieldTypes.ftString;
            case FieldTypes.ftBoolean:
              propertyDescriptorFx.SetConverter(typeof (BooleanConverter));
              goto case FieldTypes.ftString;
            case FieldTypes.ftMeasured:
              propertyDescriptorFx.SetConverter((TypeConverter) new MeasuredValueConverter(attributeType.AttributeID, new AttributeProcessor(0L, AttributableElements.None)));
              propertyDescriptorFx.SetEditor((object) new MeasureEditor(attributeType.AttributeID, (GetDefaultMeasureIDDelegate) null));
              goto case FieldTypes.ftString;
            case FieldTypes.ftGuid:
              propertyDescriptorFx.SetConverter(typeof (System.ComponentModel.GuidConverter));
              goto case FieldTypes.ftString;
            default:
              flag = false;
              goto case FieldTypes.ftString;
          }
        }
      }
      else
        flag = true;
      if (flag)
        properties2.Add((PropertyDescriptor) propertyDescriptorFx);
    }
    return properties2;
  }

  public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes((object) this, true);

  public object GetPropertyOwner(PropertyDescriptor pd) => (object) this;

  public string GetClassName() => TypeDescriptor.GetClassName((object) this, true);

  public string GetComponentName() => TypeDescriptor.GetComponentName((object) this, true);

  public TypeConverter GetConverter() => TypeDescriptor.GetConverter((object) this, true);

  public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent((object) this, true);

  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this, true);
  }

  public object GetEditor(Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this, editorBaseType, true);
  }

  public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents((object) this, true);

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this, attributes, true);
  }
}
