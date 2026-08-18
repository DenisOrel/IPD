
// Type: Intermech.Client.Core.Organizer.SchedulerSelectableElement
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public abstract class SchedulerSelectableElement : 
  ICalendarSelectableElement,
  ISelectableElement,
  IComparable<ICalendarSelectableElement>
{
  private Scheduler _scheduler;
  private DateTime _date = DateTime.MinValue;
  private Rectangle _bounds = Rectangle.Empty;
  private bool _selected;

  /// <summary>Конструктор.</summary>
  /// <param name="scheduler">Планировщик</param>
  public SchedulerSelectableElement(Scheduler scheduler) => this._scheduler = scheduler;

  /// <summary>Изменение выделенного состояния.</summary>
  public event EventHandler SelectionChanged;

  /// <summary>Дата.</summary>
  public virtual DateTime Date => this._date;

  /// <summary>Планировщик.</summary>
  public virtual Scheduler Scheduler => this._scheduler;

  /// <summary>
  /// 
  /// </summary>
  public virtual Rectangle Bounds
  {
    get => this._bounds;
    internal set => this._bounds = value;
  }

  /// <summary>Состояние элемента (выделен/не выделен).</summary>
  public virtual bool Selected
  {
    get => this._selected;
    internal set
    {
      if (this._selected == value)
        return;
      this._selected = value;
      if (this.SelectionChanged == null)
        return;
      this.SelectionChanged((object) this, new EventArgs());
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public virtual int CompareTo(ICalendarSelectableElement element)
  {
    return this.Date.CompareTo(element.Date);
  }
}
