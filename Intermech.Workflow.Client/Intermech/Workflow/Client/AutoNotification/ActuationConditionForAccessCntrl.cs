// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.ActuationConditionForAccessCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class ActuationConditionForAccessCntrl : UserControl, ICanSaveNotifSettings
{
  private readonly AccessDeniedAutoNotificationSettings _notifSettings;
  private List<int> _objTypes;
  private ActionType _accessActionType;
  private bool _isChanged;
  private IContainer components;
  private GroupBox gbObjTypes;
  private ListView lvObjTypes;
  private ColumnHeader objectTypes;
  private Intermech.Bars.ToolBar tbUsers;
  private ButtonItem btnAddObjType;
  private ButtonItem btnDeleteObjType;
  private GroupBox gbActions;
  private CheckedListBox chlbActionTypes;

  public event EventHandler Modified;

  public bool IsChanged
  {
    get => this._isChanged;
    private set
    {
      this._isChanged = value;
      EventHandler modified = this.Modified;
      if (!value || modified == null)
        return;
      modified((object) this, (EventArgs) null);
    }
  }

  public ActuationConditionForAccessCntrl(AccessDeniedAutoNotificationSettings notifSettings)
  {
    this.InitializeComponent();
    this.lvObjTypes.SmallImageList = Statics.IconSrv == null ? (ImageList) null : Statics.IconSrv.ImageList;
    this._notifSettings = notifSettings;
    this.SetDataFromSettings();
    this.UpdateControl();
    this.IsChanged = false;
  }

  private void btnAddObjType_Click(object sender, EventArgs e)
  {
    List<int> fromSelectorForm = ActuationConditionForAccessCntrl.GetTypesIDsFromSelectorForm();
    if (fromSelectorForm.Count == 0)
      return;
    this._objTypes = this.GetVerifiedObjTypesList(fromSelectorForm);
    this.UpdateObjTypesListView();
    this.IsChanged = true;
  }

  private void btnDeleteObjType_Click(object sender, EventArgs e)
  {
    if (this.lvObjTypes.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvObjTypes.SelectedItems)
      this._objTypes.Remove(Convert.ToInt32(selectedItem.Tag));
    this.UpdateObjTypesListView();
    this.IsChanged = true;
  }

  private void lvObjTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvObjTypes.SelectedItems.Count == 0)
      this.btnDeleteObjType.Enabled = false;
    else
      this.btnDeleteObjType.Enabled = true;
  }

  private void lvObjTypes_Leave(object sender, EventArgs e)
  {
    this.btnDeleteObjType.Enabled = false;
  }

  private void chlbActionTypes_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    for (int index = 0; index < this.chlbActionTypes.Items.Count; ++index)
    {
      if (index != e.Index)
        this.chlbActionTypes.SetItemChecked(index, false);
    }
  }

  private void chlbActionTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.chlbActionTypes.CheckedItems.Count > 0 && this.chlbActionTypes.CheckedItems[0] is ListItemClass checkedItem)
      this._accessActionType = (ActionType) checkedItem.Tag;
    if (this.chlbActionTypes.CheckedItems.Count == 0)
      this._accessActionType = ActionType.Any;
    this.IsChanged = true;
  }

  private void UpdateControl()
  {
    this.UpdateActionTypes();
    this.UpdateObjTypesListView();
  }

  private void UpdateActionTypes()
  {
    this.chlbActionTypes.Items.Clear();
    object[] keys = Intermech.Navigator.EventLog.Services.EventTypes.GetKeys();
    for (int index = 0; index < keys.Length; ++index)
    {
      if ((int) keys[index] != 11 && (int) keys[index] != 13 && (int) keys[index] != 12 && (int) keys[index] != 21 && (int) keys[index] != -1)
        this.chlbActionTypes.Items.Add((object) new ListItemClass((string) Intermech.Navigator.EventLog.Services.EventTypes[keys[index]], (object) (int) keys[index]));
    }
    for (int index = 0; index < this.chlbActionTypes.Items.Count; ++index)
    {
      if (this.chlbActionTypes.Items[index] is ListItemClass listItemClass && (ActionType) listItemClass.Tag == this._accessActionType)
        this.chlbActionTypes.SetItemChecked(index, true);
    }
  }

  private void UpdateObjTypesListView()
  {
    this.lvObjTypes.BeginUpdate();
    this.lvObjTypes.Items.Clear();
    foreach (int objType in this._objTypes)
    {
      if (objType != -1)
      {
        ListViewItem listViewItem = new ListViewItem(MetaDataHelper.GetObjectTypeName(objType));
        listViewItem.Tag = (object) objType;
        if (Statics.IconSrv != null)
        {
          int num = Statics.IconSrv.IndexOf(4, objType);
          listViewItem.ImageIndex = num;
        }
        this.lvObjTypes.Items.Add(listViewItem);
      }
    }
    this.lvObjTypes.EndUpdate();
    this.lvObjTypes.Refresh();
    if (this._objTypes.Count == 0 || this.lvObjTypes.SelectedItems.Count == 0)
      this.btnDeleteObjType.Enabled = false;
    else
      this.btnDeleteObjType.Enabled = true;
  }

  private void SetDataFromSettings()
  {
    this._objTypes = new List<int>((IEnumerable<int>) this._notifSettings.FilterTypes);
    this._accessActionType = this._notifSettings.AccessActionType;
  }

  private static List<int> GetTypesIDsFromSelectorForm()
  {
    List<int> fromSelectorForm = new List<int>();
    SelectorForm selectorForm = new SelectorForm(LocalizationHolder.GetString("ObjectTypes"), 4, true)
    {
      SelectFocusedWhenNothingMultiselected = false,
      ExpandLevelsOnLoad = 0
    };
    if (selectorForm.ShowDialog() == DialogResult.Cancel || selectorForm.IDList.Count == 0)
      return fromSelectorForm;
    foreach (object id in selectorForm.IDList)
      fromSelectorForm.Add(Convert.ToInt32(id));
    return fromSelectorForm;
  }

  private List<int> GetVerifiedObjTypesList(List<int> chosenTypeIDs)
  {
    List<int> list = this._objTypes.Union<int>((IEnumerable<int>) chosenTypeIDs).ToList<int>();
    foreach (int objType in this._objTypes)
    {
      foreach (int chosenTypeId in chosenTypeIDs)
      {
        if (MetaDataHelper.IsObjectTypeChildOf(objType, chosenTypeId))
          list.Remove(objType);
        if (MetaDataHelper.IsObjectTypeChildOf(chosenTypeId, objType))
          list.Remove(chosenTypeId);
      }
    }
    return list;
  }

  public override void Refresh()
  {
    base.Refresh();
    this.SetDataFromSettings();
    this.UpdateControl();
    this.IsChanged = false;
  }

  public void SaveSettings()
  {
    this._notifSettings.FilterTypes = new List<int>((IEnumerable<int>) this._objTypes);
    this._notifSettings.AccessActionType = this._accessActionType;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ActuationConditionForAccessCntrl));
    this.gbObjTypes = new GroupBox();
    this.lvObjTypes = new ListView();
    this.objectTypes = new ColumnHeader();
    this.tbUsers = new Intermech.Bars.ToolBar();
    this.btnAddObjType = new ButtonItem();
    this.btnDeleteObjType = new ButtonItem();
    this.gbActions = new GroupBox();
    this.chlbActionTypes = new CheckedListBox();
    this.gbObjTypes.SuspendLayout();
    this.gbActions.SuspendLayout();
    this.SuspendLayout();
    this.gbObjTypes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbObjTypes.Controls.Add((Control) this.lvObjTypes);
    this.gbObjTypes.Controls.Add((Control) this.tbUsers);
    this.gbObjTypes.Location = new Point(19, 170);
    this.gbObjTypes.Name = "gbObjTypes";
    this.gbObjTypes.Size = new Size(614, 172);
    this.gbObjTypes.TabIndex = 5;
    this.gbObjTypes.TabStop = false;
    this.gbObjTypes.Text = "Типы объектов, для которых будет срабатывать уведомление";
    this.lvObjTypes.Activation = ItemActivation.TwoClick;
    this.lvObjTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.objectTypes
    });
    this.lvObjTypes.Dock = DockStyle.Fill;
    this.lvObjTypes.Location = new Point(3, 40);
    this.lvObjTypes.Name = "lvObjTypes";
    this.lvObjTypes.Size = new Size(608, 129);
    this.lvObjTypes.TabIndex = 3;
    this.lvObjTypes.UseCompatibleStateImageBehavior = false;
    this.lvObjTypes.View = View.Details;
    this.lvObjTypes.SelectedIndexChanged += new EventHandler(this.lvObjTypes_SelectedIndexChanged);
    this.lvObjTypes.Leave += new EventHandler(this.lvObjTypes_Leave);
    this.objectTypes.Text = "Наименование";
    this.objectTypes.Width = 306;
    this.tbUsers.FullMenus = true;
    this.tbUsers.Guid = new Guid("789fe93a-3b81-425b-a128-f7c0eb7be0ad");
    this.tbUsers.Hidden = false;
    this.tbUsers.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btnAddObjType,
      (ToolbarItemBase) this.btnDeleteObjType
    });
    this.tbUsers.Location = new Point(3, 16 /*0x10*/);
    this.tbUsers.Name = "tbUsers";
    this.tbUsers.Size = new Size(608, 24);
    this.tbUsers.TabIndex = 2;
    this.tbUsers.Text = "toolBar1";
    this.btnAddObjType.BeginGroup = true;
    this.btnAddObjType.CommandName = "btnAddObjType";
    this.btnAddObjType.Image = (Image) componentResourceManager.GetObject("btnAddObjType.Image");
    this.btnAddObjType.ImageIndex = 0;
    this.btnAddObjType.ToolTipText = "Добавить тип объекта";
    this.btnAddObjType.Click += new EventHandler(this.btnAddObjType_Click);
    this.btnDeleteObjType.BeginGroup = true;
    this.btnDeleteObjType.CommandName = "btnDeleteObjType";
    this.btnDeleteObjType.Enabled = false;
    this.btnDeleteObjType.Image = (Image) componentResourceManager.GetObject("btnDeleteObjType.Image");
    this.btnDeleteObjType.ToolTipText = "Удалить тип объекта";
    this.btnDeleteObjType.Click += new EventHandler(this.btnDeleteObjType_Click);
    this.gbActions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbActions.Controls.Add((Control) this.chlbActionTypes);
    this.gbActions.Location = new Point(19, 4);
    this.gbActions.Name = "gbActions";
    this.gbActions.Size = new Size(614, 160 /*0xA0*/);
    this.gbActions.TabIndex = 18;
    this.gbActions.TabStop = false;
    this.gbActions.Text = "Действие";
    this.chlbActionTypes.CheckOnClick = true;
    this.chlbActionTypes.Dock = DockStyle.Fill;
    this.chlbActionTypes.IntegralHeight = false;
    this.chlbActionTypes.Location = new Point(3, 16 /*0x10*/);
    this.chlbActionTypes.Name = "chlbActionTypes";
    this.chlbActionTypes.Size = new Size(608, 141);
    this.chlbActionTypes.Sorted = true;
    this.chlbActionTypes.TabIndex = 18;
    this.chlbActionTypes.Tag = (object) "3";
    this.chlbActionTypes.ItemCheck += new ItemCheckEventHandler(this.chlbActionTypes_ItemCheck);
    this.chlbActionTypes.SelectedIndexChanged += new EventHandler(this.chlbActionTypes_SelectedIndexChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Control;
    this.Controls.Add((Control) this.gbActions);
    this.Controls.Add((Control) this.gbObjTypes);
    this.Name = nameof (ActuationConditionForAccessCntrl);
    this.Size = new Size(636, 385);
    this.gbObjTypes.ResumeLayout(false);
    this.gbActions.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
