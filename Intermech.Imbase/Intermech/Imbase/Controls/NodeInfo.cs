// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.NodeInfo
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

#nullable disable
namespace Intermech.Imbase.Controls;

public class NodeInfo
{
  internal long _objectId;
  internal readonly int _typeId;
  internal int _order;
  internal string _path = string.Empty;

  public NodeInfo(long objectId, int typeId)
  {
    this._typeId = typeId;
    this._objectId = objectId;
    this._order = 0;
  }

  public bool IsCatalog => this._typeId == Intermech.Imbase.Consts.ImbaseCatalogTypeID;

  public bool IsFolder => this._typeId == Intermech.Imbase.Consts.ImbaseFolderTypeID;

  public bool IsCatalogRecord => this._typeId == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID;

  public bool IsTableReference => this._typeId == Intermech.Imbase.Consts.ImbaseTableRefTypeID;

  public bool IsTableMix => this._typeId == Intermech.Imbase.Consts.ImbaseTableMixTypeID;

  public bool IsFavoritesFolder => this._typeId == Intermech.Imbase.Consts.ImbaseFavoritesTypeID;

  public long ObjectId => this._objectId;

  public string Path
  {
    get => this._path;
    set => this._path = value;
  }

  public int TypeId => this._typeId;

  public int Order
  {
    get => this._order;
    set => this._order = value;
  }

  public string Applicability { get; set; } = string.Empty;

  public override bool Equals(object obj)
  {
    return (obj is NodeInfo nodeInfo ? 1 : (base.Equals(obj) ? 1 : 0)) != 0 && nodeInfo != null && this._objectId == nodeInfo._objectId && this._typeId == nodeInfo._typeId && this._order == nodeInfo._order;
  }

  public override int GetHashCode() => (int) this._objectId & this._typeId;
}
