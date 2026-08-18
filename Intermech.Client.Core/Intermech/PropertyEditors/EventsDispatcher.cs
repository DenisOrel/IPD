
// Type: Intermech.PropertyEditors.EventsDispatcher
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;


namespace Intermech.PropertyEditors;

/// <summary>
/// в классе хранятся обработчики действий, которые регистрируются некоторым главным модулем.
/// класс передается в классы, формирующие меню и назначающие зарегистрированные обработчики.
/// </summary>
public class EventsDispatcher : IEventsDispatcher
{
  private Hashtable eventsList = new Hashtable();

  public Hashtable EventsList => this.eventsList;

  public void Clear() => this.eventsList.Clear();

  private void AssignHandler(int index, EventHandler handler)
  {
    this.eventsList[(object) index] = (object) handler;
  }

  public void RegisterAction(ContextMenuID mnuId, EventHandler handler)
  {
    if (this.eventsList.ContainsKey((object) mnuId))
      this.eventsList[(object) mnuId] = (object) handler;
    else
      this.eventsList.Add((object) mnuId, (object) handler);
  }
}
