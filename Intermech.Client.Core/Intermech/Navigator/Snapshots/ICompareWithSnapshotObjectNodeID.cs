
// Type: Intermech.Navigator.Snapshots.ICompareWithSnapshotObjectNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Snapshots;

/// <summary>Идентификатор ноды объекта сохранённого в итерации и сравниваемого  </summary>
public interface ICompareWithSnapshotObjectNodeID : 
  IObjectInSnapshotNodeID,
  ISavedObjectNodeID,
  IRelatedObjectNodeID,
  IObjectNodeID,
  INodeID
{
  /// <summary>Результат сравнения объектов в контексте состава сохранённого в итерации и актуального состава</summary>
  CompositionCompareResult CompareResult { get; }
}
