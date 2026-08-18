// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.XMLFileHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Globalization;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

public static class XMLFileHelper
{
  public static XmlNode CreateValueNode(XmlDocument xmlDocument, int index)
  {
    XmlElement element = xmlDocument.CreateElement(PortalConsts.XmlNodeValueAttribute);
    XmlAttribute attribute = xmlDocument.CreateAttribute("F_INLIST_ID");
    attribute.Value = Convert.ToString(index);
    element.Attributes.Append(attribute);
    return (XmlNode) element;
  }

  public static XmlNode CreateRemarkAttributeNode(XmlDocument xmlDocument, RemarkInfo attrType)
  {
    XmlNode element = (XmlNode) xmlDocument.CreateElement(PortalConsts.XmlRootNodeRemark);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_SITE_ID", Convert.ToString(attrType.PublishSite));
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_MODIFY_DATE", DateTimeHelper.ToString(attrType.PublishTime));
    if (attrType.Guid != null && GuidHelper.IsGuid(attrType.Guid))
      XMLFileHelper.AddGuidAttribute(xmlDocument, element, new Guid(attrType.Guid));
    if (attrType.Name != null && attrType.Name != string.Empty)
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_NAME", attrType.Name);
    if (attrType.ShortName != null && attrType.ShortName != string.Empty)
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_SHORT_NAME", attrType.ShortName);
    if (attrType.Alias != null && attrType.Alias != string.Empty)
      XMLFileHelper.AddAttribute(xmlDocument, element, "F_ALIAS", attrType.Alias);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_ATTRIBUTE_TYPE", Convert.ToString((int) attrType.FieldType));
    return element;
  }

  public static void AddIntegerAttribute(XmlDocument xmlDocument, XmlNode node, long value)
  {
    XMLFileHelper.AddAttribute(xmlDocument, node, "F_INTEGER_VALUE", Convert.ToString(value));
  }

  public static void AddDoubleAttribute(XmlDocument xmlDocument, XmlNode node, double value)
  {
    XMLFileHelper.AddAttribute(xmlDocument, node, "F_DOUBLE_VALUE", Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture));
  }

  public static void AddDateTimeAttribute(XmlDocument xmlDocument, XmlNode node, DateTime value)
  {
    XMLFileHelper.AddAttribute(xmlDocument, node, "F_DATE_VALUE", DateTimeHelper.ToString(value));
  }

  public static void AddGuidAttribute(XmlDocument xmlDocument, XmlNode node, Guid value)
  {
    XMLFileHelper.AddAttribute(xmlDocument, node, "F_GUID", Convert.ToString((object) value, (IFormatProvider) CultureInfo.InvariantCulture));
  }

  public static void AddStringAttribute(XmlDocument xmlDocument, XmlNode node, string value)
  {
    XMLFileHelper.AddAttribute(xmlDocument, node, "F_STRING_VALUE", value);
  }

  public static void AddAttribute(
    XmlDocument xmlDocument,
    XmlNode node,
    string attrName,
    string value)
  {
    XmlAttribute attribute = xmlDocument.CreateAttribute(attrName);
    attribute.Value = value;
    node.Attributes.Append(attribute);
  }
}
