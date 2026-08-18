// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.IAutoSelectionService
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

using Intermech.Interfaces.AutoSelection.AutoSelectionLog;
using Intermech.Interfaces.Compositions;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.AutoSelection;

/// <summary>Интерфейс службы автоподбора</summary>
public interface IAutoSelectionService
{
  /// <summary>Вызов подбора для объекта</summary>
  /// <param name="objectId">Ид. версии объекта</param>
  /// <param name="mode">Autoselection mode</param>
  /// <returns>Список ид. созданных связей</returns>
  List<long> ExecuteSelection(long objectId, AutoSelectionMode mode);

  /// <summary>Вызов подбора для объекта</summary>
  /// <param name="objectId">Ид. версии объекта</param>
  /// <param name="relationId">Ид. версии связи</param>
  /// <param name="mode">Autoselection mode</param>
  /// <returns>Список ид. созданных связей</returns>
  List<long> ExecuteSelection(long objectId, long relationId, AutoSelectionMode mode);

  /// <summary>Вызов автоподбора для объекта</summary>
  /// <param name="args">Параметры вызова автоподбора</param>
  /// <returns>Описание созданных связей при автоподборе</returns>
  List<RelObjInfoItem> ExecuteSelection(AutoSelectionParams args);

  /// <summary>
  /// Лог последего запуска подбора, null - если не запускалось
  /// </summary>
  IAutoSelectionLog GetLastExecuteLog { get; }

  /// <summary>Событие перед commit объекта</summary>
  event BeforeCommitCreation OnBeforeCommitCreation;

  /// <summary>Событие после commit объекта</summary>
  event AfterCommitCreation OnAfterCommitCreation;

  /// <summary>Событие перед созданием связи</summary>
  event BeforeCreateRelation OnBeforeCreateRelation;

  /// <summary>Событие после создания связи</summary>
  event AfterCreateRelation OnAfterCreateRelation;
}
