
// Type: Intermech.PropertyEditors.DocObjTypeForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Client.Core.Configurator;
using Intermech.Controls;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Copies;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class DocObjTypeForm : TabPageForm
{
  private bool blockOnChange;
  private PrototypeEditObjectForm prototypeEditObjectForm;
  private PrototypeLinkObjectForm prototypeLinkObjectForm;
  private int rootObjectTypeId;
  private string rootObjectTypeName = string.Empty;
  private int objectType;
  /// <summary>изменился список абонентов</summary>
  private bool subsribersChanged;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private Label label2;
  private Label label1;
  private TextBox additionalFilesEdit;
  private TextBox docFileTypeEdit;
  private Label label3;
  private GroupBox groupBox3;
  private ComboBox typeCodeInDesignation;
  private ComboBox typeNameInStamp;
  private TextBox typeCodeEdit;
  private TextBox typeNameEdit;
  private Label label4;
  private Button btnEdit;
  private Button btnDelete;
  private Button btnAdd;
  private ListView listView;
  private ColumnHeader objectPrototype;
  private ColumnHeader files;
  private Label label6;
  private Label label5;
  private GroupBox groupBox4;
  private Button btnDeleteType;
  private Button btnAddType;
  private ListView listViewObjTypes;
  private ColumnHeader columnHeader1;
  private GroupBox groupBox5;
  private ListView lvSubscribers;
  private ColumnHeader columnHeader2;
  private ColumnHeader columnHeader3;
  private Button btnChange;
  private Button button3;
  private Button btnRemoveSubscriber;
  private Button btnAddLink;

  public DocObjTypeForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.typeNameInStamp.Items.AddRange(new object[2]
    {
      (object) new BoolCBClass(true),
      (object) new BoolCBClass(false)
    });
    this.typeCodeInDesignation.Items.AddRange(new object[2]
    {
      (object) new BoolCBClass(true),
      (object) new BoolCBClass(false)
    });
  }

  private void SetCBFlag(ComboBox cb, bool flag)
  {
    for (int index = 0; index < cb.Items.Count; ++index)
    {
      if (((BoolCBClass) cb.Items[index]).Flag == flag)
      {
        cb.SelectedItem = cb.Items[index];
        break;
      }
    }
  }

  private bool GetCBFlag(ComboBox cb)
  {
    if (cb.SelectedItem != null)
      return ((BoolCBClass) cb.SelectedItem).Flag;
    throw new Exception(LocalizationHolder.rm.GetString("Client.Core_84"));
  }

  public override void FillForm(IFolder folder)
  {
    this._folder = folder as CustomFolder;
    if (StatesController.GetLoadState((object) TabPagesHolder.TabPages(this.instGuid).DocObjTypeTabPage))
      return;
    this.objectType = Convert.ToInt32(folder.Id);
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.rootObjectTypeId = sessionKeeper.Session.IdentHelper.GetObjectTypeID("cad00346-306c-11d8-b4e9-00304f19f545");
      this.rootObjectTypeName = service.GetObjectType(this.rootObjectTypeId).ObjectTypeName;
      if (sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService)) is IDocumentTypeSettingsService customService1)
      {
        this.EnableContainerEditControls();
        DocumentTypeSettings settings = customService1.GetSettings(sessionKeeper.Session.SessionGUID, this.objectType);
        this.blockOnChange = true;
        try
        {
          this.docFileTypeEdit.Text = settings.DocumentFileExt;
          this.additionalFilesEdit.Text = settings.AdditionalDocumentFileExts;
          this.typeNameEdit.Text = settings.DocumentTypeName;
          this.typeCodeEdit.Text = settings.DocumentTypeCode;
          this.SetCBFlag(this.typeNameInStamp, settings.DocumentNameInStamp);
          this.SetCBFlag(this.typeCodeInDesignation, settings.DocumentTypeCodeInDesignation);
          this.listViewObjTypes.SmallImageList = Statics.IconSrv == null ? (this.lvSubscribers.SmallImageList = (ImageList) null) : (this.lvSubscribers.SmallImageList = Statics.IconSrv.ImageList);
          this.listViewObjTypes.Items.Clear();
          if (settings.OutputObjectTypes != string.Empty)
          {
            foreach (string outputObjectType in DocumentTypeSettings.SplitOutputObjectTypes(settings.OutputObjectTypes))
            {
              IDBObjectType objectType = sessionKeeper.Session.GetObjectType(new Guid(outputObjectType), false);
              if (objectType != null)
                this.FillObjectTypesItem(objectType);
            }
          }
        }
        finally
        {
          this.blockOnChange = false;
        }
      }
      else
      {
        this.blockOnChange = true;
        try
        {
          this.docFileTypeEdit.Text = string.Empty;
          this.additionalFilesEdit.Text = string.Empty;
          this.typeNameEdit.Text = string.Empty;
          this.typeCodeEdit.Text = string.Empty;
          this.typeNameInStamp.SelectedItem = (object) null;
          this.typeCodeInDesignation.SelectedItem = (object) null;
        }
        finally
        {
          this.blockOnChange = false;
        }
        this.DisableContainerEditControls();
      }
      this.lvSubscribers.Items.Clear();
      this.btnChange.Enabled = this.btnRemoveSubscriber.Enabled = false;
      if (sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService2)
      {
        this.subsribersChanged = false;
        Dictionary<long, int> subscribers = customService2.GetSubscribers(this.objectType);
        foreach (long key in subscribers.Keys)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(key, false);
          if (dbObject != null)
          {
            int num = subscribers[key];
            if (!string.IsNullOrEmpty(dbObject.Caption))
            {
              string caption = dbObject.Caption;
            }
            else
              $"{MetaDataHelper.GetObjectName(dbObject.ObjectType)} c ID=\"{dbObject.ObjectID}\"";
            ListViewItem listViewItem = this.lvSubscribers.Items.Add(dbObject.Caption);
            if (Statics.IconSrv != null)
              listViewItem.ImageIndex = Statics.IconSrv.IndexOf(4, dbObject.ObjectType);
            listViewItem.Tag = (object) dbObject.ObjectID;
            listViewItem.SubItems.Add(num.ToString());
          }
        }
      }
    }
    PrototypeList prototypeList = new PrototypeList();
    prototypeList.Load(this.objectType);
    this.listView.SmallImageList = Statics.IconSrv == null ? (ImageList) null : Statics.IconSrv.ImageList;
    this.listView.Items.Clear();
    for (int index = 0; index < prototypeList.Count; ++index)
      DocObjTypeForm.FillListViewItem(this.listView.Items.Add(string.Empty), prototypeList[index]);
    StatesController.SetLoadState((object) TabPagesHolder.TabPages(this.instGuid).DocObjTypeTabPage, true);
  }

  public override bool SaveForm(IFolder folder)
  {
    if (StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).DocObjTypeTabPage))
    {
      if (!DocumentTypeSettings.IsValidDocumentFileExt(this.docFileTypeEdit.Text.Trim()))
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_85"));
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService)) is IDocumentTypeSettingsService customService1)
        {
          DocumentTypeSettings documentTypeSettingsData = new DocumentTypeSettings(this.docFileTypeEdit.Text.Trim(), this.additionalFilesEdit.Text.Trim(), this.typeNameEdit.Text.Trim(), this.typeCodeEdit.Text.Trim(), this.GetOutputObjTypes(), this.GetCBFlag(this.typeNameInStamp), this.GetCBFlag(this.typeCodeInDesignation));
          customService1.SetSettings(sessionKeeper.Session.SessionGUID, this.objectType, documentTypeSettingsData);
          DocumentTypeSettings settings = customService1.GetSettings(sessionKeeper.Session.SessionGUID, this.objectType);
          this.blockOnChange = true;
          try
          {
            this.typeNameEdit.Text = settings.DocumentTypeName;
            this.typeCodeEdit.Text = settings.DocumentTypeCode;
          }
          finally
          {
            this.blockOnChange = false;
          }
        }
        if (this.subsribersChanged)
        {
          if (sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService2)
          {
            Dictionary<long, int> list = new Dictionary<long, int>();
            foreach (ListViewItem listViewItem in this.lvSubscribers.Items)
            {
              long int64 = Convert.ToInt64(listViewItem.Tag);
              int int32 = Convert.ToInt32(listViewItem.SubItems[1].Text);
              list.Add(int64, int32);
            }
            customService2.ChangeSubscribers(this.objectType, list, (object) sessionKeeper.Session.SessionGUID);
          }
          this.subsribersChanged = false;
        }
      }
      StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).DocObjTypeTabPage, false);
      ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, true).FireEvent((object) null, (NotificationEventArgs) new DBObjectTypesEventArgs("DocumentTypeSettingsChanged", this.objectType));
    }
    return true;
  }

  private string GetOutputObjTypes()
  {
    string outputObjTypes = string.Empty;
    if (this.listViewObjTypes.Items != null && this.listViewObjTypes.Items.Count > 0)
    {
      bool flag = true;
      foreach (ListViewItem listViewItem in this.listViewObjTypes.Items)
      {
        Guid tag = (Guid) listViewItem.Tag;
        if (flag)
        {
          outputObjTypes += tag.ToString();
          flag = false;
        }
        else
          outputObjTypes = $"{outputObjTypes},{tag.ToString()}";
      }
    }
    return outputObjTypes;
  }

  private void FillObjectTypesItem(IDBObjectType objType)
  {
    ListViewItem listViewItem = new ListViewItem(objType.ObjectTypeName);
    listViewItem.Tag = (object) (objType as IDBGuid).GUID;
    if (Statics.IconSrv != null)
    {
      int num = Statics.IconSrv.IndexOf(4, objType.ObjectType);
      listViewItem.ImageIndex = num;
    }
    this.listViewObjTypes.Items.Add(listViewItem);
  }

  public static void FillListViewItem(ListViewItem lvi, PrototypeClass pc)
  {
    lvi.Text = pc.Caption;
    lvi.Tag = (object) pc;
    if (Statics.IconSrv != null)
    {
      int num = Statics.IconSrv.IndexOf(4, pc.ObjtypeId);
      lvi.ImageIndex = num;
    }
    string text = pc.Files.ToString();
    if (lvi.SubItems.Count == 1)
      lvi.SubItems.Add(text);
    else
      lvi.SubItems[1].Text = text;
  }

  private void CheckPrototypeEditObjectForm()
  {
    if (this.prototypeEditObjectForm != null)
      return;
    this.prototypeEditObjectForm = new PrototypeEditObjectForm();
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
    this.CheckPrototypeEditObjectForm();
    if (this.prototypeEditObjectForm.Execute(this.objectType, true, new PrototypeClass(-1L, Guid.Empty, string.Empty, this.rootObjectTypeId, this.rootObjectTypeName, new BlobInformationList(new BlobInformation[1]))) != DialogResult.OK)
      return;
    DocObjTypeForm.FillListViewItem(this.listView.Items.Add(string.Empty), this.prototypeEditObjectForm.Prototype);
    if (this.prototypeLinkObjectForm == null)
      return;
    this.prototypeLinkObjectForm.ResetData();
  }

  private void CheckPrototypeLinkObjectForm()
  {
    if (this.prototypeLinkObjectForm != null)
      return;
    this.prototypeLinkObjectForm = new PrototypeLinkObjectForm();
  }

  private void btnAddLink_Click(object sender, EventArgs e)
  {
    this.CheckPrototypeLinkObjectForm();
    List<long> exclusions = new List<long>();
    for (int index = 0; index < this.listView.Items.Count; ++index)
      exclusions.Add(((PrototypeClass) this.listView.Items[index].Tag).Id);
    if (this.prototypeLinkObjectForm.Execute(this.objectType, exclusions) != DialogResult.OK)
      return;
    List<PrototypeClass> prototypeClassList = this.prototypeLinkObjectForm.PrototypeClassList;
    for (int index = 0; index < prototypeClassList.Count; ++index)
      DocObjTypeForm.FillListViewItem(this.listView.Items.Add(string.Empty), prototypeClassList[index]);
  }

  private void EnableContainerEditControls()
  {
    this.typeNameEdit.Enabled = true;
    this.typeCodeEdit.Enabled = true;
    this.typeNameInStamp.Enabled = true;
    this.typeCodeInDesignation.Enabled = true;
  }

  private void DisableContainerEditControls()
  {
    this.typeNameEdit.Enabled = false;
    this.typeCodeEdit.Enabled = false;
    this.typeNameInStamp.Enabled = false;
    this.typeCodeInDesignation.Enabled = false;
  }

  private void btnEdit_Click(object sender, EventArgs e)
  {
    if (this.listView.SelectedItems.Count != 1)
      return;
    this.CheckPrototypeEditObjectForm();
    ListViewItem selectedItem = this.listView.SelectedItems[0];
    if (this.prototypeEditObjectForm.Execute(this.objectType, false, (PrototypeClass) selectedItem.Tag) != DialogResult.OK)
      return;
    DocObjTypeForm.FillListViewItem(selectedItem, this.prototypeEditObjectForm.Prototype);
    if (this.prototypeLinkObjectForm == null)
      return;
    this.prototypeLinkObjectForm.ResetData();
  }

  private void btnDelete_Click(object sender, EventArgs e)
  {
    if (this.listView.SelectedItems == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Guid guid = (sessionKeeper.Session.GetObjectType(this.objectType) as IDBGuid).GUID;
      foreach (ListViewItem selectedItem in this.listView.SelectedItems)
      {
        PrototypeClass tag = (PrototypeClass) selectedItem.Tag;
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(tag.Id);
        bool flag = true;
        if (dbObject1 != null)
        {
          IDBAttribute attributeByGuid = dbObject1.GetAttributeByGuid(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null)
          {
            if (attributeByGuid.ValuesCount == 1 && attributeByGuid.Values[0].ToString().ToUpper().Equals(guid.ToString().ToUpper()))
            {
              flag = false;
              if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_87"), $"{LocalizationHolder.rm.GetString("Client.Core_86")}{dbObject1.Caption}?", MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
              {
                IDBObject dbObject2 = sessionKeeper.Session.GetObject(tag.Id, false);
                if (dbObject2 != null)
                {
                  dbObject2.Delete(0L);
                  flag = true;
                  if (this.prototypeLinkObjectForm != null)
                    this.prototypeLinkObjectForm.ResetData();
                }
              }
            }
            else if (attributeByGuid.ValuesCount > 1)
            {
              for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
              {
                attributeByGuid.Index = index;
                if (attributeByGuid.Value.ToString().ToUpper().Equals(guid.ToString().ToUpper()))
                  attributeByGuid.DeleteValue();
              }
            }
          }
        }
        if (flag)
          this.listView.Items.Remove(selectedItem);
      }
    }
  }

  private void docFileTypeEdit_TextChanged(object sender, EventArgs e)
  {
    if (this.blockOnChange)
      return;
    this.SetModified(sender, e);
  }

  private void SetModified(object s, EventArgs e)
  {
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).DocObjTypeTabPage, true);
    EventsHolder.FireWasChanged(s, this.instGuid, e);
  }

  private void btnAddType_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_88"), typeof (ObjectTypeFolder), true);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0)
      return;
    this.listViewObjTypes.BeginUpdate();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (int id in selectorForm.IDList)
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(id, false);
        if (objectType != null && !this.OutObjectTypeIsPresent((objectType as IDBGuid).GUID))
          this.FillObjectTypesItem(objectType);
      }
    }
    this.SetModified((object) this.listViewObjTypes, new EventArgs());
    this.listViewObjTypes.EndUpdate();
  }

  private bool OutObjectTypeIsPresent(Guid checkedObjTypeGuid)
  {
    if (this.listViewObjTypes.Items != null && this.listViewObjTypes.Items.Count > 0)
    {
      foreach (ListViewItem listViewItem in this.listViewObjTypes.Items)
      {
        if (((Guid) listViewItem.Tag).Equals(checkedObjTypeGuid))
          return true;
      }
    }
    return false;
  }

  private void btnDeleteType_Click(object sender, EventArgs e)
  {
    if (this.listViewObjTypes.SelectedItems == null)
      return;
    this.listViewObjTypes.BeginUpdate();
    List<ListViewItem> listViewItemList = new List<ListViewItem>();
    foreach (ListViewItem selectedItem in this.listViewObjTypes.SelectedItems)
      listViewItemList.Add(selectedItem);
    for (int index = 0; index < listViewItemList.Count; ++index)
      this.listViewObjTypes.Items.Remove(listViewItemList[index]);
    this.listViewObjTypes.EndUpdate();
    this.SetModified((object) this.listViewObjTypes, new EventArgs());
  }

  private void panel_Paint(object sender, PaintEventArgs e)
  {
  }

  /// <summary>id раздела справки</summary>
  public override string HelpTopicID => this._folder == null ? base.HelpTopicID : "1023";

  /// <summary>Добавить абонента</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAddSubscriber_Click(object sender, EventArgs e)
  {
    using (AddSubscriberForm addSubscriberForm = new AddSubscriberForm(this.objectType))
    {
      if (addSubscriberForm.ShowDialog() != DialogResult.OK)
        return;
      foreach (ListViewItem listViewItem in this.lvSubscribers.Items)
      {
        if (Convert.ToInt64(listViewItem.Tag) == addSubscriberForm.subscriberID)
        {
          listViewItem.SubItems[1].Text = addSubscriberForm.Amount.ToString();
          this.SetModified((object) this.lvSubscribers, new EventArgs());
          this.subsribersChanged = true;
          return;
        }
      }
      ListViewItem listViewItem1 = this.lvSubscribers.Items.Add(addSubscriberForm.subscriberName);
      listViewItem1.ImageIndex = Statics.IconSrv.IndexOf(4, addSubscriberForm.subscriberTypeID);
      listViewItem1.Tag = (object) addSubscriberForm.subscriberID;
      listViewItem1.SubItems.Add(string.Empty).Text = addSubscriberForm.Amount.ToString();
      this.SetModified((object) this.lvSubscribers, new EventArgs());
      this.subsribersChanged = true;
    }
  }

  /// <summary>Изменить кол-во копий</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnChange_Click(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this.lvSubscribers.SelectedItems[0];
    using (AddSubscriberForm addSubscriberForm = new AddSubscriberForm(this.objectType, selectedItem.Text, Convert.ToInt32(selectedItem.SubItems[1].Text)))
    {
      if (addSubscriberForm.ShowDialog() != DialogResult.OK)
        return;
      selectedItem.SubItems[1].Text = addSubscriberForm.Amount.ToString();
      this.SetModified((object) this.lvSubscribers, new EventArgs());
      this.subsribersChanged = true;
    }
  }

  /// <summary>Удалить абонента</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnRemoveSubscriber_Click(object sender, EventArgs e)
  {
    if (this.lvSubscribers.SelectedItems == null)
      return;
    this.lvSubscribers.BeginUpdate();
    List<ListViewItem> listViewItemList = new List<ListViewItem>();
    foreach (ListViewItem selectedItem in this.lvSubscribers.SelectedItems)
      listViewItemList.Add(selectedItem);
    for (int index = 0; index < listViewItemList.Count; ++index)
      this.lvSubscribers.Items.Remove(listViewItemList[index]);
    this.lvSubscribers.EndUpdate();
    this.subsribersChanged = true;
    this.SetModified((object) this.listViewObjTypes, new EventArgs());
  }

  private void lvSubscribers_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.btnRemoveSubscriber.Enabled = this.lvSubscribers.SelectedItems.Count != 0;
    this.btnChange.Enabled = this.lvSubscribers.SelectedItems.Count != 0;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocObjTypeForm));
    this.panel = new Panel();
    this.groupBox5 = new GroupBox();
    this.lvSubscribers = new ListView();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.btnChange = new Button();
    this.button3 = new Button();
    this.btnRemoveSubscriber = new Button();
    this.groupBox2 = new GroupBox();
    this.groupBox4 = new GroupBox();
    this.btnDeleteType = new Button();
    this.btnAddType = new Button();
    this.listViewObjTypes = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.label6 = new Label();
    this.label5 = new Label();
    this.typeCodeInDesignation = new ComboBox();
    this.typeNameInStamp = new ComboBox();
    this.typeCodeEdit = new TextBox();
    this.typeNameEdit = new TextBox();
    this.label4 = new Label();
    this.label3 = new Label();
    this.groupBox3 = new GroupBox();
    this.btnAddLink = new Button();
    this.btnEdit = new Button();
    this.btnDelete = new Button();
    this.btnAdd = new Button();
    this.listView = new ListView();
    this.objectPrototype = new ColumnHeader();
    this.files = new ColumnHeader();
    this.groupBox1 = new GroupBox();
    this.additionalFilesEdit = new TextBox();
    this.docFileTypeEdit = new TextBox();
    this.label2 = new Label();
    this.label1 = new Label();
    this.panel.SuspendLayout();
    this.groupBox5.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.groupBox4.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.panel.Controls.Add((Control) this.groupBox5);
    this.panel.Controls.Add((Control) this.groupBox2);
    this.panel.Controls.Add((Control) this.groupBox1);
    componentResourceManager.ApplyResources((object) this.panel, "panel");
    this.panel.Name = "panel";
    this.panel.Paint += new PaintEventHandler(this.panel_Paint);
    this.groupBox5.Controls.Add((Control) this.lvSubscribers);
    this.groupBox5.Controls.Add((Control) this.btnChange);
    this.groupBox5.Controls.Add((Control) this.button3);
    this.groupBox5.Controls.Add((Control) this.btnRemoveSubscriber);
    componentResourceManager.ApplyResources((object) this.groupBox5, "groupBox5");
    this.groupBox5.Name = "groupBox5";
    this.groupBox5.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lvSubscribers, "lvSubscribers");
    this.lvSubscribers.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader2,
      this.columnHeader3
    });
    this.lvSubscribers.FullRowSelect = true;
    this.lvSubscribers.HideSelection = false;
    this.lvSubscribers.MultiSelect = false;
    this.lvSubscribers.Name = "lvSubscribers";
    this.lvSubscribers.UseCompatibleStateImageBehavior = false;
    this.lvSubscribers.View = View.Details;
    this.lvSubscribers.SelectedIndexChanged += new EventHandler(this.lvSubscribers_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.columnHeader3, "columnHeader3");
    componentResourceManager.ApplyResources((object) this.btnChange, "btnChange");
    this.btnChange.Name = "btnChange";
    this.btnChange.UseVisualStyleBackColor = true;
    this.btnChange.Click += new EventHandler(this.btnChange_Click);
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.Name = "button3";
    this.button3.UseVisualStyleBackColor = true;
    this.button3.Click += new EventHandler(this.btnAddSubscriber_Click);
    componentResourceManager.ApplyResources((object) this.btnRemoveSubscriber, "btnRemoveSubscriber");
    this.btnRemoveSubscriber.Name = "btnRemoveSubscriber";
    this.btnRemoveSubscriber.UseVisualStyleBackColor = true;
    this.btnRemoveSubscriber.Click += new EventHandler(this.btnRemoveSubscriber_Click);
    this.groupBox2.Controls.Add((Control) this.groupBox4);
    this.groupBox2.Controls.Add((Control) this.label6);
    this.groupBox2.Controls.Add((Control) this.label5);
    this.groupBox2.Controls.Add((Control) this.typeCodeInDesignation);
    this.groupBox2.Controls.Add((Control) this.typeNameInStamp);
    this.groupBox2.Controls.Add((Control) this.typeCodeEdit);
    this.groupBox2.Controls.Add((Control) this.typeNameEdit);
    this.groupBox2.Controls.Add((Control) this.label4);
    this.groupBox2.Controls.Add((Control) this.label3);
    this.groupBox2.Controls.Add((Control) this.groupBox3);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    this.groupBox4.Controls.Add((Control) this.btnDeleteType);
    this.groupBox4.Controls.Add((Control) this.btnAddType);
    this.groupBox4.Controls.Add((Control) this.listViewObjTypes);
    componentResourceManager.ApplyResources((object) this.groupBox4, "groupBox4");
    this.groupBox4.Name = "groupBox4";
    this.groupBox4.TabStop = false;
    componentResourceManager.ApplyResources((object) this.btnDeleteType, "btnDeleteType");
    this.btnDeleteType.Name = "btnDeleteType";
    this.btnDeleteType.UseVisualStyleBackColor = true;
    this.btnDeleteType.Click += new EventHandler(this.btnDeleteType_Click);
    componentResourceManager.ApplyResources((object) this.btnAddType, "btnAddType");
    this.btnAddType.Name = "btnAddType";
    this.btnAddType.UseVisualStyleBackColor = true;
    this.btnAddType.Click += new EventHandler(this.btnAddType_Click);
    componentResourceManager.ApplyResources((object) this.listViewObjTypes, "listViewObjTypes");
    this.listViewObjTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.listViewObjTypes.FullRowSelect = true;
    this.listViewObjTypes.HideSelection = false;
    this.listViewObjTypes.Name = "listViewObjTypes";
    this.listViewObjTypes.UseCompatibleStateImageBehavior = false;
    this.listViewObjTypes.View = View.Details;
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    this.typeCodeInDesignation.DropDownStyle = ComboBoxStyle.DropDownList;
    this.typeCodeInDesignation.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.typeCodeInDesignation, "typeCodeInDesignation");
    this.typeCodeInDesignation.Name = "typeCodeInDesignation";
    this.typeCodeInDesignation.TextChanged += new EventHandler(this.docFileTypeEdit_TextChanged);
    this.typeNameInStamp.DropDownStyle = ComboBoxStyle.DropDownList;
    this.typeNameInStamp.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.typeNameInStamp, "typeNameInStamp");
    this.typeNameInStamp.Name = "typeNameInStamp";
    this.typeNameInStamp.TextChanged += new EventHandler(this.docFileTypeEdit_TextChanged);
    componentResourceManager.ApplyResources((object) this.typeCodeEdit, "typeCodeEdit");
    this.typeCodeEdit.Name = "typeCodeEdit";
    this.typeCodeEdit.TextChanged += new EventHandler(this.docFileTypeEdit_TextChanged);
    componentResourceManager.ApplyResources((object) this.typeNameEdit, "typeNameEdit");
    this.typeNameEdit.Name = "typeNameEdit";
    this.typeNameEdit.TextChanged += new EventHandler(this.docFileTypeEdit_TextChanged);
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    this.groupBox3.Controls.Add((Control) this.btnAddLink);
    this.groupBox3.Controls.Add((Control) this.btnEdit);
    this.groupBox3.Controls.Add((Control) this.btnDelete);
    this.groupBox3.Controls.Add((Control) this.btnAdd);
    this.groupBox3.Controls.Add((Control) this.listView);
    componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.TabStop = false;
    componentResourceManager.ApplyResources((object) this.btnAddLink, "btnAddLink");
    this.btnAddLink.Name = "btnAddLink";
    this.btnAddLink.UseVisualStyleBackColor = true;
    this.btnAddLink.Click += new EventHandler(this.btnAddLink_Click);
    componentResourceManager.ApplyResources((object) this.btnEdit, "btnEdit");
    this.btnEdit.Name = "btnEdit";
    this.btnEdit.UseVisualStyleBackColor = true;
    this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    componentResourceManager.ApplyResources((object) this.listView, "listView");
    this.listView.Columns.AddRange(new ColumnHeader[2]
    {
      this.objectPrototype,
      this.files
    });
    this.listView.FullRowSelect = true;
    this.listView.HideSelection = false;
    this.listView.Name = "listView";
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    componentResourceManager.ApplyResources((object) this.objectPrototype, "objectPrototype");
    componentResourceManager.ApplyResources((object) this.files, "files");
    this.groupBox1.Controls.Add((Control) this.additionalFilesEdit);
    this.groupBox1.Controls.Add((Control) this.docFileTypeEdit);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.additionalFilesEdit, "additionalFilesEdit");
    this.additionalFilesEdit.Name = "additionalFilesEdit";
    this.additionalFilesEdit.TextChanged += new EventHandler(this.docFileTypeEdit_TextChanged);
    componentResourceManager.ApplyResources((object) this.docFileTypeEdit, "docFileTypeEdit");
    this.docFileTypeEdit.Name = "docFileTypeEdit";
    this.docFileTypeEdit.TextChanged += new EventHandler(this.docFileTypeEdit_TextChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel);
    this.Name = nameof (DocObjTypeForm);
    this.Tag = (object) "  ";
    this.panel.ResumeLayout(false);
    this.groupBox5.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.groupBox4.ResumeLayout(false);
    this.groupBox3.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }
}
