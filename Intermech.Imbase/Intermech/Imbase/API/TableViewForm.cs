// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.TableViewForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Controls;
using Intermech.Imbase.Selection;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.API;

public class TableViewForm : Form
{
  private DataTable _hierTable;
  private List<long> _idsList;
  private Dictionary<long, TreeNode> _nodes;
  private string _filter;
  private string _showFields;
  private string _sortOrder;
  private string _comment;
  private string _locateString = string.Empty;
  private string _locateField = string.Empty;
  private IContainer components;
  private Button cancelButton;
  private Button okButton;
  private SplitContainer _splitContainer;
  private TableView tableView;
  private TreeBuilder treeBuilder;
  private TreeView treeView;
  private Label label1;

  public TableViewForm()
  {
    this.InitializeComponent();
    this.tableView.Grid.DoubleClick += new EventHandler(this.Grid_DoubleClick);
    this.tableView.FocusedChanged += new EventHandler(this.tableView_FocusedChanged);
    this._nodes = new Dictionary<long, TreeNode>(32 /*0x20*/);
    this.tableView.DisableViewSettingMenuItem();
  }

  private void tableView_FocusedChanged(object sender, EventArgs e)
  {
    this.okButton.Enabled = !this.tableView.DisabledRecord();
  }

  internal void InitializeView(
    string objectDef,
    string catalogDef,
    string filter,
    string showFields,
    string sortOrder,
    string comment)
  {
    IntPtr handle = this.tableView.Handle;
    long recordId;
    long num = TableViewForm.ResolveObjectDef(objectDef, catalogDef, out this._idsList, out recordId, out this._hierTable);
    this._filter = filter;
    this._showFields = showFields;
    this._sortOrder = sortOrder;
    this._comment = comment;
    this.tableView.ObjectId = num != -1L ? num : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_8"), (object) catalogDef, (object) objectDef));
    this.tableView.RecordId = recordId;
    this.tableView.DisableViewSettingMenuItem();
    if (this._hierTable != null)
    {
      this.treeBuilder.CreateTree(this._hierTable, (IDictionary<long, TreeNode>) this._nodes);
      TableFolders.SelectFirstRefNode(this.treeView);
      this._splitContainer.Panel1Collapsed = false;
    }
    else
    {
      this._nodes.Clear();
      this._splitContainer.Panel1Collapsed = true;
    }
    this.ApplyFilterAndSort();
    if (num == -1L)
      return;
    this.tableView.RecordId = recordId;
  }

  private void ApplyFilterAndSort()
  {
    this.tableView.Filter = this._filter;
    this.tableView.ApplyShowFields(this._showFields, this._sortOrder);
    this.label1.Text = this.ExtractLocateFilter(this._comment);
  }

  private string ExtractLocateFilter(string comments)
  {
    List<string> filters = new List<string>(4);
    string[] strArray = comments.Split(new char[2]
    {
      '\n',
      '\r'
    }, StringSplitOptions.RemoveEmptyEntries);
    StringBuilder stringBuilder = new StringBuilder(comments.Length);
    int length = strArray.Length;
    for (int index = 0; index < length; ++index)
    {
      if (strArray[index].StartsWith("@"))
      {
        string str = strArray[index].Substring(1);
        if (!filters.Contains(str))
          filters.Add(str);
      }
      else
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(strArray[index]);
      }
    }
    if (filters.Count > 0)
      this.PrepareFilter(filters);
    return stringBuilder.ToString();
  }

  private void PrepareFilter(List<string> filters)
  {
    string empty = string.Empty;
    int count = filters.Count;
    for (int index = 0; index < count; ++index)
    {
      if (empty.Length > 0)
        empty += " AND ";
      empty += $"({filters[index]})";
    }
    if (empty.Length <= 0)
      return;
    this.tableView.LocateByFilter(empty);
  }

  internal void InitializeView(long linkId) => this.tableView.ObjectId = linkId;

  private void Grid_DoubleClick(object sender, EventArgs e)
  {
    if (!this.okButton.Enabled)
      return;
    this.okButton.PerformClick();
  }

  internal static long ResolveObjectDef(
    string objectDef,
    string catalogDef,
    out List<long> ids,
    out long recordId,
    out DataTable tree)
  {
    recordId = -1L;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IImbaseServer customService = sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
      long linkId;
      if (!CadmechHelper.IsImbaseKey(catalogDef, out linkId, out recordId, customService, sessionKeeper.Session))
        return customService.ResolveObjectDef(sessionKeeper.Session.SessionGUID, objectDef, catalogDef, out ids, out tree);
      ids = new List<long>(1);
      ids.Add(linkId);
      tree = (DataTable) null;
      return linkId;
    }
  }

  internal int GetData(
    ref DataTable records,
    ref FieldInfo[] fields,
    ref ContextInfo context,
    int recordCount,
    ref long recordKey)
  {
    List<FieldInfo> fieldInfoList = new List<FieldInfo>();
    TableView tableView = this.tableView;
    AttributeTypeProperties[] rowAttProps = tableView.RowAttProps;
    int[] columnsOrder = tableView.ColumnsOrder;
    DataView dataView = tableView.DataView;
    recordKey = tableView.RecordId;
    if (recordCount == 0)
    {
      records = dataView.ToTable("rows");
      if (records.Columns.Contains("F_APPLICABILITY"))
      {
        foreach (DataRow dataRow in records.Select($"{"F_APPLICABILITY"}={(System.ValueType) false}"))
          dataRow.Delete();
        records.AcceptChanges();
      }
    }
    else
    {
      records = tableView.Table.Clone();
      DataRow[] dataRowArray = tableView.Table.Select("[-2]=" + recordKey.ToString());
      records.Rows.Add(dataRowArray[0].ItemArray);
    }
    records.RemotingFormat = SerializationFormat.Binary;
    context.CatalogId = -1L;
    context.LinkId = tableView.LinkId;
    context.TableId = tableView.TableId;
    context.TableName = tableView.TableName;
    List<int> intList = new List<int>();
    foreach (AttributeTypeProperties prop in rowAttProps)
    {
      DataColumn column = records.Columns[prop.AttributeID.ToString()];
      if (column != null)
      {
        FieldInfo fieldInfo = new FieldInfo()
        {
          LongName = prop.Name,
          ShortName = prop.ShortName,
          AttributeId = prop.AttributeID,
          Units = column.ExtendedProperties.ContainsKey((object) "F_MEASURE_U") ? Convert.ToString(column.ExtendedProperties[(object) "F_MEASURE_U"]) : string.Empty,
          Required = false,
          Flags = (int) (prop.Options & (AttributeOptions.ImbaseFlag_SEARCH | AttributeOptions.ImbaseFlag_AVS | AttributeOptions.ImbaseFlag_CADMECH_T | AttributeOptions.ImbaseFlag_CADMECH | AttributeOptions.ImbaseFlag_CADPROPERTY)),
          FieldKind = prop.Computed == ComputeValueModes.NotComputableValue ? FieldKind.Data : FieldKind.Calculated
        };
        fieldInfo.FieldType = this.ConvertFieldType(prop, column, ref fieldInfo);
        fieldInfoList.Add(fieldInfo);
        if (prop.FieldType == FieldTypes.ftObjectLink)
          intList.Add(prop.AttributeID);
      }
    }
    int num1 = -2;
    if (records.Columns[num1.ToString()] != null)
    {
      FieldInfo fieldInfo = new FieldInfo();
      fieldInfo.ShortName = "F_GUID";
      fieldInfo.AttributeId = -12;
      fieldInfo.Units = "";
      fieldInfo.Required = true;
      fieldInfo.FieldKind = FieldKind.Data;
      fieldInfo.FieldType = FieldType.String;
      fieldInfo.DataSize = 36;
      fieldInfoList.Insert(0, fieldInfo);
      fieldInfo = new FieldInfo();
      fieldInfo.ShortName = "F_KEY";
      fieldInfo.AttributeId = num1;
      fieldInfo.Units = "";
      fieldInfo.Required = true;
      fieldInfo.FieldKind = FieldKind.Data;
      fieldInfo.FieldType = FieldType.Integer;
      fieldInfo.DataSize = 4;
      fieldInfoList.Insert(0, fieldInfo);
    }
    fields = fieldInfoList.ToArray();
    if (columnsOrder != null)
      Array.Sort<FieldInfo>(fields, (IComparer<FieldInfo>) new TableViewForm.FieldsInfoComparer(columnsOrder));
    if (intList.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IImbaseServer customService = session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
        foreach (DataRow row1 in (InternalDataCollectionBase) records.Rows)
        {
          foreach (int num2 in intList)
          {
            DataColumn column = records.Columns[num2.ToString()];
            if (column != null)
            {
              string sguid = Convert.ToString(row1[column]);
              Guid guid;
              if (!string.IsNullOrEmpty(sguid) && ImbaseHelper.IsGuid(sguid, out guid))
              {
                QuickObjectInfo objectInfo = session.GetObjectInfo(guid);
                if (!objectInfo.Empty && objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseFolderTypeID)
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  DataTable foldersForObjects = customService.GetFoldersForObjects(session.SessionGUID, new long[1]
                  {
                    objectInfo.ObjectID
                  }, (long[]) null);
                  foldersForObjects.DefaultView.Sort = "[F_PATH]";
                  for (int recordIndex = 0; recordIndex < foldersForObjects.DefaultView.Count; ++recordIndex)
                  {
                    DataRow row2 = foldersForObjects.DefaultView[recordIndex].Row;
                    if (recordIndex == 0)
                      stringBuilder.Append($"{Convert.ToString(row2[0])}.{objectInfo.ObjectID}|\\");
                    stringBuilder.Append('\\');
                    stringBuilder.Append(Convert.ToString(row2[2]));
                  }
                  row1[column] = (object) stringBuilder.ToString();
                }
              }
            }
          }
        }
      }
      records.AcceptChanges();
    }
    return 1;
  }

  internal void ExtractLocateField(string dia)
  {
    int length = dia.IndexOf('=');
    if (length != -1)
    {
      this._locateString = dia.Substring(length + 1);
      this._locateField = dia.Substring(0, length);
    }
    else
    {
      this._locateString = dia;
      this._locateField = string.Empty;
    }
  }

  private void PatchMeasuredColumns(DataTable records, AttributeTypeProperties[] rowProps)
  {
    int length = rowProps.Length;
    List<string> stringList = new List<string>();
    for (int index = 0; index < length; ++index)
    {
      AttributeTypeProperties rowProp = rowProps[index];
      if (rowProp.FieldType == FieldTypes.ftMeasured)
      {
        string str = rowProp.AttributeID.ToString();
        stringList.Add(str);
        records.Columns[str].ColumnName = "-" + str;
        records.Columns.Add(str, typeof (double));
      }
    }
    int count = stringList.Count;
    if (count <= 0)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) records.Rows)
    {
      for (int index = 0; index < count; ++index)
      {
        string columnName = stringList[index];
        string s = Convert.ToString(row["-" + columnName]);
        double result;
        row[columnName] = double.TryParse(s, out result) || double.TryParse(s.Replace(',', '.'), out result) ? (object) result : (object) 0.0;
      }
    }
    for (int index = 0; index < count; ++index)
    {
      string str = stringList[index];
      records.Columns.Remove("-" + str);
    }
  }

  private FieldType ConvertFieldType(
    AttributeTypeProperties prop,
    DataColumn column,
    ref FieldInfo fieldInfo)
  {
    if (column == null)
      return FieldType.Unknown;
    System.Type dataType = column.DataType;
    if (dataType.Equals(typeof (long)) || dataType.Equals(typeof (ulong)))
    {
      fieldInfo.DataSize = Marshal.SizeOf(dataType);
      return FieldType.Largeint;
    }
    if (dataType.Equals(typeof (int)) || dataType.Equals(typeof (uint)))
    {
      fieldInfo.DataSize = Marshal.SizeOf(dataType);
      return FieldType.Integer;
    }
    if (dataType.Equals(typeof (short)) || dataType.Equals(typeof (ushort)))
    {
      fieldInfo.DataSize = Marshal.SizeOf(dataType);
      return FieldType.Smallint;
    }
    if (dataType.Equals(typeof (bool)))
    {
      fieldInfo.DataSize = Marshal.SizeOf(typeof (short));
      return FieldType.Boolean;
    }
    if (dataType.Equals(typeof (double)) || dataType.Equals(typeof (Decimal)))
    {
      fieldInfo.DataSize = Marshal.SizeOf(typeof (double));
      return FieldType.Float;
    }
    fieldInfo.DataSize = 0;
    return FieldType.String;
  }

  [Browsable(false)]
  internal TableView TableView => this.tableView;

  internal bool HasTree => !this._splitContainer.Panel1Collapsed;

  private void TableViewForm_Shown(object sender, EventArgs e)
  {
    this.Activate();
    TableViewForm.SetForeWindow(this.Handle);
    this.TableViewForm_Resize((object) this, EventArgs.Empty);
  }

  internal DialogResult ShowOnlyTree()
  {
    this._splitContainer.Panel2Collapsed = true;
    this.Height /= 2;
    this.Width /= 2;
    return this.ShowDialog();
  }

  internal static IntPtr SetForeWindow(IntPtr hWnd)
  {
    IntPtr foregroundWindow = TableViewForm.GetForegroundWindow();
    if (foregroundWindow != IntPtr.Zero)
    {
      int currentThreadId = (int) TableViewForm.GetCurrentThreadId();
      uint windowThreadProcessId = TableViewForm.GetWindowThreadProcessId(foregroundWindow, out uint _);
      TableViewForm.AttachThreadInput((uint) currentThreadId, windowThreadProcessId, true);
      TableViewForm.SetForegroundWindow(hWnd);
      TableViewForm.AttachThreadInput((uint) currentThreadId, windowThreadProcessId, false);
    }
    else
      TableViewForm.SetForegroundWindow(hWnd);
    return foregroundWindow;
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool SetForegroundWindow(IntPtr hWnd);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern IntPtr GetForegroundWindow();

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern uint GetWindowThreadProcessId(IntPtr handle, out uint processId);

  [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  internal static extern uint GetCurrentThreadId();

  private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    TreeNode selectedNode = this.treeView.SelectedNode;
    if (this._nodes.ContainsValue(selectedNode))
    {
      NodeInfo nodeInfo = (NodeInfo) null;
      if (selectedNode != null)
        nodeInfo = selectedNode.Tag as NodeInfo;
      if (nodeInfo != null && nodeInfo.IsTableReference)
      {
        this.tableView.ObjectId = nodeInfo.ObjectId;
        this.okButton.Enabled = true;
        this.ApplyFilterAndSort();
        return;
      }
    }
    this.okButton.Enabled = false;
  }

  private void TableViewForm_Resize(object sender, EventArgs e)
  {
    this._splitContainer.Bounds = this.ClientRectangle with
    {
      Height = this.label1.Top - 4
    };
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TableViewForm));
    this._splitContainer = new SplitContainer();
    this.treeView = new TreeView();
    this.tableView = new TableView();
    this.cancelButton = new Button();
    this.okButton = new Button();
    this.treeBuilder = new TreeBuilder(this.components);
    this.label1 = new Label();
    this._splitContainer.BeginInit();
    this._splitContainer.Panel1.SuspendLayout();
    this._splitContainer.Panel2.SuspendLayout();
    this._splitContainer.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._splitContainer, "_splitContainer");
    this._splitContainer.Name = "_splitContainer";
    this._splitContainer.Panel1.Controls.Add((Control) this.treeView);
    this._splitContainer.Panel2.Controls.Add((Control) this.tableView);
    this._splitContainer.TabStop = false;
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.HideSelection = false;
    this.treeView.Name = "treeView";
    this.treeView.Sorted = true;
    this.treeView.AfterSelect += new TreeViewEventHandler(this.TreeView_AfterSelect);
    componentResourceManager.ApplyResources((object) this.tableView, "tableView");
    this.tableView.Filter = "";
    this.tableView.FollowSelectMode = ImFollowSelectMode.imfsmFirstRow;
    this.tableView.Name = "tableView";
    this.tableView.RecordId = -1L;
    componentResourceManager.ApplyResources((object) this.cancelButton, "cancelButton");
    this.cancelButton.DialogResult = DialogResult.Cancel;
    this.cancelButton.Name = "cancelButton";
    this.cancelButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.okButton, "okButton");
    this.okButton.DialogResult = DialogResult.OK;
    this.okButton.Name = "okButton";
    this.okButton.UseVisualStyleBackColor = true;
    this.treeBuilder.Catalogs = new long[0];
    this.treeBuilder.Checked = new long[0];
    this.treeBuilder.TreeView = this.treeView;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.AutoEllipsis = true;
    this.label1.BackColor = SystemColors.Control;
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._splitContainer);
    this.Controls.Add((Control) this.okButton);
    this.Controls.Add((Control) this.cancelButton);
    this.DoubleBuffered = true;
    this.MinimizeBox = false;
    this.Name = nameof (TableViewForm);
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Shown += new EventHandler(this.TableViewForm_Shown);
    this.Resize += new EventHandler(this.TableViewForm_Resize);
    this._splitContainer.Panel1.ResumeLayout(false);
    this._splitContainer.Panel2.ResumeLayout(false);
    this._splitContainer.EndInit();
    this._splitContainer.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class FieldsInfoComparer : IComparer<FieldInfo>
  {
    private int[] _orders;
    private int _sz;

    public FieldsInfoComparer(int[] orders)
    {
      this._orders = orders;
      this._sz = orders.Length;
    }

    public int Compare(FieldInfo x, FieldInfo y)
    {
      if (x.AttributeId == y.AttributeId)
        return 0;
      if (x.AttributeId == -2)
        return -1;
      if (y.AttributeId == -2)
        return 1;
      int index1 = this.GetIndex(x.AttributeId);
      int index2 = this.GetIndex(y.AttributeId);
      if (index1 == -1 && index2 == -1)
        return x.AttributeId - y.AttributeId;
      if (index1 == -1)
        return 1;
      return index2 == -1 ? -1 : index1 - index2;
    }

    private int GetIndex(int attId)
    {
      for (int index = 0; index < this._sz; ++index)
      {
        if (this._orders[index] == attId)
          return index;
      }
      return -1;
    }
  }
}
