// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.AdvancedImportSettingsFrm
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Tools.Client.Subsystems.Import_from_Excel.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

public class AdvancedImportSettingsFrm : Form
{
  private ConfigurationManager _configurationManager;
  private string _currentConfigurationName = string.Empty;
  private ConfigurationType _currentConfigurationType;
  private DataTable _dtData = new DataTable();
  private IContainer components;
  private ContextMenuStrip cmLvSettingItem;
  private ToolStrip tstrpLvSettingItem;
  private ToolStripButton tsbtnBindAttribute;
  private ToolStripButton tsbtnBindObjectType;
  private ToolStripButton tsbtnBindRelationType;
  private ToolStripButton tsbtnClearBindedItem;
  private ToolStripSeparator toolStripSeparator6;
  private ToolStripButton tsbtnLeftEnd;
  private ToolStripButton tsbtnLeft;
  private ToolStripButton tsbtnDown;
  private ToolStripButton tsbtnBotton;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripButton tsbtnOpenFile;
  private Panel panel2;
  private Button btnCancel;
  private Button btnImport;
  private ToolStripSeparator toolStripSeparator4;
  private ToolStripButton tsbtnSaveConfiguration;
  private OpenFileDialog ofDialog;
  private Panel pnlLeft;
  private Panel pnlRight;
  private PropertyGrid attributePropertyGrid;
  private Splitter splitter1;
  private ToolStripButton tsbtnLoadConfiguration;
  private ToolStripButton tsbtnDeleteConfiguration;
  private Panel pnlCommonProperties;
  private CheckBox chbSkipFirstRow;
  private Splitter splitter2;
  private CheckBox chbSkipRelationCreateErrs;
  private CheckBox chbSkipObjectCreateErrs;
  private ToolStripMenuItem tsItemAdd;
  private ToolStripMenuItem tsItemAddAttribute;
  private ToolStripMenuItem tsItemAddObjectType;
  private ToolStripMenuItem tsItemAddRelationType;
  private ToolStripMenuItem tsItemConfiguration;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripSeparator toolStripSeparator5;
  private ToolStripMenuItem tsItemLoadConfiguration;
  private ToolStripMenuItem tsItemSaveConfiguration;
  private ToolStripMenuItem tsItemDeleteConfiguration;
  private DataGridView dataGridView;
  private ToolStripButton tsbtnInsertLeft;
  private ToolStripButton tsbtnInsertRigth;
  private ToolStripSeparator toolStripSeparator7;
  private ToolStripMenuItem tsitemInsert;
  private ToolStripMenuItem tsitemColumnLeft;
  private ToolStripMenuItem tsitemColumnRight;
  private ToolStripMenuItem tsmiRemove;
  private ToolStripButton tsbtnDeleteColumn;
  private ToolStripMenuItem tsitemDeleteColumn;
  private Panel pnlSelectObj;
  private TextBox tbParentObj;
  private Button btnSelectObject;
  private Button btnClearSelected;
  private Button btnCard;
  private Label label1;
  private ToolStripSeparator toolStripSeparator8;
  private ToolStripMenuItem tsmiLeftEnd;
  private ToolStripMenuItem tsmiLeft;
  private ToolStripMenuItem tsmiRight;
  private ToolStripMenuItem tsmiRightEnd;
  private ToolStripSeparator toolStripSeparator9;
  private ToolStripMenuItem tsmiLoadConfigFromFile;
  private ToolStripMenuItem tsmiSaveConfigToFile;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripButton tsbLoadConfigFromFile;
  private ToolStripButton tsbSaveConfigToFile;
  private OpenFileDialog ofdConfiguration;
  private SaveFileDialog sfdConfiguration;
  private ToolStripSeparator toolStripSeparator10;
  private ToolStripLabel currentConfigName;
  private ToolStripButton tsbtnBindEntranceObjectType;
  private ToolStripMenuItem tsItemAddEntranceObjectType;

  public AdvancedImportSettingsFrm(bool isAdmin)
  {
    this.InitializeComponent();
    this._dtData.Rows.Add(this._dtData.NewRow());
    this._configurationManager = new ConfigurationManager(isAdmin);
    this.dataGridView.AutoGenerateColumns = false;
    this.UpdateCommandsState();
  }

  private ColumnConfiguration[] ColumnConfigurations
  {
    get
    {
      DataTable dtData = this._dtData;
      ColumnConfiguration[] columnConfigurationArray;
      if (dtData == null)
      {
        columnConfigurationArray = (ColumnConfiguration[]) null;
      }
      else
      {
        DataColumnCollection columns = dtData.Columns;
        columnConfigurationArray = columns != null ? columns.Cast<DataColumn>().Select<DataColumn, ColumnConfiguration>((Func<DataColumn, int, ColumnConfiguration>) ((x, i) =>
        {
          if (x.ExtendedProperties[(object) Consts.ColumnPropName] is ColumnConfiguration extendedProperty2)
            extendedProperty2.Index = i;
          return extendedProperty2;
        })).Where<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x != null)).ToArray<ColumnConfiguration>() : (ColumnConfiguration[]) null;
      }
      return columnConfigurationArray ?? new ColumnConfiguration[0];
    }
  }

  private Configuration CurrentConfiguration
  {
    get
    {
      Configuration currentConfiguration = new Configuration();
      currentConfiguration.Name = this._currentConfigurationName;
      currentConfiguration.Type = this._currentConfigurationType;
      currentConfiguration.CommonImportOptions = this.CommonImportOptions;
      currentConfiguration.ColumnConfigurations.AddRange((IEnumerable<ColumnConfiguration>) this.ColumnConfigurations);
      return currentConfiguration;
    }
  }

  public string FileName { get; private set; }

  private CommonImportOptions CommonImportOptions
  {
    get
    {
      CommonImportOptions commonImportOptions = CommonImportOptions.None;
      if (this.chbSkipFirstRow.Checked)
        commonImportOptions |= CommonImportOptions.SkipFirstRow;
      if (this.chbSkipObjectCreateErrs.Checked)
        commonImportOptions |= CommonImportOptions.IgnoreExistingObjectErrs;
      if (this.chbSkipRelationCreateErrs.Checked)
        commonImportOptions |= CommonImportOptions.IgnoreExistingRelationErrs;
      return commonImportOptions;
    }
  }

  private void BindAttributeType(DataGridViewColumn dataGridViewColumn)
  {
    if (dataGridViewColumn == null)
      return;
    DataColumn dataGridViewColumn1 = this.GetDataColumnFromDataGridViewColumn(dataGridViewColumn);
    if (dataGridViewColumn1 == null)
      return;
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.ForbiddenAttrsTypesFilter = ((IEnumerable<FieldTypes>) new FieldTypes[8]
      {
        FieldTypes.ftAutoInc,
        FieldTypes.ftBlob,
        FieldTypes.ftExternalLink,
        FieldTypes.ftObjectLink,
        FieldTypes.ftPassword,
        FieldTypes.ftShortBlob,
        FieldTypes.ftSystem,
        FieldTypes.ftUnknown
      }).ToList<FieldTypes>();
      if (!attributesSelectDlg.ShowDialog().Equals((object) DialogResult.OK) || attributesSelectDlg.SelectedAttributesID.Count <= 0)
        return;
      int attributeId = attributesSelectDlg.SelectedAttributesID[0];
      if (((IEnumerable<ColumnConfiguration>) this.ColumnConfigurations).Any<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x.ItemType == SettingItemType.AttributeType && x.TypeId == attributeId)) && MessageBox.Show("Данный атрибут уже назначен. Назначить повторно?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
        return;
      ColumnConfiguration columnConfiguration = new ColumnConfiguration()
      {
        ItemType = SettingItemType.AttributeType,
        TypeId = attributeId
      };
      this.BindSettingToDtColumn(dataGridViewColumn1, columnConfiguration);
      this.SetDataGridViewColumnStyle(dataGridViewColumn);
      this.UpdateCommandsState();
    }
  }

  private void BindObjectType(DataGridViewColumn dataGridViewColumn)
  {
    if (dataGridViewColumn == null)
      return;
    DataColumn dataGridViewColumn1 = this.GetDataColumnFromDataGridViewColumn(dataGridViewColumn);
    if (dataGridViewColumn1 == null)
      return;
    ColumnConfiguration columnConfiguration = new ColumnConfiguration()
    {
      ItemType = SettingItemType.ObjectType
    };
    this.BindSettingToDtColumn(dataGridViewColumn1, columnConfiguration);
    this.SetDataGridViewColumnStyle(dataGridViewColumn);
    this.UpdateCommandsState();
  }

  private void BindEntranceObjectType(DataGridViewColumn dataGridViewColumn)
  {
    if (dataGridViewColumn == null)
      return;
    DataColumn dataGridViewColumn1 = this.GetDataColumnFromDataGridViewColumn(dataGridViewColumn);
    if (dataGridViewColumn1 == null)
      return;
    ColumnConfiguration columnConfiguration = new ColumnConfiguration()
    {
      ItemType = SettingItemType.EntrancyObjectType
    };
    this.BindSettingToDtColumn(dataGridViewColumn1, columnConfiguration);
    this.SetDataGridViewColumnStyle(dataGridViewColumn);
    this.UpdateCommandsState();
  }

  private void BindRelationType(DataGridViewColumn dataGridViewColumn)
  {
    if (dataGridViewColumn == null)
      return;
    DataColumn dataGridViewColumn1 = this.GetDataColumnFromDataGridViewColumn(dataGridViewColumn);
    if (dataGridViewColumn1 == null)
      return;
    ColumnConfiguration columnConfiguration = new ColumnConfiguration()
    {
      ItemType = SettingItemType.RelationType
    };
    this.BindSettingToDtColumn(dataGridViewColumn1, columnConfiguration);
    this.SetDataGridViewColumnStyle(dataGridViewColumn);
    this.UpdateCommandsState();
  }

  private void ClearBindedItem(DataGridViewColumn dataGridViewColumn)
  {
    if (dataGridViewColumn == null)
      return;
    DataColumn dataGridViewColumn1 = this.GetDataColumnFromDataGridViewColumn(dataGridViewColumn);
    if (dataGridViewColumn1 == null)
      return;
    dataGridViewColumn1.ExtendedProperties.Clear();
    this.SetDataGridViewColumnStyle(dataGridViewColumn);
    this.UpdateCommandsState();
  }

  private void InsertColumn(int position)
  {
    DataColumn column = new DataColumn()
    {
      ColumnName = Guid.NewGuid().ToString(),
      Caption = string.Empty
    };
    this._dtData.Columns.Add(column);
    column.SetOrdinal(position);
    DataGridViewColumn dataGridViewColumn = new DataGridViewColumn()
    {
      SortMode = DataGridViewColumnSortMode.NotSortable,
      CellTemplate = (DataGridViewCell) new DataGridViewTextBoxCell()
    };
    this.dataGridView.Columns.Insert(position, dataGridViewColumn);
    this.MapColumns();
    this.SetFirstRowVisibleState();
    dataGridViewColumn.Selected = true;
  }

  private void InsertColumnLeft(DataGridViewColumn dataGridViewColumn)
  {
    DataColumn dataGridViewColumn1 = this.GetDataColumnFromDataGridViewColumn(dataGridViewColumn);
    if (dataGridViewColumn1 == null)
      return;
    this.InsertColumn(dataGridViewColumn1.Ordinal);
  }

  private void InsertColumnRight(DataGridViewColumn dataGridViewColumn)
  {
    DataColumn dataGridViewColumn1 = this.GetDataColumnFromDataGridViewColumn(dataGridViewColumn);
    if (dataGridViewColumn1 == null)
      return;
    this.InsertColumn(dataGridViewColumn1.Ordinal + 1);
  }

  private void DeleteColumn(DataGridViewColumn dataGridViewColumn)
  {
    this._dtData.BeginLoadData();
    DataColumn dataGridViewColumn1 = this.GetDataColumnFromDataGridViewColumn(dataGridViewColumn);
    if (dataGridViewColumn1 == null)
      return;
    this._dtData.Columns.Remove(dataGridViewColumn1);
    this._dtData.EndLoadData();
    this._dtData.AcceptChanges();
    this.dataGridView.Columns.Remove(dataGridViewColumn);
  }

  private int MoveColumnLeft(DataGridViewColumn column)
  {
    if (column == null)
      return -1;
    DataColumn dataGridViewColumn = this.GetDataColumnFromDataGridViewColumn(column);
    if (dataGridViewColumn == null)
      return -1;
    int ordinal = dataGridViewColumn.Ordinal;
    if (ordinal == 0)
      return ordinal;
    int num = ordinal - 1;
    dataGridViewColumn.SetOrdinal(num);
    this.BindGridColumns();
    if (this.dataGridView.SelectedColumns.Count > 0)
      this.dataGridView.SelectedColumns[0].Selected = false;
    this.dataGridView.Columns[num].Selected = true;
    return num;
  }

  private int MoveColumnRight(DataGridViewColumn column)
  {
    if (column == null)
      return -1;
    DataColumn dataGridViewColumn = this.GetDataColumnFromDataGridViewColumn(column);
    if (dataGridViewColumn == null)
      return -1;
    int ordinal = dataGridViewColumn.Ordinal;
    if (ordinal == this._dtData.Columns.Count - 1)
      return ordinal;
    int num = ordinal + 1;
    dataGridViewColumn.SetOrdinal(num);
    this.BindGridColumns();
    if (this.dataGridView.SelectedColumns.Count > 0)
      this.dataGridView.SelectedColumns[0].Selected = false;
    this.dataGridView.Columns[num].Selected = true;
    return num;
  }

  private void LoadSelectedConfig(Configuration configuration)
  {
    this.dataGridView.DataSource = (object) null;
    this.chbSkipFirstRow.Checked = this.chbSkipObjectCreateErrs.Checked = this.chbSkipRelationCreateErrs.Checked = false;
    this.attributePropertyGrid.SelectedObject = (object) null;
    this.chbSkipFirstRow.Checked = configuration.CommonImportOptions.HasFlag((Enum) CommonImportOptions.SkipFirstRow);
    this.chbSkipObjectCreateErrs.Checked = configuration.CommonImportOptions.HasFlag((Enum) CommonImportOptions.IgnoreExistingObjectErrs);
    this.chbSkipRelationCreateErrs.Checked = configuration.CommonImportOptions.HasFlag((Enum) CommonImportOptions.IgnoreExistingRelationErrs);
    foreach (DataColumn column in (InternalDataCollectionBase) this._dtData.Columns)
      column.ExtendedProperties.Clear();
    foreach (ColumnConfiguration columnConfiguration in configuration.ColumnConfigurations)
    {
      if (columnConfiguration != null)
      {
        while (columnConfiguration.Index > this._dtData.Columns.Count - 1)
          this._dtData.Columns.Add(new DataColumn(Guid.NewGuid().ToString(), typeof (string))
          {
            Caption = string.Empty
          });
        this.BindSettingToDtColumn(this._dtData.Columns[columnConfiguration.Index], columnConfiguration);
      }
    }
    this.dataGridView.DataSource = (object) this._dtData;
    this.BindGridColumns();
  }

  private void BindGridColumns()
  {
    this.dataGridView.Columns.Clear();
    foreach (DataColumn column in (InternalDataCollectionBase) this._dtData.Columns)
    {
      DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
      dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
      dataGridViewColumn.CellTemplate = (DataGridViewCell) new DataGridViewTextBoxCell();
      dataGridViewColumn.DataPropertyName = column.ColumnName;
      dataGridViewColumn.HeaderCell.Tag = this._dtData.Rows.Count > 0 ? (object) Convert.ToString(this._dtData.Rows[0][column.Ordinal]) : (object) (string) null;
      this.dataGridView.Columns.Add(dataGridViewColumn);
    }
    this.SetFirstRowVisibleState();
  }

  private void BindSettingToDtColumn(DataColumn column, ColumnConfiguration columnConfiguration)
  {
    if (columnConfiguration == null)
      column.ExtendedProperties.Clear();
    else
      column.ExtendedProperties[(object) Consts.ColumnPropName] = columnConfiguration.Clone();
  }

  private void SetFirstRowVisibleState()
  {
    if (this.dataGridView.Rows.Count <= 0)
      return;
    if (this.BindingContext[this.dataGridView.DataSource] is CurrencyManager currencyManager)
      currencyManager.SuspendBinding();
    this.dataGridView.Rows[0].Visible = !this.chbSkipFirstRow.Checked;
    currencyManager?.ResumeBinding();
  }

  private DataColumn GetDataColumnFromDataGridViewColumn(DataGridViewColumn dataGridViewColumn)
  {
    if (!(this.dataGridView.DataSource is DataTable dataSource))
      throw new ArgumentException("DataGridViewColumn don't have a dataSource");
    return dataSource.Columns[dataGridViewColumn.DataPropertyName];
  }

  private ColumnConfiguration GetColumnConfiguration(DataColumn column)
  {
    return column.ExtendedProperties[(object) Consts.ColumnPropName] as ColumnConfiguration;
  }

  private DataTable GetDataTableFormArray(Array array)
  {
    if (array == null || array.GetLength(1) == 0 || array.GetLength(0) == 0)
      return (DataTable) null;
    DataTable dataTableFormArray = new DataTable();
    dataTableFormArray.BeginLoadData();
    for (int index = 1; index <= array.GetLength(1); ++index)
    {
      DataColumn column = new DataColumn()
      {
        ColumnName = Guid.NewGuid().ToString(),
        Caption = string.Empty
      };
      dataTableFormArray.Columns.Add(column);
    }
    if (array.GetLength(0) == 0)
      return dataTableFormArray;
    for (int index1 = 1; index1 <= array.GetLength(0); ++index1)
    {
      DataRow row = dataTableFormArray.NewRow();
      for (int index2 = 1; index2 <= array.GetLength(1); ++index2)
        row[index2 - 1] = array.GetValue(index1, index2);
      dataTableFormArray.Rows.Add(row);
    }
    dataTableFormArray.EndLoadData();
    dataTableFormArray.AcceptChanges();
    return dataTableFormArray;
  }

  private void SetDataGridViewColumnStyle(DataGridViewColumn column)
  {
    if (!column.Displayed)
      return;
    DataColumn dataGridViewColumn = this.GetDataColumnFromDataGridViewColumn(column);
    if (dataGridViewColumn == null)
      return;
    ColumnConfiguration columnConfiguration = this.GetColumnConfiguration(dataGridViewColumn);
    this.attributePropertyGrid.SelectedObject = columnConfiguration == null || !column.Selected ? (object) (SettingItemTypeDescriptor) null : (object) new SettingItemTypeDescriptor(columnConfiguration, dataGridViewColumn.Table);
    this.SetColumnHeaderStyle(column, columnConfiguration);
  }

  private void SetColumnHeaderStyle(
    DataGridViewColumn column,
    ColumnConfiguration columnConfiguration)
  {
    if (columnConfiguration != null)
      this.SetCustomColumnHeaderStyle(column, columnConfiguration);
    else
      this.SetDefualtColumnHeaderStyle(column);
  }

  private void SetCustomColumnHeaderStyle(
    DataGridViewColumn column,
    ColumnConfiguration columnConfiguration)
  {
    column.DefaultCellStyle.BackColor = SystemColors.Window;
    column.HeaderCell.Style.BackColor = SystemColors.Control;
    switch (columnConfiguration.ItemType)
    {
      case SettingItemType.AttributeType:
        column.HeaderText = MetaDataHelper.GetAttributeTypeName(columnConfiguration.TypeId);
        column.HeaderCell.Style.ForeColor = columnConfiguration.SyncImbase ? Color.Green : Color.Black;
        column.HeaderCell.Style.Font = new Font(this.Font, FontStyle.Bold);
        break;
      case SettingItemType.ObjectType:
      case SettingItemType.EntrancyObjectType:
        column.HeaderText = columnConfiguration.ItemType == SettingItemType.ObjectType ? LocalizationHolder.rm.GetString("Tools.Client_248") : LocalizationHolder.rm.GetString("Tools.Client_303");
        column.HeaderCell.Style.ForeColor = Color.Red;
        column.HeaderCell.Style.Font = new Font(this.Font, FontStyle.Bold);
        break;
      case SettingItemType.RelationType:
        column.HeaderText = LocalizationHolder.rm.GetString("Tools.Client_249");
        column.HeaderCell.Style.ForeColor = Color.Blue;
        column.HeaderCell.Style.Font = new Font(this.Font, FontStyle.Bold);
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  private void SetDefualtColumnHeaderStyle(DataGridViewColumn column)
  {
    column.HeaderCell.Style.BackColor = column.DefaultCellStyle.BackColor = Color.LightGray;
    column.HeaderText = this.chbSkipFirstRow.Checked ? Convert.ToString(column.HeaderCell.Tag) : string.Empty;
    column.HeaderCell.Style.ForeColor = Color.Black;
    column.HeaderCell.Style.Font = new Font(this.Font, FontStyle.Regular);
  }

  private void MapColumns()
  {
    int count = this._dtData.Columns.Count;
    for (int index = 0; index < count; ++index)
    {
      DataGridViewColumn column = this.dataGridView.Columns[index];
      column.DataPropertyName = this._dtData.Columns[index].ColumnName;
      if (this._dtData.Rows.Count > 0)
        column.HeaderCell.Tag = (object) Convert.ToString(this._dtData.Rows[0][index]);
    }
  }

  private void UpdateCommandsState()
  {
    DataGridViewColumn dataGridViewColumn1 = this.dataGridView.Columns.Cast<DataGridViewColumn>().FirstOrDefault<DataGridViewColumn>((System.Func<DataGridViewColumn, bool>) (x => x.Selected));
    if (dataGridViewColumn1 == null)
    {
      this.tsbtnBindAttribute.Enabled = this.tsbtnBindObjectType.Enabled = this.tsbtnBindRelationType.Enabled = this.tsbtnClearBindedItem.Enabled = this.tsbtnInsertLeft.Enabled = this.tsbtnInsertRigth.Enabled = this.tsbtnLeftEnd.Enabled = this.tsbtnLeft.Enabled = this.tsbtnDown.Enabled = this.tsbtnBotton.Enabled = this.tsItemAddAttribute.Enabled = this.tsItemAddObjectType.Enabled = this.tsItemAddRelationType.Enabled = this.tsmiRemove.Enabled = this.tsmiLeftEnd.Enabled = this.tsmiLeft.Enabled = this.tsmiRight.Enabled = this.tsmiRightEnd.Enabled = this.tsitemColumnLeft.Enabled = this.tsitemColumnRight.Enabled = this.tsbtnDeleteColumn.Enabled = this.tsitemDeleteColumn.Enabled = this.tsbtnBindEntranceObjectType.Enabled = this.tsItemAddEntranceObjectType.Enabled = false;
    }
    else
    {
      DataColumn dataGridViewColumn2 = this.GetDataColumnFromDataGridViewColumn(dataGridViewColumn1);
      if (dataGridViewColumn2 == null)
        return;
      this.tsbtnBindAttribute.Enabled = this.tsbtnInsertLeft.Enabled = this.tsbtnInsertRigth.Enabled = this.tsItemAddAttribute.Enabled = this.tsitemColumnLeft.Enabled = this.tsitemColumnRight.Enabled = this.tsbtnDeleteColumn.Enabled = this.tsitemDeleteColumn.Enabled = true;
      this.tsbtnClearBindedItem.Enabled = this.tsmiRemove.Enabled = this.GetColumnConfiguration(dataGridViewColumn2) != null;
      ToolStripButton tsbtnBindObjectType = this.tsbtnBindObjectType;
      ToolStripMenuItem itemAddObjectType = this.tsItemAddObjectType;
      ColumnConfiguration[] columnConfigurations1 = this.ColumnConfigurations;
      int num1;
      bool flag1 = (num1 = !((IEnumerable<ColumnConfiguration>) columnConfigurations1).Any<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x != null && x.ItemType == SettingItemType.ObjectType)) ? 1 : 0) != 0;
      itemAddObjectType.Enabled = num1 != 0;
      int num2 = flag1 ? 1 : 0;
      tsbtnBindObjectType.Enabled = num2 != 0;
      ToolStripButton entranceObjectType1 = this.tsbtnBindEntranceObjectType;
      ToolStripMenuItem entranceObjectType2 = this.tsItemAddEntranceObjectType;
      ColumnConfiguration[] columnConfigurations2 = this.ColumnConfigurations;
      int num3;
      bool flag2 = (num3 = !((IEnumerable<ColumnConfiguration>) columnConfigurations2).Any<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x != null && x.ItemType == SettingItemType.EntrancyObjectType)) ? 1 : 0) != 0;
      entranceObjectType2.Enabled = num3 != 0;
      int num4 = flag2 ? 1 : 0;
      entranceObjectType1.Enabled = num4 != 0;
      ToolStripButton bindRelationType = this.tsbtnBindRelationType;
      ToolStripMenuItem itemAddRelationType = this.tsItemAddRelationType;
      ColumnConfiguration[] columnConfigurations3 = this.ColumnConfigurations;
      int num5;
      bool flag3 = (num5 = !((IEnumerable<ColumnConfiguration>) columnConfigurations3).Any<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x != null && x.ItemType == SettingItemType.RelationType)) ? 1 : 0) != 0;
      itemAddRelationType.Enabled = num5 != 0;
      int num6 = flag3 ? 1 : 0;
      bindRelationType.Enabled = num6 != 0;
      int ordinal = dataGridViewColumn2.Ordinal;
      this.tsbtnLeftEnd.Enabled = this.tsbtnLeft.Enabled = this.tsmiLeft.Enabled = this.tsmiLeftEnd.Enabled = ordinal > 0;
      this.tsbtnDown.Enabled = this.tsbtnBotton.Enabled = this.tsmiRight.Enabled = this.tsmiRightEnd.Enabled = ordinal < this._dtData.Columns.Count - 1;
    }
    this.tsbtnSaveConfiguration.Enabled = this.tsItemSaveConfiguration.Enabled = ((IEnumerable<ColumnConfiguration>) this.ColumnConfigurations).Any<ColumnConfiguration>();
  }

  private string ValidateSettings()
  {
    string empty = string.Empty;
    if (this._dtData == null || this._dtData.Columns.Count == 0 || this._dtData.Rows.Count == 0)
      return LocalizationHolder.rm.GetString("Tools.Client_251");
    ColumnConfiguration[] array1 = ((IEnumerable<ColumnConfiguration>) this.ColumnConfigurations).Where<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x.ItemType == SettingItemType.AttributeType && x.SettingItemAttributeBelongs == SettingItemAttributeSourceType.Object)).ToArray<ColumnConfiguration>();
    ColumnConfiguration[] array2 = ((IEnumerable<ColumnConfiguration>) this.ColumnConfigurations).Where<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x.ItemType == SettingItemType.AttributeType && x.SettingItemAttributeBelongs == SettingItemAttributeSourceType.Relation)).ToArray<ColumnConfiguration>();
    ColumnConfiguration[] array3 = ((IEnumerable<ColumnConfiguration>) this.ColumnConfigurations).Where<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x.ItemType == SettingItemType.AttributeType && x.SettingItemAttributeBelongs == SettingItemAttributeSourceType.Entrancy)).ToArray<ColumnConfiguration>();
    ColumnConfiguration columnConfiguration1 = ((IEnumerable<ColumnConfiguration>) this.ColumnConfigurations).FirstOrDefault<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x.ItemType == SettingItemType.ObjectType));
    ColumnConfiguration columnConfiguration2 = ((IEnumerable<ColumnConfiguration>) this.ColumnConfigurations).FirstOrDefault<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => x.ItemType == SettingItemType.RelationType));
    if (array1.Length == 0)
      return LocalizationHolder.rm.GetString("Tools.Client_252");
    if (columnConfiguration1 == null)
      return LocalizationHolder.rm.GetString("Tools.Client_253");
    if (array3.Length != 0)
    {
      if (array3.Length != 1)
        return LocalizationHolder.rm.GetString("Tools.Client_254");
    }
    else
    {
      if (array2.Length != 0 && this.tbParentObj.Tag == null)
        return LocalizationHolder.rm.GetString("Tools.Client_255");
      if (columnConfiguration2 != null && this.tbParentObj.Tag == null)
        return LocalizationHolder.rm.GetString("Tools.Client_256");
    }
    Dictionary<int, int> dictionary1 = ((IEnumerable<ColumnConfiguration>) array1).Where<ColumnConfiguration>((System.Func<ColumnConfiguration, bool>) (x => !x.SyncImbase)).Select<ColumnConfiguration, int>((System.Func<ColumnConfiguration, int>) (x => x.TypeId)).GroupBy<int, int>((System.Func<int, int>) (x => x)).Where<IGrouping<int, int>>((System.Func<IGrouping<int, int>, bool>) (x => x.Count<int>() > 1)).ToDictionary<IGrouping<int, int>, int, int>((System.Func<IGrouping<int, int>, int>) (x => x.Key), (System.Func<IGrouping<int, int>, int>) (y => y.Count<int>()));
    if (dictionary1.Count > 0)
      return string.Format(LocalizationHolder.rm.GetString("Tools.Client_257"), (object) string.Join(", ", dictionary1.Select<KeyValuePair<int, int>, string>((System.Func<KeyValuePair<int, int>, string>) (x => MetaDataHelper.GetAttributeTypeName(x.Key))).ToArray<string>()));
    Dictionary<int, int> dictionary2 = ((IEnumerable<ColumnConfiguration>) array2).Select<ColumnConfiguration, int>((System.Func<ColumnConfiguration, int>) (x => x.TypeId)).GroupBy<int, int>((System.Func<int, int>) (x => x)).Where<IGrouping<int, int>>((System.Func<IGrouping<int, int>, bool>) (x => x.Count<int>() > 1)).ToDictionary<IGrouping<int, int>, int, int>((System.Func<IGrouping<int, int>, int>) (x => x.Key), (System.Func<IGrouping<int, int>, int>) (y => y.Count<int>()));
    return dictionary2.Count > 0 ? string.Format(LocalizationHolder.rm.GetString("Tools.Client_258"), (object) string.Join(", ", dictionary2.Select<KeyValuePair<int, int>, string>((System.Func<KeyValuePair<int, int>, string>) (x => MetaDataHelper.GetAttributeTypeName(x.Key))).ToArray<string>())) : empty;
  }

  private void setCurrentConfigNameAndType(
    string configurationName,
    ConfigurationType configurationType)
  {
    this._currentConfigurationName = configurationName;
    this.currentConfigName.Text = $"{EnumTypeHelper.GetCaption((Enum) configurationType)} конфигурация '{configurationName}'";
    this._currentConfigurationType = configurationType;
  }

  private void tsbtnBindAttribute_Click(object sender, EventArgs e)
  {
    this.BindAttributeType(this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null);
  }

  private void tsbtnBindObjectType_Click(object sender, EventArgs e)
  {
    this.BindObjectType(this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null);
  }

  private void tsbtnBindEntranceObjectType_Click(object sender, EventArgs e)
  {
    this.BindEntranceObjectType(this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null);
  }

  private void tsbtnBindRelationType_Click(object sender, EventArgs e)
  {
    this.BindRelationType(this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null);
  }

  private void tsbtnClearBindedItem_Click(object sender, EventArgs e)
  {
    this.ClearBindedItem(this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null);
  }

  private void tsbtnInsertLeft_Click(object sender, EventArgs e)
  {
    this.InsertColumnLeft(this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null);
  }

  private void tsbtnInsertRigth_Click(object sender, EventArgs e)
  {
    this.InsertColumnRight(this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null);
  }

  private void tsbtnDeleteColumn_Click(object sender, EventArgs e)
  {
    DataGridViewColumn selectedColumn = this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null;
    if (selectedColumn == null)
      return;
    int index = selectedColumn.Index;
    this.DeleteColumn(selectedColumn);
    this.SetFirstRowVisibleState();
    if (index == this.dataGridView.Columns.Count)
      --index;
    if (index >= 0)
      this.dataGridView.Columns[index].Selected = true;
    this.UpdateCommandsState();
  }

  private void tsbtnLeftEnd_Click(object sender, EventArgs e)
  {
    DataGridViewColumn selectedColumn = this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null;
    if (selectedColumn == null)
      return;
    int index = selectedColumn.Index;
    if (index == 0)
      return;
    while (index > 0)
      index = this.MoveColumnLeft(this.dataGridView.Columns[index]);
  }

  private void tsbtnLeft_Click(object sender, EventArgs e)
  {
    this.MoveColumnLeft(this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null);
  }

  private void tsbtnRight_Click(object sender, EventArgs e)
  {
    this.MoveColumnRight(this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null);
  }

  private void tsbtnRightEnd_Click(object sender, EventArgs e)
  {
    DataGridViewColumn selectedColumn = this.dataGridView.SelectedColumns.Count > 0 ? this.dataGridView.SelectedColumns[0] : (DataGridViewColumn) null;
    if (selectedColumn == null)
      return;
    int index = selectedColumn.Index;
    if (index == this.dataGridView.Columns.Count - 1)
      return;
    while (index != this.dataGridView.Columns.Count - 1 && index != -1)
      index = this.MoveColumnRight(this.dataGridView.Columns[index]);
  }

  private void tsbtnSaveConfiguration_Click(object sender, EventArgs e)
  {
    using (SaveConfigDialogForm configDialogForm = new SaveConfigDialogForm((IEnumerable<Configuration>) this._configurationManager.Configurations(this._configurationManager.IsAdmin), this._configurationManager.IsAdmin, this._currentConfigurationName, this._currentConfigurationType))
    {
      if (configDialogForm.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      string configurationName = configDialogForm.ConfigurationName;
      if (string.IsNullOrEmpty(configurationName))
        return;
      this.setCurrentConfigNameAndType(configurationName, configDialogForm.ConfigurationType);
      this._configurationManager.SaveConfiguration(this.CurrentConfiguration);
    }
  }

  private void tsbtnLoadConfiguration_Click(object sender, EventArgs e)
  {
    using (OpenConfigDialogForm configDialogForm = new OpenConfigDialogForm((IEnumerable<Configuration>) this._configurationManager.Configurations()))
    {
      if (configDialogForm.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      Configuration configuration = configDialogForm.Configuration;
      if (configuration == null)
        return;
      this.setCurrentConfigNameAndType(configuration.Name, configuration.Type);
      this.LoadSelectedConfig(configuration);
    }
  }

  private void tsbtnDeleteConfiguration_Click(object sender, EventArgs e)
  {
    using (OpenConfigDialogForm configDialogForm = new OpenConfigDialogForm((IEnumerable<Configuration>) this._configurationManager.Configurations(this._configurationManager.IsAdmin)))
    {
      if (configDialogForm.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      Configuration configuration = configDialogForm.Configuration;
      if (configuration == null)
        return;
      this._configurationManager.Remove(configuration);
    }
  }

  private void tsbLoadConfigFromFile_Click(object sender, EventArgs e)
  {
    if (this.ofdConfiguration.ShowDialog() != DialogResult.OK)
      return;
    Configuration configuration = this._configurationManager.LoadConfigurationFromFile(this.ofdConfiguration.FileName);
    if (configuration == null)
      return;
    this.setCurrentConfigNameAndType(configuration.Name, configuration.Type);
    this.LoadSelectedConfig(configuration);
  }

  private void tsbSaveConfigToFile_Click(object sender, EventArgs e)
  {
    this.sfdConfiguration.FileName = this.CurrentConfiguration.Name;
    if (this.sfdConfiguration.ShowDialog() != DialogResult.OK)
      return;
    this._configurationManager.SaveConfigurationToFile(this.CurrentConfiguration, this.sfdConfiguration.FileName);
  }

  private void btnSelectObject_Click(object sender, EventArgs e)
  {
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects("", "", SelectionOptions.SelectObjects | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect);
    if (numArray == null || numArray.Length == 0)
      return;
    long objectID = numArray[0];
    this.tbParentObj.Tag = (object) objectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.tbParentObj.Text = sessionKeeper.Session.GetObjectInfo(objectID).Caption;
    this.btnClearSelected.Enabled = this.btnCard.Enabled = true;
  }

  private void btnClearSelected_Click(object sender, EventArgs e)
  {
    this.tbParentObj.Text = string.Empty;
    this.tbParentObj.Text = (string) null;
    this.btnClearSelected.Enabled = this.btnCard.Enabled = false;
  }

  private void btnCard_Click(object sender, EventArgs e)
  {
    if (this.tbParentObj.Tag == null)
      return;
    long int64 = Convert.ToInt64(this.tbParentObj.Tag);
    if (int64 == 0L)
      return;
    int num = (int) PropertiesWindow.Execute("Свойства (Карточка)", string.Empty, int64, true);
  }

  private void tbParentObj_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Delete)
    {
      this.btnClearSelected_Click(sender, new EventArgs());
      e.Handled = true;
    }
    else
    {
      e.SuppressKeyPress = true;
      e.Handled = true;
    }
  }

  private void tsbtnOpenFile_Click(object sender, EventArgs e)
  {
    if (this.ofDialog.ShowDialog() != DialogResult.OK)
      return;
    ColumnConfiguration[] columnConfigurations = this.ColumnConfigurations;
    this._dtData = this.GetDataTableFormArray(ComExcelReader.GetData(this.ofDialog.FileName));
    if (this._dtData == null || this._dtData.Columns.Count == 0 || this._dtData.Rows.Count == 0)
      return;
    this.FileName = this.ofDialog.FileName;
    this.dataGridView.DataSource = (object) this._dtData;
    foreach (ColumnConfiguration columnConfiguration in columnConfigurations)
    {
      if (columnConfiguration != null)
      {
        while (columnConfiguration.Index > this._dtData.Columns.Count - 1)
          this._dtData.Columns.Add(new DataColumn()
          {
            ColumnName = Guid.NewGuid().ToString(),
            Caption = string.Empty
          });
        this.BindSettingToDtColumn(this._dtData.Columns[columnConfiguration.Index], columnConfiguration);
      }
    }
    this.BindGridColumns();
    this.Text = $"{LocalizationHolder.rm.GetString("Tools.Client_250")} - {this.FileName}";
  }

  private void AdvancedImportSettingsFrm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.CurrentConfiguration.ColumnConfigurations.Count == 0 || this.CurrentConfiguration.Equals(this._configurationManager.Configurations().FirstOrDefault<Configuration>((System.Func<Configuration, bool>) (x => x.Name == this.CurrentConfiguration.Name && x.Type == this.CurrentConfiguration.Type))) || MessageBox.Show("Изменения в текущей конфигурации не сохранены! Сохранить изменения?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
      return;
    using (SaveConfigDialogForm configDialogForm = new SaveConfigDialogForm((IEnumerable<Configuration>) this._configurationManager.Configurations(this._configurationManager.IsAdmin), this._configurationManager.IsAdmin, this._currentConfigurationName, this._currentConfigurationType))
    {
      if (configDialogForm.ShowDialog((IWin32Window) this) != DialogResult.OK)
      {
        e.Cancel = true;
      }
      else
      {
        string configurationName = configDialogForm.ConfigurationName;
        if (string.IsNullOrEmpty(configurationName))
          return;
        this.setCurrentConfigNameAndType(configurationName, configDialogForm.ConfigurationType);
        if (this._configurationManager.SaveConfiguration(this.CurrentConfiguration) || MessageBox.Show("Текущая конфигурация не сохранена! Закрыть форму?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel)
          return;
        e.Cancel = true;
      }
    }
  }

  private void chbSkipFirstRow_CheckedChanged(object sender, EventArgs e)
  {
    this.SetFirstRowVisibleState();
  }

  private void attributePropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    this.dataGridView.Refresh();
  }

  private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
  {
    DataColumn dataGridViewColumn = this.GetDataColumnFromDataGridViewColumn(this.dataGridView.Columns[e.ColumnIndex]);
    if (dataGridViewColumn == null)
      return;
    ColumnConfiguration columnConfiguration = this.GetColumnConfiguration(dataGridViewColumn);
    if (columnConfiguration == null || columnConfiguration.ValueKind == SettingItemValueKind.Variable)
      return;
    e.CellStyle.Font = new Font(this.Font, FontStyle.Italic);
    switch (columnConfiguration.ItemType)
    {
      case SettingItemType.AttributeType:
        e.Value = (object) columnConfiguration.AttributeValue;
        e.FormattingApplied = true;
        break;
      case SettingItemType.ObjectType:
      case SettingItemType.EntrancyObjectType:
        IMSObjectType objectType = MetaDataHelper.GetObjectType(columnConfiguration.TypeId);
        e.Value = objectType != null ? (object) objectType.ObjectTypeName : (object) "Неопределенный тип объекта";
        e.FormattingApplied = true;
        break;
      case SettingItemType.RelationType:
        IMSRelationType relationType = MetaDataHelper.GetRelationType(columnConfiguration.TypeId);
        e.Value = relationType != null ? (object) relationType.Description : (object) LocalizationHolder.rm.GetString("Tools.Client_275");
        e.FormattingApplied = true;
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  private void dataGridView_ColumnStateChanged(
    object sender,
    DataGridViewColumnStateChangedEventArgs e)
  {
    DataGridViewColumn column = e.Column;
    if (!column.Displayed)
      return;
    DataColumn dataGridViewColumn = this.GetDataColumnFromDataGridViewColumn(column);
    if (dataGridViewColumn == null)
      return;
    ColumnConfiguration columnConfiguration = this.GetColumnConfiguration(dataGridViewColumn);
    if (e.StateChanged == DataGridViewElementStates.Selected)
      this.attributePropertyGrid.SelectedObject = columnConfiguration == null || !column.Selected ? (object) (SettingItemTypeDescriptor) null : (object) new SettingItemTypeDescriptor(columnConfiguration, dataGridViewColumn.Table);
    this.SetColumnHeaderStyle(column, columnConfiguration);
    this.UpdateCommandsState();
  }

  private void btnImport_Click(object sender, EventArgs e)
  {
    string text = this.ValidateSettings();
    if (text != string.Empty)
    {
      int num = (int) MessageBox.Show(text, LocalizationHolder.rm.GetString("Tools.Client_44"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
      this.DialogResult = DialogResult.OK;
  }

  private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
  {
    e.ThrowException = false;
  }

  public DataTable GetResultDataTable()
  {
    this.dataGridView.CurrentCell = (DataGridViewCell) null;
    this._dtData.BeginLoadData();
    if (this.chbSkipFirstRow.Checked && this._dtData.Rows.Count > 1)
      this._dtData.Rows[0].Delete();
    foreach (DataColumn column in (InternalDataCollectionBase) this._dtData.Columns)
    {
      ColumnConfiguration columnConfiguration = this.GetColumnConfiguration(column);
      if (columnConfiguration != null && columnConfiguration.ValueKind == SettingItemValueKind.Constant)
      {
        switch (columnConfiguration.ItemType)
        {
          case SettingItemType.AttributeType:
            column.Expression = $"'{columnConfiguration.AttributeValue}'";
            continue;
          case SettingItemType.ObjectType:
          case SettingItemType.RelationType:
          case SettingItemType.EntrancyObjectType:
            column.Expression = Convert.ToString(columnConfiguration.TypeId);
            continue;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }
    this._dtData.EndLoadData();
    this._dtData.AcceptChanges();
    ImportSettings importSettings = new ImportSettings(this.CommonImportOptions, this.tbParentObj.Tag != null ? Convert.ToInt64(this.tbParentObj.Tag) : 0L);
    this._dtData.ExtendedProperties[(object) Consts.ImportSettings] = (object) importSettings;
    return this._dtData;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AdvancedImportSettingsFrm));
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
    this.cmLvSettingItem = new ContextMenuStrip(this.components);
    this.tsItemAdd = new ToolStripMenuItem();
    this.tsItemAddAttribute = new ToolStripMenuItem();
    this.tsItemAddObjectType = new ToolStripMenuItem();
    this.tsItemAddEntranceObjectType = new ToolStripMenuItem();
    this.tsItemAddRelationType = new ToolStripMenuItem();
    this.tsmiRemove = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.tsitemInsert = new ToolStripMenuItem();
    this.tsitemColumnLeft = new ToolStripMenuItem();
    this.tsitemColumnRight = new ToolStripMenuItem();
    this.tsitemDeleteColumn = new ToolStripMenuItem();
    this.toolStripSeparator8 = new ToolStripSeparator();
    this.tsmiLeftEnd = new ToolStripMenuItem();
    this.tsmiLeft = new ToolStripMenuItem();
    this.tsmiRight = new ToolStripMenuItem();
    this.tsmiRightEnd = new ToolStripMenuItem();
    this.toolStripSeparator5 = new ToolStripSeparator();
    this.tsItemConfiguration = new ToolStripMenuItem();
    this.tsItemLoadConfiguration = new ToolStripMenuItem();
    this.tsItemSaveConfiguration = new ToolStripMenuItem();
    this.tsItemDeleteConfiguration = new ToolStripMenuItem();
    this.toolStripSeparator9 = new ToolStripSeparator();
    this.tsmiLoadConfigFromFile = new ToolStripMenuItem();
    this.tsmiSaveConfigToFile = new ToolStripMenuItem();
    this.tstrpLvSettingItem = new ToolStrip();
    this.tsbtnOpenFile = new ToolStripButton();
    this.toolStripSeparator4 = new ToolStripSeparator();
    this.tsbtnBindAttribute = new ToolStripButton();
    this.tsbtnBindObjectType = new ToolStripButton();
    this.tsbtnBindEntranceObjectType = new ToolStripButton();
    this.tsbtnBindRelationType = new ToolStripButton();
    this.tsbtnClearBindedItem = new ToolStripButton();
    this.toolStripSeparator6 = new ToolStripSeparator();
    this.tsbtnInsertLeft = new ToolStripButton();
    this.tsbtnInsertRigth = new ToolStripButton();
    this.tsbtnDeleteColumn = new ToolStripButton();
    this.toolStripSeparator7 = new ToolStripSeparator();
    this.tsbtnLeftEnd = new ToolStripButton();
    this.tsbtnLeft = new ToolStripButton();
    this.tsbtnDown = new ToolStripButton();
    this.tsbtnBotton = new ToolStripButton();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.tsbtnLoadConfiguration = new ToolStripButton();
    this.tsbtnSaveConfiguration = new ToolStripButton();
    this.tsbtnDeleteConfiguration = new ToolStripButton();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this.tsbLoadConfigFromFile = new ToolStripButton();
    this.tsbSaveConfigToFile = new ToolStripButton();
    this.toolStripSeparator10 = new ToolStripSeparator();
    this.currentConfigName = new ToolStripLabel();
    this.panel2 = new Panel();
    this.btnCancel = new Button();
    this.btnImport = new Button();
    this.ofDialog = new OpenFileDialog();
    this.pnlLeft = new Panel();
    this.dataGridView = new DataGridView();
    this.pnlRight = new Panel();
    this.pnlCommonProperties = new Panel();
    this.label1 = new Label();
    this.pnlSelectObj = new Panel();
    this.tbParentObj = new TextBox();
    this.btnSelectObject = new Button();
    this.btnClearSelected = new Button();
    this.btnCard = new Button();
    this.chbSkipRelationCreateErrs = new CheckBox();
    this.chbSkipObjectCreateErrs = new CheckBox();
    this.chbSkipFirstRow = new CheckBox();
    this.splitter2 = new Splitter();
    this.attributePropertyGrid = new PropertyGrid();
    this.splitter1 = new Splitter();
    this.ofdConfiguration = new OpenFileDialog();
    this.sfdConfiguration = new SaveFileDialog();
    this.cmLvSettingItem.SuspendLayout();
    this.tstrpLvSettingItem.SuspendLayout();
    this.panel2.SuspendLayout();
    this.pnlLeft.SuspendLayout();
    ((ISupportInitialize) this.dataGridView).BeginInit();
    this.pnlRight.SuspendLayout();
    this.pnlCommonProperties.SuspendLayout();
    this.pnlSelectObj.SuspendLayout();
    this.SuspendLayout();
    this.cmLvSettingItem.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.tsItemAdd,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.tsitemInsert,
      (ToolStripItem) this.toolStripSeparator5,
      (ToolStripItem) this.tsItemConfiguration
    });
    this.cmLvSettingItem.Name = "contextMenuStrip1";
    this.cmLvSettingItem.Size = new Size(171, 82);
    this.tsItemAdd.DropDownItems.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.tsItemAddAttribute,
      (ToolStripItem) this.tsItemAddObjectType,
      (ToolStripItem) this.tsItemAddEntranceObjectType,
      (ToolStripItem) this.tsItemAddRelationType,
      (ToolStripItem) this.tsmiRemove
    });
    this.tsItemAdd.Name = "tsItemAdd";
    this.tsItemAdd.Size = new Size(170, 22);
    this.tsItemAdd.Text = "Привязка данных";
    this.tsItemAdd.ToolTipText = "Добавить";
    this.tsItemAddAttribute.Image = (Image) componentResourceManager.GetObject("tsItemAddAttribute.Image");
    this.tsItemAddAttribute.Name = "tsItemAddAttribute";
    this.tsItemAddAttribute.Size = new Size(270, 22);
    this.tsItemAddAttribute.Text = "Назначить атрибут";
    this.tsItemAddAttribute.ToolTipText = "Добавить атрибут";
    this.tsItemAddAttribute.Click += new EventHandler(this.tsbtnBindAttribute_Click);
    this.tsItemAddObjectType.Image = (Image) componentResourceManager.GetObject("tsItemAddObjectType.Image");
    this.tsItemAddObjectType.Name = "tsItemAddObjectType";
    this.tsItemAddObjectType.Size = new Size(270, 22);
    this.tsItemAddObjectType.Text = "Назначить тип объекта";
    this.tsItemAddObjectType.ToolTipText = "Назначить тип объекта входимости";
    this.tsItemAddObjectType.Click += new EventHandler(this.tsbtnBindObjectType_Click);
    this.tsItemAddEntranceObjectType.Image = (Image) componentResourceManager.GetObject("tsItemAddEntranceObjectType.Image");
    this.tsItemAddEntranceObjectType.Name = "tsItemAddEntranceObjectType";
    this.tsItemAddEntranceObjectType.Size = new Size(270, 22);
    this.tsItemAddEntranceObjectType.Text = "Назначить тип объекта входимости";
    this.tsItemAddEntranceObjectType.Click += new EventHandler(this.tsbtnBindEntranceObjectType_Click);
    this.tsItemAddRelationType.Image = (Image) componentResourceManager.GetObject("tsItemAddRelationType.Image");
    this.tsItemAddRelationType.Name = "tsItemAddRelationType";
    this.tsItemAddRelationType.Size = new Size(270, 22);
    this.tsItemAddRelationType.Text = "Назначить тип связи";
    this.tsItemAddRelationType.ToolTipText = "Добавить тип связи";
    this.tsItemAddRelationType.Click += new EventHandler(this.tsbtnBindRelationType_Click);
    this.tsmiRemove.Image = (Image) componentResourceManager.GetObject("tsmiRemove.Image");
    this.tsmiRemove.Name = "tsmiRemove";
    this.tsmiRemove.Size = new Size(270, 22);
    this.tsmiRemove.Text = "Очистить";
    this.tsmiRemove.Click += new EventHandler(this.tsbtnClearBindedItem_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(167, 6);
    this.tsitemInsert.DropDownItems.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this.tsitemColumnLeft,
      (ToolStripItem) this.tsitemColumnRight,
      (ToolStripItem) this.tsitemDeleteColumn,
      (ToolStripItem) this.toolStripSeparator8,
      (ToolStripItem) this.tsmiLeftEnd,
      (ToolStripItem) this.tsmiLeft,
      (ToolStripItem) this.tsmiRight,
      (ToolStripItem) this.tsmiRightEnd
    });
    this.tsitemInsert.Name = "tsitemInsert";
    this.tsitemInsert.Size = new Size(170, 22);
    this.tsitemInsert.Text = "Данные";
    this.tsitemColumnLeft.Image = (Image) componentResourceManager.GetObject("tsitemColumnLeft.Image");
    this.tsitemColumnLeft.Name = "tsitemColumnLeft";
    this.tsitemColumnLeft.Size = new Size(211, 22);
    this.tsitemColumnLeft.Text = "Вставить столбец слева";
    this.tsitemColumnLeft.Click += new EventHandler(this.tsbtnInsertLeft_Click);
    this.tsitemColumnRight.Image = (Image) componentResourceManager.GetObject("tsitemColumnRight.Image");
    this.tsitemColumnRight.Name = "tsitemColumnRight";
    this.tsitemColumnRight.Size = new Size(211, 22);
    this.tsitemColumnRight.Text = "Вставить столбец справа";
    this.tsitemColumnRight.Click += new EventHandler(this.tsbtnInsertRigth_Click);
    this.tsitemDeleteColumn.Image = (Image) componentResourceManager.GetObject("tsitemDeleteColumn.Image");
    this.tsitemDeleteColumn.Name = "tsitemDeleteColumn";
    this.tsitemDeleteColumn.Size = new Size(211, 22);
    this.tsitemDeleteColumn.Text = "Удалить столбец";
    this.tsitemDeleteColumn.Click += new EventHandler(this.tsbtnDeleteColumn_Click);
    this.toolStripSeparator8.Name = "toolStripSeparator8";
    this.toolStripSeparator8.Size = new Size(208 /*0xD0*/, 6);
    this.tsmiLeftEnd.Enabled = false;
    this.tsmiLeftEnd.Image = (Image) componentResourceManager.GetObject("tsmiLeftEnd.Image");
    this.tsmiLeftEnd.Name = "tsmiLeftEnd";
    this.tsmiLeftEnd.Size = new Size(211, 22);
    this.tsmiLeftEnd.Text = "Первый";
    this.tsmiLeftEnd.ToolTipText = "Первый";
    this.tsmiLeftEnd.Click += new EventHandler(this.tsbtnLeftEnd_Click);
    this.tsmiLeft.Enabled = false;
    this.tsmiLeft.Image = (Image) componentResourceManager.GetObject("tsmiLeft.Image");
    this.tsmiLeft.Name = "tsmiLeft";
    this.tsmiLeft.Size = new Size(211, 22);
    this.tsmiLeft.Text = "Влево";
    this.tsmiLeft.ToolTipText = "Влево";
    this.tsmiLeft.Click += new EventHandler(this.tsbtnLeft_Click);
    this.tsmiRight.Enabled = false;
    this.tsmiRight.Image = (Image) componentResourceManager.GetObject("tsmiRight.Image");
    this.tsmiRight.Name = "tsmiRight";
    this.tsmiRight.Size = new Size(211, 22);
    this.tsmiRight.Text = "Вправо";
    this.tsmiRight.ToolTipText = "Вправо";
    this.tsmiRight.Click += new EventHandler(this.tsbtnRight_Click);
    this.tsmiRightEnd.Enabled = false;
    this.tsmiRightEnd.Image = (Image) componentResourceManager.GetObject("tsmiRightEnd.Image");
    this.tsmiRightEnd.Name = "tsmiRightEnd";
    this.tsmiRightEnd.Size = new Size(211, 22);
    this.tsmiRightEnd.Text = "Последний";
    this.tsmiRightEnd.ToolTipText = "Последний";
    this.tsmiRightEnd.Click += new EventHandler(this.tsbtnRightEnd_Click);
    this.toolStripSeparator5.Name = "toolStripSeparator5";
    this.toolStripSeparator5.Size = new Size(167, 6);
    this.tsItemConfiguration.DropDownItems.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.tsItemLoadConfiguration,
      (ToolStripItem) this.tsItemSaveConfiguration,
      (ToolStripItem) this.tsItemDeleteConfiguration,
      (ToolStripItem) this.toolStripSeparator9,
      (ToolStripItem) this.tsmiLoadConfigFromFile,
      (ToolStripItem) this.tsmiSaveConfigToFile
    });
    this.tsItemConfiguration.Name = "tsItemConfiguration";
    this.tsItemConfiguration.Size = new Size(170, 22);
    this.tsItemConfiguration.Text = "Конфигурация";
    this.tsItemConfiguration.ToolTipText = "Конфигурация";
    this.tsItemLoadConfiguration.Image = (Image) componentResourceManager.GetObject("tsItemLoadConfiguration.Image");
    this.tsItemLoadConfiguration.Name = "tsItemLoadConfiguration";
    this.tsItemLoadConfiguration.Size = new Size(302, 22);
    this.tsItemLoadConfiguration.Text = "Загрузить конфигурацию";
    this.tsItemLoadConfiguration.ToolTipText = "Загрузить конфигурацию";
    this.tsItemLoadConfiguration.Click += new EventHandler(this.tsbtnLoadConfiguration_Click);
    this.tsItemSaveConfiguration.Image = (Image) componentResourceManager.GetObject("tsItemSaveConfiguration.Image");
    this.tsItemSaveConfiguration.Name = "tsItemSaveConfiguration";
    this.tsItemSaveConfiguration.Size = new Size(302, 22);
    this.tsItemSaveConfiguration.Text = "Сохранить конфигурацию";
    this.tsItemSaveConfiguration.ToolTipText = "Сохранить конфигурацию";
    this.tsItemSaveConfiguration.Click += new EventHandler(this.tsbtnSaveConfiguration_Click);
    this.tsItemDeleteConfiguration.Image = (Image) componentResourceManager.GetObject("tsItemDeleteConfiguration.Image");
    this.tsItemDeleteConfiguration.Name = "tsItemDeleteConfiguration";
    this.tsItemDeleteConfiguration.Size = new Size(302, 22);
    this.tsItemDeleteConfiguration.Text = "Удалить конфигурацию";
    this.tsItemDeleteConfiguration.ToolTipText = "Удалить конфигурацию";
    this.tsItemDeleteConfiguration.Click += new EventHandler(this.tsbtnDeleteConfiguration_Click);
    this.toolStripSeparator9.Name = "toolStripSeparator9";
    this.toolStripSeparator9.Size = new Size(299, 6);
    this.tsmiLoadConfigFromFile.Image = (Image) componentResourceManager.GetObject("tsmiLoadConfigFromFile.Image");
    this.tsmiLoadConfigFromFile.Name = "tsmiLoadConfigFromFile";
    this.tsmiLoadConfigFromFile.Size = new Size(302, 22);
    this.tsmiLoadConfigFromFile.Text = "Импортировать конфигурацию из файла";
    this.tsmiLoadConfigFromFile.Click += new EventHandler(this.tsbLoadConfigFromFile_Click);
    this.tsmiSaveConfigToFile.Image = (Image) componentResourceManager.GetObject("tsmiSaveConfigToFile.Image");
    this.tsmiSaveConfigToFile.Name = "tsmiSaveConfigToFile";
    this.tsmiSaveConfigToFile.Size = new Size(302, 22);
    this.tsmiSaveConfigToFile.Text = "Сохранить конфигурацию в файл";
    this.tsmiSaveConfigToFile.Click += new EventHandler(this.tsbSaveConfigToFile_Click);
    this.tstrpLvSettingItem.AutoSize = false;
    this.tstrpLvSettingItem.GripStyle = ToolStripGripStyle.Hidden;
    this.tstrpLvSettingItem.Items.AddRange(new ToolStripItem[25]
    {
      (ToolStripItem) this.tsbtnOpenFile,
      (ToolStripItem) this.toolStripSeparator4,
      (ToolStripItem) this.tsbtnBindAttribute,
      (ToolStripItem) this.tsbtnBindObjectType,
      (ToolStripItem) this.tsbtnBindEntranceObjectType,
      (ToolStripItem) this.tsbtnBindRelationType,
      (ToolStripItem) this.tsbtnClearBindedItem,
      (ToolStripItem) this.toolStripSeparator6,
      (ToolStripItem) this.tsbtnInsertLeft,
      (ToolStripItem) this.tsbtnInsertRigth,
      (ToolStripItem) this.tsbtnDeleteColumn,
      (ToolStripItem) this.toolStripSeparator7,
      (ToolStripItem) this.tsbtnLeftEnd,
      (ToolStripItem) this.tsbtnLeft,
      (ToolStripItem) this.tsbtnDown,
      (ToolStripItem) this.tsbtnBotton,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.tsbtnLoadConfiguration,
      (ToolStripItem) this.tsbtnSaveConfiguration,
      (ToolStripItem) this.tsbtnDeleteConfiguration,
      (ToolStripItem) this.toolStripSeparator3,
      (ToolStripItem) this.tsbLoadConfigFromFile,
      (ToolStripItem) this.tsbSaveConfigToFile,
      (ToolStripItem) this.toolStripSeparator10,
      (ToolStripItem) this.currentConfigName
    });
    this.tstrpLvSettingItem.Location = new Point(0, 0);
    this.tstrpLvSettingItem.Name = "tstrpLvSettingItem";
    this.tstrpLvSettingItem.Size = new Size(734, 40);
    this.tstrpLvSettingItem.TabIndex = 10;
    this.tstrpLvSettingItem.Text = "toolStrip1";
    this.tsbtnOpenFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnOpenFile.Image = (Image) componentResourceManager.GetObject("tsbtnOpenFile.Image");
    this.tsbtnOpenFile.ImageTransparentColor = Color.Magenta;
    this.tsbtnOpenFile.Name = "tsbtnOpenFile";
    this.tsbtnOpenFile.Size = new Size(23, 37);
    this.tsbtnOpenFile.Text = "Открыть файл";
    this.tsbtnOpenFile.Click += new EventHandler(this.tsbtnOpenFile_Click);
    this.toolStripSeparator4.Name = "toolStripSeparator4";
    this.toolStripSeparator4.Size = new Size(6, 40);
    this.tsbtnBindAttribute.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnBindAttribute.Enabled = false;
    this.tsbtnBindAttribute.Image = (Image) componentResourceManager.GetObject("tsbtnBindAttribute.Image");
    this.tsbtnBindAttribute.ImageTransparentColor = Color.Magenta;
    this.tsbtnBindAttribute.Name = "tsbtnBindAttribute";
    this.tsbtnBindAttribute.Size = new Size(23, 37);
    this.tsbtnBindAttribute.Text = "Назначить атрибут";
    this.tsbtnBindAttribute.Click += new EventHandler(this.tsbtnBindAttribute_Click);
    this.tsbtnBindObjectType.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnBindObjectType.Enabled = false;
    this.tsbtnBindObjectType.Image = (Image) componentResourceManager.GetObject("tsbtnBindObjectType.Image");
    this.tsbtnBindObjectType.ImageTransparentColor = Color.Magenta;
    this.tsbtnBindObjectType.Name = "tsbtnBindObjectType";
    this.tsbtnBindObjectType.Size = new Size(23, 37);
    this.tsbtnBindObjectType.Text = "Назначить тип объекта";
    this.tsbtnBindObjectType.ToolTipText = "Назначить тип объекта";
    this.tsbtnBindObjectType.Click += new EventHandler(this.tsbtnBindObjectType_Click);
    this.tsbtnBindEntranceObjectType.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnBindEntranceObjectType.Enabled = false;
    this.tsbtnBindEntranceObjectType.Image = (Image) componentResourceManager.GetObject("tsbtnBindEntranceObjectType.Image");
    this.tsbtnBindEntranceObjectType.ImageTransparentColor = Color.Magenta;
    this.tsbtnBindEntranceObjectType.Name = "tsbtnBindEntranceObjectType";
    this.tsbtnBindEntranceObjectType.Size = new Size(23, 37);
    this.tsbtnBindEntranceObjectType.Text = "Назначить тип объекта входимости";
    this.tsbtnBindEntranceObjectType.ToolTipText = "Назначить тип объекта входимости";
    this.tsbtnBindEntranceObjectType.Click += new EventHandler(this.tsbtnBindEntranceObjectType_Click);
    this.tsbtnBindRelationType.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnBindRelationType.Enabled = false;
    this.tsbtnBindRelationType.Image = (Image) componentResourceManager.GetObject("tsbtnBindRelationType.Image");
    this.tsbtnBindRelationType.ImageTransparentColor = Color.Magenta;
    this.tsbtnBindRelationType.Name = "tsbtnBindRelationType";
    this.tsbtnBindRelationType.Size = new Size(23, 37);
    this.tsbtnBindRelationType.Text = "Назначить тип связи";
    this.tsbtnBindRelationType.ToolTipText = "Назначить тип связи";
    this.tsbtnBindRelationType.Click += new EventHandler(this.tsbtnBindRelationType_Click);
    this.tsbtnClearBindedItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnClearBindedItem.Enabled = false;
    this.tsbtnClearBindedItem.Image = (Image) componentResourceManager.GetObject("tsbtnClearBindedItem.Image");
    this.tsbtnClearBindedItem.ImageTransparentColor = Color.Magenta;
    this.tsbtnClearBindedItem.Name = "tsbtnClearBindedItem";
    this.tsbtnClearBindedItem.Size = new Size(23, 37);
    this.tsbtnClearBindedItem.Text = "Очистить";
    this.tsbtnClearBindedItem.Click += new EventHandler(this.tsbtnClearBindedItem_Click);
    this.toolStripSeparator6.Name = "toolStripSeparator6";
    this.toolStripSeparator6.Size = new Size(6, 40);
    this.tsbtnInsertLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnInsertLeft.Enabled = false;
    this.tsbtnInsertLeft.Image = (Image) componentResourceManager.GetObject("tsbtnInsertLeft.Image");
    this.tsbtnInsertLeft.ImageTransparentColor = Color.Magenta;
    this.tsbtnInsertLeft.Name = "tsbtnInsertLeft";
    this.tsbtnInsertLeft.Size = new Size(23, 37);
    this.tsbtnInsertLeft.Text = "Вставить столбец слева";
    this.tsbtnInsertLeft.Click += new EventHandler(this.tsbtnInsertLeft_Click);
    this.tsbtnInsertRigth.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnInsertRigth.Enabled = false;
    this.tsbtnInsertRigth.Image = (Image) componentResourceManager.GetObject("tsbtnInsertRigth.Image");
    this.tsbtnInsertRigth.ImageTransparentColor = Color.Magenta;
    this.tsbtnInsertRigth.Name = "tsbtnInsertRigth";
    this.tsbtnInsertRigth.Size = new Size(23, 37);
    this.tsbtnInsertRigth.Text = "Вставить столбец справа";
    this.tsbtnInsertRigth.Click += new EventHandler(this.tsbtnInsertRigth_Click);
    this.tsbtnDeleteColumn.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnDeleteColumn.Enabled = false;
    this.tsbtnDeleteColumn.Image = (Image) componentResourceManager.GetObject("tsbtnDeleteColumn.Image");
    this.tsbtnDeleteColumn.ImageTransparentColor = Color.Magenta;
    this.tsbtnDeleteColumn.Name = "tsbtnDeleteColumn";
    this.tsbtnDeleteColumn.Size = new Size(23, 37);
    this.tsbtnDeleteColumn.Text = "Удалить столбец";
    this.tsbtnDeleteColumn.Click += new EventHandler(this.tsbtnDeleteColumn_Click);
    this.toolStripSeparator7.Name = "toolStripSeparator7";
    this.toolStripSeparator7.Size = new Size(6, 40);
    this.tsbtnLeftEnd.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnLeftEnd.Enabled = false;
    this.tsbtnLeftEnd.Image = (Image) componentResourceManager.GetObject("tsbtnLeftEnd.Image");
    this.tsbtnLeftEnd.ImageTransparentColor = Color.Magenta;
    this.tsbtnLeftEnd.Name = "tsbtnLeftEnd";
    this.tsbtnLeftEnd.Size = new Size(23, 37);
    this.tsbtnLeftEnd.Text = "Первый";
    this.tsbtnLeftEnd.Click += new EventHandler(this.tsbtnLeftEnd_Click);
    this.tsbtnLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnLeft.Enabled = false;
    this.tsbtnLeft.Image = (Image) componentResourceManager.GetObject("tsbtnLeft.Image");
    this.tsbtnLeft.ImageTransparentColor = Color.Magenta;
    this.tsbtnLeft.Name = "tsbtnLeft";
    this.tsbtnLeft.Size = new Size(23, 37);
    this.tsbtnLeft.Text = "Влево";
    this.tsbtnLeft.Click += new EventHandler(this.tsbtnLeft_Click);
    this.tsbtnDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnDown.Enabled = false;
    this.tsbtnDown.Image = (Image) componentResourceManager.GetObject("tsbtnDown.Image");
    this.tsbtnDown.ImageTransparentColor = Color.Magenta;
    this.tsbtnDown.Name = "tsbtnDown";
    this.tsbtnDown.Size = new Size(23, 37);
    this.tsbtnDown.Text = "Вправо";
    this.tsbtnDown.Click += new EventHandler(this.tsbtnRight_Click);
    this.tsbtnBotton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnBotton.Enabled = false;
    this.tsbtnBotton.Image = (Image) componentResourceManager.GetObject("tsbtnBotton.Image");
    this.tsbtnBotton.ImageTransparentColor = Color.Magenta;
    this.tsbtnBotton.Name = "tsbtnBotton";
    this.tsbtnBotton.Size = new Size(23, 37);
    this.tsbtnBotton.Text = "Последний";
    this.tsbtnBotton.Click += new EventHandler(this.tsbtnRightEnd_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(6, 40);
    this.tsbtnLoadConfiguration.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnLoadConfiguration.Image = (Image) componentResourceManager.GetObject("tsbtnLoadConfiguration.Image");
    this.tsbtnLoadConfiguration.ImageTransparentColor = Color.Magenta;
    this.tsbtnLoadConfiguration.Name = "tsbtnLoadConfiguration";
    this.tsbtnLoadConfiguration.Size = new Size(23, 37);
    this.tsbtnLoadConfiguration.Text = "Загрузить конфигурацию";
    this.tsbtnLoadConfiguration.Click += new EventHandler(this.tsbtnLoadConfiguration_Click);
    this.tsbtnSaveConfiguration.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnSaveConfiguration.Enabled = false;
    this.tsbtnSaveConfiguration.Image = (Image) componentResourceManager.GetObject("tsbtnSaveConfiguration.Image");
    this.tsbtnSaveConfiguration.ImageTransparentColor = Color.Magenta;
    this.tsbtnSaveConfiguration.Name = "tsbtnSaveConfiguration";
    this.tsbtnSaveConfiguration.Size = new Size(23, 37);
    this.tsbtnSaveConfiguration.Text = "Сохранить конфигурацию";
    this.tsbtnSaveConfiguration.Click += new EventHandler(this.tsbtnSaveConfiguration_Click);
    this.tsbtnDeleteConfiguration.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnDeleteConfiguration.Image = (Image) componentResourceManager.GetObject("tsbtnDeleteConfiguration.Image");
    this.tsbtnDeleteConfiguration.ImageTransparentColor = Color.Magenta;
    this.tsbtnDeleteConfiguration.Name = "tsbtnDeleteConfiguration";
    this.tsbtnDeleteConfiguration.Size = new Size(23, 37);
    this.tsbtnDeleteConfiguration.Text = "Удалить конфигурацию";
    this.tsbtnDeleteConfiguration.Click += new EventHandler(this.tsbtnDeleteConfiguration_Click);
    this.toolStripSeparator3.Name = "toolStripSeparator3";
    this.toolStripSeparator3.Size = new Size(6, 40);
    this.tsbLoadConfigFromFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbLoadConfigFromFile.Image = (Image) componentResourceManager.GetObject("tsbLoadConfigFromFile.Image");
    this.tsbLoadConfigFromFile.ImageTransparentColor = Color.Magenta;
    this.tsbLoadConfigFromFile.Name = "tsbLoadConfigFromFile";
    this.tsbLoadConfigFromFile.Size = new Size(23, 37);
    this.tsbLoadConfigFromFile.Text = "Импортировать конфигурацию из файла";
    this.tsbLoadConfigFromFile.ToolTipText = "Открыть конфигурацию из файла";
    this.tsbLoadConfigFromFile.Click += new EventHandler(this.tsbLoadConfigFromFile_Click);
    this.tsbSaveConfigToFile.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbSaveConfigToFile.Image = (Image) componentResourceManager.GetObject("tsbSaveConfigToFile.Image");
    this.tsbSaveConfigToFile.ImageTransparentColor = Color.Magenta;
    this.tsbSaveConfigToFile.Name = "tsbSaveConfigToFile";
    this.tsbSaveConfigToFile.Size = new Size(23, 37);
    this.tsbSaveConfigToFile.Text = "Сохранить конфигурацию в файл";
    this.tsbSaveConfigToFile.Click += new EventHandler(this.tsbSaveConfigToFile_Click);
    this.toolStripSeparator10.Name = "toolStripSeparator10";
    this.toolStripSeparator10.Size = new Size(6, 40);
    this.currentConfigName.Name = "currentConfigName";
    this.currentConfigName.Size = new Size(0, 37);
    this.panel2.Controls.Add((Control) this.btnCancel);
    this.panel2.Controls.Add((Control) this.btnImport);
    this.panel2.Dock = DockStyle.Bottom;
    this.panel2.Location = new Point(0, 411);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(734, 50);
    this.panel2.TabIndex = 11;
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(598, 11);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(124, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnImport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnImport.Location = new Point(463, 11);
    this.btnImport.Name = "btnImport";
    this.btnImport.Size = new Size((int) sbyte.MaxValue, 23);
    this.btnImport.TabIndex = 0;
    this.btnImport.Text = "Импорт";
    this.btnImport.UseVisualStyleBackColor = true;
    this.btnImport.Click += new EventHandler(this.btnImport_Click);
    this.ofDialog.DefaultExt = "xlsx";
    this.ofDialog.Filter = "Excel files|*.xlsx;*.xls|All files|*.*";
    this.ofDialog.RestoreDirectory = true;
    this.pnlLeft.Controls.Add((Control) this.dataGridView);
    this.pnlLeft.Dock = DockStyle.Fill;
    this.pnlLeft.Location = new Point(0, 40);
    this.pnlLeft.Name = "pnlLeft";
    this.pnlLeft.Size = new Size(448, 371);
    this.pnlLeft.TabIndex = 12;
    this.dataGridView.AllowUserToAddRows = false;
    this.dataGridView.AllowUserToDeleteRows = false;
    this.dataGridView.AllowUserToResizeRows = false;
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle1.BackColor = SystemColors.Control;
    gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle1.ForeColor = SystemColors.WindowText;
    gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
    this.dataGridView.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
    this.dataGridView.ContextMenuStrip = this.cmLvSettingItem;
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle2.BackColor = SystemColors.Window;
    gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle2.ForeColor = SystemColors.ControlText;
    gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
    this.dataGridView.DefaultCellStyle = gridViewCellStyle2;
    this.dataGridView.Dock = DockStyle.Fill;
    this.dataGridView.EnableHeadersVisualStyles = false;
    this.dataGridView.Location = new Point(0, 0);
    this.dataGridView.MultiSelect = false;
    this.dataGridView.Name = "dataGridView";
    this.dataGridView.ReadOnly = true;
    gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle3.BackColor = SystemColors.Control;
    gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle3.ForeColor = SystemColors.WindowText;
    gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
    this.dataGridView.RowHeadersDefaultCellStyle = gridViewCellStyle3;
    this.dataGridView.RowHeadersVisible = false;
    this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
    this.dataGridView.ShowEditingIcon = false;
    this.dataGridView.Size = new Size(448, 371);
    this.dataGridView.TabIndex = 14;
    this.dataGridView.VirtualMode = true;
    this.dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
    this.dataGridView.ColumnStateChanged += new DataGridViewColumnStateChangedEventHandler(this.dataGridView_ColumnStateChanged);
    this.dataGridView.DataError += new DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
    this.pnlRight.Controls.Add((Control) this.pnlCommonProperties);
    this.pnlRight.Controls.Add((Control) this.splitter2);
    this.pnlRight.Controls.Add((Control) this.attributePropertyGrid);
    this.pnlRight.Dock = DockStyle.Right;
    this.pnlRight.Location = new Point(451, 40);
    this.pnlRight.MinimumSize = new Size(280, 0);
    this.pnlRight.Name = "pnlRight";
    this.pnlRight.Size = new Size(283, 371);
    this.pnlRight.TabIndex = 13;
    this.pnlCommonProperties.Controls.Add((Control) this.label1);
    this.pnlCommonProperties.Controls.Add((Control) this.pnlSelectObj);
    this.pnlCommonProperties.Controls.Add((Control) this.chbSkipRelationCreateErrs);
    this.pnlCommonProperties.Controls.Add((Control) this.chbSkipObjectCreateErrs);
    this.pnlCommonProperties.Controls.Add((Control) this.chbSkipFirstRow);
    this.pnlCommonProperties.Dock = DockStyle.Fill;
    this.pnlCommonProperties.Location = new Point(0, 226);
    this.pnlCommonProperties.Name = "pnlCommonProperties";
    this.pnlCommonProperties.Size = new Size(283, 145);
    this.pnlCommonProperties.TabIndex = 14;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(9, 89);
    this.label1.Name = "label1";
    this.label1.Size = new Size(250, 13);
    this.label1.TabIndex = 7;
    this.label1.Text = "Головной объект для импортируемых объектов";
    this.pnlSelectObj.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.pnlSelectObj.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.pnlSelectObj.Controls.Add((Control) this.tbParentObj);
    this.pnlSelectObj.Controls.Add((Control) this.btnSelectObject);
    this.pnlSelectObj.Controls.Add((Control) this.btnClearSelected);
    this.pnlSelectObj.Controls.Add((Control) this.btnCard);
    this.pnlSelectObj.Location = new Point(9, 105);
    this.pnlSelectObj.Name = "pnlSelectObj";
    this.pnlSelectObj.Size = new Size(262, 22);
    this.pnlSelectObj.TabIndex = 6;
    this.tbParentObj.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tbParentObj.Location = new Point(0, 1);
    this.tbParentObj.Margin = new Padding(0);
    this.tbParentObj.Name = "tbParentObj";
    this.tbParentObj.Size = new Size(200, 20);
    this.tbParentObj.TabIndex = 9;
    this.tbParentObj.KeyDown += new KeyEventHandler(this.tbParentObj_KeyDown);
    this.btnSelectObject.Dock = DockStyle.Right;
    this.btnSelectObject.Image = (Image) componentResourceManager.GetObject("btnSelectObject.Image");
    this.btnSelectObject.Location = new Point(202, 0);
    this.btnSelectObject.Margin = new Padding(2, 0, 0, 0);
    this.btnSelectObject.Name = "btnSelectObject";
    this.btnSelectObject.Size = new Size(20, 22);
    this.btnSelectObject.TabIndex = 10;
    this.btnSelectObject.UseVisualStyleBackColor = true;
    this.btnSelectObject.Click += new EventHandler(this.btnSelectObject_Click);
    this.btnClearSelected.Dock = DockStyle.Right;
    this.btnClearSelected.Enabled = false;
    this.btnClearSelected.Image = (Image) componentResourceManager.GetObject("btnClearSelected.Image");
    this.btnClearSelected.Location = new Point(222, 0);
    this.btnClearSelected.Margin = new Padding(2, 0, 0, 0);
    this.btnClearSelected.Name = "btnClearSelected";
    this.btnClearSelected.Size = new Size(20, 22);
    this.btnClearSelected.TabIndex = 11;
    this.btnClearSelected.UseVisualStyleBackColor = true;
    this.btnClearSelected.Click += new EventHandler(this.btnClearSelected_Click);
    this.btnCard.Dock = DockStyle.Right;
    this.btnCard.Enabled = false;
    this.btnCard.Image = (Image) componentResourceManager.GetObject("btnCard.Image");
    this.btnCard.Location = new Point(242, 0);
    this.btnCard.Margin = new Padding(2, 0, 3, 0);
    this.btnCard.Name = "btnCard";
    this.btnCard.Size = new Size(20, 22);
    this.btnCard.TabIndex = 12;
    this.btnCard.UseVisualStyleBackColor = true;
    this.btnCard.Click += new EventHandler(this.btnCard_Click);
    this.chbSkipRelationCreateErrs.AutoSize = true;
    this.chbSkipRelationCreateErrs.Location = new Point(9, 63 /*0x3F*/);
    this.chbSkipRelationCreateErrs.Name = "chbSkipRelationCreateErrs";
    this.chbSkipRelationCreateErrs.Size = new Size(222, 17);
    this.chbSkipRelationCreateErrs.TabIndex = 3;
    this.chbSkipRelationCreateErrs.Text = "Игнорировать ошибки наличия связей";
    this.chbSkipRelationCreateErrs.UseVisualStyleBackColor = true;
    this.chbSkipObjectCreateErrs.AutoSize = true;
    this.chbSkipObjectCreateErrs.Location = new Point(9, 40);
    this.chbSkipObjectCreateErrs.Name = "chbSkipObjectCreateErrs";
    this.chbSkipObjectCreateErrs.Size = new Size(234, 17);
    this.chbSkipObjectCreateErrs.TabIndex = 2;
    this.chbSkipObjectCreateErrs.Text = "Игнорировать ошибки наличия объектов";
    this.chbSkipObjectCreateErrs.UseVisualStyleBackColor = true;
    this.chbSkipFirstRow.AutoSize = true;
    this.chbSkipFirstRow.Location = new Point(9, 17);
    this.chbSkipFirstRow.Name = "chbSkipFirstRow";
    this.chbSkipFirstRow.Size = new Size(250, 17);
    this.chbSkipFirstRow.TabIndex = 0;
    this.chbSkipFirstRow.Text = "Первая строка содержит названия колонок";
    this.chbSkipFirstRow.UseVisualStyleBackColor = true;
    this.chbSkipFirstRow.CheckedChanged += new EventHandler(this.chbSkipFirstRow_CheckedChanged);
    this.splitter2.Dock = DockStyle.Top;
    this.splitter2.Location = new Point(0, 223);
    this.splitter2.Name = "splitter2";
    this.splitter2.Size = new Size(283, 3);
    this.splitter2.TabIndex = 13;
    this.splitter2.TabStop = false;
    this.attributePropertyGrid.Dock = DockStyle.Top;
    this.attributePropertyGrid.LineColor = SystemColors.ControlDark;
    this.attributePropertyGrid.Location = new Point(0, 0);
    this.attributePropertyGrid.Name = "attributePropertyGrid";
    this.attributePropertyGrid.Size = new Size(283, 223);
    this.attributePropertyGrid.TabIndex = 11;
    this.attributePropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.attributePropertyGrid_PropertyValueChanged);
    this.splitter1.Dock = DockStyle.Right;
    this.splitter1.Location = new Point(448, 40);
    this.splitter1.Name = "splitter1";
    this.splitter1.Size = new Size(3, 371);
    this.splitter1.TabIndex = 14;
    this.splitter1.TabStop = false;
    this.ofdConfiguration.Filter = "Configuration files|*.impcfg|All files|*.*";
    this.ofdConfiguration.RestoreDirectory = true;
    this.sfdConfiguration.Filter = "Configuration files|*.impcfg|All files|*.*";
    this.sfdConfiguration.RestoreDirectory = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(734, 461);
    this.Controls.Add((Control) this.pnlLeft);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.pnlRight);
    this.Controls.Add((Control) this.tstrpLvSettingItem);
    this.Controls.Add((Control) this.panel2);
    this.MinimumSize = new Size(475, 500);
    this.Name = nameof (AdvancedImportSettingsFrm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Настройки импорта";
    this.FormClosing += new FormClosingEventHandler(this.AdvancedImportSettingsFrm_FormClosing);
    this.cmLvSettingItem.ResumeLayout(false);
    this.tstrpLvSettingItem.ResumeLayout(false);
    this.tstrpLvSettingItem.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.pnlLeft.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView).EndInit();
    this.pnlRight.ResumeLayout(false);
    this.pnlCommonProperties.ResumeLayout(false);
    this.pnlCommonProperties.PerformLayout();
    this.pnlSelectObj.ResumeLayout(false);
    this.pnlSelectObj.PerformLayout();
    this.ResumeLayout(false);
  }
}
