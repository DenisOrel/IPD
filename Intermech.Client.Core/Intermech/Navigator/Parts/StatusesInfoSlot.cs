
// Type: Intermech.Navigator.Parts.StatusesInfoSlot
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Parts;

internal class StatusesInfoSlot(int uniqueId, INodeStatusesInfo statesInfo) : 
  Slot<INodeStatusesInfo>(uniqueId, statesInfo),
  IComparable,
  IComparable<StatusesInfoSlot>
{
  public static int CompareByUniqueId(StatusesInfoSlot first, StatusesInfoSlot second)
  {
    return (first != null ? first.UniqueId : 0).CompareTo(second != null ? second.UniqueId : 0);
  }

  public int CompareTo(object obj)
  {
    return StatusesInfoSlot.CompareByUniqueId(this, obj as StatusesInfoSlot);
  }

  public int CompareTo(StatusesInfoSlot other) => StatusesInfoSlot.CompareByUniqueId(this, other);
}
