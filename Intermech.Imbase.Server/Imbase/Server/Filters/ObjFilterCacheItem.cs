// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Filters.ObjFilterCacheItem
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces.Imbase.Filters;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Imbase.Server.Filters;

internal class ObjFilterCacheItem : IComparable, IComparable<ObjFilterCacheItem>
{
  private readonly ImbaseObjFilterInfo _info;
  private ImbaseObjFilterData _data;

  public ObjFilterCacheItem(ImbaseObjFilterInfo info)
    : this(info, (ImbaseObjFilterData) null)
  {
  }

  public ObjFilterCacheItem(ImbaseObjFilterInfo info, ImbaseObjFilterData data)
  {
    this._info = info;
    this._data = data;
  }

  public ImbaseObjFilterInfo Info
  {
    [DebuggerStepThrough] get => this._info;
  }

  public ImbaseObjFilterData Data
  {
    [DebuggerStepThrough] get => this._data;
    [DebuggerStepThrough] set => this._data = value;
  }

  public int CompareTo(object obj) => this.CompareTo(obj as ObjFilterCacheItem);

  public int CompareTo(ObjFilterCacheItem other)
  {
    return other == null || other.Info == null ? -1 : this.Info.ObjectID.CompareTo(other.Info.ObjectID);
  }
}
