// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.SearchAttributes
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using System;
using System.Globalization;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices.Import;

internal static class SearchAttributes
{
  public static readonly string RLFExtention = ".rlf";
  public static readonly string[] RedliningExtensions = new string[2]
  {
    ".rlf2",
    SearchAttributes.RLFExtention
  };

  public static void Create(
    IUserSession session,
    IDBObjectType objType,
    ImportingObject importObject,
    XmlNode rootNode)
  {
    if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00348-306c-11d8-b4e9-00304f19f545")).Contains(objType.ObjectType))
      SearchAttributes.CreateECOAttributes(session, importObject, rootNode);
    else if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545")).Contains(objType.ObjectType))
      SearchAttributes.CreateDocAttributes(session, importObject, rootNode);
    SearchAttributes.CreateNoteAttribute(importObject, rootNode);
  }

  private static void CreateNoteAttribute(ImportingObject importObject, XmlNode rootNode)
  {
    XmlAttribute sysAttribute = SearchAttributes.GetSysAttribute(rootNode, "F_VER_NOTE");
    if (sysAttribute == null || string.IsNullOrEmpty(sysAttribute.Value))
      return;
    int attrNoteID = MetaDataHelper.GetAttributeTypeID("cad00021-306c-11d8-b4e9-00304f19f545");
    if (!importObject.Attributes.Exists((Predicate<AttributeRecord>) (x => x.AttributeId.Equals(attrNoteID))))
    {
      importObject.AddAttribute(new AttributeRecord(attrNoteID, 0L, 0, (object) null, (object) null, (object) null, (object) null, (object) sysAttribute.Value, (object) null));
    }
    else
    {
      AttributeRecord attributeRecord = importObject.Attributes.Find((Predicate<AttributeRecord>) (x => x.AttributeId.Equals(attrNoteID)));
      if (attributeRecord.StringValue != null)
        return;
      attributeRecord.StringValue = (object) sysAttribute.Value;
    }
  }

  private static void CreateDocAttributes(
    IUserSession session,
    ImportingObject importObject,
    XmlNode rootNode)
  {
    SearchAttributes.CreateChangeNoAttribute(session, importObject, rootNode);
    SearchAttributes.CreateECODateTimeAttributeMethod("F_START_DATE", "cad007a0-306c-11d8-b4e9-00304f19f545", importObject, rootNode);
    SearchAttributes.CreateECODateTimeAttributeMethod("F_FINISH_DATE", "cadd9562-306c-11d8-b4e9-00304f19f545", importObject, rootNode);
    SearchAttributes.CreateECODateTimeAttributeMethod("F_CHKINDATE", "cad0079f-306c-11d8-b4e9-00304f19f545", importObject, rootNode);
  }

  private static void CreateChangeNoAttribute(
    IUserSession session,
    ImportingObject importObject,
    XmlNode rootNode)
  {
    SearchAttributes.CreateECOAttributeMethod("F_VER_CODE", "cad00770-306c-11d8-b4e9-00304f19f545", importObject, rootNode, (SearchAttributes.CreateECOAttributesMethodHandler) ((attributeID, value) => new AttributeRecord(attributeID, 0L, 0, (object) null, (object) null, (object) null, (object) null, (object) value, (object) null)));
  }

  private static void CreateECOAttributes(
    IUserSession session,
    ImportingObject importObject,
    XmlNode rootNode)
  {
    SearchAttributes.CreateChangeNoAttribute(session, importObject, rootNode);
    SearchAttributes.CreateECODateTimeAttributeMethod("F_START_DATE", "cad007a0-306c-11d8-b4e9-00304f19f545", importObject, rootNode);
    SearchAttributes.CreateECODateTimeAttributeMethod("F_FINISH_DATE", "cad0079e-306c-11d8-b4e9-00304f19f545", importObject, rootNode);
    SearchAttributes.CreateECODateTimeAttributeMethod("F_CHKINDATE", "cad0079f-306c-11d8-b4e9-00304f19f545", importObject, rootNode);
  }

  private static void CreateECODateTimeAttributeMethod(
    string nodeAttributeName,
    string attributeGuid,
    ImportingObject importObject,
    XmlNode rootNode)
  {
    DateTime result;
    SearchAttributes.CreateECOAttributeMethod(nodeAttributeName, attributeGuid, importObject, rootNode, (SearchAttributes.CreateECOAttributesMethodHandler) ((attributeID, value) => DateTime.TryParse(value, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) && result.ToOADate() != 0.0 ? new AttributeRecord(attributeID, 0L, 0, (object) null, (object) null, (object) null, (object) null, (object) null, (object) result) : (AttributeRecord) null));
  }

  private static void CreateECOAttributeMethod(
    string nodeAttributeName,
    string attributeGuid,
    ImportingObject importObject,
    XmlNode rootNode,
    SearchAttributes.CreateECOAttributesMethodHandler method)
  {
    if (method == null)
      throw new ArgumentNullException();
    int attributeID = MetaDataHelper.GetAttributeTypeID(attributeGuid);
    if (importObject.Attributes.Exists((Predicate<AttributeRecord>) (x => x.AttributeId.Equals(attributeID))))
      return;
    XmlAttribute sysAttribute = SearchAttributes.GetSysAttribute(rootNode, nodeAttributeName);
    if (sysAttribute == null || string.IsNullOrEmpty(sysAttribute.Value))
      return;
    AttributeRecord attribute = method(attributeID, sysAttribute.Value);
    if (attribute == null)
      return;
    importObject.AddAttribute(attribute);
  }

  private static XmlAttribute GetSysAttribute(XmlNode rootNode, string nodeAttributeName)
  {
    return rootNode.SelectSingleNode(PortalConsts.XmlNodeSysAttribute)?.Attributes[nodeAttributeName];
  }

  public static bool HandleAttribute(
    AttributeInfo attrInfo,
    XmlNode nodeAttribute,
    ImportingObject importObject)
  {
    if (attrInfo.Name == "Документ с ограниченной видимостью")
    {
      AttributeValue attributeValue = AttributesFile.GetAttributeValue(nodeAttribute.ChildNodes[0]);
      importObject.Object.AccessLevel = Convert.ToInt32(attributeValue.StringValue);
      return true;
    }
    if (attrInfo.Name == "Идентификатор группового изделия")
    {
      AttributeValue attributeValue = AttributesFile.GetAttributeValue(nodeAttribute.ChildNodes[0]);
      if (attributeValue != null && attributeValue.IntegerValue > 0L)
      {
        byte[] b = new byte[16 /*0x10*/];
        BitConverter.GetBytes(attributeValue.IntegerValue).CopyTo((Array) b, 0);
        importObject.AddAttribute(new AttributeRecord(MetaDataHelper.GetAttributeTypeID("cad001f9-306c-11d8-b4e9-00304f19f545"), 0L, 0, (object) null, (object) null, (object) null, (object) null, (object) new Guid(b).ToString(), (object) null));
        return true;
      }
    }
    else if (attrInfo.Name == "Графа для подписей")
    {
      AttributeValue value = AttributesFile.GetAttributeValue(nodeAttribute.ChildNodes[0]);
      if (value != null)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(SignConsts.GraphAttrTypeGuid);
        int index = attributeType.PossibleValuesDescriptions.FindIndex((Predicate<object>) (x => x.ToString().Equals(value.StringValue)));
        if (index >= 0)
          importObject.AddAttribute(new AttributeRecord(attributeType.AttributeID, 0L, 0, (object) null, (object) null, (object) null, (object) null, attributeType.PossibleValues[index], (object) null));
      }
      return true;
    }
    return false;
  }

  private delegate AttributeRecord CreateECOAttributesMethodHandler(int attributeID, string value);
}
