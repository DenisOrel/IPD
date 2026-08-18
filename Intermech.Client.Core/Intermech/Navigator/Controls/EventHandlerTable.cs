
// Type: Intermech.Navigator.Controls.EventHandlerTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Specialized;


namespace Intermech.Navigator.Controls;

public class EventHandlerTable
{
  private IDictionary _handlers = (IDictionary) new HybridDictionary();

  public void AddHandler(object key, Delegate value)
  {
    Delegate handler = (Delegate) this._handlers[key];
    Delegate @delegate = (object) handler != null ? Delegate.Combine(handler, value) : value;
    this._handlers[key] = (object) @delegate;
  }

  public void RemoveHandler(object key, Delegate value)
  {
    Delegate handler = (Delegate) this._handlers[key];
    if ((object) handler == null)
      return;
    this._handlers[key] = (object) Delegate.Remove(handler, value);
  }

  public Delegate this[object key] => (Delegate) this._handlers[key];
}
