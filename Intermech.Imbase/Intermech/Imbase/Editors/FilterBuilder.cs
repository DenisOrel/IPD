// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.FilterBuilder
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class FilterBuilder : Form
{
  private string _filter;
  private System.Collections.Generic.List<int> _substColumns;
  private IContainer components;
  private DataGridView dgvFilter;
  private Button btOk;
  private Button btCancel;
  private DataSet dataSet1;
  private DataTable conditions;
  private DataColumn dataColumn2;
  private DataColumn dataColumn3;
  private DataColumn dataColumn4;
  private DataTable condsMap;
  private DataColumn dataColumn1;
  private DataColumn dataColumn5;
  private CheckBox checkBox1;
  private DataGridViewTextBoxColumn F_NAME;
  private DataGridViewComboBoxColumn F_COND;
  private DataGridViewTextBoxColumn F_DATA;

  public FilterBuilder()
  {
    this._filter = string.Empty;
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 893);
  }

  private bool AddSelection => this.checkBox1.Checked;

  public string Filter => this._filter;

  public System.Collections.Generic.List<int> List
  {
    set => this._substColumns = value;
  }

  public static bool BuildConditionString(
    AttributeTypeProperties[] columns,
    ref string filter,
    out bool addSelection,
    System.Collections.Generic.List<int> substColumns)
  {
    addSelection = false;
    bool flag = false;
    using (FilterBuilder filterBuilder = new FilterBuilder())
    {
      filterBuilder.SetData(columns);
      filterBuilder.List = substColumns;
      flag = filterBuilder.ShowDialog() == DialogResult.OK;
      if (flag)
      {
        filter = filterBuilder.Filter;
        addSelection = filterBuilder.AddSelection;
        if (filter.Length == 0)
          flag = false;
      }
    }
    return flag;
  }

  private string BuildFilter() => string.Empty;

  private void SetData(AttributeTypeProperties[] columns)
  {
    DataTable table = this.dataSet1.Tables[0];
    table.Clear();
    this.FillConditionsMap();
    DataRowCollection rows = table.Rows;
    int length = columns.Length;
    for (int index = 0; index < length; ++index)
      rows.Add((object) columns[index], (object) Condition.None, (object) string.Empty);
  }

  private void FillConditionsMap() => ConditionHelper.FillConditionsMap(this.condsMap);

  private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
  {
    int num = (int) MessageBox.Show(e.Exception.Message);
  }

  private void BtOk_Click(object sender, EventArgs e) => this._filter = this.CreateFilterString();

  private string CreateFilterString()
  {
    DataRowCollection rows = this.conditions.Rows;
    int count = rows.Count;
    string filterString = string.Empty;
    for (int index = 0; index < count; ++index)
    {
      string str = this.BuildFilterString(rows[index]);
      if (str.Length > 0)
        filterString = filterString.Length <= 0 ? $"({str})" : $"{filterString} AND ({str})";
    }
    return filterString;
  }

  private string BuildFilterString(DataRow dataRow)
  {
    if (DBNull.Value.Equals(dataRow[1]))
      return string.Empty;
    Condition int32 = (Condition) Convert.ToInt32(dataRow[1]);
    AttributeTypeProperties attributeTypeProperties = (AttributeTypeProperties) dataRow[0];
    string data1 = Convert.ToString(dataRow[2]);
    if (int32 == Condition.None || string.IsNullOrEmpty(data1))
      return string.Empty;
    bool needQuote = this.NeedQuotes(attributeTypeProperties.FieldType);
    if ((attributeTypeProperties.MultiValueMode == MultiValueModes.MultiValues ? 1 : (attributeTypeProperties.MultiValueMode == MultiValueModes.MultiValuesFromList ? 1 : 0)) != 0)
      this._substColumns.Add(attributeTypeProperties.AttributeID);
    string str1 = string.Empty;
    string str2 = $"[{attributeTypeProperties.AttributeID.ToString()}]";
    switch (int32)
    {
      case Condition.Equal:
      case Condition.NotEqual:
        if (needQuote)
          data1 = this.ApplyQuotes(data1);
        if (needQuote)
        {
          if (data1.IndexOfAny(new char[4]
          {
            '_',
            '%',
            '*',
            '?'
          }) != -1)
          {
            string str3 = data1.Replace('?', '_').Replace('*', '%');
            return int32 == Condition.Equal ? $"{str2} LIKE {str3}" : $"{str2} NOT LIKE {str3}";
          }
        }
        string str4 = "=";
        if (int32 == Condition.NotEqual)
          str4 = "<>";
        return $"{str2}{str4}{data1}";
      case Condition.Substring:
        string data2 = $"%{data1.Replace("*", "[*]").Replace("%", "[%]")}%";
        if (needQuote)
          data2 = this.ApplyQuotes(data2);
        return $"{str2} LIKE {data2}";
      case Condition.Greater:
        str1 = ">";
        break;
      case Condition.GreaterOrEqual:
        str1 = ">=";
        break;
      case Condition.Less:
        str1 = "<";
        break;
      case Condition.LessOrEqual:
        str1 = "<=";
        break;
      case Condition.Between:
        string[] pair1 = this.GetPair(data1, needQuote);
        return string.Format("({0} >=  {1} AND {0} <=  {2})", (object) str2, (object) pair1[0], (object) pair1[1]);
      case Condition.NotBetween:
        string[] pair2 = this.GetPair(data1, needQuote);
        return string.Format("({0} < {1} OR {0} >  {2})", (object) str2, (object) pair2[0], (object) pair2[1]);
      case Condition.InList:
        return $"{str2} IN({this.BuildList(data1, needQuote)})";
      case Condition.NotInList:
        return $"{str2} NOT IN({this.BuildList(data1, needQuote)})";
    }
    if (needQuote)
      data1 = this.ApplyQuotes(data1);
    return str1.Length > 0 ? $"{str2}{str1}{data1}" : string.Empty;
  }

  private string BuildList(string data, bool needQuote)
  {
    string[] strArray = data.Split(new char[1]{ ';' }, StringSplitOptions.RemoveEmptyEntries);
    StringBuilder stringBuilder = new StringBuilder(data.Length);
    int length = strArray.Length;
    for (int index = 0; index < length; ++index)
    {
      string data1 = strArray[index];
      if (needQuote)
        data1 = this.ApplyQuotes(data1);
      stringBuilder.Append(data1);
      if (index != length - 1)
        stringBuilder.Append(',');
    }
    return stringBuilder.ToString();
  }

  private string[] GetPair(string data, bool needQuote)
  {
    string[] pair = new string[2];
    string[] strArray = data.Split(';');
    if (strArray.Length == 1)
    {
      pair[0] = data;
      pair[1] = data;
    }
    else
    {
      pair[0] = strArray[0];
      pair[1] = strArray[1];
    }
    if (needQuote)
    {
      pair[0] = this.ApplyQuotes(pair[0]);
      pair[1] = this.ApplyQuotes(pair[1]);
    }
    return pair;
  }

  private string ApplyQuotes(string data)
  {
    if (string.IsNullOrEmpty(data) || data[0] == '\'')
      return data;
    data = $"'{data}'";
    return data;
  }

  private bool NeedQuotes(FieldTypes fieldTypes)
  {
    return fieldTypes == FieldTypes.ftString || fieldTypes == FieldTypes.ftMemo || fieldTypes == FieldTypes.ftGuid;
  }

  private void OndgvFilter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
  {
    if (this.dgvFilter.Rows.Count == 0)
      return;
    switch (e.ColumnIndex)
    {
      case 1:
        if (!(this.dgvFilter.Rows[e.RowIndex].Cells["F_COND"].Value is DBNull))
          break;
        this.dgvFilter.Rows[e.RowIndex].Cells["F_DATA"].Value = (object) null;
        break;
      case 2:
        object obj1 = this.dgvFilter.Rows[e.RowIndex].Cells["F_DATA"].Value;
        object obj2 = this.dgvFilter.Rows[e.RowIndex].Cells["F_COND"].Value;
        Condition condition = obj2 is DBNull ? Condition.None : (Condition) obj2;
        if (obj1 is DBNull)
        {
          this.dgvFilter.Rows[e.RowIndex].Cells["F_COND"].Value = (object) Condition.None;
          break;
        }
        if (condition != Condition.None)
          break;
        this.dgvFilter.Rows[e.RowIndex].Cells["F_COND"].Value = (object) Condition.Equal;
        break;
    }
  }

  private void FilterBuilder_Shown(object sender, EventArgs e)
  {
    this.dgvFilter.AutoResizeColumn(0);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilterBuilder));
    this.dgvFilter = new DataGridView();
    this.dataSet1 = new DataSet();
    this.conditions = new DataTable();
    this.dataColumn2 = new DataColumn();
    this.dataColumn3 = new DataColumn();
    this.dataColumn4 = new DataColumn();
    this.condsMap = new DataTable();
    this.dataColumn1 = new DataColumn();
    this.dataColumn5 = new DataColumn();
    this.btOk = new Button();
    this.btCancel = new Button();
    this.checkBox1 = new CheckBox();
    this.F_NAME = new DataGridViewTextBoxColumn();
    this.F_COND = new DataGridViewComboBoxColumn();
    this.F_DATA = new DataGridViewTextBoxColumn();
    ((ISupportInitialize) this.dgvFilter).BeginInit();
    this.dataSet1.BeginInit();
    this.conditions.BeginInit();
    this.condsMap.BeginInit();
    this.SuspendLayout();
    this.dgvFilter.AllowUserToAddRows = false;
    this.dgvFilter.AllowUserToDeleteRows = false;
    this.dgvFilter.AllowUserToResizeRows = false;
    componentResourceManager.ApplyResources((object) this.dgvFilter, "dgvFilter");
    this.dgvFilter.AutoGenerateColumns = false;
    this.dgvFilter.BackgroundColor = SystemColors.Control;
    this.dgvFilter.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgvFilter.Columns.AddRange((DataGridViewColumn) this.F_NAME, (DataGridViewColumn) this.F_COND, (DataGridViewColumn) this.F_DATA);
    this.dgvFilter.DataMember = "Conditions";
    this.dgvFilter.DataSource = (object) this.dataSet1;
    this.dgvFilter.MultiSelect = false;
    this.dgvFilter.Name = "dgvFilter";
    this.dgvFilter.RowHeadersVisible = false;
    this.dgvFilter.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgvFilter.CellValueChanged += new DataGridViewCellEventHandler(this.OndgvFilter_CellValueChanged);
    this.dgvFilter.DataError += new DataGridViewDataErrorEventHandler(this.DataGridView1_DataError);
    this.dataSet1.DataSetName = "NewDataSet";
    this.dataSet1.Tables.AddRange(new DataTable[2]
    {
      this.conditions,
      this.condsMap
    });
    this.conditions.Columns.AddRange(new DataColumn[3]
    {
      this.dataColumn2,
      this.dataColumn3,
      this.dataColumn4
    });
    this.conditions.TableName = "Conditions";
    this.dataColumn2.ColumnName = "F_NAME";
    this.dataColumn2.DataType = typeof (object);
    this.dataColumn3.ColumnName = "F_COND";
    this.dataColumn3.DataType = typeof (object);
    this.dataColumn4.ColumnName = "F_DATA";
    this.condsMap.Columns.AddRange(new DataColumn[2]
    {
      this.dataColumn1,
      this.dataColumn5
    });
    this.condsMap.Constraints.AddRange(new Constraint[1]
    {
      (Constraint) new UniqueConstraint("Constraint1", new string[1]
      {
        "F_COND"
      }, true)
    });
    this.condsMap.PrimaryKey = new DataColumn[1]
    {
      this.dataColumn1
    };
    this.condsMap.TableName = "CondsMap";
    this.dataColumn1.AllowDBNull = false;
    this.dataColumn1.ColumnName = "F_COND";
    this.dataColumn1.DataType = typeof (object);
    this.dataColumn5.ColumnName = "F_NAME";
    componentResourceManager.ApplyResources((object) this.btOk, "btOk");
    this.btOk.DialogResult = DialogResult.OK;
    this.btOk.Name = "btOk";
    this.btOk.UseVisualStyleBackColor = true;
    this.btOk.Click += new EventHandler(this.BtOk_Click);
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    this.btCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.TabStop = false;
    this.checkBox1.UseVisualStyleBackColor = true;
    this.F_NAME.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
    this.F_NAME.DataPropertyName = "F_NAME";
    componentResourceManager.ApplyResources((object) this.F_NAME, "F_NAME");
    this.F_NAME.Name = "F_NAME";
    this.F_NAME.ReadOnly = true;
    this.F_COND.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
    this.F_COND.DataPropertyName = "F_COND";
    this.F_COND.DataSource = (object) this.dataSet1;
    this.F_COND.DisplayMember = "CondsMap.F_NAME";
    componentResourceManager.ApplyResources((object) this.F_COND, "F_COND");
    this.F_COND.MaxDropDownItems = 13;
    this.F_COND.Name = "F_COND";
    this.F_COND.Resizable = DataGridViewTriState.True;
    this.F_COND.SortMode = DataGridViewColumnSortMode.Automatic;
    this.F_COND.ValueMember = "CondsMap.F_COND";
    this.F_DATA.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.F_DATA.DataPropertyName = "F_DATA";
    componentResourceManager.ApplyResources((object) this.F_DATA, "F_DATA");
    this.F_DATA.Name = "F_DATA";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.checkBox1);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOk);
    this.Controls.Add((Control) this.dgvFilter);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FilterBuilder);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Shown += new EventHandler(this.FilterBuilder_Shown);
    ((ISupportInitialize) this.dgvFilter).EndInit();
    this.dataSet1.EndInit();
    this.conditions.EndInit();
    this.condsMap.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
