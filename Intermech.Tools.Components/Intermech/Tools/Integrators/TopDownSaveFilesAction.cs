// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.TopDownSaveFilesAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет организовать сохранение всех измененных анализатором документов в порядке их обработки: начиная от головного документа и до самых дальних документов-зависимостей.
/// </summary>
public sealed class TopDownSaveFilesAction : NearestCheckpointAction
{
  private readonly OrderedList<TopDownSaveFilesAction.DocumentEntry> documents;
  private readonly bool needReversePass;

  private TopDownSaveFilesAction(
    CooperativeScheduler scheduler,
    int capacity,
    bool needReversePass)
    : base(scheduler)
  {
    this.documents = capacity > 0 ? new OrderedList<TopDownSaveFilesAction.DocumentEntry>(capacity, (IComparer<TopDownSaveFilesAction.DocumentEntry>) new TopDownSaveFilesAction.DocumentEntryComparer()) : throw new ArgumentOutOfRangeException(nameof (capacity));
    this.needReversePass = needReversePass;
  }

  /// <summary>
  /// Этот метод выполняет роль конструктора. Он возвращает задачу, извлекая ее из базы данных анализатора, а при отсутствии задачи в базе данных -
  /// создает ее.
  /// </summary>
  /// <param name="ctx">Рабочий контекст анализатора изменений</param>
  /// <param name="needReversePass">Требуется ли выполнять дополнительный проход по документам в обратном порядке</param>
  /// <returns>Объект задачи</returns>
  /// <remarks>
  /// В случае сложных (как правило, циклических) зависимостей между документами одного прохода от головного документа может быть недостаточно.
  /// </remarks>
  public static TopDownSaveFilesAction GetOrCreate(
    CaptureChangesDriverContext ctx,
    bool needReversePass)
  {
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    return CaptureChangesDatabaseGlobals<TopDownSaveFilesAction>.GetOrCreate(ctx.Database, (Func<TopDownSaveFilesAction>) (() => new TopDownSaveFilesAction(ctx.Scheduler, ctx.Database.Count, needReversePass)));
  }

  public void RegisterDocument(SectionEntity documentItem, IAction saveAction)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    if (!documentItem.Sections.Contains<FilesSection>())
      throw new ArgumentException();
    if (saveAction == null)
      throw new ArgumentNullException(nameof (saveAction));
    this.documents.Add(new TopDownSaveFilesAction.DocumentEntry(documentItem, saveAction));
  }

  protected override void DoPerform()
  {
    base.DoPerform();
    if (this.documents.Count <= 0)
      return;
    foreach (TopDownSaveFilesAction.DocumentEntry document in this.documents)
      this.Scheduler.AddTask(document.SaveAction);
    if (!this.needReversePass || this.documents.Count <= 1)
      return;
    this.Scheduler.AddTask((IAction) new MethodAction(new Action(this.ScheduleReversePass)));
  }

  private void ScheduleReversePass()
  {
    for (int index = this.documents.Count - 1; index >= 0; --index)
      this.Scheduler.AddTask(this.documents[index].SaveAction);
  }

  private sealed class DocumentEntry
  {
    public readonly SectionEntity DocItem;
    public readonly IAction SaveAction;

    public DocumentEntry(SectionEntity docItem, IAction saveAction)
    {
      this.DocItem = docItem;
      this.SaveAction = saveAction;
    }
  }

  private sealed class DocumentEntryComparer : IComparer<TopDownSaveFilesAction.DocumentEntry>
  {
    public int Compare(
      TopDownSaveFilesAction.DocumentEntry x,
      TopDownSaveFilesAction.DocumentEntry y)
    {
      return x.DocItem.UniqueId.CompareTo(y.DocItem.UniqueId);
    }
  }
}
