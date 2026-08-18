
// Type: Intermech.Client.Core.CompositionView.CompositionViewEvents
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Класс для генерации событий</summary>
public class CompositionViewEvents
{
  /// <summary>Событие на начало создания всех связей/объектов</summary>
  public event BeforeAllCreations onBeforeAllCreations;

  /// <summary>Событие на завершение создания всех связей/объектов</summary>
  public event AfterAllCreations onAfterAllCreations;

  /// <summary>Событие на завершение создания объекта</summary>
  public event AfterCommitCreation OnAfterCommitCreation;

  /// <summary>Событие на завершение создания связи</summary>
  public event AfterCreateRelation OnAfterCreateRelation;

  /// <summary>Вызов события onBeforeAllCreations</summary>
  /// <param name="sender"></param>
  /// <param name="session"></param>
  public static void RaiseBeforeAllCreations(object sender, IUserSession session)
  {
    if (CompositionViewHolder.CompositionViewEvents.onBeforeAllCreations == null)
      return;
    CompositionViewHolder.CompositionViewEvents.onBeforeAllCreations(sender, session);
  }

  /// <summary>Вызов события onAfterAllCreations</summary>
  /// <param name="sender"></param>
  /// <param name="session"></param>
  public static void RaiseAfterAllCreations(object sender, IUserSession session)
  {
    if (CompositionViewHolder.CompositionViewEvents.onAfterAllCreations == null)
      return;
    CompositionViewHolder.CompositionViewEvents.onAfterAllCreations(sender, session);
  }

  /// <summary>Вызвать событие OnAfterCommitCreation</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void RaiseCommitCreation(object sender, CompositionViewObjectEventArgs e)
  {
    if (this.OnAfterCommitCreation == null)
      return;
    this.OnAfterCommitCreation(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="method"></param>
  /// <param name="objectID"></param>
  /// <param name="relationIDs"></param>
  public static void RaiseCommitCreation(
    object sender,
    CVButtonMethod method,
    long objectID,
    List<long> relationIDs)
  {
    CompositionViewObjectEventArgs e = new CompositionViewObjectEventArgs(method, objectID);
    CompositionViewHolder.CompositionViewEvents.RaiseCommitCreation(sender, e);
    if (e.RelationIDs.Count <= 0)
      return;
    relationIDs.AddRange((IEnumerable<long>) e.RelationIDs);
  }

  /// <summary>Вызвать событие OnAfterCreateRelation</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void RaiseCreateRelation(object sender, CompositionViewRelationEventArgs e)
  {
    if (this.OnAfterCreateRelation == null)
      return;
    this.OnAfterCreateRelation(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="method"></param>
  /// <param name="projObjectID"></param>
  /// <param name="partObjectID"></param>
  /// <param name="relationID"></param>
  /// <param name="relationIDs"></param>
  public static void RaiseCreateRelation(
    object sender,
    CVButtonMethod method,
    long projObjectID,
    long partObjectID,
    long relationID,
    List<long> relationIDs)
  {
    CompositionViewRelationEventArgs e = new CompositionViewRelationEventArgs(method, projObjectID, partObjectID, relationID);
    CompositionViewHolder.CompositionViewEvents.RaiseCreateRelation(sender, e);
    if (e.RelationIDs.Count <= 0)
      return;
    relationIDs.AddRange((IEnumerable<long>) e.RelationIDs);
  }
}
