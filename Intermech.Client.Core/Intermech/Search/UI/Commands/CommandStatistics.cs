
// Type: Intermech.Search.UI.Commands.CommandStatistics
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Search.UI.Commands;

[Serializable]
public sealed class CommandStatistics : ICloneable
{
  private long _totalUsesCount;
  [NonSerialized]
  private int _currentSessionUsesCount;

  public long TotalUsesCount
  {
    get => this._totalUsesCount;
    set => this._totalUsesCount = value;
  }

  public int CurrentSessionUsesCount
  {
    get => this._currentSessionUsesCount;
    set => this._currentSessionUsesCount = value;
  }

  public object Clone()
  {
    return (object) new CommandStatistics()
    {
      TotalUsesCount = this.TotalUsesCount,
      CurrentSessionUsesCount = this.CurrentSessionUsesCount
    };
  }
}
