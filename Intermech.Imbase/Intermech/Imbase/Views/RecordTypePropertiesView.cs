// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.RecordTypePropertiesView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class RecordTypePropertiesView : UserControl, IView
{
  private Guid instGuid = new Guid("376B0451-25C0-4c01-B776-BBD52B733AC1");
  private bool _modified;
  private int _imageIndex = -1;
  private IDBTypedObjectID _item;
  private bool _firstLoad = true;
  private int _recordType = -1;
  private CustomFolder _folder;
  private ITabPageForm _pageForm;
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Label label1;
  private Button button1;
  private Button button2;

  public RecordTypePropertiesView()
  {
    this.InitializeComponent();
    if (PropertyFormsHolder.PropertyForms(this.instGuid) == null)
      PropertyFormsHolder.RegisterPropertyForms(this.instGuid);
    if (TabPagesHolder.TabPages(this.instGuid) == null)
      TabPagesHolder.RegisterTabPages(this.instGuid);
    if (!(ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service))
      return;
    this._imageIndex = service.ImageIndex("imgDocumentLayout");
  }

  public bool Modified
  {
    get => this._modified;
    set
    {
      this._modified = value;
      this.button1.Enabled = this.button2.Enabled = value;
    }
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._item = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    this._firstLoad = true;
  }

  public void Activate(IView previousView)
  {
    if (this._firstLoad)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._recordType = RecordTypePropertiesView.GetRecordType(this._item, sessionKeeper.Session);
        if (!this._recordType.Equals(-1))
        {
          this._folder = (CustomFolder) null;
          ObjectTypeProperties propertiesStructure = sessionKeeper.Session.GetObjectType(this._recordType).PropertiesStructure;
          this._folder = (CustomFolder) new ObjectTypeFolder(this.instGuid, propertiesStructure.ObjectTypeName, (object) new TreeView(), propertiesStructure.ObjectType, false, propertiesStructure.ObjectInstanceName, propertiesStructure.Versionable, propertiesStructure.Note, propertiesStructure.DefaultRelation, propertiesStructure.ObjectTypeGuid, propertiesStructure.AreaID, propertiesStructure.CaptionAttribute, propertiesStructure.AnyAttributes, propertiesStructure.ObjectTypeShortName, propertiesStructure.LifetimeReserve, propertiesStructure.Options, propertiesStructure.SchemaID);
          this._folder.LoadData(new Panel(), true);
          ITabPageForm pageProcessingForm = TabPagesHolder.TabPages(this.instGuid).PropertyTabPage.TabPageProcessingForm;
          if (this._pageForm != pageProcessingForm)
          {
            if (this._pageForm is Control)
              this.tableLayoutPanel1.Controls.Remove(this._pageForm as Control);
            this._pageForm = pageProcessingForm;
            if (this._pageForm is Control)
            {
              this.tableLayoutPanel1.Controls.Add(this._pageForm as Control, 0, 1);
              this.tableLayoutPanel1.SetColumnSpan(this._pageForm as Control, 3);
            }
          }
          this._pageForm.FillForm((IFolder) this._folder);
        }
        else
        {
          if (this._pageForm is Control)
            this.tableLayoutPanel1.Controls.Remove(this._pageForm as Control);
          this._pageForm = (ITabPageForm) null;
        }
      }
      this._firstLoad = false;
    }
    EventsHolder.RegisterEvent(this.instGuid, (Delegate) new EventsHolder.WasChangedEventHandler(this.WasChanged));
  }

  public static int GetRecordType(IDBTypedObjectID item, IUserSession session)
  {
    int recordType = -1;
    long objectID = item.ObjectID;
    int num = item.ObjectType;
    if (num.Equals(Intermech.Imbase.Consts.ImbaseTableRefTypeID))
    {
      IDBObject dbObject = session.GetObject(objectID, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
        if (attributeById != null && !attributeById.IsNull)
        {
          objectID = attributeById.AsInteger;
          num = Intermech.Imbase.Consts.ImbaseTableTypeID;
        }
        else
          num = -1;
      }
      else
        num = -1;
    }
    if (num.Equals(Intermech.Imbase.Consts.ImbaseTableTypeID))
    {
      IDBObject dbObject = session.GetObject(objectID, false);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID);
        if (attributeById != null && !attributeById.IsNull)
        {
          Guid anObjectTypeGuid = Guid.Empty;
          try
          {
            anObjectTypeGuid = new Guid(attributeById.AsString);
          }
          catch (Exception ex)
          {
            if (!(ex is FormatException))
              throw ex;
          }
          if (!anObjectTypeGuid.Equals(Guid.Empty))
          {
            IDBObjectType objectType = session.GetObjectType(anObjectTypeGuid, false);
            if (objectType != null)
              recordType = objectType.ObjectType;
          }
        }
      }
    }
    return recordType;
  }

  private void WasChanged(object sender, EventArgs e)
  {
    if (this._folder == null)
      return;
    this.Modified = true;
    this._folder.InChange = true;
  }

  public void Deactivate(IView nextView)
  {
    if (this._folder != null && this.Modified)
    {
      if (MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_101"), (object) this._folder.Text), LocalizationHolder.rm.GetString("Imbase.Client_102"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals((object) DialogResult.Yes))
      {
        this.button1_Click((object) this, EventArgs.Empty);
        if (this.Modified)
          throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Client_103"));
      }
      else
        this.button2_Click((object) this, EventArgs.Empty);
    }
    EventsHolder.UnregisterEvent(this.instGuid, typeof (EventsHolder.WasChangedEventHandler));
  }

  public string Caption => LocalizationHolder.rm.GetString("Imbase.Client_104");

  public int ImageIndex => this._imageIndex;

  public int OrderID => 10100;

  private void button1_Click(object sender, EventArgs e)
  {
    if (this._folder == null || !this.Modified)
      return;
    this.Modified = !this._folder.ApplyData();
  }

  private void button2_Click(object sender, EventArgs e)
  {
    if (this._folder == null || !this.Modified)
      return;
    this._folder.Cancel(true);
    this.Modified = false;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._pageForm is Control)
        this.tableLayoutPanel1.Controls.Remove(this._pageForm as Control);
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RecordTypePropertiesView));
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.button1 = new Button();
    this.button2 = new Button();
    this.label1 = new Label();
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.AccessibleDescription = (string) null;
    this.tableLayoutPanel1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.BackgroundImage = (Image) null;
    this.tableLayoutPanel1.Controls.Add((Control) this.button1, 1, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.button2, 2, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.label1, 0, 0);
    this.tableLayoutPanel1.Font = (Font) null;
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.button1.AccessibleDescription = (string) null;
    this.button1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.BackgroundImage = (Image) null;
    this.button1.Font = (Font) null;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    this.button2.AccessibleDescription = (string) null;
    this.button2.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.BackgroundImage = (Image) null;
    this.button2.Font = (Font) null;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    this.button2.Click += new EventHandler(this.button2_Click);
    this.label1.AccessibleDescription = (string) null;
    this.label1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.label1, 3);
    this.label1.Font = (Font) null;
    this.label1.Name = "label1";
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Font = (Font) null;
    this.Name = nameof (RecordTypePropertiesView);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
