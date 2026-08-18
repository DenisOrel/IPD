// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.TableReportEditor
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Columns;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Document.Client.Reports.Controls;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.Report;

/// <summary>Редактор табличных отчетов</summary>
public class TableReportEditor : Form
{
  /// <summary>ID табличного отчета</summary>
  public long ReportID;
  /// <summary>Тип объектов</summary>
  public int ObjectTypeID = -1;
  /// <summary>Название левой колонки</summary>
  private static readonly string _mainFieldName = LocalizationHolder.rm.GetString("Document.Client_6");
  /// <summary>Таблица для грида</summary>
  private DataTable _table;
  /// <summary>Отчет</summary>
  private TableReport _report;
  public long NewObjectID;
  private static List<int> _nonMathColumn = new List<int>((IEnumerable<int>) new int[10]
  {
    -11,
    -6,
    -39,
    -4,
    -9,
    -7,
    -8,
    -23,
    -52,
    -36
  });
  private int _parentMode;
  private bool _changed;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel2;
  private Panel panel1;
  private TextBox tbReportName;
  private Label label1;
  private Button btnCancel;
  private Button btnApply;
  private Panel panel3;
  private GridControl gridControl1;
  private GridView gridView1;
  private Panel panel5;
  private GroupBox groupBox1;
  private BindingSource bindingSource1;
  private Intermech.Bars.ToolBar toolBarValues;
  private ButtonItem btnAddValue;
  private ButtonItem btnDeleteValue;
  private ButtonItem biPrev;
  private ButtonItem biNext;
  private LabelItem lSelectedColumn;
  private RepositoryItemTextEdit repositoryItemTextEdit;
  private RepositoryItemSpinEdit repositoryItemSpinEdit;
  private RepositoryItemComboBox repositoryItemComboBox1;
  private RepositoryItemComboBox repositoryItemComboBox2;
  private RepositoryItemComboBox repositoryItemComboBox3;
  private TabControl tabControl1;
  private TabPage tabPage1;
  private TabPage tabPage2;
  private TextBox tbReportCaption;
  private Label label3;
  private Label label4;
  private System.Windows.Forms.ComboBox cbDateFormat;
  private CheckBox cbCountItems;
  private CheckBox cbResult;
  private Label label5;
  private System.Windows.Forms.ComboBox cbPageNumberFormat;
  private GroupBox groupBox3;
  private RepositoryItemComboBox repositoryItemComboBox4;
  private RepositoryItemComboBox repositoryItemComboBox5;
  private PersistentRepository persistentRepository1;
  private RepositoryItemButtonEdit repositoryItemButtonEdit1;
  private Button button1;
  private TextBox beTemplate;
  private Label label2;
  private ToolTip toolTipOne;
  private CheckBox cbRowNumbers;
  private GroupBox groupBox2;
  private Label label6;
  private NumericUpDown nudWidthNumColunm;
  private TextBox tbCaptionNumColunm;
  private Label label7;
  private Button btnChooseType;
  private TextBox tbDocType;
  private Label lblType;
  private Button btnDeleteDocType;

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
      if (value == 1)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          sessionKeeper.Session.GetObjectType(this.ObjectTypeID);
          this.Text = LocalizationHolder.rm.GetString("Document.Client_7");
          this.btnApply.Text = LocalizationHolder.rm.GetString("Document.Client_8");
          this.btnCancel.Enabled = true;
        }
      }
      if (value == 0)
      {
        this.btnApply.Text = LocalizationHolder.rm.GetString("Document.Client_9");
        this.btnCancel.Enabled = true;
      }
      this._parentMode = value;
    }
  }

  /// <summary>Флаг изменения данных в форме</summary>
  public bool Changed => this._changed;

  public TableReportEditor()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 855);
    this._report = new TableReport();
    this.UpdateGrid();
    this.SetComboboxes();
    this.SetButtons();
  }

  /// <summary>
  /// Вызвать форму как модальный диалог. При успехе создать новый объект и вернуть его ID
  /// </summary>
  /// <param name="ObjectTypeID">Идентификатор типа создаваемого объекта</param>
  /// <param name="TemplateObjectID">Идентификатор объекта-прототипа</param>
  /// <returns>0 при ошибке или отмене</returns>
  public static long Execute(int ObjectTypeID, long TemplateObjectID)
  {
    if (ObjectTypeID == 0)
      return 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(ObjectTypeID);
      IDBObject dbObject = TemplateObjectID != -1L ? objectCollection.Create(TemplateObjectID) : objectCollection.Create();
      using (TableReportEditor tableReportEditor = new TableReportEditor())
      {
        tableReportEditor.ObjectTypeID = ObjectTypeID;
        tableReportEditor.ParentMode = 1;
        tableReportEditor.ReportID = dbObject.ObjectID;
        tableReportEditor.LoadObjectData(tableReportEditor.ReportID);
        return tableReportEditor.ExecuteForm();
      }
    }
  }

  /// <summary>
  /// Вызвать форму как модальный диалог. При успехе создать новый объект и вернуть его ID
  /// </summary>
  /// <returns>0 при ошибке или отмене</returns>
  private long ExecuteForm()
  {
    this.DialogResult = DialogResult.None;
    int num = (int) this.ShowDialog();
    return this.DialogResult != DialogResult.OK ? 0L : this.ReportID;
  }

  /// <summary>Создание первой колонки в гриде</summary>
  private void InitializeMainColumn()
  {
    string[] strArray = new string[7]
    {
      LocalizationHolder.rm.GetString("Document.Client_10"),
      LocalizationHolder.rm.GetString("Document.Client_11"),
      LocalizationHolder.rm.GetString("Document.Client_12"),
      LocalizationHolder.rm.GetString("Document.Client_13"),
      LocalizationHolder.rm.GetString("Document.Client_14"),
      LocalizationHolder.rm.GetString("Document.Client_15"),
      LocalizationHolder.rm.GetString("Document.Client_16")
    };
    this._table = new DataTable();
    DataColumn column = new DataColumn(TableReportEditor._mainFieldName);
    this._table.Columns.Add(column);
    for (int index = 0; index < strArray.Length; ++index)
    {
      DataRow row = this._table.NewRow();
      row[column] = (object) strArray[index];
      this._table.Rows.Add(row);
    }
    this._table.AcceptChanges();
  }

  /// <summary>Первоначальная установка repository editors</summary>
  private void SetComboboxes()
  {
    foreach (MathTotal mathTotal in Enum.GetValues(typeof (MathTotal)))
      this.repositoryItemComboBox1.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) mathTotal));
    foreach (SortOrders sortOrders in Enum.GetValues(typeof (SortOrders)))
      this.repositoryItemComboBox2.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) sortOrders));
    this.repositoryItemComboBox3.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) MathTotal.None));
    this.repositoryItemComboBox3.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) MathTotal.Min));
    this.repositoryItemComboBox3.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) MathTotal.Max));
    this.repositoryItemComboBox4.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) AttributeSourceTypes.Object));
    this.repositoryItemComboBox4.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) AttributeSourceTypes.Relation));
    foreach (int num in (Intermech.Interfaces.Document.HorzAlignment[]) Enum.GetValues(typeof (Intermech.Interfaces.Document.HorzAlignment)))
      this.repositoryItemComboBox5.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) (Intermech.Interfaces.Document.HorzAlignment) num));
    this.cbPageNumberFormat.Items.Clear();
    this.cbDateFormat.Items.Clear();
    foreach (int num in (PageNumberPosition[]) Enum.GetValues(typeof (PageNumberPosition)))
      this.cbPageNumberFormat.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) (PageNumberPosition) num));
    foreach (int num in (DatePrintFormats[]) Enum.GetValues(typeof (DatePrintFormats)))
      this.cbDateFormat.Items.Add((object) EnumDescConverter.GetEnumDescription((Enum) (DatePrintFormats) num));
    this.cbPageNumberFormat.SelectedIndex = 0;
    this.cbDateFormat.SelectedIndex = 0;
  }

  /// <summary>Загрузка данных в грид</summary>
  private void UpdateGrid()
  {
    this.gridControl1.BeginUpdate();
    this.InitializeMainColumn();
    for (int index = 0; index < this._report.Columns.Count; ++index)
    {
      ReportColumn column = this._report.GetColumn(index);
      if (column != null)
      {
        this._table.Columns.Add(new DataColumn(column.AttributeName));
        this._table.Rows[0][index + 1] = (object) column.Caption;
        this._table.Rows[1][index + 1] = (object) EnumDescConverter.GetEnumDescription((Enum) column.AttributeSource);
        this._table.Rows[2][index + 1] = (object) column.Width;
        this._table.Rows[3][index + 1] = (object) EnumDescConverter.GetEnumDescription((Enum) column.Alignment);
        this._table.Rows[4][index + 1] = (object) EnumDescConverter.GetEnumDescription((Enum) column.SortOrder);
        this._table.Rows[5][index + 1] = (object) column.FormatString;
        this._table.Rows[6][index + 1] = (object) EnumDescConverter.GetEnumDescription((Enum) column.Result);
      }
    }
    this._table.AcceptChanges();
    this.gridControl1.DataSource = (object) new DataView(this._table);
    this.gridView1.PopulateColumns();
    this.gridControl1.EndUpdate();
    this.gridView1.Columns[0].FieldName = TableReportEditor._mainFieldName;
    this.gridView1.Columns[0].Options = ColumnOptions.ReadOnly | ColumnOptions.FixedWidth | ColumnOptions.NonEditable;
    this.gridView1.Columns[0].Fixed = FixedStyle.Left;
    this.gridView1.Columns[0].Width = 150;
    this.gridView1.Columns[0].Style = this.gridControl1.Styles["Style1"];
    if (this._report.Columns.Count <= 0)
      return;
    for (int index = 0; index < this._report.Columns.Count; ++index)
    {
      ReportColumn column = this._report.GetColumn(index);
      if (column != null)
      {
        this.gridView1.Columns[index + 1].Name = column.AttributeName;
        this.gridView1.Columns[index + 1].FieldName = column.AttributeID.ToString();
        this.gridView1.Columns[index + 1].Width = 150;
        this.gridView1.Columns[index + 1].Style = this.gridControl1.Styles["Row"];
        this.gridView1.Columns[index + 1].Options = ColumnOptions.CanResized | ColumnOptions.CanFocused | ColumnOptions.ShowInCustomizationForm;
      }
    }
  }

  /// <summary>Корректно назначить контрол-предок для формы</summary>
  /// <param name="aParent">Владелец формы</param>
  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
  }

  /// <summary>Управление кнопками</summary>
  private void SetButtons()
  {
    this.btnApply.Enabled = this._report.TemplateID != 0L && this._report.Columns.Count > 0 && this._changed;
    if (this.ParentMode == 1)
      this.btnApply.Enabled = this._report.TemplateID != 0L && this._report.Columns.Count > 0;
    if (this.ParentMode == 0 || this.ParentMode == 1)
      this.btnCancel.Enabled = true;
    else
      this.btnCancel.Enabled = this._changed;
  }

  /// <summary>Загрузка данных в форму</summary>
  /// <param name="reportID">Идентификатор отчета</param>
  public void LoadObjectData(long reportID)
  {
    this.ReportID = reportID;
    this._report.LoadData(reportID);
    this.UpdateFormControls();
  }

  /// <summary>Обновить контролы на форме</summary>
  private void UpdateFormControls()
  {
    this.tbReportName.Text = this._report.ReportName;
    this.tbReportCaption.Text = this._report.ReportCaption;
    this.beTemplate.Text = this._report.TemplateName;
    this.cbResult.Checked = this._report.ResultItem;
    this.cbCountItems.Checked = this._report.CountItems;
    this.cbPageNumberFormat.SelectedIndex = (int) this._report.PageNumber;
    this.cbDateFormat.SelectedIndex = (int) this._report.DatePrint;
    this.cbRowNumbers.Checked = this._report.RowNumbers;
    this.nudWidthNumColunm.Value = (Decimal) this._report.RowNumbersColumnWidth;
    this.nudWidthNumColunm.Enabled = this._report.RowNumbers;
    this.tbCaptionNumColunm.Text = this._report.RowNumbersColumnCaption;
    this.tbCaptionNumColunm.Enabled = this._report.RowNumbers;
    if (!string.IsNullOrWhiteSpace(this._report.GeneratedDocTypeGuid))
    {
      this.tbDocType.Text = MetaDataHelper.GetObjectTypeName(new Guid(this._report.GeneratedDocTypeGuid));
      this.tbDocType.Tag = (object) this._report.GeneratedDocTypeGuid;
      this.btnDeleteDocType.Enabled = true;
    }
    this.UpdateGrid();
    this.SetButtons();
  }

  private void CancelChanges()
  {
    if (this._parentMode == 2)
    {
      this._report.LoadData(this.ReportID);
      this._changed = false;
      this.UpdateFormControls();
    }
    else if (this._parentMode == 1)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.GetObject(this.ReportID, false)?.Delete(0L);
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }
    else
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }
  }

  public void SaveChanges(IUserSession session)
  {
    IDBObject report = session.GetObject(this.ReportID);
    this.SaveChanges(session, report);
  }

  /// <summary>Сохранение изменений</summary>
  public void SaveChanges(IUserSession session, IDBObject report)
  {
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545")), (object) new object[1]
    {
      (object) this._report.ReportName
    }));
    attributeValuesList.Add(new AttributeValues(ReportGuids.AttributeReportCaptionId, (object) new object[1]
    {
      (object) this._report.ReportCaption
    }));
    attributeValuesList.Add(new AttributeValues(ReportGuids.AttrTemplateId, (object) new object[1]
    {
      (object) this._report.TemplateID
    }));
    string generatedDocTypeGuid = string.IsNullOrWhiteSpace(this.tbDocType.Text) ? (string) null : this._report.GeneratedDocTypeGuid;
    attributeValuesList.Add(new AttributeValues(ReportGuids.AttributeGeneratedDocTypeId, (object) new object[1]
    {
      (object) generatedDocTypeGuid
    }));
    List<string> stringList = new List<string>();
    foreach (ReportColumn column in this._report.Columns)
      stringList.Add(column.ToValue());
    attributeValuesList.Add(new AttributeValues(ReportGuids.AttributeColumnsId, (object) stringList.ToArray()));
    TableReportPropAttProxy reportPropAttProxy = new TableReportPropAttProxy(this._report.ResultItem, this._report.CountItems, this._report.DatePrint, this._report.PageNumber, this._report.RowNumbers, this._report.RowNumbersColumnWidth, this._report.RowNumbersColumnCaption);
    if (reportPropAttProxy.IsValid)
      attributeValuesList.Add(new AttributeValues(ReportGuids.AttributeParametersId, (object) new object[1]
      {
        (object) reportPropAttProxy.Value
      }));
    session.SetObjectAttributesValues(report.ObjectID, true, attributeValuesList.ToArray());
    this._changed = false;
    INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
    if (this._parentMode == 0)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
      service?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", report.ObjectID));
    }
    else if (this._parentMode == 1)
    {
      try
      {
        report.CommitCreation(false);
        this.ReportID = report.ObjectID;
        this.DialogResult = DialogResult.OK;
        this.Close();
        service?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", report.ObjectID));
      }
      catch
      {
        throw;
      }
    }
    else
    {
      service?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", report.ObjectID));
      this.SetButtons();
    }
  }

  /// <summary>Добавить колонку</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAddValue_Click(object sender, EventArgs e)
  {
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true);
    if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag = false;
      foreach (int num1 in attributesSelectDlg.SelectedAttributesID)
      {
        if (!this._report.IsAttributePresent(num1))
        {
          ReportColumn reportColumn = new ReportColumn();
          reportColumn.AttributeID = num1;
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(num1);
          reportColumn.AttributeName = attributeType.Name;
          reportColumn.AttributeType = attributeType.AttributeType == FieldTypes.ftSystem ? ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) num1) : attributeType.AttributeType;
          reportColumn.Index = this._report.Columns.Count;
          reportColumn.Caption = attributeType.Name;
          int num2 = 0;
          for (int index = 0; index < this._report.Columns.Count; ++index)
            num2 += this._report.Columns[index].Width;
          int num3 = 100 - num2;
          reportColumn.Width = num3 > 0 ? num3 : 1;
          reportColumn.Alignment = reportColumn.AttributeType == FieldTypes.ftAutoInc || reportColumn.AttributeType == FieldTypes.ftDouble || reportColumn.AttributeType == FieldTypes.ftInteger || reportColumn.AttributeType == FieldTypes.ftMeasured ? Intermech.Interfaces.Document.HorzAlignment.Right : Intermech.Interfaces.Document.HorzAlignment.Left;
          reportColumn.SortOrder = SortOrders.NONE;
          reportColumn.FormatString = reportColumn.AttributeType == FieldTypes.ftInteger || reportColumn.AttributeType == FieldTypes.ftAutoInc ? "D" : string.Empty;
          reportColumn.Result = MathTotal.None;
          this._report.Columns.Add(reportColumn);
          flag = true;
        }
      }
      if (!flag)
        return;
      this.UpdateGrid();
      this._changed = true;
      this.SetButtons();
    }
  }

  /// <summary>Удалить колонку</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnDeleteValue_Click(object sender, EventArgs e)
  {
    GridColumn focusedColumn = this.gridView1.FocusedColumn;
    IMMessageBoxButton messageBoxButton1 = new IMMessageBoxButton(LocalizationHolder.rm.GetString("Document.Client_17"), DialogResult.Yes);
    IMMessageBoxButton messageBoxButton2 = new IMMessageBoxButton(LocalizationHolder.rm.GetString("Document.Client_18"), DialogResult.No);
    if (focusedColumn == null)
      return;
    if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Document.Client_19"), string.Format(LocalizationHolder.rm.GetString("Document.Client_20"), (object) focusedColumn.Caption), new IMMessageBoxButton[2]
    {
      messageBoxButton1,
      messageBoxButton2
    }, IMMessageBoxImage.Question) != DialogResult.Yes || !this._report.DeleteColumn(focusedColumn.FieldName))
      return;
    this.UpdateGrid();
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Выбор шаблона</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOpenTemplate_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Document.Client_21"), LocalizationHolder.rm.GetString("Document.Client_22"), ObjectTypesHelper.GetObjTypeID("cad00287-306c-11d8-b4e9-00304f19f545"), SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    long objectID = numArray[0];
    if (objectID == this._report.TemplateID)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
      this._report.TemplateID = dbObject.ObjectID;
      this._report.TemplateName = dbObject.Caption;
      this.beTemplate.Text = this._report.TemplateName;
    }
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Изменилось название отчета</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbReportName_TextChanged(object sender, EventArgs e)
  {
    if (!(this._report.ReportName != this.tbReportName.Text))
      return;
    this._report.ReportName = this.tbReportName.Text;
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Изменился заголовок отчета</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbReportCaption_TextChanged(object sender, EventArgs e)
  {
    if (!(this._report.ReportCaption != this.tbReportCaption.Text))
      return;
    this._report.ReportCaption = this.tbReportCaption.Text;
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Изменился флаг печати результирующей строки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbResult_CheckedChanged(object sender, EventArgs e)
  {
    if (this._report.ResultItem == this.cbResult.Checked)
      return;
    this._report.ResultItem = this.cbResult.Checked;
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Изменился флаг печати количества позиций</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbCountItems_CheckedChanged(object sender, EventArgs e)
  {
    if (this._report.CountItems == this.cbCountItems.Checked)
      return;
    this._report.CountItems = this.cbCountItems.Checked;
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Изменился режим печати страниц</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbPageNumberFormat_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._report.PageNumber == (PageNumberPosition) this.cbPageNumberFormat.SelectedIndex)
      return;
    this._report.PageNumber = (PageNumberPosition) this.cbPageNumberFormat.SelectedIndex;
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Изменился режим печати даты отчета</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbDateFormat_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._report.DatePrint == (DatePrintFormats) this.cbDateFormat.SelectedIndex)
      return;
    this._report.DatePrint = (DatePrintFormats) this.cbDateFormat.SelectedIndex;
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Изменилась сфокуссированная колонка</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void gridView1_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
  {
    if (e.FocusedColumn == null)
      return;
    if (e.FocusedColumn.FieldName == TableReportEditor._mainFieldName)
    {
      this.btnDeleteValue.Enabled = false;
      this.lSelectedColumn.Text = string.Empty;
      this.biPrev.Enabled = false;
      this.biNext.Enabled = false;
    }
    else
    {
      this.btnDeleteValue.Enabled = true;
      this.lSelectedColumn.Text = string.Format(LocalizationHolder.rm.GetString("Document.Client_23"), (object) e.FocusedColumn.Caption);
      this.biPrev.Enabled = e.FocusedColumn.VisibleIndex > 1;
      this.biNext.Enabled = e.FocusedColumn.VisibleIndex < this._report.Columns.Count;
    }
  }

  /// <summary>Переместим колонку назад</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void biPrev_Click(object sender, EventArgs e)
  {
    int visibleIndex = this.gridView1.FocusedColumn.VisibleIndex;
    ReportColumn column1 = this._report.GetColumn(visibleIndex - 1);
    ReportColumn column2 = this._report.GetColumn(visibleIndex - 2);
    --column1.Index;
    ++column2.Index;
    --this.gridView1.FocusedColumn.VisibleIndex;
    this.gridView1_FocusedColumnChanged((object) this, new FocusedColumnChangedEventArgs((GridColumn) null, this.gridView1.FocusedColumn));
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Переместим колонку вперед</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void biNext_Click(object sender, EventArgs e)
  {
    int visibleIndex = this.gridView1.FocusedColumn.VisibleIndex;
    ReportColumn column1 = this._report.GetColumn(visibleIndex - 1);
    ReportColumn column2 = this._report.GetColumn(visibleIndex);
    ++column1.Index;
    --column2.Index;
    --this.gridView1.GetVisibleColumn(visibleIndex + 1).VisibleIndex;
    this.gridView1_FocusedColumnChanged((object) this, new FocusedColumnChangedEventArgs((GridColumn) null, this.gridView1.FocusedColumn));
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Прорисовка сфокуссированной ячейки грида</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    if (!(e.Column.FieldName != TableReportEditor._mainFieldName))
      return;
    e.Column.HeaderStyleName = e.Column == this.gridView1.FocusedColumn ? "Style2" : "HeaderPanel";
    if (focusedColumn == null)
      return;
    switch (e.RowHandle)
    {
      case 0:
        e.RepositoryItem = (RepositoryItem) this.repositoryItemTextEdit;
        break;
      case 1:
        e.RepositoryItem = (RepositoryItem) this.repositoryItemComboBox4;
        break;
      case 2:
        e.RepositoryItem = (RepositoryItem) this.repositoryItemSpinEdit;
        break;
      case 3:
        e.RepositoryItem = (RepositoryItem) this.repositoryItemComboBox5;
        break;
      case 4:
        e.RepositoryItem = (RepositoryItem) this.repositoryItemComboBox2;
        break;
      case 5:
        e.RepositoryItem = (RepositoryItem) this.repositoryItemButtonEdit1;
        break;
      case 6:
        if (!TableReportEditor._nonMathColumn.Contains(focusedColumn.AttributeID))
        {
          if (focusedColumn.AttributeType == FieldTypes.ftDateTime)
          {
            e.RepositoryItem = (RepositoryItem) this.repositoryItemComboBox3;
            break;
          }
          if (focusedColumn.AttributeType == FieldTypes.ftAutoInc || focusedColumn.AttributeType == FieldTypes.ftDouble || focusedColumn.AttributeType == FieldTypes.ftInteger || focusedColumn.AttributeType == FieldTypes.ftMeasured)
          {
            e.RepositoryItem = (RepositoryItem) this.repositoryItemComboBox1;
            break;
          }
        }
        e.RepositoryItem.ReadOnly = true;
        break;
    }
  }

  /// <summary>Изменилось значение заголовка</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void repositoryItemTextEdit_EditValueChanged(object sender, EventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    if (!(focusedColumn.Caption != Convert.ToString(this.gridView1.ActiveEditor.EditValue)))
      return;
    focusedColumn.Caption = Convert.ToString(this.gridView1.ActiveEditor.EditValue);
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Изменилось значение ширины</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void repositoryItemSpinEdit_EditValueChanging(object sender, ChangingEventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    if (e.NewValue == null || !(Convert.ToString(e.NewValue) != string.Empty) || focusedColumn.Width == Convert.ToInt32(e.NewValue))
      return;
    focusedColumn.Width = Convert.ToInt32(e.NewValue);
    this._changed = true;
    this.SetButtons();
  }

  private void repositoryItemSpinEdit_Enter(object sender, EventArgs e)
  {
  }

  /// <summary>Изменилось значение строки форматирования</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void repositoryItemTextEdit1_EditValueChanged(object sender, EventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    if (focusedColumn.FormatString != Convert.ToString(this.gridView1.ActiveEditor.EditValue))
    {
      this._changed = true;
      this.SetButtons();
    }
    focusedColumn.FormatString = Convert.ToString(this.gridView1.ActiveEditor.EditValue);
    this.gridView1.SetRowCellValue(5, this.gridView1.FocusedColumn, this.gridView1.ActiveEditor.EditValue);
    if (this.gridView1.GetDataRow(5).HasErrors)
      this.gridView1.GetDataRow(5).ClearErrors();
    if (focusedColumn.AttributeType == FieldTypes.ftAutoInc || focusedColumn.AttributeType == FieldTypes.ftInteger)
    {
      this.gridView1.SetRowCellValue(5, this.gridView1.FocusedColumn, (object) "D");
      focusedColumn.FormatString = "D";
    }
    else
    {
      if (focusedColumn.AttributeType == FieldTypes.ftDateTime || focusedColumn.AttributeType == FieldTypes.ftBoolean || focusedColumn.AttributeType == FieldTypes.ftDouble || focusedColumn.AttributeType == FieldTypes.ftMeasured)
        return;
      this.gridView1.SetRowCellValue(5, this.gridView1.FocusedColumn, (object) string.Empty);
      focusedColumn.FormatString = string.Empty;
    }
  }

  private ReportColumn FocusedColumn
  {
    get
    {
      int result;
      return this.gridView1.FocusedColumn != null && int.TryParse(this.gridView1.FocusedColumn.FieldName, out result) ? this._report.GetColumnByID(result) : (ReportColumn) null;
    }
  }

  /// <summary>Изменилось значение  выравнивания в колонке</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void repositoryItemComboBox5_SelectedIndexChanged(object sender, EventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    if (focusedColumn.Alignment == (Intermech.Interfaces.Document.HorzAlignment) (sender as ComboBoxEdit).SelectedIndex)
      return;
    focusedColumn.Alignment = (Intermech.Interfaces.Document.HorzAlignment) (sender as ComboBoxEdit).SelectedIndex;
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Изменилось значение итога</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    if (focusedColumn.Result == (MathTotal) (sender as ComboBoxEdit).SelectedIndex)
      return;
    focusedColumn.Result = (MathTotal) (sender as ComboBoxEdit).SelectedIndex;
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Изменилось значение порядка сортировки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void repositoryItemComboBox2_SelectedIndexChanged(object sender, EventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    if (focusedColumn.SortOrder == (SortOrders) (sender as ComboBoxEdit).SelectedIndex)
      return;
    focusedColumn.SortOrder = (SortOrders) (sender as ComboBoxEdit).SelectedIndex;
    this._changed = true;
    this.SetButtons();
  }

  private void repositoryItemComboBox4_SelectedIndexChanged(object sender, EventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    AttributeSourceTypes attributeSourceTypes = AttributeSourceTypes.Object;
    switch ((sender as ComboBoxEdit).SelectedIndex)
    {
      case 0:
        attributeSourceTypes = AttributeSourceTypes.Object;
        break;
      case 1:
        attributeSourceTypes = AttributeSourceTypes.Relation;
        break;
    }
    if (focusedColumn.AttributeSource == attributeSourceTypes)
      return;
    focusedColumn.AttributeSource = attributeSourceTypes;
    this._changed = true;
    this.SetButtons();
  }

  private void repositoryItemComboBox3_SelectedIndexChanged(object sender, EventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    bool flag = false;
    switch ((sender as ComboBoxEdit).SelectedIndex)
    {
      case 0:
        if (focusedColumn.Result != MathTotal.None)
        {
          focusedColumn.Result = MathTotal.None;
          flag = true;
          break;
        }
        break;
      case 1:
        if (focusedColumn.Result != MathTotal.Min)
        {
          focusedColumn.Result = MathTotal.Min;
          flag = true;
          break;
        }
        break;
      case 2:
        if (focusedColumn.Result != MathTotal.Max)
        {
          focusedColumn.Result = MathTotal.Max;
          flag = true;
          break;
        }
        break;
    }
    if (!flag)
      return;
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Открываем редактор строки форматирования</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    if (focusedColumn.AttributeType != FieldTypes.ftMeasured && focusedColumn.AttributeType != FieldTypes.ftDouble && focusedColumn.AttributeType != FieldTypes.ftBoolean && focusedColumn.AttributeType != FieldTypes.ftDateTime)
      return;
    SelectFormat selectFormat = new SelectFormat(focusedColumn.AttributeType, focusedColumn.FormatString);
    if (selectFormat.ShowDialog() != DialogResult.OK)
      return;
    this.gridView1.ActiveEditor.EditValue = (object) selectFormat.format;
    this.gridView1.RefreshEditor(false);
  }

  /// <summary>Покидаем строку форматирования</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void repositoryItemButtonEdit1_Leave(object sender, EventArgs e)
  {
    ReportColumn focusedColumn = this.FocusedColumn;
    if (this.gridView1.GetDataRow(5).HasErrors)
      this.gridView1.GetDataRow(5).ClearErrors();
    if (focusedColumn == null)
      return;
    if (focusedColumn.AttributeType == FieldTypes.ftAutoInc || focusedColumn.AttributeType == FieldTypes.ftInteger)
    {
      this.gridView1.SetRowCellValue(5, this.gridView1.FocusedColumn, (object) "D");
      focusedColumn.FormatString = "D";
    }
    else
    {
      if (focusedColumn.AttributeType == FieldTypes.ftDateTime || focusedColumn.AttributeType == FieldTypes.ftBoolean || focusedColumn.AttributeType == FieldTypes.ftDouble || focusedColumn.AttributeType == FieldTypes.ftMeasured)
        return;
      this.gridView1.SetRowCellValue(5, this.gridView1.FocusedColumn, (object) string.Empty);
      focusedColumn.FormatString = string.Empty;
    }
  }

  /// <summary>Нажали кнопку OK</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnApply_Click(object sender, EventArgs e)
  {
    TableElement node = DocumentEditorPlugin.LoadDocumentFromDBObject(this._report.TemplateID, createIfNotFound: true).FindNode("table") as TableElement;
    bool flag = true;
    if (node != null)
    {
      int num1 = 0;
      foreach (ReportColumn column in this._report.Columns)
        num1 += column.Width;
      if (num1 > 100)
      {
        flag = false;
        int num2 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Document.Client_26"), string.Format(LocalizationHolder.rm.GetString("Document.Client_27"), (object) num1), MessageBoxButtons.OK, IMMessageBoxImage.Error);
      }
    }
    for (int index = 0; index < this._report.Columns.Count; ++index)
    {
      if (!SelectFormat.IsValidate(this._report.Columns[index].AttributeType, this._report.Columns[index].FormatString))
      {
        GridColumn column = this.gridView1.Columns[this._report.Columns[index].AttributeID.ToString()];
        this.gridView1.FocusedColumn = column;
        this.gridView1.GetDataRow(5).SetColumnError(column.VisibleIndex, LocalizationHolder.rm.GetString("Document.Client_113"));
        return;
      }
    }
    if (!flag)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.SaveChanges(sessionKeeper.Session);
  }

  /// <summary>Нажали кнопку "Отмена"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e) => this.CancelChanges();

  private void cbRowNumbers_CheckedChanged(object sender, EventArgs e)
  {
    if (this._report.RowNumbers != this.cbRowNumbers.Checked)
    {
      this._report.RowNumbers = this.cbRowNumbers.Checked;
      this._changed = true;
      this.SetButtons();
    }
    this.nudWidthNumColunm.Enabled = this.cbRowNumbers.Checked;
    this.tbCaptionNumColunm.Enabled = this.cbRowNumbers.Checked;
  }

  private void nudWidthNumColunm_ValueChanged(object sender, EventArgs e)
  {
    if (this._report.RowNumbersColumnWidth == (int) this.nudWidthNumColunm.Value)
      return;
    this._report.RowNumbersColumnWidth = (int) this.nudWidthNumColunm.Value;
    this._changed = true;
    this.SetButtons();
  }

  private void tbCaptionNumColunm_TextChanged(object sender, EventArgs e)
  {
    if (!(this._report.RowNumbersColumnCaption != this.tbCaptionNumColunm.Text))
      return;
    this._report.RowNumbersColumnCaption = this.tbCaptionNumColunm.Text;
    this._changed = true;
    this.SetButtons();
  }

  private void btnChooseType_Click(object sender, EventArgs e)
  {
    int documentObjectTypeId;
    string documentObjectTypeName;
    if (!DocumentEditorPlugin.SelectDocumentDBObjectType(out documentObjectTypeId, out documentObjectTypeName))
      return;
    this.tbDocType.Text = documentObjectTypeName;
    this.tbDocType.Tag = (object) MetaDataHelper.GetObjectTypeGuid(documentObjectTypeId).ToString();
    this.btnDeleteDocType.Enabled = true;
    if (!(this._report.GeneratedDocTypeGuid != Convert.ToString(this.tbDocType.Tag)))
      return;
    this._report.GeneratedDocTypeGuid = Convert.ToString(this.tbDocType.Tag);
    this._changed = true;
    this.SetButtons();
  }

  /// <summary>Удаление типа по умолчанию</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnDeleteDocType_Click(object sender, EventArgs e)
  {
    this._report.GeneratedDocTypeGuid = string.Empty;
    this.tbDocType.Tag = (object) string.Empty;
    this.tbDocType.Text = string.Empty;
    this.btnDeleteDocType.Enabled = false;
    this._changed = true;
    this.SetButtons();
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TableReportEditor));
    this.panel2 = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.panel1 = new Panel();
    this.tabControl1 = new TabControl();
    this.tabPage1 = new TabPage();
    this.groupBox1 = new GroupBox();
    this.gridControl1 = new GridControl();
    this.gridView1 = new GridView();
    this.repositoryItemTextEdit = new RepositoryItemTextEdit();
    this.repositoryItemSpinEdit = new RepositoryItemSpinEdit();
    this.repositoryItemComboBox1 = new RepositoryItemComboBox();
    this.repositoryItemComboBox2 = new RepositoryItemComboBox();
    this.repositoryItemComboBox3 = new RepositoryItemComboBox();
    this.repositoryItemComboBox4 = new RepositoryItemComboBox();
    this.repositoryItemComboBox5 = new RepositoryItemComboBox();
    this.repositoryItemButtonEdit1 = new RepositoryItemButtonEdit();
    this.panel5 = new Panel();
    this.toolBarValues = new Intermech.Bars.ToolBar();
    this.btnAddValue = new ButtonItem();
    this.btnDeleteValue = new ButtonItem();
    this.biPrev = new ButtonItem();
    this.biNext = new ButtonItem();
    this.lSelectedColumn = new LabelItem();
    this.panel3 = new Panel();
    this.btnDeleteDocType = new Button();
    this.btnChooseType = new Button();
    this.tbDocType = new TextBox();
    this.lblType = new Label();
    this.button1 = new Button();
    this.beTemplate = new TextBox();
    this.label2 = new Label();
    this.tbReportCaption = new TextBox();
    this.label3 = new Label();
    this.tbReportName = new TextBox();
    this.label1 = new Label();
    this.tabPage2 = new TabPage();
    this.groupBox3 = new GroupBox();
    this.groupBox2 = new GroupBox();
    this.tbCaptionNumColunm = new TextBox();
    this.label7 = new Label();
    this.cbRowNumbers = new CheckBox();
    this.label6 = new Label();
    this.nudWidthNumColunm = new NumericUpDown();
    this.label4 = new Label();
    this.cbDateFormat = new System.Windows.Forms.ComboBox();
    this.cbPageNumberFormat = new System.Windows.Forms.ComboBox();
    this.cbCountItems = new CheckBox();
    this.cbResult = new CheckBox();
    this.label5 = new Label();
    this.persistentRepository1 = new PersistentRepository();
    this.toolTipOne = new ToolTip(this.components);
    this.bindingSource1 = new BindingSource(this.components);
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.tabControl1.SuspendLayout();
    this.tabPage1.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.gridControl1.BeginInit();
    this.gridView1.BeginInit();
    this.repositoryItemTextEdit.BeginInit();
    this.repositoryItemSpinEdit.BeginInit();
    this.repositoryItemComboBox1.BeginInit();
    this.repositoryItemComboBox2.BeginInit();
    this.repositoryItemComboBox3.BeginInit();
    this.repositoryItemComboBox4.BeginInit();
    this.repositoryItemComboBox5.BeginInit();
    this.repositoryItemButtonEdit1.BeginInit();
    this.panel5.SuspendLayout();
    this.panel3.SuspendLayout();
    this.tabPage2.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.nudWidthNumColunm.BeginInit();
    ((ISupportInitialize) this.bindingSource1).BeginInit();
    this.SuspendLayout();
    this.panel2.Controls.Add((Control) this.btnCancel);
    this.panel2.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.panel1.Controls.Add((Control) this.tabControl1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.tabControl1.Controls.Add((Control) this.tabPage1);
    this.tabControl1.Controls.Add((Control) this.tabPage2);
    componentResourceManager.ApplyResources((object) this.tabControl1, "tabControl1");
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    this.tabPage1.BackColor = Color.Transparent;
    this.tabPage1.Controls.Add((Control) this.groupBox1);
    this.tabPage1.Controls.Add((Control) this.panel5);
    this.tabPage1.Controls.Add((Control) this.panel3);
    componentResourceManager.ApplyResources((object) this.tabPage1, "tabPage1");
    this.tabPage1.Name = "tabPage1";
    this.tabPage1.UseVisualStyleBackColor = true;
    this.groupBox1.Controls.Add((Control) this.gridControl1);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.gridControl1, "gridControl1");
    this.gridControl1.EmbeddedNavigator.Name = "";
    this.gridControl1.MainView = (BaseView) this.gridView1;
    this.gridControl1.Name = "gridControl1";
    this.gridControl1.RepositoryItems.AddRange(new RepositoryItem[8]
    {
      (RepositoryItem) this.repositoryItemTextEdit,
      (RepositoryItem) this.repositoryItemSpinEdit,
      (RepositoryItem) this.repositoryItemComboBox1,
      (RepositoryItem) this.repositoryItemComboBox2,
      (RepositoryItem) this.repositoryItemComboBox3,
      (RepositoryItem) this.repositoryItemComboBox4,
      (RepositoryItem) this.repositoryItemComboBox5,
      (RepositoryItem) this.repositoryItemButtonEdit1
    });
    this.gridControl1.Styles.AddReplace("Style1", (object) new ViewStyleEx("Style1", "", SystemColors.Control, SystemColors.WindowText, Color.Empty, LinearGradientMode.Horizontal));
    this.gridControl1.Styles.AddReplace("Style2", (object) new ViewStyleEx("Style2", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204), SystemColors.Control, SystemColors.WindowText, Color.Empty, LinearGradientMode.Horizontal));
    this.gridControl1.Styles.AddReplace("FocusedCell", (object) new ViewStyleEx("FocusedCell", "Grid", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, SystemColors.Window, SystemColors.WindowText, SystemColors.Window, LinearGradientMode.Horizontal));
    this.gridView1.GridControl = this.gridControl1;
    this.gridView1.GroupFooterShowMode = GroupFooterShowMode.Hidden;
    this.gridView1.Name = "gridView1";
    this.gridView1.OptionsCustomization.AllowFilter = false;
    this.gridView1.OptionsCustomization.AllowGroup = false;
    this.gridView1.OptionsCustomization.AllowSort = false;
    this.gridView1.OptionsDetail.EnableDetailToolTip = true;
    this.gridView1.OptionsMenu.EnableColumnMenu = false;
    this.gridView1.OptionsMenu.EnableFooterMenu = false;
    this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
    this.gridView1.OptionsView.ColumnAutoWidth = false;
    this.gridView1.OptionsView.ShowFilterPanel = false;
    this.gridView1.OptionsView.ShowGroupPanel = false;
    this.gridView1.OptionsView.ShowIndicator = false;
    this.gridView1.CustomRowCellEdit += new CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
    this.gridView1.FocusedColumnChanged += new FocusedColumnChangedEventHandler(this.gridView1_FocusedColumnChanged);
    this.repositoryItemTextEdit.AutoHeight = false;
    this.repositoryItemTextEdit.Name = "repositoryItemTextEdit";
    this.repositoryItemTextEdit.EditValueChanged += new EventHandler(this.repositoryItemTextEdit_EditValueChanged);
    this.repositoryItemSpinEdit.AutoHeight = false;
    this.repositoryItemSpinEdit.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.repositoryItemSpinEdit.MaxValue = new Decimal(new int[4]
    {
      100,
      0,
      0,
      0
    });
    this.repositoryItemSpinEdit.MinValue = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.repositoryItemSpinEdit.Name = "repositoryItemSpinEdit";
    this.repositoryItemSpinEdit.UseCtrlIncrement = true;
    this.repositoryItemSpinEdit.EditValueChanging += new ChangingEventHandler(this.repositoryItemSpinEdit_EditValueChanging);
    this.repositoryItemSpinEdit.Enter += new EventHandler(this.repositoryItemSpinEdit_Enter);
    this.repositoryItemComboBox1.AutoHeight = false;
    this.repositoryItemComboBox1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
    this.repositoryItemComboBox1.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.repositoryItemComboBox1.SelectedIndexChanged += new EventHandler(this.repositoryItemComboBox1_SelectedIndexChanged);
    this.repositoryItemComboBox2.AutoHeight = false;
    this.repositoryItemComboBox2.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
    this.repositoryItemComboBox2.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.repositoryItemComboBox2.SelectedIndexChanged += new EventHandler(this.repositoryItemComboBox2_SelectedIndexChanged);
    this.repositoryItemComboBox3.AutoHeight = false;
    this.repositoryItemComboBox3.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.repositoryItemComboBox3.Name = "repositoryItemComboBox3";
    this.repositoryItemComboBox3.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.repositoryItemComboBox3.SelectedIndexChanged += new EventHandler(this.repositoryItemComboBox3_SelectedIndexChanged);
    this.repositoryItemComboBox4.AutoHeight = false;
    this.repositoryItemComboBox4.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.repositoryItemComboBox4.Name = "repositoryItemComboBox4";
    this.repositoryItemComboBox4.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.repositoryItemComboBox4.SelectedIndexChanged += new EventHandler(this.repositoryItemComboBox4_SelectedIndexChanged);
    this.repositoryItemComboBox5.AutoHeight = false;
    this.repositoryItemComboBox5.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.repositoryItemComboBox5.Name = "repositoryItemComboBox5";
    this.repositoryItemComboBox5.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.repositoryItemComboBox5.SelectedIndexChanged += new EventHandler(this.repositoryItemComboBox5_SelectedIndexChanged);
    this.repositoryItemButtonEdit1.AutoHeight = false;
    this.repositoryItemButtonEdit1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
    this.repositoryItemButtonEdit1.ButtonClick += new ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
    this.repositoryItemButtonEdit1.EditValueChanged += new EventHandler(this.repositoryItemTextEdit1_EditValueChanged);
    this.repositoryItemButtonEdit1.Leave += new EventHandler(this.repositoryItemButtonEdit1_Leave);
    this.panel5.Controls.Add((Control) this.toolBarValues);
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.Name = "panel5";
    this.toolBarValues.AllowVerticalDock = false;
    componentResourceManager.ApplyResources((object) this.toolBarValues, "toolBarValues");
    this.toolBarValues.DockLine = 3;
    this.toolBarValues.FullMenus = true;
    this.toolBarValues.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarValues.Hidden = false;
    this.toolBarValues.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.btnAddValue,
      (ToolbarItemBase) this.btnDeleteValue,
      (ToolbarItemBase) this.biPrev,
      (ToolbarItemBase) this.biNext,
      (ToolbarItemBase) this.lSelectedColumn
    });
    this.toolBarValues.MinimumFloatingSize = new Size(250, 30);
    this.toolBarValues.Name = "toolBarValues";
    this.toolBarValues.Overflow = ToolBarOverflow.Wrap;
    this.toolBarValues.Stretch = true;
    componentResourceManager.ApplyResources((object) this.btnAddValue, "btnAddValue");
    this.btnAddValue.Image = (Image) Intermech.Document.Client.Properties.Resources.add_col;
    this.btnAddValue.ImageIndex = 0;
    this.btnAddValue.ShowText = true;
    this.btnAddValue.Click += new EventHandler(this.btnAddValue_Click);
    componentResourceManager.ApplyResources((object) this.btnDeleteValue, "btnDeleteValue");
    this.btnDeleteValue.Image = (Image) Intermech.Document.Client.Properties.Resources.del_col;
    this.btnDeleteValue.ImageIndex = 1;
    this.btnDeleteValue.ShowText = true;
    this.btnDeleteValue.Click += new EventHandler(this.btnDeleteValue_Click);
    this.biPrev.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.biPrev, "biPrev");
    this.biPrev.Enabled = false;
    this.biPrev.Image = (Image) Intermech.Document.Client.Properties.Resources.Outline_Promote;
    this.biPrev.Click += new EventHandler(this.biPrev_Click);
    componentResourceManager.ApplyResources((object) this.biNext, "biNext");
    this.biNext.Enabled = false;
    this.biNext.Image = (Image) Intermech.Document.Client.Properties.Resources.Outline_Demote;
    this.biNext.Click += new EventHandler(this.biNext_Click);
    this.lSelectedColumn.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.lSelectedColumn, "lSelectedColumn");
    this.lSelectedColumn.Stretch = true;
    this.panel3.Controls.Add((Control) this.btnDeleteDocType);
    this.panel3.Controls.Add((Control) this.btnChooseType);
    this.panel3.Controls.Add((Control) this.tbDocType);
    this.panel3.Controls.Add((Control) this.lblType);
    this.panel3.Controls.Add((Control) this.button1);
    this.panel3.Controls.Add((Control) this.beTemplate);
    this.panel3.Controls.Add((Control) this.label2);
    this.panel3.Controls.Add((Control) this.tbReportCaption);
    this.panel3.Controls.Add((Control) this.label3);
    this.panel3.Controls.Add((Control) this.tbReportName);
    this.panel3.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.btnDeleteDocType, "btnDeleteDocType");
    this.btnDeleteDocType.Image = (Image) Intermech.Document.Client.Properties.Resources.del_t;
    this.btnDeleteDocType.Name = "btnDeleteDocType";
    this.toolTipOne.SetToolTip((Control) this.btnDeleteDocType, componentResourceManager.GetString("btnDeleteDocType.ToolTip"));
    this.btnDeleteDocType.UseVisualStyleBackColor = true;
    this.btnDeleteDocType.Click += new EventHandler(this.btnDeleteDocType_Click);
    componentResourceManager.ApplyResources((object) this.btnChooseType, "btnChooseType");
    this.btnChooseType.Name = "btnChooseType";
    this.toolTipOne.SetToolTip((Control) this.btnChooseType, componentResourceManager.GetString("btnChooseType.ToolTip"));
    this.btnChooseType.UseVisualStyleBackColor = true;
    this.btnChooseType.Click += new EventHandler(this.btnChooseType_Click);
    componentResourceManager.ApplyResources((object) this.tbDocType, "tbDocType");
    this.tbDocType.Name = "tbDocType";
    componentResourceManager.ApplyResources((object) this.lblType, "lblType");
    this.lblType.Name = "lblType";
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.toolTipOne.SetToolTip((Control) this.button1, componentResourceManager.GetString("button1.ToolTip"));
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.btnOpenTemplate_Click);
    componentResourceManager.ApplyResources((object) this.beTemplate, "beTemplate");
    this.beTemplate.Name = "beTemplate";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.tbReportCaption, "tbReportCaption");
    this.tbReportCaption.Name = "tbReportCaption";
    this.tbReportCaption.TextChanged += new EventHandler(this.tbReportCaption_TextChanged);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.tbReportName, "tbReportName");
    this.tbReportName.Name = "tbReportName";
    this.tbReportName.TextChanged += new EventHandler(this.tbReportName_TextChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.tabPage2.BackColor = Color.Transparent;
    this.tabPage2.Controls.Add((Control) this.groupBox3);
    componentResourceManager.ApplyResources((object) this.tabPage2, "tabPage2");
    this.tabPage2.Name = "tabPage2";
    this.tabPage2.UseVisualStyleBackColor = true;
    this.groupBox3.Controls.Add((Control) this.groupBox2);
    this.groupBox3.Controls.Add((Control) this.label4);
    this.groupBox3.Controls.Add((Control) this.cbDateFormat);
    this.groupBox3.Controls.Add((Control) this.cbPageNumberFormat);
    this.groupBox3.Controls.Add((Control) this.cbCountItems);
    this.groupBox3.Controls.Add((Control) this.cbResult);
    this.groupBox3.Controls.Add((Control) this.label5);
    componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.TabStop = false;
    this.groupBox2.Controls.Add((Control) this.tbCaptionNumColunm);
    this.groupBox2.Controls.Add((Control) this.label7);
    this.groupBox2.Controls.Add((Control) this.cbRowNumbers);
    this.groupBox2.Controls.Add((Control) this.label6);
    this.groupBox2.Controls.Add((Control) this.nudWidthNumColunm);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tbCaptionNumColunm, "tbCaptionNumColunm");
    this.tbCaptionNumColunm.Name = "tbCaptionNumColunm";
    this.tbCaptionNumColunm.TextChanged += new EventHandler(this.tbCaptionNumColunm_TextChanged);
    componentResourceManager.ApplyResources((object) this.label7, "label7");
    this.label7.Name = "label7";
    componentResourceManager.ApplyResources((object) this.cbRowNumbers, "cbRowNumbers");
    this.cbRowNumbers.Name = "cbRowNumbers";
    this.cbRowNumbers.UseVisualStyleBackColor = true;
    this.cbRowNumbers.CheckedChanged += new EventHandler(this.cbRowNumbers_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.nudWidthNumColunm, "nudWidthNumColunm");
    this.nudWidthNumColunm.Maximum = new Decimal(new int[4]
    {
      400,
      0,
      0,
      0
    });
    this.nudWidthNumColunm.Name = "nudWidthNumColunm";
    this.nudWidthNumColunm.Value = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this.nudWidthNumColunm.ValueChanged += new EventHandler(this.nudWidthNumColunm_ValueChanged);
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    this.cbDateFormat.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbDateFormat.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.cbDateFormat, "cbDateFormat");
    this.cbDateFormat.Name = "cbDateFormat";
    this.cbDateFormat.SelectedIndexChanged += new EventHandler(this.cbDateFormat_SelectedIndexChanged);
    this.cbPageNumberFormat.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbPageNumberFormat.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.cbPageNumberFormat, "cbPageNumberFormat");
    this.cbPageNumberFormat.Name = "cbPageNumberFormat";
    this.cbPageNumberFormat.SelectedIndexChanged += new EventHandler(this.cbPageNumberFormat_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.cbCountItems, "cbCountItems");
    this.cbCountItems.Name = "cbCountItems";
    this.cbCountItems.UseVisualStyleBackColor = true;
    this.cbCountItems.CheckedChanged += new EventHandler(this.cbCountItems_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbResult, "cbResult");
    this.cbResult.Checked = true;
    this.cbResult.CheckState = CheckState.Checked;
    this.cbResult.Name = "cbResult";
    this.cbResult.UseVisualStyleBackColor = true;
    this.cbResult.CheckedChanged += new EventHandler(this.cbResult_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    this.AcceptButton = (IButtonControl) this.btnApply;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (TableReportEditor);
    this.Tag = (object) " ";
    this.panel2.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.tabControl1.ResumeLayout(false);
    this.tabPage1.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.gridControl1.EndInit();
    this.gridView1.EndInit();
    this.repositoryItemTextEdit.EndInit();
    this.repositoryItemSpinEdit.EndInit();
    this.repositoryItemComboBox1.EndInit();
    this.repositoryItemComboBox2.EndInit();
    this.repositoryItemComboBox3.EndInit();
    this.repositoryItemComboBox4.EndInit();
    this.repositoryItemComboBox5.EndInit();
    this.repositoryItemButtonEdit1.EndInit();
    this.panel5.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.panel3.PerformLayout();
    this.tabPage2.ResumeLayout(false);
    this.groupBox3.ResumeLayout(false);
    this.groupBox3.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.nudWidthNumColunm.EndInit();
    ((ISupportInitialize) this.bindingSource1).EndInit();
    this.ResumeLayout(false);
  }
}
