// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseCatalogRefAttProxy
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Imbase;

internal class ImbaseCatalogRefAttProxy
{
  protected long _id;
  protected string _name;

  public ImbaseCatalogRefAttProxy(long id)
  {
    this._id = id;
    if (this._id == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._name = sessionKeeper.Session.GetObjectInfo(this._id).Caption;
  }

  public override string ToString() => this._name;

  public long ID => this._id;
}
