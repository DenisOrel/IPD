// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.XMLFilePropertyNode`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal abstract class XMLFilePropertyNode<TValue> : XMLPropertyNode<TValue>
{
  protected string directory;

  public XMLFilePropertyNode(IUserSession session, XmlNode node, string nodeID, string directory)
    : base(session, node, nodeID, false)
  {
    this.directory = directory;
    this.ReadValue(session, node);
  }
}
