// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.StringNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class StringNode(IUserSession session, XmlNode node, string nodeID) : 
  XMLPropertyNode<string>(session, node, nodeID)
{
  protected override string GetValue(IUserSession session, string nodeAttributeValue)
  {
    return nodeAttributeValue;
  }
}
