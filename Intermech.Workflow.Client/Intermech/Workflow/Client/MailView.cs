// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailView
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Workflow.Design;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class MailView : UserControl, IView
{
  private NavigatorTreeView _treeView;
  private IContainer components;
  private EnhListView foldersView;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private ColumnHeader columnHeader3;

  public MailView()
  {
    this.InitializeComponent();
    this.foldersView.Sorting = SortOrder.None;
  }

  internal static long CountMail(MailType Type, bool countRead, bool countUnread)
  {
    return MailView.CountMail(Type, countRead, countUnread, ProcessPriority.Unreal);
  }

  internal static long CountMail(
    MailType Type,
    bool countRead,
    bool countUnread,
    ProcessPriority priority)
  {
    ColumnDescriptor[] columns = new ColumnDescriptor[0];
    ConditionStructure[] array = new ConditionStructure[0];
    switch (Type)
    {
      case MailType.Inbox:
        array = InboxNode.StaticConditions;
        break;
      case MailType.Sent:
        array = OutboxNode.StaticConditions;
        break;
      case MailType.Completed:
        array = CompletedNode.StaticConditions;
        break;
      case MailType.Trash:
        array = TrashNode.StaticConditions;
        break;
    }
    if (countRead && !countUnread)
    {
      Array.Resize<ConditionStructure>(ref array, array.Length + 1);
      array[array.Length - 1] = new ConditionStructure(wfConsts.AttrRecipStatusID, RelationalOperators.Equal, (object) 1, LogicalOperators.AND, 0, false);
    }
    if (countUnread && !countRead)
    {
      Array.Resize<ConditionStructure>(ref array, array.Length + 2);
      array[array.Length - 2] = new ConditionStructure(wfConsts.AttrRecipStatusID, RelationalOperators.Equal, (object) 0, LogicalOperators.OR, 1, false);
      array[array.Length - 1] = new ConditionStructure(wfConsts.AttrRecipStatusID, RelationalOperators.NotExistsOrEmpty, (object) 0, LogicalOperators.AND, -1, false);
    }
    if (priority != ProcessPriority.Unreal)
    {
      LogicalOperators logicalOperator = LogicalOperators.AND;
      if (priority == ProcessPriority.Normal)
        logicalOperator = LogicalOperators.OR;
      Array.Resize<ConditionStructure>(ref array, array.Length + 1);
      array[array.Length - 1] = new ConditionStructure(wfConsts.AttrPriorityID, RelationalOperators.Equal, (object) (int) priority, logicalOperator, logicalOperator == LogicalOperators.AND ? 0 : 1, false);
      if (logicalOperator == LogicalOperators.OR)
      {
        Array.Resize<ConditionStructure>(ref array, array.Length + 1);
        array[array.Length - 1] = new ConditionStructure(wfConsts.AttrPriorityID, RelationalOperators.NotExistsOrEmpty, (object) 0, LogicalOperators.AND, -1, false);
      }
    }
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(array, columns, recordCount: 0);
    if (dbRecordSetParams.Tags == null)
      dbRecordSetParams.Tags = new HybridDictionary();
    dbRecordSetParams.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesSelector();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.ObjectsSelect(wfConsts.ProcessAtomsTypeID, dbRecordSetParams);
      if (dataTable.Rows.Count > 0)
      {
        object obj = dataTable.Rows[0][0];
        if (!obj.Equals((object) DBNull.Value))
          return Convert.ToInt64(obj);
      }
    }
    return 0;
  }

  private long CountMail(MailType Type) => MailView.CountMail(Type, true, true);

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._treeView = (NavigatorTreeView) provider.GetService(typeof (NavigatorTreeView));
    Intermech.Workflow.Design.Holder.LastMailTree = this._treeView;
  }

  public void Activate(IView previousView)
  {
    this.foldersView.SmallImageList = BaseHolder.IconService.ImageList;
    this.foldersView.Items.Clear();
    ListViewItem listViewItem1 = this.foldersView.Items.Add(LocalizationHolder.rm.GetString("Workflow.Client_12"));
    listViewItem1.ImageIndex = BaseHolder.IconService.IndexOf(Intermech.Navigator.Consts.CategoryMailInbox, 0);
    long num = this.CountMail(MailType.Inbox);
    listViewItem1.Tag = (object) MailType.Inbox;
    listViewItem1.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) MailView.CountMail(MailType.Inbox, false, true)));
    listViewItem1.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) num));
    ListViewItem listViewItem2 = this.foldersView.Items.Add(LocalizationHolder.rm.GetString("Workflow.Client_14"));
    listViewItem2.ImageIndex = BaseHolder.IconService.IndexOf(Intermech.Navigator.Consts.CategoryMailOutbox, 0);
    listViewItem2.Tag = (object) MailType.Sent;
    listViewItem2.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) 0));
    listViewItem2.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) this.CountMail(MailType.Sent)));
    ListViewItem listViewItem3 = this.foldersView.Items.Add(LocalizationHolder.rm.GetString("Workflow.Client_13"));
    listViewItem3.ImageIndex = BaseHolder.IconService.IndexOf(Intermech.Navigator.Consts.CategoryMailProcessed, 0);
    listViewItem3.Tag = (object) MailType.Completed;
    listViewItem3.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) 0));
    listViewItem3.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) this.CountMail(MailType.Completed)));
    ListViewItem listViewItem4 = this.foldersView.Items.Add(LocalizationHolder.rm.GetString("Workflow.Client_15"));
    listViewItem4.ImageIndex = BaseHolder.IconService.IndexOf(Intermech.Navigator.Consts.CategoryMailTrash, 0);
    listViewItem4.Tag = (object) MailType.Trash;
    listViewItem4.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) 0));
    listViewItem4.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) this.CountMail(MailType.Trash)));
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => LocalizationHolder.rm.GetString("Workflow.Client_16");

  public int ImageIndex => -1;

  public int OrderID => 0;

  private void foldersView_ItemActivate(object sender, EventArgs e)
  {
    if (!(sender is ListView) || this._treeView == null)
      return;
    ListViewItem focusedItem = (sender as ListView).FocusedItem;
    if (focusedItem == null)
      return;
    NavigatorTreeNode focusedNode = this._treeView.FocusedNode;
    if (focusedNode == null)
      return;
    if (focusedNode.Children.Count == 0)
      focusedNode.Expanded = true;
    int int32 = Convert.ToInt32(focusedItem.Tag);
    if (int32 >= focusedNode.Children.Count)
      return;
    this._treeView.FocusedNode = focusedNode.Children[int32];
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MailView));
    this.foldersView = new EnhListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.SuspendLayout();
    this.foldersView.AccessibleDescription = (string) null;
    this.foldersView.AccessibleName = (string) null;
    this.foldersView.Activation = ItemActivation.OneClick;
    componentResourceManager.ApplyResources((object) this.foldersView, "foldersView");
    this.foldersView.BackgroundImage = (Image) null;
    this.foldersView.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnHeader1,
      this.columnHeader2,
      this.columnHeader3
    });
    this.foldersView.Font = (Font) null;
    this.foldersView.Name = "foldersView";
    this.foldersView.Sorting = SortOrder.Ascending;
    this.foldersView.OwnerDraw = true;
    this.foldersView.RadioGroups = false;
    this.foldersView.SortColumn = 0;
    this.foldersView.SubitemImages = (ImageList) null;
    this.foldersView.UseCompatibleStateImageBehavior = false;
    this.foldersView.View = View.Details;
    this.foldersView.ItemActivate += new EventHandler(this.foldersView_ItemActivate);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.columnHeader3, "columnHeader3");
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.foldersView);
    this.Font = (Font) null;
    this.Name = nameof (MailView);
    this.ResumeLayout(false);
  }
}
