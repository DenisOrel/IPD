// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.ObjectTypeRec
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Imbase.Controls;
using System;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

internal class ObjectTypeRec : NodeInfo
{
  private readonly Guid _typeGuid;
  private readonly string _typeName;
  private bool _hasImbaseCatalog;

  public ObjectTypeRec(long objectId, int typeId, Guid typeGuid, string typeName)
    : base(objectId, typeId)
  {
    this._typeGuid = typeGuid;
    this._typeName = typeName;
  }

  public Guid TypeGuid => this._typeGuid;

  public string TypeName => this._typeName;

  public bool HasImbaseCatalogs
  {
    get => this._hasImbaseCatalog;
    set => this._hasImbaseCatalog = value;
  }

  public override string ToString() => this._typeName;
}
