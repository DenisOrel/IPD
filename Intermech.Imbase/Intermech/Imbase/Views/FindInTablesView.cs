// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.FindInTablesView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Docking;
using Intermech.Imbase.Controls;
using Intermech.Imbase.Editors;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class FindInTablesView : DockControl, IImbaseView, ILoadDataAsync
{
  private long _targetId;
  private NavigatorTreeNode _parentINode;
  private TreeNode _parentTreeNode;
  private LocateNodeEventHandler _locateHandler;
  private Icon _ico;
  private DataTable _dtLinks;
  private Dictionary<string, ListViewItem> _AllAttrs = new Dictionary<string, ListViewItem>();
  private List<ConditionItem> _conditions = new List<ConditionItem>();
  private bool _condsLoaded;
  private string _btnNextText;
  private string _btnPrevText;
  private List<int> _substColumns = new List<int>(32 /*0x20*/);
  private Func<object> DataLoader;
  private int _index = -1;
  private CancellationTokenSource _cts;
  private IContainer components;
  private ContextMenuStrip contextMenu;
  private ToolStripMenuItem miSelectAll;
  private ToolStripMenuItem miClearAll;
  private ToolStripMenuItem miInvert;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private ImageList ilCheckedStates;
  private DataColumn dataColumn1;
  private Button _btnUpdate;
  private Button _btnStep;
  private ListView _lvTables;
  private ListView _lvAttrs;
  private DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
  private TextWithButtonColumn textWithButtonColumn1;
  private DataSet _ds;
  private DataTable conditions;
  private DataColumn dataColumn2;
  private DataColumn dataColumn3;
  private DataColumn dataColumn4;
  private DataTable condsMap;
  private DataColumn dataColumn5;
  private DataColumn dataColumn6;
  private Panel pnlFirst;
  private Panel pnlBottom;
  private TableLayoutPanel tlpBottom;
  private Panel pnlSecond;
  private DataGridView _dgvConditions;
  private Button _btnSearch;
  private ListView _lvResult;
  private ColumnHeader colName;
  private ColumnHeader colCounter;
  private TableLayoutPanel tlpSecond;
  private TableLayoutPanel tlpFirst;
  private ColumnHeader colTablesName;
  private ColumnHeader colAttrsName;
  private Panel _pnlProgress;
  private Label _lbTaskInfo;
  private ProgressBar _progress;
  private System.Windows.Forms.Timer _timer;
  private Panel _pnlScanInfo;
  private Label _lbCompleted;
  private DataGridViewTextBoxColumn F_NAME;
  private DataGridViewComboBoxColumn F_COND;
  private TextWithButtonColumn F_DATA;

  public Icon Icon
  {
    get
    {
      return this._ico ?? (this._ico = Intermech.Imbase.ResourceHelper.GetResourceData<Icon>(this.GetType().Assembly, "Intermech.Imbase.Resources.FindInTables.ico"));
    }
  }

  private List<ListViewItem> CheckedTables
  {
    get
    {
      List<ListViewItem> checkedTables = new List<ListViewItem>();
      foreach (ListViewItem listViewItem in this._lvTables.Items)
      {
        if (listViewItem.StateImageIndex != 0)
          checkedTables.Add(listViewItem);
      }
      return checkedTables;
    }
  }

  public override string HelpID => "1753";

  public FindInTablesView()
  {
    this.InitializeComponent();
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service)
    {
      this.TabImageIndex = service.ImageIndex("imgFindInTables");
      this._btnUpdate.Image = service.ImageList.Images[service.ImageIndex("imgRefresh")];
      this._btnSearch.Image = service.ImageList.Images[service.ImageIndex("imgSearch")];
    }
    this._btnNextText = LocalizationHolder.rm.GetString("Imbase_Wizards_Next");
    this._btnPrevText = LocalizationHolder.rm.GetString("Imbase_Wizards_Prev");
    this.pnlFirst.Dock = DockStyle.Fill;
    this._btnStep.Text = this._btnNextText;
    this._lvTables.Columns[0].Width = -2;
    this._lvAttrs.Columns[0].Width = -2;
    HelpProvidersClass.SetHelpOptionForControl((Control) this, this.HelpID);
  }

  public static void Show(object parentNode, bool modal, LocateNodeEventHandler locateHandler)
  {
    FindInTablesView view = new FindInTablesView();
    view.SetData(parentNode, locateHandler);
    if (modal)
    {
      ImbaseViewForm.FindOrCreateViewForm(ImbaseViewForm.FormType.FindInTables, (IImbaseView) view, view.Icon).Show();
    }
    else
    {
      if (!(ServicesManager.GetService(typeof (DockManager)) is DockManager service))
        return;
      view.Manager = service;
      view.Float();
      if (!(view.Parent?.Parent is Form))
        return;
      view.Parent.Parent.MinimumSize = new Size(view.MinimumSize.Width + 20, view.MinimumSize.Height + 40);
    }
  }

  private void OnBeforeFirstShown(object sender, EventArgs e)
  {
    this._timer.Tick += new EventHandler(this.On_timer_Tick);
    this._timer.Start();
  }

  private void On_btnSearch_Click(object sender, EventArgs e) => this.Search();

  private void On_btnStep_Click(object sender, EventArgs e)
  {
    if (this.pnlFirst.Visible)
    {
      DataTable table = this._ds.Tables[0];
      table.Clear();
      this.FillConditionsMap();
      DataRowCollection rows = table.Rows;
      string config = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadString("Imbase", "FindInTables", "T" + this._targetId.ToString(), string.Empty, DBConfigMode.UserOnly);
      if (!string.IsNullOrEmpty(config))
        this._conditions = ConditionHelper.StringToConds(config);
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      foreach (ListViewItem listViewItem in this._lvAttrs.Items)
      {
        if (listViewItem.ImageIndex == Convert.ToInt32((object) FindInTablesView.chbStates.Checked))
        {
          IDBAttributeTypeInfo attributeType = service.GetAttributeType(Convert.ToInt32(listViewItem.Name));
          if (attributeType != null)
          {
            DataRow dataRow = rows.Add((object) attributeType.PropertiesStructure, (object) Condition.None, (object) string.Empty);
            ConditionItem conditionItem = ConditionItem.Find(this._conditions, attributeType.AttributeID);
            if (conditionItem != null)
            {
              dataRow[1] = (object) conditionItem.Condition;
              dataRow[2] = conditionItem.StringData;
            }
          }
        }
      }
      this._lvResult.Items.Clear();
      this._btnSearch.Enabled = this._dgvConditions.Rows.Count > 0;
      this._btnStep.Text = this._btnPrevText;
      this.pnlFirst.Visible = false;
      this.pnlSecond.Visible = true;
      this.pnlSecond.Dock = DockStyle.Fill;
    }
    else
    {
      this._btnStep.Text = this._btnNextText;
      this.pnlFirst.Visible = true;
      this.pnlSecond.Visible = false;
      this.pnlFirst.Dock = DockStyle.Fill;
      this._cts?.Cancel();
      this._progress.Value = 0;
      this._lbTaskInfo.Text = string.Empty;
      this._pnlProgress.Visible = false;
    }
  }

  private void On_btnUpdate_Click(object sender, EventArgs e)
  {
    List<ListViewItem> checkedTables = this.CheckedTables;
    this._lvAttrs.BeginUpdate();
    try
    {
      this._lvAttrs.Items.Clear();
      if (checkedTables.Count > 0)
      {
        List<ListViewItem> allAttrs = new List<ListViewItem>();
        if (checkedTables[0].Tag is List<ListViewItem> first)
        {
          first.ForEach((Action<ListViewItem>) (x => allAttrs.Add(x)));
          for (int index = 1; index < checkedTables.Count; ++index)
          {
            if (checkedTables[index].Tag is List<ListViewItem> tag)
            {
              first = first.Intersect<ListViewItem>((IEnumerable<ListViewItem>) tag).ToList<ListViewItem>();
              allAttrs = tag.Union<ListViewItem>((IEnumerable<ListViewItem>) allAttrs).ToList<ListViewItem>();
            }
          }
          allAttrs.ForEach((Action<ListViewItem>) (x => x.ImageIndex = Convert.ToInt32((object) FindInTablesView.chbStates.Indeterminate)));
          first.ForEach((Action<ListViewItem>) (x => x.ImageIndex = Convert.ToInt32((object) FindInTablesView.chbStates.Checked)));
        }
        this._lvAttrs.Items.AddRange(allAttrs.ToArray());
      }
    }
    finally
    {
      this._lvAttrs.EndUpdate();
    }
    this.conditions.Rows.Clear();
    this._lvResult.BeginUpdate();
    this._lvResult.Items.Clear();
    this._lvResult.EndUpdate();
    this._btnUpdate.Enabled = this._btnSearch.Enabled = false;
    this._btnStep.Enabled = this._lvAttrs.Items.Count > sc_7959.ssp_imbase_7960(573653079);
  }

  private void On_dgvConditions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
  {
    if (this._dgvConditions.Rows.Count <= 0)
      return;
    this._btnSearch.Enabled = true;
    DataGridViewRow row = this._dgvConditions.Rows[e.RowIndex];
    switch (e.ColumnIndex)
    {
      case 1:
        if (!(row.Cells["F_COND"].Value is DBNull))
          break;
        row.Cells["F_DATA"].Value = (object) null;
        break;
      case 2:
        object obj1 = row.Cells["F_DATA"].Value;
        object obj2 = row.Cells["F_COND"].Value;
        Condition condition = obj2 is DBNull ? Condition.None : (Condition) obj2;
        switch (condition)
        {
          case Condition.Equal:
          case Condition.NotEqual:
            if (condition != Condition.None || obj1 is DBNull)
              return;
            string str = Convert.ToString(obj1);
            row.Cells["F_COND"].Value = (object) (Condition) (str.IndexOf(';') > -1 ? 10 : 1);
            return;
          default:
            if (obj1 is DBNull)
            {
              row.Cells["F_COND"].Value = (object) Condition.None;
              return;
            }
            goto case Condition.Equal;
        }
    }
  }

  private void On_lvAttrs_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Space || this._lvAttrs.SelectedItems.Count <= 0)
      return;
    this.AttrsCheckedChanged(this._lvAttrs.SelectedItems[0].Index);
  }

  private void On_lvAttrs_MouseDown(object sender, MouseEventArgs e)
  {
    if (this._lvAttrs.TopItem == null)
      return;
    double num = this._lvAttrs.Items.Count > 1 ? (double) (this._lvAttrs.Items[1].Position.Y - this._lvAttrs.Items[0].Position.Y) : 17.0;
    this._index = Convert.ToInt32(Math.Ceiling((double) e.Y / num) - 1.0 + (double) this._lvAttrs.TopItem.Index);
    this._index = this._index < 0 ? 0 : this._index;
  }

  private void On_lvAttrs_MouseUp(object sender, MouseEventArgs e)
  {
    if (this._index > -1 && this._index < this._lvAttrs.Items.Count)
    {
      this.AttrsCheckedChanged(this._index);
      if (this._lvAttrs.SelectedItems.Count > 0)
        this._lvAttrs.EnsureVisible(this._lvAttrs.SelectedItems[0].Index);
    }
    this._index = -1;
  }

  private void On_lvResult_ItemSelectionChanged(
    object sender,
    ListViewItemSelectionChangedEventArgs e)
  {
    SelectedRecords.Clear();
    SelectedRecords.Conditions = this._conditions;
    if (!(sender is ListView listView) || listView.SelectedItems.Count != 1)
      return;
    long int64 = Convert.ToInt64(listView.SelectedItems[0].Name);
    long[] tag = listView.SelectedItems[0].Tag as long[];
    SelectedRecords.Add(int64, tag);
    if (this._locateHandler != null)
      this._locateHandler((object) this, new LocateNodeEventArgs(int64, FindHelper.GetDataTable(int64)));
    else if (this._parentINode != null)
    {
      NavigatorTreeNode node = FindHelper.SearchNodeByNodeID(this._parentINode, int64);
      if (FindHelper.IsValidNode(node))
        node.Focus();
    }
    if (this._parentTreeNode == null)
      return;
    TreeNode treeNode = FindHelper.SearchNodeByNodeID(this._parentTreeNode, int64);
    if (treeNode == null)
      return;
    treeNode.EnsureVisible();
    treeNode.TreeView.SelectedNode = treeNode;
  }

  private void On_lvResult_SizeChanged(object sender, EventArgs e)
  {
    this._lvResult.Columns[1].Width = -2;
  }

  private void On_lvTables_ItemChecked(object sender, ItemCheckedEventArgs e)
  {
    if (e.Item == null || e.Item.Tag == null)
      return;
    this._btnUpdate.Enabled = true;
  }

  private void On_timer_Tick(object sender, EventArgs e)
  {
    this._timer.Stop();
    this._pnlProgress.Visible = true;
    this._cts = new CancellationTokenSource();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string asString = sessionKeeper.Session.GetObjectActualCopy(this._targetId, false)?.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId)?.AsString;
      if (string.IsNullOrEmpty(asString))
        return;
      this._dtLinks = this.GetTableIDs(sessionKeeper.Session, asString);
      if (this._dtLinks == null)
        return;
      ConcurrentBag<ListViewItem> listTables = new ConcurrentBag<ListViewItem>();
      string strCompleted = LocalizationHolder.rm.GetString("Imbase.Processed.Msg");
      int index = -1;
      double count = Convert.ToDouble(this._dtLinks.Rows.Count);
      this._lbTaskInfo.Text = LocalizationHolder.rm.GetString("Imbase_FindInTablesView_AnalyzeTableFileds");
      ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
      ParallelOptions po = new ParallelOptions()
      {
        CancellationToken = this._cts.Token,
        MaxDegreeOfParallelism = Environment.ProcessorCount
      };
      Task.Factory.StartNew((Action) (() => Parallel.ForEach<DataRow>((IEnumerable<DataRow>) this._dtLinks.AsEnumerable(), po, (Action<DataRow>) (r =>
      {
        try
        {
          po.CancellationToken.ThrowIfCancellationRequested();
          ListViewItem listViewItem = this.CreatelvTablesItem(Convert.ToInt64(r["TableID"]));
          if (listViewItem != null)
            listTables.Add(listViewItem);
          if (!this.InvokeRequired)
            return;
          this.Invoke((Delegate) (() =>
          {
            this._lbCompleted.Text = string.Format(strCompleted, (object) this._progress.Value);
            this._progress.Value = Convert.ToInt32(Math.Floor((double) ++index / count * 100.0));
            this._progress.Refresh();
          }));
        }
        catch (OperationCanceledException ex)
        {
        }
        catch (Exception ex)
        {
          exceptions.Enqueue(ex);
        }
      })))).ContinueWith<object>((System.Func<Task, object>) (t => this.Invoke((Delegate) (() =>
      {
        if (exceptions.Count > 0)
          throw new AggregateException((IEnumerable<Exception>) exceptions);
        if (listTables.Count > 0)
        {
          this._lvTables.BeginUpdate();
          this._lvTables.Items.AddRange(listTables.ToArray());
          this._lvTables.EndUpdate();
          this._btnUpdate.Enabled = true;
          this.On_btnUpdate_Click((object) null, new EventArgs());
        }
        this._pnlProgress.Visible = false;
      }))), this._cts.Token);
    }
  }

  private void FindInTablesView_Closing(object sender, CancelEventArgs e)
  {
    this.ViewClosing(sender, e);
  }

  private void OnMenuItem_Click(object sender, EventArgs e)
  {
    int num;
    switch (Convert.ToInt16(sender is ToolStripDropDownItem stripDropDownItem ? stripDropDownItem.Tag : (object) null))
    {
      case 0:
        num = 1;
        break;
      case 2:
        int int32_1 = Convert.ToInt32((object) FindInTablesView.chbStates.Checked);
        int int32_2 = Convert.ToInt32((object) FindInTablesView.chbStates.Unchecked);
        IEnumerator enumerator = this._lvTables.Items.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            ListViewItem current = (ListViewItem) enumerator.Current;
            current.StateImageIndex = current.StateImageIndex == int32_1 ? int32_2 : int32_1;
          }
          goto label_17;
        }
        finally
        {
          if (enumerator is IDisposable disposable)
            disposable.Dispose();
        }
      default:
        num = 0;
        break;
    }
    int int32_3 = Convert.ToInt32((object) (FindInTablesView.chbStates) num);
    foreach (ListViewItem listViewItem in this._lvTables.Items)
      listViewItem.StateImageIndex = int32_3;
label_17:
    this._btnUpdate.Enabled = true;
  }

  private void OnTextWithButtonColumn_ButtonClick(object sender, EventArgs e)
  {
    try
    {
      this.DataLoader = new Func<object>(this.LoadDataForSearch);
      DataTable dt;
      using (ProgressForm progressForm = new ProgressForm((ILoadDataAsync) this))
        dt = progressForm.ShowDialog((IWin32Window) this) != DialogResult.Cancel ? progressForm.Data as DataTable : throw new OperationCanceledException();
      if (dt == null)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Imbase_ElementList_Empty"), LocalizationHolder.rm.GetString("Imbase_FindInTablesView_DialogCaption"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        using (AttrValuesFromTables valuesFromTables = new AttrValuesFromTables(dt))
        {
          if (valuesFromTables.ShowDialog((IWin32Window) this) != DialogResult.OK || !(sender is DataGridViewTextBoxCell gridViewTextBoxCell))
            return;
          ((TextWithButtonCell) this._dgvConditions.Rows[gridViewTextBoxCell.RowIndex].Cells[gridViewTextBoxCell.ColumnIndex]).Value = this.ConvertListToString(valuesFromTables.SelectedValues);
        }
      }
    }
    catch (OperationCanceledException ex)
    {
    }
  }

  private string ApplyQuotes(string data)
  {
    return string.IsNullOrEmpty(data) || data[0] == '\'' ? data : $"'{data}'";
  }

  private void AttrsCheckedChanged(int itemIndex)
  {
    ListViewItem listViewItem = this._lvAttrs.Items[itemIndex];
    if (listViewItem.ImageIndex == 0)
      listViewItem.ImageIndex = 1;
    else if (listViewItem.ImageIndex == 1)
    {
      listViewItem.ImageIndex = 0;
    }
    else
    {
      listViewItem.ImageIndex = 1;
      this.CheckedTables.Except<ListViewItem>((IEnumerable<ListViewItem>) (listViewItem.Tag as List<ListViewItem>)).ToList<ListViewItem>().ForEach((Action<ListViewItem>) (x => x.StateImageIndex = Convert.ToInt32((object) FindInTablesView.chbStates.Unchecked)));
      this.On_btnUpdate_Click((object) null, new EventArgs());
    }
  }

  private string BuildFilterString(DataRow dataRow, List<ConditionItem> conds)
  {
    string str1 = string.Empty;
    if (dataRow[1] != DBNull.Value)
    {
      AttributeTypeProperties attributeTypeProperties = (AttributeTypeProperties) dataRow[0];
      Condition int32 = (Condition) Convert.ToInt32(dataRow[1]);
      string data1 = Convert.ToString(dataRow[2]);
      if (!string.IsNullOrEmpty(data1) || int32 == Condition.Equal || int32 == Condition.NotEqual)
      {
        Condition condition = int32 != Condition.None ? int32 : Condition.Equal;
        ConditionItem conditionItem = (ConditionItem) null;
        if (conds != null)
        {
          conditionItem = new ConditionItem()
          {
            AttId = attributeTypeProperties.AttributeID,
            Condition = condition,
            Data = data1
          };
          conds.Add(conditionItem);
        }
        if (attributeTypeProperties.MultiValueMode == MultiValueModes.MultiValues || attributeTypeProperties.MultiValueMode == MultiValueModes.MultiValuesFromList)
          this._substColumns.Add(attributeTypeProperties.AttributeID);
        if (attributeTypeProperties.FieldType == FieldTypes.ftDouble || attributeTypeProperties.FieldType == FieldTypes.ftMeasured)
          data1 = data1.Replace(',', '.');
        bool needQuote = this.NeedQuotes(attributeTypeProperties.FieldType);
        if (!needQuote)
          this.CheckChars(data1);
        string str2 = $"[{attributeTypeProperties.AttributeGuid}]";
        switch (condition)
        {
          case Condition.Equal:
            if (needQuote)
            {
              if (data1.IndexOfAny(new char[2]{ '*', '?' }) != -1)
              {
                string data2 = data1.Replace('?', '_').Replace('*', '%');
                str1 = $"{str2} LIKE {this.ApplyQuotes(data2)}";
                break;
              }
            }
            str1 = !string.IsNullOrEmpty(data1) ? $"{str2}={(needQuote ? this.ApplyQuotes(data1) : data1)}" : string.Format(needQuote ? "{0}='' OR {0} is NULL" : "{0} is NULL", (object) str2);
            break;
          case Condition.NotEqual:
            if (needQuote)
            {
              if (data1.IndexOfAny(new char[2]{ '*', '?' }) != -1)
              {
                string data3 = data1.Replace('?', '_').Replace('*', '%');
                str1 = $"{str2} NOT LIKE {this.ApplyQuotes(data3)}";
                break;
              }
            }
            str1 = !string.IsNullOrEmpty(data1) ? string.Format("{0}<>{1} OR {0} is NULL", (object) str2, needQuote ? (object) this.ApplyQuotes(data1) : (object) data1) : string.Format(needQuote ? "{0}<>'' AND {0} is not NULL" : "{0} is not NULL", (object) str2);
            break;
          case Condition.Substring:
            string data4 = $"%{data1.Replace("*", "[*]").Replace("%", "[%]")}%";
            str1 = $"{str2} LIKE {(needQuote ? this.ApplyQuotes(data4) : data4)}";
            break;
          case Condition.Between:
            string[] pair1 = this.GetPair(data1, needQuote);
            if (conditionItem != null)
            {
              conditionItem.Data = pair1[0];
              conditionItem.Data2 = pair1[1];
            }
            str1 = string.Format("({0} >= {1} AND {0} <= {2})", (object) str2, (object) pair1[0], (object) pair1[1]);
            break;
          case Condition.NotBetween:
            string[] pair2 = this.GetPair(data1, needQuote);
            if (conditionItem != null)
            {
              conditionItem.Data = pair2[0];
              conditionItem.Data2 = pair2[1];
            }
            str1 = string.Format("({0} < {1} OR {0} > {2})", (object) str2, (object) pair2[0], (object) pair2[1]);
            break;
          case Condition.InList:
            str1 = $"{str2} IN ({this.BuildList(data1, needQuote)})";
            break;
          case Condition.NotInList:
            str1 = $"{str2} NOT IN ({this.BuildList(data1, needQuote)})";
            break;
          default:
            if (needQuote)
              data1 = this.ApplyQuotes(data1);
            switch (condition)
            {
              case Condition.Greater:
                str1 = $"{str2} > {data1}";
                break;
              case Condition.GreaterOrEqual:
                str1 = $"{str2} >= {data1}";
                break;
              case Condition.Less:
                str1 = $"{str2} < {data1}";
                break;
              case Condition.LessOrEqual:
                str1 = $"{str2} <= {data1}";
                break;
            }
            break;
        }
      }
    }
    return str1;
  }

  private void CheckChars(string data)
  {
    char[] charArray = data.ToCharArray();
    for (int index = 0; index < charArray.Length; ++index)
    {
      char c = charArray[index];
      if (!char.IsDigit(c) && c != ';' && c != '.' && c != '-')
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_1150"), (object) data, (object) index, (object) c), LocalizationHolder.rm.GetString("Imbase.Client_1151"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        throw new AbortException();
      }
    }
  }

  private string BuildList(string data, bool needQuote)
  {
    string[] strArray = data.Split(new char[1]{ ';' }, StringSplitOptions.RemoveEmptyEntries);
    if (needQuote)
    {
      for (int index = 0; index < strArray.Length; ++index)
        strArray[index] = this.ApplyQuotes(strArray[index]);
    }
    return string.Join(",", strArray);
  }

  private string ConvertListToString(List<string> list)
  {
    return list == null ? string.Empty : string.Join("; ", list.ToArray());
  }

  private List<ListViewItem> CreateAttrsList(
    IUserSession session,
    long tableID,
    ListViewItem tableItem)
  {
    List<ListViewItem> attrsList = new List<ListViewItem>();
    DataSet tables = TableLoadHelper.GetTables(session, tableID, true);
    if (tables != null && tables.Tables.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) tables.Tables["IMS_ATTR_TYPES"].Rows)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(Convert.ToString(row["F_ATTRIBUTE_GUID"])));
        if (!string.IsNullOrEmpty(attributeType.Name))
        {
          ListViewItem listViewItem;
          if (this._AllAttrs.TryGetValue(attributeType.Name, out listViewItem))
          {
            if (listViewItem.Tag is List<ListViewItem> tag)
            {
              // ISSUE: explicit non-virtual call
              __nonvirtual (tag.Add(tableItem));
            }
          }
          else
          {
            listViewItem = new ListViewItem(" " + attributeType.Name, Convert.ToInt32((object) FindInTablesView.chbStates.Indeterminate))
            {
              Name = attributeType.AttributeID.ToString(),
              Tag = (object) new List<ListViewItem>()
              {
                tableItem
              }
            };
            this._AllAttrs.Add(attributeType.Name, listViewItem);
          }
          attrsList.Add(listViewItem);
        }
      }
    }
    return attrsList;
  }

  private string CreateFilterString(List<ConditionItem> conds)
  {
    string filterString = string.Empty;
    if (this.InvokeRequired)
      filterString = Convert.ToString(this.Invoke((Delegate) new FindInTablesView.CreateFilterDelegate(this.CreateFilterString), (object) conds));
    else if (this.conditions.Rows.Count > 0)
    {
      List<string> stringList = new List<string>(this.conditions.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) this.conditions.Rows)
      {
        string str = this.BuildFilterString(row, conds);
        if (!string.IsNullOrEmpty(str))
          stringList.Add(str);
      }
      filterString = string.Join(" AND ", stringList.ToArray());
    }
    return filterString;
  }

  private string CreateFilterString(string excludeColumnName)
  {
    string filterString = string.Empty;
    if (this.InvokeRequired)
      filterString = Convert.ToString(this.Invoke((Delegate) new System.Func<string, string>(this.CreateFilterString), (object) excludeColumnName));
    else if (this.conditions.Rows.Count > 0)
    {
      List<string> stringList = new List<string>(this.conditions.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) this.conditions.Rows)
      {
        if (row[1] == DBNull.Value || !((AttributeTypeProperties) row[0]).AttributeGuid.ToString().Equals(excludeColumnName))
        {
          string str = this.BuildFilterString(row, (List<ConditionItem>) null);
          if (!string.IsNullOrEmpty(str))
            stringList.Add(str);
        }
      }
      filterString = string.Join(" AND ", stringList.ToArray());
    }
    return filterString;
  }

  private ListViewItem CreatelvTablesItem(long tableID)
  {
    ListViewItem tableItem = (ListViewItem) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(tableID);
      if (!objectInfo.Empty)
      {
        tableItem = new ListViewItem(objectInfo.Caption)
        {
          Name = tableID.ToString(),
          StateImageIndex = Convert.ToInt32((object) FindInTablesView.chbStates.Checked)
        };
        try
        {
          tableItem.Tag = (object) this.CreateAttrsList(sessionKeeper.Session, tableID, tableItem);
        }
        catch
        {
          tableItem.Tag = (object) new List<ListViewItem>(0);
        }
      }
    }
    return tableItem;
  }

  private void FillConditionsMap() => ConditionHelper.FillConditionsMap(this.condsMap);

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

  private DataTable GetTableIDs(IUserSession session, string strKey)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor3 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) strKey, LogicalOperators.AND, 0, false),
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[3]
    {
      columnDescriptor1,
      columnDescriptor2,
      columnDescriptor3
    });
    DataTable tableIds = objectCollection.Select(paramSet);
    if (tableIds != null)
    {
      if (tableIds.Rows.Count > 0)
      {
        tableIds.Columns[0].ColumnName = "LinkID";
        tableIds.Columns[2].ColumnName = "TableID";
      }
      else
        tableIds = (DataTable) null;
    }
    return tableIds;
  }

  private bool NeedQuotes(FieldTypes fieldTypes)
  {
    return fieldTypes == FieldTypes.ftObjectLink || fieldTypes == FieldTypes.ftString || fieldTypes == FieldTypes.ftMemo || fieldTypes == FieldTypes.ftGuid;
  }

  private void SetData(object parentNode, LocateNodeEventHandler locateHandler)
  {
    this._locateHandler = locateHandler;
    this._parentINode = parentNode as NavigatorTreeNode;
    if (this._parentINode != null)
    {
      this._targetId = ((NodeID) this._parentINode.NodeID).ObjectID;
    }
    else
    {
      this._parentTreeNode = parentNode as TreeNode;
      if (this._parentTreeNode != null)
        this._targetId = this._parentTreeNode.Tag is NodeInfo tag ? tag.ObjectId : 0L;
    }
    (this._dgvConditions.Columns["F_DATA"] as TextWithButtonColumn).ButtonClick += new EventHandler(this.OnTextWithButtonColumn_ButtonClick);
  }

  private IEnumerable<long> GetCheckedTables()
  {
    return !this.InvokeRequired ? this.CheckedTables.Select<ListViewItem, long>((System.Func<ListViewItem, long>) (x => Convert.ToInt64(x.Name))) : this.Invoke((Delegate) new Func<IEnumerable<long>>(this.GetCheckedTables)) as IEnumerable<long>;
  }

  private List<object> GetValues(string columnName, out System.Type columnType)
  {
    List<object> source1 = new List<object>();
    IEnumerable<long> tableIDs = this.GetCheckedTables();
    Lookup<long, long> lookup = (Lookup<long, long>) this._dtLinks.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => tableIDs.Contains<long>(Convert.ToInt64(x["TableID"])))).ToLookup<DataRow, long, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x["TableID"])), (System.Func<DataRow, long>) (x => Convert.ToInt64(x["LinkID"])));
    columnType = (System.Type) null;
    int num = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (IGrouping<long, long> grouping in lookup)
      {
        DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, grouping.Key, true);
        if (tables != null && tables.Tables.Contains("IMS_ATTR_TYPES") && tables.Tables.Contains("IMS_DATA"))
        {
          DataTable dataTable = tables.Tables["IMS_DATA"];
          foreach (long linkId in (IEnumerable<long>) grouping)
          {
            ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
            TableLoadHelper.AssignAttributes(sessionKeeper.Session, linkId, grouping.Key, dataTable, tables.Tables["IMS_ATTR_TYPES"], out AttributeTypeProperties[] _, new List<CalculatedColumn>(), ref keyInfo);
            DataColumn column = dataTable.Columns[columnName];
            if (columnType == (System.Type) null)
              columnType = column.DataType != typeof (ValuesArray) ? column.DataType : column.ExtendedProperties[(object) "dataType"] as System.Type;
            string filterString = this.CreateFilterString(columnName);
            if (!string.IsNullOrEmpty(filterString))
            {
              DataRow[] source2 = dataTable.Select(filterString);
              if (((IEnumerable<DataRow>) source2).Any<DataRow>())
                dataTable = ((IEnumerable<DataRow>) source2).CopyToDataTable<DataRow>();
              else
                continue;
            }
            List<object> list = dataTable.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => x[columnName] != null && x[columnName] != DBNull.Value)).Select<DataRow, object>((System.Func<DataRow, object>) (x => x[columnName])).Distinct<object>().ToList<object>();
            if (list.Count != 0)
              source1.AddRange((IEnumerable<object>) list);
          }
        }
        Action<int, int> action = this.SetProgress != null ? this.SetProgress : throw new OperationCanceledException();
        if (action != null)
          action(lookup.Count, ++num);
      }
    }
    return source1.Count <= 0 ? (List<object>) null : source1.Distinct<object>().ToList<object>();
  }

  private List<T> Cast<T>(List<object> objs)
  {
    List<T> objList = (objs != null ? objs.Cast<T>().ToList<T>() : (List<T>) null) ?? new List<T>(0);
    if (objList.Count > 0)
      objList.Sort();
    return objList;
  }

  private void FillSimpleDataSource<T>(List<T> values, DataTable dtSource)
  {
    values.ForEach((Action<T>) (x => dtSource.Rows.Add((object) x, (object) x)));
  }

  private object LoadDataForSearch()
  {
    DataTable dataTable = (DataTable) null;
    if (this._dgvConditions.SelectedCells.Count > 0 && this._dgvConditions.SelectedCells[0] is DataGridViewTextBoxCell selectedCell)
    {
      AttributeTypeProperties attributeTypeProperties = (AttributeTypeProperties) this._dgvConditions.Rows[selectedCell.RowIndex].Cells[0].Value;
      System.Type columnType;
      List<object> values = this.GetValues(attributeTypeProperties.AttributeGuid.ToString(), out columnType);
      if (values != null)
      {
        DataTable dtSource = new DataTable();
        dtSource.Columns.AddRange(new DataColumn[2]
        {
          new DataColumn(),
          new DataColumn()
        });
        if (attributeTypeProperties.FieldType == FieldTypes.ftObjectLink)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            foreach (object obj in values)
            {
              string str = Convert.ToString(obj);
              QuickObjectInfo quickObjectInfo = GuidHelper.IsGuid(str) ? sessionKeeper.Session.GetObjectInfo(new Guid(str)) : sessionKeeper.Session.GetObjectInfo(Convert.ToInt64(str));
              if (!quickObjectInfo.Empty)
                dtSource.Rows.Add(obj, (object) quickObjectInfo.Caption);
            }
          }
        }
        else if (columnType == typeof (string))
        {
          List<string> list = values.Cast<string>().Where<string>((System.Func<string, bool>) (x => !string.IsNullOrEmpty(x.Trim()))).ToList<string>();
          if (list.Count > 0)
            list.Sort();
          this.FillSimpleDataSource<string>(list, dtSource);
        }
        else if (columnType == typeof (short) || columnType == typeof (int) || columnType == typeof (long))
          this.FillSimpleDataSource<long>(this.Cast<long>(values), dtSource);
        else if (columnType == typeof (double))
          this.FillSimpleDataSource<double>(this.Cast<double>(values), dtSource);
        else if (columnType == typeof (DateTime))
          this.FillSimpleDataSource<DateTime>(this.Cast<DateTime>(values), dtSource);
        dataTable = dtSource.Rows.Count > 0 ? dtSource : (DataTable) null;
      }
    }
    return (object) dataTable;
  }

  private void Search()
  {
    this._cts = new CancellationTokenSource();
    ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
    string strCompleted = LocalizationHolder.rm.GetString("Imbase.Processed.Msg");
    this._pnlProgress.Visible = true;
    this._lbTaskInfo.Text = LocalizationHolder.rm.GetString("Imbase_FindInTablesView_DialogCaption");
    this._progress.Value = 0;
    SelectedRecords.Clear();
    this._lvResult.Items.Clear();
    this._btnSearch.Enabled = false;
    this._substColumns.Clear();
    ParallelOptions po = new ParallelOptions()
    {
      CancellationToken = this._cts.Token,
      MaxDegreeOfParallelism = Environment.ProcessorCount
    };
    this._conditions.Clear();
    string strQuery = this.CreateFilterString(this._conditions);
    if (string.IsNullOrEmpty(strQuery))
      return;
    SelectedRecords.Conditions = this._conditions;
    bool needSubst = this._substColumns.Count > 0;
    IEnumerable<long> tableIDs = this.GetCheckedTables();
    Lookup<long, long> lookup = (Lookup<long, long>) this._dtLinks.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => tableIDs.Contains<long>(Convert.ToInt64(x["TableID"])))).ToLookup<DataRow, long, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x["TableID"])), (System.Func<DataRow, long>) (x => Convert.ToInt64(x["LinkID"])));
    int index = 0;
    double count = (double) lookup.Count;
    Task.Factory.StartNew((Action) (() => Parallel.ForEach<IGrouping<long, long>>((IEnumerable<IGrouping<long, long>>) lookup, po, (Action<IGrouping<long, long>>) (item =>
    {
      try
      {
        po.CancellationToken.ThrowIfCancellationRequested();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, item.Key, true);
          if (tables != null && tables.Tables.Contains("IMS_ATTR_TYPES") && tables.Tables.Contains("IMS_DATA"))
          {
            DataTable table = tables.Tables["IMS_DATA"];
            QuickObjectInfo info = sessionKeeper.Session.GetObjectInfo(item.Key);
            foreach (long num in (IEnumerable<long>) item)
            {
              long linkID = num;
              ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
              AttributeTypeProperties[] columnsAttributes;
              TableLoadHelper.AssignAttributes(sessionKeeper.Session, linkID, item.Key, table, tables.Tables["IMS_ATTR_TYPES"], out columnsAttributes, new List<CalculatedColumn>(), ref keyInfo);
              DataRow[] dataRowArray;
              if (needSubst)
              {
                using (DataTable stbstTable = TableLoadHelper.CreateStbstTable(table, ref strQuery, this._substColumns, columnsAttributes))
                  dataRowArray = stbstTable.Select(strQuery);
              }
              else
                dataRowArray = table.Select(strQuery);
              if (dataRowArray.Length != 0)
              {
                long[] arrRecordsIDs = new long[dataRowArray.Length];
                for (int index1 = 0; index1 < dataRowArray.Length; ++index1)
                  arrRecordsIDs[index1] = Convert.ToInt64(dataRowArray[index1]["F_KEY"]);
                if (this.InvokeRequired)
                  this.Invoke((Delegate) (() =>
                  {
                    this._lvResult.BeginUpdate();
                    try
                    {
                      ListViewItem listViewItem = new ListViewItem(info.Caption)
                      {
                        Name = Convert.ToString(linkID),
                        Tag = (object) arrRecordsIDs
                      };
                      listViewItem.SubItems.Add(arrRecordsIDs.Length.ToString());
                      this._lvResult.Items.Add(listViewItem);
                    }
                    finally
                    {
                      this._lvResult.EndUpdate();
                    }
                  }));
              }
            }
          }
          if (!this.InvokeRequired)
            return;
          this.Invoke((Delegate) (() =>
          {
            this._lbCompleted.Text = string.Format(strCompleted, (object) this._progress.Value);
            this._progress.Value = Convert.ToInt32(Math.Floor((double) ++index / count * 100.0));
            this._progress.Refresh();
          }));
        }
      }
      catch (OperationCanceledException ex)
      {
      }
      catch (Exception ex)
      {
        exceptions.Enqueue(ex);
      }
    })))).ContinueWith<object>((System.Func<Task, object>) (t => this.Invoke((Delegate) (() =>
    {
      if (exceptions.Count > 0)
        throw new AggregateException((IEnumerable<Exception>) exceptions);
      this._pnlProgress.Visible = false;
      if (this._lvResult.Items.Count != 0)
        return;
      string caption = LocalizationHolder.rm.GetString("Imbase_FindInTables_FindResult_Caption");
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Imbase_FindInTables_FindResult_NoRecordsMsg"), caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }))), this._cts.Token);
  }

  public void FirstShown(object sender, EventArgs e) => this.OnBeforeFirstShown(sender, e);

  public void ViewClosing(object sender, CancelEventArgs e)
  {
    this._cts?.Cancel();
    if (this._conditions.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        string str = ConditionHelper.CondsToString(this._conditions);
        sessionKeeper.Session.Configurations.WriteString("Imbase", "FindInTables", "T" + this._targetId.ToString(), str, sessionKeeper.Session.UserID);
      }
    }
    SelectedRecords.Conditions = (List<ConditionItem>) null;
  }

  object ILoadDataAsync.LoadData()
  {
    object obj = (object) null;
    if (this.DataLoader != null)
      obj = this.DataLoader();
    return obj;
  }

  public event Action<int, int> SetProgress;

  private void FindInTablesView_VisibleChanged(object sender, EventArgs e)
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._ico?.Dispose();
      this._ico = (Icon) null;
      if (this.components != null)
        this.components?.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FindInTablesView));
    this.contextMenu = new ContextMenuStrip(this.components);
    this.miSelectAll = new ToolStripMenuItem();
    this.miClearAll = new ToolStripMenuItem();
    this.miInvert = new ToolStripMenuItem();
    this.ilCheckedStates = new ImageList(this.components);
    this._btnUpdate = new Button();
    this._btnStep = new Button();
    this._lvTables = new ListView();
    this.colTablesName = new ColumnHeader();
    this._lvAttrs = new ListView();
    this.colAttrsName = new ColumnHeader();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewComboBoxColumn1 = new DataGridViewComboBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.textWithButtonColumn1 = new TextWithButtonColumn();
    this.dataColumn1 = new DataColumn();
    this._ds = new DataSet();
    this.conditions = new DataTable();
    this.dataColumn2 = new DataColumn();
    this.dataColumn3 = new DataColumn();
    this.dataColumn4 = new DataColumn();
    this.condsMap = new DataTable();
    this.dataColumn5 = new DataColumn();
    this.dataColumn6 = new DataColumn();
    this.pnlFirst = new Panel();
    this.tlpFirst = new TableLayoutPanel();
    this.pnlBottom = new Panel();
    this.tlpBottom = new TableLayoutPanel();
    this.pnlSecond = new Panel();
    this.tlpSecond = new TableLayoutPanel();
    this._dgvConditions = new DataGridView();
    this.F_NAME = new DataGridViewTextBoxColumn();
    this.F_COND = new DataGridViewComboBoxColumn();
    this.F_DATA = new TextWithButtonColumn();
    this._lvResult = new ListView();
    this.colName = new ColumnHeader();
    this.colCounter = new ColumnHeader();
    this._btnSearch = new Button();
    this._pnlProgress = new Panel();
    this._progress = new ProgressBar();
    this._pnlScanInfo = new Panel();
    this._lbCompleted = new Label();
    this._lbTaskInfo = new Label();
    this._timer = new System.Windows.Forms.Timer(this.components);
    this.contextMenu.SuspendLayout();
    this._ds.BeginInit();
    this.conditions.BeginInit();
    this.condsMap.BeginInit();
    this.pnlFirst.SuspendLayout();
    this.tlpFirst.SuspendLayout();
    this.pnlBottom.SuspendLayout();
    this.tlpBottom.SuspendLayout();
    this.pnlSecond.SuspendLayout();
    this.tlpSecond.SuspendLayout();
    ((ISupportInitialize) this._dgvConditions).BeginInit();
    this._pnlProgress.SuspendLayout();
    this._pnlScanInfo.SuspendLayout();
    this.SuspendLayout();
    this.contextMenu.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.miSelectAll,
      (ToolStripItem) this.miClearAll,
      (ToolStripItem) this.miInvert
    });
    this.contextMenu.Name = "contextMenu";
    componentResourceManager.ApplyResources((object) this.contextMenu, "contextMenu");
    this.miSelectAll.Name = "miSelectAll";
    componentResourceManager.ApplyResources((object) this.miSelectAll, "miSelectAll");
    this.miSelectAll.Tag = (object) "0";
    this.miSelectAll.Click += new EventHandler(this.OnMenuItem_Click);
    this.miClearAll.Name = "miClearAll";
    componentResourceManager.ApplyResources((object) this.miClearAll, "miClearAll");
    this.miClearAll.Tag = (object) "1";
    this.miClearAll.Click += new EventHandler(this.OnMenuItem_Click);
    this.miInvert.Name = "miInvert";
    componentResourceManager.ApplyResources((object) this.miInvert, "miInvert");
    this.miInvert.Tag = (object) "2";
    this.miInvert.Click += new EventHandler(this.OnMenuItem_Click);
    this.ilCheckedStates.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilCheckedStates.ImageStream");
    this.ilCheckedStates.TransparentColor = Color.Transparent;
    this.ilCheckedStates.Images.SetKeyName(0, "Unchecked.bmp");
    this.ilCheckedStates.Images.SetKeyName(1, "Checked.bmp");
    this.ilCheckedStates.Images.SetKeyName(2, "Indeterminate.bmp");
    componentResourceManager.ApplyResources((object) this._btnUpdate, "_btnUpdate");
    this._btnUpdate.ImageList = this.ilCheckedStates;
    this._btnUpdate.Name = "_btnUpdate";
    this._btnUpdate.UseVisualStyleBackColor = true;
    this._btnUpdate.Click += new EventHandler(this.On_btnUpdate_Click);
    componentResourceManager.ApplyResources((object) this._btnStep, "_btnStep");
    this._btnStep.Name = "_btnStep";
    this._btnStep.UseVisualStyleBackColor = true;
    this._btnStep.Click += new EventHandler(this.On_btnStep_Click);
    this._lvTables.CheckBoxes = true;
    this._lvTables.Columns.AddRange(new ColumnHeader[1]
    {
      this.colTablesName
    });
    this._lvTables.ContextMenuStrip = this.contextMenu;
    componentResourceManager.ApplyResources((object) this._lvTables, "_lvTables");
    this._lvTables.FullRowSelect = true;
    this._lvTables.HeaderStyle = ColumnHeaderStyle.None;
    this._lvTables.HideSelection = false;
    this._lvTables.MultiSelect = false;
    this._lvTables.Name = "_lvTables";
    this.tlpFirst.SetRowSpan((Control) this._lvTables, 3);
    this._lvTables.Sorting = SortOrder.Ascending;
    this._lvTables.UseCompatibleStateImageBehavior = false;
    this._lvTables.View = View.Details;
    this._lvTables.ItemChecked += new ItemCheckedEventHandler(this.On_lvTables_ItemChecked);
    componentResourceManager.ApplyResources((object) this.colTablesName, "colTablesName");
    this._lvAttrs.Columns.AddRange(new ColumnHeader[1]
    {
      this.colAttrsName
    });
    componentResourceManager.ApplyResources((object) this._lvAttrs, "_lvAttrs");
    this._lvAttrs.FullRowSelect = true;
    this._lvAttrs.HeaderStyle = ColumnHeaderStyle.None;
    this._lvAttrs.HideSelection = false;
    this._lvAttrs.MultiSelect = false;
    this._lvAttrs.Name = "_lvAttrs";
    this.tlpFirst.SetRowSpan((Control) this._lvAttrs, 3);
    this._lvAttrs.SmallImageList = this.ilCheckedStates;
    this._lvAttrs.Sorting = SortOrder.Ascending;
    this._lvAttrs.UseCompatibleStateImageBehavior = false;
    this._lvAttrs.View = View.Details;
    this._lvAttrs.KeyDown += new KeyEventHandler(this.On_lvAttrs_KeyDown);
    this._lvAttrs.MouseDown += new MouseEventHandler(this.On_lvAttrs_MouseDown);
    this._lvAttrs.MouseUp += new MouseEventHandler(this.On_lvAttrs_MouseUp);
    componentResourceManager.ApplyResources((object) this.colAttrsName, "colAttrsName");
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn1.DataPropertyName = "F_NAME";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewComboBoxColumn1.DataPropertyName = "F_COND";
    componentResourceManager.ApplyResources((object) this.dataGridViewComboBoxColumn1, "dataGridViewComboBoxColumn1");
    this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
    this.dataGridViewComboBoxColumn1.Resizable = DataGridViewTriState.True;
    this.dataGridViewComboBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
    this.dataGridViewTextBoxColumn2.DataPropertyName = "F_DATA";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.textWithButtonColumn1.DataPropertyName = "F_VALUE";
    componentResourceManager.ApplyResources((object) this.textWithButtonColumn1, "textWithButtonColumn1");
    this.textWithButtonColumn1.Name = "textWithButtonColumn1";
    this.textWithButtonColumn1.Resizable = DataGridViewTriState.True;
    this.textWithButtonColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
    this.textWithButtonColumn1.TextReadOnly = false;
    this.dataColumn1.AllowDBNull = false;
    this.dataColumn1.ColumnName = "F_COND";
    this.dataColumn1.DataType = typeof (object);
    this._ds.DataSetName = "NewDataSet";
    this._ds.Tables.AddRange(new DataTable[2]
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
      this.dataColumn5,
      this.dataColumn6
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
      this.dataColumn5
    };
    this.condsMap.TableName = "CondsMap";
    this.dataColumn5.AllowDBNull = false;
    this.dataColumn5.ColumnName = "F_COND";
    this.dataColumn5.DataType = typeof (object);
    this.dataColumn6.ColumnName = "F_NAME";
    this.pnlFirst.Controls.Add((Control) this.tlpFirst);
    componentResourceManager.ApplyResources((object) this.pnlFirst, "pnlFirst");
    this.pnlFirst.Name = "pnlFirst";
    componentResourceManager.ApplyResources((object) this.tlpFirst, "tlpFirst");
    this.tlpFirst.Controls.Add((Control) this._btnUpdate, 1, 1);
    this.tlpFirst.Controls.Add((Control) this._lvTables, 0, 0);
    this.tlpFirst.Controls.Add((Control) this._lvAttrs, 2, 0);
    this.tlpFirst.Name = "tlpFirst";
    this.pnlBottom.Controls.Add((Control) this.tlpBottom);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    componentResourceManager.ApplyResources((object) this.tlpBottom, "tlpBottom");
    this.tlpBottom.Controls.Add((Control) this._btnStep, 1, 0);
    this.tlpBottom.Name = "tlpBottom";
    this.pnlSecond.Controls.Add((Control) this.tlpSecond);
    componentResourceManager.ApplyResources((object) this.pnlSecond, "pnlSecond");
    this.pnlSecond.Name = "pnlSecond";
    componentResourceManager.ApplyResources((object) this.tlpSecond, "tlpSecond");
    this.tlpSecond.Controls.Add((Control) this._dgvConditions, 0, 0);
    this.tlpSecond.Controls.Add((Control) this._lvResult, 0, 2);
    this.tlpSecond.Controls.Add((Control) this._btnSearch, 1, 1);
    this.tlpSecond.Name = "tlpSecond";
    this._dgvConditions.AllowUserToAddRows = false;
    this._dgvConditions.AllowUserToDeleteRows = false;
    this._dgvConditions.AllowUserToResizeRows = false;
    this._dgvConditions.AutoGenerateColumns = false;
    this._dgvConditions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    this._dgvConditions.BackgroundColor = SystemColors.Window;
    this._dgvConditions.BorderStyle = BorderStyle.Fixed3D;
    this._dgvConditions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._dgvConditions.Columns.AddRange((DataGridViewColumn) this.F_NAME, (DataGridViewColumn) this.F_COND, (DataGridViewColumn) this.F_DATA);
    this.tlpSecond.SetColumnSpan((Control) this._dgvConditions, 3);
    this._dgvConditions.DataMember = "Conditions";
    this._dgvConditions.DataSource = (object) this._ds;
    componentResourceManager.ApplyResources((object) this._dgvConditions, "_dgvConditions");
    this._dgvConditions.Name = "_dgvConditions";
    this._dgvConditions.RowHeadersVisible = false;
    this._dgvConditions.CellValueChanged += new DataGridViewCellEventHandler(this.On_dgvConditions_CellValueChanged);
    this.F_NAME.DataPropertyName = "F_NAME";
    componentResourceManager.ApplyResources((object) this.F_NAME, "F_NAME");
    this.F_NAME.Name = "F_NAME";
    this.F_COND.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
    this.F_COND.DataPropertyName = "F_COND";
    this.F_COND.DataSource = (object) this._ds;
    this.F_COND.DisplayMember = "CondsMap.F_NAME";
    this.F_COND.FillWeight = 150f;
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
    this.F_DATA.TextReadOnly = false;
    this._lvResult.Columns.AddRange(new ColumnHeader[2]
    {
      this.colName,
      this.colCounter
    });
    this.tlpSecond.SetColumnSpan((Control) this._lvResult, 3);
    componentResourceManager.ApplyResources((object) this._lvResult, "_lvResult");
    this._lvResult.FullRowSelect = true;
    this._lvResult.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this._lvResult.HideSelection = false;
    this._lvResult.MultiSelect = false;
    this._lvResult.Name = "_lvResult";
    this._lvResult.UseCompatibleStateImageBehavior = false;
    this._lvResult.View = View.Details;
    this._lvResult.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.On_lvResult_ItemSelectionChanged);
    this._lvResult.SizeChanged += new EventHandler(this.On_lvResult_SizeChanged);
    componentResourceManager.ApplyResources((object) this.colName, "colName");
    componentResourceManager.ApplyResources((object) this.colCounter, "colCounter");
    componentResourceManager.ApplyResources((object) this._btnSearch, "_btnSearch");
    this._btnSearch.Name = "_btnSearch";
    this._btnSearch.UseVisualStyleBackColor = true;
    this._btnSearch.Click += new EventHandler(this.On_btnSearch_Click);
    this._pnlProgress.Controls.Add((Control) this._progress);
    this._pnlProgress.Controls.Add((Control) this._pnlScanInfo);
    componentResourceManager.ApplyResources((object) this._pnlProgress, "_pnlProgress");
    this._pnlProgress.Name = "_pnlProgress";
    componentResourceManager.ApplyResources((object) this._progress, "_progress");
    this._progress.Name = "_progress";
    this._progress.Step = 1;
    this._pnlScanInfo.Controls.Add((Control) this._lbCompleted);
    this._pnlScanInfo.Controls.Add((Control) this._lbTaskInfo);
    componentResourceManager.ApplyResources((object) this._pnlScanInfo, "_pnlScanInfo");
    this._pnlScanInfo.Name = "_pnlScanInfo";
    componentResourceManager.ApplyResources((object) this._lbCompleted, "_lbCompleted");
    this._lbCompleted.Name = "_lbCompleted";
    componentResourceManager.ApplyResources((object) this._lbTaskInfo, "_lbTaskInfo");
    this._lbTaskInfo.Name = "_lbTaskInfo";
    this._timer.Interval = 500;
    this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlSecond);
    this.Controls.Add((Control) this.pnlFirst);
    this.Controls.Add((Control) this._pnlProgress);
    this.Controls.Add((Control) this.pnlBottom);
    this.DoubleBuffered = true;
    this.FloatingSize = new Size(766, 496);
    this.Name = nameof (FindInTablesView);
    this.PersistState = false;
    this.ShowImageInDocumentTab = true;
    this.BeforeFirstShown += new EventHandler(this.OnBeforeFirstShown);
    this.Closing += new CancelEventHandler(this.FindInTablesView_Closing);
    this.VisibleChanged += new EventHandler(this.FindInTablesView_VisibleChanged);
    this.contextMenu.ResumeLayout(false);
    this._ds.EndInit();
    this.conditions.EndInit();
    this.condsMap.EndInit();
    this.pnlFirst.ResumeLayout(false);
    this.tlpFirst.ResumeLayout(false);
    this.pnlBottom.ResumeLayout(false);
    this.tlpBottom.ResumeLayout(false);
    this.pnlSecond.ResumeLayout(false);
    this.tlpSecond.ResumeLayout(false);
    ((ISupportInitialize) this._dgvConditions).EndInit();
    this._pnlProgress.ResumeLayout(false);
    this._pnlScanInfo.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  internal enum chbStates
  {
    Unchecked,
    Checked,
    Indeterminate,
  }

  private delegate string CreateFilterDelegate(List<ConditionItem> conds);
}
