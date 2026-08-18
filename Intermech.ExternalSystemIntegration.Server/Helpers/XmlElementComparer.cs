// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.Helpers.XmlElementComparer
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server.Helpers;

internal class XmlElementComparer : IEqualityComparer<XmlElement>
{
  private string _errorMessage = string.Empty;

  public string ErrorMessage => this._errorMessage;

  public bool Equals(XmlElement aEtalonElement, XmlElement aCustomElement)
  {
    this._errorMessage = string.Empty;
    return aEtalonElement != null && aCustomElement != null && this.EqualsElemsName(aEtalonElement, aCustomElement) && this.EqualsElemsContent(aEtalonElement, aCustomElement) && this.EqualElemsAttribute(aEtalonElement, aCustomElement);
  }

  public int GetHashCode(XmlElement obj) => obj.Name.GetHashCode();

  private bool EqualsElemsContent(XmlElement aEtalonElement, XmlElement aCustomElement)
  {
    XmlCharacterData xmlCharacterData1 = aEtalonElement.ChildNodes.OfType<XmlCharacterData>().FirstOrDefault<XmlCharacterData>((Func<XmlCharacterData, bool>) (etalonElContentItem => etalonElContentItem.NodeType == XmlNodeType.CDATA || etalonElContentItem.NodeType == XmlNodeType.Text));
    string aSource = xmlCharacterData1 != null ? xmlCharacterData1.Value : string.Empty;
    XmlCharacterData xmlCharacterData2 = aCustomElement.ChildNodes.OfType<XmlCharacterData>().FirstOrDefault<XmlCharacterData>((Func<XmlCharacterData, bool>) (customElContentItem => customElContentItem.NodeType == XmlNodeType.CDATA || customElContentItem.NodeType == XmlNodeType.Text));
    string str = xmlCharacterData2 != null ? xmlCharacterData2.Value : string.Empty;
    int num = aSource.Length <= 0 || XmlParserService.IsAttributeName(aSource) ? 1 : (aSource == str ? 1 : 0);
    if (num != 0)
      return num != 0;
    this._errorMessage = string.Format(LocalizationHolder.rm.GetString("ExtInt_17"), (object) aEtalonElement.Name, (object) aSource);
    return num != 0;
  }

  private bool EqualsElemsName(XmlElement aEtalonElement, XmlElement aCustomElement)
  {
    int num = aEtalonElement.Name == aCustomElement.Name ? 1 : 0;
    if (num != 0)
      return num != 0;
    this._errorMessage = string.Format(LocalizationHolder.rm.GetString("ExtInt_18"), (object) aEtalonElement.Name);
    return num != 0;
  }

  private bool EqualElemsAttribute(XmlElement aEtalonElement, XmlElement aCustomElement)
  {
    int num = aEtalonElement.Attributes.OfType<XmlAttribute>().SequenceEqual<XmlAttribute>(aCustomElement.Attributes.OfType<XmlAttribute>(), (IEqualityComparer<XmlAttribute>) new XmlAttributeComparer()) ? 1 : 0;
    if (num != 0)
      return num != 0;
    this._errorMessage = string.Format(LocalizationHolder.rm.GetString("ExtInt_19"), (object) aEtalonElement.Name);
    return num != 0;
  }
}
