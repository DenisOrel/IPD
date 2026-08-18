// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.Helpers.XmlAttributeComparer
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server.Helpers;

internal class XmlAttributeComparer : IEqualityComparer<XmlAttribute>
{
  public bool Equals(XmlAttribute aEtalonAttribute, XmlAttribute aCustomAttribute)
  {
    if (aEtalonAttribute == null || aCustomAttribute == null)
      return false;
    if (aEtalonAttribute.Value == string.Empty || XmlParserService.IsAttributeName(aEtalonAttribute.Value))
      return aEtalonAttribute.Name == aCustomAttribute.Name;
    return aEtalonAttribute.Name == aCustomAttribute.Name && aEtalonAttribute.Value == aCustomAttribute.Value;
  }

  public int GetHashCode(XmlAttribute obj) => obj.Name.GetHashCode();
}
