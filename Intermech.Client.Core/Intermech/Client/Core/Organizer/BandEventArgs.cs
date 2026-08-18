
// Type: Intermech.Client.Core.Organizer.BandEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class BandEventArgs : EventArgs
{
  private NavigationBand _activeBand;
  private bool _cancel;

  /// <summary>Конструктор.</summary>
  /// <param name="activeBand">Активная панель</param>
  public BandEventArgs(NavigationBand activeBand) => this._activeBand = activeBand;

  /// <summary>Активная панель.</summary>
  public NavigationBand ActiveBand
  {
    get => this._activeBand;
    set => this._activeBand = value;
  }

  /// <summary>Отменено.</summary>
  public bool Canceled
  {
    get => this._cancel;
    set => this._cancel = value;
  }
}
