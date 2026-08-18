// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.CreateCopyTableDialog
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class CreateCopyTableDialog : Form
{
  private DataSet _ds;
  private long _prototypeTableID;
  private long _prototypeTableRefID;
  private long _newTableID;
  private long _newTableRefID;
  private long _parentID;
  private int _relationTypeID = -1;
  private long _newRelationID;
  private string _startTableCaption = string.Empty;
  private string _startTableRefCaption = string.Empty;
  private IContainer components;
  private Panel pnlBottom;
  private Button btnCancel;
  private Button btnOk;
  private TabControl tabControl;
  private TabPage pageTable;
  private ObjectPropertyGrid propTablesGrid;
  private TabPage pageLink;
  private ObjectPropertyGrid propLinksGrid;

  public CreateCopyTableDialog(DataSet copyDS, long tableID)
  {
    this.InitializeComponent();
    this._ds = copyDS;
    this._prototypeTableID = tableID;
  }

  public CreateCopyTableDialog(DataSet copyDS, long tableRefID, long parentID, int relationTypeID)
  {
    this.InitializeComponent();
    this._ds = copyDS;
    this._prototypeTableRefID = tableRefID;
    this._parentID = parentID;
    this._relationTypeID = relationTypeID;
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this._prototypeTableID != 0L)
        this.CreatePrototypeTable(sessionKeeper.Session);
      else
        this.CreatePrototypeTableRef(sessionKeeper.Session);
    }
    if (this._newTableRefID != 0L)
      this.propLinksGrid.Load(this._newTableRefID, AttributableElements.Object, GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.CheckVisibility, true, typeof (ObjectAllAttributesGridTab));
    else
      this.tabControl.TabPages.RemoveAt(1);
    if (this._newTableID != 0L)
    {
      this.propTablesGrid.Load(this._newTableID, AttributableElements.Object, GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.CheckVisibility, true, typeof (ObjectAllAttributesGridTab));
    }
    else
    {
      if (this.tabControl.TabPages.Count <= 1)
        return;
      this.tabControl.TabPages.RemoveAt(0);
    }
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.DialogResult == DialogResult.OK)
      {
        INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
        ITablesDisplayService customService1 = sessionKeeper.Session.GetCustomService(typeof (ITablesDisplayService)) as ITablesDisplayService;
        try
        {
          this.propTablesGrid.Save();
          this.propLinksGrid.Save();
          if (this._newTableRefID != 0L)
          {
            IDBObject objectActualCopy1 = sessionKeeper.Session.GetObjectActualCopy(this._newTableRefID, false);
            if (objectActualCopy1.Caption == this._startTableRefCaption)
            {
              IDBObject objectActualCopy2 = sessionKeeper.Session.GetObjectActualCopy(this._newTableID, false);
              if (objectActualCopy2 != null && objectActualCopy2.Caption != this._startTableCaption)
                objectActualCopy1.Caption = objectActualCopy2.Caption;
            }
            if (objectActualCopy1.CheckoutBy == sessionKeeper.Session.UserID)
            {
              try
              {
                objectActualCopy1.CheckIn();
                this._newTableRefID = objectActualCopy1.ObjectID;
                objectActualCopy1 = sessionKeeper.Session.GetObjectActualCopy(this._newTableRefID, false);
                if (this._newTableID != 0L)
                  this._newTableID = sessionKeeper.Session.GetObjectActualCopy(Math.Abs(this._newTableID), false).ObjectID;
              }
              catch (NotUniqueIndexValueException ex)
              {
                ExceptionHelper.ExceptionService.ShowException((Exception) ex);
              }
              this._newTableRefID = objectActualCopy1.ObjectID;
            }
            else if (sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService2)
            {
              customService2.CheckUniqueBeforeTableRefAttrChange(sessionKeeper.Session.SessionGUID, this._newTableRefID, this._newTableID);
              customService2.UpdateAfterTableRefAttrChanged(sessionKeeper.Session.SessionGUID, this._newTableRefID);
            }
            if (customService1 != null)
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._prototypeTableRefID);
              customService1.CloneSettings(objectInfo.VersionGuid, objectActualCopy1.ObjectGUID);
            }
            if (service != null)
            {
              if (this._newTableID != 0L)
                service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsManagedEventArgs("ObjectsCreated", this._newTableID, true));
              service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsManagedEventArgs("ObjectsCreated", this._newTableRefID, true));
              service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", this._newRelationID, this._parentID, this._relationTypeID));
            }
          }
          else if (this._newTableID != 0L)
          {
            IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._newTableID, false);
            if (objectActualCopy.CheckoutBy == sessionKeeper.Session.UserID)
            {
              objectActualCopy.CheckIn();
              this._newTableID = objectActualCopy.ObjectID;
              objectActualCopy = sessionKeeper.Session.GetObject(this._newTableID);
            }
            if (customService1 != null)
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._prototypeTableID);
              customService1.CloneSettings(objectInfo.VersionGuid, objectActualCopy.ObjectGUID);
            }
            service?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsManagedEventArgs("ObjectsCreated", this._newTableID, true));
          }
        }
        catch (Exception ex)
        {
          e.Cancel = true;
          ExceptionHelper.ExceptionService.ShowException(ex);
        }
      }
      else
      {
        long newRelationId = this._newRelationID;
        if (this._newTableRefID != 0L)
          sessionKeeper.Session.GetObjectActualCopy(this._newTableRefID, false).Delete(0L);
        if (this._newTableID != 0L)
          sessionKeeper.Session.GetObjectActualCopy(this._newTableID, false).Delete(0L);
      }
    }
    base.OnClosing(e);
  }

  private void CreatePrototypeTable(IUserSession session)
  {
    IDBObject dbObject = (IDBObject) null;
    try
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableTypeID);
      if (objectCollection == null)
        throw new Exception(LocalizationHolder.rm.GetString("Imbase_TableType_NullCollection"));
      dbObject = session.GetObjectActualCopy(this._prototypeTableID, false) != null ? objectCollection.Create(this._prototypeTableID) : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_NullSourceTable"), (object) Convert.ToString(this._prototypeTableID)));
      if (dbObject == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Prototype_NullNewObject"), (object) this._prototypeTableID));
      List<AttributeValues> attributeValuesList = new List<AttributeValues>(2);
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid != null)
      {
        if (attributeByGuid.AttributeType is IDBAttributeType4 attributeType && attributeType.Required == RequiredModes.AutoRequired)
        {
          if ((attributeType.Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
            attributeValuesList.Add(new AttributeValues(attributeByGuid.AttributeID, (object) 0));
          else
            attributeValuesList.Add(new AttributeValues(attributeByGuid.AttributeID, (object) DBNull.Value));
        }
        else
          attributeByGuid.Delete(0L);
      }
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID);
      if (attributeById != null)
        attributeValuesList.Add(new AttributeValues(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID, (object) ImbaseHelper.CreateInternalTableName(session, Convert.ToString(attributeById.Value))));
      dbObject.SetAttributesValues(attributeValuesList.ToArray());
      TableLoadHelper.ChangeRecordGuids(this._ds.Tables["IMS_DATA"]);
      TableLoadHelper.StoreData(session, dbObject.ObjectID, this._ds, session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
      dbObject.CommitCreation(false, true);
      this._newTableID = dbObject.ObjectID;
      this._startTableCaption = dbObject.Caption;
    }
    catch (Exception ex)
    {
      if (dbObject != null)
      {
        dbObject.Delete(0L);
        this._newTableID = 0L;
      }
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void CreatePrototypeTableRef(IUserSession session)
  {
    this._prototypeTableID = TableLoadHelper.GetTableReference(session, this._prototypeTableRefID);
    this.CreatePrototypeTable(session);
    IDBObject dbObject = (IDBObject) null;
    IDBRelation dbRelation = (IDBRelation) null;
    try
    {
      dbObject = (session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID) ?? throw new Exception(LocalizationHolder.rm.GetString("Imbase_TableRefType_NullCollection"))).Create(this._prototypeTableRefID);
      if (dbObject == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Prototype_NullNewObject"), (object) this._prototypeTableRefID));
      List<AttributeValues> attributeValuesList = new List<AttributeValues>(3);
      if (this._newTableID != 0L)
        attributeValuesList.Add(new AttributeValues(Intermech.Imbase.Consts.ImbaseTableRefAttID, (object) Math.Abs(this._newTableID)));
      QuickObjectInfo objectInfo = session.GetObjectInfo(this._prototypeTableRefID);
      attributeValuesList.Add(new AttributeValues(-50, (object) $"{objectInfo.Caption}{LocalizationHolder.rm.GetString("Imbase_CopyTable_Suffix")}"));
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid != null)
      {
        if (attributeByGuid.AttributeType is IDBAttributeType4 attributeType && attributeType.Required == RequiredModes.AutoRequired)
        {
          if ((attributeType.Options & AttributeOptions.DisableNulls) != AttributeOptions.None)
            attributeValuesList.Add(new AttributeValues(attributeByGuid.AttributeID, (object) 0));
          else
            attributeValuesList.Add(new AttributeValues(attributeByGuid.AttributeID, (object) DBNull.Value));
        }
        else
          attributeByGuid.Delete(0L);
      }
      dbObject.SetAttributesValues(attributeValuesList.ToArray());
      dbRelation = (session.GetRelationCollection(this._relationTypeID) ?? throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Prototype_NullRelationCollection"), (object) this._relationTypeID))).Create(this._parentID, dbObject.ObjectID, DateTime.Now);
      this._newRelationID = dbRelation.RelationID;
      dbObject.CommitCreation(false, true);
      this._newTableRefID = dbObject.ObjectID;
      this._startTableRefCaption = dbObject.Caption;
    }
    catch (Exception ex)
    {
      dbRelation?.Delete(0L);
      if (dbObject != null)
      {
        dbObject.Delete(0L);
        this._newTableRefID = 0L;
      }
      if (this._newTableID != 0L)
      {
        session.GetObjectActualCopy(this._newTableID, false).Delete(0L);
        this._newTableID = 0L;
      }
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CreateCopyTableDialog));
    this.pnlBottom = new Panel();
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.tabControl = new TabControl();
    this.pageTable = new TabPage();
    this.propTablesGrid = new ObjectPropertyGrid();
    this.pageLink = new TabPage();
    this.propLinksGrid = new ObjectPropertyGrid();
    this.pnlBottom.SuspendLayout();
    this.tabControl.SuspendLayout();
    this.pageTable.SuspendLayout();
    this.pageLink.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Controls.Add((Control) this.btnCancel);
    this.pnlBottom.Controls.Add((Control) this.btnOk);
    this.pnlBottom.Name = "pnlBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.tabControl, "tabControl");
    this.tabControl.Controls.Add((Control) this.pageTable);
    this.tabControl.Controls.Add((Control) this.pageLink);
    this.tabControl.Name = "tabControl";
    this.tabControl.SelectedIndex = 0;
    componentResourceManager.ApplyResources((object) this.pageTable, "pageTable");
    this.pageTable.Controls.Add((Control) this.propTablesGrid);
    this.pageTable.Name = "pageTable";
    this.pageTable.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.propTablesGrid, "propTablesGrid");
    this.propTablesGrid.CommandsActiveLinkColor = SystemColors.ActiveCaption;
    this.propTablesGrid.CommandsDisabledLinkColor = SystemColors.ControlDark;
    this.propTablesGrid.CommandsLinkColor = SystemColors.ActiveCaption;
    this.propTablesGrid.InternalMenuEnabled = true;
    this.propTablesGrid.LockTypeChange = false;
    this.propTablesGrid.Name = "propTablesGrid";
    this.propTablesGrid.ToolbarVisible = false;
    componentResourceManager.ApplyResources((object) this.pageLink, "pageLink");
    this.pageLink.Controls.Add((Control) this.propLinksGrid);
    this.pageLink.Name = "pageLink";
    this.pageLink.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.propLinksGrid, "propLinksGrid");
    this.propLinksGrid.CommandsActiveLinkColor = SystemColors.ActiveCaption;
    this.propLinksGrid.CommandsDisabledLinkColor = SystemColors.ControlDark;
    this.propLinksGrid.CommandsLinkColor = SystemColors.ActiveCaption;
    this.propLinksGrid.InternalMenuEnabled = true;
    this.propLinksGrid.LockTypeChange = false;
    this.propLinksGrid.Name = "propLinksGrid";
    this.propLinksGrid.ToolbarVisible = false;
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.tabControl);
    this.Controls.Add((Control) this.pnlBottom);
    this.Name = nameof (CreateCopyTableDialog);
    this.ShowInTaskbar = false;
    this.pnlBottom.ResumeLayout(false);
    this.tabControl.ResumeLayout(false);
    this.pageTable.ResumeLayout(false);
    this.pageLink.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
