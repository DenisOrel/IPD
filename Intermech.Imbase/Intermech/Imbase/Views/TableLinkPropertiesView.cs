// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.TableLinkPropertiesView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Imbase.Views;

[ViewDescriptionProvider(typeof (TableLinkPropertiesView.TableLinkPropertiesViewDescriptionProvider))]
internal class TableLinkPropertiesView : PropertiesView
{
  private long _objectId;

  public TableLinkPropertiesView()
  {
    this._objectId = 0L;
    this.Subscribe();
  }

  private void Unsubscribe()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectChanged));
  }

  private void Subscribe()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Subscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectChanged));
  }

  private void OnObjectChanged(object sender, NotificationEventArgs ne)
  {
    if (!(ne is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.EventName != "ObjectsChanged" || !objectsEventArgs.ObjectIDs.Contains(this._objectId))
      return;
    long objId1 = this._objID;
    this.GetDataFromNodeId();
    long objId2 = this._objID;
    if (objId1 == objId2)
      return;
    this.LoadData();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.Unsubscribe();
    base.Dispose(disposing);
  }

  protected override void GetDataFromNodeId()
  {
    IDBTypedObjectID data = (IDBTypedObjectID) this._parentNode.GetData(this._nodeID, typeof (IDBTypedObjectID));
    this._objectId = data.ObjectID;
    if (data.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy1 = sessionKeeper.Session.GetObjectActualCopy(data.ObjectID, false);
        if (objectActualCopy1 != null)
        {
          IDBAttribute attributeById = objectActualCopy1.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
          if (attributeById != null && attributeById.Value != null && attributeById.Value != DBNull.Value)
          {
            long int64 = Convert.ToInt64(attributeById.Value);
            IDBObject objectActualCopy2 = sessionKeeper.Session.GetObjectActualCopy(int64, false);
            if (objectActualCopy2 != null)
              this._objID = objectActualCopy2.ObjectID;
            this._objTypeID = Intermech.Imbase.Consts.ImbaseTableTypeID;
          }
          else
          {
            this._objID = 0L;
            this._objTypeID = -1;
          }
        }
      }
    }
    this._prjLinkID = -1L;
  }

  public override string Caption => LocalizationHolder.rm.GetString("ImbaseTableLinkViewCaption");

  public override int OrderID => base.OrderID + 1;

  protected class TableLinkPropertiesViewDescriptionProvider : 
    PropertiesView.PropertiesViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      IServiceProvider serviceProvider)
    {
      ViewDescription viewDescription = base.DoGetViewDescription(selectedItems, serviceProvider);
      viewDescription.Caption = LocalizationHolder.rm.GetString("ImbaseTableLinkViewCaption");
      ++viewDescription.OrderID;
      return viewDescription;
    }
  }
}
