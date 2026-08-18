// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.CatalogsNodeID
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase;

public class CatalogsNodeID : INodeID
{
  private string _catalogName;
  private object _cookie;

  public CatalogsNodeID(string catalogName)
  {
    this._catalogName = catalogName;
    this._cookie = (object) null;
  }

  public string CatalogName => this._catalogName;

  public int CategoryID => Consts.CatalogsNodeCategoryID;

  public int TypeID => 0;

  public object Cookie
  {
    get => this._cookie;
    set => this._cookie = value;
  }

  public override bool Equals(object obj)
  {
    return !(obj is CatalogsNodeID catalogsNodeId) ? base.Equals(obj) : this.CatalogName.Equals(catalogsNodeId.CatalogName, StringComparison.InvariantCultureIgnoreCase);
  }

  public override int GetHashCode() => this.CatalogName.GetHashCode();
}
