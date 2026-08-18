
// Type: Intermech.Navigator.Snapshots.IObjectInSnapshotNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Snapshots;

/// <summary>Идентификатор ноды объекта сохранённого в итерации </summary>
public interface IObjectInSnapshotNodeID : 
  ISavedObjectNodeID,
  IRelatedObjectNodeID,
  IObjectNodeID,
  INodeID
{
  /// <summary>ID итерации</summary>
  [NotEmpty]
  long SnapshotID { get; }
}
