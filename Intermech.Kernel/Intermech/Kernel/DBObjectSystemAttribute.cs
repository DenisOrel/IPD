// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectSystemAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;


namespace Intermech.Kernel;

internal class DBObjectSystemAttribute : DBSystemAttribute
{
  private DBObject _DBObject;

  public DBObjectSystemAttribute(
    UserSession uSession,
    ObligatoryObjectAttributes attribute,
    IDBAttributeCollection attrs,
    DBObject obj)
    : base(uSession, attribute, attrs)
  {
    this._ParentObject = (DBAttributable) obj;
    this._DBObject = obj;
    this._DBObjectID = obj.ObjectID;
    this._DB_ID = obj.ID;
  }

  public override long DBObjectID => this._DBObject.ObjectID;

  public override long DB_ID => this._DBObject.ID;

  public override void Clear()
  {
    if (this.ObligatoryAttribute == ObligatoryObjectAttributes.F_SITE_ID)
      this._DBObject.SetSiteID(string.Empty);
    else if (this.ObligatoryAttribute == ObligatoryObjectAttributes.CAPTION)
      this._DBObject.Caption = string.Empty;
    else
      base.Clear();
  }

  private void ThrowUnknownAttributeException()
  {
    throw new KernelException(string.Format(sc_12579.ssp_appserver_12580(), (object) this.ObligatoryAttribute));
  }

  public override string Description
  {
    get => this._DBObject.GetDescriptionsByID((int) this.ObligatoryAttribute, true)[0];
  }

  public override bool IsObjectAttribute => true;

  public override int TypeID => this._DBObject.ObjectType;

  public override object Value
  {
    get => this._DBObject.GetValuesByID((int) this.ObligatoryAttribute, true)[0];
    set
    {
      switch (this.ObligatoryAttribute)
      {
        case ObligatoryObjectAttributes.F_CREATOR_ID:
        case ObligatoryObjectAttributes.F_OBJECT_VER_TYPE:
        case ObligatoryObjectAttributes.F_SITE_ID:
        case ObligatoryObjectAttributes.F_BASE_VERSION:
        case ObligatoryObjectAttributes.F_MODIFICATION_ID:
        case ObligatoryObjectAttributes.F_OBJ_CREATE:
        case ObligatoryObjectAttributes.F_MODIFY_DATE:
        case ObligatoryObjectAttributes.F_LEVEL_ID:
        case ObligatoryObjectAttributes.F_CHKOUT_BY:
        case ObligatoryObjectAttributes.F_VERSION_ID:
        case ObligatoryObjectAttributes.F_ID:
        case ObligatoryObjectAttributes.F_OBJECT_ID:
          throw new ReadOnlyAttributeException(this.Name, this.AttributeID);
        case ObligatoryObjectAttributes.F_ACCESS:
          this._DBObject.AccessLevel = Convert.ToInt32(value);
          break;
        case ObligatoryObjectAttributes.CAPTION:
          this._DBObject.Caption = Convert.ToString(value);
          break;
        case ObligatoryObjectAttributes.F_PROJECT_ID:
          this._DBObject.ProjectID = Convert.ToInt64(value);
          break;
        case ObligatoryObjectAttributes.F_GUID:
          this._DBObject.ObjectGUID = new Guid(value.ToString());
          break;
        case ObligatoryObjectAttributes.F_OWNER_ID:
          this._DBObject.OwnerID = Convert.ToInt64(value);
          break;
        case ObligatoryObjectAttributes.F_OBJECT_TYPE:
          this._DBObject.ObjectType = Convert.ToInt32(value);
          break;
        case ObligatoryObjectAttributes.F_LC_STEP:
          this._DBObject.LCStep = Convert.ToInt32(value);
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
        case ObligatoryObjectAttributes.F_CREATOR_ID:
        case ObligatoryObjectAttributes.F_OBJECT_VER_TYPE:
        case ObligatoryObjectAttributes.F_SITE_ID:
        case ObligatoryObjectAttributes.F_BASE_VERSION:
        case ObligatoryObjectAttributes.F_MODIFICATION_ID:
        case ObligatoryObjectAttributes.F_OBJ_CREATE:
        case ObligatoryObjectAttributes.F_MODIFY_DATE:
        case ObligatoryObjectAttributes.F_LEVEL_ID:
        case ObligatoryObjectAttributes.F_CHKOUT_BY:
        case ObligatoryObjectAttributes.F_VERSION_ID:
        case ObligatoryObjectAttributes.F_ID:
        case ObligatoryObjectAttributes.F_OBJECT_ID:
          return true;
        case ObligatoryObjectAttributes.F_ACCESS:
          return this._DBObject.isReadOnlyAccessLevel();
        case ObligatoryObjectAttributes.CAPTION:
          return this._DBObject.isReadOnlyCaption();
        case ObligatoryObjectAttributes.F_PROJECT_ID:
          return this._DBObject.ReadOnlyProjectID();
        case ObligatoryObjectAttributes.F_GUID:
          return this._DBObject.ReadOnly || !this.UserSession.DeveloperMode;
        case ObligatoryObjectAttributes.F_OWNER_ID:
          return !this._DBObject.CheckAccess(ActionType.TakeOwnership, this._DBObject.GetDefaultAccess(ActionType.TakeOwnership), false);
        case ObligatoryObjectAttributes.F_OBJECT_TYPE:
          return this._DBObject.CheckoutBy != 0L || this._DBObject.ObjectModifyMode == ObjectModifyModes.CantModify || this._DBObject.ObjectModifyMode == ObjectModifyModes.CreateVersion || !this._DBObject.CheckAccess(ActionType.Edit, this._DBObject.GetDefaultAccess(ActionType.Edit), false);
        case ObligatoryObjectAttributes.F_LC_STEP:
          return this._DBObject.CheckoutBy != 0L;
        default:
          this.ThrowUnknownAttributeException();
          return true;
      }
    }
  }
}
