// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.CreateObjectForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.API;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class CreateObjectForm : Form, IObjectCreatorParams
{
  private long _linkId;
  private long _recordId;
  private long _objectId;
  private System.IServiceProvider _services;
  private long[] _existingObjects;
  private int _objectType;
  private bool _createNew;
  private ICategoryTypeIconService _ctis;
  private IContainer components;
  private Button btCancel;
  private Button btNewWindow;
  private Button btCreateByPrototipe;
  private Button btCreateNew;
  private Label label1;
  private ObjectPropertyGrid objPropertyGrid;
  private Button btShowCard;
  private ListView listView1;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private ColumnHeader columnHeader3;

  internal static void ShowCreateObjectDialog(
    long linkId,
    long recordId,
    System.IServiceProvider services)
  {
    using (CreateObjectForm createObjectForm = new CreateObjectForm())
    {
      createObjectForm.SetData(linkId, recordId, services);
      long objectId1 = createObjectForm.ObjectId;
      if (!createObjectForm.CreateNew)
      {
        if (objectId1 == -1L)
        {
          createObjectForm.CreateObject();
        }
        else
        {
          if (objectId1 == -1L)
            return;
          int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, objectId1, false);
        }
      }
      else
      {
        long[] existingObjects = createObjectForm.ExistingObjects;
        if (existingObjects != null && existingObjects.Length == 0)
        {
          createObjectForm.CreateObject();
        }
        else
        {
          if (createObjectForm.ShowDialog() != DialogResult.OK)
            return;
          long objectId2 = createObjectForm.ObjectId;
          if (objectId2 == -1L)
            return;
          int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, objectId2, true);
        }
      }
    }
  }

  private void SetData(long linkId, long recordId, System.IServiceProvider services)
  {
    this._linkId = linkId;
    this._recordId = recordId;
    this._services = services;
    this._createNew = false;
    this._objectType = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      CadmechHelper.GetServer(session).GetObjectCreateInfo(session.SessionGUID, this._linkId, this._recordId, ref this._createNew, ref this._objectType, ref this._existingObjects);
      this._objectId = -1L;
      try
      {
        this.listView1.BeginUpdate();
        if (this._existingObjects == null)
          return;
        int length = this._existingObjects.Length;
        if (length > 0)
          this._objectId = this._existingObjects[0];
        for (int index = 0; index < length; ++index)
        {
          long existingObject = this._existingObjects[index];
          IDBObject dbObject = session.GetObject(existingObject);
          IMSObjectType objectType = MetaDataHelper.GetObjectType(dbObject.ObjectType);
          if (objectType != null)
          {
            ListViewItem listViewItem = new ListViewItem(objectType.ObjectTypeName);
            listViewItem.SubItems.Add(dbObject.ObjectID.ToString());
            listViewItem.SubItems.Add(dbObject.Caption);
            this.listView1.Items.Add(listViewItem);
            listViewItem.Tag = (object) existingObject;
            if (this._ctis != null)
              listViewItem.ImageIndex = this._ctis.IndexOf(4, dbObject.ObjectType);
          }
        }
        this.columnHeader3.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
      }
      finally
      {
        this.listView1.EndUpdate();
      }
    }
  }

  public long ObjectId => this._objectId;

  public bool CreateNew => this._createNew;

  public long[] ExistingObjects => this._existingObjects;

  public CreateObjectForm()
  {
    this.InitializeComponent();
    this._ctis = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    if (this._ctis == null)
      return;
    this.listView1.SmallImageList = this._ctis.ImageList;
  }

  private void btNewWindow_Click(object sender, EventArgs e)
  {
    long objectId = this.GetObjectId();
    if (objectId == -1L)
      return;
    Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectId), this._services);
    this._objectId = -1L;
    this.DialogResult = DialogResult.Cancel;
  }

  private void btCreateByPrototipe_Click(object sender, EventArgs e)
  {
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    long objectId = this.GetObjectId();
    if (service == null || objectId == -1L)
      return;
    this._objectId = service.CreateObjectByTemplateDialog(objectId);
    this.DialogResult = DialogResult.Cancel;
  }

  public long CreateObject()
  {
    this.btCreateNew_Click((object) this, EventArgs.Empty);
    return this._objectId;
  }

  private void btCreateNew_Click(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service1))
      return;
    service1.AfterDraftCreatedEvent += new AfterDraftCreatedEventHandler(this.cDlg_ObjectCreatorDraftCreatedEvent);
    try
    {
      OpenEditorMode OpenEditor = OpenEditorMode.None;
      this._objectId = service1.CreateObjectByTypeDialog(this._objectType, out OpenEditor, (IObjectCreatorParams) this);
      if (this._objectId != 0L)
      {
        if (this._objectId != -1L)
        {
          if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service2)
          {
            DBObjectsEventArgs e1 = new DBObjectsEventArgs("ObjectsCreated", this._objectId);
            service2.FireEvent((object) null, (NotificationEventArgs) e1);
          }
        }
      }
    }
    finally
    {
      service1.AfterDraftCreatedEvent -= new AfterDraftCreatedEventHandler(this.cDlg_ObjectCreatorDraftCreatedEvent);
    }
    this.DialogResult = DialogResult.Cancel;
  }

  private void cDlg_ObjectCreatorDraftCreatedEvent(object sender, AfterDraftCreatedEventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      string text = ServiceUtils.GetService<IImbaseServer>((object) session, true).FillObjectAttributes(session.SessionGUID, e.ObjectID, this._linkId, this._recordId, false);
      if (text == string.Empty)
        return;
      IOutputView service = ApplicationServices.Container.GetService<IOutputView>();
      if (service == null)
        return;
      service.ShowView();
      service.WriteString("Создание объекта по записи Imbase", text);
      service.Activate("Создание объекта по записи Imbase");
    }
  }

  private long GetObjectId()
  {
    return this.listView1.SelectedItems.Count > 0 ? Convert.ToInt64(this.listView1.SelectedItems[0].Tag) : -1L;
  }

  private void listView1_SelectedIndexChanged(object sender, EventArgs e)
  {
    long objectId = this.GetObjectId();
    bool flag = objectId != -1L;
    this.btCreateByPrototipe.Enabled = flag;
    this.btNewWindow.Enabled = flag;
    this.btShowCard.Enabled = flag;
    if (flag)
      this.objPropertyGrid.Load(objectId, AttributableElements.Object, GetAttributeValuesModes.None, false);
    else
      this.objPropertyGrid.SelectedObject = (object) null;
  }

  private void btShowCard_Click(object sender, EventArgs e)
  {
    long objectId = this.GetObjectId();
    if (objectId == -1L)
      return;
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, objectId, true);
  }

  public bool RawMode => true;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CreateObjectForm));
    this.btCancel = new Button();
    this.btNewWindow = new Button();
    this.btCreateByPrototipe = new Button();
    this.btCreateNew = new Button();
    this.label1 = new Label();
    this.objPropertyGrid = new ObjectPropertyGrid();
    this.btShowCard = new Button();
    this.listView1 = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    this.btCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btNewWindow, "btNewWindow");
    this.btNewWindow.Name = "btNewWindow";
    this.btNewWindow.UseVisualStyleBackColor = true;
    this.btNewWindow.Click += new EventHandler(this.btNewWindow_Click);
    componentResourceManager.ApplyResources((object) this.btCreateByPrototipe, "btCreateByPrototipe");
    this.btCreateByPrototipe.Name = "btCreateByPrototipe";
    this.btCreateByPrototipe.UseVisualStyleBackColor = true;
    this.btCreateByPrototipe.Click += new EventHandler(this.btCreateByPrototipe_Click);
    componentResourceManager.ApplyResources((object) this.btCreateNew, "btCreateNew");
    this.btCreateNew.Name = "btCreateNew";
    this.btCreateNew.UseVisualStyleBackColor = true;
    this.btCreateNew.Click += new EventHandler(this.btCreateNew_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.objPropertyGrid, "objPropertyGrid");
    this.objPropertyGrid.CommandsActiveLinkColor = SystemColors.ActiveCaption;
    this.objPropertyGrid.CommandsDisabledLinkColor = SystemColors.ControlDark;
    this.objPropertyGrid.CommandsLinkColor = SystemColors.ActiveCaption;
    this.objPropertyGrid.InternalMenuEnabled = true;
    this.objPropertyGrid.LockTypeChange = false;
    this.objPropertyGrid.Name = "objPropertyGrid";
    componentResourceManager.ApplyResources((object) this.btShowCard, "btShowCard");
    this.btShowCard.Name = "btShowCard";
    this.btShowCard.UseVisualStyleBackColor = true;
    this.btShowCard.Click += new EventHandler(this.btShowCard_Click);
    componentResourceManager.ApplyResources((object) this.listView1, "listView1");
    this.listView1.Activation = ItemActivation.OneClick;
    this.listView1.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnHeader1,
      this.columnHeader2,
      this.columnHeader3
    });
    this.listView1.FullRowSelect = true;
    this.listView1.GridLines = true;
    this.listView1.HideSelection = false;
    this.listView1.HoverSelection = true;
    this.listView1.Name = "listView1";
    this.listView1.Sorting = SortOrder.Ascending;
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.Details;
    this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.columnHeader3, "columnHeader3");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.listView1);
    this.Controls.Add((Control) this.btShowCard);
    this.Controls.Add((Control) this.objPropertyGrid);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btCreateNew);
    this.Controls.Add((Control) this.btCreateByPrototipe);
    this.Controls.Add((Control) this.btNewWindow);
    this.Controls.Add((Control) this.btCancel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Name = nameof (CreateObjectForm);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
