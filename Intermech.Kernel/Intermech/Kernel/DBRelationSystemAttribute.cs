// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRelationSystemAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;


namespace Intermech.Kernel;

internal class DBRelationSystemAttribute : DBSystemAttribute
{
  private IDBRelation _DBRelation;

  public DBRelationSystemAttribute(
    UserSession uSession,
    ObligatoryObjectAttributes attribute,
    IDBAttributeCollection attrs,
    IDBRelation rel)
    : base(uSession, attribute, attrs)
  {
    this._ParentObject = rel as DBAttributable;
    this._DBRelation = rel;
    this._DBRelationID = rel.RelationID;
  }

  public override long DBObjectID => this._DBRelation.RelationID;

  public override long DB_ID => 0;

  public override bool IsObjectAttribute => false;

  public override int TypeID => this._DBRelation.RelationType;

  private void ThrowUnknownAttributeException()
  {
    throw new KernelException(string.Format(sc_12581.ssp_appserver_12582(), (object) this.ObligatoryAttribute));
  }

  public override string Description
  {
    get => this._DBRelation.GetDescriptionsByID((int) this.ObligatoryAttribute, true)[0];
  }

  public override object Value
  {
    get => this._DBRelation.GetValuesByID((int) this.ObligatoryAttribute, true)[0];
    set
    {
      switch (this.ObligatoryAttribute)
      {
        case ObligatoryObjectAttributes.F_PRJ_GUID:
        case ObligatoryObjectAttributes.F_CREATE_DATE:
        case ObligatoryObjectAttributes.F_PART_ID:
        case ObligatoryObjectAttributes.F_PRJLINK_ID:
          throw new ReadOnlyAttributeException(this.Name, this.AttributeID);
        case ObligatoryObjectAttributes.F_RELATION_TYPE:
          this._DBRelation.RelationType = Convert.ToInt32(value);
          break;
        case ObligatoryObjectAttributes.F_PROJ_ID:
          this._DBRelation.ProjID = Convert.ToInt64(value);
          break;
        default:
          this.ThrowUnknownAttributeException();
          break;
      }
    }
  }

  public override bool ReadOnly
  {
    get
    {
      switch (this.ObligatoryAttribute)
      {
        case ObligatoryObjectAttributes.F_PRJ_GUID:
        case ObligatoryObjectAttributes.F_CREATE_DATE:
        case ObligatoryObjectAttributes.F_RELATION_TYPE:
        case ObligatoryObjectAttributes.F_PART_ID:
        case ObligatoryObjectAttributes.F_PRJLINK_ID:
          return true;
        case ObligatoryObjectAttributes.F_PROJ_ID:
          return this._DBRelation.ReadOnly;
        default:
          this.ThrowUnknownAttributeException();
          return true;
      }
    }
  }
}
