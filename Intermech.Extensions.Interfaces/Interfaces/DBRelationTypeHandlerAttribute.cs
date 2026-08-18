// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.DBRelationTypeHandlerAttribute
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Interfaces;

public sealed class DBRelationTypeHandlerAttribute : Attribute
{
  [CanBeEmpty]
  private int _relationTypeID = -1;
  [CanBeNull]
  private string _relationTypeName;

  [NotEmpty]
  public int RelationTypeID
  {
    get
    {
      if (this._relationTypeID == -1)
        this._relationTypeID = MetaDataHelperService.Instance.GetRelationTypeID(this.RelationTypeGuid);
      return this._relationTypeID;
    }
  }

  [NotEmpty]
  public Guid RelationTypeGuid { get; }

  [NotNull]
  [NotWhitespace]
  public string RelationTypeName
  {
    get
    {
      if (this._relationTypeName == null)
        this._relationTypeName = MetaDataHelperService.Instance.GetRelationTypeName(this.RelationTypeGuid);
      return this._relationTypeName;
    }
  }

  public DBRelationTypeHandlerAttribute([NotEmpty] Guid dbTypeGuid)
  {
    this.RelationTypeGuid = dbTypeGuid;
  }

  public DBRelationTypeHandlerAttribute([NotNull, NotWhitespace] string dbTypeGuid)
    : this(new Guid(dbTypeGuid))
  {
  }
}
