// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadDraftObject
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using ImSSP;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.TechAcad.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechAcad.Connector;

internal class TechAcadDraftObject : SingleThreadedObject, IDraftObject
{
  protected readonly ObjInfoItem _draftInfoItem;
  private string _objectName;
  private readonly NavWindow _navWindow;
  private TechAcadSketchObjectList _sketchList;
  private bool _saveMode;
  private bool _saveStructMode;

  private void Initialize()
  {
  }

  public TechAcadDraftObject([NotNull] ObjInfoItem draftInfoItem, NavWindow navWindow)
  {
    this._draftInfoItem = draftInfoItem;
    this._navWindow = navWindow;
    this.Initialize();
  }

  public virtual long DraftID => this._draftInfoItem.ObjectID;

  public int ObjTypeID => this._draftInfoItem.ObjTypeID;

  public virtual string Name
  {
    get
    {
      if (this._objectName != null)
        return this._objectName;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this.DraftID, new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
        this._objectName = objectAttributeByGuid == null || objectAttributeByGuid.Value == DBNull.Value ? string.Empty : objectAttributeByGuid.AsString;
      }
      return this._objectName;
    }
    set
    {
      if (this.ModifyMode == ModifyMode.CantModify || this.DraftID == -1L)
        return;
      IMSObjectType objectType = MetaDataHelper.GetObjectType(this._draftInfoItem.ObjTypeID);
      if (objectType == null)
        return;
      int attributeId = MetaDataHelper.GetAttributeID((object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
      if (MetaDataHelper.GetAttribute4ObjectType(this._draftInfoItem.ObjTypeID, attributeId) == null && !objectType.AnyAttributes)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.SetObjectAttributesValues(this.DraftID, false, new AttributeValues[1]
        {
          new AttributeValues(attributeId, (object) value)
        });
        this._objectName = value;
      }
    }
  }

  public virtual ModifyMode ModifyMode
  {
    get
    {
      if (this.DraftID == 0L)
        return ModifyMode.CantModify;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.DraftID, false);
        if (dbObject == null)
          return ModifyMode.CantModify;
        switch (dbObject.ObjectModifyMode)
        {
          case ObjectModifyModes.InBase:
            return ModifyMode.InBase;
          case ObjectModifyModes.CreateVersion:
          case ObjectModifyModes.CantModify:
            return ModifyMode.CantModify;
          default:
            return ModifyMode.CheckOut;
        }
      }
    }
  }

  public virtual ISketchCollection SketchCollection
  {
    get
    {
      if (this._sketchList != null)
        return (ISketchCollection) this._sketchList;
      this._sketchList = new TechAcadSketchObjectList((TechAcadTPObject) null);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._draftInfoItem.ObjectID, false);
        if (dbObject != null)
          this._sketchList.LoadSketchCollection(this, (TechAcadTPObject) null, (IDBAttributable) dbObject);
      }
      return (ISketchCollection) this._sketchList;
    }
  }

  public virtual ITPObjectCollection ObjectCollection
  {
    get
    {
      if (this.DraftID == 0L)
        return (ITPObjectCollection) null;
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          Guid attributeID1 = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
          Guid attributeID2 = new Guid("cad0001f-306c-11d8-b4e9-00304f19f545");
          Dictionary<string, ColumnDescriptor> columns = new Dictionary<string, ColumnDescriptor>()
          {
            {
              "F_ID",
              new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
            },
            {
              "F_OBJECT_TYPE",
              new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
            },
            {
              attributeID1.ToString(),
              new ColumnDescriptor((object) attributeID1, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
            },
            {
              attributeID2.ToString(),
              new ColumnDescriptor((object) attributeID2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
            }
          };
          List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(this.DraftID, sessionKeeper.Session, new int[2]
          {
            TechCardConsts.RelTypes.TechRelationID,
            TechCardConsts.RelTypes.TechDraftRelationID
          }, false, (ConditionStructure[]) null, columns);
          if (parentSostavTree == null)
            return (ITPObjectCollection) null;
          int objectTypeId = MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.DraftBaseGUID);
          TechAcadTPObjectList objectCollection = new TechAcadTPObjectList();
          foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
          {
            if (sostavTreeItem != null && !MetaDataHelper.IsObjectTypeChildOf(sostavTreeItem.ObjectTypeID, objectTypeId))
            {
              long int64 = Convert.ToInt64(sostavTreeItem.Values["F_ID"]);
              int int32 = Convert.ToInt32(sostavTreeItem.Values["F_OBJECT_TYPE"]);
              string objName = sostavTreeItem.Values[attributeID1.ToString()].ToString();
              string objDesign = sostavTreeItem.Values[attributeID2.ToString()].ToString();
              objectCollection.Items.Add(new TechAcadTPObject(sostavTreeItem.ProjID, int64, int32, objName, objDesign, this._navWindow));
            }
          }
          return (ITPObjectCollection) objectCollection;
        }
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19155.ssp_techacad_19156() + (object) ex);
        throw;
      }
    }
  }

  public virtual string Extract(int checkOutMode)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._draftInfoItem))
      return string.Empty;
    try
    {
      if (checkOutMode == 1)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this._draftInfoItem.ObjectID, false);
          if (dbObject == null)
            return string.Empty;
          if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
            this._draftInfoItem.ObjectID = dbObject.CheckOut().ObjectID;
        }
      }
      ITechAcadService service = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, false);
      return service != null ? service.ExtractPicture(this.DraftID) : string.Empty;
    }
    catch (Exception ex)
    {
      Plugin.LogError(sc_19155.ssp_techacad_19157() + (object) ex);
      throw;
    }
  }

  public virtual void Close(int needSave)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._draftInfoItem))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._draftInfoItem.ObjectID, false);
      if (dbObject == null || dbObject.ObjectModifyMode == ObjectModifyModes.CantModify)
        return;
      ITechAcadService service1 = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, false);
      IDBTransactions service2 = ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, false);
      service2?.StartTransaction();
      try
      {
        if (needSave == 0)
        {
          service1?.UnloadPicture(this.DraftID);
          if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
            dbObject.CancelChanges();
          service2?.Commit();
        }
        else
        {
          if (dbObject.ObjectModifyMode == ObjectModifyModes.InBase || dbObject.CheckoutBy == sessionKeeper.Session.UserID)
          {
            this._sketchList?.SaveSketchCollection((IDBAttributable) dbObject);
            service1?.UnloadPicture(this.DraftID);
          }
          if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
            dbObject.CheckIn();
          service2?.Commit();
        }
      }
      catch (Exception ex)
      {
        service2?.Rollback();
        Plugin.LogError(sc_19155.ssp_techacad_19158() + (object) ex);
        throw;
      }
    }
  }

  public virtual void Save()
  {
    if (this._saveMode)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.DraftID, false);
      IDBTransactions service = ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, false);
      service?.StartTransaction();
      this._saveMode = true;
      try
      {
        if (this._sketchList != null && dbObject != null)
          this._sketchList.SaveSketchCollection((IDBAttributable) dbObject);
        ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, false)?.SaveOnlyPicture(this.DraftID);
        this._sketchList?.ClearChangeStatus();
        service?.Commit();
      }
      catch (Exception ex)
      {
        service?.Rollback();
        Plugin.LogError(sc_19155.ssp_techacad_19159() + (object) ex);
        throw;
      }
      finally
      {
        this._saveMode = false;
      }
    }
  }

  public virtual void SaveStucture()
  {
    if (this._saveStructMode)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.DraftID, false);
      if (dbObject == null || this._sketchList == null)
        return;
      IDBTransactions service = ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, false);
      service?.StartTransaction();
      this._saveStructMode = true;
      try
      {
        this._sketchList.SaveSketchCollection((IDBAttributable) dbObject);
        this._sketchList.ClearChangeStatus();
        service?.Commit();
      }
      catch (Exception ex)
      {
        service?.Rollback();
        Plugin.LogError(sc_19155.ssp_techacad_19160() + (object) ex);
        throw;
      }
      finally
      {
        this._saveStructMode = false;
      }
    }
  }

  public DraftFileMode FileMode
  {
    get
    {
      ITechAcadService service = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, true);
      if (string.IsNullOrEmpty(service.GetPictureLocalPath(this._draftInfoItem.ObjectID)))
        return DraftFileMode.InBase;
      return !service.IsPictureEditable(this._draftInfoItem.ObjectID) ? DraftFileMode.ReadMode : DraftFileMode.WriteMode;
    }
  }
}
