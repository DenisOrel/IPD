
// Type: Intermech.Navigator.Snapshots.CompareWithSnapshotObjectNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using System.Diagnostics;


namespace Intermech.Navigator.Snapshots;

/// <summary>Идентификатор ноды объекта, сохранённого в итерации и входящего там в состав другого объекта, сохранённого в итерации</summary>
public class CompareWithSnapshotObjectNodeID : 
  ObjectSavedInSnapshotNodeID,
  INodeID,
  IRelatedObjectNodeID,
  IObjectNodeID,
  ISavedObjectNodeID,
  IObjectInSnapshotNodeID,
  ICompareWithSnapshotObjectNodeID
{
  /// <summary>Результат сравнения объектов в контексте состава сохранённого в итерации и актуального состава</summary>
  protected CompositionCompareResult _CompareResult;

  /// <summary>Конструктор идентификатора ноды сохранённого (напр. в итерации, возможно отсутствующего в БД) объекта</summary>
  /// <param name="createObjectNodeParams">Структура с параметрами для создания идентификатора ноды</param>
  /// <param name="snapshotID">Идентификатор итерации</param>
  public CompareWithSnapshotObjectNodeID(
    [NotNull] CreateObjectNodeParams createObjectNodeParams,
    long snapshotID,
    CompositionCompareResult compareResult = CompositionCompareResult.NotChecked)
    : base(createObjectNodeParams, snapshotID)
  {
    this._CompareResult = compareResult;
  }

  /// <summary>Результат сравнения объектов в контексте состава сохранённого в итерации и актуального состава</summary>
  public CompositionCompareResult CompareResult
  {
    [DebuggerStepThrough] get => this._CompareResult;
    [DebuggerStepThrough] set => this._CompareResult = value;
  }
}
