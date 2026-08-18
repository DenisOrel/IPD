// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.ImportOldTableConflictEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Imbase.Portal;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class ImportOldTableConflictEditor : Form
{
  private DataTable _possibleValues;
  private long _tableID;
  private DataTableImporter _importer;
  private IContainer components;
  private SplitContainer splitContainer1;
  private SplitContainer splitContainer2;
  private GroupBox groupBox1;
  private PropertyGrid propertyGrid1;
  private GroupBox groupBox2;
  private PropertyGrid propertyGrid2;
  private Button bCancel;
  private Button bOK;
  private Button btSelect;
  private TreeView treeView1;
  private ImageList imageList1;
  private Button btCreate;

  public ImportOldTableConflictEditor() => this.InitializeComponent();

  public void InitData(IUserSession session, IDBObject table)
  {
    this.ReadMetadata(session, table);
    this._tableID = table.ObjectID;
    this.FillComponents(session);
  }

  private void FillComponents(IUserSession session)
  {
    List<AttributesComparison> comparisons = this._importer.Comparisons;
    foreach (FieldRecord field in this._importer.GetFields())
    {
      FieldRecord fieldRecord = field;
      TreeNode node = this.treeView1.Nodes.Add(fieldRecord.LongName);
      Guid destGuid = Guid.Empty;
      if (comparisons != null)
      {
        AttributesComparison attributesComparison = comparisons.Find((Predicate<AttributesComparison>) (x => x.SourceName.Equals(fieldRecord.Field)));
        if (attributesComparison != null)
          destGuid = attributesComparison.DestinationGuid;
      }
      ImportOldTableConflictEditor.AttributeItem attributeItem = this.CreateAttributeItem(session, fieldRecord, destGuid);
      node.Tag = (object) attributeItem;
      this.RefreshNode(node);
    }
    this.treeView1.SelectedNode = this.treeView1.Nodes[0];
  }

  private void RefreshNode(TreeNode node)
  {
    node.ImageIndex = node.SelectedImageIndex = ((ImportOldTableConflictEditor.AttributeItem) node.Tag).Error ? 1 : 0;
    node.ToolTipText = ((ImportOldTableConflictEditor.AttributeItem) node.Tag).ErrorMessage;
  }

  private void ReadMetadata(IUserSession session, IDBObject table)
  {
    this._importer = new DataTableImporter(session, table);
    if (this._importer.ReadPortalData(table.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false)) != 4)
      throw new Exception("Недостаточно файлов данных для импорта таблицы IMBASE");
  }

  private ImportOldTableConflictEditor.AttributeItem CreateAttributeItem(
    IUserSession session,
    FieldRecord fieldRecord,
    Guid destGuid)
  {
    ImportOldTableConflictEditor.AttributeItem attributeItem = new ImportOldTableConflictEditor.AttributeItem(fieldRecord);
    MultiValueModes multiValueMode = MultiValueModes.SingleValue;
    int id = 0;
    FieldTypes fieldType = fieldRecord.FieldType;
    long dataSize = (long) this._importer.GetDataSize(fieldRecord);
    Guid guid = Guid.Empty;
    string errorMessage = string.Empty;
    int errorAttId = 0;
    IDBAttributeType dbAttributeType = this._importer.CheckAttribute(session, fieldRecord, out errorMessage, destGuid, out errorAttId);
    if (dbAttributeType != null)
    {
      guid = dbAttributeType.PropertiesStructure.AttributeGuid;
      id = dbAttributeType.AttributeID;
    }
    else
    {
      attributeItem.Error = true;
      attributeItem.ErrorMessage = errorMessage;
      if (Guid.Empty.Equals(destGuid) && errorAttId != 0)
      {
        IDBAttributeType attributeType = session.GetAttributeType(errorAttId, false);
        if (attributeType != null)
          destGuid = attributeType.PropertiesStructure.AttributeGuid;
      }
    }
    ImportOldTableConflictEditor.TableAttribute tableAttribute1 = new ImportOldTableConflictEditor.TableAttribute(id, guid, fieldRecord.LongName, fieldType, dataSize, multiValueMode, new PossibleValuesCollection());
    attributeItem.TableAttributes[0] = tableAttribute1;
    ImportOldTableConflictEditor.TableAttribute tableAttribute2 = (ImportOldTableConflictEditor.TableAttribute) null;
    IDBAttributeType attrType = (IDBAttributeType) null;
    if (destGuid != Guid.Empty)
      attrType = session.GetAttributeType(destGuid, false);
    if (attrType == null && !Guid.Empty.Equals(guid))
      attrType = session.GetAttributeType(guid, false);
    if (attrType != null)
    {
      tableAttribute2 = this.TableAttributeFromBase(attrType);
      if (!((ITablesMergingService) session.GetCustomService(typeof (ITablesMergingService))).CheckAttribute(session.SessionGUID, attrType.AttributeID, this._possibleValues, tableAttribute1.Id, tableAttribute1.FieldType, tableAttribute1.Size, tableAttribute1.MultiValueMode, out errorMessage))
      {
        attributeItem.Error = true;
        attributeItem.ErrorMessage = errorMessage;
      }
    }
    if (tableAttribute2 != null)
      attributeItem.TableAttributes[1] = tableAttribute2;
    return attributeItem;
  }

  private ImportOldTableConflictEditor.TableAttribute TableAttributeFromBase(
    IDBAttributeType attrType)
  {
    PossibleValuesCollection pvc = new PossibleValuesCollection();
    if (attrType.MultipleValued == MultiValueModes.MultiValuesFromList || attrType.MultipleValued == MultiValueModes.SingleValueFromList)
    {
      DataRow[] possibleValuesRows = attrType.GetPossibleValuesRows();
      if (possibleValuesRows != null && possibleValuesRows.Length != 0)
      {
        foreach (DataRow dataRow in possibleValuesRows)
          pvc.Add(new PossibleValue(Convert.ToString(dataRow["F_DESCRIPTION"]), dataRow[attrType.PossibleValueFieldName]));
      }
    }
    return new ImportOldTableConflictEditor.TableAttribute(attrType.AttributeID, (attrType as IDBGuid).GUID, attrType.Name, attrType.AttributeType, attrType.SizeType, attrType.MultipleValued, pvc);
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

  private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
  {
    ImportOldTableConflictEditor.AttributeItem selectedItem = this.SelectedItem;
    this.propertyGrid1.SelectedObject = (object) selectedItem.TableAttributes[0];
    this.propertyGrid2.SelectedObject = (object) selectedItem.TableAttributes[1];
  }

  private void OnSelectClick(object sender, EventArgs e)
  {
    ImportOldTableConflictEditor.AttributeItem selectedItem = this.SelectedItem;
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.SelectorFilter = (ISelectorFilter) new ForbiddenAttrs(this.UsedAttIDs());
      attributesSelectDlg.AllowedAttrsTypesFilter = this.GetValidTypes(selectedItem.TableAttributes[0].FieldType);
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.SetNewAttribute(sessionKeeper.Session, this.treeView1.SelectedNode, attributesSelectDlg.SelectedAttributesID[0], true);
    }
  }

  private List<FieldTypes> GetValidTypes(FieldTypes fieldType)
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    List<FieldTypes> convertList = new List<FieldTypes>();
    RelationalOperators[] enabledOperators = (RelationalOperators[]) null;
    bool computableAttribute = false;
    AttributeCacheHelper.GetAttributeTypeValues(fieldType, -1, ref empty1, ref empty3, ref convertList, ref enabledOperators, ref computableAttribute, ref empty2);
    List<FieldTypes> validTypes = new List<FieldTypes>();
    foreach (FieldTypes fieldTypes in convertList)
      validTypes.Add(fieldTypes);
    if (!validTypes.Contains(fieldType))
      validTypes.Add(fieldType);
    return validTypes;
  }

  private List<int> UsedAttIDs()
  {
    List<int> intList = new List<int>();
    foreach (TreeNode node in this.treeView1.Nodes)
    {
      ImportOldTableConflictEditor.AttributeItem tag = node.Tag as ImportOldTableConflictEditor.AttributeItem;
      if (tag.TableAttributes[1] != null && !intList.Contains(tag.TableAttributes[1].Id))
        intList.Add(tag.TableAttributes[1].Id);
    }
    return intList;
  }

  private void SetNewAttribute(IUserSession session, TreeNode node, int attributeID, bool replace)
  {
    using (new SessionKeeper())
    {
      IDBAttributeType attributeType = session.GetAttributeType(attributeID);
      FieldRecord fieldRecord = this.SelectedItem.FieldRecord;
      ImportOldTableConflictEditor.AttributeItem tag = node.Tag as ImportOldTableConflictEditor.AttributeItem;
      ImportOldTableConflictEditor.TableAttribute tableAttribute = (ImportOldTableConflictEditor.TableAttribute) null;
      if (replace && tag != null)
        tableAttribute = tag.TableAttributes[0];
      ImportOldTableConflictEditor.AttributeItem attributeItem = this.CreateAttributeItem(session, fieldRecord, attributeType.PropertiesStructure.AttributeGuid);
      if (tableAttribute != null)
        attributeItem.TableAttributes[0] = tableAttribute;
      node.Tag = (object) attributeItem;
      this.treeView1_AfterSelect((object) this, new TreeViewEventArgs(node));
      this.RefreshNode(this.treeView1.SelectedNode);
    }
  }

  private void OnCreateClick(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int attribute = this.CreateAttribute(this.SelectedItem.FieldRecord, sessionKeeper.Session.GetAttributeTypeCollection(-1));
      this.SetNewAttribute(sessionKeeper.Session, this.treeView1.SelectedNode, attribute, false);
    }
  }

  private int CreateAttribute(FieldRecord field, IDBAttributeTypeCollection collection)
  {
    MultiValueModes _multiValueMode = MultiValueModes.SingleValue;
    string DefValue = field.Data;
    if (!string.IsNullOrEmpty(DefValue) && DefValue.StartsWith("IM_LOOKUP,"))
      DefValue = string.Empty;
    object _defaultValue = this._importer.FormingValue(field.FieldType, (object) DefValue);
    if (field.FieldType == FieldTypes.ftMeasured)
    {
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(field.Units);
      field.Width = descriptor == null ? Intermech.Imbase.Consts.mmUnitID : descriptor.PhysicalQuantityID;
    }
    else if (field.FieldType == FieldTypes.ftString)
    {
      int num = this._importer.GetDataSize(field);
      if (num == 0)
        num = Intermech.Consts.MaxStringSize;
      field.Width = (long) num;
    }
    else
      field.Width = 0L;
    if (field.FieldType == FieldTypes.ftString && field.Width > (long) Intermech.Consts.MaxStringSize)
      field.FieldType = FieldTypes.ftMemo;
    if (Convert.ToString(_defaultValue).StartsWith("$"))
      _defaultValue = (object) null;
    AttributeTypeProperties attrProperties = new AttributeTypeProperties(0, field.LongName, field.ShortName, string.Empty, string.Empty, field.FieldType, _defaultValue, _multiValueMode, ComputeValueModes.NotComputableValue, field.Width, string.Empty, UniqueValueModes.NotUnique, 0, string.Empty, string.Empty, field.GUID, OptimizationModes.Write, false, AttributeOptions.ImbaseFlag_UsedInTables, string.Empty, 0, 0);
    return collection.Create(attrProperties);
  }

  private ImportOldTableConflictEditor.AttributeItem SelectedItem
  {
    get => this.treeView1.SelectedNode.Tag as ImportOldTableConflictEditor.AttributeItem;
  }

  private void bOK_Click(object sender, EventArgs e)
  {
    foreach (TreeNode node in this.treeView1.Nodes)
    {
      ImportOldTableConflictEditor.AttributeItem tag = node.Tag as ImportOldTableConflictEditor.AttributeItem;
      if (tag.TableAttributes[1] == null)
      {
        int num = (int) MessageBox.Show($"Для атрибута {tag.TableAttributes[0].Name} не назначено соответствие.{Environment.NewLine}Завершение редактирования конфликта невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      if (tag.Error)
      {
        int num = (int) MessageBox.Show($"У атрибута {tag.TableAttributes[0].Name} есть ошибки.{Environment.NewLine}Завершение редактирования конфликта невозможно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
    }
    if (MessageBox.Show("Завершить редактирование конфликта импорта с записью данных в таблицу Imbase?", this.Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag = false;
      List<AttributesComparison> values = new List<AttributesComparison>();
      foreach (TreeNode node in this.treeView1.Nodes)
      {
        ImportOldTableConflictEditor.AttributeItem tag = node.Tag as ImportOldTableConflictEditor.AttributeItem;
        ImportOldTableConflictEditor.TableAttribute tableAttribute1 = tag.TableAttributes[0];
        ImportOldTableConflictEditor.TableAttribute tableAttribute2 = tag.TableAttributes[1];
        if (tableAttribute1.Guid != tableAttribute2.Guid)
        {
          values.Add(new AttributesComparison(new Guid(tableAttribute1.Guid), tag.FieldRecord.Field, new Guid(tableAttribute2.Guid)));
          flag = true;
        }
      }
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._tableID);
      if (flag)
        AttributesComparisonHelper.SaveToAttribute(dbObject, values);
      else
        dbObject.GetAttributeByGuid(PortalConsts.attributeComparisonAttributes, false)?.Delete(0L);
      this._importer.ReadComparsions(dbObject);
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
      DataSet dataSet;
      if (this._importer.TryCreateDataTable(sessionKeeper.Session, dbObject, attributeByGuid, out dataSet))
      {
        ((ITablesMergingService) sessionKeeper.Session.GetCustomService(typeof (ITablesMergingService))).Merge(sessionKeeper.Session.SessionGUID, this._tableID, dataSet, true);
        if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
          service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this._tableID));
      }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImportOldTableConflictEditor));
    this.splitContainer1 = new SplitContainer();
    this.treeView1 = new TreeView();
    this.imageList1 = new ImageList(this.components);
    this.splitContainer2 = new SplitContainer();
    this.groupBox1 = new GroupBox();
    this.propertyGrid1 = new PropertyGrid();
    this.btCreate = new Button();
    this.groupBox2 = new GroupBox();
    this.propertyGrid2 = new PropertyGrid();
    this.btSelect = new Button();
    this.bOK = new Button();
    this.bCancel = new Button();
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
    this.splitContainer1.Size = new Size(729, 417);
    this.splitContainer1.SplitterDistance = 203;
    this.splitContainer1.TabIndex = 2;
    this.treeView1.Dock = DockStyle.Fill;
    this.treeView1.ImageIndex = 0;
    this.treeView1.ImageList = this.imageList1;
    this.treeView1.Location = new Point(0, 0);
    this.treeView1.Name = "treeView1";
    this.treeView1.SelectedImageIndex = 0;
    this.treeView1.ShowNodeToolTips = true;
    this.treeView1.Size = new Size(203, 417);
    this.treeView1.TabIndex = 0;
    this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "empty.ico");
    this.imageList1.Images.SetKeyName(1, "warning.png");
    this.splitContainer2.Dock = DockStyle.Fill;
    this.splitContainer2.Location = new Point(0, 0);
    this.splitContainer2.Name = "splitContainer2";
    this.splitContainer2.Orientation = Orientation.Horizontal;
    this.splitContainer2.Panel1.Controls.Add((Control) this.groupBox1);
    this.splitContainer2.Panel2.Controls.Add((Control) this.btCreate);
    this.splitContainer2.Panel2.Controls.Add((Control) this.groupBox2);
    this.splitContainer2.Panel2.Controls.Add((Control) this.btSelect);
    this.splitContainer2.Panel2.Controls.Add((Control) this.bOK);
    this.splitContainer2.Panel2.Controls.Add((Control) this.bCancel);
    this.splitContainer2.Size = new Size(522, 417);
    this.splitContainer2.SplitterDistance = 192 /*0xC0*/;
    this.splitContainer2.TabIndex = 0;
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.propertyGrid1);
    this.groupBox1.ForeColor = SystemColors.HotTrack;
    this.groupBox1.Location = new Point(6, 3);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(513, 186);
    this.groupBox1.TabIndex = 0;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Свойства атрибута в базе-источнике";
    this.propertyGrid1.Dock = DockStyle.Fill;
    this.propertyGrid1.HelpVisible = false;
    this.propertyGrid1.Location = new Point(3, 16 /*0x10*/);
    this.propertyGrid1.Name = "propertyGrid1";
    this.propertyGrid1.Size = new Size(507, 167);
    this.propertyGrid1.TabIndex = 0;
    this.btCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btCreate.Location = new Point(136, 173);
    this.btCreate.Name = "btCreate";
    this.btCreate.Size = new Size(121, 27);
    this.btCreate.TabIndex = 7;
    this.btCreate.Text = "Создать";
    this.btCreate.UseVisualStyleBackColor = true;
    this.btCreate.Click += new EventHandler(this.OnCreateClick);
    this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox2.Controls.Add((Control) this.propertyGrid2);
    this.groupBox2.ForeColor = SystemColors.HotTrack;
    this.groupBox2.Location = new Point(6, 5);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(510, 162);
    this.groupBox2.TabIndex = 1;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Свойства атрибута в базе-приемнике";
    this.propertyGrid2.Dock = DockStyle.Fill;
    this.propertyGrid2.HelpVisible = false;
    this.propertyGrid2.Location = new Point(3, 16 /*0x10*/);
    this.propertyGrid2.Name = "propertyGrid2";
    this.propertyGrid2.Size = new Size(504, 143);
    this.propertyGrid2.TabIndex = 1;
    this.btSelect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btSelect.Location = new Point(9, 173);
    this.btSelect.Name = "btSelect";
    this.btSelect.Size = new Size(121, 27);
    this.btSelect.TabIndex = 6;
    this.btSelect.Text = "Выбрать";
    this.btSelect.UseVisualStyleBackColor = true;
    this.btSelect.Click += new EventHandler(this.OnSelectClick);
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.Location = new Point(266, 173);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 4;
    this.bOK.Text = "ОК";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(393, 173);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 3;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(729, 417);
    this.Controls.Add((Control) this.splitContainer1);
    this.MinimumSize = new Size(745, 455);
    this.Name = nameof (ImportOldTableConflictEditor);
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
    private Guid _guid;
    public bool Error;
    public FieldRecord FieldRecord;
    public string ErrorMessage;
    public ImportOldTableConflictEditor.TableAttribute[] TableAttributes;

    public Guid Guid
    {
      get => this._guid;
      set => this._guid = value;
    }

    public AttributeItem(FieldRecord fieldRecord)
    {
      this.FieldRecord = fieldRecord;
      this.Error = false;
      this.TableAttributes = new ImportOldTableConflictEditor.TableAttribute[2];
    }
  }

  private class TableAttribute
  {
    [Browsable(false)]
    public FieldTypes FieldType;

    [DisplayName("Глобальный идентификатор")]
    public string Guid { get; private set; }

    [DisplayName("Наименование")]
    public string Name { get; private set; }

    [DisplayName("Тип данных")]
    public string FieldTypeDescription
    {
      get => EnumDescConverter.GetEnumDescription((Enum) this.FieldType);
    }

    [DisplayName("Размер")]
    public string Size { get; private set; }

    [Browsable(false)]
    public int Id { get; private set; }

    [DisplayName("Список")]
    public string MultiValueModeDescription
    {
      get => EnumDescConverter.GetEnumDescription((Enum) this.MultiValueMode);
    }

    [Browsable(false)]
    public MultiValueModes MultiValueMode { get; private set; }

    public TableAttribute(
      int id,
      Guid guid,
      string name,
      FieldTypes fieldType,
      long size,
      MultiValueModes multiValueMode,
      PossibleValuesCollection pvc)
    {
      this.Id = id;
      this.Guid = guid.ToString();
      this.Name = name;
      this.FieldType = fieldType;
      this.Size = fieldType == FieldTypes.ftString ? size.ToString() : string.Empty;
      this.MultiValueMode = multiValueMode;
    }
  }
}
