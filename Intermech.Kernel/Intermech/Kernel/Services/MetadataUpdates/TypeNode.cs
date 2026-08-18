// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.TypeNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal abstract class TypeNode : XMLPropertyNode<int>
{
  private readonly int _unknownTypeId;

  public TypeNode(IUserSession session, XmlNode node, string nodeID, int unknownTypeId)
    : base(session, node, nodeID, false)
  {
    this._unknownTypeId = unknownTypeId;
    this.ReadValue(session, node);
  }

  protected override int GetValue(IUserSession session, string nodeAttributeValue)
  {
    return nodeAttributeValue != string.Empty && GuidHelper.IsGuid(nodeAttributeValue) ? this.GetTypeID(session, new Guid(nodeAttributeValue)) : this._unknownTypeId;
  }

  protected abstract int GetTypeID(IUserSession session, Guid guid);
}
