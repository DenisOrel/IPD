// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.IEventsDispatcher
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections;

#nullable disable
namespace Intermech.PropertyEditors;

/// <summary>
/// Интерфейс регистрации событий на команды меню DatabaseConfigurator.
/// </summary>
public interface IEventsDispatcher
{
  void RegisterAction(ContextMenuID mnuID, EventHandler handler);

  Hashtable EventsList { get; }
}
