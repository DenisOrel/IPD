// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.TechNavigatorEventArgs
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls;

/// <summary>Класс для генерации событий</summary>
public class TechNavigatorEventArgs : EventArgs
{
  /// <summary>Интерфейс события</summary>
  private readonly IIOEvent _event;

  /// <summary>Конструктор</summary>
  /// <param name="aEvent">Интерфейс события</param>
  public TechNavigatorEventArgs(IIOEvent aEvent) => this._event = aEvent;

  /// <summary>IIOEvent</summary>
  public IIOEvent Event => this._event;
}
