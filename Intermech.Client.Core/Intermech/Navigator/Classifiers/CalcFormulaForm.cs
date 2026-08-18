
// Type: Intermech.Navigator.Classifiers.CalcFormulaForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Columns;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;


namespace Intermech.Navigator.Classifiers;

/// <summary>Summary description for CalcFormulaForm.</summary>
public class CalcFormulaForm : Form
{
  private int _parentMode;
  /// <summary>Были ли изменения в правиле отбора</summary>
  public bool IsChanged;
  private ClassifierCalcFormula _currentClassifier;
  private ClassifierCalcFormula _parentClassifier;
  private ArrayList _parentAttributes = new ArrayList();
  private int _currentIndex = -1;
  public DataTable _dataSource;
  private bool _childPresent;
  private IContainer components;
  private RepositoryItemCalcEdit repositoryItemCalcEdit1;
  private RepositoryItemTextEdit repositoryItemTextEdit1;
  private RepositoryItemComboBox repositoryItemComboBox2;
  private RepositoryItemComboBox repositoryItemComboBox1;
  private RepositoryItemDateEdit repositoryItemDateEdit1;
  private Panel panel2;
  private Panel panel3;
  private Panel panel1;
  private GridControl gridControl2;
  private GridView gridView;
  private Button btnCancel;
  private Button btnApply;
  private Button btnDelete;
  private Button btnAdd;
  private Button btnImport;
  private RepositoryItemTextEdit repositoryItemTextEdit2;
  private RepositoryItemComboBox repositoryItemComboBox3;
  private Button bCreateDown;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem miAdd;
  private ToolStripMenuItem miDelete;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripMenuItem miImport;
  private GroupBox groupBox2;
  private ImageList imageList1;
  private GroupBox groupBox1;
  internal PictureBox pictureBox1;
  private Label lDataType;
  private Label lAttr;
  private Panel panel4;
  private CheckBox cbSizeControl;
  private CheckBox cbUseMissed;
  private RepositoryItemButtonEdit repositoryItemButtonEdit1;
  /// <summary>
  /// Список типов объектов к которым относиться данный классификатор/папка
  /// </summary>
  private List<int> _forObjectTypes;

  /// <summary>
  /// Где размещена наша форма
  /// 0 - самостоятельная форма (по дефолту)
  /// 1 - на форме-создателе новых объектов
  /// 2 - на вьюшке "Навигатора"
  /// </summary>
  public int ParentMode
  {
    get => this._parentMode;
    set
    {
      this._parentMode = value;
      if (value != 1)
        return;
      this.btnApply.Visible = false;
      this.btnCancel.Visible = false;
    }
  }

  public ClassifierCalcFormula parentClassifier
  {
    get => this._parentClassifier;
    set
    {
      if (this._parentClassifier == value)
        return;
      this._parentClassifier = value;
      this._parentAttributes = new ArrayList();
      if (value == null || value.CalcFormulaValue == null || value.CalcFormulaValue.Length == 0)
        return;
      foreach (object obj in value.CalcFormulaValue)
      {
        if (obj != null && obj.ToString() != string.Empty)
          this._parentAttributes.Add((object) CalcFormulaRules.GetAttributeAndFormula(obj.ToString()).AttributeGuid);
      }
      this._parentAttributes.Sort();
    }
  }

  public ClassifierCalcFormula CurrentClassifier
  {
    get => this._currentClassifier;
    set
    {
      if (this._currentClassifier != value)
        this._currentClassifier = value;
      this._dataSource = this.SetDataSource();
    }
  }

  public CalcFormulaForm() => this.InitializeComponent();

  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.AutoScaleMode = AutoScaleMode.None;
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.AutoScaleMode = AutoScaleMode.None;
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
  }

  private DataTable SetDataSource()
  {
    return this._dataSource != null ? CalcFormulaRules.RefreshDataTable(this.CurrentClassifier, this._parentAttributes) : CalcFormulaRules.FormingDataTable(this.CurrentClassifier, this._parentAttributes);
  }

  /// <summary>Сохранить данные в объект с ID = RuleObjectID</summary>
  public void SaveObjectData()
  {
    this.SaveNotCheckedData();
    this.CurrentClassifier.ApplyChanges();
  }

  /// <summary>Загрузить данные в форму</summary>
  public void LoadObjectData()
  {
    this.lDataType.Text = string.Empty;
    this.lAttr.Text = string.Empty;
    this._forObjectTypes = (List<int>) null;
    if (this._currentClassifier == null)
      return;
    GridView mainView = this.gridControl2.MainView as GridView;
    mainView.ClearGrouping();
    mainView.ClearSorting();
    mainView.ClearColumnsFilter();
    this.gridControl2.DataSource = (object) null;
    this.gridControl2.DataSource = (object) this._dataSource;
    GridColumn gridColumn1 = mainView.Columns.ColumnByFieldName("TAG");
    if (gridColumn1 != null)
      gridColumn1.VisibleIndex = -1;
    GridColumn gridColumn2 = mainView.Columns.ColumnByFieldName("PARENT");
    if (gridColumn2 != null)
      gridColumn2.VisibleIndex = -1;
    this.IsChanged = false;
    this.btnApply.Enabled = false;
    this.btnCancel.Enabled = false;
    if (this.parentClassifier != null)
      this.btnImport.Enabled = this.IsEnableImport();
    else
      this.btnImport.Enabled = false;
    this.btnDelete.Enabled = this._dataSource.Rows.Count > 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SortedRelationTypeID);
      relationCollection.ChildObjectTypes = (IList<int>) MetaDataHelper.OptimizeChildObjectTypes((IEnumerable<int>) MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545")));
      this._childPresent = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
      {
        (object) -2
      }), this._currentClassifier.ClassifierID, false).Rows.Count > 0;
    }
    this.bCreateDown.Enabled = this.IsEnableCreateDown();
    this.RefreshSizeControl();
  }

  private void RefreshSizeControl()
  {
    bool visible = false;
    if (this._dataSource.Rows.Count > 0)
    {
      int[] selectedRows = this.gridView.GetSelectedRows();
      if (selectedRows != null && selectedRows.Length != 0)
        visible = this.CurrentClassifier.Attributes[Convert.ToInt32(this.gridView.GetDataRow(selectedRows[0])["TAG"])].AttributeValue.AttrType == FieldTypes.ftString;
    }
    this.VisibleSizeControl(visible);
  }

  private bool IsEnableCreateDown() => this._childPresent && this._dataSource.Rows.Count > 0;

  /// <summary>Определяем енабле кнопки "Импортировать"</summary>
  private bool IsEnableImport()
  {
    if (this.parentClassifier == null || this._parentAttributes.Count == 0)
      return false;
    ArrayList arrayList = new ArrayList();
    if (this.CurrentClassifier.Attributes != null)
    {
      foreach (ClassifierAttribute attribute in this.CurrentClassifier.Attributes)
      {
        if (attribute.Action != ClassifierAttributesAction.Delete)
          arrayList.Add((object) attribute.AttributeValue.AttrGUID);
      }
    }
    if (arrayList.Count > 1)
      arrayList.Sort();
    for (int index = 0; index < this._parentAttributes.Count; ++index)
    {
      if (arrayList.BinarySearch((object) this._parentAttributes[index].ToString()) < 0)
        return true;
    }
    return false;
  }

  private void SaveNotCheckedData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int rowHandle = 0; rowHandle < this.gridView.RowCount; ++rowHandle)
      {
        DataRow dataRow = this.gridView.GetDataRow(rowHandle);
        if (dataRow != null)
        {
          int int32 = Convert.ToInt32(dataRow["TAG"]);
          if (dataRow["FORMULA"].ToString() != this.CurrentClassifier.Attributes[int32].Formula.Caption)
          {
            this.CurrentClassifier.Attributes[int32].Formula = CalcFormulaRules.GetFormula(sessionKeeper.Session, this.CurrentClassifier.Attributes[int32].AttributeValue.AttrType, (object) Convert.ToString(dataRow["FORMULA"]), false);
            this.CurrentClassifier.FormCalcFormulaValue(int32);
          }
        }
      }
    }
  }

  private void RefreshGrid()
  {
    this.lDataType.Text = string.Empty;
    this.lAttr.Text = string.Empty;
    int currentIndex = this._currentIndex;
    this._dataSource = this.SetDataSource();
    this.gridControl2.DataSource = (object) this._dataSource;
    this.gridControl2.RefreshDataSource();
    bool flag = false;
    for (int rowHandle = 0; rowHandle < this.gridView.RowCount; ++rowHandle)
    {
      if (this.gridView.GetDataRow(rowHandle)["TAG"].ToString() == currentIndex.ToString())
      {
        this.gridView.FocusedRowHandle = rowHandle;
        this.gridView.SelectRow(rowHandle);
        flag = true;
        break;
      }
    }
    if (!flag)
    {
      this.gridView.FocusedRowHandle = 0;
      this.gridView.SelectRow(0);
    }
    this.RefreshButtons();
  }

  private void RefreshButtons()
  {
    this.btnApply.Enabled = this.IsChanged;
    this.btnCancel.Enabled = this.IsChanged;
    this.btnImport.Enabled = this.IsEnableImport();
    this.btnDelete.Enabled = this._dataSource.Rows.Count > 0;
    this.bCreateDown.Enabled = this.IsEnableCreateDown();
  }

  /// <summary>
  /// Проверяет, есть ли у атрибута допустимые значения и если есть, то
  /// заполняет контрол и в нем выделяет текущее значение
  /// </summary>
  private bool CheckPossibleValues(CustomRowCellEditEventArgs e, int index)
  {
    if (this.CurrentClassifier.Attributes[index].AttributeValue.AttrPossibleValues.Count <= 0)
      return false;
    e.RepositoryItem = (RepositoryItem) this.repositoryItemComboBox2;
    this.repositoryItemComboBox2.Items.Clear();
    foreach (MyElement attrPossibleValue in this.CurrentClassifier.Attributes[index].AttributeValue.AttrPossibleValues)
      this.repositoryItemComboBox2.Items.Add((object) attrPossibleValue.Caption);
    return true;
  }

  private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
  {
    DataRow dataRow = e.Column.View.GetDataRow(this.gridView.FocusedRowHandle);
    if (dataRow == null)
      return;
    int int32 = Convert.ToInt32(dataRow["TAG"]);
    CalcFormulaAttribute attributeValue = this.CurrentClassifier.Attributes[int32].AttributeValue;
    if (attributeValue != null)
    {
      this.VisibleSizeControl(attributeValue.AttrType == FieldTypes.ftString);
      this.cbSizeControl.CheckedChanged -= new EventHandler(this.cbSizeControl_CheckedChanged);
      this.cbSizeControl.Checked = this.CurrentClassifier.Attributes[int32].SizeControl;
      this.cbSizeControl.CheckedChanged += new EventHandler(this.cbSizeControl_CheckedChanged);
      this.cbUseMissed.CheckedChanged -= new EventHandler(this.cbUseMissed_CheckedChanged);
      this.cbUseMissed.Checked = this.CurrentClassifier.Attributes[int32].UseMissed;
      this.cbUseMissed.CheckedChanged += new EventHandler(this.cbUseMissed_CheckedChanged);
      this._currentIndex = int32;
      this.lDataType.Text = EnumDescConverter.GetEnumDescription((Enum) attributeValue.AttrType);
      this.lAttr.Text = Convert.ToInt16(dataRow["PARENT"]) == (short) 0 ? LocalizationHolder.rm.GetString("Client.Core_244") : LocalizationHolder.rm.GetString("Client.Core_245");
      this.pictureBox1.Image = Convert.ToInt16(dataRow["PARENT"]) == (short) 0 ? this.imageList1.Images[0] : this.imageList1.Images[1];
      if (e.Column.FieldName == "ATTRIBUTE")
        e.RepositoryItem = (RepositoryItem) this.repositoryItemComboBox1;
      if (!(e.Column.FieldName == "FORMULA"))
        return;
      switch (this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrType)
      {
        case FieldTypes.ftString:
          if (this.CheckPossibleValues(e, this._currentIndex))
            break;
          e.RepositoryItem = (RepositoryItem) this.repositoryItemTextEdit1;
          break;
        case FieldTypes.ftInteger:
          if (this.CheckPossibleValues(e, this._currentIndex))
            break;
          e.RepositoryItem = (RepositoryItem) this.repositoryItemCalcEdit1;
          break;
        case FieldTypes.ftDouble:
          if (this.CheckPossibleValues(e, this._currentIndex))
            break;
          e.RepositoryItem = (RepositoryItem) this.repositoryItemCalcEdit1;
          break;
        case FieldTypes.ftDateTime:
          if (!this.CheckPossibleValues(e, this._currentIndex))
            e.RepositoryItem = (RepositoryItem) this.repositoryItemDateEdit1;
          this.SetEditAndDisplayFormat(this.repositoryItemDateEdit1, this.CurrentClassifier.Attributes[int32].Formula.Caption);
          break;
        case FieldTypes.ftObjectLink:
        case FieldTypes.ftObjectLinkByID:
          if (this.CheckPossibleValues(e, this._currentIndex))
            break;
          e.RepositoryItem = (RepositoryItem) this.repositoryItemButtonEdit1;
          break;
        case FieldTypes.ftBoolean:
          if (this.CheckPossibleValues(e, this._currentIndex))
            break;
          e.RepositoryItem = (RepositoryItem) this.repositoryItemComboBox2;
          this.repositoryItemComboBox2.Items.Clear();
          this.repositoryItemComboBox2.Items.Add((object) Intermech.Consts.TrueValue);
          this.repositoryItemComboBox2.Items.Add((object) Intermech.Consts.FalseValue);
          break;
        case FieldTypes.ftAutoInc:
          if (this.CheckPossibleValues(e, this._currentIndex))
            break;
          e.RepositoryItem = (RepositoryItem) this.repositoryItemCalcEdit1;
          break;
      }
    }
    else
    {
      this.lDataType.Text = string.Empty;
      this.lAttr.Text = string.Empty;
      this.VisibleSizeControl(false);
    }
  }

  private void VisibleSizeControl(bool visible)
  {
    this.cbSizeControl.Visible = visible;
    this.cbUseMissed.Visible = visible;
  }

  /// <summary>формат даты в соответствии с настройками винды</summary>
  /// <param name="dateEdit"></param>
  /// <param name="value"></param>
  private void SetEditAndDisplayFormat(RepositoryItemDateEdit dateEdit, string value)
  {
    if (string.IsNullOrEmpty(value))
      return;
    value = DateTimeHelper.GenerateVisibleDateFormat(value);
    dateEdit.DisplayFormat.FormatString = value;
    dateEdit.EditFormat.FormatString = value;
  }

  /// <summary>Открываем selectionview для выбора атрибута</summary>
  private void repositoryItemComboBox1_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    string str = string.Empty;
    if (this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue != null)
      str = this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrGUID;
    ArrayList attributesDialog = this.GetAttributesDialog(false);
    if (attributesDialog == null)
      return;
    this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue = new CalcFormulaAttribute(Convert.ToInt32(attributesDialog[0]));
    if (str != this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrGUID)
      this.CurrentClassifier.Attributes[this._currentIndex].Formula = new MyElement();
    this.CurrentClassifier.FormCalcFormulaValue(this._currentIndex);
    this.IsChanged = true;
    this.RefreshGrid();
  }

  /// <summary>Выбор объекта для атрибута ссылки на объект</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    CalcFormulaAttribute attributeValue = this.CurrentClassifier.Attributes[Convert.ToInt32(this.gridView.Columns.View.GetDataRow(this.gridView.FocusedRowHandle)["TAG"])].AttributeValue;
    if (attributeValue == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attributeValue.AttrID);
      int int32 = Convert.ToInt32(attributeType.SizeType);
      long[] numArray = int32 >= 0 ? SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_22"), $"{LocalizationHolder.rm.GetString("Client.Core_248")}{attributeType.Name}'", int32, SelectionOptions.Default) : SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_22"), $"{LocalizationHolder.rm.GetString("Client.Core_248")}{attributeType.Name}'", SelectionOptions.Default);
      if (numArray == null || numArray.Length == 0)
        return;
      this.CurrentClassifier.Attributes[this._currentIndex].Formula = CalcFormulaRules.GetFormula(sessionKeeper.Session, this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrType, (object) numArray[0], false);
      this.CurrentClassifier.FormCalcFormulaValue(this._currentIndex);
      this.IsChanged = true;
      this.RefreshGrid();
    }
  }

  /// <summary>Выходим из текст едита</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void repositoryItemTextEdit1_Leave(object sender, EventArgs e)
  {
    string text = ((Control) sender).Text;
    if (!(this.CurrentClassifier.Attributes[this._currentIndex].Formula.Caption != text))
      return;
    if (!CalcFormulaRules.CheckFormula(text))
    {
      int num = (int) IMMessageBox.Show(MessageDialogs.msgError, LocalizationHolder.rm.GetString("Client.Core_249") + text, MessageBoxButtons.OK, IMMessageBoxImage.Error);
      ((Control) sender).Text = this.CurrentClassifier.Attributes[this._currentIndex].Formula.Caption;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.CurrentClassifier.Attributes[this._currentIndex].Formula = CalcFormulaRules.GetFormula(sessionKeeper.Session, this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrType, (object) text, false);
      this.CurrentClassifier.FormCalcFormulaValue(this._currentIndex);
      this.IsChanged = true;
      this.RefreshGrid();
    }
  }

  private void repositoryItemTextEdit1_EditValueChanged(object sender, EventArgs e)
  {
    if (!(this.CurrentClassifier.Attributes[this._currentIndex].Formula.Caption != ((Control) sender).Text))
      return;
    this.IsChanged = true;
    this.RefreshButtons();
  }

  private void repositoryItemCalcEdit1_Leave(object sender, EventArgs e)
  {
    if (!(this.CurrentClassifier.Attributes[this._currentIndex].Formula.Caption != ((Control) sender).Text))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.CurrentClassifier.Attributes[this._currentIndex].Formula = CalcFormulaRules.GetFormula(sessionKeeper.Session, this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrType, (object) ((Control) sender).Text, false);
    this.CurrentClassifier.FormCalcFormulaValue(this._currentIndex);
    this.IsChanged = true;
    this.RefreshGrid();
  }

  private void repositoryItemCalcEdit1_EditValueChanged(object sender, EventArgs e)
  {
    if (!(this.CurrentClassifier.Attributes[this._currentIndex].Formula.Caption != ((Control) sender).Text))
      return;
    this.IsChanged = true;
    this.RefreshButtons();
  }

  private void repositoryItemComboBox2_Leave(object sender, EventArgs e)
  {
    if (!(this.CurrentClassifier.Attributes[this._currentIndex].Formula.Caption != ((Control) sender).Text) || this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue == null)
      return;
    if (this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrPossibleValues.Count > 0)
    {
      int selectedIndex = ((ComboBoxEdit) sender).SelectedIndex;
      if (selectedIndex >= 0)
        this.CurrentClassifier.Attributes[this._currentIndex].Formula = (MyElement) this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrPossibleValues[selectedIndex];
    }
    if (this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrType == FieldTypes.ftBoolean)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.CurrentClassifier.Attributes[this._currentIndex].Formula = CalcFormulaRules.GetFormula(sessionKeeper.Session, this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrType, ((Control) sender).Text == Intermech.Consts.TrueValue ? (object) "1" : (object) "0", false);
    }
    this.CurrentClassifier.FormCalcFormulaValue(this._currentIndex);
    this.IsChanged = true;
    this.RefreshGrid();
  }

  private void repositoryItemDateEdit1_Leave(object sender, EventArgs e)
  {
    string dateTime = Convert.ToString(((BaseEdit) sender).EditValue);
    if (!(this.CurrentClassifier.Attributes[this._currentIndex].Formula.Caption != dateTime) || !DateTimeHelper.IsDateValid(dateTime))
      return;
    this.SetEditAndDisplayFormat(this.repositoryItemDateEdit1, dateTime);
    if (((DateEdit) sender).DateTime.Ticks > 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.CurrentClassifier.Attributes[this._currentIndex].Formula = CalcFormulaRules.GetFormula(sessionKeeper.Session, this.CurrentClassifier.Attributes[this._currentIndex].AttributeValue.AttrType, (object) ((Control) sender).Text, false);
      this.CurrentClassifier.FormCalcFormulaValue(this._currentIndex);
      this.RefreshGrid();
      this.IsChanged = true;
    }
    else
      this.CurrentClassifier.Attributes[this._currentIndex].Formula = new MyElement();
    this.IsChanged = true;
    this.RefreshGrid();
  }

  private void repositoryItemDateEdit1_EditValueChanged(object sender, EventArgs e)
  {
    string dateTime = Convert.ToString(((BaseEdit) sender).EditValue);
    if (!(this.CurrentClassifier.Attributes[this._currentIndex].Formula.Caption != dateTime) || !DateTimeHelper.IsDateValid(dateTime))
      return;
    this.SetEditAndDisplayFormat(this.repositoryItemDateEdit1, dateTime);
    this.IsChanged = true;
    this.RefreshButtons();
  }

  private void LoadObjectTypes()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long objectID = this._currentClassifier.ClassifierID;
      IDBObject dbObject = (IDBObject) null;
      if (this._parentClassifier != null)
      {
        IDBObject childClassifier = sessionKeeper.Session.GetObject(this._parentClassifier.ClassifierID);
        if (MetaDataHelper.GetObjectTypeChildrenID(new Guid("cad00157-306c-11d8-b4e9-00304f19f545")).Contains(childClassifier.ObjectType))
        {
          if (childClassifier.ObjectType == MetaDataHelper.GetObjectTypeID(new Guid("cad00150-306c-11d8-b4e9-00304f19f545")))
          {
            long rootClassifier = (sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService).GetRootClassifier((object) sessionKeeper.Session.SessionGUID, childClassifier);
            if (rootClassifier != 0L)
              objectID = rootClassifier;
          }
          else
          {
            objectID = this._parentClassifier.ClassifierID;
            dbObject = childClassifier;
          }
        }
      }
      if (dbObject == null)
        dbObject = sessionKeeper.Session.GetObject(objectID);
      IDBAttribute byGuid1 = dbObject.Attributes.FindByGUID(new Guid("cad00e8f-306c-11d8-b4e9-00304f19f545"));
      if (byGuid1 != null && !byGuid1.IsNull && Convert.ToInt32(byGuid1.Value) == 3)
      {
        IDBAttribute byGuid2 = dbObject.Attributes.FindByGUID(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
        if (byGuid2 == null || byGuid2.ValuesCount <= 0)
          return;
        this._forObjectTypes = new List<int>(byGuid2.ValuesCount);
        for (int index1 = 0; index1 < byGuid2.ValuesCount; ++index1)
        {
          if (CompareValuesHelper.NormalizedValue(byGuid2.Values[index1]) != null)
          {
            string str = byGuid2.Values[index1].ToString();
            if (GuidHelper.IsGuid(str))
            {
              Guid objTypeGuid = new Guid(str);
              if (objTypeGuid != Guid.Empty)
              {
                int objectTypeId = MetaDataHelper.GetObjectTypeID(objTypeGuid);
                if (!this._forObjectTypes.Contains(objectTypeId))
                  this._forObjectTypes.Add(objectTypeId);
                List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(objectTypeId);
                for (int index2 = 0; index2 < objectTypeChildrenId.Count; ++index2)
                {
                  if (!this._forObjectTypes.Contains(objectTypeChildrenId[index2]))
                    this._forObjectTypes.Add(objectTypeChildrenId[index2]);
                }
              }
            }
          }
        }
      }
      else
        this._forObjectTypes = new List<int>(0);
    }
  }

  private ArrayList GetAttributesDialog(bool multiSelect)
  {
    if (this._currentClassifier == null)
      return (ArrayList) null;
    if (this._forObjectTypes == null)
      this.LoadObjectTypes();
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(multiSelect);
    if (this._forObjectTypes.Count > 0)
      attributesSelectDlg.LoadAttrDialogForObjectsTypes(this._forObjectTypes);
    List<int> attributes = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetAttributeTypeCollection(-1).Select(string.Empty);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        switch ((MultiValueModes) Convert.ToInt32(dataTable.Rows[index]["F_MULTIPLE_VALUED"]))
        {
          case MultiValueModes.MultiValues:
          case MultiValueModes.MultiValuesFromList:
            attributes.Add(Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"]));
            break;
          default:
            if (CalcFormulaRules.possibleTypes.Contains((object) (FieldTypes) Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_TYPE"])))
              break;
            goto case MultiValueModes.MultiValues;
        }
      }
    }
    if (this.CurrentClassifier.Attributes != null && this.CurrentClassifier.Attributes.Length != 0)
    {
      foreach (ClassifierAttribute attribute in this.CurrentClassifier.Attributes)
      {
        if (attribute.Action != ClassifierAttributesAction.Delete && !attributes.Contains(attribute.AttributeValue.AttrID))
          attributes.Add(attribute.AttributeValue.AttrID);
      }
    }
    attributesSelectDlg.SelectorFilter = (ISelectorFilter) new CalcFormulaForm.AttributesFilter(attributes);
    return attributesSelectDlg.ShowDialog() == DialogResult.OK && attributesSelectDlg.SelectedAttributesID.Count > 0 ? new ArrayList((ICollection) attributesSelectDlg.SelectedAttributesID.ToArray()) : (ArrayList) null;
  }

  private void AddAttribute()
  {
    this.SaveNotCheckedData();
    ArrayList attributesDialog = this.GetAttributesDialog(true);
    if (attributesDialog == null)
      return;
    for (int index = 0; index < attributesDialog.Count; ++index)
      this.CurrentClassifier.AddAttribute(new CalcFormulaAttribute(Convert.ToInt32(attributesDialog[index])), new MyElement(), false, false);
    this.IsChanged = true;
    this._currentIndex = this.CurrentClassifier.Attributes.Length - 1;
    this.RefreshGrid();
  }

  private void DeleteAttribute()
  {
    this.SaveNotCheckedData();
    StringBuilder stringBuilder = new StringBuilder();
    int[] selectedRows = this.gridView.GetSelectedRows();
    CalcFormulaAttribute[] attr = new CalcFormulaAttribute[this.gridView.SelectedRowsCount];
    int num = 0;
    for (int index = 0; index < this.gridView.SelectedRowsCount; ++index)
    {
      DataRow dataRow = this.gridView.GetDataRow(selectedRows[index]);
      if (dataRow != null)
      {
        if (num >= 100)
        {
          stringBuilder.Append("\n");
          num = 0;
        }
        string str = $"\"{this.CurrentClassifier.Attributes[Convert.ToInt32(dataRow["TAG"])].AttributeValue.AttrName}\",";
        stringBuilder.Append(str);
        attr[index] = this.CurrentClassifier.Attributes[Convert.ToInt32(dataRow["TAG"])].AttributeValue;
        num += str.Length;
      }
    }
    if (stringBuilder.Length <= 0)
      return;
    stringBuilder.Remove(stringBuilder.Length - 1, 1);
    string format = this.gridView.SelectedRowsCount > 1 ? LocalizationHolder.rm.GetString("Client.Core_253") : LocalizationHolder.rm.GetString("Client.Core_252");
    if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_255"), string.Format(format, (object) stringBuilder), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    this.CurrentClassifier.DeleteAttribute(attr);
    this.IsChanged = true;
    this._currentIndex = this.CurrentClassifier.Attributes.Length != 0 ? this._currentIndex - 1 : 0;
    this.RefreshGrid();
  }

  private void ImportAttributes()
  {
    if (this._parentAttributes.Count <= 0 || IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1211"), LocalizationHolder.rm.GetString("Client.Core_254"), MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    List<string> stringList = new List<string>(this.CurrentClassifier.Attributes != null ? this.CurrentClassifier.Attributes.Length : 0);
    if (this.CurrentClassifier.Attributes != null)
    {
      foreach (ClassifierAttribute attribute in this.CurrentClassifier.Attributes)
      {
        if (attribute.Action != ClassifierAttributesAction.Delete)
          stringList.Add(attribute.AttributeValue.AttrGUID);
      }
    }
    bool flag = false;
    for (int index = 0; index < this._parentAttributes.Count; ++index)
    {
      if (!stringList.Contains(this._parentAttributes[index].ToString()))
      {
        this.CurrentClassifier.AddAttribute(new CalcFormulaAttribute(this._parentAttributes[index].ToString()), new MyElement(), false, false);
        flag = true;
      }
    }
    if (!flag)
      return;
    this.IsChanged = true;
    this.RefreshGrid();
  }

  private void btnAdd_Click(object sender, EventArgs e) => this.AddAttribute();

  private void btnDelete_Click(object sender, EventArgs e) => this.DeleteAttribute();

  private void btnApply_Click(object sender, EventArgs e)
  {
    this.SaveNotCheckedData();
    this.CurrentClassifier.ApplyChanges();
    this.IsChanged = false;
    this.RefreshGrid();
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    this._currentClassifier = new ClassifierCalcFormula(this._currentClassifier.ClassifierID);
    this._dataSource = (DataTable) null;
    this._currentIndex = 0;
    this.IsChanged = false;
    this.RefreshGrid();
  }

  private void btnImport_Click(object sender, EventArgs e) => this.ImportAttributes();

  private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
  {
    DataRow dataRow = this.gridView.GetDataRow(e.RowHandle);
    if (dataRow == null || Convert.ToInt16(dataRow["PARENT"]) == (short) 0 || this.gridView.SelectedRowsCount <= 0)
      return;
    ArrayList arrayList = new ArrayList();
    arrayList.AddRange((ICollection) this.gridView.GetSelectedRows());
    if (arrayList.Count > 1)
      arrayList.Sort();
    arrayList.BinarySearch((object) e.RowHandle);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CalcFormulaForm));
    this.repositoryItemCalcEdit1 = new RepositoryItemCalcEdit();
    this.repositoryItemTextEdit1 = new RepositoryItemTextEdit();
    this.repositoryItemComboBox2 = new RepositoryItemComboBox();
    this.repositoryItemComboBox1 = new RepositoryItemComboBox();
    this.repositoryItemDateEdit1 = new RepositoryItemDateEdit();
    this.repositoryItemButtonEdit1 = new RepositoryItemButtonEdit();
    this.panel2 = new Panel();
    this.groupBox1 = new GroupBox();
    this.pictureBox1 = new PictureBox();
    this.lDataType = new Label();
    this.lAttr = new Label();
    this.panel4 = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.panel3 = new Panel();
    this.cbSizeControl = new CheckBox();
    this.bCreateDown = new Button();
    this.btnImport = new Button();
    this.btnDelete = new Button();
    this.btnAdd = new Button();
    this.panel1 = new Panel();
    this.groupBox2 = new GroupBox();
    this.gridControl2 = new GridControl();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.miAdd = new ToolStripMenuItem();
    this.miDelete = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.miImport = new ToolStripMenuItem();
    this.gridView = new GridView();
    this.repositoryItemTextEdit2 = new RepositoryItemTextEdit();
    this.repositoryItemComboBox3 = new RepositoryItemComboBox();
    this.imageList1 = new ImageList(this.components);
    this.cbUseMissed = new CheckBox();
    this.repositoryItemCalcEdit1.BeginInit();
    this.repositoryItemTextEdit1.BeginInit();
    this.repositoryItemComboBox2.BeginInit();
    this.repositoryItemComboBox1.BeginInit();
    this.repositoryItemDateEdit1.BeginInit();
    this.repositoryItemButtonEdit1.BeginInit();
    this.panel2.SuspendLayout();
    this.groupBox1.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.panel4.SuspendLayout();
    this.panel3.SuspendLayout();
    this.panel1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.gridControl2.BeginInit();
    this.contextMenuStrip1.SuspendLayout();
    this.gridView.BeginInit();
    this.repositoryItemTextEdit2.BeginInit();
    this.repositoryItemComboBox3.BeginInit();
    this.SuspendLayout();
    this.repositoryItemCalcEdit1.AutoHeight = false;
    this.repositoryItemCalcEdit1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
    this.repositoryItemCalcEdit1.EditValueChanged += new EventHandler(this.repositoryItemCalcEdit1_EditValueChanged);
    this.repositoryItemCalcEdit1.Leave += new EventHandler(this.repositoryItemCalcEdit1_Leave);
    this.repositoryItemTextEdit1.AutoHeight = false;
    this.repositoryItemTextEdit1.MaskData.SaveLiteral = (bool) componentResourceManager.GetObject("repositoryItemTextEdit1.MaskData.SaveLiteral");
    this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
    this.repositoryItemTextEdit1.EditValueChanged += new EventHandler(this.repositoryItemTextEdit1_EditValueChanged);
    this.repositoryItemTextEdit1.Leave += new EventHandler(this.repositoryItemTextEdit1_Leave);
    this.repositoryItemComboBox2.AutoHeight = false;
    this.repositoryItemComboBox2.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.repositoryItemComboBox2.CaseSensitiveSearch = true;
    this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
    this.repositoryItemComboBox2.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.repositoryItemComboBox2.EditValueChanged += new EventHandler(this.repositoryItemComboBox2_Leave);
    this.repositoryItemComboBox1.AutoHeight = false;
    this.repositoryItemComboBox1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.repositoryItemComboBox1.CycleOnDblClick = false;
    this.repositoryItemComboBox1.HotTrackDropDownItems = false;
    this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
    this.repositoryItemComboBox1.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.UseBackColor | StyleOptions.UseFont | StyleOptions.UseForeColor, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.repositoryItemComboBox1.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.repositoryItemComboBox1.ButtonClick += new ButtonPressedEventHandler(this.repositoryItemComboBox1_ButtonClick);
    this.repositoryItemDateEdit1.AutoHeight = false;
    this.repositoryItemDateEdit1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.repositoryItemDateEdit1.MaskData.EditMask = componentResourceManager.GetString("repositoryItemDateEdit1.MaskData.EditMask");
    this.repositoryItemDateEdit1.MaskData.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("repositoryItemDateEdit1.MaskData.IgnoreMaskBlank");
    this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
    this.repositoryItemDateEdit1.EditValueChanged += new EventHandler(this.repositoryItemDateEdit1_EditValueChanged);
    this.repositoryItemDateEdit1.Leave += new EventHandler(this.repositoryItemDateEdit1_Leave);
    this.repositoryItemButtonEdit1.AutoHeight = false;
    this.repositoryItemButtonEdit1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
    this.repositoryItemButtonEdit1.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.repositoryItemButtonEdit1.ButtonClick += new ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
    this.panel2.Controls.Add((Control) this.groupBox1);
    this.panel2.Controls.Add((Control) this.panel4);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.groupBox1.Controls.Add((Control) this.pictureBox1);
    this.groupBox1.Controls.Add((Control) this.lDataType);
    this.groupBox1.Controls.Add((Control) this.lAttr);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lDataType, "lDataType");
    this.lDataType.Name = "lDataType";
    componentResourceManager.ApplyResources((object) this.lAttr, "lAttr");
    this.lAttr.Name = "lAttr";
    this.panel4.Controls.Add((Control) this.btnCancel);
    this.panel4.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Name = "panel4";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Hand;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Hand;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.panel3.Controls.Add((Control) this.cbUseMissed);
    this.panel3.Controls.Add((Control) this.cbSizeControl);
    this.panel3.Controls.Add((Control) this.bCreateDown);
    this.panel3.Controls.Add((Control) this.btnImport);
    this.panel3.Controls.Add((Control) this.btnDelete);
    this.panel3.Controls.Add((Control) this.btnAdd);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.cbSizeControl, "cbSizeControl");
    this.cbSizeControl.Name = "cbSizeControl";
    this.cbSizeControl.UseVisualStyleBackColor = true;
    this.cbSizeControl.CheckedChanged += new EventHandler(this.cbSizeControl_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.bCreateDown, "bCreateDown");
    this.bCreateDown.Name = "bCreateDown";
    this.bCreateDown.UseVisualStyleBackColor = true;
    this.bCreateDown.Click += new EventHandler(this.bCreateDown_Click);
    componentResourceManager.ApplyResources((object) this.btnImport, "btnImport");
    this.btnImport.Cursor = Cursors.Hand;
    this.btnImport.Name = "btnImport";
    this.btnImport.Click += new EventHandler(this.btnImport_Click);
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Cursor = Cursors.Hand;
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Cursor = Cursors.Hand;
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.panel1.Controls.Add((Control) this.groupBox2);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.groupBox2.Controls.Add((Control) this.gridControl2);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    this.gridControl2.ContextMenuStrip = this.contextMenuStrip1;
    componentResourceManager.ApplyResources((object) this.gridControl2, "gridControl2");
    this.gridControl2.EmbeddedNavigator.Name = "";
    this.gridControl2.MainView = (BaseView) this.gridView;
    this.gridControl2.Name = "gridControl2";
    this.gridControl2.RepositoryItems.AddRange(new RepositoryItem[2]
    {
      (RepositoryItem) this.repositoryItemTextEdit2,
      (RepositoryItem) this.repositoryItemComboBox3
    });
    this.gridControl2.Styles.AddReplace("EvenRow", (object) new ViewStyleEx("EvenRow", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.UseBackColor, Color.MediumAquamarine, SystemColors.WindowText, Color.GhostWhite, LinearGradientMode.ForwardDiagonal));
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.miAdd,
      (ToolStripItem) this.miDelete,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.miImport
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip1, "contextMenuStrip1");
    this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
    this.miAdd.Name = "miAdd";
    componentResourceManager.ApplyResources((object) this.miAdd, "miAdd");
    this.miAdd.Click += new EventHandler(this.miAdd_Click);
    this.miDelete.Name = "miDelete";
    componentResourceManager.ApplyResources((object) this.miDelete, "miDelete");
    this.miDelete.Click += new EventHandler(this.miDelete_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this.miImport.Name = "miImport";
    componentResourceManager.ApplyResources((object) this.miImport, "miImport");
    this.miImport.Click += new EventHandler(this.miImport_Click);
    this.gridView.GridControl = this.gridControl2;
    componentResourceManager.ApplyResources((object) this.gridView, "gridView");
    this.gridView.Name = "gridView";
    this.gridView.OptionsCustomization.AllowFilter = false;
    this.gridView.OptionsCustomization.AllowGroup = false;
    this.gridView.OptionsMenu.EnableColumnMenu = false;
    this.gridView.OptionsMenu.EnableFooterMenu = false;
    this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
    this.gridView.OptionsSelection.MultiSelect = true;
    this.gridView.OptionsView.ShowDetailButtons = false;
    this.gridView.OptionsView.ShowFilterPanel = false;
    this.gridView.OptionsView.ShowGroupPanel = false;
    this.gridView.CustomDrawCell += new RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
    this.gridView.RowCellStyle += new RowCellStyleEventHandler(this.gridView_RowCellStyle);
    this.gridView.CustomRowCellEdit += new CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
    this.repositoryItemTextEdit2.AllowFocused = false;
    this.repositoryItemTextEdit2.AutoHeight = false;
    this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
    this.repositoryItemComboBox3.AutoHeight = false;
    this.repositoryItemComboBox3.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.repositoryItemComboBox3.Name = "repositoryItemComboBox3";
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "element_new.png");
    this.imageList1.Images.SetKeyName(1, "branch_element.png");
    componentResourceManager.ApplyResources((object) this.cbUseMissed, "cbUseMissed");
    this.cbUseMissed.Name = "cbUseMissed";
    this.cbUseMissed.UseVisualStyleBackColor = true;
    this.cbUseMissed.CheckedChanged += new EventHandler(this.cbUseMissed_CheckedChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel2);
    this.Name = nameof (CalcFormulaForm);
    this.repositoryItemCalcEdit1.EndInit();
    this.repositoryItemTextEdit1.EndInit();
    this.repositoryItemComboBox2.EndInit();
    this.repositoryItemComboBox1.EndInit();
    this.repositoryItemDateEdit1.EndInit();
    this.repositoryItemButtonEdit1.EndInit();
    this.panel2.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.panel4.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.gridControl2.EndInit();
    this.contextMenuStrip1.ResumeLayout(false);
    this.gridView.EndInit();
    this.repositoryItemTextEdit2.EndInit();
    this.repositoryItemComboBox3.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void CalcFormulaForm_Load(object sender, EventArgs e)
  {
    if (this.ParentMode != 0)
      return;
    FormStorage.LoadLayout((Control) this);
  }

  private void CalcFormulaForm_Closed(object sender, EventArgs e)
  {
    if (this.ParentMode != 0)
      return;
    FormStorage.SaveLayout((Control) this);
  }

  private void miImport_Click(object sender, EventArgs e) => this.ImportAttributes();

  private void miDelete_Click(object sender, EventArgs e) => this.DeleteAttribute();

  private void miAdd_Click(object sender, EventArgs e) => this.AddAttribute();

  private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
  {
    this.miDelete.Enabled = this.gridView.FocusedRowHandle >= 0;
    if (this.parentClassifier != null)
      this.miImport.Enabled = this.IsEnableImport();
    else
      this.miImport.Enabled = false;
  }

  private void miChangeAttribute_Click(object sender, EventArgs e)
  {
    this.repositoryItemComboBox1_ButtonClick((object) this, (ButtonPressedEventArgs) null);
  }

  private void gridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
  {
    if (!(e.Column.FieldName == "ATTRIBUTE"))
      return;
    e.Graphics.FillRectangle(e.Style.BackBrush, e.Bounds);
    DataRow dataRow = this.gridView.GetDataRow(e.RowHandle);
    Image image = Convert.ToInt16(dataRow["PARENT"]) == (short) 0 ? this.imageList1.Images[0] : this.imageList1.Images[1];
    Rectangle rect;
    ref Rectangle local = ref rect;
    Rectangle bounds1 = e.Bounds;
    int x = bounds1.X + 2;
    bounds1 = e.Bounds;
    int y = bounds1.Y + 2;
    int width = image.Width;
    int height = image.Height;
    local = new Rectangle(x, y, width, height);
    e.Graphics.DrawImageUnscaled(image, rect);
    Rectangle bounds2 = e.Bounds;
    bounds2.X += image.Width + 4;
    bounds2.Width -= image.Width + 4;
    e.Graphics.DrawString(Convert.ToString(dataRow["ATTRIBUTE"]), e.Style.Font, e.Style.ForeBrush, (RectangleF) bounds2, e.Style.StrFormat);
    e.Handled = true;
  }

  private void bCreateDown_Click(object sender, EventArgs e)
  {
    int[] selectedRows = this.gridView.GetSelectedRows();
    string empty = string.Empty;
    string Message = selectedRows.Length <= 1 ? LocalizationHolder.rm.GetString("Client.Core_259") : LocalizationHolder.rm.GetString("Client.Core_258");
    if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_256"), Message, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> longList = this.LoadChildClassifs(sessionKeeper.Session);
      for (int index1 = 0; index1 < this.gridView.SelectedRowsCount; ++index1)
      {
        DataRow dataRow = this.gridView.GetDataRow(selectedRows[index1]);
        if (dataRow != null)
        {
          ClassifierAttribute attribute = this.CurrentClassifier.Attributes[Convert.ToInt32(dataRow["TAG"])];
          for (int index2 = 0; index2 < longList.Count; ++index2)
          {
            ClassifierCalcFormula classifierCalcFormula = new ClassifierCalcFormula(sessionKeeper.Session, longList[index2]);
            classifierCalcFormula.ChangeAttribute(attribute.AttributeValue, attribute.Formula, attribute.SizeControl, attribute.UseMissed);
            classifierCalcFormula.ApplyChanges(sessionKeeper.Session);
          }
        }
      }
    }
  }

  private List<long> LoadChildClassifs(IUserSession session)
  {
    IFiltrationService service = ServicesManager.GetService(typeof (IFiltrationService)) as IFiltrationService;
    List<ColumnDescriptor> columns = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) -2)
    };
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00157-306c-11d8-b4e9-00304f19f545"));
    List<int> searchRelationTypes = new List<int>(1)
    {
      session.IdentHelper.SortedRelationTypeID
    };
    QuickObjectInfo objectInfo = session.GetObjectInfo(this._currentClassifier.ClassifierID);
    DataTable dataTable = (session.GetCustomService(typeof (ICompositionLoadService)) as ICompositionLoadService).LoadComposition((object) session.SessionGUID, this._currentClassifier.ClassifierID, objectInfo.ObjectTypeID, (IEnumerable<int>) searchRelationTypes, (IEnumerable<int>) childrenIdRecursive, (IEnumerable<ColumnDescriptor>) columns, true, false, service.RuleClass, (IEnumerable<ConditionStructure>) null, service.Filtration.OwnerID, (HybridDictionary) null, -1);
    List<long> longList = new List<long>(dataTable.Rows.Count);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      longList.Add((long) Convert.ToInt32(dataTable.Rows[index][0]));
    return longList;
  }

  private void cbSizeControl_CheckedChanged(object sender, EventArgs e)
  {
    ClassifierAttribute currentAttribute = this.GetCurrentAttribute();
    if (currentAttribute == null)
      return;
    currentAttribute.SizeControl = this.cbSizeControl.Checked;
    this.CurrentClassifier.FormCalcFormulaValue(this._currentIndex);
    this.IsChanged = true;
    this.RefreshButtons();
  }

  private void cbUseMissed_CheckedChanged(object sender, EventArgs e)
  {
    ClassifierAttribute currentAttribute = this.GetCurrentAttribute();
    if (currentAttribute == null)
      return;
    currentAttribute.UseMissed = this.cbUseMissed.Checked;
    this.CurrentClassifier.FormCalcFormulaValue(this._currentIndex);
    this.IsChanged = true;
    this.RefreshButtons();
  }

  private ClassifierAttribute GetCurrentAttribute()
  {
    int[] selectedRows = this.gridView.GetSelectedRows();
    if (selectedRows != null && selectedRows.Length != 0)
    {
      ClassifierAttribute attribute = this.CurrentClassifier.Attributes[Convert.ToInt32(this.gridView.GetDataRow(selectedRows[0])["TAG"])];
      if (attribute != null)
        return attribute;
    }
    return (ClassifierAttribute) null;
  }

  /// <summary>
  /// Фильтр на атрибуты которые уже присутствуют в формулах
  /// </summary>
  private class AttributesFilter : ISelectorFilter
  {
    private List<int> _attributes;

    public AttributesFilter(List<int> attributes) => this._attributes = attributes;

    public bool IsInFilter(int category, object id) => this._attributes.Contains((int) id);
  }
}
