
// Type: Intermech.Search.EmptySelectedItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Search;

public sealed class EmptySelectedItems : ISelectedItems, ISimpleSelectedItems
{
  public bool IsCollage => false;

  public INodeID GetItemID(int index) => (INodeID) null;

  public object GetParentData(int index, Type dataFormat) => (object) null;

  public NodeIDPath GetParentPath(int index) => (NodeIDPath) null;

  public int Count => 0;

  public object GetItemData(int index, Type dataFormat) => (object) null;
}
