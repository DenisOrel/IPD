// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.ActuationConditionForRelationCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class ActuationConditionForRelationCntrl : UserControl, ICanSaveNotifSettings
{
  private readonly RelationAutoNotificationSettings _notifSettings;
  private List<int> _relTypes;
  private List<int> _objTypes;
  private string _formulaForAttr;
  private bool _useOldAttrValue;
  private long _scriptID;
  private bool _isChanged;
  private IContainer components;
  private Panel panel1;
  private GroupBox gbRelationTypes;
  private ListView lvRelationTypes;
  private ColumnHeader relationTypes;
  private Intermech.Bars.ToolBar toolBar1;
  private ButtonItem btnAddRelType;
  private ButtonItem btnDeleteRelType;
  private GroupBox gbScriptChoosing;
  private Button btnClearScript;
  private Button btnChooseScript;
  private TextBox tbChoosedScript;
  private GroupBox gbFormulaForAttr;
  private RichTextBox rtbFormula;
  private RadioButton rbOldValues;
  private RadioButton rbNewValues;
  private Button btnClearFormula;
  private Button btnAddFormula;
  private Label warningLabel;
  private GroupBox gbObjTypes;
  private ListView lvObjTypes;
  private ColumnHeader objectTypes;
  private Intermech.Bars.ToolBar tbUsers;
  private ButtonItem btnAddObjType;
  private ButtonItem btnDeleteObjType;

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

  public ActuationConditionForRelationCntrl(
    AttributableAutoNotificationSettings notificationSettings)
  {
    this.InitializeComponent();
    this.lvObjTypes.SmallImageList = Statics.IconSrv == null ? (this.lvRelationTypes.SmallImageList = (ImageList) null) : (this.lvRelationTypes.SmallImageList = Statics.IconSrv.ImageList);
    this._notifSettings = notificationSettings as RelationAutoNotificationSettings;
    this.SetDataFromSettings();
    this.UpdateControl();
    this.SetAttrWarningLabel();
    this.IsChanged = false;
  }

  private void SetAttrWarningLabel()
  {
    bool notificationMode;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      notificationMode = sessionKeeper.Session.SendAttrs2DelayedNotificationMode;
    this.warningLabel.Visible = !notificationMode;
    if (this.warningLabel.Visible)
      return;
    this.gbFormulaForAttr.Height -= this.warningLabel.Height;
  }

  private void SetDataFromSettings()
  {
    this._relTypes = new List<int>((IEnumerable<int>) this._notifSettings.FilterTypes);
    this._objTypes = new List<int>((IEnumerable<int>) this._notifSettings.ObjectTypeIds);
    this._formulaForAttr = this._notifSettings.ActuationCondition.FormulaForAttribute.Formula;
    this._useOldAttrValue = this._notifSettings.ActuationCondition.FormulaForAttribute.UseOldAttrValues;
    this._scriptID = this._notifSettings.ActuationCondition.ScriptID;
  }

  public void UpdateControl()
  {
    this.UpdateRelTypesListView();
    this.UpdateObjTypesListView();
    this.UpdateFormulaForAttr();
    this.UpdateScript();
  }

  private List<int> GetRelTypesIDsFromSelectorForm()
  {
    List<int> fromSelectorForm = new List<int>();
    SelectorForm selectorForm = new SelectorForm(LocalizationHolder.GetString("RelationTypes"), 6, true);
    selectorForm.InitSelectionAsType(new ArrayList((ICollection) this._relTypes), (ArrayList) null);
    if (selectorForm.ShowDialog() == DialogResult.Cancel || selectorForm.IDList.Count == 0)
      return fromSelectorForm;
    foreach (object id in selectorForm.IDList)
      fromSelectorForm.Add(Convert.ToInt32(id));
    return fromSelectorForm;
  }

  private void UpdateRelTypesListView()
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

  private List<int> GetTypesIDsFromSelectorForm()
  {
    List<int> fromSelectorForm = new List<int>();
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.GetString("ObjectTypes"), typeof (ObjectTypeFolder), true);
    selectorForm.InitSelectionAsType(new ArrayList((ICollection) this._objTypes), (ArrayList) null);
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
        if (chosenTypeId != objType)
        {
          if (MetaDataHelper.IsObjectTypeChildOf(objType, chosenTypeId))
            list.Remove(objType);
          if (MetaDataHelper.IsObjectTypeChildOf(chosenTypeId, objType))
            list.Remove(chosenTypeId);
        }
      }
    }
    return list;
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

  private void UpdateScript() => this.gbScriptChoosing.Visible = false;

  private void UpdateFormulaForAttr()
  {
    this.rtbFormula.Text = this._formulaForAttr;
    switch (this._notifSettings.NotifEventType)
    {
      case NotificationEventType.AddLink:
        this.rbOldValues.Enabled = false;
        this._useOldAttrValue = false;
        break;
      case NotificationEventType.DeleteLink:
        this.rbNewValues.Enabled = false;
        this._useOldAttrValue = true;
        break;
    }
    if (this._useOldAttrValue)
      this.rbOldValues.Checked = true;
    else
      this.rbNewValues.Checked = true;
  }

  private void ClearFormula()
  {
    this.rtbFormula.Text = string.Empty;
    this._formulaForAttr = string.Empty;
  }

  private List<int> GetRelationTypesCommonAttrIds()
  {
    List<int> typesCommonAttrIds = new List<int>();
    if (this._relTypes != null && this._relTypes.Count > 0)
    {
      List<IMSAttribute4RelationType> resultData = MetaDataHelper.GetAttribute4RelationTypeList(this._relTypes[0]);
      if (resultData != null)
      {
        for (int index = 1; index < this._relTypes.Count; ++index)
        {
          List<IMSAttribute4RelationType> relationTypeList = MetaDataHelper.GetAttribute4RelationTypeList(this._relTypes[index]);
          if (relationTypeList == null)
          {
            resultData.Clear();
            break;
          }
          GenericListHelper.GetDifference<IMSAttribute4RelationType>((IList<IMSAttribute4RelationType>) resultData, (IList<IMSAttribute4RelationType>) relationTypeList, GenericListHelper.SearchMode.smExistInBoth, out resultData);
          if (resultData.Count == 0)
            break;
        }
        if (resultData.Count > 0)
          typesCommonAttrIds = resultData.Select<IMSAttribute4RelationType, int>((Func<IMSAttribute4RelationType, int>) (x => x.AttributeID)).ToList<int>().Distinct<int>().ToList<int>();
      }
    }
    return typesCommonAttrIds;
  }

  private void btnAddRelType_Click(object sender, EventArgs e)
  {
    List<int> fromSelectorForm = this.GetRelTypesIDsFromSelectorForm();
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

  private void btnDeleteRelType_Click(object sender, EventArgs e)
  {
    if (this.lvRelationTypes.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvRelationTypes.SelectedItems)
      this._relTypes.Remove(Convert.ToInt32(selectedItem.Tag));
    this.UpdateRelationTypeListViewItems();
    this.IsChanged = true;
  }

  private void lvRelationTypes_Leave(object sender, EventArgs e)
  {
    this.btnDeleteRelType.Enabled = false;
  }

  private void lvRelationTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvRelationTypes.SelectedItems.Count == 0)
      this.btnDeleteRelType.Enabled = false;
    else
      this.btnDeleteRelType.Enabled = true;
  }

  private void btnAddFormula_Click(object sender, EventArgs e)
  {
    List<int> typesCommonAttrIds = this.GetRelationTypesCommonAttrIds();
    if (typesCommonAttrIds.Count == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.GetString("NoCommonAttrsForRelationsMessage"), LocalizationHolder.GetString("Workflow.Client_89"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      List<Intermech.Expressions.Variable> variables = new List<Intermech.Expressions.Variable>();
      foreach (int attrTypeID in typesCommonAttrIds)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
        Intermech.Expressions.Variable variable = new Intermech.Expressions.Variable(attributeType.Name, Intermech.Navigator.DBObjects.Helper.ConvertType(attributeType.FieldType), attributeType.FieldType);
        variables.Add(variable);
      }
      ExpressionEditor.EditExpression(ref this._formulaForAttr, (ICollection) variables, (CreateVariableEventHandler) null);
      this.rtbFormula.Text = this._formulaForAttr;
      this.IsChanged = true;
    }
  }

  private void btnClearFormula_Click(object sender, EventArgs e)
  {
    this.ClearFormula();
    this.IsChanged = true;
  }

  private void rbNewValues_CheckedChanged(object sender, EventArgs e)
  {
    this._useOldAttrValue = !this.rbNewValues.Checked;
    this.IsChanged = true;
  }

  private void btnClearScript_Click(object sender, EventArgs e)
  {
    this._scriptID = 0L;
    this.tbChoosedScript.Text = string.Empty;
  }

  private void btnChooseScript_Click(object sender, EventArgs e)
  {
  }

  private void btnAddObjType_Click(object sender, EventArgs e)
  {
    List<int> fromSelectorForm = this.GetTypesIDsFromSelectorForm();
    if (fromSelectorForm.Count == 0)
      return;
    this._objTypes = this.GetVerifiedObjTypesList(fromSelectorForm);
    this.UpdateObjTypesListView();
    this.IsChanged = true;
  }

  private void btnDeleteObjType_Click(object sender, EventArgs e)
  {
    if (this.lvObjTypes.FocusedItem == null)
      return;
    foreach (ListViewItem selectedItem in this.lvObjTypes.SelectedItems)
      this._objTypes.Remove(Convert.ToInt32(selectedItem.Tag));
    this.UpdateObjTypesListView();
    this.IsChanged = true;
  }

  private void lvObjTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvObjTypes.FocusedItem == null && this.lvObjTypes.SelectedItems.Count == 0)
      this.btnDeleteObjType.Enabled = false;
    else
      this.btnDeleteObjType.Enabled = true;
  }

  private void lvObjTypes_Leave(object sender, EventArgs e)
  {
    this.btnDeleteObjType.Enabled = false;
  }

  private void lvObjTypes_ItemActivate(object sender, EventArgs e)
  {
    if (this.lvObjTypes.SelectedItems.Count <= 0)
      return;
    this.btnDeleteObjType.Enabled = true;
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
    this._notifSettings.FilterTypes = new List<int>((IEnumerable<int>) this._relTypes);
    this._notifSettings.ActuationCondition.FormulaForAttribute = new FormulaForAttribute(this.rtbFormula.Text, false, this._useOldAttrValue);
    this._notifSettings.ObjectTypeIds = new List<int>((IEnumerable<int>) this._objTypes);
    this._notifSettings.ActuationCondition.ScriptID = this._scriptID;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ActuationConditionForRelationCntrl));
    this.panel1 = new Panel();
    this.gbObjTypes = new GroupBox();
    this.lvObjTypes = new ListView();
    this.objectTypes = new ColumnHeader();
    this.tbUsers = new Intermech.Bars.ToolBar();
    this.btnAddObjType = new ButtonItem();
    this.btnDeleteObjType = new ButtonItem();
    this.gbScriptChoosing = new GroupBox();
    this.btnClearScript = new Button();
    this.btnChooseScript = new Button();
    this.tbChoosedScript = new TextBox();
    this.gbFormulaForAttr = new GroupBox();
    this.warningLabel = new Label();
    this.rtbFormula = new RichTextBox();
    this.rbOldValues = new RadioButton();
    this.rbNewValues = new RadioButton();
    this.btnClearFormula = new Button();
    this.btnAddFormula = new Button();
    this.gbRelationTypes = new GroupBox();
    this.lvRelationTypes = new ListView();
    this.relationTypes = new ColumnHeader();
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.btnAddRelType = new ButtonItem();
    this.btnDeleteRelType = new ButtonItem();
    this.panel1.SuspendLayout();
    this.gbObjTypes.SuspendLayout();
    this.gbScriptChoosing.SuspendLayout();
    this.gbFormulaForAttr.SuspendLayout();
    this.gbRelationTypes.SuspendLayout();
    this.SuspendLayout();
    this.panel1.AutoScroll = true;
    this.panel1.BackColor = SystemColors.Control;
    this.panel1.Controls.Add((Control) this.gbObjTypes);
    this.panel1.Controls.Add((Control) this.gbScriptChoosing);
    this.panel1.Controls.Add((Control) this.gbFormulaForAttr);
    this.panel1.Controls.Add((Control) this.gbRelationTypes);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(0, 0);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(1054, 585);
    this.panel1.TabIndex = 5;
    this.gbObjTypes.Controls.Add((Control) this.lvObjTypes);
    this.gbObjTypes.Controls.Add((Control) this.tbUsers);
    this.gbObjTypes.Location = new Point(6, 179);
    this.gbObjTypes.Name = "gbObjTypes";
    this.gbObjTypes.Size = new Size(444, 168);
    this.gbObjTypes.TabIndex = 9;
    this.gbObjTypes.TabStop = false;
    this.gbObjTypes.Text = "Типы объектов, для которых будет срабатывать уведомление";
    this.lvObjTypes.Activation = ItemActivation.OneClick;
    this.lvObjTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.objectTypes
    });
    this.lvObjTypes.Dock = DockStyle.Fill;
    this.lvObjTypes.FullRowSelect = true;
    this.lvObjTypes.HideSelection = false;
    this.lvObjTypes.Location = new Point(3, 40);
    this.lvObjTypes.Name = "lvObjTypes";
    this.lvObjTypes.Size = new Size(438, 125);
    this.lvObjTypes.TabIndex = 3;
    this.lvObjTypes.UseCompatibleStateImageBehavior = false;
    this.lvObjTypes.View = View.Details;
    this.lvObjTypes.ItemActivate += new EventHandler(this.lvObjTypes_ItemActivate);
    this.lvObjTypes.SelectedIndexChanged += new EventHandler(this.lvObjTypes_SelectedIndexChanged);
    this.lvObjTypes.Leave += new EventHandler(this.lvObjTypes_Leave);
    this.objectTypes.Text = "Наименование";
    this.objectTypes.Width = 437;
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
    this.tbUsers.Size = new Size(438, 24);
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
    this.gbScriptChoosing.Controls.Add((Control) this.btnClearScript);
    this.gbScriptChoosing.Controls.Add((Control) this.btnChooseScript);
    this.gbScriptChoosing.Controls.Add((Control) this.tbChoosedScript);
    this.gbScriptChoosing.Location = new Point(462, 266);
    this.gbScriptChoosing.Name = "gbScriptChoosing";
    this.gbScriptChoosing.Size = new Size(444, 53);
    this.gbScriptChoosing.TabIndex = 8;
    this.gbScriptChoosing.TabStop = false;
    this.gbScriptChoosing.Text = "Скрипт";
    this.btnClearScript.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnClearScript.Image = (Image) componentResourceManager.GetObject("btnClearScript.Image");
    this.btnClearScript.Location = new Point(407, 16 /*0x10*/);
    this.btnClearScript.Name = "btnClearScript";
    this.btnClearScript.Size = new Size(31 /*0x1F*/, 23);
    this.btnClearScript.TabIndex = 2;
    this.btnClearScript.UseVisualStyleBackColor = true;
    this.btnClearScript.Click += new EventHandler(this.btnClearScript_Click);
    this.btnChooseScript.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnChooseScript.Location = new Point(370, 17);
    this.btnChooseScript.Name = "btnChooseScript";
    this.btnChooseScript.Size = new Size(31 /*0x1F*/, 23);
    this.btnChooseScript.TabIndex = 1;
    this.btnChooseScript.Text = "...";
    this.btnChooseScript.UseVisualStyleBackColor = true;
    this.btnChooseScript.Click += new EventHandler(this.btnChooseScript_Click);
    this.tbChoosedScript.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbChoosedScript.Enabled = false;
    this.tbChoosedScript.Location = new Point(6, 19);
    this.tbChoosedScript.Name = "tbChoosedScript";
    this.tbChoosedScript.Size = new Size(358, 20);
    this.tbChoosedScript.TabIndex = 0;
    this.gbFormulaForAttr.AutoSize = true;
    this.gbFormulaForAttr.Controls.Add((Control) this.warningLabel);
    this.gbFormulaForAttr.Controls.Add((Control) this.rtbFormula);
    this.gbFormulaForAttr.Controls.Add((Control) this.rbOldValues);
    this.gbFormulaForAttr.Controls.Add((Control) this.rbNewValues);
    this.gbFormulaForAttr.Controls.Add((Control) this.btnClearFormula);
    this.gbFormulaForAttr.Controls.Add((Control) this.btnAddFormula);
    this.gbFormulaForAttr.Location = new Point(462, 3);
    this.gbFormulaForAttr.Name = "gbFormulaForAttr";
    this.gbFormulaForAttr.Size = new Size(470, 247);
    this.gbFormulaForAttr.TabIndex = 7;
    this.gbFormulaForAttr.TabStop = false;
    this.gbFormulaForAttr.Text = "Формула на атрибуты связи";
    this.warningLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.warningLabel.AutoEllipsis = true;
    this.warningLabel.ForeColor = Color.Red;
    this.warningLabel.Location = new Point(4, 155);
    this.warningLabel.Name = "warningLabel";
    this.warningLabel.Size = new Size(460, 78);
    this.warningLabel.TabIndex = 11;
    this.warningLabel.Text = componentResourceManager.GetString("warningLabel.Text");
    this.rtbFormula.Enabled = false;
    this.rtbFormula.Location = new Point(7, 20);
    this.rtbFormula.Name = "rtbFormula";
    this.rtbFormula.Size = new Size(374, 76);
    this.rtbFormula.TabIndex = 9;
    this.rtbFormula.Text = "";
    this.rbOldValues.AutoSize = true;
    this.rbOldValues.Location = new Point(8, 126);
    this.rbOldValues.Name = "rbOldValues";
    this.rbOldValues.Size = new Size(242, 17);
    this.rbOldValues.TabIndex = 7;
    this.rbOldValues.TabStop = true;
    this.rbOldValues.Text = "Использовать старые значения атрибутов";
    this.rbOldValues.UseVisualStyleBackColor = true;
    this.rbNewValues.AutoSize = true;
    this.rbNewValues.Checked = true;
    this.rbNewValues.Location = new Point(8, 102);
    this.rbNewValues.Name = "rbNewValues";
    this.rbNewValues.Size = new Size(237, 17);
    this.rbNewValues.TabIndex = 6;
    this.rbNewValues.TabStop = true;
    this.rbNewValues.Text = "Использовать новые значения атрибутов";
    this.rbNewValues.UseVisualStyleBackColor = true;
    this.rbNewValues.CheckedChanged += new EventHandler(this.rbNewValues_CheckedChanged);
    this.btnClearFormula.Image = (Image) componentResourceManager.GetObject("btnClearFormula.Image");
    this.btnClearFormula.Location = new Point(424, 20);
    this.btnClearFormula.Name = "btnClearFormula";
    this.btnClearFormula.Size = new Size(31 /*0x1F*/, 23);
    this.btnClearFormula.TabIndex = 4;
    this.btnClearFormula.UseVisualStyleBackColor = true;
    this.btnClearFormula.Click += new EventHandler(this.btnClearFormula_Click);
    this.btnAddFormula.Location = new Point(387, 20);
    this.btnAddFormula.Name = "btnAddFormula";
    this.btnAddFormula.Size = new Size(31 /*0x1F*/, 23);
    this.btnAddFormula.TabIndex = 3;
    this.btnAddFormula.Text = "...";
    this.btnAddFormula.UseVisualStyleBackColor = true;
    this.btnAddFormula.Click += new EventHandler(this.btnAddFormula_Click);
    this.gbRelationTypes.Controls.Add((Control) this.lvRelationTypes);
    this.gbRelationTypes.Controls.Add((Control) this.toolBar1);
    this.gbRelationTypes.Location = new Point(3, 3);
    this.gbRelationTypes.Name = "gbRelationTypes";
    this.gbRelationTypes.Size = new Size(447, 170);
    this.gbRelationTypes.TabIndex = 5;
    this.gbRelationTypes.TabStop = false;
    this.gbRelationTypes.Text = "Типы связей, для которых будет срабатывать уведомление";
    this.lvRelationTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.relationTypes
    });
    this.lvRelationTypes.Dock = DockStyle.Fill;
    this.lvRelationTypes.FullRowSelect = true;
    this.lvRelationTypes.HideSelection = false;
    this.lvRelationTypes.Location = new Point(3, 40);
    this.lvRelationTypes.Name = "lvRelationTypes";
    this.lvRelationTypes.Size = new Size(441, (int) sbyte.MaxValue);
    this.lvRelationTypes.TabIndex = 4;
    this.lvRelationTypes.UseCompatibleStateImageBehavior = false;
    this.lvRelationTypes.View = View.Details;
    this.lvRelationTypes.SelectedIndexChanged += new EventHandler(this.lvRelationTypes_SelectedIndexChanged);
    this.lvRelationTypes.Leave += new EventHandler(this.lvRelationTypes_Leave);
    this.relationTypes.Text = "Наименование";
    this.relationTypes.Width = 449;
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
    this.toolBar1.Size = new Size(441, 24);
    this.toolBar1.TabIndex = 3;
    this.toolBar1.Text = "toolBar1";
    this.btnAddRelType.BeginGroup = true;
    this.btnAddRelType.CommandName = "btnAddGroup";
    this.btnAddRelType.Image = (Image) componentResourceManager.GetObject("btnAddRelType.Image");
    this.btnAddRelType.ImageIndex = 0;
    this.btnAddRelType.ToolTipText = "Добавить тип связи";
    this.btnAddRelType.Click += new EventHandler(this.btnAddRelType_Click);
    this.btnDeleteRelType.BeginGroup = true;
    this.btnDeleteRelType.CommandName = "btnDeleteRelation";
    this.btnDeleteRelType.Enabled = false;
    this.btnDeleteRelType.Image = (Image) componentResourceManager.GetObject("btnDeleteRelType.Image");
    this.btnDeleteRelType.ToolTipText = "Удалить тип связи";
    this.btnDeleteRelType.Click += new EventHandler(this.btnDeleteRelType_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (ActuationConditionForRelationCntrl);
    this.Size = new Size(1054, 585);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.gbObjTypes.ResumeLayout(false);
    this.gbScriptChoosing.ResumeLayout(false);
    this.gbScriptChoosing.PerformLayout();
    this.gbFormulaForAttr.ResumeLayout(false);
    this.gbFormulaForAttr.PerformLayout();
    this.gbRelationTypes.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
