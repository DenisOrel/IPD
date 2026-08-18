// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.DataSetFileNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class DataSetFileNode(
  IUserSession session,
  XmlNode node,
  string nodeID,
  string directory) : XMLFilePropertyNode<DataSet>(session, node, nodeID, directory)
{
  protected override DataSet GetValue(IUserSession session, string nodeAttributeValue)
  {
    if (!(nodeAttributeValue != string.Empty))
      return (DataSet) null;
    using (FileStream serializationStream = new FileStream(Path.Combine(this.directory, nodeAttributeValue), FileMode.Open, FileAccess.Read))
      return (DataSet) new BinaryFormatter().Deserialize((Stream) serializationStream);
  }
}
