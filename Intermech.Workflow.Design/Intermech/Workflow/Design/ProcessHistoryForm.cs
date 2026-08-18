// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ProcessHistoryForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for ProcessHistoryDlg.</summary>
public class ProcessHistoryForm : FormEx
{
  private EnhListView historyView;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private ColumnHeader partHeader;
  private ColumnHeader columnHeader4;
  private ColumnHeader columnHeader5;
  private ColumnHeader columnHeader6;
  private ColumnHeader columnHeader7;
  private IContainer components;
  private ContextMenuBarItem _contextMenu;
  private StatusStrip statusStrip;
  private Panel bottomPanel;
  private Splitter msgSplitter;
  private Panel browserPanel;
  private ImageList imageList1;
  private Intermech.Bars.ToolBar MsgToolBar;
  private LabelItem labelItem2;
  private ButtonItem CloseButtonItem;
  private Panel SubProcessHintPanel;
  private PictureBox InfoImage;
  private Button LevelUpButton;
  private Label label1;
  private bool inShow;
  private long _parentPID;
  private Intermech.Bars.ToolBar ToolBar;
  private string _initialCaption;
  private long _processID;
  private ColumnDescriptor[] _selectColumns;
  private MenuItemBase _messagesMI;
  private bool _msgViewCreated;
  private MessagesView _msgView;
  private ButtonItem _propsBI;
  private ActivityButtonItem _messageButtonItem;
  private List<ListViewItem> _allItems;
  private ButtonItem _refreshButton;

  public ProcessHistoryForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 829);
    this.historyView.SmallImageList = BaseHolder.IconService.ImageList;
    this.historyView.SubitemImages = BaseHolder.NamedList.ImageList;
    this.historyView.RefineSorting += new EnhListView.SortingRefiner(this.historyView_RefineSorting);
    this.historyView.StateImageList = StateList.PlusMinusImageList;
    this.PopulateToolbar();
  }

  private void historyView_RefineSorting(
    EnhListView.ListViewItemComparer sender,
    ListViewItem li1,
    ListViewItem li2,
    ListViewItem.ListViewSubItem si1,
    ListViewItem.ListViewSubItem si2,
    ref int sortResult)
  {
    if (sortResult != 0 || li1 is NodeListViewItem && li2 is NodeListViewItem && ((NodeListViewItem) li1).Level != ((NodeListViewItem) li2).Level || sender.Column != 3 && sender.Column != 4)
      return;
    int typeId1 = (li1.Tag as ProcessHistoryForm.ActivityInfo).TypeID;
    int typeId2 = (li2.Tag as ProcessHistoryForm.ActivityInfo).TypeID;
    if (typeId1 == wfConsts.StartTypeID && typeId2 != wfConsts.StartTypeID)
      sortResult = -1;
    else if (typeId2 == wfConsts.StartTypeID && typeId1 != wfConsts.StartTypeID)
      sortResult = 1;
    if (sender.Order != SortOrder.Descending)
      return;
    sortResult *= -1;
  }

  public void OnGetSubprocessChildren(NodeListViewItem item)
  {
    ProcessHistoryForm.ActivityInfo tag = item.Tag as ProcessHistoryForm.ActivityInfo;
    long activityId = tag.ActivityID;
    long pid = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(activityId, wfConsts.AttrSubprocessID);
      if (objectAttributeById != null)
        pid = objectAttributeById.AsInteger;
    }
    if (pid > 0L)
      item.Children = this.GetProcessItems(item, pid);
    if (!this._messageButtonItem.Checked)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ConditionStructure[] conds = new ConditionStructure[1]
      {
        new ConditionStructure(wfConsts.AttrActivityID, RelationalOperators.Equal, (object) tag.ActivityID, LogicalOperators.NONE, 0, false)
      };
      object[] columns = new object[6]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION,
        (object) wfConsts.AttrRecipStatusID,
        (object) wfConsts.AttrRecipID,
        (object) wfConsts.AttrStartedID,
        (object) wfConsts.AttrIOUserID
      };
      DataTable dataTable = MiscFunx.SimpleSelect(sessionKeeper.Session, wfConsts.MessageTypeID, columns, conds);
      if (dataTable.Rows.Count <= 0)
        return;
      string text1 = "<?>";
      item.HasChildren = true;
      List<ListViewItem> collection = new List<ListViewItem>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        NodeListViewItem nodeListViewItem1 = new NodeListViewItem(item, row[1].ToString());
        nodeListViewItem1.Tag = (object) new ProcessHistoryForm.ActivityInfo(tag.ProcessID, tag.ActivityID, tag.TypeID, tag.Status);
        nodeListViewItem1.ImageIndex = BaseHolder.IconService.IndexOf(4, wfConsts.MessageTypeID);
        NodeListViewItem nodeListViewItem2 = nodeListViewItem1;
        ImageListViewSubItem imageListViewSubItem = new ImageListViewSubItem("Отправлено исполнителю", Holder.ActivityResultImageIndex[0]);
        nodeListViewItem2.SubItems.Add((ListViewItem.ListViewSubItem) imageListViewSubItem);
        string text2 = MiscFunx.UserRefToString(row[3]);
        if (!DBNull.Value.Equals(row[5]))
        {
          text2 = $"{text2}/{row[5].ToString()}";
          if (this.partHeader.Tag == null)
          {
            this.partHeader.Text = $"{this.partHeader.Text}/{LocalizationHolder.GetString("ActingAs")}";
            this.partHeader.Tag = (object) 1;
          }
        }
        nodeListViewItem2.SubItems.Add(text2);
        DateTime minValue = DateTime.MinValue;
        if (row[4] != DBNull.Value)
        {
          DateTime dateTime = Convert.ToDateTime(row[4]);
          nodeListViewItem2.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) dateTime));
        }
        else
          nodeListViewItem2.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) DateTime.MaxValue, text1));
        nodeListViewItem2.SubItems.Add(string.Empty);
        nodeListViewItem2.SubItems.Add(string.Empty);
        if (row[2] != DBNull.Value)
        {
          string enumDescription = SimpleFuncs.GetEnumDescription((Enum) (RecipStatus) Convert.ToInt32(row[2]));
          nodeListViewItem2.SubItems.Add(enumDescription);
        }
        else
          nodeListViewItem2.SubItems.Add(text1);
        collection.Add((ListViewItem) nodeListViewItem2);
      }
      item.Children.AddRange((IEnumerable<ListViewItem>) collection);
    }
  }

  public void FillProcessInfo(long pid)
  {
    this._parentPID = 0L;
    this._processID = pid;
    if (this._initialCaption == null)
      this._initialCaption = this.Text;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(pid);
      if (!objectInfo.Empty)
      {
        this.Text = $"{this._initialCaption} \"{objectInfo.Caption}\"";
        IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(pid, wfConsts.AttrParentProcessID);
        if (objectAttributeById != null)
          this._parentPID = objectAttributeById.AsInteger;
      }
    }
    this.historyView.Items.Clear();
    this._allItems = (List<ListViewItem>) null;
    this.SubProcessHintPanel.Visible = this._parentPID != 0L;
    this.historyView.Items.AddRange(this.GetProcessItems((NodeListViewItem) null, pid).ToArray());
    this.historyView.SortColumn = 3;
    this.historyView.AllowManualSorting = false;
    this.historyView.RestoreSelectedPos();
  }

  private bool ExpandWorkOffers(
    NodeListViewItem parent,
    List<ListViewItem> list,
    long ProcessID,
    long ActivityID,
    int ActivityTypeID)
  {
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ParticipantList participantList = new ParticipantList("Expanded");
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(ActivityID, wfConsts.AttrParticipantsID);
      if (objectAttributeById != null)
      {
        string addData = ParticipantList.ExtractAddData(objectAttributeById.Value.ToString());
        participantList.AsString = addData;
      }
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(wfConsts.WorkOfferTypeID);
      ConditionStructure[] conditions = new ConditionStructure[2]
      {
        new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.Equal, (object) ProcessID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
        new ConditionStructure(wfConsts.AttrActivityID, RelationalOperators.Equal, (object) ActivityID, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
      };
      object[] row1 = (object[]) null;
      DBRecordSetParams paramSet = new DBRecordSetParams(conditions, this.SelectColumns);
      foreach (DataRow row2 in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      {
        if (row1 == null)
          row1 = row2.ItemArray;
        ListViewItem row3 = this.CreateRow(parent, ProcessID, ActivityID, ActivityStatus.ParticipantWaiting, ActivityTypeID, row2);
        list.Add(row3);
        if (!DBNull.Value.Equals(row2[9]))
        {
          long int64 = Convert.ToInt64(row2[9]);
          Participant participant = participantList.Find(ParticipantKind.User, int64);
          if (participant != null)
            participantList.Remove(participant);
        }
        flag = true;
      }
      if (row1 != null)
      {
        foreach (Participant participant in participantList)
        {
          row1[2] = (object) ActivityResult.Back;
          row1[3] = (object) wfFunx.GetUserName(participant.ID);
          ListViewItem row4 = this.CreateRow(parent, ProcessID, ActivityID, ActivityStatus.ParticipantWaiting, ActivityTypeID, row1);
          list.Add(row4);
        }
      }
    }
    return flag;
  }

  private ListViewItem CreateRow(
    NodeListViewItem parent,
    long ProcessID,
    long ActivityID,
    ActivityStatus status,
    int TypeID,
    DataRow row)
  {
    return this.CreateRow(parent, ProcessID, ActivityID, status, TypeID, row.ItemArray);
  }

  private ListViewItem CreateRow(
    NodeListViewItem parent,
    long ProcessID,
    long ActivityID,
    ActivityStatus status,
    int TypeID,
    object[] row)
  {
    NodeListViewItem row1;
    if (TypeID == wfConsts.SubProcessTypeID)
    {
      row1 = new NodeListViewItem(parent, row[1].ToString());
      row1.OnGetChildren += new OnGetChildrenEventHandler(this.OnGetSubprocessChildren);
      row1.HasChildren = true;
    }
    else
    {
      row1 = new NodeListViewItem(parent, row[1].ToString());
      if (this._messageButtonItem.Checked)
      {
        row1.OnGetChildren += new OnGetChildrenEventHandler(this.OnGetMessagesFromActivity);
        row1.HasChildren = true;
      }
      else
        row1.HasChildren = false;
    }
    row1.Tag = (object) new ProcessHistoryForm.ActivityInfo(ProcessID, ActivityID, TypeID, status);
    row1.ImageIndex = BaseHolder.IconService.IndexOf(4, TypeID);
    if (row[2] != DBNull.Value)
    {
      ActivityResult int32 = (ActivityResult) Convert.ToInt32(row[2]);
      string enumDescription = SimpleFuncs.GetEnumDescription((Enum) int32);
      int rejectWoImageIndex;
      if (status == ActivityStatus.ParticipantWaiting && int32 == ActivityResult.Back)
      {
        rejectWoImageIndex = Holder.RejectWOImageIndex;
        enumDescription = LocalizationHolder.rm.GetString("WORejectResult");
      }
      else
        rejectWoImageIndex = Holder.ActivityResultImageIndex[(int) int32];
      ImageListViewSubItem imageListViewSubItem = new ImageListViewSubItem(enumDescription, rejectWoImageIndex);
      row1.SubItems.Add((ListViewItem.ListViewSubItem) imageListViewSubItem);
    }
    else
      row1.SubItems.Add("<?>");
    string text1 = MiscFunx.UserRefToString(row[3]);
    int num = text1 == LocalizationHolder.rm.GetString("Workflow.Design_73") ? 1 : 0;
    if (!DBNull.Value.Equals(row[11]))
    {
      text1 = $"{text1}/{row[11].ToString()}";
      if (this.partHeader.Tag == null)
      {
        this.partHeader.Text = $"{this.partHeader.Text}/{LocalizationHolder.GetString("ActingAs")}";
        this.partHeader.Tag = (object) 1;
      }
    }
    if (num == 0 && !DBNull.Value.Equals(row[12]))
    {
      long result;
      long.TryParse(row[12].ToString(), out result);
      if (result != 0L)
        text1 += " (Выполнено автоматически системой)";
    }
    row1.SubItems.Add(text1);
    DateTime dateTime1 = DateTime.MinValue;
    DateTime dateTime2 = DateTime.MinValue;
    if (row[4] != DBNull.Value)
    {
      dateTime1 = Convert.ToDateTime(row[4]);
      row1.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) dateTime1));
    }
    else
      row1.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) DateTime.MaxValue, "<?>"));
    if (row[5] != DBNull.Value)
    {
      row1.SubItems.Add(row[5].ToString());
      dateTime2 = Convert.ToDateTime(row[5]);
    }
    else
      row1.SubItems.Add("<?>");
    string text2 = "<?>";
    if (dateTime1 != DateTime.MinValue && dateTime2 != DateTime.MinValue)
    {
      TimeSpan timeSpan = dateTime2.Subtract(dateTime1);
      text2 = $"{timeSpan.Days} {timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
    }
    row1.SubItems.Add(text2);
    Enum @enum = (Enum) status;
    if (!DBNull.Value.Equals(row[8]) && (status == ActivityStatus.Executed || status == ActivityStatus.ParticipantWaiting))
    {
      DeletionStatus int32 = (DeletionStatus) Convert.ToInt32(row[8]);
      if (int32 != DeletionStatus.Normal)
      {
        row1.UseItemStyleForSubItems = true;
        row1.ForeColor = SystemColors.GrayText;
        @enum = (Enum) int32;
      }
    }
    string enumDescription1 = SimpleFuncs.GetEnumDescription(@enum);
    if (TypeID == wfConsts.RemoteSubProcessTypeID && row[10] != DBNull.Value)
    {
      string enumDescription2 = SimpleFuncs.GetEnumDescription((Enum) (RemoteProcessStatus) Convert.ToInt64(row[10]));
      if (enumDescription2 != "")
        enumDescription1 += $" ({enumDescription2})";
    }
    row1.SubItems.Add(enumDescription1);
    return (ListViewItem) row1;
  }

  private void OnGetMessagesFromActivity(NodeListViewItem item)
  {
    if (!this._messageButtonItem.Checked)
      return;
    if (!(item.Tag is ProcessHistoryForm.ActivityInfo tag))
    {
      item.HasChildren = false;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ConditionStructure[] conds = new ConditionStructure[1]
        {
          new ConditionStructure(wfConsts.AttrActivityID, RelationalOperators.Equal, (object) tag.ActivityID, LogicalOperators.NONE, 0, false)
        };
        object[] columns = new object[6]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.CAPTION,
          (object) wfConsts.AttrRecipStatusID,
          (object) wfConsts.AttrRecipID,
          (object) wfConsts.AttrStartedID,
          (object) wfConsts.AttrIOUserID
        };
        DataTable dataTable = MiscFunx.SimpleSelect(sessionKeeper.Session, wfConsts.MessageTypeID, columns, conds);
        if (dataTable.Rows.Count > 0)
        {
          string text1 = "<?>";
          item.HasChildren = true;
          List<ListViewItem> listViewItemList = new List<ListViewItem>();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            NodeListViewItem nodeListViewItem1 = new NodeListViewItem(item, row[1].ToString());
            nodeListViewItem1.Tag = (object) new ProcessHistoryForm.ActivityInfo(tag.ProcessID, tag.ActivityID, tag.TypeID, tag.Status);
            nodeListViewItem1.ImageIndex = BaseHolder.IconService.IndexOf(4, wfConsts.MessageTypeID);
            NodeListViewItem nodeListViewItem2 = nodeListViewItem1;
            ImageListViewSubItem imageListViewSubItem = new ImageListViewSubItem("Отправлено исполнителю", Holder.ActivityResultImageIndex[0]);
            nodeListViewItem2.SubItems.Add((ListViewItem.ListViewSubItem) imageListViewSubItem);
            string text2 = MiscFunx.UserRefToString(row[3]);
            if (!DBNull.Value.Equals(row[5]))
            {
              text2 = $"{text2}/{row[5].ToString()}";
              if (this.partHeader.Tag == null)
              {
                this.partHeader.Text = $"{this.partHeader.Text}/{LocalizationHolder.GetString("ActingAs")}";
                this.partHeader.Tag = (object) 1;
              }
            }
            nodeListViewItem2.SubItems.Add(text2);
            DateTime minValue = DateTime.MinValue;
            if (row[4] != DBNull.Value)
            {
              DateTime dateTime = Convert.ToDateTime(row[4]);
              nodeListViewItem2.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) dateTime));
            }
            else
              nodeListViewItem2.SubItems.Add((ListViewItem.ListViewSubItem) new TypedObjectSubItem((object) DateTime.MaxValue, text1));
            nodeListViewItem2.SubItems.Add(string.Empty);
            nodeListViewItem2.SubItems.Add(string.Empty);
            if (row[2] != DBNull.Value)
            {
              string enumDescription = SimpleFuncs.GetEnumDescription((Enum) (RecipStatus) Convert.ToInt32(row[2]));
              nodeListViewItem2.SubItems.Add(enumDescription);
            }
            else
              nodeListViewItem2.SubItems.Add(text1);
            listViewItemList.Add((ListViewItem) nodeListViewItem2);
          }
          item.Children = listViewItemList;
        }
        else
          item.HasChildren = false;
      }
    }
  }

  protected ColumnDescriptor[] SelectColumns
  {
    get
    {
      if (this._selectColumns == null)
        this._selectColumns = new ColumnDescriptor[13]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION),
          new ColumnDescriptor((object) wfConsts.AttrActivityResultID),
          new ColumnDescriptor((object) wfConsts.AttrRecipID),
          new ColumnDescriptor((object) wfConsts.AttrStartedID, SortOrders.ASC, 0),
          new ColumnDescriptor((object) wfConsts.AttrCompletedID, SortOrders.ASC, 1),
          new ColumnDescriptor((object) wfConsts.AttrActivityStatusID),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
          new ColumnDescriptor((object) wfConsts.AttrRecipDeletionID),
          new ColumnDescriptor((object) wfConsts.AttrRecipID, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
          new ColumnDescriptor((object) wfConsts.AttrRemoteProcessStatusID),
          new ColumnDescriptor((object) wfConsts.AttrIOUserID),
          new ColumnDescriptor((object) wfConsts.AutoExecuteAttributeID)
        };
      return this._selectColumns;
    }
  }

  private List<ListViewItem> GetProcessItems(NodeListViewItem parent, long pid)
  {
    List<ListViewItem> list = new List<ListViewItem>();
    DataTable dataTable = (DataTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(wfConsts.ActivitiesTypeID);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.Equal, (object) pid, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
        new ConditionStructure(wfConsts.AttrActivityStatusID, RelationalOperators.NotEmpty, (object) 0, LogicalOperators.NONE, 0, false)
      }, this.SelectColumns);
      if (paramSet.Tags == null)
        paramSet.Tags = new HybridDictionary();
      paramSet.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesByObjectRefSelector(wfConsts.AttrProcessID, pid);
      dataTable = objectCollection.Select(paramSet);
    }
    foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
    {
      ActivityStatus int32_1 = (ActivityStatus) Convert.ToInt32(row1[6]);
      long int64 = Convert.ToInt64(row1[0]);
      int int32_2 = Convert.ToInt32(row1[7]);
      if (int32_1 != ActivityStatus.ParticipantWaiting || !this.ExpandWorkOffers(parent, list, pid, int64, int32_2))
      {
        ListViewItem row2 = this.CreateRow(parent, pid, int64, int32_1, int32_2, row1);
        list.Add(row2);
      }
    }
    return list;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.MsgToolBar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProcessHistoryForm));
    this.statusStrip = new StatusStrip();
    this.bottomPanel = new Panel();
    this.browserPanel = new Panel();
    this.MsgToolBar = new Intermech.Bars.ToolBar();
    this.labelItem2 = new LabelItem();
    this.CloseButtonItem = new ButtonItem();
    this.msgSplitter = new Splitter();
    this.imageList1 = new ImageList(this.components);
    this.SubProcessHintPanel = new Panel();
    this.LevelUpButton = new Button();
    this.label1 = new Label();
    this.InfoImage = new PictureBox();
    this.ToolBar = new Intermech.Bars.ToolBar();
    this.historyView = new EnhListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.partHeader = new ColumnHeader();
    this.columnHeader4 = new ColumnHeader();
    this.columnHeader5 = new ColumnHeader();
    this.columnHeader6 = new ColumnHeader();
    this.columnHeader7 = new ColumnHeader();
    this.bottomPanel.SuspendLayout();
    this.SubProcessHintPanel.SuspendLayout();
    ((ISupportInitialize) this.InfoImage).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.statusStrip, "statusStrip");
    this.statusStrip.Name = "statusStrip";
    this.bottomPanel.Controls.Add((Control) this.browserPanel);
    this.bottomPanel.Controls.Add((Control) this.MsgToolBar);
    componentResourceManager.ApplyResources((object) this.bottomPanel, "bottomPanel");
    this.bottomPanel.Name = "bottomPanel";
    componentResourceManager.ApplyResources((object) this.browserPanel, "browserPanel");
    this.browserPanel.Name = "browserPanel";
    this.MsgToolBar.FullMenus = true;
    this.MsgToolBar.Guid = new Guid("4c39eaf4-8599-471a-bdce-c15676d41ead");
    this.MsgToolBar.Hidden = false;
    this.MsgToolBar.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.labelItem2,
      (ToolbarItemBase) this.CloseButtonItem
    });
    componentResourceManager.ApplyResources((object) this.MsgToolBar, "MsgToolBar");
    this.MsgToolBar.Movable = false;
    this.MsgToolBar.Name = "MsgToolBar";
    componentResourceManager.ApplyResources((object) this.labelItem2, "labelItem2");
    this.labelItem2.Enabled = false;
    this.labelItem2.Locked = true;
    this.labelItem2.Stretch = true;
    componentResourceManager.ApplyResources((object) this.CloseButtonItem, "CloseButtonItem");
    this.CloseButtonItem.Click += new EventHandler(this.CloseButtonItem_Click);
    componentResourceManager.ApplyResources((object) this.msgSplitter, "msgSplitter");
    this.msgSplitter.Name = "msgSplitter";
    this.msgSplitter.TabStop = false;
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "добавить.png");
    this.imageList1.Images.SetKeyName(1, "убрать.png");
    this.imageList1.Images.SetKeyName(2, "plus.bmp");
    this.imageList1.Images.SetKeyName(3, "minus.bmp");
    this.SubProcessHintPanel.Controls.Add((Control) this.LevelUpButton);
    this.SubProcessHintPanel.Controls.Add((Control) this.label1);
    this.SubProcessHintPanel.Controls.Add((Control) this.InfoImage);
    componentResourceManager.ApplyResources((object) this.SubProcessHintPanel, "SubProcessHintPanel");
    this.SubProcessHintPanel.Name = "SubProcessHintPanel";
    componentResourceManager.ApplyResources((object) this.LevelUpButton, "LevelUpButton");
    this.LevelUpButton.Name = "LevelUpButton";
    this.LevelUpButton.UseVisualStyleBackColor = true;
    this.LevelUpButton.Click += new EventHandler(this.LevelUpButton_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.InfoImage.BackColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.InfoImage, "InfoImage");
    this.InfoImage.Name = "InfoImage";
    this.InfoImage.TabStop = false;
    this.ToolBar.FullMenus = true;
    this.ToolBar.Guid = new Guid("4c39eaf4-8599-471a-bdce-c15676d41ead");
    this.ToolBar.Hidden = false;
    componentResourceManager.ApplyResources((object) this.ToolBar, "ToolBar");
    this.ToolBar.Movable = false;
    this.ToolBar.Name = "ToolBar";
    this.historyView.AllowManualSorting = true;
    this.historyView.Columns.AddRange(new ColumnHeader[7]
    {
      this.columnHeader1,
      this.columnHeader2,
      this.partHeader,
      this.columnHeader4,
      this.columnHeader5,
      this.columnHeader6,
      this.columnHeader7
    });
    componentResourceManager.ApplyResources((object) this.historyView, "historyView");
    this.historyView.FullRowSelect = true;
    this.historyView.HideSelection = false;
    this.historyView.MultiSelect = false;
    this.historyView.Name = "historyView";
    this.historyView.OwnerDraw = true;
    this.historyView.RadioGroups = false;
    this.historyView.SortColumn = 0;
    this.historyView.Sorting = SortOrder.Ascending;
    this.historyView.SubitemImages = (ImageList) null;
    this.historyView.UseCompatibleStateImageBehavior = false;
    this.historyView.View = View.Details;
    this.historyView.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.historyView_ItemSelectionChanged);
    this.historyView.SelectedIndexChanged += new EventHandler(this.historyView_SelectedIndexChanged);
    this.historyView.DoubleClick += new EventHandler(this.historyView_DoubleClick);
    this.historyView.MouseDown += new MouseEventHandler(this.historyView_MouseDown);
    this.historyView.MouseUp += new MouseEventHandler(this.historyView_MouseUp);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.partHeader, "partHeader");
    componentResourceManager.ApplyResources((object) this.columnHeader4, "columnHeader4");
    componentResourceManager.ApplyResources((object) this.columnHeader5, "columnHeader5");
    componentResourceManager.ApplyResources((object) this.columnHeader6, "columnHeader6");
    componentResourceManager.ApplyResources((object) this.columnHeader7, "columnHeader7");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.historyView);
    this.Controls.Add((Control) this.ToolBar);
    this.Controls.Add((Control) this.msgSplitter);
    this.Controls.Add((Control) this.bottomPanel);
    this.Controls.Add((Control) this.SubProcessHintPanel);
    this.Controls.Add((Control) this.statusStrip);
    this.HelpButton = true;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ProcessHistoryForm);
    this.ShowInTaskbar = false;
    this.Closed += new EventHandler(this.ProcessHistoryForm_Closed);
    this.Load += new EventHandler(this.ProcessHistoryForm_Load);
    this.bottomPanel.ResumeLayout(false);
    this.SubProcessHintPanel.ResumeLayout(false);
    this.SubProcessHintPanel.PerformLayout();
    ((ISupportInitialize) this.InfoImage).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void PropertiesMI_Click(object sender, EventArgs e)
  {
    if (this.historyView.SelectedItems.Count <= 0)
      return;
    wfFunx.ShowActivityProperties((this.historyView.SelectedItems[0].Tag as ProcessHistoryForm.ActivityInfo).ActivityID);
  }

  private void MessagesMI_Click(object sender, EventArgs e)
  {
    this.ShowMessages = !(sender as MenuItemBase).Checked;
  }

  private void VariablesMI_Click(object sender, EventArgs e)
  {
    if (this.historyView.SelectedItems.Count <= 0)
      return;
    ProcessHistoryForm.ActivityInfo tag = this.historyView.SelectedItems[0].Tag as ProcessHistoryForm.ActivityInfo;
    wfFunx.ShowVariables(tag.ProcessID, tag.ActivityID, tag.Status == ActivityStatus.Executed);
  }

  private void historyView_DoubleClick(object sender, EventArgs e)
  {
    ListViewItem listViewItem = (ListViewItem) null;
    if (this.historyView.SelectedItems.Count > 0)
      listViewItem = this.historyView.SelectedItems[0];
    if (listViewItem is NodeListViewItem && ((NodeListViewItem) listViewItem).Toggle())
      return;
    this.PropertiesMI_Click((object) null, (EventArgs) null);
  }

  private void historyView_MouseUp(object sender, MouseEventArgs e)
  {
    if (this.inShow || e.Button != MouseButtons.Right)
      return;
    this.inShow = true;
    try
    {
      this.ContextMenu.Show(BaseHolder.PopupHost, (Control) this, new Point(e.X + 1, e.Y + 1));
    }
    finally
    {
      this.inShow = false;
    }
  }

  private IDictionary ColumnsToDictionary(ListView.ColumnHeaderCollection c)
  {
    ListDictionary dictionary = new ListDictionary();
    for (int index = 0; index < c.Count; ++index)
      dictionary.Add((object) ("col" + index.ToString()), (object) c[index].Width);
    return (IDictionary) dictionary;
  }

  private void DictionaryToColumns(IDictionary dict, ListView.ColumnHeaderCollection c)
  {
    for (int index = 0; index < dict.Count && c.Count >= index; ++index)
    {
      object obj = dict[(object) ("col" + index.ToString())];
      if (obj != null)
        c[index].Width = Convert.ToInt32(obj);
    }
  }

  private void ProcessHistoryForm_Load(object sender, EventArgs e)
  {
    this.MsgToolBar.ImageList = BaseHolder.NamedList.ImageList;
    this.CloseButtonItem.ImageIndex = Holder.CloseBtnImageIndex;
    if (ApplicationServices.Container.GetService(typeof (BarManager)) is BarManager service)
      this.MsgToolBar.Renderer = service.Renderer;
    HybridDictionary layoutData = this.historyView.LayoutData;
    FormStorage.LoadLayout((Control) this, (IDictionary) layoutData);
    this.historyView.LayoutData = layoutData;
    object obj1 = layoutData[(object) "ShowMessages"] ?? (object) true;
    object obj2 = layoutData[(object) "BottomHeight"] ?? (object) this.bottomPanel.Height;
    this.ShowMessages = Convert.ToBoolean(obj1);
    this.bottomPanel.Height = Convert.ToInt32(obj2);
    if (this.InfoImage.Image is Bitmap)
      (this.InfoImage.Image as Bitmap).MakeTransparent();
    object obj3 = layoutData[(object) "VisibleTypes"];
    if (obj3 == null)
      return;
    this.VisibleActivityTypes = obj3.ToString();
  }

  private void ProcessHistoryForm_Closed(object sender, EventArgs e)
  {
    HybridDictionary layoutData = this.historyView.LayoutData;
    layoutData[(object) "ShowMessages"] = (object) this.ShowMessages;
    layoutData[(object) sc_21864.ssp_workflow_21865()] = (object) this.bottomPanel.Height;
    layoutData[(object) "VisibleTypes"] = (object) this.VisibleActivityTypes;
    FormStorage.SaveLayout((Control) this, (IDictionary) layoutData);
  }

  private ContextMenuBarItem ContextMenu
  {
    get
    {
      if (this._contextMenu == null)
      {
        this._contextMenu = new ContextMenuBarItem();
        this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("Variables_Cmd"), new EventHandler(this.VariablesMI_Click));
        this._messagesMI = (MenuItemBase) this._contextMenu.Items[this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("ShowMessages"), new EventHandler(this.MessagesMI_Click))];
        this._messagesMI.BeginGroup = true;
        int index = this._contextMenu.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_90"), new EventHandler(this.PropertiesMI_Click));
        this._contextMenu.Items[index].ImageIndex = BaseHolder.NamedList.ImageIndex("imgProp");
        this._contextMenu.Items[index].BeginGroup = true;
        this._contextMenu.Items.Add("Заменить исполнителя...", new EventHandler(this.ReplaceParticipant_Click));
      }
      this._messagesMI.Checked = this.ShowMessages;
      return this._contextMenu;
    }
  }

  private void ReplaceParticipant_Click(object sender, EventArgs e)
  {
    using (ReplaceUserForm replaceUserForm = new ReplaceUserForm())
    {
      if (replaceUserForm.ShowDialog() != DialogResult.OK || replaceUserForm.UserID <= 0L || replaceUserForm.ToUserID <= 0L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this._processID == 0L || !(sessionKeeper.Session.GetObject(this._processID, false) is IProcess process))
          return;
        process.ReplaceParticipant(replaceUserForm.UserID, replaceUserForm.ToUserID);
        this.PerformMailRefresh();
        this.FillProcessInfo(this._processID);
      }
    }
  }

  protected void PerformMailRefresh()
  {
    NotificationEventArgs e = (NotificationEventArgs) new MailRefreshWithoutFormPopupEventArgs("MailRefresh");
    BaseHolder.NotificationService.FireEvent((object) null, e);
  }

  private void CheckMessagesBrowser()
  {
    if (this._msgViewCreated)
      return;
    this._msgView = new MessagesView(true);
    this._msgView.Parent = (Control) this.browserPanel;
    this._msgView.Dock = DockStyle.Fill;
  }

  private void historyView_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.ShowMessages || this._msgView == null || this.historyView.SelectedItems.Count <= 0)
      return;
    ProcessHistoryForm.ActivityInfo tag = this.historyView.SelectedItems[0].Tag as ProcessHistoryForm.ActivityInfo;
    this._msgView.ObjectTypeID = tag.TypeID;
    this._msgView.ObjectID = tag.ActivityID;
  }

  private bool ShowMessages
  {
    get => this.bottomPanel.Visible;
    set
    {
      this.bottomPanel.Visible = value;
      this.msgSplitter.Visible = value;
      if (value)
        this.CheckMessagesBrowser();
      this.historyView_SelectedIndexChanged((object) null, (EventArgs) null);
    }
  }

  private void historyView_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Clicks > 1 || !(this.historyView.GetItemAt(e.X, e.Y) is NodeListViewItem itemAt) || !itemAt.InsideStateImage(e.Location))
      return;
    itemAt.Selected = true;
    itemAt.Focused = true;
    itemAt.Toggle();
  }

  private void CloseButtonItem_Click(object sender, EventArgs e) => this.ShowMessages = false;

  private void LevelUpButton_Click(object sender, EventArgs e)
  {
    if (this._parentPID == 0L)
      return;
    this.FillProcessInfo(this._parentPID);
  }

  public void PopulateToolbar()
  {
    this.ToolBar.ImageList = BaseHolder.NamedList.ImageList;
    this._propsBI = new ButtonItem();
    this._propsBI.Text = LocalizationHolder.rm.GetString("Workflow.Design_90");
    this._propsBI.ImageIndex = BaseHolder.NamedList.ImageIndex("imgProp");
    this._propsBI.Click += new EventHandler(this.PropertiesMI_Click);
    this._propsBI.Enabled = false;
    this.ToolBar.Items.Add((ToolbarItemBase) this._propsBI);
    ButtonItem buttonItem = new ButtonItem();
    buttonItem.BeginGroup = true;
    buttonItem.Text = "Обновить";
    buttonItem.ToolTipText = "Обновить";
    buttonItem.ImageIndex = BaseHolder.NamedList.ImageIndex("imgRefresh");
    this._refreshButton = buttonItem;
    this._refreshButton.Click += new EventHandler(this.refreshButton_Click);
    this.ToolBar.Items.Add((ToolbarItemBase) this._refreshButton);
    for (int index1 = 0; index1 < ActivityInfos.Items.Count; ++index1)
    {
      Intermech.Workflow.ActivityInfo activityInfo = ActivityInfos.Items[index1];
      ActivityButtonItem activityButtonItem = new ActivityButtonItem();
      if (activityInfo.Type == wfConsts.LifeCycleTypeID)
        activityButtonItem.Visible = false;
      int index2 = BaseHolder.IconService.IndexOf(4, activityInfo.Type);
      if (index2 > -1)
        activityButtonItem.Image = BaseHolder.ImageTo16x16(BaseHolder.IconService.ImageList.Images[index2]);
      activityButtonItem.ToolTipText = activityInfo.ObjectName;
      if (index1 == 0)
        activityButtonItem.BeginGroup = true;
      activityButtonItem.AutoToggle = AutoToggleType.Single;
      activityButtonItem.Checked = true;
      activityButtonItem.Type = activityInfo.Type;
      activityButtonItem.Click += new EventHandler(this.ActivitiesTypeFilter_Click);
      activityButtonItem.BeginGroup = index1 == 0;
      this.ToolBar.Items.Add((ToolbarItemBase) activityButtonItem);
      CheckUncheckContextMenu.Attach(this.ToolBar, new EventHandler(this.ActivitiesTypeFilter_Click));
    }
    this._messageButtonItem = new ActivityButtonItem();
    int index = BaseHolder.IconService.IndexOf(4, wfConsts.MessageTypeID);
    if (index > -1)
      this._messageButtonItem.Image = BaseHolder.ImageTo16x16(BaseHolder.IconService.ImageList.Images[index]);
    this._messageButtonItem.ToolTipText = "Почтовое сообщение";
    this._messageButtonItem.BeginGroup = true;
    this._messageButtonItem.AutoToggle = AutoToggleType.Single;
    this._messageButtonItem.Checked = false;
    this._messageButtonItem.Type = wfConsts.MessageTypeID;
    this._messageButtonItem.Click += new EventHandler(this.ActivitiesTypeFilter_Click);
    this.ToolBar.Items.Add((ToolbarItemBase) this._messageButtonItem);
  }

  private void ActivitiesTypeFilter_Click(object sender, EventArgs e)
  {
    if (this._allItems == null)
      this._allItems = this.historyView.Items.Cast<ListViewItem>().ToList<ListViewItem>();
    HashSet<int> intSet = new HashSet<int>();
    intSet.Clear();
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.ToolBar.Items)
    {
      if (toolbarItemBase is ActivityButtonItem activityButtonItem && activityButtonItem.Checked)
        intSet.Add(activityButtonItem.Type);
    }
    this.historyView.BeginUpdate();
    try
    {
      this.historyView.Items.Clear();
      foreach (ListViewItem allItem in this._allItems)
      {
        if (allItem.Tag is ProcessHistoryForm.ActivityInfo tag && intSet.Contains(tag.TypeID))
        {
          if (allItem is NodeListViewItem nodeListViewItem)
          {
            nodeListViewItem.HasChildren = true;
            if (tag.TypeID == wfConsts.SubProcessTypeID)
            {
              nodeListViewItem.OnGetChildren -= new OnGetChildrenEventHandler(this.OnGetSubprocessChildren);
              nodeListViewItem.OnGetChildren += new OnGetChildrenEventHandler(this.OnGetSubprocessChildren);
            }
            else
            {
              nodeListViewItem.OnGetChildren -= new OnGetChildrenEventHandler(this.OnGetMessagesFromActivity);
              nodeListViewItem.OnGetChildren += new OnGetChildrenEventHandler(this.OnGetMessagesFromActivity);
              if (!this._messageButtonItem.Checked)
                nodeListViewItem.HasChildren = false;
            }
          }
          this.historyView.Items.Add(allItem);
        }
      }
      this._propsBI.Enabled = this.historyView.SelectedItems.Count > 0;
    }
    finally
    {
      this.historyView.EndUpdate();
    }
  }

  private void historyView_ItemSelectionChanged(
    object sender,
    ListViewItemSelectionChangedEventArgs e)
  {
    if (this._propsBI == null)
      return;
    this._propsBI.Enabled = e.IsSelected;
  }

  public string VisibleActivityTypes
  {
    get
    {
      bool flag = true;
      List<int> values = new List<int>();
      foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.ToolBar.Items)
      {
        if (toolbarItemBase is ActivityButtonItem activityButtonItem)
        {
          if (activityButtonItem.Checked)
            values.Add(activityButtonItem.Type);
          else
            flag = false;
        }
      }
      return flag ? "" : string.Join<int>(",", (IEnumerable<int>) values);
    }
    set
    {
      List<string> stringList = new List<string>((IEnumerable<string>) value.Split(','));
      foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.ToolBar.Items)
      {
        if (toolbarItemBase is ActivityButtonItem activityButtonItem)
          activityButtonItem.Checked = value == "" || stringList.Contains(Convert.ToString(activityButtonItem.Type));
      }
      this.ActivitiesTypeFilter_Click((object) null, (EventArgs) null);
    }
  }

  private void refreshButton_Click(object sender, EventArgs e)
  {
    this.FillProcessInfo(this._processID);
  }

  private class ActivityInfo
  {
    public readonly long ProcessID;
    public readonly long ActivityID;
    public readonly int TypeID;
    public readonly ActivityStatus Status;

    public ActivityInfo(long processID, long activityID, int typeID, ActivityStatus status)
    {
      this.ProcessID = processID;
      this.ActivityID = activityID;
      this.TypeID = typeID;
      this.Status = status;
    }
  }
}
