// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.SimpleFileNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.IO;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class SimpleFileNode(
  IUserSession session,
  XmlNode node,
  string nodeID,
  string directory) : XMLFilePropertyNode<byte[]>(session, node, nodeID, directory)
{
  protected override byte[] GetValue(IUserSession session, string nodeAttributeValue)
  {
    byte[] buffer = (byte[]) null;
    if (nodeAttributeValue != string.Empty)
    {
      using (FileStream fileStream = new FileStream(Path.Combine(this.directory, nodeAttributeValue), FileMode.Open, FileAccess.Read))
      {
        buffer = new byte[fileStream.Length];
        fileStream.Read(buffer, 0, Convert.ToInt32(fileStream.Length));
      }
    }
    return buffer;
  }
}
