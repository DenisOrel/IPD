
// Type: Intermech.PropertyEditors.Attr4RelTypeForm
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

/// <summary>Форма редактирования атрибутов на типы связей</summary>
public class Attr4RelTypeForm : TabPageForm, IPositionAssigner
{
  private PropertyGrid propertyGrid;
  private ListView listView;
  private ColumnHeader attrColumnHeader;
  private IContainer components;
  private Splitter splitter1;
  private int lastAttributeID = -1;
  private ListViewItem lastLVI;
  private Attr4RelTypeList attr4RelTypeList = new Attr4RelTypeList();
  private Attr4RelTypeList attr4RelTypeListOld = new Attr4RelTypeList();
  private ContextMenuStrip contextMenuStrip;
  private ToolStripMenuItem addItem;
  private ToolStripMenuItem deleteItem;

  public Attr4RelTypeList Attr4RelTypeList => this.attr4RelTypeList;

  public Attr4RelTypeForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.attr4RelTypeList = new Attr4RelTypeList(new EventsHolder.GetListDelegate(this.GetMasterListProc));
    this.attr4RelTypeListOld = new Attr4RelTypeList(new EventsHolder.GetListDelegate(this.GetMasterListProc));
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Attr4RelTypeForm));
    this.contextMenuStrip = new ContextMenuStrip(this.components);
    this.addItem = new ToolStripMenuItem();
    this.deleteItem = new ToolStripMenuItem();
    this.propertyGrid = new PropertyGrid();
    this.splitter1 = new Splitter();
    this.listView = new ListView();
    this.attrColumnHeader = new ColumnHeader();
    this.contextMenuStrip.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.contextMenuStrip, "contextMenuStrip");
    this.contextMenuStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.addItem,
      (ToolStripItem) this.deleteItem
    });
    this.contextMenuStrip.Name = "contextMenuStrip";
    componentResourceManager.ApplyResources((object) this.addItem, "addItem");
    this.addItem.Name = "addItem";
    this.addItem.Click += new EventHandler(this.menuItem1_Click);
    componentResourceManager.ApplyResources((object) this.deleteItem, "deleteItem");
    this.deleteItem.Name = "deleteItem";
    this.deleteItem.Click += new EventHandler(this.menuItem2_Click);
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.CategoryForeColor = SystemColors.InactiveCaptionText;
    this.propertyGrid.LineColor = SystemColors.ScrollBar;
    this.propertyGrid.Name = "propertyGrid";
    this.propertyGrid.PropertySort = PropertySort.Alphabetical;
    this.propertyGrid.Tag = (object) "    ";
    this.propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.listView, "listView");
    this.listView.Columns.AddRange(new ColumnHeader[1]
    {
      this.attrColumnHeader
    });
    this.listView.ContextMenuStrip = this.contextMenuStrip;
    this.listView.HideSelection = false;
    this.listView.MultiSelect = false;
    this.listView.Name = "listView";
    this.listView.Sorting = SortOrder.Ascending;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.listView.SelectedIndexChanged += new EventHandler(this.listView_SelectedIndexChanged);
    this.listView.DoubleClick += new EventHandler(this.listView_DoubleClick);
    componentResourceManager.ApplyResources((object) this.attrColumnHeader, "attrColumnHeader");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.propertyGrid);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.listView);
    this.Name = nameof (Attr4RelTypeForm);
    this.Tag = (object) "  ";
    this.contextMenuStrip.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private int FindIndexByMasterId(Attr4RelTypeList list, int masterId)
  {
    for (int index = 0; index < list.Count; ++index)
    {
      if (((Attr4RelTypeClass) list[index]).Attribute4RelationTypeProperties.MasterAttributeID == masterId)
        return index;
    }
    return -1;
  }

  private void SortListsByMaster()
  {
    int startIndex = 0;
    int num1 = 0;
    for (int index = 0; index < this.attr4RelTypeListOld.Count; ++index)
    {
      if (this.FindIndexByMasterId(this.attr4RelTypeListOld, ((Attr4RelTypeClass) this.attr4RelTypeListOld[index]).Attribute4RelationTypeProperties.AttributeID) == -1)
      {
        ++num1;
        if (index > 0)
        {
          Attr4RelTypeClass attr4RelTypeClass = (Attr4RelTypeClass) this.attr4RelTypeListOld[index];
          this.attr4RelTypeListOld.RemoveAt(index);
          this.attr4RelTypeListOld.Insert(0, (object) attr4RelTypeClass);
        }
      }
    }
    int finishIndex1 = startIndex + num1 - 1;
    this.attr4RelTypeListOld.SortByAttrAtFormula(startIndex, finishIndex1, false);
    int finishIndex2 = this.attr4RelTypeList.Count - 1;
    int num2 = 0;
    for (int index = 0; index < this.attr4RelTypeList.Count; ++index)
    {
      if (this.FindIndexByMasterId(this.attr4RelTypeList, ((Attr4RelTypeClass) this.attr4RelTypeList[index]).Attribute4RelationTypeProperties.AttributeID) != -1)
      {
        ++num2;
        if (index > 0)
        {
          Attr4RelTypeClass attr4RelType = (Attr4RelTypeClass) this.attr4RelTypeList[index];
          this.attr4RelTypeList.RemoveAt(index);
          this.attr4RelTypeList.Insert(0, (object) attr4RelType);
        }
      }
    }
    this.attr4RelTypeList.SortByAttrAtFormula(finishIndex2 - num2 + 1, finishIndex2, true);
  }

  public override void FillForm(IFolder folder)
  {
    this._folder = folder as CustomFolder;
    if (StatesController.GetLoadState((object) TabPagesHolder.TabPages(this.instGuid).Attr4RelTypeTabPage))
      return;
    this.ClearForm();
    this.LoadAttr4RelTypeList();
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
    StatesController.SetLoadState((object) TabPagesHolder.TabPages(this.instGuid).Attr4RelTypeTabPage, true);
  }

  public override bool SaveForm(IFolder folder)
  {
    List<int> addedIDs = new List<int>(0);
    List<int> changedIDs = new List<int>(0);
    List<int> removedIDs = new List<int>(0);
    if (StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Attr4RelTypeTabPage))
    {
      if (this.lastLVI != null)
        ((Attr4RelTypeClass) this.lastLVI.Tag).SaveValues();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        using (RemoteLock remoteLock = new RemoteLock())
        {
          IDBRelationType serverObject = this._folder.GetServerObject(sessionKeeper.Session) as IDBRelationType;
          remoteLock.Add((object) serverObject);
          IDBAttribute4RelationTypeCollection attributes = serverObject.Attributes as IDBAttribute4RelationTypeCollection;
          remoteLock.Add((object) attributes);
          this.SortListsByMaster();
          int index1 = 0;
          while (index1 < this.attr4RelTypeListOld.Count)
          {
            if ((bool) ((Attr4TypeClass) this.attr4RelTypeListOld[index1]).Tag)
            {
              int attributeId = ((Attr4RelTypeClass) this.attr4RelTypeListOld[index1]).Attribute4RelationTypeProperties.AttributeID;
              if (this.attr4RelTypeList.IndexOfByAttributeID(attributeId) == -1)
              {
                if (((Attr4RelTypeClass) this.attr4RelTypeListOld[index1]).AttributeTypeProperties.FieldType == FieldTypes.ftObjectLink)
                {
                  for (int index2 = 0; index2 < this.attr4RelTypeListOld.Count; ++index2)
                  {
                    if (((Attr4RelTypeClass) this.attr4RelTypeListOld[index2]).Attribute4RelationTypeProperties.MasterAttributeID == ((Attr4RelTypeClass) this.attr4RelTypeListOld[index1]).Attribute4RelationTypeProperties.AttributeID)
                    {
                      Attribute4RelationTypeProperties relationTypeProperties = ((Attr4RelTypeClass) this.attr4RelTypeListOld[index2]).Attribute4RelationTypeProperties;
                      if (attributes.GetAttributeByID(relationTypeProperties.AttributeID) is IDBAttributeType4Relation attributeById)
                      {
                        relationTypeProperties.MasterAttributeID = 0;
                        relationTypeProperties.SourceAttributeID = 0;
                        attributeById.Attribute4RelationPropertiesStructure = relationTypeProperties;
                        ((Attr4RelTypeClass) this.attr4RelTypeListOld[index2]).Attribute4RelationTypeProperties = relationTypeProperties;
                      }
                    }
                  }
                }
                IDBAttributeType4Relation attributeById1 = attributes.GetAttributeByID(attributeId) as IDBAttributeType4Relation;
                using (new RemoteLock((object) attributeById1))
                {
                  int DeleteMode = 0;
                  if (!attributeById1.IsContent && IMMessageBox.Show(MessageDialogs.msgConfirmDelete, string.Format(LocalizationHolder.rm.GetString("Client.Core_57"), (object) attributeById1.Name), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
                    DeleteMode = Intermech.Consts.DeleteInstances;
                  attributeById1.Delete((long) DeleteMode);
                  this.attr4RelTypeListOld.RemoveAt(index1);
                  removedIDs.Add(attributeById1.AttributeID);
                  continue;
                }
              }
            }
            ++index1;
          }
          for (int index3 = 0; index3 < this.attr4RelTypeList.Count; ++index3)
          {
            int index4 = this.attr4RelTypeListOld.IndexOfByAttributeID(((Attr4RelTypeClass) this.attr4RelTypeList[index3]).Attribute4RelationTypeProperties.AttributeID);
            if (index4 == -1)
            {
              IDBAttributeType4Relation attributeType4Relation = attributes.Create(((Attr4RelTypeClass) this.attr4RelTypeList[index3]).Attribute4RelationTypeProperties);
              if (attributeType4Relation != null)
                addedIDs.Add(attributeType4Relation.AttributeID);
              if (this.FindIndexByMasterId(this.attr4RelTypeList, ((Attr4RelTypeClass) this.attr4RelTypeList[index3]).Attribute4RelationTypeProperties.AttributeID) == -1)
              {
                Attr4RelTypeClass attr4RelTypeClass = Attr4RelTypeClass.Clone((Attr4RelTypeClass) this.attr4RelTypeList[index3]);
                attr4RelTypeClass.Tag = (object) true;
                this.attr4RelTypeListOld.Insert(0, (object) attr4RelTypeClass);
              }
              else
              {
                Attr4RelTypeClass attr4RelTypeClass = Attr4RelTypeClass.Clone((Attr4RelTypeClass) this.attr4RelTypeList[index3]);
                attr4RelTypeClass.Tag = (object) true;
                this.attr4RelTypeListOld.Add((object) attr4RelTypeClass);
              }
            }
            else if (!((Attr4RelTypeClass) this.attr4RelTypeListOld[index4]).Attribute4RelationTypeProperties.Equals((object) ((Attr4RelTypeClass) this.attr4RelTypeList[index3]).Attribute4RelationTypeProperties) && attributes.GetAttributeByID(((Attr4RelTypeClass) this.attr4RelTypeList[index3]).Attribute4RelationTypeProperties.AttributeID) is IDBAttributeType4Relation attributeById)
            {
              int attributeId = attributeById.AttributeID;
              changedIDs.Add(attributeId);
              try
              {
                attributeById.Attribute4RelationPropertiesStructure = ((Attr4RelTypeClass) this.attr4RelTypeList[index3]).Attribute4RelationTypeProperties;
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
              ((Attr4RelTypeClass) this.attr4RelTypeListOld[index4]).Attribute4RelationTypeProperties = ((Attr4RelTypeClass) this.attr4RelTypeList[index3]).Attribute4RelationTypeProperties;
            }
          }
          this.attr4RelTypeListOld.Assign(this.attr4RelTypeList);
          this.DefineFilteredAttributes();
          StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Attr4RelTypeTabPage, false);
          if (addedIDs.Count <= 0 && changedIDs.Count <= 0)
          {
            if (removedIDs.Count <= 0)
              goto label_54;
          }
          INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
          DBAttributes4TypeEventArgs e = new DBAttributes4TypeEventArgs("Attribute4RelTypeEvent", serverObject.RelationType, (IList<int>) addedIDs, (IList<int>) changedIDs, (IList<int>) removedIDs);
          if (service != null)
          {
            if (e != null)
              service.FireEvent((object) null, (NotificationEventArgs) e);
          }
        }
      }
    }
label_54:
    return true;
  }

  private void DefineFilteredAttributes()
  {
    for (int index = 0; index < this.attr4RelTypeListOld.Count; ++index)
      ((Attr4TypeClass) this.attr4RelTypeListOld[index]).Tag = (object) true;
  }

  private void SetIcon(ListViewItem lvi, FieldTypes fieldType)
  {
    int num = Statics.IconSrv.IndexOf(3, -1, (object) fieldType);
    lvi.ImageIndex = num;
  }

  private void ClearForm()
  {
  }

  private void FillListView()
  {
    this.listView.BeginUpdate();
    try
    {
      this.listView.Items.Clear();
      this.listView.SmallImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
      for (int index = 0; index < this.attr4RelTypeList.Count; ++index)
      {
        if ((bool) ((Attr4TypeClass) this.attr4RelTypeListOld[index]).Tag)
        {
          ListViewItem lvi = new ListViewItem(((Attr4RelTypeClass) this.attr4RelTypeList[index]).AttributeTypeProperties.Name);
          lvi.Tag = (object) (Attr4RelTypeClass) this.attr4RelTypeList[index];
          this.SetIcon(lvi, ((Attr4RelTypeClass) this.attr4RelTypeList[index]).AttributeTypeProperties.FieldType);
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

  public void LoadAttr4RelTypeList()
  {
    this.attr4RelTypeList.Clear();
    if (this._folder.Id == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.attr4RelTypeList.Fill((IDBCollection) (this._folder.GetServerObject(sessionKeeper.Session) as IDBRelationType).Attributes);
    this.attr4RelTypeListOld.Assign(this.attr4RelTypeList);
    this.DefineFilteredAttributes();
  }

  private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    if (!(this.propertyGrid.SelectedObject as Attr4RelTypeClass).ChangeEventProcessing(s, e))
      return;
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Attr4RelTypeTabPage, true);
    EventsHolder.FireWasChanged(s, this.instGuid, (EventArgs) e);
  }

  private Attr4RelTypeClass GetAddAttr4RelType(
    int attrId,
    IDBAttributeTypeCollection iDBAttributeTypeCollectionFull,
    out IDBAttributeType attrType)
  {
    attrType = (IDBAttributeType) null;
    if (this.attr4RelTypeList.IndexOfByAttributeID(attrId) != -1)
      return (Attr4RelTypeClass) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      AttributeTypePropertiesValidator validatorForRelationType = iDBAttributeTypeCollectionFull.GetValidatorForRelationType(attrId);
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrId);
      object _DefaultValue = validatorForRelationType.DefaultValue;
      if (attributeType.AttributeType == FieldTypes.ftDateTime && _DefaultValue != null && _DefaultValue is string)
        _DefaultValue = DateTimeCultureConverter.ConvertUniversalDateTimeStringToCurrentDateTime(_DefaultValue.ToString());
      Attribute4RelationTypeProperties aAttribute4RelationTypeProperties = new Attribute4RelationTypeProperties(attrId, (int) this._folder.Id, validatorForRelationType.RequiredMode[0], string.Empty, validatorForRelationType.Computed[0], (string) validatorForRelationType.Formula, _DefaultValue, validatorForRelationType.OptimizationMode[0], validatorForRelationType.IsContent, validatorForRelationType.Options, validatorForRelationType.Mask, validatorForRelationType.MasterAttributeID, validatorForRelationType.SourceAttributeID);
      attrType = sessionKeeper.Session.GetAttributeType(attrId);
      if (attrType == null)
        return (Attr4RelTypeClass) null;
      DataTable possibleValues = attrType.GetPossibleValues();
      return new Attr4RelTypeClass(aAttribute4RelationTypeProperties, attrType.PropertiesStructure, possibleValues);
    }
  }

  private Attr4RelTypeClass[] GetAddAttr4RelTypeArray(
    int addingAttrId,
    IDBAttributeTypeCollection iDBAttributeTypeCollectionFull)
  {
    ArrayList arrayList = new ArrayList();
    IDBAttributeType attrType1 = (IDBAttributeType) null;
    Attr4RelTypeClass addAttr4RelType1 = this.GetAddAttr4RelType(addingAttrId, iDBAttributeTypeCollectionFull, out attrType1);
    if (addAttr4RelType1 == null)
      return (Attr4RelTypeClass[]) null;
    arrayList.Add((object) addAttr4RelType1);
    if (attrType1.MasterAttributeID != 0)
    {
      if (IMMessageBox.Show(MessageDialogs.msgConfirmAction, string.Format(LocalizationHolder.rm.GetString("Client.Core_52"), (object) MetaDataHelper.GetAttributeTypeName(attrType1.MasterAttributeID)), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
      {
        IDBAttributeType attrType2 = (IDBAttributeType) null;
        Attr4RelTypeClass addAttr4RelType2 = this.GetAddAttr4RelType(attrType1.MasterAttributeID, iDBAttributeTypeCollectionFull, out attrType2);
        if (addAttr4RelType2 != null)
        {
          Attribute4RelationTypeProperties relationTypeProperties = addAttr4RelType2.Attribute4RelationTypeProperties with
          {
            MasterAttributeID = 0,
            SourceAttributeID = 0
          };
          addAttr4RelType2.Attribute4RelationTypeProperties = relationTypeProperties;
          arrayList.Add((object) addAttr4RelType2);
        }
      }
      else
      {
        Attribute4RelationTypeProperties relationTypeProperties = addAttr4RelType1.Attribute4RelationTypeProperties with
        {
          MasterAttributeID = 0,
          SourceAttributeID = 0
        };
        addAttr4RelType1.Attribute4RelationTypeProperties = relationTypeProperties;
      }
    }
    if (addAttr4RelType1.AttributeTypeProperties.FieldType == FieldTypes.ftObjectLink)
    {
      DataRow[] dataRowArray = DataHolders.AttributesHolder.DataTable.Select("F_MASTER_ID=" + addAttr4RelType1.Attribute4RelationTypeProperties.AttributeID.ToString());
      if (dataRowArray.Length != 0 && IMMessageBox.Show(MessageDialogs.msgConfirmAction, string.Format(LocalizationHolder.rm.GetString("Client.Core_53"), (object) dataRowArray.Length.ToString()), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
      {
        foreach (DataRow dataRow in dataRowArray)
        {
          IDBAttributeType attrType3 = (IDBAttributeType) null;
          Attr4RelTypeClass addAttr4RelType3 = this.GetAddAttr4RelType(Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]), iDBAttributeTypeCollectionFull, out attrType3);
          if (addAttr4RelType3 != null)
          {
            Attribute4RelationTypeProperties relationTypeProperties = addAttr4RelType3.Attribute4RelationTypeProperties with
            {
              MasterAttributeID = 0,
              SourceAttributeID = 0
            };
            addAttr4RelType3.Attribute4RelationTypeProperties = relationTypeProperties;
            arrayList.Add((object) addAttr4RelType3);
          }
        }
      }
    }
    return (Attr4RelTypeClass[]) arrayList.ToArray(typeof (Attr4RelTypeClass));
  }

  private bool AddAttr4RelType()
  {
    bool flag = false;
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true);
    if (attributesSelectDlg.ShowDialog() == DialogResult.OK && attributesSelectDlg.SelectedAttributesID.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeTypeCollection attributeTypeCollection = sessionKeeper.Session.GetAttributeTypeCollection(-1, false);
        for (int index1 = 0; index1 < attributesSelectDlg.SelectedAttributesID.Count; ++index1)
        {
          Attr4RelTypeClass[] attr4RelTypeArray = this.GetAddAttr4RelTypeArray(attributesSelectDlg.SelectedAttributesID[index1], attributeTypeCollection);
          if (attr4RelTypeArray != null)
          {
            for (int index2 = 0; index2 < attr4RelTypeArray.Length; ++index2)
            {
              Attr4RelTypeClass attr4RelTypeClass = attr4RelTypeArray[index2];
              this.attr4RelTypeList.Add((object) attr4RelTypeClass);
              ListViewItem lvi = new ListViewItem(attr4RelTypeClass.AttributeName);
              lvi.Tag = (object) attr4RelTypeClass;
              this.SetIcon(lvi, attr4RelTypeClass.AttributeTypeProperties.FieldType);
              this.listView.Items.Add(lvi);
              this.listView.SelectedItems.Clear();
              lvi.Selected = true;
              flag = true;
            }
          }
          else
          {
            int num = (int) IMMessageBox.Show(MessageDialogs.msgError, $"{LocalizationHolder.rm.GetString("Client.Core_55")}{sessionKeeper.Session.GetAttributeType(attributesSelectDlg.SelectedAttributesID[index1]).Name}\"", MessageBoxButtons.OK, IMMessageBoxImage.Error);
          }
        }
      }
    }
    return flag;
  }

  private bool DeleteAttr4RelType()
  {
    ListView.SelectedListViewItemCollection selectedItems = this.listView.SelectedItems;
    if (selectedItems.Count == 0 || IMMessageBox.Show(MessageDialogs.msgConfirmDelete, MessageDialogs.msgReallyDelete, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return false;
    while (selectedItems.Count > 0)
    {
      int index1 = this.attr4RelTypeList.IndexOf(selectedItems[0].Tag);
      if (index1 != -1)
      {
        if (((Attr4RelTypeClass) this.attr4RelTypeList[index1]).AttributeTypeProperties.FieldType == FieldTypes.ftObjectLink)
        {
          for (int index2 = 0; index2 < this.attr4RelTypeList.Count; ++index2)
          {
            if (index2 != index1 && ((Attr4RelTypeClass) this.attr4RelTypeList[index2]).Attribute4RelationTypeProperties.MasterAttributeID == ((Attr4RelTypeClass) this.attr4RelTypeList[index1]).Attribute4RelationTypeProperties.AttributeID)
            {
              Attribute4RelationTypeProperties relationTypeProperties = ((Attr4RelTypeClass) this.attr4RelTypeList[index2]).Attribute4RelationTypeProperties with
              {
                MasterAttributeID = 0,
                SourceAttributeID = 0
              };
              ((Attr4RelTypeClass) this.attr4RelTypeList[index2]).Attribute4RelationTypeProperties = relationTypeProperties;
            }
          }
        }
        this.attr4RelTypeList.RemoveAt(index1);
      }
      selectedItems[0].Remove();
    }
    if (this.listView.Items.Count > 0)
      this.listView.Items[0].Selected = true;
    else
      this.propertyGrid.SelectedObject = (object) null;
    return true;
  }

  private void FixStates(object sender, EventArgs e)
  {
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Attr4RelTypeTabPage, true);
    EventsHolder.FireWasChanged(sender, this.instGuid, e);
  }

  private void menuItem1_Click(object sender, EventArgs e)
  {
    if (!this.AddAttr4RelType())
      return;
    this.FixStates(sender, e);
  }

  private void menuItem2_Click(object sender, EventArgs e)
  {
    if (!this.DeleteAttr4RelType())
      return;
    this.FixStates(sender, e);
  }

  private void listView_SelectedIndexChanged(object sender, EventArgs e)
  {
    ListView.SelectedListViewItemCollection selectedItems = this.listView.SelectedItems;
    if (selectedItems.Count == 0 && this.lastLVI != null)
      ((Attr4RelTypeClass) this.lastLVI.Tag).SaveValues();
    if (selectedItems.Count <= 0)
      return;
    this.propertyGrid.SelectedObject = selectedItems[0].Tag;
    ((Attr4RelTypeClass) this.propertyGrid.SelectedObject).FillValues(this.propertyGrid);
    this.lastLVI = selectedItems[0];
    this.lastAttributeID = ((Attr4RelTypeClass) this.lastLVI.Tag).Attribute4RelationTypeProperties.AttributeID;
  }

  private int GetListViewIndexByAttributeId(int aAttributeID)
  {
    int indexByAttributeId = -1;
    for (int index = 0; index < this.listView.Items.Count; ++index)
    {
      if (((Attr4RelTypeClass) this.listView.Items[index].Tag).Attribute4RelationTypeProperties.AttributeID == aAttributeID)
      {
        indexByAttributeId = index;
        break;
      }
    }
    return indexByAttributeId;
  }

  private ArrayList GetMasterListProc(object s, params object[] args)
  {
    ArrayList masterListProc = new ArrayList();
    int num = -1;
    if (this.listView.SelectedItems.Count > 0)
      num = ((Attr4RelTypeClass) this.listView.SelectedItems[0].Tag).Attribute4RelationTypeProperties.AttributeID;
    for (int index = 0; index < this.attr4RelTypeList.Count; ++index)
    {
      if (((Attr4RelTypeClass) this.attr4RelTypeList[index]).Attribute4RelationTypeProperties.MasterAttributeID == num)
        return masterListProc;
    }
    masterListProc.Add((object) new AttributePropertyClass(0));
    for (int index = 0; index < this.attr4RelTypeList.Count; ++index)
    {
      Attr4RelTypeClass attr4RelType = (Attr4RelTypeClass) this.attr4RelTypeList[index];
      int attributeId = attr4RelType.Attribute4RelationTypeProperties.AttributeID;
      if (attr4RelType.AttributeTypeProperties.FieldType == FieldTypes.ftObjectLink && num != attributeId && attr4RelType.Attribute4RelationTypeProperties.MasterAttributeID == 0)
        masterListProc.Add((object) new AttributePropertyClass(attributeId));
    }
    return masterListProc;
  }

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

  /// <summary>id раздела справки</summary>
  public override string HelpTopicID => this._folder == null ? base.HelpTopicID : "1033";

  private void listView_DoubleClick(object sender, EventArgs e)
  {
    if (this._folder.IDatabaseConfiguratorControl == null || this._folder.IDatabaseConfiguratorControl.GetConfiguratorAction() != ConfiguratorAction.None)
      return;
    int id = 0;
    if (this.listView.SelectedItems.Count > 0 && this.listView.SelectedItems[0].Tag is Attr4RelTypeClass tag)
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
