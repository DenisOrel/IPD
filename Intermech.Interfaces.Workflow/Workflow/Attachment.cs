// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Attachment
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

[Serializable]
public class Attachment
{
  protected internal long _objectID;
  protected internal int _typeID;
  [NonSerialized]
  protected internal long _relationID;
  protected internal long _id;
  protected internal object _tag;
  internal long _сheckOutBy;
  public AttachmentList InnerList;
  internal long _relationOwnerID;

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID
  {
    get => this._objectID;
    set => this._objectID = value;
  }

  /// <summary>Идентификатор типа объекта</summary>
  public int TypeID
  {
    get => this._typeID;
    set => this._typeID = value;
  }

  /// <summary>Идентификатор связи</summary>
  public long RelationID => this._relationID;

  /// <summary>Идентификатор объекта</summary>
  public long ID
  {
    get => this._id;
    set => this._id = value;
  }

  public object Tag
  {
    get => this._tag;
    set => this._tag = value;
  }

  public long CheckOutBy
  {
    get => this._сheckOutBy;
    set => this._сheckOutBy = value;
  }

  public Attachment()
  {
  }

  public Attachment(Attachment proto)
    : this()
  {
    this.Assign(proto);
  }

  public virtual void Assign(Attachment att)
  {
    this._objectID = att.ObjectID;
    this._id = att.ID;
    this._typeID = att.TypeID;
    this._relationOwnerID = att.RelationOwnerID;
    this._tag = att.Tag;
    this._сheckOutBy = att.CheckOutBy;
  }

  public override bool Equals(object obj)
  {
    return obj is Attachment ? ((Attachment) obj).ObjectID == this.ObjectID : base.Equals(obj);
  }

  public override int GetHashCode() => this.ObjectID.GetHashCode();

  public bool IsGroupingObject => this.InnerList != null;

  public long RelationOwnerID
  {
    get => this._relationOwnerID;
    set => this._relationOwnerID = value;
  }
}
