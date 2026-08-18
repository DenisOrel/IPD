
// Type: Intermech.PropertyEditors.Attr4ObjTypeForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DatabaseConfigurator;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Форма редактирования атрибутов на типы объектов.</summary>
public class Attr4ObjTypeForm : TabPageForm, IPositionAssigner
{
  private ListView listView;
  private ColumnHeader attrColumnHeader;
  private PropertyGrid propertyGrid;
  private IContainer components;
  private Splitter splitter1;
  private int lastAttributeID = -1;
  private ListViewItem lastLVI;
  private Attr4ObjTypeList attr4ObjTypeList;
  private Attr4ObjTypeList attr4ObjTypeListOld;
  private ContextMenuStrip contextMenuStrip;
  private ToolStripMenuItem addItem;
  private ToolStripMenuItem deleteItem;

  /// <summary>
  /// 
  /// </summary>
  public Attr4ObjTypeList Attr4ObjTypeList => this.attr4ObjTypeList;

  /// <summary>Конструктор.</summary>
  /// <param name="aInstGuid"></param>
  public Attr4ObjTypeForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.attr4ObjTypeList = new Attr4ObjTypeList(new EventsHolder.GetListDelegate(this.GetMasterListProc));
    this.attr4ObjTypeListOld = new Attr4ObjTypeList(new EventsHolder.GetListDelegate(this.GetMasterListProc));
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    if (service == null)
      return;
    this.contextMenuStrip.ImageList = service.ImageList;
    this.addItem.ImageIndex = service.ImageIndex("imgInsertItem");
    this.deleteItem.ImageIndex = service.ImageIndex("imgDelete");
  }

  /// <summary>Clean up any resources being used.</summary>
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Attr4ObjTypeForm));
    this.listView = new ListView();
    this.attrColumnHeader = new ColumnHeader();
    this.contextMenuStrip = new ContextMenuStrip(this.components);
    this.addItem = new ToolStripMenuItem();
    this.deleteItem = new ToolStripMenuItem();
    this.propertyGrid = new PropertyGrid();
    this.splitter1 = new Splitter();
    this.contextMenuStrip.SuspendLayout();
    this.SuspendLayout();
    this.listView.Columns.AddRange(new ColumnHeader[1]
    {
      this.attrColumnHeader
    });
    this.listView.ContextMenuStrip = this.contextMenuStrip;
    componentResourceManager.ApplyResources((object) this.listView, "listView");
    this.listView.HideSelection = false;
    this.listView.MultiSelect = false;
    this.listView.Name = "listView";
    this.listView.Sorting = SortOrder.Ascending;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.listView.SelectedIndexChanged += new EventHandler(this.OnlistView_SelectedIndexChanged);
    this.listView.DoubleClick += new EventHandler(this.listView_DoubleClick);
    componentResourceManager.ApplyResources((object) this.attrColumnHeader, "attrColumnHeader");
    this.contextMenuStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.addItem,
      (ToolStripItem) this.deleteItem
    });
    this.contextMenuStrip.Name = "contextMenuStrip";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip, "contextMenuStrip");
    this.addItem.Name = "addItem";
    componentResourceManager.ApplyResources((object) this.addItem, "addItem");
    this.addItem.Click += new EventHandler(this.OnaddItem_Click);
    this.deleteItem.Name = "deleteItem";
    componentResourceManager.ApplyResources((object) this.deleteItem, "deleteItem");
    this.deleteItem.Click += new EventHandler(this.OndeleteItem_Click);
    this.propertyGrid.CategoryForeColor = SystemColors.InactiveCaptionText;
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.LineColor = SystemColors.ScrollBar;
    this.propertyGrid.Name = "propertyGrid";
    this.propertyGrid.PropertySort = PropertySort.Alphabetical;
    this.propertyGrid.Tag = (object) "       ";
    this.propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.OnpropertyGrid_PropertyValueChanged);
    this.propertyGrid.Click += new EventHandler(this.OnpropertyGrid_Click);
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.Controls.Add((Control) this.propertyGrid);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.listView);
    this.Name = nameof (Attr4ObjTypeForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "    ";
    this.contextMenuStrip.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool AddAttr4ObjType()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag = false;
      AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true);
      if (attributesSelectDlg.ShowDialog() == DialogResult.OK && attributesSelectDlg.SelectedAttributesID.Count > 0)
      {
        IDBAttributeTypeCollection attributeTypeCollection = sessionKeeper.Session.GetAttributeTypeCollection(-1, false);
        for (int index1 = 0; index1 < attributesSelectDlg.SelectedAttributesID.Count; ++index1)
        {
          Attr4ObjTypeClass[] attr4ObjTypeArray = this.GetAddAttr4ObjTypeArray(attributesSelectDlg.SelectedAttributesID[index1], attributeTypeCollection);
          if (attr4ObjTypeArray != null)
          {
            for (int index2 = 0; index2 < attr4ObjTypeArray.Length; ++index2)
            {
              Attr4ObjTypeClass attr4ObjTypeClass = attr4ObjTypeArray[index2];
              this.attr4ObjTypeList.Add((object) attr4ObjTypeClass);
              ListViewItem lvi = new ListViewItem(attr4ObjTypeClass.AttributeName);
              lvi.Tag = (object) attr4ObjTypeClass;
              this.SetIcon(lvi, attr4ObjTypeClass.AttributeTypeProperties.FieldType);
              this.listView.Items.Add(lvi);
              this.listView.SelectedItems.Clear();
              lvi.Selected = true;
              flag = true;
            }
          }
          else
          {
            string Message = $"{LocalizationHolder.rm.GetString("Client.Core_55")}{sessionKeeper.Session.GetAttributeType(attributesSelectDlg.SelectedAttributesID[index1]).Name}\"";
            int num = (int) IMMessageBox.Show(MessageDialogs.msgError, Message, MessageBoxButtons.OK, IMMessageBoxImage.Error);
          }
        }
      }
      return flag;
    }
  }

  private void SetIcon(ListViewItem lvi, FieldTypes fieldType)
  {
    int num = Statics.IconSrv.IndexOf(3, -1, (object) fieldType);
    lvi.ImageIndex = num;
  }

  /// <summary>
  /// 
  /// </summary>
  private void ClearForm()
  {
  }

  /// <summary>назначаем флаг по attr4ObjTypeListOld</summary>
  private void DefineFilteredAttributes()
  {
    for (int index = 0; index < this.attr4ObjTypeListOld.Count; ++index)
      ((Attr4TypeClass) this.attr4ObjTypeListOld[index]).Tag = (object) true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool DeleteAttr4ObjType()
  {
    ListView.SelectedListViewItemCollection selectedItems = this.listView.SelectedItems;
    if (selectedItems.Count == 0 || IMMessageBox.Show(MessageDialogs.msgConfirmDelete, MessageDialogs.msgReallyDelete, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return false;
    int result = -1;
    if (this._folder.Id != null)
      int.TryParse(this._folder.Id.ToString(), out result);
    this.lastLVI = (ListViewItem) null;
    while (selectedItems.Count > 0)
    {
      int index1 = this.attr4ObjTypeList.IndexOf(selectedItems[0].Tag);
      if (index1 != -1)
      {
        if (((Attr4ObjTypeClass) this.attr4ObjTypeList[index1]).AttributeTypeProperties.FieldType == FieldTypes.ftObjectLink)
        {
          for (int index2 = 0; index2 < this.attr4ObjTypeList.Count; ++index2)
          {
            if (index2 != index1 && ((Attr4ObjTypeClass) this.attr4ObjTypeList[index2]).Attribute4ObjectTypeProperties.MasterAttributeID == ((Attr4ObjTypeClass) this.attr4ObjTypeList[index1]).Attribute4ObjectTypeProperties.AttributeID)
            {
              Attribute4ObjectTypeProperties objectTypeProperties = ((Attr4ObjTypeClass) this.attr4ObjTypeList[index2]).Attribute4ObjectTypeProperties with
              {
                MasterAttributeID = 0,
                SourceAttributeID = 0
              };
              ((Attr4ObjTypeClass) this.attr4ObjTypeList[index2]).Attribute4ObjectTypeProperties = objectTypeProperties;
            }
          }
        }
        this.attr4ObjTypeList.RemoveAt(index1);
      }
      selectedItems[0].Remove();
    }
    if (this.listView.Items.Count > 0)
      this.listView.Items[0].Selected = true;
    else
      this.propertyGrid.SelectedObject = (object) null;
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  private void FillListView()
  {
    this.listView.BeginUpdate();
    try
    {
      this.listView.Items.Clear();
      this.listView.SmallImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
      for (int index = 0; index < this.attr4ObjTypeList.Count; ++index)
      {
        if ((bool) ((Attr4TypeClass) this.attr4ObjTypeListOld[index]).Tag)
        {
          ListViewItem lvi = new ListViewItem(((Attr4TypeClass) this.attr4ObjTypeList[index]).AttributeName);
          lvi.Tag = (object) (Attr4ObjTypeClass) this.attr4ObjTypeList[index];
          this.SetIcon(lvi, ((Attr4ObjTypeClass) this.attr4ObjTypeList[index]).AttributeTypeProperties.FieldType);
          this.listView.Items.Add(lvi);
        }
      }
    }
    finally
    {
      this.listView.EndUpdate();
      this.lastLVI = (ListViewItem) null;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="list"></param>
  /// <param name="masterId"></param>
  /// <returns></returns>
  private int FindIndexByMasterId(Attr4ObjTypeList list, int masterId)
  {
    for (int index = 0; index < list.Count; ++index)
    {
      if (((Attr4ObjTypeClass) list[index]).Attribute4ObjectTypeProperties.MasterAttributeID == masterId)
        return index;
    }
    return -1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FixStates(object sender, EventArgs e)
  {
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage, true);
    EventsHolder.FireWasChanged(sender, this.instGuid, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aAttributeID"></param>
  /// <returns></returns>
  private int GetListViewIndexByAttributeId(int aAttributeID)
  {
    int indexByAttributeId = -1;
    for (int index = 0; index < this.listView.Items.Count; ++index)
    {
      if (((Attr4ObjTypeClass) this.listView.Items[index].Tag).Attribute4ObjectTypeProperties.AttributeID == aAttributeID)
      {
        indexByAttributeId = index;
        break;
      }
    }
    return indexByAttributeId;
  }

  /// <summary>Собираем список мастер атрибутов для показа.</summary>
  /// <param name="s"></param>
  /// <param name="args"></param>
  /// <returns></returns>
  private ArrayList GetMasterListProc(object s, params object[] args)
  {
    ArrayList masterListProc = new ArrayList();
    int num = -1;
    if (this.listView.SelectedItems.Count > 0)
      num = ((Attr4ObjTypeClass) this.listView.SelectedItems[0].Tag).Attribute4ObjectTypeProperties.AttributeID;
    for (int index = 0; index < this.attr4ObjTypeList.Count; ++index)
    {
      if (((Attr4ObjTypeClass) this.attr4ObjTypeList[index]).Attribute4ObjectTypeProperties.MasterAttributeID == num)
        return masterListProc;
    }
    masterListProc.Add((object) new AttributePropertyClass(0));
    for (int index = 0; index < this.attr4ObjTypeList.Count; ++index)
    {
      Attr4ObjTypeClass attr4ObjType = (Attr4ObjTypeClass) this.attr4ObjTypeList[index];
      int attributeId = attr4ObjType.Attribute4ObjectTypeProperties.AttributeID;
      if (attr4ObjType.AttributeTypeProperties.FieldType == FieldTypes.ftObjectLink && num != attributeId && attr4ObjType.Attribute4ObjectTypeProperties.MasterAttributeID == 0)
        masterListProc.Add((object) new AttributePropertyClass(attributeId));
    }
    return masterListProc;
  }

  /// <summary>
  /// сортируем:
  /// attr4ObjTypeList - сначала master, потом остальное - для правильного добавления
  /// attr4ObjTypeListOld - сначала остальное, потом master - для правильного удаления удаленных
  /// </summary>
  private void SortListsByMaster()
  {
    int startIndex = 0;
    int num1 = 0;
    for (int index = 0; index < this.attr4ObjTypeListOld.Count; ++index)
    {
      if (this.FindIndexByMasterId(this.attr4ObjTypeListOld, ((Attr4ObjTypeClass) this.attr4ObjTypeListOld[index]).Attribute4ObjectTypeProperties.AttributeID) == -1)
      {
        ++num1;
        if (index > 0)
        {
          Attr4ObjTypeClass attr4ObjTypeClass = (Attr4ObjTypeClass) this.attr4ObjTypeListOld[index];
          this.attr4ObjTypeListOld.RemoveAt(index);
          this.attr4ObjTypeListOld.Insert(0, (object) attr4ObjTypeClass);
        }
      }
    }
    int finishIndex1 = startIndex + num1 - 1;
    this.attr4ObjTypeListOld.SortByAttrAtFormula(startIndex, finishIndex1, false);
    int finishIndex2 = this.attr4ObjTypeList.Count - 1;
    int num2 = 0;
    for (int index = 0; index < this.attr4ObjTypeList.Count; ++index)
    {
      if (this.FindIndexByMasterId(this.attr4ObjTypeList, ((Attr4ObjTypeClass) this.attr4ObjTypeList[index]).Attribute4ObjectTypeProperties.AttributeID) != -1)
      {
        if (index > 0)
        {
          Attr4ObjTypeClass attr4ObjType = (Attr4ObjTypeClass) this.attr4ObjTypeList[index];
          this.attr4ObjTypeList.RemoveAt(index);
          this.attr4ObjTypeList.Insert(0, (object) attr4ObjType);
        }
      }
      else
        ++num2;
    }
    this.attr4ObjTypeList.SortByAttrAtFormula(finishIndex2 - num2 + 1, finishIndex2, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="folder"></param>
  public override void FillForm(IFolder folder)
  {
    this._folder = folder as CustomFolder;
    if (StatesController.GetLoadState((object) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage))
      return;
    this.ClearForm();
    this.LoadAttr4ObjTypeList();
    this.FillListView();
    this.listView.SelectedItems.Clear();
    if (this.listView.Items.Count > 0)
    {
      int indexByAttributeId = this.GetListViewIndexByAttributeId(this.lastAttributeID);
      if (indexByAttributeId != -1)
      {
        this.listView.Items[indexByAttributeId].Selected = true;
        this.listView.EnsureVisible(indexByAttributeId);
      }
      else
        this.listView.Items[0].Selected = true;
    }
    else
      this.propertyGrid.SelectedObject = (object) null;
    StatesController.SetLoadState((object) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="folder"></param>
  /// <returns></returns>
  public override bool SaveForm(IFolder folder)
  {
    List<int> addedIDs = new List<int>(0);
    List<int> changedIDs = new List<int>(0);
    List<int> removedIDs = new List<int>(0);
    if (StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage))
    {
      if (this.lastLVI != null)
        ((Attr4ObjTypeClass) this.lastLVI.Tag).SaveValues();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        using (RemoteLock remoteLock = new RemoteLock())
        {
          IDBObjectType serverObject = this._folder.GetServerObject(sessionKeeper.Session) as IDBObjectType;
          remoteLock.Add((object) serverObject);
          IDBAttribute4ObjectTypeCollection attributes1 = serverObject.Attributes as IDBAttribute4ObjectTypeCollection;
          remoteLock.Add((object) attributes1);
          this.SortListsByMaster();
          int index1 = 0;
          while (index1 < this.attr4ObjTypeListOld.Count)
          {
            if ((bool) ((Attr4TypeClass) this.attr4ObjTypeListOld[index1]).Tag)
            {
              int attributeId = ((Attr4ObjTypeClass) this.attr4ObjTypeListOld[index1]).Attribute4ObjectTypeProperties.AttributeID;
              if (this.attr4ObjTypeList.IndexOfByAttributeID(attributeId) == -1)
              {
                if (((Attr4ObjTypeClass) this.attr4ObjTypeListOld[index1]).AttributeTypeProperties.FieldType == FieldTypes.ftObjectLink)
                {
                  for (int index2 = 0; index2 < this.attr4ObjTypeListOld.Count; ++index2)
                  {
                    if (((Attr4ObjTypeClass) this.attr4ObjTypeListOld[index2]).Attribute4ObjectTypeProperties.MasterAttributeID == ((Attr4ObjTypeClass) this.attr4ObjTypeListOld[index1]).Attribute4ObjectTypeProperties.AttributeID)
                    {
                      Attribute4ObjectTypeProperties objectTypeProperties = ((Attr4ObjTypeClass) this.attr4ObjTypeListOld[index2]).Attribute4ObjectTypeProperties;
                      if (attributes1.GetAttributeByID(objectTypeProperties.AttributeID) is IDBAttributeType4Object attributeById)
                      {
                        objectTypeProperties.MasterAttributeID = 0;
                        objectTypeProperties.SourceAttributeID = 0;
                        attributeById.Attribute4ObjectPropertiesStructure = objectTypeProperties;
                        ((Attr4ObjTypeClass) this.attr4ObjTypeListOld[index2]).Attribute4ObjectTypeProperties = objectTypeProperties;
                      }
                    }
                  }
                }
                IDBAttributeType4Object attributeById1 = attributes1.GetAttributeByID(attributeId) as IDBAttributeType4Object;
                using (new RemoteLock((object) attributeById1))
                {
                  int DeleteMode = 0;
                  string Message = string.Format(LocalizationHolder.rm.GetString("Client.Core_51"), (object) attributeById1.Name);
                  if (!attributeById1.IsContent)
                  {
                    bool flag = false;
                    CustomFolder tag = this._folder.NodeParent.Tag as CustomFolder;
                    if (tag is ObjectTypeFolder)
                    {
                      int id = (int) tag.Id;
                      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(id);
                      if (objectType != null && objectType.Attributes is IDBAttribute4ObjectTypeCollection attributes2 && attributes2.GetAttributeByID(attributeId) is IDBAttributeType4Object attributeById2 && (attributeById2.InheritMode == InheritModes.Public || attributeById2.InheritMode == InheritModes.Inherited))
                        flag = true;
                    }
                    if (!flag && IMMessageBox.Show(MessageDialogs.msgConfirmDelete, Message, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
                      DeleteMode = Intermech.Consts.DeleteInstances;
                  }
                  attributeById1.Delete((long) DeleteMode);
                  this.attr4ObjTypeListOld.RemoveAt(index1);
                  removedIDs.Add(attributeById1.AttributeID);
                  continue;
                }
              }
            }
            ++index1;
          }
          for (int index3 = 0; index3 < this.attr4ObjTypeList.Count; ++index3)
          {
            int index4 = this.attr4ObjTypeListOld.IndexOfByAttributeID(((Attr4ObjTypeClass) this.attr4ObjTypeList[index3]).Attribute4ObjectTypeProperties.AttributeID);
            if (index4 == -1 || ((Attr4ObjTypeClass) this.attr4ObjTypeListOld[index4]).Attribute4ObjectTypeProperties.InheritMode == InheritModes.Inherited && ((Attr4ObjTypeClass) this.attr4ObjTypeList[index3]).Attribute4ObjectTypeProperties.InheritMode != InheritModes.Inherited)
            {
              IDBAttributeType4Object attributeType4Object = attributes1.Create(((Attr4ObjTypeClass) this.attr4ObjTypeList[index3]).Attribute4ObjectTypeProperties);
              if (attributeType4Object != null)
                addedIDs.Add(attributeType4Object.AttributeID);
              if (this.FindIndexByMasterId(this.attr4ObjTypeList, ((Attr4ObjTypeClass) this.attr4ObjTypeList[index3]).Attribute4ObjectTypeProperties.AttributeID) == -1)
              {
                Attr4ObjTypeClass attr4ObjTypeClass = Attr4ObjTypeClass.Clone((Attr4ObjTypeClass) this.attr4ObjTypeList[index3]);
                attr4ObjTypeClass.Tag = (object) true;
                this.attr4ObjTypeListOld.Insert(0, (object) attr4ObjTypeClass);
              }
              else
              {
                Attr4ObjTypeClass attr4ObjTypeClass = Attr4ObjTypeClass.Clone((Attr4ObjTypeClass) this.attr4ObjTypeList[index3]);
                attr4ObjTypeClass.Tag = (object) true;
                this.attr4ObjTypeListOld.Add((object) attr4ObjTypeClass);
              }
            }
            else if (!((Attr4ObjTypeClass) this.attr4ObjTypeListOld[index4]).Attribute4ObjectTypeProperties.Equals((object) ((Attr4ObjTypeClass) this.attr4ObjTypeList[index3]).Attribute4ObjectTypeProperties) && attributes1.GetAttributeByID(((Attr4ObjTypeClass) this.attr4ObjTypeList[index3]).Attribute4ObjectTypeProperties.AttributeID) is IDBAttributeType4Object attributeById)
            {
              int attributeId = attributeById.AttributeID;
              changedIDs.Add(attributeId);
              try
              {
                attributeById.Attribute4ObjectPropertiesStructure = ((Attr4ObjTypeClass) this.attr4ObjTypeList[index3]).Attribute4ObjectTypeProperties;
              }
              catch
              {
                this.listView.SelectedItems.Clear();
                if (this.listView.Items.Count > 0)
                {
                  int indexByAttributeId = this.GetListViewIndexByAttributeId(attributeId);
                  if (indexByAttributeId != -1)
                    this.listView.Items[indexByAttributeId].Selected = true;
                  else
                    this.listView.Items[0].Selected = true;
                }
                else
                  this.propertyGrid.SelectedObject = (object) null;
                throw;
              }
              ((Attr4ObjTypeClass) this.attr4ObjTypeListOld[index4]).Attribute4ObjectTypeProperties = ((Attr4ObjTypeClass) this.attr4ObjTypeList[index3]).Attribute4ObjectTypeProperties;
            }
          }
          this.attr4ObjTypeListOld.Assign(this.attr4ObjTypeList);
          this.DefineFilteredAttributes();
          StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage, false);
          if (addedIDs.Count <= 0 && changedIDs.Count <= 0)
          {
            if (removedIDs.Count <= 0)
              goto label_58;
          }
          INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
          DBAttributes4TypeEventArgs e = new DBAttributes4TypeEventArgs("Attribute4ObjTypeEvent", serverObject.ObjectType, (IList<int>) addedIDs, (IList<int>) changedIDs, (IList<int>) removedIDs, true);
          if (service != null)
          {
            if (e != null)
              service.FireEvent((object) null, (NotificationEventArgs) e);
          }
        }
      }
    }
label_58:
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  public void LoadAttr4ObjTypeList()
  {
    this.attr4ObjTypeList.Clear();
    if (this._folder.Id == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.attr4ObjTypeList.Fill((IDBCollection) (this._folder.GetServerObject(sessionKeeper.Session) as IDBObjectType).Attributes);
    this.attr4ObjTypeListOld.Assign(this.attr4ObjTypeList);
    this.DefineFilteredAttributes();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override bool RefreshAfterCanceling()
  {
    if (this.lastLVI != null)
      ((Attr4ObjTypeClass) this.lastLVI.Tag).CancelChanges();
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnaddItem_Click(object sender, EventArgs e)
  {
    if (!this.AddAttr4ObjType())
      return;
    this.FixStates(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OndeleteItem_Click(object sender, EventArgs e)
  {
    if (!this.DeleteAttr4ObjType())
      return;
    this.FixStates(sender, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnlistView_SelectedIndexChanged(object sender, EventArgs e)
  {
    ListView.SelectedListViewItemCollection selectedItems = this.listView.SelectedItems;
    if (selectedItems.Count == 0 && this.lastLVI != null)
      ((Attr4ObjTypeClass) this.lastLVI.Tag).SaveValues();
    if (selectedItems.Count <= 0)
      return;
    this.propertyGrid.SelectedObject = selectedItems[0].Tag;
    ((Attr4ObjTypeClass) this.propertyGrid.SelectedObject).FillValues(this.propertyGrid);
    this.lastLVI = selectedItems[0];
    this.lastAttributeID = ((Attr4ObjTypeClass) this.lastLVI.Tag).Attribute4ObjectTypeProperties.AttributeID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnpropertyGrid_Click(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="s"></param>
  /// <param name="e"></param>
  private void OnpropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    if (!(this.propertyGrid.SelectedObject as Attr4ObjTypeClass).ChangeEventProcessing(s, e))
      return;
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage, true);
    EventsHolder.FireWasChanged(s, this.instGuid, (EventArgs) e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attrId"></param>
  /// <param name="iDBAttributeTypeCollectionFull"></param>
  /// <param name="attrType"></param>
  /// <returns></returns>
  private Attr4ObjTypeClass GetAddAttr4ObjType(
    int attrId,
    IDBAttributeTypeCollection iDBAttributeTypeCollectionFull,
    out IDBAttributeType attrType)
  {
    attrType = (IDBAttributeType) null;
    if (this.attr4ObjTypeList.IndexOfByAttributeID(attrId) != -1)
      return (Attr4ObjTypeClass) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      AttributeTypePropertiesValidator validatorForObjectType = iDBAttributeTypeCollectionFull.GetValidatorForObjectType(attrId);
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrId);
      object _DefaultValue = validatorForObjectType.DefaultValue;
      if (attributeType.AttributeType == FieldTypes.ftDateTime && _DefaultValue != null && _DefaultValue is string)
        _DefaultValue = DateTimeCultureConverter.ConvertUniversalDateTimeStringToCurrentDateTime(_DefaultValue.ToString());
      Attribute4ObjectTypeProperties aAttribute4ObjectTypeProperties = new Attribute4ObjectTypeProperties(attrId, (int) this._folder.Id, validatorForObjectType.InheritMode[0], validatorForObjectType.RequiredMode[0], string.Empty, validatorForObjectType.Computed[0], (string) validatorForObjectType.Formula, validatorForObjectType.Unique[0], validatorForObjectType.LevelID, _DefaultValue, validatorForObjectType.OptimizationMode[0], validatorForObjectType.IsContent, validatorForObjectType.Options, validatorForObjectType.Mask, validatorForObjectType.MasterAttributeID, validatorForObjectType.SourceAttributeID);
      attrType = sessionKeeper.Session.GetAttributeType(attrId);
      if (attrType == null)
        return (Attr4ObjTypeClass) null;
      DataTable possibleValues = attrType.GetPossibleValues();
      return new Attr4ObjTypeClass(aAttribute4ObjectTypeProperties, attrType.PropertiesStructure, possibleValues);
    }
  }

  /// <summary>
  /// По id добавляемого атрибута возвращает список Attr4ObjTypeClass.
  /// количество &gt; 1 может определяться тем, что по master-source вытянули еще какие то атрибуты
  /// iDBAttributeTypeCollection - фильтрованная коллекция типов атрибутов (в зависимости от пользователя)
  /// iDBAttributeTypeCollectionFull - полная коллекция типов атрибутов
  /// </summary>
  /// <param name="addingAttrId"></param>
  /// <param name="iDBAttributeTypeCollectionFull"></param>
  /// <returns></returns>
  private Attr4ObjTypeClass[] GetAddAttr4ObjTypeArray(
    int addingAttrId,
    IDBAttributeTypeCollection iDBAttributeTypeCollectionFull)
  {
    ArrayList arrayList = new ArrayList();
    IDBAttributeType attrType1 = (IDBAttributeType) null;
    Attr4ObjTypeClass addAttr4ObjType1 = this.GetAddAttr4ObjType(addingAttrId, iDBAttributeTypeCollectionFull, out attrType1);
    if (addAttr4ObjType1 == null)
      return (Attr4ObjTypeClass[]) null;
    arrayList.Add((object) addAttr4ObjType1);
    if (attrType1.MasterAttributeID != 0)
    {
      string Message = string.Format(LocalizationHolder.rm.GetString("Client.Core_52"), (object) MetaDataHelper.GetAttributeTypeName(attrType1.MasterAttributeID));
      if (IMMessageBox.Show(MessageDialogs.msgConfirmAction, Message, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
      {
        IDBAttributeType attrType2 = (IDBAttributeType) null;
        Attr4ObjTypeClass addAttr4ObjType2 = this.GetAddAttr4ObjType(attrType1.MasterAttributeID, iDBAttributeTypeCollectionFull, out attrType2);
        if (addAttr4ObjType2 != null)
        {
          Attribute4ObjectTypeProperties objectTypeProperties = addAttr4ObjType2.Attribute4ObjectTypeProperties with
          {
            MasterAttributeID = 0,
            SourceAttributeID = 0
          };
          addAttr4ObjType2.Attribute4ObjectTypeProperties = objectTypeProperties;
          arrayList.Add((object) addAttr4ObjType2);
        }
      }
      else
      {
        Attribute4ObjectTypeProperties objectTypeProperties = addAttr4ObjType1.Attribute4ObjectTypeProperties with
        {
          MasterAttributeID = 0,
          SourceAttributeID = 0
        };
        addAttr4ObjType1.Attribute4ObjectTypeProperties = objectTypeProperties;
      }
    }
    if (addAttr4ObjType1.AttributeTypeProperties.FieldType == FieldTypes.ftObjectLink)
    {
      DataRow[] dataRowArray = DataHolders.AttributesHolder.DataTable.Select($"{"F_MASTER_ID"}={addAttr4ObjType1.Attribute4ObjectTypeProperties.AttributeID}");
      if (dataRowArray.Length != 0)
      {
        string Message = string.Format(LocalizationHolder.rm.GetString("Client.Core_53"), (object) dataRowArray.Length.ToString());
        if (IMMessageBox.Show(MessageDialogs.msgConfirmAction, Message, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
        {
          foreach (DataRow dataRow in dataRowArray)
          {
            IDBAttributeType attrType3 = (IDBAttributeType) null;
            Attr4ObjTypeClass addAttr4ObjType3 = this.GetAddAttr4ObjType(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]), iDBAttributeTypeCollectionFull, out attrType3);
            if (addAttr4ObjType3 != null)
            {
              Attribute4ObjectTypeProperties objectTypeProperties = addAttr4ObjType3.Attribute4ObjectTypeProperties with
              {
                MasterAttributeID = 0,
                SourceAttributeID = 0
              };
              addAttr4ObjType3.Attribute4ObjectTypeProperties = objectTypeProperties;
              arrayList.Add((object) addAttr4ObjType3);
            }
          }
        }
      }
    }
    return (Attr4ObjTypeClass[]) arrayList.ToArray(typeof (Attr4ObjTypeClass));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="category"></param>
  /// <param name="typeid"></param>
  public void SetPositionAt(int category, object typeid)
  {
    if (category != 3)
      return;
    int indexByAttributeId = this.GetListViewIndexByAttributeId((int) typeid);
    if (indexByAttributeId == -1)
      return;
    this.listView.Items[indexByAttributeId].Selected = true;
    this.listView.Focus();
  }

  /// <summary>id раздела справки.</summary>
  public override string HelpTopicID => this._folder != null ? "1024" : base.HelpTopicID;

  private void listView_DoubleClick(object sender, EventArgs e)
  {
    if (this._folder.IDatabaseConfiguratorControl == null || this._folder.IDatabaseConfiguratorControl.GetConfiguratorAction() != ConfiguratorAction.None)
      return;
    int id = 0;
    if (this.listView.SelectedItems.Count > 0 && this.listView.SelectedItems[0].Tag is Attr4ObjTypeClass tag)
      id = tag.AttributeID;
    if (StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage))
    {
      switch (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_132"), LocalizationHolder.rm.GetString("Client.Core_1081"), MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Warning))
      {
        case DialogResult.Yes:
          EventsHolder.FireApply(sender, this.instGuid, new EventsHolder.BoolArgs(true));
          break;
        case DialogResult.No:
          EventsHolder.FireCancel(sender, this.instGuid, (EventArgs) null);
          break;
        default:
          return;
      }
    }
    if (StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Attr4ObjTypeTabPage) || id == 0)
      return;
    EventsHolder.FireJumpToConfiguratorTreeNode(sender, this.instGuid, new EventsHolder.JumpToConfiguratorTreeNodeArgs(3, (object) id));
  }
}
