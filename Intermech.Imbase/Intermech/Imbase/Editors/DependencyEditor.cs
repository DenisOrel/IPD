// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.DependencyEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class DependencyEditor : Form
{
  private AttributeTypeProperties _masterAtp;
  private AttributeTypeProperties _depAtp;
  private List<Tuple<object, object>> _result;
  private List<CheckValueItem> _checkItems;
  private int _currentPosition;
  private CurrencyManager _currencyManager;
  private DataTable _condsMap;
  private string _clipName;
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Button btCancel;
  private Button btOk;
  private DataGridView _gridMaster;
  private DataGridViewTextBoxColumn Column1;
  private DataGridViewTextBoxColumn Column2;
  private Button btClear;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
  private Label label1;
  private Label label2;
  private DataGridView _gridDest;
  private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn Column3;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem mnSelectAll;
  private ToolStripMenuItem mnInvert;
  private ToolStripMenuItem mnClear;
  private ToolStripMenuItem mnCopy;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripMenuItem mnPaste;
  private TextBox _edFilterData;
  private ComboBox _cbCondition;
  private Button btBuildFilter;
  private ToolTip errorToolTip;
  private ToolTip toolTip;

  internal static bool EditDependency(
    ref AttributeTypeProperties masterAtp,
    ref AttributeTypeProperties depAtp,
    ref List<Tuple<object, object>> result)
  {
    using (DependencyEditor dependencyEditor = new DependencyEditor())
    {
      dependencyEditor.SetData(ref masterAtp, ref depAtp, ref result);
      return dependencyEditor.ShowDialog() == DialogResult.OK;
    }
  }

  public DependencyEditor()
  {
    this.InitializeComponent();
    this.CreateCondsMap();
  }

  private void CreateCondsMap()
  {
    this._condsMap = new DataTable();
    this._condsMap.Columns.Add(new DataColumn("F_COND", typeof (Condition)));
    this._condsMap.Columns.Add(new DataColumn("F_DESCR", typeof (string)));
    ConditionHelper.FillConditionsMap(this._condsMap);
    this._cbCondition.DataSource = (object) this._condsMap;
    this._cbCondition.DisplayMember = "F_DESCR";
    this._cbCondition.ValueMember = "F_COND";
    if (!(ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service))
      return;
    this.btBuildFilter.ImageList = service.ImageList;
    this.btBuildFilter.ImageIndex = service.ImageIndex("imgFilter");
  }

  private void SetData(
    ref AttributeTypeProperties masterAtp,
    ref AttributeTypeProperties depAtp,
    ref List<Tuple<object, object>> result)
  {
    if (result == null)
      result = new List<Tuple<object, object>>();
    this._result = result;
    this._result.Sort();
    this._depAtp = depAtp;
    this._masterAtp = masterAtp;
    IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(this._masterAtp.AttributeID);
    List<object> possibleValues1 = attributeType1.PossibleValues;
    List<object> valuesDescriptions = attributeType1.PossibleValuesDescriptions;
    if (attributeType1.FieldType == FieldTypes.ftObjectLink)
    {
      this.PatchObjectRefLists(ref possibleValues1, ref valuesDescriptions);
      this._gridMaster.Columns[0].Visible = false;
    }
    this.label1.Text = string.Format(this.label1.Text, (object) attributeType1.Name);
    int count1 = possibleValues1.Count;
    List<Tuple<object, string>> tupleList = new List<Tuple<object, string>>(count1);
    for (int index = 0; index < count1; ++index)
    {
      string empty = string.Empty;
      if (valuesDescriptions != null && valuesDescriptions[index] != null)
        empty = valuesDescriptions[index].ToString();
      tupleList.Add(new Tuple<object, string>(possibleValues1[index], empty));
    }
    this._gridMaster.DataSource = (object) tupleList;
    this._currencyManager = this._gridMaster.BindingContext[this._gridMaster.DataSource] as CurrencyManager;
    this._currencyManager.CurrentChanged += new EventHandler(this.Cm_CurrentChanged);
    IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(this._depAtp.AttributeID);
    List<object> possibleValues2 = attributeType2.PossibleValues;
    valuesDescriptions = attributeType2.PossibleValuesDescriptions;
    if (attributeType2.FieldType == FieldTypes.ftObjectLink)
    {
      this.PatchObjectRefLists(ref possibleValues2, ref valuesDescriptions);
      this._gridDest.Columns[1].Visible = false;
    }
    this.label2.Text = string.Format(this.label2.Text, (object) attributeType2.Name);
    this._clipName = "$DEP_" + attributeType2.AttributeID.ToString();
    AutoCompleteStringCollection stringCollection = new AutoCompleteStringCollection();
    this._checkItems = new List<CheckValueItem>();
    if (possibleValues2 != null)
    {
      int count2 = possibleValues2.Count;
      for (int index = 0; index < count2; ++index)
      {
        string empty = string.Empty;
        if (valuesDescriptions != null && valuesDescriptions[index] != null)
          empty = valuesDescriptions[index].ToString();
        this._checkItems.Add(new CheckValueItem(possibleValues2[index], (object) empty));
        if (possibleValues2[index] != null)
        {
          string str = possibleValues2[index].ToString();
          if (!string.IsNullOrEmpty(str))
            stringCollection.Add(str);
        }
      }
      this._gridDest.DataSource = (object) this._checkItems;
    }
    this._edFilterData.AutoCompleteMode = AutoCompleteMode.Suggest;
    this._edFilterData.AutoCompleteCustomSource = stringCollection;
    this._edFilterData.AutoCompleteSource = AutoCompleteSource.CustomSource;
    this.PositionChanged(0);
  }

  private void PatchObjectRefLists(ref List<object> values, ref List<object> descs)
  {
    values = new List<object>((IEnumerable<object>) values);
    descs = new List<object>((IEnumerable<object>) descs);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      int count = values.Count;
      for (int index = 0; index < count; ++index)
      {
        if (values[index] != null && !DBNull.Value.Equals(values[index]))
        {
          long int64 = Convert.ToInt64(values[index]);
          QuickObjectInfo objectInfo = session.GetObjectInfo(int64);
          if (!objectInfo.Empty)
          {
            values[index] = (object) objectInfo.VersionGuid.ToString();
            if (string.IsNullOrEmpty(Convert.ToString(descs[index])))
              descs[index] = (object) objectInfo.Caption;
          }
        }
      }
    }
  }

  private void PositionChanged(int newPosition)
  {
    this._gridDest.EndEdit();
    if (this._checkItems == null)
      return;
    int count = this._checkItems.Count;
    if (this._currentPosition != newPosition)
    {
      Tuple<object, string> tuple1 = this._currencyManager.List[this._currentPosition] as Tuple<object, string>;
      for (int index1 = 0; index1 < count; ++index1)
      {
        CheckValueItem checkItem = this._checkItems[index1];
        Tuple<object, object> tuple2 = new Tuple<object, object>(tuple1.Item1, checkItem.Value);
        int index2 = this._result.BinarySearch(tuple2);
        if (checkItem.Checked && index2 < 0)
          this._result.Insert(~index2, tuple2);
        else if (!checkItem.Checked && index2 >= 0)
          this._result.RemoveAt(index2);
      }
    }
    if (newPosition == -1)
      return;
    this._currentPosition = newPosition;
    Tuple<object, string> tuple = this._currencyManager.List[this._currentPosition] as Tuple<object, string>;
    for (int index = 0; index < count; ++index)
    {
      CheckValueItem checkItem = this._checkItems[index];
      int num = this._result.BinarySearch(new Tuple<object, object>(tuple.Item1, checkItem.Value));
      checkItem.Checked = num >= 0;
    }
    this._gridDest.Invalidate();
  }

  private void Cm_CurrentChanged(object sender, EventArgs e)
  {
    this.PositionChanged(this._currencyManager.Position);
  }

  private void DependencyEditor_FormClosing(object sender, FormClosingEventArgs e)
  {
    this.PositionChanged(-1);
  }

  private void OnSelectionMenuClick(object sender, EventArgs e)
  {
    this._gridDest.EndEdit();
    if (sender is ToolStripMenuItem toolStripMenuItem)
    {
      int int32 = Convert.ToInt32(toolStripMenuItem.Tag);
      foreach (CheckValueItem checkItem in this._checkItems)
      {
        if (checkItem.Visible)
        {
          switch (int32)
          {
            case 0:
              checkItem.Checked = true;
              continue;
            case 1:
              checkItem.Checked = !checkItem.Checked;
              continue;
            case 2:
              checkItem.Checked = false;
              continue;
            default:
              continue;
          }
        }
      }
    }
    this._gridDest.Invalidate();
  }

  private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
  {
    this.mnPaste.Enabled = Clipboard.ContainsData(this._clipName);
  }

  private void MnCopy_Click(object sender, EventArgs e)
  {
    Clipboard.SetData(this._clipName, this.CreateListClone(this._checkItems));
  }

  private void MnPaste_Click(object sender, EventArgs e)
  {
    this._gridDest.CancelEdit();
    if (!Clipboard.ContainsData(this._clipName) || !(Clipboard.GetData(this._clipName) is List<CheckValueItem> data))
      return;
    List<CheckValueItem> checkValueItemList = new List<CheckValueItem>();
    foreach (CheckValueItem checkValueItem in data)
    {
      foreach (CheckValueItem checkItem in this._checkItems)
      {
        if (object.Equals(checkItem.Value, checkValueItem.Value) && !checkItem.Visible && checkValueItem.Checked)
          checkValueItemList.Add(checkValueItem);
      }
    }
    if (checkValueItemList.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder(1024 /*0x0400*/);
      string newLine = Environment.NewLine;
      foreach (CheckValueItem checkValueItem in checkValueItemList)
      {
        stringBuilder.Append(newLine);
        stringBuilder.Append($"{checkValueItem.Value.ToString()} \"{checkValueItem.Name}\"");
      }
      if (MessageBox.Show($"{$"При вставке обнаружены отмеченные элементы, не удовлетворяющие условию фильтра:{newLine}{stringBuilder.ToString()}{newLine}"}{newLine}Очистить фильтр ?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      this._checkItems.ForEach((Action<CheckValueItem>) (x => x.Visible = true));
      this.ApplyVisibleRows();
    }
    foreach (CheckValueItem checkValueItem in data)
    {
      foreach (CheckValueItem checkItem in this._checkItems)
      {
        if (object.Equals(checkItem.Value, checkValueItem.Value))
          checkItem.Checked = checkValueItem.Checked;
      }
    }
    this._gridDest.Invalidate();
  }

  private void ApplyVisibleRows()
  {
    bool flag = false;
    this._gridDest.CurrentCell = (DataGridViewCell) null;
    foreach (DataGridViewRow row in (IEnumerable) this._gridDest.Rows)
    {
      if (row.DataBoundItem is CheckValueItem dataBoundItem)
      {
        row.Visible = dataBoundItem.Visible;
        if (!dataBoundItem.Visible)
          flag = true;
      }
    }
    if (flag)
      this.btBuildFilter.FlatAppearance.BorderSize = 2;
    else
      this.btBuildFilter.FlatAppearance.BorderSize = 0;
  }

  private object CreateListClone(List<CheckValueItem> checkItems)
  {
    this._gridDest.EndEdit();
    return (object) checkItems.Select<CheckValueItem, CheckValueItem>((System.Func<CheckValueItem, CheckValueItem>) (x => x.Clone() as CheckValueItem)).ToList<CheckValueItem>();
  }

  private void OnClear_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show("Вы действительно хотите очистить зависимости ?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
      return;
    this._result.Clear();
    this._checkItems.ForEach((Action<CheckValueItem>) (x => x.Checked = false));
    this._gridDest.Invalidate();
  }

  private void OnBuildFilter_Click(object sender, EventArgs e)
  {
    bool flag = false;
    try
    {
      string text = this._edFilterData.Text;
      Condition condition = (Condition) this._cbCondition.SelectedValue;
      if (this.btBuildFilter.FlatAppearance.BorderSize > 0)
        condition = Condition.None;
      else if (string.IsNullOrWhiteSpace(text))
        return;
      DependencyEditor.EvalHandlerDelegate evalHandlerDelegate = (DependencyEditor.EvalHandlerDelegate) null;
      switch (condition)
      {
        case Condition.None:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.NoneEval);
          break;
        case Condition.Equal:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.EqualEval);
          break;
        case Condition.NotEqual:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.NotEqualEval);
          break;
        case Condition.Substring:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.SubstringEval);
          break;
        case Condition.Greater:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.GreaterEval);
          break;
        case Condition.GreaterOrEqual:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.GreaterOrEqualEval);
          break;
        case Condition.Less:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.LessEval);
          break;
        case Condition.LessOrEqual:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.LessOrEqualEval);
          break;
        case Condition.Between:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.BetweenEval);
          break;
        case Condition.NotBetween:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.NotBetweenEval);
          break;
        case Condition.InList:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.InListEval);
          break;
        case Condition.NotInList:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.NotInListEval);
          break;
        case Condition.NotSubstring:
          evalHandlerDelegate = new DependencyEditor.EvalHandlerDelegate(Evaluator.NotSubstringEval);
          break;
      }
      List<CheckValueItem> checkValueItemList = new List<CheckValueItem>();
      foreach (CheckValueItem checkItem in this._checkItems)
      {
        checkItem.Visible = evalHandlerDelegate(checkItem.Value, text);
        if (!checkItem.Visible && checkItem.Checked)
          checkValueItemList.Add(checkItem);
      }
      if (checkValueItemList.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder(1024 /*0x0400*/);
        string newLine = Environment.NewLine;
        foreach (CheckValueItem checkValueItem in checkValueItemList)
        {
          stringBuilder.Append(newLine);
          stringBuilder.Append($"{checkValueItem.Value.ToString()} \"{checkValueItem.Name}\"");
        }
        if (MessageBox.Show($"{$"При применении фильтра с допустимых значений, не удовлетворяющим условию, будут сняты отметки:{newLine}{stringBuilder.ToString()}{newLine}"}{newLine}Продолжить ?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        foreach (CheckValueItem checkValueItem in checkValueItemList)
          checkValueItem.Checked = false;
      }
    }
    catch (Exception ex)
    {
      flag = true;
      this.errorToolTip.Show(ex.Message, (IWin32Window) this._edFilterData, 0, -this._edFilterData.Height * 2, 1500);
    }
    if (flag)
      this._edFilterData.ForeColor = Color.Red;
    else
      this._edFilterData.ForeColor = SystemColors.ControlText;
    this.ApplyVisibleRows();
    this._gridDest.Invalidate();
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
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this._gridDest = new DataGridView();
    this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.Column3 = new DataGridViewTextBoxColumn();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.mnSelectAll = new ToolStripMenuItem();
    this.mnInvert = new ToolStripMenuItem();
    this.mnClear = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.mnCopy = new ToolStripMenuItem();
    this.mnPaste = new ToolStripMenuItem();
    this._gridMaster = new DataGridView();
    this.Column1 = new DataGridViewTextBoxColumn();
    this.Column2 = new DataGridViewTextBoxColumn();
    this.label1 = new Label();
    this.label2 = new Label();
    this.btCancel = new Button();
    this.btOk = new Button();
    this.btClear = new Button();
    this._edFilterData = new TextBox();
    this._cbCondition = new ComboBox();
    this.btBuildFilter = new Button();
    this.errorToolTip = new ToolTip(this.components);
    this.toolTip = new ToolTip(this.components);
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
    this.tableLayoutPanel1.SuspendLayout();
    ((ISupportInitialize) this._gridDest).BeginInit();
    this.contextMenuStrip1.SuspendLayout();
    ((ISupportInitialize) this._gridMaster).BeginInit();
    this.SuspendLayout();
    this.tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tableLayoutPanel1.ColumnCount = 2;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel1.Controls.Add((Control) this._gridDest, 1, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this._gridMaster, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.label1, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.label2, 1, 0);
    this.tableLayoutPanel1.Location = new Point(12, 12);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(782, 365);
    this.tableLayoutPanel1.TabIndex = 0;
    this._gridDest.AllowUserToAddRows = false;
    this._gridDest.AllowUserToDeleteRows = false;
    this._gridDest.AllowUserToResizeRows = false;
    this._gridDest.BackgroundColor = SystemColors.Control;
    this._gridDest.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._gridDest.Columns.AddRange((DataGridViewColumn) this.dataGridViewCheckBoxColumn1, (DataGridViewColumn) this.dataGridViewTextBoxColumn1, (DataGridViewColumn) this.Column3);
    this._gridDest.ContextMenuStrip = this.contextMenuStrip1;
    this._gridDest.Dock = DockStyle.Fill;
    this._gridDest.EditMode = DataGridViewEditMode.EditOnEnter;
    this._gridDest.Location = new Point(394, 23);
    this._gridDest.MultiSelect = false;
    this._gridDest.Name = "_gridDest";
    this._gridDest.RowHeadersVisible = false;
    this._gridDest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this._gridDest.Size = new Size(385, 339);
    this._gridDest.TabIndex = 8;
    this.dataGridViewCheckBoxColumn1.DataPropertyName = "Checked";
    this.dataGridViewCheckBoxColumn1.HeaderText = "";
    this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
    this.dataGridViewCheckBoxColumn1.Resizable = DataGridViewTriState.True;
    this.dataGridViewCheckBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
    this.dataGridViewCheckBoxColumn1.Width = 32 /*0x20*/;
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
    this.dataGridViewTextBoxColumn1.DataPropertyName = "Value";
    this.dataGridViewTextBoxColumn1.HeaderText = "Значение";
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn1.Width = 80 /*0x50*/;
    this.Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.Column3.DataPropertyName = "Name";
    this.Column3.HeaderText = "Описание";
    this.Column3.Name = "Column3";
    this.Column3.ReadOnly = true;
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.mnSelectAll,
      (ToolStripItem) this.mnInvert,
      (ToolStripItem) this.mnClear,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.mnCopy,
      (ToolStripItem) this.mnPaste
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size(191, 120);
    this.contextMenuStrip1.Opening += new CancelEventHandler(this.ContextMenuStrip1_Opening);
    this.mnSelectAll.Name = "mnSelectAll";
    this.mnSelectAll.ShortcutKeys = Keys.A | Keys.Control;
    this.mnSelectAll.Size = new Size(190, 22);
    this.mnSelectAll.Tag = (object) "0";
    this.mnSelectAll.Text = "Выделить все";
    this.mnSelectAll.Click += new EventHandler(this.OnSelectionMenuClick);
    this.mnInvert.Name = "mnInvert";
    this.mnInvert.Size = new Size(190, 22);
    this.mnInvert.Tag = (object) "1";
    this.mnInvert.Text = "Инвертировать";
    this.mnInvert.Click += new EventHandler(this.OnSelectionMenuClick);
    this.mnClear.Name = "mnClear";
    this.mnClear.Size = new Size(190, 22);
    this.mnClear.Tag = (object) "2";
    this.mnClear.Text = "Снять отметки";
    this.mnClear.Click += new EventHandler(this.OnSelectionMenuClick);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(187, 6);
    this.mnCopy.Name = "mnCopy";
    this.mnCopy.ShortcutKeys = Keys.C | Keys.Control;
    this.mnCopy.Size = new Size(190, 22);
    this.mnCopy.Text = "Копировать";
    this.mnCopy.Click += new EventHandler(this.MnCopy_Click);
    this.mnPaste.Name = "mnPaste";
    this.mnPaste.ShortcutKeys = Keys.V | Keys.Control;
    this.mnPaste.Size = new Size(190, 22);
    this.mnPaste.Text = "Вставить";
    this.mnPaste.Click += new EventHandler(this.MnPaste_Click);
    this._gridMaster.AllowUserToAddRows = false;
    this._gridMaster.AllowUserToDeleteRows = false;
    this._gridMaster.AllowUserToResizeRows = false;
    this._gridMaster.BackgroundColor = SystemColors.Control;
    this._gridMaster.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._gridMaster.Columns.AddRange((DataGridViewColumn) this.Column1, (DataGridViewColumn) this.Column2);
    this._gridMaster.Dock = DockStyle.Fill;
    this._gridMaster.Location = new Point(3, 23);
    this._gridMaster.Name = "_gridMaster";
    this._gridMaster.ReadOnly = true;
    this._gridMaster.RowHeadersVisible = false;
    this._gridMaster.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this._gridMaster.Size = new Size(385, 339);
    this._gridMaster.TabIndex = 7;
    this.Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
    this.Column1.DataPropertyName = "Item1";
    this.Column1.HeaderText = "Значение";
    this.Column1.Name = "Column1";
    this.Column1.ReadOnly = true;
    this.Column1.Width = 80 /*0x50*/;
    this.Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.Column2.DataPropertyName = "Item2";
    this.Column2.HeaderText = "Описание";
    this.Column2.Name = "Column2";
    this.Column2.ReadOnly = true;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(3, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(129, 13);
    this.label1.TabIndex = 2;
    this.label1.Text = "Основной атрибут: \"{0}\"";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(394, 0);
    this.label2.Name = "label2";
    this.label2.Size = new Size(138, 13);
    this.label2.TabIndex = 3;
    this.label2.Text = "Зависимый атрибут: \"{0}\"";
    this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Location = new Point(716, 410);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(75, 23);
    this.btCancel.TabIndex = 2;
    this.btCancel.Text = "Отмена";
    this.btCancel.UseVisualStyleBackColor = true;
    this.btOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOk.DialogResult = DialogResult.OK;
    this.btOk.Location = new Point(716, 381);
    this.btOk.Name = "btOk";
    this.btOk.Size = new Size(75, 23);
    this.btOk.TabIndex = 1;
    this.btOk.Text = "OK";
    this.btOk.UseVisualStyleBackColor = true;
    this.btClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btClear.Location = new Point(15, 410);
    this.btClear.Name = "btClear";
    this.btClear.Size = new Size(143, 23);
    this.btClear.TabIndex = 6;
    this.btClear.Text = "Очистить зависимость";
    this.btClear.UseVisualStyleBackColor = true;
    this.btClear.Click += new EventHandler(this.OnClear_Click);
    this._edFilterData.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._edFilterData.BackColor = SystemColors.Window;
    this._edFilterData.ForeColor = SystemColors.ControlText;
    this._edFilterData.Location = new Point(505, 412);
    this._edFilterData.Name = "_edFilterData";
    this._edFilterData.Size = new Size(119, 20);
    this._edFilterData.TabIndex = 4;
    this._cbCondition.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._cbCondition.DropDownStyle = ComboBoxStyle.DropDownList;
    this._cbCondition.FormattingEnabled = true;
    this._cbCondition.Location = new Point(505, 383);
    this._cbCondition.Name = "_cbCondition";
    this._cbCondition.Size = new Size(119, 21);
    this._cbCondition.TabIndex = 3;
    this.btBuildFilter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btBuildFilter.FlatAppearance.BorderColor = Color.DeepSkyBlue;
    this.btBuildFilter.FlatAppearance.BorderSize = 0;
    this.btBuildFilter.FlatStyle = FlatStyle.Flat;
    this.btBuildFilter.Location = new Point(630, 410);
    this.btBuildFilter.Name = "btBuildFilter";
    this.btBuildFilter.Size = new Size(26, 23);
    this.btBuildFilter.TabIndex = 5;
    this.toolTip.SetToolTip((Control) this.btBuildFilter, "Задать фильтр");
    this.btBuildFilter.UseVisualStyleBackColor = true;
    this.btBuildFilter.Click += new EventHandler(this.OnBuildFilter_Click);
    this.errorToolTip.BackColor = Color.Bisque;
    this.errorToolTip.ToolTipIcon = ToolTipIcon.Error;
    this.errorToolTip.ToolTipTitle = "Ошибка вычисления фильтра";
    this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn2.DataPropertyName = "Item3";
    this.dataGridViewTextBoxColumn2.HeaderText = "Описание";
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.ReadOnly = true;
    this.dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
    this.dataGridViewTextBoxColumn3.DataPropertyName = "Item1";
    this.dataGridViewTextBoxColumn3.HeaderText = "Значение";
    this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
    this.dataGridViewTextBoxColumn3.ReadOnly = true;
    this.dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn4.DataPropertyName = "Item2";
    this.dataGridViewTextBoxColumn4.HeaderText = "Описание";
    this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
    this.dataGridViewTextBoxColumn4.ReadOnly = true;
    this.AcceptButton = (IButtonControl) this.btOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.ClientSize = new Size(806, 445);
    this.Controls.Add((Control) this.btBuildFilter);
    this.Controls.Add((Control) this._cbCondition);
    this.Controls.Add((Control) this._edFilterData);
    this.Controls.Add((Control) this.btClear);
    this.Controls.Add((Control) this.btOk);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.DoubleBuffered = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(481, 245);
    this.Name = nameof (DependencyEditor);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Редактор зависимостей";
    this.FormClosing += new FormClosingEventHandler(this.DependencyEditor_FormClosing);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    ((ISupportInitialize) this._gridDest).EndInit();
    this.contextMenuStrip1.ResumeLayout(false);
    ((ISupportInitialize) this._gridMaster).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  internal delegate bool EvalHandlerDelegate(object value, string check);
}
