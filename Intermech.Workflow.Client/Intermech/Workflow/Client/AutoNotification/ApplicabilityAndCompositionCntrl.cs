// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.ApplicabilityAndCompositionCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class ApplicabilityAndCompositionCntrl : UserControl
{
  private List<int> _objTypes = new List<int>();
  private List<int> _relTypes = new List<int>();
  private long _ruleID;
  private bool _isChanged;
  private IContainer components;
  private GroupBox gbRelationTypes;
  private ListView lvRelationTypes;
  private Intermech.Bars.ToolBar toolBar1;
  private ButtonItem btnAddRelType;
  private ButtonItem btnDeleteRelType;
  private GroupBox gbObjTypes;
  private ListView lvObjTypes;
  private Intermech.Bars.ToolBar tbUsers;
  private ButtonItem btnAddObjType;
  private ButtonItem btnDeleteObjType;
  private GroupBox gbVersionRule;
  private Button btnVersionRule;
  private TextBox tbVersionRule;
  private ColumnHeader relationTypes;
  private ColumnHeader objectTypes;
  private ButtonItem buttonItem1;
  private ButtonItem buttonItem2;

  public List<int> ObjTypes => this._objTypes;

  public List<int> RelTypes => this._relTypes;

  public long RuleID => this._ruleID;

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

  public ApplicabilityAndCompositionCntrl()
  {
    this.InitializeComponent();
    this.lvObjTypes.SmallImageList = Statics.IconSrv == null ? (this.lvRelationTypes.SmallImageList = (ImageList) null) : (this.lvRelationTypes.SmallImageList = Statics.IconSrv.ImageList);
    this._ruleID = wfConsts.FiltrationBaseVersionsID;
    this.UpdateVersionRuleTextBox();
  }

  private void btnAddObjType_Click(object sender, EventArgs e)
  {
    List<int> fromSelectorForm = ApplicabilityAndCompositionCntrl.GetTypesIDsFromSelectorForm(4);
    if (fromSelectorForm.Count == 0)
      return;
    this._objTypes = this.GetVerifiedObjTypesList(fromSelectorForm);
    this.UpdateObjTypeListViewItems();
    this.IsChanged = true;
  }

  private void btnDeleteObjType_Click(object sender, EventArgs e)
  {
    if (this.lvObjTypes.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvObjTypes.SelectedItems)
      this._objTypes.Remove(Convert.ToInt32(selectedItem.Tag));
    this.UpdateObjTypeListViewItems();
    this.IsChanged = true;
  }

  private void lvObjTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvObjTypes.SelectedItems.Count == 0)
      this.btnDeleteObjType.Enabled = false;
    else
      this.btnDeleteObjType.Enabled = true;
  }

  private void btnDeleteRelation_Click(object sender, EventArgs e)
  {
    if (this.lvRelationTypes.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvRelationTypes.SelectedItems)
      this._relTypes.Remove(Convert.ToInt32(selectedItem.Tag));
    this.UpdateRelationTypeListViewItems();
    this.IsChanged = true;
  }

  private void btnAddRelation_Click(object sender, EventArgs e)
  {
    List<int> fromSelectorForm = ApplicabilityAndCompositionCntrl.GetTypesIDsFromSelectorForm(6);
    if (fromSelectorForm.Count == 0)
      return;
    foreach (int num in fromSelectorForm)
    {
      if (!this._relTypes.Contains(num))
        this._relTypes.Add(num);
    }
    this.UpdateRelationTypeListViewItems();
    this.IsChanged = true;
  }

  private void lvRelationTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvRelationTypes.SelectedItems.Count == 0)
      this.btnDeleteRelType.Enabled = false;
    else
      this.btnDeleteRelType.Enabled = true;
  }

  private void lvObjTypes_Leave(object sender, EventArgs e)
  {
    this.btnDeleteObjType.Enabled = false;
  }

  private void lvRelationTypes_Leave(object sender, EventArgs e)
  {
    this.btnDeleteRelType.Enabled = false;
  }

  private static List<int> GetTypesIDsFromSelectorForm(int requestedType)
  {
    List<int> fromSelectorForm = new List<int>();
    SelectorForm selectorForm = new SelectorForm();
    switch (requestedType)
    {
      case 4:
        selectorForm = new SelectorForm(LocalizationHolder.GetString("ObjectTypes"), 4, true)
        {
          SelectFocusedWhenNothingMultiselected = false,
          ExpandLevelsOnLoad = 0
        };
        break;
      case 6:
        selectorForm = new SelectorForm(LocalizationHolder.GetString("RelationTypes"), 6, true)
        {
          SelectFocusedWhenNothingMultiselected = false,
          ExpandLevelsOnLoad = 1
        };
        break;
    }
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

  private void UpdateObjTypeListViewItems()
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

  private void UpdateRelationTypeListViewItems()
  {
    this.lvRelationTypes.BeginUpdate();
    this.lvRelationTypes.Items.Clear();
    foreach (int relType in this._relTypes)
    {
      if (relType != -1)
      {
        ListViewItem listViewItem = new ListViewItem(MetaDataHelper.GetRelationTypeName(relType));
        listViewItem.Tag = (object) relType;
        if (Statics.IconSrv != null)
        {
          int num = Statics.IconSrv.IndexOf(6, relType);
          listViewItem.ImageIndex = num;
        }
        this.lvRelationTypes.Items.Add(listViewItem);
      }
    }
    this.lvRelationTypes.EndUpdate();
    this.lvRelationTypes.Refresh();
    if (this._relTypes.Count == 0 || this.lvRelationTypes.SelectedItems.Count == 0)
      this.btnDeleteRelType.Enabled = false;
    else
      this.btnDeleteRelType.Enabled = true;
  }

  private void btnVersionRule_Click(object sender, EventArgs e)
  {
    long[] numArray = VersionRulesSelectionForm.Execute(VersionRulesSelectFilter.vrfNone);
    if (numArray == null || numArray.Length == 0)
      return;
    this._ruleID = numArray[0];
    this.UpdateVersionRuleTextBox();
    this.IsChanged = true;
  }

  private void UpdateVersionRuleTextBox()
  {
    if (this._ruleID == 0L)
    {
      this.tbVersionRule.Text = string.Empty;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.tbVersionRule.Text = sessionKeeper.Session.GetObjectInfo(this._ruleID).Caption;
    }
  }

  public void SetData(List<int> objTypes, List<int> relTypes, long ruleID)
  {
    this._objTypes = new List<int>((IEnumerable<int>) objTypes);
    this._relTypes = new List<int>((IEnumerable<int>) relTypes);
    this._ruleID = ruleID;
    this.UpdateControl();
  }

  private void UpdateControl()
  {
    this.UpdateObjTypeListViewItems();
    this.UpdateRelationTypeListViewItems();
    this.UpdateVersionRuleTextBox();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ApplicabilityAndCompositionCntrl));
    this.gbRelationTypes = new GroupBox();
    this.lvRelationTypes = new ListView();
    this.relationTypes = new ColumnHeader();
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.btnAddRelType = new ButtonItem();
    this.btnDeleteRelType = new ButtonItem();
    this.gbObjTypes = new GroupBox();
    this.lvObjTypes = new ListView();
    this.objectTypes = new ColumnHeader();
    this.tbUsers = new Intermech.Bars.ToolBar();
    this.btnAddObjType = new ButtonItem();
    this.btnDeleteObjType = new ButtonItem();
    this.gbVersionRule = new GroupBox();
    this.btnVersionRule = new Button();
    this.tbVersionRule = new TextBox();
    this.buttonItem1 = new ButtonItem();
    this.buttonItem2 = new ButtonItem();
    this.gbRelationTypes.SuspendLayout();
    this.gbObjTypes.SuspendLayout();
    this.gbVersionRule.SuspendLayout();
    this.SuspendLayout();
    this.gbRelationTypes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbRelationTypes.Controls.Add((Control) this.lvRelationTypes);
    this.gbRelationTypes.Controls.Add((Control) this.toolBar1);
    this.gbRelationTypes.Location = new Point(3, 136);
    this.gbRelationTypes.Name = "gbRelationTypes";
    this.gbRelationTypes.Size = new Size(332, 126);
    this.gbRelationTypes.TabIndex = 3;
    this.gbRelationTypes.TabStop = false;
    this.gbRelationTypes.Text = "Типы связей";
    this.lvRelationTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.relationTypes
    });
    this.lvRelationTypes.Dock = DockStyle.Fill;
    this.lvRelationTypes.FullRowSelect = true;
    this.lvRelationTypes.HideSelection = false;
    this.lvRelationTypes.Location = new Point(3, 40);
    this.lvRelationTypes.Name = "lvRelationTypes";
    this.lvRelationTypes.Size = new Size(326, 83);
    this.lvRelationTypes.TabIndex = 4;
    this.lvRelationTypes.UseCompatibleStateImageBehavior = false;
    this.lvRelationTypes.View = View.Details;
    this.lvRelationTypes.SelectedIndexChanged += new EventHandler(this.lvRelationTypes_SelectedIndexChanged);
    this.lvRelationTypes.Leave += new EventHandler(this.lvRelationTypes_Leave);
    this.relationTypes.Text = "Наименование";
    this.relationTypes.Width = 322;
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("789fe93a-3b81-425b-a128-f7c0eb7be0ad");
    this.toolBar1.Hidden = false;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btnAddRelType,
      (ToolbarItemBase) this.btnDeleteRelType
    });
    this.toolBar1.Location = new Point(3, 16 /*0x10*/);
    this.toolBar1.Name = "toolBar1";
    this.toolBar1.Size = new Size(326, 24);
    this.toolBar1.TabIndex = 3;
    this.toolBar1.Text = "toolBar1";
    this.btnAddRelType.BeginGroup = true;
    this.btnAddRelType.CommandName = "btnAddGroup";
    this.btnAddRelType.Image = (Image) componentResourceManager.GetObject("btnAddRelType.Image");
    this.btnAddRelType.ImageIndex = 0;
    this.btnAddRelType.ToolTipText = "Добавить тип связи";
    this.btnAddRelType.Click += new EventHandler(this.btnAddRelation_Click);
    this.btnDeleteRelType.BeginGroup = true;
    this.btnDeleteRelType.CommandName = "btnDeleteRelation";
    this.btnDeleteRelType.Enabled = false;
    this.btnDeleteRelType.Image = (Image) componentResourceManager.GetObject("btnDeleteRelType.Image");
    this.btnDeleteRelType.ToolTipText = "Удалить тип связи";
    this.btnDeleteRelType.Click += new EventHandler(this.btnDeleteRelation_Click);
    this.gbObjTypes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbObjTypes.Controls.Add((Control) this.lvObjTypes);
    this.gbObjTypes.Controls.Add((Control) this.tbUsers);
    this.gbObjTypes.Location = new Point(3, 3);
    this.gbObjTypes.Name = "gbObjTypes";
    this.gbObjTypes.Size = new Size(332, (int) sbyte.MaxValue);
    this.gbObjTypes.TabIndex = 2;
    this.gbObjTypes.TabStop = false;
    this.gbObjTypes.Text = "Типы объектов";
    this.lvObjTypes.Activation = ItemActivation.TwoClick;
    this.lvObjTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.objectTypes
    });
    this.lvObjTypes.Dock = DockStyle.Fill;
    this.lvObjTypes.FullRowSelect = true;
    this.lvObjTypes.HideSelection = false;
    this.lvObjTypes.Location = new Point(3, 40);
    this.lvObjTypes.Name = "lvObjTypes";
    this.lvObjTypes.Size = new Size(326, 84);
    this.lvObjTypes.TabIndex = 3;
    this.lvObjTypes.UseCompatibleStateImageBehavior = false;
    this.lvObjTypes.View = View.Details;
    this.lvObjTypes.SelectedIndexChanged += new EventHandler(this.lvObjTypes_SelectedIndexChanged);
    this.lvObjTypes.Leave += new EventHandler(this.lvObjTypes_Leave);
    this.objectTypes.Text = "Наименование";
    this.objectTypes.Width = 321;
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
    this.tbUsers.Size = new Size(326, 24);
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
    this.gbVersionRule.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbVersionRule.Controls.Add((Control) this.btnVersionRule);
    this.gbVersionRule.Controls.Add((Control) this.tbVersionRule);
    this.gbVersionRule.Location = new Point(3, 266);
    this.gbVersionRule.Name = "gbVersionRule";
    this.gbVersionRule.Size = new Size(329, 58);
    this.gbVersionRule.TabIndex = 4;
    this.gbVersionRule.TabStop = false;
    this.gbVersionRule.Text = "Правило подбора версий";
    this.btnVersionRule.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnVersionRule.Location = new Point(292, 16 /*0x10*/);
    this.btnVersionRule.Name = "btnVersionRule";
    this.btnVersionRule.Size = new Size(31 /*0x1F*/, 23);
    this.btnVersionRule.TabIndex = 1;
    this.btnVersionRule.Text = "...";
    this.btnVersionRule.UseVisualStyleBackColor = true;
    this.btnVersionRule.Click += new EventHandler(this.btnVersionRule_Click);
    this.tbVersionRule.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbVersionRule.Location = new Point(6, 19);
    this.tbVersionRule.Name = "tbVersionRule";
    this.tbVersionRule.ReadOnly = true;
    this.tbVersionRule.Size = new Size(280, 20);
    this.tbVersionRule.TabIndex = 0;
    this.buttonItem1.BeginGroup = true;
    this.buttonItem1.CommandName = "btnDeleteRelation";
    this.buttonItem1.Enabled = false;
    this.buttonItem1.Image = (Image) componentResourceManager.GetObject("buttonItem1.Image");
    this.buttonItem1.ToolTipText = "Удалить тип связи";
    this.buttonItem2.BeginGroup = true;
    this.buttonItem2.CommandName = "btnDeleteRelation";
    this.buttonItem2.Enabled = false;
    this.buttonItem2.Image = (Image) componentResourceManager.GetObject("buttonItem2.Image");
    this.buttonItem2.ToolTipText = "Удалить тип связи";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.Controls.Add((Control) this.gbVersionRule);
    this.Controls.Add((Control) this.gbRelationTypes);
    this.Controls.Add((Control) this.gbObjTypes);
    this.Name = nameof (ApplicabilityAndCompositionCntrl);
    this.Size = new Size(338, 327);
    this.gbRelationTypes.ResumeLayout(false);
    this.gbObjTypes.ResumeLayout(false);
    this.gbVersionRule.ResumeLayout(false);
    this.gbVersionRule.PerformLayout();
    this.ResumeLayout(false);
  }
}
