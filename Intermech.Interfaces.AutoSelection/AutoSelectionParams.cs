// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.AutoSelectionParams
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.AutoSelection;

/// <summary>
/// Класс для передачи аргументов (параметров)  автоподбора
/// </summary>
[Serializable]
public class AutoSelectionParams
{
  /// <summary>
  /// Ид. версии объекта, для которого вызывается автодпобор
  /// </summary>
  protected long _objectID;
  /// <summary>Идентификаторы связей с родительскими объектами</summary>
  /// <remarks>Для условий на родительские объекты, на параметры связей</remarks>
  protected long[] _projectRelationIDs;
  /// <summary>Идентификаторы версий родительских объектов</summary>
  /// <remarks>Позволяет указать "будущие" родительские объекты, в случае если связи еще не созданы </remarks>
  protected long[] _projectObjectIDs;
  /// <summary>Режим автоподбора</summary>
  protected AutoSelectionMode _mode;

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Ид. версии объекта, для которого вызывается автодпобор</param>
  /// <param name="mode">Режим автоподбора</param>
  public AutoSelectionParams(long objectId, AutoSelectionMode mode)
    : this(objectId, 0L, mode)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Ид. версии объекта, для которого вызывается автодпобор</param>
  /// <param name="projectRelationId">Идентификатор связи с родительским объектом</param>
  /// <param name="mode">Режим автоподбора</param>
  public AutoSelectionParams(long objectId, long projectRelationId, AutoSelectionMode mode)
  {
    long objectId1 = objectId;
    long[] projectRelationIDs;
    if (projectRelationId == 0L)
      projectRelationIDs = (long[]) null;
    else
      projectRelationIDs = new long[1]{ projectRelationId };
    int mode1 = (int) mode;
    // ISSUE: explicit constructor call
    this.\u002Ector(objectId1, projectRelationIDs, (AutoSelectionMode) mode1);
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Ид. версии объекта, для которого вызывается автодпобор</param>
  /// <param name="projectRelationIDs">Идентификаторы связей с родительскими объектами</param>
  /// <param name="mode">Режим автоподбора</param>
  public AutoSelectionParams(long objectId, long[] projectRelationIDs, AutoSelectionMode mode)
    : this(objectId, projectRelationIDs, (long[]) null, mode)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId">Ид. версии объекта, для которого вызывается автодпобор</param>
  /// <param name="projectRelationIDs">Идентификаторы связей с родительскими объектами</param>
  /// <param name="projectObjectIDs">Идентификаторы версий родительских объектов</param>
  /// <param name="mode">Режим автоподбора</param>
  public AutoSelectionParams(
    long objectId,
    long[] projectRelationIDs,
    long[] projectObjectIDs,
    AutoSelectionMode mode)
  {
    this._objectID = objectId;
    this._projectRelationIDs = projectRelationIDs;
    this._projectObjectIDs = projectObjectIDs;
    this._mode = mode;
  }

  /// <summary>
  /// Ид. версии объекта, для которого вызывается автодпобор
  /// </summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this._objectID;
    [DebuggerStepThrough] set => this._objectID = value;
  }

  /// <summary>Идентификаторы связей с родительскими объектами</summary>
  /// <remarks>Для условий на родительские объекты, на параметры связей</remarks>
  public long[] ProjectRelationIDs
  {
    [DebuggerStepThrough] get => this._projectRelationIDs;
    [DebuggerStepThrough] set => this._projectRelationIDs = value;
  }

  /// <summary>Идентификаторы версий родительских объектов</summary>
  /// <remarks>Позволяет указать "будущие" родительские объекты, в случае если связи еще не созданы </remarks>
  public long[] ProjectObjectIDs
  {
    [DebuggerStepThrough] get => this._projectObjectIDs;
    [DebuggerStepThrough] set => this._projectObjectIDs = value;
  }

  /// <summary>Режим автоподбора</summary>
  public AutoSelectionMode Mode
  {
    [DebuggerStepThrough] get => this._mode;
    [DebuggerStepThrough] set => this._mode = value;
  }
}
