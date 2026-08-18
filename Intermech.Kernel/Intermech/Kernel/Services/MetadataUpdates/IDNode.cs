// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.IDNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class IDNode(IUserSession session, XmlNode node, string nodeID) : 
  XMLPropertyNode<CalculateIDValue>(session, node, nodeID)
{
  protected override CalculateIDValue GetValue(IUserSession session, string nodeAttributeValue)
  {
    return nodeAttributeValue != string.Empty && GuidHelper.IsGuid(nodeAttributeValue) ? new CalculateIDValue(session, new Guid(nodeAttributeValue)) : (CalculateIDValue) null;
  }
}
