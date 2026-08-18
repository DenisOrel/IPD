// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FileNodeItems
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class FileNodeItems : INodeItems
{
  private const string PropFileId = "StorageFileId";

  public ContentAttributes GetAttributesOf(INodeID nodeID) => ContentAttributes.None;

  public INode GetChild(INodeID nodeID) => (INode) null;

  public string GetAddress(INodeID nodeID) => (string) null;

  public INodeID ParseAddress(string address) => (INodeID) null;

  public PersistentState Serialize(INodeID nodeID)
  {
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("StorageFileId", (object) (FileNodeID) nodeID);
    return persistentState;
  }

  public INodeID Deserialize(PersistentState persistNodeID)
  {
    object obj = persistNodeID.GetValue("StorageFileId");
    return obj != null && obj is FileNodeID ? (INodeID) new FileNodeID(((FileNodeID) obj).FileID, ((FileNodeID) obj).FileZipSize) : (INodeID) null;
  }

  public object GetData(INodeID nodeID, Type dataFormat)
  {
    return dataFormat == typeof (IFileID) ? (object) nodeID : (object) null;
  }

  public object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    object[] data = new object[nodeIDs.Count];
    if (dataFormat == typeof (IFileID))
    {
      for (int index = 0; index < nodeIDs.Count; ++index)
        data[index] = (object) nodeIDs[index];
    }
    return data;
  }

  public IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    return (IUpdateAnalyser) null;
  }

  public object GetService(Type service) => (object) null;
}
