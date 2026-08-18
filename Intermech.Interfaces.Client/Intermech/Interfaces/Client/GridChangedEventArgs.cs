// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.GridChangedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Класс для события, показывающего что произошли изменения в ObjectPropertyGrid.
/// ApplyNeeded = true показывает что произошли изменения которые потребуют применения изменений.
/// DirectWriteOccured = true показывает что произошла запись напрямую в базу(работа с атрибутом типа ftAutoInc).
///     т.е. Grid изменился, но при ApplyNeeded = false применение изменений не потребуется.
/// </summary>
public class GridChangedEventArgs : EventArgs
{
  private bool _applyNeeded;
  private bool _directWriteOccured;

  public bool ApplyNeeded => this._applyNeeded;

  public bool DirectWriteOccured => this._directWriteOccured;

  public GridChangedEventArgs(bool applyNeeded, bool directWriteOccured)
  {
    this._applyNeeded = applyNeeded;
    this._directWriteOccured = directWriteOccured;
  }
}
