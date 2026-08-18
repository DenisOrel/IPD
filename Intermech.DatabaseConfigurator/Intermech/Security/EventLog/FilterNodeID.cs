// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterNodeID
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DatabaseConfigurator;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Security.EventLog;

public class FilterNodeID : INodeID, IFilterGuid
{
  private Guid _filterGuid;
  private object _cookie;

  public FilterNodeID(Guid filterGuid)
  {
    this._filterGuid = filterGuid;
    this._cookie = (object) null;
  }

  public Guid Guid => this._filterGuid;

  int INodeID.CategoryID => DatabaseConfiguratorConsts.EventFilterCategoryID;

  int INodeID.TypeID => 0;

  object INodeID.Cookie
  {
    get => this._cookie;
    set => this._cookie = value;
  }

  Guid IFilterGuid.Value => this._filterGuid;

  public override bool Equals(object obj)
  {
    return obj is FilterNodeID filterNodeId && this._filterGuid == filterNodeId._filterGuid;
  }

  public override int GetHashCode() => this._filterGuid.GetHashCode();
}
