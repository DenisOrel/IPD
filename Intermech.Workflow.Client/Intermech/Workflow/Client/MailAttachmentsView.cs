// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailAttachmentsView
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Signs.Interfaces;
using Intermech.Workflow.Design;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Workflow.Client;

[ViewDescriptionProvider(typeof (MailAttachmentsView.MailAttachmentsViewDescriptionProvider))]
internal class MailAttachmentsView : BaseAttachmentsView
{
  private bool _fillUnsigned;
  private int _categoryID;
  private int _objectTypeID;
  private List<long> _unsignedObjects;
  private GraphsSet _requiredSigns;
  private Font _boldFont;
  private AbortableBackgroundWorker _worker;
  private WarningControl _WOWarningControl;

  public MailAttachmentsView()
  {
    BaseHolder.NotificationService.Subscribe("RelationsCreated", new NotificationEventHandler(this.RelationsCreatedEvent));
  }

  public override void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    base.Initialize(items, services);
    this._categoryID = MailItemsView.NodeCategoryID(items, services);
    this.ReadOnly = this._categoryID != Intermech.Navigator.Consts.CategoryMailInbox;
  }

  protected override void AdjustObject(ref IDBObject obj, ref bool readOnly)
  {
    base.AdjustObject(ref obj, ref readOnly);
    this._unsignedObjects = (List<long>) null;
    this._objectTypeID = obj != null ? obj.ObjectType : 0;
    this.ShowWOWarning = this._categoryID == Intermech.Navigator.Consts.CategoryMailInbox && this._initialObjectType == wfConsts.WorkOfferTypeID;
    this._requiredSigns = (GraphsSet) null;
    if (this._objectTypeID != wfConsts.ApproveTypeID)
      return;
    IDBAttribute attributeById = obj.GetAttributeByID(wfConsts.AttrRequiredSignsID);
    if (attributeById == null)
      return;
    this._requiredSigns = new RequiredSigns(attributeById).GraphsSet;
  }

  protected override IServiceContainer GetMenuServiceContainer()
  {
    IServiceContainer serviceContainer = base.GetMenuServiceContainer();
    serviceContainer.RemoveService(typeof (GraphsSet));
    if (this._requiredSigns != null)
      serviceContainer.AddService(typeof (GraphsSet), (object) this._requiredSigns);
    return serviceContainer;
  }

  protected override void Loaded()
  {
    base.Loaded();
    this.StopWorker();
    this._fillUnsigned = this._objectTypeID == wfConsts.ApproveTypeID;
  }

  public Font BoldFont
  {
    get
    {
      if (this._boldFont == null)
        this._boldFont = new Font(this.Font, FontStyle.Bold);
      return this._boldFont;
    }
  }

  protected override void GridDynamicFont(object sender, iGDynamicFontEventArgs e)
  {
    base.GridDynamicFont(sender, e);
    if (this._fillUnsigned && this.Width != 0)
    {
      this._fillUnsigned = false;
      this.FillUnsignedObjectsAsync();
    }
    if (this._unsignedObjects == null)
      return;
    INodeID nodeIdForRow = this.GetNodeIDForRow(e.RowIndex);
    if (!(nodeIdForRow is Intermech.Navigator.DBObjects.NodeID))
      return;
    long objectId = ((Intermech.Navigator.DBObjects.NodeID) nodeIdForRow).ObjectID;
    if (!this._unsignedObjects.Contains(objectId) && !this._unsignedObjects.Contains(-objectId))
      return;
    e.Font = this.BoldFont;
  }

  private void RelationsCreatedEvent(object sender, NotificationEventArgs e)
  {
    if (!(e is DBRelationsEventArgs) || !((DBRelationsEventArgs) e).KnownRelationTypes.Contains(wfConsts.SignsRelationTypeID))
      return;
    bool flag = false;
    foreach (long projId in ((DBRelationsEventArgs) e).ProjIDs)
    {
      if (this.Attachments.IndexOfID(projId) != -1)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      return;
    this.FillUnsignedObjectsAsync();
  }

  protected override void Dispose(bool disposing)
  {
    this.StopWorker();
    BaseHolder.NotificationService.Unsubscribe("RelationsCreated", new NotificationEventHandler(this.RelationsCreatedEvent));
    base.Dispose(disposing);
  }

  protected void FillUnsignedObjects()
  {
    this._unsignedObjects = (List<long>) null;
    if (this._objectTypeID != wfConsts.ApproveTypeID)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetObject(this._objectID) is IApproveActivity approveActivity))
        return;
      this._unsignedObjects = approveActivity.GetUnsignedObjects();
      if (this._unsignedObjects.Count != 0)
        return;
      this._unsignedObjects = (List<long>) null;
    }
  }

  protected void FillUnsignedObjectsAsync()
  {
    if (this._worker != null || this._objectTypeID != wfConsts.ApproveTypeID)
      return;
    this._worker = new AbortableBackgroundWorker();
    this._worker.DoWork += (DoWorkEventHandler) ((o, args) => this.FillUnsignedObjects());
    this._worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.WorkerCompleted);
    StatusPopup.Show(Intermech.Workflow.Design.Holder.LoadingImage, LocalizationHolder.rm.GetString("CheckingSigns"), (System.Windows.Forms.Control) this);
    this._worker.RunWorkerAsync();
  }

  private void WorkerCompleted(object o, RunWorkerCompletedEventArgs args)
  {
    StatusPopup.Hide((System.Windows.Forms.Control) this);
    this.Refresh();
    if (this._worker == null)
      return;
    this._worker.Dispose();
    this._worker = (AbortableBackgroundWorker) null;
  }

  protected void StopWorker()
  {
    if (this._worker == null)
      return;
    StatusPopup.Hide((System.Windows.Forms.Control) this);
    this._worker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(this.WorkerCompleted);
    this._worker.Abort();
    this._worker.Dispose();
    this._worker = (AbortableBackgroundWorker) null;
  }

  private bool ShowWOWarning
  {
    get => this._WOWarningControl != null;
    set
    {
      if (value)
      {
        if (this._WOWarningControl != null)
          return;
        this._WOWarningControl = WarningControl.Show((System.Windows.Forms.Control) this);
      }
      else
      {
        if (this._WOWarningControl == null)
          return;
        this._WOWarningControl.Dispose();
        this._WOWarningControl = (WarningControl) null;
      }
    }
  }

  private sealed class MailAttachmentsViewDescriptionProvider : 
    BaseAttachmentsView.BaseAttachmentsViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return base.DoGetViewDescription(selectedItems, serviceProvider);
    }
  }
}
