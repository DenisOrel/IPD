// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.ObjectActionsSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Реализует секцию данных для объекта IPS, содержащую серверные и клиентские операции по захвату
/// изменений в самом объекте, а также в связях, выходящих из этого объекта.
/// </summary>
public sealed class ObjectActionsSection
{
  public static readonly SectionPropertyReference RequireCheckoutRef = new SectionPropertyReference(typeof (ObjectActionsSection), nameof (RequireCheckout));
  private readonly ActionQueuePair objectActions;
  private readonly ActionQueuePair relationActions;
  private bool requireCheckout;

  /// <summary>Создает объект.</summary>
  public ObjectActionsSection()
  {
    this.objectActions = new ActionQueuePair();
    this.relationActions = new ActionQueuePair();
  }

  /// <summary>
  /// Возвращает построитель операций по захвату изменений в самом объекте.
  /// </summary>
  public ActionQueuePair ObjectActions => this.objectActions;

  /// <summary>
  /// Возвращает построитель операций по захвату изменений в связях, выходящих из этого объекта.
  /// </summary>
  public ActionQueuePair RelationActions => this.relationActions;

  /// <summary>
  /// Возвращает или устанавливает признак, что объект должен быть взят на изменение, так как это требуется
  /// для одной из операций по захвату изменений.
  /// </summary>
  [Indexable(IndexType.Equality, false)]
  public bool RequireCheckout
  {
    [DebuggerStepThrough] get => this.requireCheckout;
    [DebuggerStepThrough] set
    {
      if (this.requireCheckout == value)
        return;
      this.requireCheckout = value;
      if (this.RequireCheckoutChanged == null)
        return;
      this.RequireCheckoutChanged((object) this, EventArgs.Empty);
    }
  }

  public event EventHandler RequireCheckoutChanged;
}
