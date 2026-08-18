// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.PossibleValuesNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class PossibleValuesNode(IUserSession session, XmlNode node, string directory) : 
  XMLFilePropertyNode<DataTable>(session, node, "F_POSSIBLE_VALUES", directory)
{
  protected override DataTable GetValue(IUserSession session, string nodeAttributeValue)
  {
    DataTable dataTable = (DataTable) null;
    if (nodeAttributeValue != string.Empty)
    {
      string str = Path.Combine(this.directory, nodeAttributeValue);
      FileInfo fileInfo = new FileInfo(str);
      if (fileInfo.Exists && fileInfo.Length > 0L)
      {
        using (FileStream serializationStream = File.OpenRead(str))
        {
          dataTable = (DataTable) new BinaryFormatter().Deserialize((Stream) serializationStream);
          serializationStream.Close();
        }
        if (dataTable != null)
          dataTable.RemotingFormat = SerializationFormat.Binary;
      }
    }
    return dataTable;
  }
}
