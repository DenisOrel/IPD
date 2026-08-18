// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.LanguageNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class LanguageNode(IUserSession session, XmlNode node) : XMLPropertyNode<string>(session, node, "F_LANGUAGE_ID")
{
  protected override string GetValue(IUserSession session, string nodeAttributeValue)
  {
    if (!string.IsNullOrEmpty(nodeAttributeValue))
    {
      IDBLanguageType language = session.GetLanguage(new Guid(nodeAttributeValue), false);
      if (language != null)
        return language.LanguageID;
    }
    return string.Empty;
  }
}
