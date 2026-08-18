// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.UniqueIndexesView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Imbase.Editors;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class UniqueIndexesView : DockControl, IImbaseView
{
  private List<int> _indexes;
  private DataTable _dtIndexesData;
  private Dictionary<int, string> _attrsIDsNames;
  private Dictionary<int, FieldTypes> _attrsIDsTypes;
  private IContainer components;
  private System.Windows.Forms.TabPage tp;
  private System.Windows.Forms.TabControl _tabControl;
  private SplitContainer _splitContainer;
  private ListView lvValues;
  private ColumnHeader colHeader;
  private ListView lvRecords;
  private ColumnHeader colTablesName;
  private Panel pnlBottom;
  private Button btnMove;
  private ColumnHeader colFullPath;

  public UniqueIndexesView()
  {
    this.InitializeComponent();
    this._tabControl.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    this.lvValues.Columns[0].Width = -2;
    this.lvRecords.Columns[1].Width = -2;
  }

  public static void Show(List<int> dtIndexes, DataTable dtindexesData)
  {
    if (!(ServicesManager.GetService(typeof (DockManager)) is DockManager service))
      return;
    UniqueIndexesView uniqueIndexesView = new UniqueIndexesView();
    uniqueIndexesView.SetData(dtIndexes, dtindexesData);
    uniqueIndexesView.Show(service);
    uniqueIndexesView.Activate();
  }

  private void On_tabControl_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.lvValues.Items.Clear();
    this.lvRecords.Items.Clear();
    System.Windows.Forms.TabPage selectedTab = (sender as System.Windows.Forms.TabControl).SelectedTab;
    this._splitContainer.Parent = (Control) selectedTab;
    this.lvValues.Items.Clear();
    if (selectedTab.Tag != null)
    {
      this.lvValues.Items.AddRange((selectedTab.Tag as List<ListViewItem>).ToArray());
    }
    else
    {
      List<ListViewItem> listlvValuesItems = this.CreateListlvValuesItems(selectedTab.Name);
      selectedTab.Tag = (object) listlvValuesItems;
      this.lvValues.Items.AddRange(listlvValuesItems.ToArray());
    }
  }

  private void OnBeforeFirstShown(object sender, EventArgs e)
  {
    if (this._indexes == null || this._indexes.Count == 0)
      return;
    int index1 = this._indexes[0];
    this._tabControl.TabPages[0].Name = index1.ToString();
    this._tabControl.TabPages[0].Text = this.GetAttrsNameByID(index1);
    this._tabControl.TabPages[0].ImageIndex = Statics.IconSrv.IndexOf(3, -1, (object) (FieldTypes) (this._attrsIDsTypes.ContainsKey(index1) ? (int) this._attrsIDsTypes[index1] : 0));
    for (int index2 = 1; index2 < this._indexes.Count; ++index2)
    {
      System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage(this.GetAttrsNameByID(this._indexes[index2]));
      tabPage.Name = this._indexes[index2].ToString();
      tabPage.Padding = this._tabControl.TabPages[0].Padding;
      FieldTypes data = this._attrsIDsTypes.ContainsKey(index1) ? this._attrsIDsTypes[index1] : FieldTypes.ftUnknown;
      tabPage.ImageIndex = Statics.IconSrv.IndexOf(3, -1, (object) data);
      this._tabControl.TabPages.Add(tabPage);
    }
    List<ListViewItem> listlvValuesItems = this.CreateListlvValuesItems(this._tabControl.TabPages[0].Name);
    this._tabControl.TabPages[0].Tag = (object) listlvValuesItems;
    this.lvValues.Items.AddRange(listlvValuesItems.ToArray());
    if (this.lvValues.Items.Count <= 0)
      return;
    this.lvValues.Items[0].Selected = true;
  }

  private void OnbtnMove_Click(object sender, EventArgs e)
  {
    long int64 = Convert.ToInt64(this.lvRecords.SelectedItems[0].Tag);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(int64);
      if (dbObject == null)
        return;
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(dbObject.ObjectType);
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
      if (attributeById == null || attributeById.Value == null)
        return;
      string str = Convert.ToString(attributeById.Value);
      string conditionValue = str.Substring(0, str.Length - 2);
      if (string.IsNullOrEmpty(conditionValue))
        return;
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0, true)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      })
      {
        Contents = new ColumnContents[1]
        {
          ColumnContents.ID
        }
      });
      if (dataTable.Rows.Count == 0)
        return;
      EditorHelper.CreateEditor(int64, Convert.ToInt64(dataTable.Rows[0][0]), objectType.DefaultRelation).Show();
    }
  }

  private void OnlvRecords_Layout(object sender, LayoutEventArgs e)
  {
    this.lvRecords.Columns[1].Width = -2;
  }

  private void OnlvRecords_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.btnMove.Enabled = (sender as ListView).SelectedItems.Count > 0;
  }

  private void OnlvValues_Layout(object sender, LayoutEventArgs e)
  {
    this.lvValues.Columns[0].Width = -2;
  }

  private void OnlvValues_SelectedIndexChanged(object sender, EventArgs e)
  {
    if ((sender as ListView).SelectedItems.Count == 0)
      return;
    this.lvRecords.Items.Clear();
    ListViewItem selectedItem = (sender as ListView).SelectedItems[0];
    if (selectedItem.Tag == null)
    {
      DataRow[] dataRowArray = this._dtIndexesData.Select($"{IndexesField.F_TEXT}='{selectedItem.Text}'");
      if (dataRowArray.Length == 0)
        return;
      List<ListViewItem> listlvRecordsItems = this.CreateListlvRecordsItems(this._tabControl.SelectedTab.Name, dataRowArray[0][IndexesField.F_HASHTEXT].ToString());
      this.lvRecords.Items.AddRange(listlvRecordsItems.ToArray());
      selectedItem.Tag = (object) listlvRecordsItems;
    }
    else
      this.lvRecords.Items.AddRange((selectedItem.Tag as List<ListViewItem>).ToArray());
  }

  private List<ListViewItem> CreateListlvRecordsItems(string attrsID, string hashText)
  {
    if (this._dtIndexesData == null)
      return new List<ListViewItem>(0);
    DataRow[] dataRowArray = this._dtIndexesData.Select($"{IndexesField.F_ATTRIBUTE_ID}={attrsID} AND {IndexesField.F_HASHTEXT}='{hashText}'");
    List<string> stringList = new List<string>(dataRowArray.Length);
    List<ListViewItem> listlvRecordsItems = (List<ListViewItem>) null;
    DataTable dtSource = new DataTable();
    dtSource.Columns.AddRange(new DataColumn[3]
    {
      new DataColumn("Key"),
      new DataColumn("Path"),
      new DataColumn("LinksID")
    });
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string empty = string.Empty;
      foreach (DataRow dataRow in dataRowArray)
      {
        string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID(sessionKeeper.Session, Convert.ToInt64(dataRow[IndexesField.F_LINK_ID]));
        if (!stringList.Contains(classifKeyByObjId))
        {
          stringList.Add(classifKeyByObjId);
          IDBObject dbObject = sessionKeeper.Session.GetObject(Convert.ToInt64(dataRow[IndexesField.F_LINK_ID]));
          if (dbObject != null)
          {
            DataRow row = dtSource.NewRow();
            row["Key"] = (object) classifKeyByObjId;
            row["Path"] = (object) dbObject.Caption;
            row["LinksID"] = (object) dbObject.ObjectID;
            dtSource.Rows.Add(row);
          }
        }
      }
      string columnName = TableLoadHelper.BuildFullPathForObject(dtSource, sessionKeeper.Session);
      listlvRecordsItems = new List<ListViewItem>(dtSource.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dtSource.Rows)
        listlvRecordsItems.Add(new ListViewItem(Convert.ToString(row["Path"]))
        {
          SubItems = {
            row[columnName].ToString()
          },
          Tag = row["LinksID"]
        });
    }
    return listlvRecordsItems;
  }

  private List<ListViewItem> CreateListlvValuesItems(string attrsID)
  {
    if (this._dtIndexesData == null)
      return new List<ListViewItem>(0);
    DataRow[] dataRowArray = this._dtIndexesData.Select($"{IndexesField.F_ATTRIBUTE_ID}={attrsID}");
    if (dataRowArray.Length == 0)
      return new List<ListViewItem>(0);
    List<string> stringList = new List<string>(dataRowArray.Length);
    List<ListViewItem> listlvValuesItems = new List<ListViewItem>(dataRowArray.Length);
    string empty = string.Empty;
    foreach (DataRow dataRow in dataRowArray)
    {
      string str = Convert.ToString(dataRow[IndexesField.F_HASHTEXT]);
      if (!stringList.Contains(str))
      {
        stringList.Add(str);
        listlvValuesItems.Add(new ListViewItem(Convert.ToString(dataRow[IndexesField.F_TEXT])));
      }
    }
    return listlvValuesItems;
  }

  private string GetAttrsNameByID(int attrsID)
  {
    if (!this._attrsIDsNames.ContainsKey(attrsID))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrsID);
        if (attributeType == null)
          return string.Empty;
        this._attrsIDsTypes.Add(attrsID, attributeType.AttributeType);
        this._attrsIDsNames.Add(attrsID, attributeType.Name);
      }
    }
    return this._attrsIDsNames[attrsID];
  }

  private void SetData(List<int> indexes, DataTable dtIndexesData)
  {
    this._indexes = indexes;
    this._dtIndexesData = dtIndexesData;
    this._attrsIDsNames = new Dictionary<int, string>(indexes.Count);
    this._attrsIDsTypes = new Dictionary<int, FieldTypes>(indexes.Count);
  }

  public void FirstShown(object sender, EventArgs e) => this.OnBeforeFirstShown(sender, e);

  public void ViewClosing(object sender, CancelEventArgs e)
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UniqueIndexesView));
    this._splitContainer = new SplitContainer();
    this.lvValues = new ListView();
    this.colHeader = new ColumnHeader();
    this.lvRecords = new ListView();
    this.colTablesName = new ColumnHeader();
    this.colFullPath = new ColumnHeader();
    this.tp = new System.Windows.Forms.TabPage();
    this.pnlBottom = new Panel();
    this.btnMove = new Button();
    this._tabControl = new System.Windows.Forms.TabControl();
    this._splitContainer.BeginInit();
    this._splitContainer.Panel1.SuspendLayout();
    this._splitContainer.Panel2.SuspendLayout();
    this._splitContainer.SuspendLayout();
    this.tp.SuspendLayout();
    this.pnlBottom.SuspendLayout();
    this._tabControl.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._splitContainer, "_splitContainer");
    this._splitContainer.Name = "_splitContainer";
    this._splitContainer.Panel1.Controls.Add((Control) this.lvValues);
    this._splitContainer.Panel2.Controls.Add((Control) this.lvRecords);
    this.lvValues.Columns.AddRange(new ColumnHeader[1]
    {
      this.colHeader
    });
    componentResourceManager.ApplyResources((object) this.lvValues, "lvValues");
    this.lvValues.FullRowSelect = true;
    this.lvValues.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvValues.HideSelection = false;
    this.lvValues.MultiSelect = false;
    this.lvValues.Name = "lvValues";
    this.lvValues.ShowItemToolTips = true;
    this.lvValues.Sorting = SortOrder.Ascending;
    this.lvValues.UseCompatibleStateImageBehavior = false;
    this.lvValues.View = View.Details;
    this.lvValues.SelectedIndexChanged += new EventHandler(this.OnlvValues_SelectedIndexChanged);
    this.lvValues.Layout += new LayoutEventHandler(this.OnlvValues_Layout);
    componentResourceManager.ApplyResources((object) this.colHeader, "colHeader");
    this.lvRecords.Columns.AddRange(new ColumnHeader[2]
    {
      this.colTablesName,
      this.colFullPath
    });
    componentResourceManager.ApplyResources((object) this.lvRecords, "lvRecords");
    this.lvRecords.FullRowSelect = true;
    this.lvRecords.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvRecords.HideSelection = false;
    this.lvRecords.MultiSelect = false;
    this.lvRecords.Name = "lvRecords";
    this.lvRecords.ShowItemToolTips = true;
    this.lvRecords.Sorting = SortOrder.Ascending;
    this.lvRecords.UseCompatibleStateImageBehavior = false;
    this.lvRecords.View = View.Details;
    this.lvRecords.SelectedIndexChanged += new EventHandler(this.OnlvRecords_SelectedIndexChanged);
    this.lvRecords.DoubleClick += new EventHandler(this.OnbtnMove_Click);
    this.lvRecords.Layout += new LayoutEventHandler(this.OnlvRecords_Layout);
    componentResourceManager.ApplyResources((object) this.colTablesName, "colTablesName");
    componentResourceManager.ApplyResources((object) this.colFullPath, "colFullPath");
    this.tp.Controls.Add((Control) this._splitContainer);
    this.tp.Controls.Add((Control) this.pnlBottom);
    componentResourceManager.ApplyResources((object) this.tp, "tp");
    this.tp.Name = "tp";
    this.tp.UseVisualStyleBackColor = true;
    this.pnlBottom.Controls.Add((Control) this.btnMove);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    componentResourceManager.ApplyResources((object) this.btnMove, "btnMove");
    this.btnMove.Name = "btnMove";
    this.btnMove.UseVisualStyleBackColor = true;
    this.btnMove.Click += new EventHandler(this.OnbtnMove_Click);
    this._tabControl.Controls.Add((Control) this.tp);
    componentResourceManager.ApplyResources((object) this._tabControl, "_tabControl");
    this._tabControl.Name = "_tabControl";
    this._tabControl.SelectedIndex = 0;
    this._tabControl.SelectedIndexChanged += new EventHandler(this.On_tabControl_SelectedIndexChanged);
    this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._tabControl);
    this.DoubleBuffered = true;
    this.FloatingSize = new Size(766, 496);
    this.Name = nameof (UniqueIndexesView);
    this.PersistState = false;
    this.ShowImageInDocumentTab = true;
    this.BeforeFirstShown += new EventHandler(this.OnBeforeFirstShown);
    this._splitContainer.Panel1.ResumeLayout(false);
    this._splitContainer.Panel2.ResumeLayout(false);
    this._splitContainer.EndInit();
    this._splitContainer.ResumeLayout(false);
    this.tp.ResumeLayout(false);
    this.pnlBottom.ResumeLayout(false);
    this._tabControl.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
