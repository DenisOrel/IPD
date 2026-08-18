// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.ImportTableConflictEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class ImportTableConflictEditor : Form
{
  private DataSet _tableData;
  private DataTable _metadata;
  private DataTable _possibleValues;
  private long _tableID;
  private IContainer components;
  private SplitContainer splitContainer1;
  private SplitContainer splitContainer2;
  private GroupBox groupBox1;
  private PropertyGrid propertyGrid1;
  private GroupBox groupBox2;
  private PropertyGrid propertyGrid2;
  private Button bCancel;
  private Button bOK;
  private Button bSelect;
  private TreeView treeView1;
  private ImageList imageList1;
  private Button bCreate;
  private Button bUpdate;

  public ImportTableConflictEditor() => this.InitializeComponent();

  public void Init(IDBObject table, IDBAttribute attrData)
  {
    this.ReadMetadata(table);
    if (this._metadata == null)
    {
      this.bCreate.Enabled = this.bUpdate.Enabled = this.bSelect.Enabled = this.bOK.Enabled = false;
    }
    else
    {
      this._tableID = table.ObjectID;
      this._tableData = TablesMergingHelper.UnpackDataSetFromAttribute(attrData);
      this.FillComponents();
    }
  }

  private void FillComponents()
  {
    List<AttributesComparison> attributesComparisonList = this.ReadAttributesComparison();
    foreach (DataRow row in (InternalDataCollectionBase) this._attributes.Rows)
    {
      Guid guid = new Guid(Convert.ToString(row["F_ATTRIBUTE_GUID"]));
      DataRow[] dataRowArray = this._metadata.Select($"F_GUID ='{guid}'");
      string name = Convert.ToString(dataRowArray[0]["F_NAME"]);
      TreeNode node = this.treeView1.Nodes.Add(name);
      Guid destGuid = Guid.Empty;
      if (attributesComparisonList != null)
      {
        AttributesComparison attributesComparison = attributesComparisonList.Find((Predicate<AttributesComparison>) (x => x.SourceGuid == guid || x.SourceName.Equals(name)));
        if (attributesComparison != null)
          destGuid = attributesComparison.DestinationGuid;
      }
      ImportTableConflictEditor.AttributeItem attributeItem = this.CreateAttributeItem(guid, name, dataRowArray[0], destGuid);
      node.Tag = (object) attributeItem;
      this.RefreshNode(node);
    }
    this.treeView1.SelectedNode = this.treeView1.Nodes[0];
  }

  private void RefreshNode(TreeNode node)
  {
    node.ImageIndex = node.SelectedImageIndex = ((ImportTableConflictEditor.AttributeItem) node.Tag).Error ? 1 : 0;
    node.ToolTipText = ((ImportTableConflictEditor.AttributeItem) node.Tag).ErrorMessage;
  }

  private List<AttributesComparison> ReadAttributesComparison()
  {
    List<AttributesComparison> attributesComparisonList = new List<AttributesComparison>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(this._tableID).GetAttributeByGuid(PortalConsts.attributeComparisonAttributes, false);
      if (attributeByGuid != null)
      {
        for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
        {
          attributeByGuid.Index = index;
          if (!string.IsNullOrEmpty(attributeByGuid.AsString))
            attributesComparisonList.Add(new AttributesComparison(attributeByGuid.AsString));
        }
      }
    }
    return attributesComparisonList.Count <= 0 ? (List<AttributesComparison>) null : attributesComparisonList;
  }

  private void ReadMetadata(IDBObject table)
  {
    IDBAttribute attributeByGuid = table.GetAttributeByGuid(PortalConsts.attributeTableAttributes, false);
    if (attributeByGuid == null)
      return;
    DataSet dataSet = TablesMergingHelper.UnpackDataSetFromAttribute(attributeByGuid);
    this._metadata = dataSet.Tables["IMS_ATTRIBUTES"];
    this._possibleValues = dataSet.Tables["IMS_POSSIBLE_VALUES"];
  }

  private ImportTableConflictEditor.AttributeItem CreateAttributeItem(
    Guid guid,
    string name,
    DataRow sourceRow,
    Guid destGuid)
  {
    ImportTableConflictEditor.AttributeItem attributeItem = new ImportTableConflictEditor.AttributeItem(guid);
    MultiValueModes int32_1 = (MultiValueModes) Convert.ToInt32(sourceRow["F_MULTIPLE_VALUED"]);
    int int32_2 = Convert.ToInt32(sourceRow["F_ATTRIBUTE_ID"]);
    FieldTypes int32_3 = (FieldTypes) Convert.ToInt32(sourceRow["F_ATTRIBUTE_TYPE"]);
    PossibleValuesCollection possibleValues = new PossibleValuesCollection();
    if (int32_1 == MultiValueModes.MultiValuesFromList || int32_1 == MultiValueModes.SingleValueFromList)
    {
      DataRow[] dataRowArray = this._possibleValues.Select($"F_ATTRIBUTE_ID={int32_2}");
      if (dataRowArray.Length != 0)
      {
        string possibleValueFieldName = this.GetPossibleValueFieldName(int32_3, int32_2);
        foreach (DataRow dataRow in dataRowArray)
          possibleValues.Add(new PossibleValue(Convert.ToString(dataRow["F_DESCRIPTION"]), dataRow[possibleValueFieldName]));
      }
    }
    long int64 = sourceRow["F_SIZE_TYPE"] != DBNull.Value ? Convert.ToInt64(sourceRow["F_SIZE_TYPE"]) : 0L;
    TableAttribute tableAttribute1 = new TableAttribute(int32_2, guid, name, int32_3, int64, int32_1, possibleValues);
    attributeItem.TableAttributes[0] = tableAttribute1;
    TableAttribute tableAttribute2 = (TableAttribute) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attrType = (IDBAttributeType) null;
      if (destGuid != Guid.Empty)
        attrType = sessionKeeper.Session.GetAttributeType(destGuid, false);
      if (attrType == null)
        attrType = sessionKeeper.Session.GetAttributeType(guid, false);
      if (attrType != null)
      {
        tableAttribute2 = this.TableAttributeFromBase(attrType);
        string errorMessage;
        if (!((ITablesMergingService) sessionKeeper.Session.GetCustomService(typeof (ITablesMergingService))).CheckAttribute(sessionKeeper.Session.SessionGUID, attrType.AttributeID, this._possibleValues, tableAttribute1.Id, tableAttribute1.FieldType, tableAttribute1.Size, tableAttribute1.MultiValueMode, out errorMessage))
        {
          attributeItem.Error = true;
          attributeItem.ErrorMessage = errorMessage;
        }
      }
      else
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(name, false);
        if (attributeType != null)
        {
          tableAttribute2 = this.TableAttributeFromBase(attributeType);
          attributeItem.Error = true;
          attributeItem.ErrorMessage = "Различие в глобальных идентификаторах";
        }
        else
          attributeItem.Error = true;
      }
      if (tableAttribute2 != null)
        attributeItem.TableAttributes[1] = tableAttribute2;
    }
    return attributeItem;
  }

  private TableAttribute TableAttributeFromBase(IDBAttributeType attrType)
  {
    PossibleValuesCollection possibleValues = new PossibleValuesCollection();
    if (attrType.MultipleValued == MultiValueModes.MultiValuesFromList || attrType.MultipleValued == MultiValueModes.SingleValueFromList)
    {
      DataRow[] possibleValuesRows = attrType.GetPossibleValuesRows();
      if (possibleValuesRows != null && possibleValuesRows.Length != 0)
      {
        foreach (DataRow dataRow in possibleValuesRows)
          possibleValues.Add(new PossibleValue(Convert.ToString(dataRow["F_DESCRIPTION"]), dataRow[attrType.PossibleValueFieldName]));
      }
    }
    return new TableAttribute(attrType.AttributeID, (attrType as IDBGuid).GUID, attrType.Name, attrType.AttributeType, attrType.SizeType, attrType.MultipleValued, possibleValues);
  }

  private string GetPossibleValueFieldName(FieldTypes type, int attributeID)
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    List<FieldTypes> convertList = new List<FieldTypes>();
    RelationalOperators[] enabledOperators = (RelationalOperators[]) null;
    bool computableAttribute = false;
    AttributeCacheHelper.GetAttributeTypeValues(type, attributeID, ref empty1, ref empty2, ref convertList, ref enabledOperators, ref computableAttribute, ref empty3);
    return empty3;
  }

  private DataTable _attributes => this._tableData.Tables["IMS_ATTR_TYPES"];

  private void OnTreeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.propertyGrid1.SelectedObject = (object) this._selectedItem.TableAttributes[0];
    this.propertyGrid2.SelectedObject = (object) this._selectedItem.TableAttributes[1];
    this.RefreshButtons();
  }

  private void RefreshButtons()
  {
    this.bCreate.Enabled = this._selectedItem.TableAttributes[1] == null;
    this.bUpdate.Enabled = this._selectedItem.Error && this._selectedItem.TableAttributes[1] != null;
  }

  private void OnSelectClick(object sender, EventArgs e)
  {
    ImportTableConflictEditor.AttributeItem selectedItem = this._selectedItem;
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.SetNewAttribute(sessionKeeper.Session, selectedItem, attributesSelectDlg.SelectedAttributesID[0]);
    }
  }

  private void SetNewAttribute(
    IUserSession session,
    ImportTableConflictEditor.AttributeItem attribute,
    int attributeID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = session.GetAttributeType(attributeID);
      TableAttribute tableAttribute = this.TableAttributeFromBase(attributeType);
      string errorMessage;
      if (!((ITablesMergingService) sessionKeeper.Session.GetCustomService(typeof (ITablesMergingService))).CheckAttribute(sessionKeeper.Session.SessionGUID, attributeType.AttributeID, this._possibleValues, attribute.TableAttributes[0].Id, attribute.TableAttributes[0].FieldType, attribute.TableAttributes[0].Size, attribute.TableAttributes[0].MultiValueMode, out errorMessage))
      {
        attribute.Error = true;
        attribute.ErrorMessage = errorMessage;
      }
      else
      {
        attribute.Error = false;
        attribute.ErrorMessage = (string) null;
      }
      attribute.TableAttributes[1] = tableAttribute;
      this.propertyGrid2.SelectedObject = (object) tableAttribute;
      this.RefreshNode(this.treeView1.SelectedNode);
      this.RefreshButtons();
    }
  }

  private void OnCreateClick(object sender, EventArgs e)
  {
    ImportTableConflictEditor.AttributeItem selectedItem = this._selectedItem;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataRow row = this._metadata.Rows.Find((object) selectedItem.TableAttributes[0].Id);
      int num = sessionKeeper.Session.GetAttributeTypeCollection(-1).Create(new AttributeTypeProperties(row));
      if (selectedItem.TableAttributes[0].PossibleValues != null)
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(num);
        attributeType.SetNewPossibleValues(this.GetPossibleValuesTable(selectedItem.TableAttributes[0].Id, attributeType.PossibleValueFieldName));
      }
      this.SetNewAttribute(sessionKeeper.Session, selectedItem, num);
    }
  }

  private void OnUpdateClick(object sender, EventArgs e)
  {
    TableAttribute[] tableAttributes = this._selectedItem.TableAttributes;
    if (tableAttributes[0] == null || tableAttributes[1] == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(tableAttributes[1].Id);
      if (tableAttributes[0].Name != attributeType.Name)
        attributeType.Name = tableAttributes[0].Name;
      if (tableAttributes[0].FieldType == FieldTypes.ftString && attributeType.AttributeType == FieldTypes.ftString && Convert.ToInt64(tableAttributes[0].Size) > attributeType.SizeType)
        attributeType.SizeType = Convert.ToInt64(tableAttributes[0].Size);
      if (tableAttributes[0].MultiValueMode != attributeType.MultipleValued)
        attributeType.MultipleValued = tableAttributes[0].MultiValueMode;
      if (tableAttributes[0].FieldType == attributeType.AttributeType && tableAttributes[0].PossibleValues != null && (attributeType.MultipleValued == MultiValueModes.SingleValueFromList || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList))
      {
        string possibleValueFieldName = attributeType.PossibleValueFieldName;
        DataTable possibleValuesTable = this.GetPossibleValuesTable(tableAttributes[0].Id, attributeType.PossibleValueFieldName);
        DataTable possibleValues = attributeType.GetPossibleValues();
        DataTable valuesTable = DataSetProcessor.CopyTable(possibleValues);
        int int32 = Convert.ToInt32(valuesTable.Compute("max([F_INLIST_ID])", string.Empty));
        bool flag = false;
        foreach (DataRow row1 in (InternalDataCollectionBase) possibleValuesTable.Rows)
        {
          DataRow dataRow = (DataRow) null;
          foreach (DataRow row2 in (InternalDataCollectionBase) possibleValues.Rows)
          {
            if (AttributesTypeHelper.EqualValues(row1[possibleValueFieldName], row2[possibleValueFieldName], possibleValueFieldName))
            {
              dataRow = row2;
              break;
            }
          }
          if (dataRow == null)
          {
            DataRow row3 = valuesTable.NewRow();
            row3["F_INLIST_ID"] = (object) int32++;
            row3[possibleValueFieldName] = row1[possibleValueFieldName];
            row3["F_DESCRIPTION"] = row1["F_DESCRIPTION"];
            valuesTable.Rows.Add(row3);
            flag = true;
          }
          else if (!AttributesTypeHelper.EqualValues(row1["F_DESCRIPTION"], dataRow["F_DESCRIPTION"], "F_STRING_VALUE"))
          {
            DataRow[] dataRowArray = valuesTable.Select($"{"F_INLIST_ID"}={dataRow["F_INLIST_ID"]}");
            if (dataRowArray != null && dataRowArray.Length == 1)
              dataRowArray[0]["F_DESCRIPTION"] = row1["F_DESCRIPTION"];
          }
        }
        if (flag)
          attributeType.SetNewPossibleValues(valuesTable);
      }
      this.SetNewAttribute(sessionKeeper.Session, this._selectedItem, attributeType.AttributeID);
    }
  }

  private DataTable GetPossibleValuesTable(int oldId, string possibleValueFieldName)
  {
    DataTable possibleValuesTable = new DataTable();
    possibleValuesTable.Columns.Add("F_INLIST_ID", this._possibleValues.Columns["F_INLIST_ID"].DataType);
    possibleValuesTable.Columns.Add(possibleValueFieldName, this._possibleValues.Columns[possibleValueFieldName].DataType);
    possibleValuesTable.Columns.Add("F_DESCRIPTION", this._possibleValues.Columns["F_DESCRIPTION"].DataType);
    foreach (DataRow dataRow in this._possibleValues.Select($"F_ATTRIBUTE_ID={oldId}"))
    {
      DataRow row = possibleValuesTable.NewRow();
      row["F_INLIST_ID"] = dataRow["F_INLIST_ID"];
      row[possibleValueFieldName] = dataRow[possibleValueFieldName];
      row["F_DESCRIPTION"] = dataRow["F_DESCRIPTION"];
      possibleValuesTable.Rows.Add(row);
    }
    return possibleValuesTable;
  }

  private ImportTableConflictEditor.AttributeItem _selectedItem
  {
    get => this.treeView1.SelectedNode.Tag as ImportTableConflictEditor.AttributeItem;
  }

  private void OK_Click(object sender, EventArgs e)
  {
    foreach (TreeNode node in this.treeView1.Nodes)
    {
      ImportTableConflictEditor.AttributeItem tag = node.Tag as ImportTableConflictEditor.AttributeItem;
      if (tag.TableAttributes[1] == null)
      {
        int num = (int) MessageBox.Show($"Для атрибута {tag.TableAttributes[0].Name} не назначено соответствие. Завершение редактирования конфликта невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
    }
    if (MessageBox.Show("Завершить редактирование конфликта импорта с записью данных в таблицу Imbase?", this.Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    DataTable attributes = this._attributes;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag = false;
      List<AttributesComparison> values = new List<AttributesComparison>();
      foreach (TreeNode node in this.treeView1.Nodes)
      {
        ImportTableConflictEditor.AttributeItem tag = node.Tag as ImportTableConflictEditor.AttributeItem;
        DataRow dataRow = attributes.Select($"F_ATTRIBUTE_GUID='{tag.Guid}'")[0];
        TableAttribute tableAttribute1 = tag.TableAttributes[0];
        TableAttribute tableAttribute2 = tag.TableAttributes[1];
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(tableAttribute2.Id);
        if (tableAttribute1.Guid != tableAttribute2.Guid)
        {
          values.Add(new AttributesComparison(new Guid(tableAttribute1.Guid), tableAttribute1.Name, new Guid(tableAttribute2.Guid)));
          dataRow["F_ATTRIBUTE_GUID"] = (object) tableAttribute2.Guid.ToString();
          dataRow["F_DEFAULT_VALUE"] = attributeType.DefaultValue;
          flag = true;
        }
      }
      IDBObject table = sessionKeeper.Session.GetObject(this._tableID);
      if (flag)
      {
        AttributesComparisonHelper.SaveToAttribute(table, values);
        attributes.AcceptChanges();
      }
      else
        table.GetAttributeByGuid(PortalConsts.attributeComparisonAttributes, false)?.Delete(0L);
      ((ITablesMergingService) sessionKeeper.Session.GetCustomService(typeof (ITablesMergingService))).Merge(sessionKeeper.Session.SessionGUID, this._tableID, this._tableData, true);
    }
    this.DialogResult = DialogResult.OK;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImportTableConflictEditor));
    this.splitContainer1 = new SplitContainer();
    this.treeView1 = new TreeView();
    this.imageList1 = new ImageList(this.components);
    this.splitContainer2 = new SplitContainer();
    this.groupBox1 = new GroupBox();
    this.propertyGrid1 = new PropertyGrid();
    this.bCreate = new Button();
    this.groupBox2 = new GroupBox();
    this.propertyGrid2 = new PropertyGrid();
    this.bSelect = new Button();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.bUpdate = new Button();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.splitContainer2.BeginInit();
    this.splitContainer2.Panel1.SuspendLayout();
    this.splitContainer2.Panel2.SuspendLayout();
    this.splitContainer2.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.treeView1);
    this.splitContainer1.Panel2.Controls.Add((Control) this.splitContainer2);
    this.splitContainer1.Size = new Size(884, 416);
    this.splitContainer1.SplitterDistance = 246;
    this.splitContainer1.TabIndex = 2;
    this.treeView1.Dock = DockStyle.Fill;
    this.treeView1.ImageIndex = 0;
    this.treeView1.ImageList = this.imageList1;
    this.treeView1.Location = new Point(0, 0);
    this.treeView1.Name = "treeView1";
    this.treeView1.SelectedImageIndex = 0;
    this.treeView1.ShowNodeToolTips = true;
    this.treeView1.Size = new Size(246, 416);
    this.treeView1.TabIndex = 0;
    this.treeView1.AfterSelect += new TreeViewEventHandler(this.OnTreeView_AfterSelect);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "empty.ico");
    this.imageList1.Images.SetKeyName(1, "warning.png");
    this.splitContainer2.Dock = DockStyle.Fill;
    this.splitContainer2.Location = new Point(0, 0);
    this.splitContainer2.Name = "splitContainer2";
    this.splitContainer2.Orientation = Orientation.Horizontal;
    this.splitContainer2.Panel1.Controls.Add((Control) this.groupBox1);
    this.splitContainer2.Panel2.Controls.Add((Control) this.bUpdate);
    this.splitContainer2.Panel2.Controls.Add((Control) this.bCreate);
    this.splitContainer2.Panel2.Controls.Add((Control) this.groupBox2);
    this.splitContainer2.Panel2.Controls.Add((Control) this.bSelect);
    this.splitContainer2.Panel2.Controls.Add((Control) this.bOK);
    this.splitContainer2.Panel2.Controls.Add((Control) this.bCancel);
    this.splitContainer2.Size = new Size(634, 416);
    this.splitContainer2.SplitterDistance = 191;
    this.splitContainer2.TabIndex = 0;
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.propertyGrid1);
    this.groupBox1.ForeColor = SystemColors.HotTrack;
    this.groupBox1.Location = new Point(6, 3);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(625, 185);
    this.groupBox1.TabIndex = 0;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Свойства атрибута в базе-источнике";
    this.propertyGrid1.Dock = DockStyle.Fill;
    this.propertyGrid1.HelpVisible = false;
    this.propertyGrid1.Location = new Point(3, 16 /*0x10*/);
    this.propertyGrid1.Name = "propertyGrid1";
    this.propertyGrid1.Size = new Size(619, 166);
    this.propertyGrid1.TabIndex = 0;
    this.bCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.bCreate.Location = new Point(133, 173);
    this.bCreate.Name = "bCreate";
    this.bCreate.Size = new Size(121, 27);
    this.bCreate.TabIndex = 7;
    this.bCreate.Text = "Создать";
    this.bCreate.UseVisualStyleBackColor = true;
    this.bCreate.Click += new EventHandler(this.OnCreateClick);
    this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox2.Controls.Add((Control) this.propertyGrid2);
    this.groupBox2.ForeColor = SystemColors.HotTrack;
    this.groupBox2.Location = new Point(6, 5);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(622, 162);
    this.groupBox2.TabIndex = 1;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Свойства атрибута в базе-приемнике";
    this.propertyGrid2.Dock = DockStyle.Fill;
    this.propertyGrid2.HelpVisible = false;
    this.propertyGrid2.Location = new Point(3, 16 /*0x10*/);
    this.propertyGrid2.Name = "propertyGrid2";
    this.propertyGrid2.Size = new Size(616, 143);
    this.propertyGrid2.TabIndex = 1;
    this.bSelect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.bSelect.Location = new Point(9, 173);
    this.bSelect.Name = "bSelect";
    this.bSelect.Size = new Size(121, 27);
    this.bSelect.TabIndex = 6;
    this.bSelect.Text = "Выбрать";
    this.bSelect.UseVisualStyleBackColor = true;
    this.bSelect.Click += new EventHandler(this.OnSelectClick);
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.Location = new Point(381, 173);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 4;
    this.bOK.Text = "ОК";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.OK_Click);
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(505, 173);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 3;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.bUpdate.Location = new Point(257, 173);
    this.bUpdate.Name = "bUpdate";
    this.bUpdate.Size = new Size(121, 27);
    this.bUpdate.TabIndex = 8;
    this.bUpdate.Text = "Обновить";
    this.bUpdate.UseVisualStyleBackColor = true;
    this.bUpdate.Click += new EventHandler(this.OnUpdateClick);
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(884, 416);
    this.Controls.Add((Control) this.splitContainer1);
    this.MinimumSize = new Size(900, 455);
    this.Name = nameof (ImportTableConflictEditor);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Редактор конфликта импорта таблицы Imbase";
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.splitContainer2.Panel1.ResumeLayout(false);
    this.splitContainer2.Panel2.ResumeLayout(false);
    this.splitContainer2.EndInit();
    this.splitContainer2.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class AttributeItem
  {
    public Guid Guid;
    public bool Error;
    public string ErrorMessage;
    public TableAttribute[] TableAttributes;

    public AttributeItem(Guid guid)
    {
      this.Guid = guid;
      this.Error = false;
      this.TableAttributes = new TableAttribute[2];
    }
  }
}
