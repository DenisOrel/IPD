// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Indexes.NotUniqueRecordsCtrl
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
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
namespace Intermech.Imbase.Indexes;

public class NotUniqueRecordsCtrl : UserControl
{
  private int _attrId;
  private INamedImageList _namedImageList;
  private int _imgObjectIndex;
  private DataView _detailDataView;
  private DataTable _dtDetail;
  private bool _loading;
  private IContainer components;
  private Panel panel1;
  private Label _lb;
  private Button _btnUnique;
  private Label _lbCaption;
  private SplitContainer spltContainer;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataColumn fUniqueText;
  private DataTable _dtMaster;
  private DataSet dataSet;
  private ContextMenuStrip cmStrip;
  private ToolStripMenuItem miGoToTable;
  private ToolStripMenuItem miOpenObjInNewWindow;
  private ToolStripMenuItem miUnite;
  private ToolStripSeparator toolStripSeparator1;
  private DataGridView dgvValues;
  private DataGridView dgvInfo;
  private TextBox tbValuesFilter;
  private DataGridViewTextBoxColumn fTEXTMaster;
  private DataColumn dataColumn1;
  private DataGridViewCheckBoxColumn CheckedColumn;
  private DataGridViewTextBoxColumn F_TABKEY;
  private DataGridViewTextBoxColumn fTEXTDataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn fTABLEIDDataGridViewTextBoxColumn;
  private DataGridViewTextBoxColumn fLINKIDDataGridViewTextBoxColumn;
  private DataGridViewTextBoxColumn fFULLPATHDataGridViewTextBoxColumn;

  internal bool CanUniteDuplicates
  {
    get
    {
      int count = this.dgvInfo.Rows.Count;
      if (count < 2)
        return false;
      int num = 0;
      foreach (DataGridViewRow row in (IEnumerable) this.dgvInfo.Rows)
      {
        if (Convert.ToBoolean(row.Cells[this.CheckedColumn.Index].Value))
          ++num;
      }
      return count - num == 1;
    }
  }

  internal IEnumerable<RecordInfo> Duplicates
  {
    get
    {
      List<RecordInfo> duplicates = new List<RecordInfo>();
      foreach (DataGridViewRow row1 in (IEnumerable) this.dgvInfo.Rows)
      {
        if (Convert.ToBoolean(row1.Cells[this.CheckedColumn.Index].Value) && !row1.Selected && row1.DataBoundItem is DataRowView dataBoundItem)
        {
          DataRow row2 = dataBoundItem.Row;
          long linkId = Convert.ToInt64(row2[IndexesField.F_LINK_ID]);
          long recordId = Convert.ToInt64(row2[IndexesField.F_TABKEY]);
          RecordInfo recordInfo = this.RecsInfo.FirstOrDefault<RecordInfo>((System.Func<RecordInfo, bool>) (x => x.LinkId == linkId && x.RecordId == recordId));
          if (recordInfo != null)
            duplicates.Add(recordInfo);
        }
      }
      return (IEnumerable<RecordInfo>) duplicates;
    }
  }

  internal RecordInfo Current
  {
    get
    {
      if (this.dgvInfo.SelectedRows.Count <= 0 || !(this.dgvInfo.SelectedRows[0].DataBoundItem is DataRowView dataBoundItem))
        return (RecordInfo) null;
      DataRow row = dataBoundItem.Row;
      long linkId = Convert.ToInt64(row[IndexesField.F_LINK_ID]);
      long recordId = Convert.ToInt64(row[IndexesField.F_TABKEY]);
      return this.RecsInfo.FirstOrDefault<RecordInfo>((System.Func<RecordInfo, bool>) (x => x.LinkId == linkId && x.RecordId == recordId));
    }
  }

  internal IEnumerable<RecordInfo> NotUsedRecords
  {
    get
    {
      List<RecordInfo> notUsedRecords = new List<RecordInfo>();
      foreach (DataGridViewRow row1 in (IEnumerable) this.dgvInfo.Rows)
      {
        if (!Convert.ToBoolean(row1.Cells[this.CheckedColumn.Index].Value) && !row1.Selected && row1.DataBoundItem is DataRowView dataBoundItem)
        {
          DataRow row2 = dataBoundItem.Row;
          long linkId = Convert.ToInt64(row2[IndexesField.F_LINK_ID]);
          long recordId = Convert.ToInt64(row2[IndexesField.F_TABKEY]);
          RecordInfo recordInfo = this.RecsInfo.FirstOrDefault<RecordInfo>((System.Func<RecordInfo, bool>) (x => x.LinkId == linkId && x.RecordId == recordId));
          if (recordInfo != null)
            notUsedRecords.Add(recordInfo);
        }
      }
      return (IEnumerable<RecordInfo>) notUsedRecords;
    }
  }

  internal List<RecordInfo> RecsInfo { get; } = new List<RecordInfo>();

  public System.IServiceProvider Provider { get; set; }

  public long CatalogID { get; set; }

  public int AttrID
  {
    get => this._attrId;
    set
    {
      this._attrId = value;
      if (this._attrId != 0 && this.CatalogID != 0L)
      {
        this._btnUnique.Enabled = true;
      }
      else
      {
        this._btnUnique.Enabled = false;
        this.Clear();
      }
    }
  }

  public NotUniqueRecordsCtrl()
  {
    this.InitializeComponent();
    this._namedImageList = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, false);
    INamedImageList namedImageList = this._namedImageList;
    this._imgObjectIndex = namedImageList != null ? namedImageList.ImageIndex("imgObject") : -1;
  }

  public event NotUniqueRecordsCtrl.GetNotUniqueEventHandler GetNotUnique;

  private void On_btnUnique_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) is IImbaseIndexingService customService))
      {
        ExceptionHelper.ExceptionService.ShowException(new Exception(LocalizationHolder.rm.GetString("Imbase_Null_Indexing_Service")));
      }
      else
      {
        DataTable notUniqueValues = customService.GetNotUniqueValues(sessionKeeper.Session.SessionGUID, this.CatalogID, this._attrId);
        if (notUniqueValues.Rows.Count > 0)
        {
          this.Fill(notUniqueValues);
          NotUniqueRecordsCtrl.GetNotUniqueEventHandler getNotUnique = this.GetNotUnique;
          if (getNotUnique == null)
            return;
          getNotUnique(this._attrId, notUniqueValues);
        }
        else
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("NotUniqueData_AllUnique"));
        }
      }
    }
  }

  protected string EscapeString(string value)
  {
    char[] anyOf = new char[5]{ '%', '*', '[', ']', '\'' };
    if (string.IsNullOrEmpty(value) || value.LastIndexOfAny(anyOf) < 0)
      return value;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (char ch in value.ToCharArray())
    {
      switch (ch)
      {
        case '%':
        case '*':
        case '[':
        case ']':
          stringBuilder.Append('[');
          stringBuilder.Append(ch);
          stringBuilder.Append(']');
          break;
        case '\'':
          stringBuilder.Append("\\");
          break;
        default:
          stringBuilder.Append(ch);
          break;
      }
    }
    return stringBuilder.ToString();
  }

  private void dgvValues_SelectionChanged(object sender, EventArgs e)
  {
    if (this._loading)
      return;
    this.RecsInfo.Clear();
    if (this.dgvValues.CurrentCell == null)
    {
      this._detailDataView.RowFilter = string.Empty;
    }
    else
    {
      if (this.dgvValues.CurrentRow.DataBoundItem is DataRowView dataBoundItem1)
        this._detailDataView.RowFilter = $"[F_HASHTEXT] = {TableLoadHelper.QuoteString(dataBoundItem1.Row[IndexesField.F_HASHTEXT].ToString())}";
      Application.DoEvents();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IImbaseServer service = ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, true);
        foreach (DataGridViewRow row1 in (IEnumerable) this.dgvInfo.Rows)
        {
          if (row1.DataBoundItem is DataRowView dataBoundItem2)
          {
            DataRow row2 = dataBoundItem2.Row;
            long int64_1 = Convert.ToInt64(row2[IndexesField.F_LINK_ID]);
            long int64_2 = Convert.ToInt64(row2[IndexesField.F_TABLE_ID]);
            long int64_3 = Convert.ToInt64(row2[IndexesField.F_TABKEY]);
            bool createNew = false;
            int type = 0;
            long[] existingObjects = new long[0];
            service.GetObjectCreateInfo(sessionKeeper.Session.SessionGUID, int64_1, int64_3, ref createNew, ref type, ref existingObjects);
            this.RecsInfo.Add(new RecordInfo()
            {
              LinkId = int64_1,
              TableId = int64_2,
              RecordId = int64_3,
              CreateNewMode = createNew,
              ObjectType = type,
              ObjectIds = ((IEnumerable<long>) existingObjects).ToList<long>()
            });
            row1.Cells[this.CheckedColumn.Name].Value = (object) (bool) (existingObjects.Length != 0 ? 0 : (!row1.Selected ? 1 : 0));
          }
        }
      }
      this.dgvInfo.Invalidate();
    }
  }

  private void dgvInfo_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
  {
    if (this._loading || !this.dgvInfo.RowHeadersVisible || e.ColumnIndex != -1 || e.RowIndex == -1 || this.dgvInfo.Rows.Count <= e.RowIndex)
      return;
    if (!(this.dgvInfo.Rows[e.RowIndex].DataBoundItem is DataRowView dataBoundItem))
    {
      e.Handled = true;
    }
    else
    {
      DataRow row = dataBoundItem.Row;
      long linkId = Convert.ToInt64(row[IndexesField.F_LINK_ID]);
      long recordId = Convert.ToInt64(row[IndexesField.F_TABKEY]);
      e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
      Rectangle cellBounds = e.CellBounds;
      RecordInfo recordInfo = this.RecsInfo.FirstOrDefault<RecordInfo>((System.Func<RecordInfo, bool>) (x => x.LinkId == linkId && x.RecordId == recordId));
      if ((recordInfo != null ? (recordInfo.ObjectIds.Count > 0 ? 1 : 0) : 0) != 0 && this._namedImageList != null && this._imgObjectIndex != -1)
      {
        int x = cellBounds.Right - 20;
        int y = cellBounds.Top + (cellBounds.Height - 16 /*0x10*/) / 2;
        if (x > 12)
          this._namedImageList.ImageList.Draw(e.Graphics, new Point(x, y), this._imgObjectIndex);
      }
      e.Handled = true;
    }
  }

  private void dgvInfo_SelectionChanged(object sender, EventArgs e)
  {
    if (this._loading || this.dgvInfo.SelectedRows.Count <= 0)
      return;
    this.dgvInfo.SelectedRows[0].Cells[this.CheckedColumn.Name].Value = (object) false;
  }

  private void dgvInfo_RowLeave(object sender, DataGridViewCellEventArgs e)
  {
    if (this._loading || ((DataView) this.dgvInfo.DataSource).Count <= e.RowIndex || !(this.dgvInfo.Rows[e.RowIndex].DataBoundItem is DataRowView dataBoundItem))
      return;
    DataRow row = dataBoundItem.Row;
    long linkId = Convert.ToInt64(row[IndexesField.F_LINK_ID]);
    long recordId = Convert.ToInt64(row[IndexesField.F_TABKEY]);
    if (this.dgvInfo.SelectedRows.Count <= 0)
      return;
    RecordInfo recordInfo = this.RecsInfo.FirstOrDefault<RecordInfo>((System.Func<RecordInfo, bool>) (x => x.LinkId == linkId && x.RecordId == recordId));
    if ((recordInfo != null ? (recordInfo.ObjectIds.Count == 0 ? 1 : 0) : 0) == 0)
      return;
    this.dgvInfo.SelectedRows[0].Cells[this.CheckedColumn.Name].Value = (object) true;
  }

  private void miOpenObjInNewWindow_Click(object sender, EventArgs e)
  {
    RecordInfo current = this.Current;
    if ((current != null ? (current.ObjectIds.Count > 0 ? 1 : 0) : 0) == 0)
      return;
    Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(this.Current.ObjectIds.First<long>()), this.Provider);
  }

  private void miGoToTable_Click(object sender, EventArgs e)
  {
    if (this.Current == null)
      return;
    List<long> list = this.Duplicates.Where<RecordInfo>((System.Func<RecordInfo, bool>) (x => x.TableId == this.Current.TableId && x.LinkId == this.Current.LinkId)).Select<RecordInfo, long>((System.Func<RecordInfo, long>) (x => x.RecordId)).Distinct<long>().ToList<long>();
    if (!list.Contains(this.Current.RecordId))
      list.Add(this.Current.RecordId);
    SelectedRecords.Add(this.Current.LinkId, list.ToArray());
    Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(this.Current.LinkId), this.Provider);
  }

  private void miUnite_Click(object sender, EventArgs e)
  {
    if (this.Current == null || !this.Duplicates.Any<RecordInfo>())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IImbaseServer service1 = ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, true);
      IAdminUtilsService service2 = ServiceUtils.GetService<IAdminUtilsService>((object) sessionKeeper.Session, true);
      INotificationService service3 = ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, true);
      ITablesIndexer service4 = ServiceUtils.GetService<ITablesIndexer>((object) sessionKeeper.Session, true);
      IImbaseIndexingService service5 = ServiceUtils.GetService<IImbaseIndexingService>((object) sessionKeeper.Session, true);
      try
      {
        this.CheckRemoveRecordRight(sessionKeeper.Session);
        IEnumerable<IGrouping<long, RecordInfo>> groupings = this.Duplicates.GroupBy<RecordInfo, long>((System.Func<RecordInfo, long>) (d => d.TableId));
        bool flag1 = this.NotUsedRecords.Any<RecordInfo>();
        if (this.Duplicates.Any<RecordInfo>((System.Func<RecordInfo, bool>) (x => x.ObjectIds.Count > 0)))
        {
          if (this.Current.CreateNewMode || this.Current.ObjectIds.Count > 1)
            throw new Exception(LocalizationHolder.rm.GetString("MainRecordHasInvalidCreateMode"));
          long toObjectID = this.Current.ObjectIds.Count == 0 ? service1.CreateObject(sessionKeeper.Session.SessionGUID, this.CatalogID, this.Current.LinkId, this.Current.RecordId, true, -1) : this.Current.ObjectIds.First<long>();
          if (toObjectID == 0L)
            throw new Exception(LocalizationHolder.rm.GetString("RecordObjectNotFound"));
          foreach (IGrouping<long, RecordInfo> grouping in groupings)
          {
            DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, grouping.Key, true);
            DataTable table = tables.Tables["IMS_DATA"];
            bool flag2 = false;
            List<RecordInfo> source = new List<RecordInfo>();
            foreach (RecordInfo recordInfo in (IEnumerable<RecordInfo>) grouping)
            {
              RecordInfo duplicate = recordInfo;
              if (duplicate.CreateNewMode || duplicate.ObjectIds.Count > 1)
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("RecordObjectNotFound"), (object) duplicate.RecordId, (object) duplicate.LinkId));
              service2.CombineObjects(sessionKeeper.Session.SessionGUID, duplicate.ObjectIds.ToArray(), toObjectID);
              service3.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) duplicate.ObjectIds));
              DataRow dataRow = table.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (r => Convert.ToInt64(r["F_KEY"]) == duplicate.RecordId));
              if (dataRow != null)
              {
                dataRow.Delete();
                table.AcceptChanges();
                flag2 = true;
              }
              source.Add(duplicate);
            }
            if (flag2)
              TableLoadHelper.StoreData(sessionKeeper.Session, grouping.Key, tables, service4);
            List<long> list = source.Select<RecordInfo, long>((System.Func<RecordInfo, long>) (x => x.RecordId)).ToList<long>();
            service5.UpdateAfterTableDataChanged(sessionKeeper.Session.SessionGUID, Guid.NewGuid(), grouping.Key, list, new List<int>());
            foreach (RecordInfo duplicate in source)
              this.RemoveDuplicateRow(duplicate);
          }
        }
        else
        {
          foreach (IGrouping<long, RecordInfo> grouping in groupings)
          {
            DataSet tables = TableLoadHelper.GetTables(sessionKeeper.Session, grouping.Key, true);
            DataTable table = tables.Tables["IMS_DATA"];
            bool flag3 = false;
            List<RecordInfo> source = new List<RecordInfo>();
            foreach (RecordInfo recordInfo in (IEnumerable<RecordInfo>) grouping)
            {
              RecordInfo duplicate = recordInfo;
              DataRow dataRow = table.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (r => Convert.ToInt64(r["F_KEY"]) == duplicate.RecordId));
              if (dataRow != null)
              {
                dataRow.Delete();
                table.AcceptChanges();
                flag3 = true;
              }
              source.Add(duplicate);
            }
            if (flag3)
              TableLoadHelper.StoreData(sessionKeeper.Session, grouping.Key, tables, service4);
            List<long> list = source.Select<RecordInfo, long>((System.Func<RecordInfo, long>) (x => x.RecordId)).ToList<long>();
            service5.UpdateAfterTableDataChanged(sessionKeeper.Session.SessionGUID, Guid.NewGuid(), grouping.Key, list, new List<int>());
            foreach (RecordInfo duplicate in source)
              this.RemoveDuplicateRow(duplicate);
          }
        }
        if (flag1)
          return;
        this.RemoveEtalonRow();
        this.RemoveMasterRow();
      }
      catch (Exception ex)
      {
        NotUniqueRecordsCtrl.WriteOutputMessage(ex.Message);
      }
    }
  }

  private void cmStrip_Opening(object sender, CancelEventArgs e)
  {
    ToolStripMenuItem openObjInNewWindow = this.miOpenObjInNewWindow;
    RecordInfo current = this.Current;
    int num = current != null ? (current.ObjectIds.Count > 0 ? 1 : 0) : 0;
    openObjInNewWindow.Enabled = num != 0;
    this.miUnite.Enabled = this.Current != null && this.Duplicates.Any<RecordInfo>() && this.CanUniteDuplicates;
  }

  private void dgvInfo_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    DataGridView.HitTestInfo hitTestInfo = this.dgvInfo.HitTest(e.X, e.Y);
    if (hitTestInfo.RowIndex != -1 && hitTestInfo.ColumnIndex != -1)
    {
      if (this.dgvInfo.SelectedRows[0].Index != hitTestInfo.RowIndex)
      {
        this.dgvInfo.CurrentCell = this.dgvInfo.Rows[hitTestInfo.RowIndex].Cells[0];
        this.dgvInfo.Rows[hitTestInfo.RowIndex].Selected = true;
      }
      this.cmStrip.Show((Control) this.dgvInfo, new Point(e.X, e.Y));
    }
    else
      this.cmStrip.Visible = false;
  }

  private void CheckRemoveRecordRight(IUserSession session)
  {
    bool flag = true;
    StringBuilder stringBuilder = new StringBuilder();
    IImbaseServer service = ServiceUtils.GetService<IImbaseServer>((object) session, true);
    foreach (RecordInfo duplicate in this.Duplicates)
    {
      if (!service.GetSecurityForRecord(session.SessionGUID, duplicate.TableId, duplicate.RecordId).CheckAccess(ActionType.Delete, true, false))
      {
        stringBuilder.AppendLine(string.Format(LocalizationHolder.rm.GetString("RecordNoRight"), (object) duplicate.RecordId, (object) duplicate.TableId));
        flag = false;
      }
    }
    if (!flag)
      throw new Exception(stringBuilder.ToString());
  }

  private void Clear()
  {
    this._dtMaster.Clear();
    this._dtDetail?.Clear();
    this.tbValuesFilter.Text = string.Empty;
    this.SetLabelCountText();
  }

  private void SetLabelCountText()
  {
    int num1 = this._lb.Text.IndexOf(":");
    int num2;
    this._lb.Text = $"{this._lb.Text.Substring(0, num2 = num1 + 1)} {Convert.ToString(this._dtMaster.Rows.Count)}";
  }

  private void RemoveMasterRow()
  {
    if (this.dgvInfo.Rows.Count != 0 || this.dgvValues.CurrentRow == null || !(this.dgvValues.CurrentRow.DataBoundItem is DataRowView dataBoundItem))
      return;
    DataRow row = dataBoundItem.Row;
    this._dtMaster.BeginLoadData();
    row.Delete();
    this._dtMaster.AcceptChanges();
    this._dtMaster.EndLoadData();
  }

  private void RemoveEtalonRow()
  {
    this._dtDetail.BeginLoadData();
    this._dtDetail.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x[IndexesField.F_LINK_ID]) == this.Current.LinkId && Convert.ToInt64(x[IndexesField.F_TABKEY]) == this.Current.RecordId)).ToList<DataRow>().ForEach((Action<DataRow>) (x => x.Delete()));
    this._dtDetail.AcceptChanges();
    this._dtDetail.EndLoadData();
  }

  private void RemoveDuplicateRow(RecordInfo duplicate)
  {
    this.dgvInfo.SuspendLayout();
    this._dtDetail.BeginLoadData();
    this._dtDetail.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x[IndexesField.F_LINK_ID]) == duplicate.LinkId && Convert.ToInt64(x[IndexesField.F_TABKEY]) == duplicate.RecordId)).ToList<DataRow>().ForEach((Action<DataRow>) (x => x.Delete()));
    this._dtDetail.AcceptChanges();
    this._dtDetail.EndLoadData();
    this.dgvInfo.ResumeLayout();
  }

  private static void WriteOutputMessage(string msg)
  {
    IOutputView service = ServiceUtils.GetService<IOutputView>((object) ServicesManager.ServiceContainer, true);
    service.ClearText(LocalizationHolder.rm.GetString("MergeRecords"));
    service.WriteString(LocalizationHolder.rm.GetString("MergeRecords"), msg);
    service.Activate(LocalizationHolder.rm.GetString("MergeRecords"));
  }

  public void Fill(DataTable dt)
  {
    this._loading = true;
    try
    {
      if (dt == null)
        return;
      this.Clear();
      this._dtMaster.BeginLoadData();
      List<string> stringList = new List<string>(dt.Rows.Count / 2);
      foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
      {
        string str = row[IndexesField.F_TEXT].ToString();
        int num = stringList.BinarySearch(str);
        if (num < 0)
        {
          this._dtMaster.Rows.Add((object) str, row[IndexesField.F_HASHTEXT]);
          stringList.Insert(~num, str);
        }
      }
      this._dtMaster.AcceptChanges();
      this._dtMaster.EndLoadData();
      this.dgvInfo.AutoGenerateColumns = false;
      this.dgvValues.AutoGenerateColumns = false;
      this._dtDetail = dt;
      this._detailDataView = this._dtDetail.DefaultView;
      this._detailDataView.RowFilter = "0=1";
      this.dgvValues.DataSource = (object) this._dtMaster.DefaultView;
      this.dgvInfo.DataSource = (object) this._detailDataView;
      this.dgvValues.Sort(this.dgvValues.Columns[0], ListSortDirection.Ascending);
      this.SetLabelCountText();
    }
    finally
    {
      this._loading = false;
    }
    this.dgvValues_SelectionChanged((object) this, new EventArgs());
  }

  private void tbValuesFilter_TextChanged(object sender, EventArgs e)
  {
    if (this._loading)
      return;
    string str = this.tbValuesFilter.Text;
    if (!string.IsNullOrEmpty(str))
      str = $"[F_TEXT] LIKE('*{this.EscapeString(str)}*')";
    this._dtMaster.DefaultView.RowFilter = str;
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
    this.panel1 = new Panel();
    this._btnUnique = new Button();
    this._lb = new Label();
    this._lbCaption = new Label();
    this.spltContainer = new SplitContainer();
    this.dgvValues = new DataGridView();
    this.fTEXTMaster = new DataGridViewTextBoxColumn();
    this.tbValuesFilter = new TextBox();
    this.dgvInfo = new DataGridView();
    this.dataSet = new DataSet();
    this._dtMaster = new DataTable();
    this.fUniqueText = new DataColumn();
    this.dataColumn1 = new DataColumn();
    this.cmStrip = new ContextMenuStrip(this.components);
    this.miUnite = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.miGoToTable = new ToolStripMenuItem();
    this.miOpenObjInNewWindow = new ToolStripMenuItem();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.CheckedColumn = new DataGridViewCheckBoxColumn();
    this.F_TABKEY = new DataGridViewTextBoxColumn();
    this.fTEXTDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.fTABLEIDDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
    this.fLINKIDDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
    this.fFULLPATHDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
    this.panel1.SuspendLayout();
    this.spltContainer.BeginInit();
    this.spltContainer.Panel1.SuspendLayout();
    this.spltContainer.Panel2.SuspendLayout();
    this.spltContainer.SuspendLayout();
    ((ISupportInitialize) this.dgvValues).BeginInit();
    ((ISupportInitialize) this.dgvInfo).BeginInit();
    this.dataSet.BeginInit();
    this._dtMaster.BeginInit();
    this.cmStrip.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this._btnUnique);
    this.panel1.Controls.Add((Control) this._lb);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 372);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(639, 29);
    this.panel1.TabIndex = 6;
    this._btnUnique.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._btnUnique.Enabled = false;
    this._btnUnique.ImeMode = ImeMode.NoControl;
    this._btnUnique.Location = new Point(561, 3);
    this._btnUnique.Name = "_btnUnique";
    this._btnUnique.Size = new Size(75, 23);
    this._btnUnique.TabIndex = 7;
    this._btnUnique.Text = "Проверить";
    this._btnUnique.UseVisualStyleBackColor = true;
    this._btnUnique.Click += new EventHandler(this.On_btnUnique_Click);
    this._lb.AutoSize = true;
    this._lb.Location = new Point(3, 8);
    this._lb.Name = "_lb";
    this._lb.Size = new Size(151, 13);
    this._lb.TabIndex = 1;
    this._lb.Text = "Общее количество записей:";
    this._lbCaption.Dock = DockStyle.Top;
    this._lbCaption.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this._lbCaption.Location = new Point(0, 0);
    this._lbCaption.Name = "_lbCaption";
    this._lbCaption.Size = new Size(639, 29);
    this._lbCaption.TabIndex = 7;
    this._lbCaption.Text = "Неуникальные значения индекса";
    this._lbCaption.TextAlign = ContentAlignment.MiddleLeft;
    this.spltContainer.Dock = DockStyle.Fill;
    this.spltContainer.Location = new Point(0, 29);
    this.spltContainer.Name = "spltContainer";
    this.spltContainer.Panel1.Controls.Add((Control) this.dgvValues);
    this.spltContainer.Panel1.Controls.Add((Control) this.tbValuesFilter);
    this.spltContainer.Panel2.Controls.Add((Control) this.dgvInfo);
    this.spltContainer.Size = new Size(639, 343);
    this.spltContainer.SplitterDistance = 121;
    this.spltContainer.TabIndex = 8;
    this.dgvValues.AllowUserToAddRows = false;
    this.dgvValues.AllowUserToDeleteRows = false;
    this.dgvValues.AllowUserToResizeRows = false;
    this.dgvValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    this.dgvValues.BackgroundColor = SystemColors.Window;
    this.dgvValues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgvValues.Columns.AddRange((DataGridViewColumn) this.fTEXTMaster);
    this.dgvValues.Dock = DockStyle.Fill;
    this.dgvValues.EditMode = DataGridViewEditMode.EditProgrammatically;
    this.dgvValues.Location = new Point(0, 0);
    this.dgvValues.MultiSelect = false;
    this.dgvValues.Name = "dgvValues";
    this.dgvValues.ReadOnly = true;
    this.dgvValues.RowHeadersVisible = false;
    this.dgvValues.Size = new Size(121, 323);
    this.dgvValues.TabIndex = 1;
    this.dgvValues.SelectionChanged += new EventHandler(this.dgvValues_SelectionChanged);
    this.fTEXTMaster.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.fTEXTMaster.DataPropertyName = "F_TEXT";
    this.fTEXTMaster.HeaderText = "Значение";
    this.fTEXTMaster.Name = "fTEXTMaster";
    this.fTEXTMaster.ReadOnly = true;
    this.fTEXTMaster.ToolTipText = "Значение";
    this.tbValuesFilter.Dock = DockStyle.Bottom;
    this.tbValuesFilter.Location = new Point(0, 323);
    this.tbValuesFilter.Name = "tbValuesFilter";
    this.tbValuesFilter.Size = new Size(121, 20);
    this.tbValuesFilter.TabIndex = 8;
    this.tbValuesFilter.TextChanged += new EventHandler(this.tbValuesFilter_TextChanged);
    this.dgvInfo.AllowUserToAddRows = false;
    this.dgvInfo.AllowUserToDeleteRows = false;
    this.dgvInfo.AllowUserToResizeRows = false;
    this.dgvInfo.BackgroundColor = SystemColors.Window;
    this.dgvInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
    this.dgvInfo.Columns.AddRange((DataGridViewColumn) this.CheckedColumn, (DataGridViewColumn) this.F_TABKEY, (DataGridViewColumn) this.fTEXTDataGridViewTextBoxColumn1, (DataGridViewColumn) this.fTABLEIDDataGridViewTextBoxColumn, (DataGridViewColumn) this.fLINKIDDataGridViewTextBoxColumn, (DataGridViewColumn) this.fFULLPATHDataGridViewTextBoxColumn);
    this.dgvInfo.Dock = DockStyle.Fill;
    this.dgvInfo.EditMode = DataGridViewEditMode.EditOnEnter;
    this.dgvInfo.Location = new Point(0, 0);
    this.dgvInfo.MultiSelect = false;
    this.dgvInfo.Name = "dgvInfo";
    this.dgvInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dgvInfo.ShowEditingIcon = false;
    this.dgvInfo.Size = new Size(514, 343);
    this.dgvInfo.TabIndex = 2;
    this.dgvInfo.CellPainting += new DataGridViewCellPaintingEventHandler(this.dgvInfo_CellPainting);
    this.dgvInfo.RowLeave += new DataGridViewCellEventHandler(this.dgvInfo_RowLeave);
    this.dgvInfo.SelectionChanged += new EventHandler(this.dgvInfo_SelectionChanged);
    this.dgvInfo.MouseDown += new MouseEventHandler(this.dgvInfo_MouseDown);
    this.dataSet.DataSetName = "IndexData";
    this.dataSet.Tables.AddRange(new DataTable[1]
    {
      this._dtMaster
    });
    this._dtMaster.Columns.AddRange(new DataColumn[2]
    {
      this.fUniqueText,
      this.dataColumn1
    });
    this._dtMaster.TableName = "dtMaster";
    this.fUniqueText.Caption = "F_TEXT";
    this.fUniqueText.ColumnName = "F_TEXT";
    this.fUniqueText.ReadOnly = true;
    this.dataColumn1.ColumnName = "F_HASHTEXT";
    this.cmStrip.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.miUnite,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.miGoToTable,
      (ToolStripItem) this.miOpenObjInNewWindow
    });
    this.cmStrip.Name = "cmStrip";
    this.cmStrip.Size = new Size(240 /*0xF0*/, 76);
    this.cmStrip.Opening += new CancelEventHandler(this.cmStrip_Opening);
    this.miUnite.Name = "miUnite";
    this.miUnite.Size = new Size(239, 22);
    this.miUnite.Text = "Объединить";
    this.miUnite.Click += new EventHandler(this.miUnite_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(236, 6);
    this.miGoToTable.Name = "miGoToTable";
    this.miGoToTable.Size = new Size(239, 22);
    this.miGoToTable.Text = "Перейти в таблицу";
    this.miGoToTable.Click += new EventHandler(this.miGoToTable_Click);
    this.miOpenObjInNewWindow.Name = "miOpenObjInNewWindow";
    this.miOpenObjInNewWindow.Size = new Size(239, 22);
    this.miOpenObjInNewWindow.Text = "Открыть объект в новом окне";
    this.miOpenObjInNewWindow.Click += new EventHandler(this.miOpenObjInNewWindow_Click);
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn1.DataPropertyName = "F_TEXT";
    this.dataGridViewTextBoxColumn1.HeaderText = "F_TEXT";
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn1.ToolTipText = "Значение";
    this.CheckedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
    this.CheckedColumn.Frozen = true;
    this.CheckedColumn.HeaderText = "Дубль";
    this.CheckedColumn.Name = "CheckedColumn";
    this.CheckedColumn.ToolTipText = "Запись дубликат";
    this.CheckedColumn.Width = 45;
    this.F_TABKEY.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
    this.F_TABKEY.DataPropertyName = "F_TABKEY";
    this.F_TABKEY.HeaderText = "ID Записи";
    this.F_TABKEY.Name = "F_TABKEY";
    this.F_TABKEY.ReadOnly = true;
    this.F_TABKEY.Width = 83;
    this.fTEXTDataGridViewTextBoxColumn1.DataPropertyName = "F_TEXT";
    this.fTEXTDataGridViewTextBoxColumn1.HeaderText = "F_TEXT";
    this.fTEXTDataGridViewTextBoxColumn1.Name = "fTEXTDataGridViewTextBoxColumn1";
    this.fTEXTDataGridViewTextBoxColumn1.ReadOnly = true;
    this.fTEXTDataGridViewTextBoxColumn1.Visible = false;
    this.fTABLEIDDataGridViewTextBoxColumn.DataPropertyName = "F_TABLE_ID";
    this.fTABLEIDDataGridViewTextBoxColumn.HeaderText = "ID таблицы";
    this.fTABLEIDDataGridViewTextBoxColumn.Name = "fTABLEIDDataGridViewTextBoxColumn";
    this.fTABLEIDDataGridViewTextBoxColumn.ReadOnly = true;
    this.fTABLEIDDataGridViewTextBoxColumn.ToolTipText = "Идентификатор таблицы";
    this.fLINKIDDataGridViewTextBoxColumn.DataPropertyName = "F_LINK_ID";
    this.fLINKIDDataGridViewTextBoxColumn.HeaderText = "ID ярлыка";
    this.fLINKIDDataGridViewTextBoxColumn.Name = "fLINKIDDataGridViewTextBoxColumn";
    this.fLINKIDDataGridViewTextBoxColumn.ReadOnly = true;
    this.fLINKIDDataGridViewTextBoxColumn.ToolTipText = "Идентификатор ярлыка";
    this.fFULLPATHDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.fFULLPATHDataGridViewTextBoxColumn.DataPropertyName = "F_FULL_PATH";
    this.fFULLPATHDataGridViewTextBoxColumn.HeaderText = "Полный путь";
    this.fFULLPATHDataGridViewTextBoxColumn.Name = "fFULLPATHDataGridViewTextBoxColumn";
    this.fFULLPATHDataGridViewTextBoxColumn.ReadOnly = true;
    this.fFULLPATHDataGridViewTextBoxColumn.ToolTipText = "Полный путь к записи";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.spltContainer);
    this.Controls.Add((Control) this._lbCaption);
    this.Controls.Add((Control) this.panel1);
    this.MinimumSize = new Size(350, 150);
    this.Name = nameof (NotUniqueRecordsCtrl);
    this.Size = new Size(639, 401);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.spltContainer.Panel1.ResumeLayout(false);
    this.spltContainer.Panel1.PerformLayout();
    this.spltContainer.Panel2.ResumeLayout(false);
    this.spltContainer.EndInit();
    this.spltContainer.ResumeLayout(false);
    ((ISupportInitialize) this.dgvValues).EndInit();
    ((ISupportInitialize) this.dgvInfo).EndInit();
    this.dataSet.EndInit();
    this._dtMaster.EndInit();
    this.cmStrip.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public delegate void GetNotUniqueEventHandler(int attrID, DataTable dtNotUnique);
}
