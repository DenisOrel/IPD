// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.LCShemaNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class LCShemaNode(IUserSession session, XmlNode node, string nodeID) : TypeNode(session, node, nodeID, 0)
{
  protected override int GetTypeID(IUserSession session, Guid guid)
  {
    return session.GetLCSchema(guid, true).SchemaID;
  }
}
