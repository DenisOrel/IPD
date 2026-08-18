// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.AttributesFile
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.Server.WebPortal.ValueConverters;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal;

public class AttributesFile
{
  public static object ParceValue(
    IUserSession session,
    IDBAttributeType attrType,
    AttributeValue rec,
    AttributeInfo attrInfo,
    string iPath,
    bool throwException)
  {
    if (rec.IsEmpty)
      return (object) null;
    IValueConverter valueConverter = (IValueConverter) null;
    switch (attrType.AttributeType)
    {
      case FieldTypes.ftString:
        valueConverter = (IValueConverter) new StringValueConverter(attrType, rec, attrType.AttributeType, iPath);
        break;
      case FieldTypes.ftInteger:
      case FieldTypes.ftAutoInc:
        valueConverter = (IValueConverter) new IntegerValueConverter(attrType, rec);
        break;
      case FieldTypes.ftDouble:
        valueConverter = (IValueConverter) new DoubleValueConverter(attrType, rec);
        break;
      case FieldTypes.ftDateTime:
        valueConverter = (IValueConverter) new DateTimeValueConverter(attrType, rec);
        break;
      case FieldTypes.ftBoolean:
        valueConverter = (IValueConverter) new BooleanValueConverter(attrType, rec);
        break;
      case FieldTypes.ftMeasured:
        valueConverter = (IValueConverter) new MeasuredValueConverter(attrType, rec);
        break;
      case FieldTypes.ftGuid:
        valueConverter = (IValueConverter) new GuidValueConverter(attrType, rec);
        break;
    }
    return valueConverter?.GetValue(session, throwException);
  }

  public static XmlNode FindAttributeValueNode(XmlNode rootNode, string AttrName)
  {
    for (int i = 0; i < rootNode.ChildNodes.Count; ++i)
    {
      if (rootNode.ChildNodes[i].Name == PortalConsts.XmlNodeAttribute && rootNode.ChildNodes[i].Attributes["F_NAME"].Value == AttrName)
        return rootNode.ChildNodes[i].FirstChild;
    }
    return (XmlNode) null;
  }

  public static AttributeValue GetAttributeValue(XmlNode valueNode)
  {
    if (valueNode == null)
      return (AttributeValue) null;
    AttributeValue attributeValue = new AttributeValue();
    string nodeAttributeValue1;
    if ((nodeAttributeValue1 = AttributesFile.GetNodeAttributeValue(valueNode, "F_INLIST_ID")) != null)
      attributeValue.InListID = Convert.ToInt32(nodeAttributeValue1);
    string nodeAttributeValue2;
    if ((nodeAttributeValue2 = AttributesFile.GetNodeAttributeValue(valueNode, "F_STRING_VALUE")) != null)
      attributeValue.StringValue = nodeAttributeValue2;
    string nodeAttributeValue3;
    if ((nodeAttributeValue3 = AttributesFile.GetNodeAttributeValue(valueNode, "F_INTEGER_VALUE")) != null)
      attributeValue.IntegerValue = Convert.ToInt64(nodeAttributeValue3);
    string nodeAttributeValue4;
    if ((nodeAttributeValue4 = AttributesFile.GetNodeAttributeValue(valueNode, "F_DOUBLE_VALUE")) != null)
      attributeValue.DoubleValue = Convert.ToDouble(nodeAttributeValue4, (IFormatProvider) CultureInfo.InvariantCulture);
    string nodeAttributeValue5;
    if ((nodeAttributeValue5 = AttributesFile.GetNodeAttributeValue(valueNode, "F_DATE_VALUE")) != null)
      attributeValue.DateTimeValue = nodeAttributeValue5;
    string nodeAttributeValue6;
    if ((nodeAttributeValue6 = AttributesFile.GetNodeAttributeValue(valueNode, "F_FILE")) != null)
      attributeValue.FileName = nodeAttributeValue6;
    string nodeAttributeValue7;
    if ((nodeAttributeValue7 = AttributesFile.GetNodeAttributeValue(valueNode, "F_FILE_TYPE")) != null)
      attributeValue.FileType = (FileTypes) Convert.ToInt32(nodeAttributeValue7);
    string nodeAttributeValue8;
    if ((nodeAttributeValue8 = AttributesFile.GetNodeAttributeValue(valueNode, "F_FILE_AUTHOR")) != null)
      attributeValue.FileAuthor = nodeAttributeValue8;
    string nodeAttributeValue9;
    if ((nodeAttributeValue9 = AttributesFile.GetNodeAttributeValue(valueNode, "F_ARC_METHOD")) != null)
      attributeValue.ArcMethod = (ArcMethods) Convert.ToInt32(nodeAttributeValue9);
    string nodeAttributeValue10;
    if ((nodeAttributeValue10 = AttributesFile.GetNodeAttributeValue(valueNode, "F_GUID")) != null && GuidHelper.IsGuid(nodeAttributeValue10))
      attributeValue.GuidValue = nodeAttributeValue10;
    string nodeAttributeValue11;
    if ((nodeAttributeValue11 = AttributesFile.GetNodeAttributeValue(valueNode, "F_DESCRIPTION")) != null)
      attributeValue.Description = nodeAttributeValue11;
    return attributeValue;
  }

  public static AttributeInfo GetAttributeInfo(XmlNode node)
  {
    AttributeInfo attributeInfo = new AttributeInfo();
    string nodeAttributeValue1;
    if ((nodeAttributeValue1 = AttributesFile.GetNodeAttributeValue(node, "F_GUID")) != null && GuidHelper.IsGuid(nodeAttributeValue1))
      attributeInfo.Guid = nodeAttributeValue1;
    string nodeAttributeValue2;
    if ((nodeAttributeValue2 = AttributesFile.GetNodeAttributeValue(node, "F_NAME")) != null)
      attributeInfo.Name = nodeAttributeValue2;
    string nodeAttributeValue3;
    if ((nodeAttributeValue3 = AttributesFile.GetNodeAttributeValue(node, "F_SHORT_NAME")) != null)
      attributeInfo.ShortName = nodeAttributeValue3;
    string nodeAttributeValue4;
    if ((nodeAttributeValue4 = AttributesFile.GetNodeAttributeValue(node, "F_ALIAS")) != null)
      attributeInfo.Alias = nodeAttributeValue4;
    string nodeAttributeValue5;
    if ((nodeAttributeValue5 = AttributesFile.GetNodeAttributeValue(node, "F_ATTRIBUTE_TYPE")) != null)
      attributeInfo.FieldType = (FieldTypes) Convert.ToInt32(nodeAttributeValue5);
    return attributeInfo;
  }

  public static RemoteData GetAutoTransferAttributes(XmlNode rootNode)
  {
    for (int i = 0; i < rootNode.ChildNodes.Count; ++i)
    {
      if (rootNode.ChildNodes[i].Name == PortalConsts.XmlNodeSysAttribute)
      {
        string data = string.Empty;
        RemoteMessage message1 = (RemoteMessage) null;
        string message2 = (string) null;
        string addData = (string) null;
        string nodeAttributeValue1;
        if ((nodeAttributeValue1 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_PARAMS")) != null)
          data = nodeAttributeValue1;
        string nodeAttributeValue2;
        if ((nodeAttributeValue2 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_MESSAGE")) != null)
          message2 = nodeAttributeValue2;
        string nodeAttributeValue3;
        if ((nodeAttributeValue3 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_ADD_DATA")) != null)
          addData = nodeAttributeValue3;
        if (message2 != null)
          message1 = new RemoteMessage(message2, addData);
        return new RemoteData(data, message1);
      }
    }
    return (RemoteData) null;
  }

  public static ObjectInfo GetObjectAttributes(XmlNode rootNode)
  {
    return AttributesFile.GetObjectAttributes(rootNode, out string _);
  }

  public static ObjectInfo GetObjectAttributes(XmlNode rootNode, out string siteID)
  {
    ObjectInfo objectAttributes = new ObjectInfo();
    siteID = string.Empty;
    for (int i = 0; i < rootNode.ChildNodes.Count; ++i)
    {
      if (rootNode.ChildNodes[i].Name == PortalConsts.XmlNodeSysAttribute)
      {
        Guid attributeGuidValue1 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_GUID");
        objectAttributes.Guid = attributeGuidValue1;
        Guid attributeGuidValue2 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_OBJECT_GUID");
        objectAttributes.ObjectGuid = attributeGuidValue2;
        Guid attributeGuidValue3 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_PARENT_GUID");
        objectAttributes.ParentGuid = attributeGuidValue3;
        Guid attributeGuidValue4 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_OBJTYPE_GUID");
        objectAttributes.ObjectTypeGuid = attributeGuidValue4;
        string nodeAttributeValue1;
        if ((nodeAttributeValue1 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_OBJ_TYPE_NAME")) != null)
          objectAttributes.ObjTypeName = nodeAttributeValue1;
        string nodeAttributeValue2;
        if ((nodeAttributeValue2 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_OBJ_NAME")) != null)
          objectAttributes.ObjInstanceName = nodeAttributeValue2;
        string nodeAttributeValue3;
        if ((nodeAttributeValue3 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "CAPTION")) != null)
          objectAttributes.Caption = nodeAttributeValue3;
        string nodeAttributeValue4;
        if ((nodeAttributeValue4 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_SITE_ID")) != null)
          siteID = nodeAttributeValue4;
        string nodeAttributeValue5;
        if (!string.IsNullOrEmpty(nodeAttributeValue5 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_ROOT_TYPE")))
          objectAttributes.RootType = (PublishObjectRootType) Enum.Parse(typeof (PublishObjectRootType), nodeAttributeValue5);
        string nodeAttributeValue6;
        if ((nodeAttributeValue6 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_OBJTYPE_SHORTNAME")) != null)
          objectAttributes.ObjTypeShortName = nodeAttributeValue6;
        string nodeAttributeValue7;
        if ((nodeAttributeValue7 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_DOCTYPE_EXT")) != null)
          objectAttributes.DocFileExt = nodeAttributeValue7;
        string nodeAttributeValue8;
        if ((nodeAttributeValue8 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_CREATE_DATE")) != null)
          objectAttributes.CreateDate = nodeAttributeValue8 != string.Empty ? DateTimeHelper.ToDateTime(nodeAttributeValue8) : DateTime.Now;
        Guid attributeGuidValue5 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_PUBLISH_OBJTYPE");
        objectAttributes.PublishObjectType = attributeGuidValue5;
        Guid attributeGuidValue6 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_OWNER_ID");
        objectAttributes.OwnerGuid = attributeGuidValue6;
        Guid attributeGuidValue7 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_LC_STEP");
        objectAttributes.LCStep = attributeGuidValue7;
        Guid attributeGuidValue8 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_LEVEL_ID");
        objectAttributes.LCLevel = attributeGuidValue8;
        Guid attributeGuidValue9 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_PROJECT_ID");
        objectAttributes.ProjectGuid = attributeGuidValue9;
        Guid attributeGuidValue10 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_LINKED_GUID");
        objectAttributes.LinkedGuid = attributeGuidValue10;
        int result1 = -1;
        string nodeAttributeValue9;
        if ((nodeAttributeValue9 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_BASE_VERSION")) != null && int.TryParse(nodeAttributeValue9, out result1))
          objectAttributes.BaseVersion = Convert.ToBoolean(result1);
        string nodeAttributeValue10;
        if ((nodeAttributeValue10 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_VER_CODE")) != null && int.TryParse(nodeAttributeValue10, out result1))
          objectAttributes.VerCode = result1;
        string nodeAttributeValue11;
        if ((nodeAttributeValue11 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_ACCESS")) != null && int.TryParse(nodeAttributeValue11, out result1))
          objectAttributes.Access = result1;
        long result2 = 0;
        string nodeAttributeValue12;
        if ((nodeAttributeValue12 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_MODIFICATION_ID")) != null && long.TryParse(nodeAttributeValue12, out result2))
          objectAttributes.ModificationID = result2;
        Guid attributeGuidValue11 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_CREATOR_ID");
        objectAttributes.CreatorGuid = attributeGuidValue11;
      }
    }
    return objectAttributes;
  }

  public static RelationInfo GetRelationAttributes(XmlNode rootNode)
  {
    RelationInfo relationAttributes = new RelationInfo();
    for (int i = 0; i < rootNode.ChildNodes.Count; ++i)
    {
      if (rootNode.ChildNodes[i].Name == PortalConsts.XmlNodeSysAttribute)
      {
        Guid attributeGuidValue1 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_GUID");
        relationAttributes.Guid = attributeGuidValue1;
        Guid attributeGuidValue2 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_PROJECT_GUID");
        relationAttributes.ProjectGuid = attributeGuidValue2;
        Guid attributeGuidValue3 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_PART_GUID");
        relationAttributes.PartGuid = attributeGuidValue3;
        Guid attributeGuidValue4 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_RELATION_TYPE_GUID");
        relationAttributes.RelationTypeGuid = attributeGuidValue4;
        string nodeAttributeValue1;
        if ((nodeAttributeValue1 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_RELATION_TYPE_NAME")) != null)
          relationAttributes.RelationTypeName = nodeAttributeValue1;
        string nodeAttributeValue2;
        if ((nodeAttributeValue2 = AttributesFile.GetNodeAttributeValue(rootNode.ChildNodes[i], "F_CREATE_DATE")) != null)
          relationAttributes.CreateDate = nodeAttributeValue2 != string.Empty ? DateTimeHelper.ToDateTime(nodeAttributeValue2) : DateTime.Now;
        Guid attributeGuidValue5 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_COMP_VERSION_ID");
        relationAttributes.CompositionVersionGuid = attributeGuidValue5;
        Guid attributeGuidValue6 = AttributesFile.GetNodeAttributeGuidValue(rootNode.ChildNodes[i], "F_REL_CREATOR");
        relationAttributes.CreatorGuid = attributeGuidValue6;
        break;
      }
    }
    return relationAttributes;
  }

  private static string GetNodeAttributeValue(XmlNode valueNode, string attributeName)
  {
    return valueNode.Attributes[attributeName]?.Value;
  }

  private static Guid GetNodeAttributeGuidValue(XmlNode valueNode, string attributeName)
  {
    XmlAttribute attribute = valueNode.Attributes[attributeName];
    return attribute == null || !GuidHelper.IsGuid(attribute.Value) ? Guid.Empty : new Guid(attribute.Value);
  }

  private static FileStream GetFileStream(string iPath, string fileName)
  {
    FileInfo fileInfo = new FileInfo(Path.Combine(iPath, fileName));
    return !fileInfo.Exists ? (FileStream) null : new FileStream(fileInfo.FullName, FileMode.Open);
  }

  private static void AddBlobValue(
    IDBAttributeType attrType,
    IDBAttributable attributable,
    IDBAttribute attr,
    BlobInformation bi,
    FileStream iStream,
    int index)
  {
    if (attr == null)
      attr = attributable.Attributes.AddAttribute(attrType.AttributeID, false);
    else if (index == 0 && attr.ValuesCount > 1)
      attr.ClearValues();
    if (attrType.MultipleValued == MultiValueModes.MultiValues && index > 0)
    {
      attr.AddValue((object) null);
      attr.Index = index;
    }
    IBlobWriter blobWriter = attr as IBlobWriter;
    blobWriter.OpenBlob(bi, false);
    byte[] numArray1 = new byte[Consts.BlobTransferBufferLength];
    int length = iStream.Read(numArray1, 0, Consts.BlobTransferBufferLength);
    while (length > 0)
    {
      byte[] numArray2 = new byte[length];
      Array.Copy((Array) numArray1, (Array) numArray2, length);
      blobWriter.WriteDataBlock(numArray2);
      if (length < Consts.BlobTransferBufferLength)
        break;
    }
  }

  private static void AddValue(
    IDBAttributeType attrType,
    IDBAttributable attributable,
    IDBAttribute attr,
    object value,
    int index,
    bool throwException)
  {
    AttributeValues attributeValues = new AttributeValues(attrType.AttributeID, attrType.AttributeType, attrType.MultipleValued, attrType.Computed)
    {
      ThrowSetException = throwException
    };
    if (attrType.MultipleValued == MultiValueModes.SingleValue || attrType.MultipleValued == MultiValueModes.SingleValueFromList)
      attributeValues.Values = new object[1]{ value };
    else if (index == 0)
    {
      attributeValues.Values = new object[1]{ value };
    }
    else
    {
      List<object> objectList = attr.Values == null || attr.Values.Length == 0 ? new List<object>(1) : new List<object>((IEnumerable<object>) attr.Values);
      if (objectList.Count <= index)
        objectList.Add(value);
      else
        objectList[index] = value;
      attributeValues.Values = objectList.ToArray();
    }
    attributable.SetAttributesValues(new AttributeValues[1]
    {
      attributeValues
    });
  }
}
