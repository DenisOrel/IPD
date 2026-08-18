// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.RelationNotSupportInterfaceException`1
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class RelationNotSupportInterfaceException<IDbRelationInterface> : 
  InvalidCastException,
  ISerializable,
  IRelationException
  where IDbRelationInterface : IDBRelation
{
  [CanBeNull]
  [NotWhitespace]
  private string _relationTypeName;

  [NotEmpty]
  public long RelationID { get; }

  [NotEmpty]
  public int RelationTypeID { get; }

  [NotNull]
  [NotWhitespace]
  public string RelationTypeName
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this._relationTypeName == null)
        this._relationTypeName = this.RelationTypeID != -1 ? MetaDataHelperService.Instance.GetRelationTypeName(this.RelationTypeID) : $"Unknown relation type with id = {this.RelationTypeID}";
      return this._relationTypeName;
    }
  }

  public RelationNotSupportInterfaceException([NotNull] IDBRelation dbRelation)
  {
    this.RelationID = dbRelation.RelationID;
    this.RelationTypeID = dbRelation.TypeID;
  }

  public RelationNotSupportInterfaceException([NotEmpty] long relationID, [CanBeEmpty] long relationTypeID = -1)
  {
    this.RelationID = relationID;
    this.RelationTypeID = -1;
  }

  protected RelationNotSupportInterfaceException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.RelationID = info.GetInt64(nameof (RelationID));
    this.RelationTypeID = info.GetInt32(nameof (RelationTypeID));
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("RelationID", this.RelationID);
    info.AddValue("RelationTypeID", this.RelationTypeID);
  }

  [NotNull]
  public override string Message
  {
    get
    {
      return this.RelationTypeID == -1 ? $"Relation with id={this.RelationID} don`t support {typeof (IDbRelationInterface)} interface." : $"Relation with id={this.RelationID} of type '{this.RelationTypeName}' don`t support {typeof (IDbRelationInterface)} interface.";
    }
  }
}
