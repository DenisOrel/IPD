// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.Helpers.XmlParserService
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server.Helpers;

public class XmlParserService : LongLifeObject, IXMLParser
{
  private StringBuilder _compareErrorMessage;

  public XmlParserService() => this._compareErrorMessage = new StringBuilder();

  public string CompareErrorMessage => this._compareErrorMessage.ToString();

  public bool CompareNodes(string aEtalonNode, string aCustomNode)
  {
    XmlDocument xmlDocument1 = new XmlDocument();
    xmlDocument1.LoadXml(aEtalonNode);
    XmlNode documentElement1 = (XmlNode) xmlDocument1.DocumentElement;
    if (documentElement1 == null)
      return false;
    XmlElement[] array = documentElement1.ChildNodes.OfType<XmlElement>().ToArray<XmlElement>();
    XmlDocument xmlDocument2 = new XmlDocument();
    xmlDocument2.LoadXml(aCustomNode);
    XmlNode documentElement2 = (XmlNode) xmlDocument2.DocumentElement;
    if (documentElement2 == null)
      return false;
    XmlElement[] customNodeChildElements = documentElement2.ChildNodes.OfType<XmlElement>().ToArray<XmlElement>();
    XmlElementComparer comparer = new XmlElementComparer();
    if (((IEnumerable<XmlElement>) array).SequenceEqual<XmlElement>((IEnumerable<XmlElement>) customNodeChildElements, (IEqualityComparer<XmlElement>) comparer))
      return ((IEnumerable<XmlElement>) array).Select<XmlElement, bool>((Func<XmlElement, bool>) (etalonNodeChildElement => ((IEnumerable<XmlElement>) customNodeChildElements).Any<XmlElement>((Func<XmlElement, bool>) (customNodeChildElement => this.CompareNodes(etalonNodeChildElement.OuterXml, customNodeChildElement.OuterXml))))).All<bool>((Func<bool, bool>) (x => x));
    this._compareErrorMessage.Clear();
    this._compareErrorMessage.AppendLine(string.Format(LocalizationHolder.rm.GetString("ExtInt_20"), (object) documentElement1.Name, (object) comparer.ErrorMessage));
    return false;
  }

  public Dictionary<int, string> ExtractAttributeFromNodes(
    Guid aSessionGuid,
    string aEtalonNode,
    string aCustomNode)
  {
    Dictionary<int, string> adictionary = new Dictionary<int, string>();
    XmlDocument xmlDocument1 = new XmlDocument();
    xmlDocument1.LoadXml(aEtalonNode);
    XmlNode documentElement1 = (XmlNode) xmlDocument1.DocumentElement;
    if (documentElement1 == null)
      return adictionary;
    XmlDocument xmlDocument2 = new XmlDocument();
    xmlDocument2.LoadXml(aCustomNode);
    XmlNode documentElement2 = (XmlNode) xmlDocument2.DocumentElement;
    if (documentElement2 == null)
      return adictionary;
    if (documentElement1.Attributes != null && documentElement2.Attributes != null)
    {
      List<XmlAttribute> list1 = documentElement1.Attributes.OfType<XmlAttribute>().ToList<XmlAttribute>();
      List<XmlAttribute> list2 = documentElement2.Attributes.OfType<XmlAttribute>().ToList<XmlAttribute>();
      if (list1.Count > 0 && list2.Count > 0)
      {
        foreach (XmlAttribute xmlAttribute1 in list1.ToArray())
        {
          foreach (XmlAttribute xmlAttribute2 in list2.ToArray())
          {
            if (!(xmlAttribute1.Name != xmlAttribute2.Name))
            {
              this.ExtractAttributeValue(aSessionGuid, xmlAttribute1.Value, xmlAttribute2.Value, adictionary);
              list1.Remove(xmlAttribute1);
              list2.Remove(xmlAttribute2);
              break;
            }
          }
        }
      }
    }
    string aattributeName = documentElement1.ChildNodes.OfType<XmlCharacterData>().Any<XmlCharacterData>() ? documentElement1.ChildNodes.OfType<XmlCharacterData>().First<XmlCharacterData>().Value : string.Empty;
    string aattributeValue = documentElement2.ChildNodes.OfType<XmlCharacterData>().Any<XmlCharacterData>() ? documentElement2.ChildNodes.OfType<XmlCharacterData>().First<XmlCharacterData>().Value : string.Empty;
    if (aattributeName.Length > 0 && aattributeValue.Length > 0)
      this.ExtractAttributeValue(aSessionGuid, aattributeName, aattributeValue, adictionary);
    List<XmlElement> list3 = documentElement1.ChildNodes.OfType<XmlElement>().ToList<XmlElement>();
    List<XmlElement> list4 = documentElement2.ChildNodes.OfType<XmlElement>().ToList<XmlElement>();
    if (list3.Count <= 0 || list4.Count <= 0)
      return adictionary;
    foreach (XmlElement xmlElement1 in list3.ToArray())
    {
      foreach (XmlElement xmlElement2 in list4.ToArray())
      {
        if (this.CompareNodes(xmlElement1.OuterXml, xmlElement2.OuterXml))
        {
          foreach (KeyValuePair<int, string> attributeFromNode in this.ExtractAttributeFromNodes(aSessionGuid, xmlElement1.OuterXml, xmlElement2.OuterXml))
          {
            if (!adictionary.ContainsKey(attributeFromNode.Key))
              adictionary.Add(attributeFromNode.Key, attributeFromNode.Value);
          }
          list3.Remove(xmlElement1);
          list4.Remove(xmlElement2);
          break;
        }
      }
    }
    return adictionary;
  }

  private void ExtractAttributeValue(
    Guid aSessionGuid,
    string aattributeName,
    string aattributeValue,
    Dictionary<int, string> adictionary)
  {
    if (!XmlParserService.IsAttributeName(aattributeName))
      return;
    string anAttributeName = aattributeName.Substring(1, aattributeName.Length - 2);
    if (!(UserSession.GetSessionByID(aSessionGuid) is UserSession sessionById))
      return;
    IDBAttributeType attributeType = sessionById.GetAttributeType(anAttributeName, false);
    if (attributeType == null || adictionary.ContainsKey(attributeType.AttributeID))
      return;
    adictionary.Add(attributeType.AttributeID, aattributeValue);
  }

  public static bool IsAttributeName(string aSource)
  {
    if (aSource.Length <= 3)
      return false;
    int num1 = aSource.IndexOf('[');
    int num2 = aSource.IndexOf(']');
    return num1 > -1 && num2 > num1 && num2 == aSource.Length - 1;
  }
}
