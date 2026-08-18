// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadDraftObjectList
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Controls;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.TechAcad.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechAcad.Connector;

internal class TechAcadDraftObjectList : SingleThreadedObject, IDraftCollection
{
  private readonly List<TechAcadDraftObject> _items = new List<TechAcadDraftObject>();
  private readonly ITPObject _tpObject;
  private bool _checkObjModifyMode;

  internal List<TechAcadDraftObject> Items => this._items;

  private void InitializeData()
  {
    if (this._tpObject == null)
      return;
    foreach (IMSApplicability typeApplicability in MetaDataHelper.GetObjectTypeApplicabilities(this._tpObject.TPObjectType.ObjTypeID))
    {
      if (typeApplicability.RelationTypeID == TechCardConsts.RelTypes.TechDraftRelationID && MetaDataHelper.IsObjectTypeChildOf(TechCardConsts.ObjectTypes.DraftCadmechID, typeApplicability.ChildObjectTypeID))
      {
        this._checkObjModifyMode = typeApplicability.IsContent;
        break;
      }
    }
  }

  public TechAcadDraftObjectList(ITPObject tpObject)
  {
    this._tpObject = tpObject;
    this.InitializeData();
  }

  public virtual IDraftObject get_Item(int index) => (IDraftObject) this._items[index];

  public virtual int ItemCount => this._items.Count;

  public virtual int ReadOnly
  {
    get
    {
      if (this._tpObject == null || !this._checkObjModifyMode)
        return 0;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._tpObject.ObjID, false);
        if (dbObject == null)
          return 1;
        switch (dbObject.ObjectModifyMode)
        {
          case ObjectModifyModes.InBase:
            return 0;
          case ObjectModifyModes.CantModify:
            return 1;
          default:
            return dbObject.CheckoutBy == 0L ? 1 : 0;
        }
      }
    }
  }

  public virtual IDraftObject Add()
  {
    if (this.ReadOnly != 0)
    {
      Plugin.LogError(string.Format(sc_19155.ssp_techacad_19161(), (object) this._tpObject?.Designation, (object) this._tpObject?.ObjID));
      return (IDraftObject) null;
    }
    List<RelObjInfoItem> source = new List<RelObjInfoItem>();
    TechAcadDraftObject techAcadDraftObject;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(TechCardConsts.ObjectTypes.DraftCadmechGUID);
      if (objectCollection == null)
        return (IDraftObject) null;
      IDBTransactions service1 = ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, false);
      service1?.StartTransaction();
      try
      {
        IDBObject dbObject = objectCollection.Create();
        if (this._tpObject != null)
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(dbObject.ObjectType);
          if (objectType == null)
          {
            service1?.Rollback();
            return (IDraftObject) null;
          }
          int attributeId1 = MetaDataHelper.GetAttributeID((object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
          int attributeId2 = MetaDataHelper.GetAttributeID((object) new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
          List<AttributeValues> attributeValuesList = new List<AttributeValues>();
          if (MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, attributeId1) != null || objectType.AnyAttributes)
            attributeValuesList.Add(new AttributeValues(attributeId1, (object) this._tpObject.Name));
          if (MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, attributeId2) != null || objectType.AnyAttributes)
          {
            string initValue = this._tpObject.Designation;
            if (this._tpObject.ParentObject != null)
            {
              ITPObject parentObject = this._tpObject.ParentObject;
              while (parentObject.ParentObject != null)
                parentObject = parentObject.ParentObject;
              initValue = $"{parentObject.Designation} ({initValue})";
            }
            attributeValuesList.Add(new AttributeValues(attributeId2, (object) initValue));
          }
          if (attributeValuesList.Count != 0)
          {
            AttributeValues[] array = attributeValuesList.ToArray();
            dbObject.SetAttributesValues(array);
          }
        }
        ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, false)?.CreatePicture(dbObject.ObjectID);
        dbObject.CommitCreation(true);
        if (this._tpObject != null)
        {
          IDBRelation dbRel = sessionKeeper.Session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID).Create(this._tpObject.ObjID, dbObject.ObjectID);
          if (dbRel != null)
          {
            ICompositionsAutomaticSortingService service2 = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) sessionKeeper.Session, false);
            if (service2 != null)
            {
              ICompositionsAutomaticSortingSession session = service2.CreateSession((object) sessionKeeper.Session.SessionGUID);
              try
              {
                session.ProceedRelation(new CompositionSortingProjInfo(dbRel.RelationID, dbRel.RelationType, this._tpObject.ObjID, this._tpObject.TPObjectType.ObjTypeID, dbObject.ObjectType, 0L), (object) sessionKeeper.Session.SessionGUID);
              }
              finally
              {
                service2.DisposeSession((object) sessionKeeper.Session.SessionGUID);
              }
            }
            RelObjInfoItem relObjInfoItem = new RelObjInfoItem(dbRel)
            {
              ProjInfo = new ObjInfoItem(this._tpObject.ObjID)
            };
            source.Add(relObjInfoItem);
          }
        }
        service1?.Commit();
        techAcadDraftObject = new TechAcadDraftObject(new ObjInfoItem(dbObject), (NavWindow) null);
        this.Items.Add(techAcadDraftObject);
      }
      catch (Exception ex)
      {
        service1?.Rollback();
        Plugin.LogError(sc_19155.ssp_techacad_19162() + (object) ex);
        throw;
      }
    }
    if (source.Count > 0)
      ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToArray<long>(), (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToArray<long>(), (IList<int>) null, (IList<int>) source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToArray<int>()));
    return (IDraftObject) techAcadDraftObject;
  }

  public virtual void Remove(int index)
  {
    if (this.ReadOnly != 0 || index >= this.ItemCount)
      return;
    TechAcadDraftObject techAcadDraftObject = this.Items[index];
    if (techAcadDraftObject == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions service = ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, false);
      service?.StartTransaction();
      try
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(techAcadDraftObject.DraftID, false);
        if (dbObject != null)
        {
          ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, false)?.UnloadPicture(dbObject.ObjectID);
          dbObject.Delete(0L);
        }
        service?.Commit();
        this.Items.RemoveAt(index);
      }
      catch (Exception ex)
      {
        service?.Rollback();
        Plugin.LogError(sc_19155.ssp_techacad_19163() + (object) ex);
        throw;
      }
    }
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", techAcadDraftObject.DraftID));
  }

  public virtual IDraftObject get_ItemByID(long draftId)
  {
    return this.Items.Cast<IDraftObject>().FirstOrDefault<IDraftObject>((Func<IDraftObject, bool>) (draft => draft.DraftID.Equals(draftId)));
  }
}
