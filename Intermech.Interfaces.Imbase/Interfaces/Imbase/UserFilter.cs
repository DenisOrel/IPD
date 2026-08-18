// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.UserFilter
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[Serializable]
public class UserFilter
{
  private bool _enabled;
  private List<Guid> _recordGuids;

  public UserFilter()
    : this(false, new List<Guid>())
  {
  }

  public UserFilter(bool enabled, List<Guid> recordGuids)
  {
    this._enabled = enabled;
    this._recordGuids = recordGuids;
  }

  public bool Enabled
  {
    get => this._enabled;
    set => this._enabled = value;
  }

  public List<Guid> RecordGuids
  {
    get => this._recordGuids;
    set => this._recordGuids = value;
  }
}
