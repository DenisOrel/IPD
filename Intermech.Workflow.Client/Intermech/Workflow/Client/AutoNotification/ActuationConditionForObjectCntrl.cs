// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.ActuationConditionForObjectCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Expressions;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class ActuationConditionForObjectCntrl : UserControl, ICanSaveNotifSettings
{
  private readonly AttributableAutoNotificationSettings _notifSettings;
  private List<int> _objTypes;
  private string _formulaForAttr;
  private bool _spreadFormula;
  private bool _useOldAttrValue;
  private long _scriptID;
  private int _lcSchemeID;
  private int _lcStepID = -1;
  private int _lcLevelID;
  private List<int> _attrIDs = new List<int>();
  private bool _isChanged;
  private IContainer components;
  private Panel panel1;
  private GroupBox gbObjTypes;
  private ListView lvObjTypes;
  private ColumnHeader objectTypes;
  private Intermech.Bars.ToolBar tbUsers;
  private ButtonItem btnAddObjType;
  private ButtonItem btnDeleteObjType;
  private GroupBox gbFormulaForAttr;
  private GroupBox gbScriptChoosing;
  private Button btnClearScript;
  private Button btnChooseScript;
  private TextBox tbChoosedScript;
  private GroupBox gbLCLevel;
  private ComboBox cbLCLevel;
  private GroupBox gbAttrTypes;
  private ListView lvAttrTypes;
  private ColumnHeader columnHeader1;
  private Intermech.Bars.ToolBar toolBar1;
  private ButtonItem btnAddAttrType;
  private ButtonItem btnDeleteAttrType;
  private GroupBox gbLCStep;
  private ComboBox cbLCStep;
  private Panel pnlFormula;
  private Label warningLabel;
  private RichTextBox rtbFormula;
  private CheckBox cbSpreadFormula;
  private RadioButton rbOldValues;
  private RadioButton rbNewValues;
  private Button btnClearFormula;
  private Button btnAddFormula;

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

  public ActuationConditionForObjectCntrl(AttributableAutoNotificationSettings notifSettings)
  {
    this.InitializeComponent();
    this.lvObjTypes.SmallImageList = Statics.IconSrv == null ? (this.lvAttrTypes.SmallImageList = (ImageList) null) : (this.lvAttrTypes.SmallImageList = Statics.IconSrv.ImageList);
    this._notifSettings = notifSettings;
    this.SetDataFromSettings();
    this.UpdateControl();
    this.SetAttrWarningLabel();
    this.IsChanged = false;
  }

  private void UpdateControl()
  {
    this.UpdateObjTypesListView();
    this.UpdateFormulaForAttr();
    this.UpdateScript();
    this.UpdateCbLCScheme();
    this.UpdateCbLCLevel();
    this.UpdateAttrListBox();
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

  private void UpdateAttrListBox()
  {
    if (this._notifSettings.NotifEventType != NotificationEventType.Write)
    {
      this.gbAttrTypes.Visible = false;
    }
    else
    {
      this.gbAttrTypes.Visible = true;
      this.lvAttrTypes.BeginUpdate();
      this.lvAttrTypes.Items.Clear();
      foreach (int attrId in this._attrIDs)
      {
        if (attrId != 0)
        {
          ListViewItem listViewItem = new ListViewItem(MetaDataHelper.GetAttributeTypeName(attrId))
          {
            Tag = (object) attrId
          };
          if (Statics.IconSrv != null)
          {
            int num = Statics.IconSrv.IndexOf(3, -1, (object) MetaDataHelper.GetAttributeType(attrId).FieldType);
            listViewItem.ImageIndex = num;
          }
          this.lvAttrTypes.Items.Add(listViewItem);
        }
      }
      this.lvAttrTypes.EndUpdate();
      this.lvAttrTypes.Refresh();
      if (this._attrIDs.Count == 0 || this.lvAttrTypes.SelectedItems.Count == 0)
        this.btnDeleteAttrType.Enabled = false;
      else
        this.btnDeleteAttrType.Enabled = true;
    }
  }

  private void UpdateScript()
  {
    this.gbScriptChoosing.Visible = false;
    if (this._scriptID == 0L)
    {
      this.tbChoosedScript.Text = string.Empty;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.tbChoosedScript.Text = sessionKeeper.Session.GetObjectInfo(this._scriptID).Caption;
    }
  }

  private void UpdateFormulaForAttr()
  {
    this.rtbFormula.Text = this._formulaForAttr;
    switch (this._notifSettings.NotifEventType)
    {
      case NotificationEventType.Create:
      case NotificationEventType.CheckOut:
        this.rbOldValues.Enabled = false;
        this._useOldAttrValue = false;
        break;
      case NotificationEventType.CreateVersion:
      case NotificationEventType.Delete:
      case NotificationEventType.NextLCStep:
      case NotificationEventType.NextLCLevel:
      case NotificationEventType.Cancel:
        this.rbNewValues.Enabled = false;
        this._useOldAttrValue = true;
        break;
    }
    if (this._useOldAttrValue)
      this.rbOldValues.Checked = true;
    else
      this.rbNewValues.Checked = true;
    if (this._spreadFormula)
      this.cbSpreadFormula.Checked = true;
    else
      this.cbSpreadFormula.Checked = false;
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
    this._formulaForAttr = this._notifSettings.ActuationCondition.FormulaForAttribute.Formula;
    this._spreadFormula = this._notifSettings.ActuationCondition.FormulaForAttribute.SpreadFormulaForObject;
    this._useOldAttrValue = this._notifSettings.ActuationCondition.FormulaForAttribute.UseOldAttrValues;
    this._scriptID = this._notifSettings.ActuationCondition.ScriptID;
    switch (this._notifSettings.NotifEventType)
    {
      case NotificationEventType.NextLCStep:
        if (!(this._notifSettings is LCStepAutoNotificationSettings notifSettings1))
          break;
        this._lcSchemeID = notifSettings1.SchemeID;
        this._lcStepID = notifSettings1.LCStepID;
        break;
      case NotificationEventType.NextLCLevel:
        if (!(this._notifSettings is LCLevelAutoNotificationSettings notifSettings2))
          break;
        this._lcLevelID = notifSettings2.LCLevelID;
        break;
      case NotificationEventType.Write:
        if (!(this._notifSettings is AttrChangingAutoNotificationSettings notifSettings3))
          break;
        this._attrIDs = new List<int>((IEnumerable<int>) notifSettings3.AttrIDs);
        break;
    }
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

  private void ClearFormula()
  {
    this.rtbFormula.Text = string.Empty;
    this._formulaForAttr = string.Empty;
  }

  private List<int> GetObjTypesCommonAttrIds()
  {
    List<int> typesCommonAttrIds = new List<int>();
    if (this._objTypes != null && this._objTypes.Count > 0)
    {
      List<IMSAttribute4ObjectType> resultData = MetaDataHelper.GetAttribute4ObjectTypeList(this._objTypes[0]);
      if (resultData != null)
      {
        for (int index = 1; index < this._objTypes.Count; ++index)
        {
          List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(this._objTypes[index]);
          if (attribute4ObjectTypeList == null)
          {
            resultData.Clear();
            break;
          }
          GenericListHelper.GetDifference<IMSAttribute4ObjectType>((IList<IMSAttribute4ObjectType>) resultData, (IList<IMSAttribute4ObjectType>) attribute4ObjectTypeList, GenericListHelper.SearchMode.smExistInBoth, out resultData);
          if (resultData.Count == 0)
            break;
        }
        if (resultData.Count > 0)
          typesCommonAttrIds = resultData.Select<IMSAttribute4ObjectType, int>((System.Func<IMSAttribute4ObjectType, int>) (x => x.AttributeID)).ToList<int>().Distinct<int>().ToList<int>();
      }
    }
    return typesCommonAttrIds;
  }

  private void ClearCbLCStep()
  {
    this.cbLCStep.Items.Clear();
    this.cbLCStep.Text = string.Empty;
  }

  private int GetCommonLCScheme(ref int firstStepID)
  {
    int schemaID = 0;
    int num = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (int objType in this._objTypes)
      {
        int schemaId = sessionKeeper.Session.GetObjectType(objType).SchemaID;
        if (num == 0 && schemaId != 0)
        {
          schemaID = schemaId;
          ++num;
        }
        else if (schemaId != schemaID)
          return 0;
      }
      if (schemaID != 0)
        firstStepID = sessionKeeper.Session.GetLCSchema(schemaID).GetStepsCollection().GetFirstStep();
    }
    return schemaID;
  }

  private void UpdateCbLCLevel()
  {
    if (this._notifSettings.NotifEventType != NotificationEventType.NextLCLevel)
    {
      this.gbLCLevel.Visible = false;
    }
    else
    {
      this.gbLCLevel.Visible = true;
      List<IMSLifeCycleLevel> lcLevelsList = MetaDataHelper.GetLCLevelsList();
      int num = 0;
      List<ListItemClass> listItemClassList = new List<ListItemClass>();
      this.cbLCLevel.BeginUpdate();
      this.cbLCStep.Items.Clear();
      foreach (IMSLifeCycleLevel imsLifeCycleLevel in lcLevelsList)
        listItemClassList.Add(new ListItemClass(imsLifeCycleLevel.Name, (object) imsLifeCycleLevel.LevelID));
      listItemClassList.Sort();
      for (int index = 0; index < listItemClassList.Count; ++index)
      {
        if ((int) listItemClassList[index].Tag == this._lcLevelID)
          num = index;
      }
      this.cbLCLevel.Items.AddRange((object[]) listItemClassList.ToArray());
      this.cbLCLevel.SelectedIndex = num;
      this.cbLCLevel.EndUpdate();
    }
  }

  private void UpdateCbLCScheme()
  {
    if (this._notifSettings.NotifEventType != NotificationEventType.NextLCStep)
    {
      this.gbLCStep.Visible = false;
    }
    else
    {
      this.gbLCStep.Visible = true;
      if (this._lcSchemeID == 0)
      {
        this.ClearCbLCStep();
      }
      else
      {
        this.cbLCStep.BeginUpdate();
        this.cbLCStep.Items.Clear();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DataTable table = sessionKeeper.Session.GetLCSchema(this._lcSchemeID).GetStepsCollection().GetSchema().Tables["IMS_LC_STEPS"];
          int num1 = 0;
          int num2 = 0;
          List<ListItemClass> listItemClassList = new List<ListItemClass>();
          foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
          {
            int int32 = Convert.ToInt32(row["F_LC_STEP"]);
            string aName = Convert.ToString(row["F_LC_NAME"]);
            listItemClassList.Add(new ListItemClass(aName, (object) int32));
            ++num2;
          }
          listItemClassList.Sort();
          for (int index = 0; index < listItemClassList.Count; ++index)
          {
            if ((int) listItemClassList[index].Tag == this._lcStepID)
              num1 = index;
          }
          this.cbLCStep.Items.AddRange((object[]) listItemClassList.ToArray());
          this.cbLCStep.SelectedIndex = num1;
        }
        this.cbLCStep.EndUpdate();
      }
    }
  }

  private void btnAddObjType_Click(object sender, EventArgs e)
  {
    List<int> fromSelectorForm = this.GetTypesIDsFromSelectorForm();
    if (fromSelectorForm.Count == 0)
      return;
    this._objTypes = this.GetVerifiedObjTypesList(fromSelectorForm);
    this.UpdateObjTypesListView();
    this.ClearFormula();
    if (this._notifSettings.NotifEventType == NotificationEventType.NextLCStep)
    {
      int firstStepID = -1;
      int commonLcScheme = this.GetCommonLCScheme(ref firstStepID);
      if (commonLcScheme != this._lcSchemeID)
      {
        if (commonLcScheme == 0)
        {
          this._lcSchemeID = 0;
          this._lcStepID = -1;
          this.ClearCbLCStep();
        }
        else if (commonLcScheme != this._lcSchemeID)
        {
          this._lcSchemeID = commonLcScheme;
          this._lcStepID = firstStepID;
          this.UpdateCbLCScheme();
        }
      }
    }
    this.IsChanged = true;
  }

  private void btnDeleteObjType_Click(object sender, EventArgs e)
  {
    if (this.lvObjTypes.FocusedItem == null)
      return;
    foreach (ListViewItem selectedItem in this.lvObjTypes.SelectedItems)
      this._objTypes.Remove(Convert.ToInt32(selectedItem.Tag));
    this.UpdateObjTypesListView();
    if (this._objTypes.Count > 0)
    {
      int firstStepID = -1;
      int commonLcScheme = this.GetCommonLCScheme(ref firstStepID);
      if (commonLcScheme != this._lcSchemeID)
      {
        this._lcSchemeID = commonLcScheme;
        this._lcStepID = firstStepID;
        this.UpdateCbLCScheme();
      }
    }
    if (this._objTypes.Count == 0)
    {
      this.ClearFormula();
      switch (this._notifSettings.NotifEventType)
      {
        case NotificationEventType.NextLCStep:
          this._lcSchemeID = 0;
          this._lcStepID = -1;
          this.ClearCbLCStep();
          break;
        case NotificationEventType.NextLCLevel:
          this._lcLevelID = 0;
          this.UpdateCbLCLevel();
          break;
        case NotificationEventType.Write:
          this._attrIDs.Clear();
          this.UpdateAttrListBox();
          break;
      }
    }
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

  private void cbSpreadFormula_CheckedChanged(object sender, EventArgs e)
  {
    this._spreadFormula = this.cbSpreadFormula.Checked;
    this.IsChanged = true;
  }

  private void btnAddFormula_Click(object sender, EventArgs e)
  {
    List<int> typesCommonAttrIds = this.GetObjTypesCommonAttrIds();
    if (typesCommonAttrIds.Count == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.GetString("NoCommonAttrsForObjectsMessage"), LocalizationHolder.GetString("Workflow.Client_89"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

  private void cbLCStep_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._lcStepID = Convert.ToInt32((this.cbLCStep.SelectedItem as ListItemClass).Tag);
    this.IsChanged = true;
  }

  private void cbLCLevel_SelectedIndexChanged_1(object sender, EventArgs e)
  {
    this._lcLevelID = Convert.ToInt32((this.cbLCLevel.SelectedItem as ListItemClass).Tag);
    this.IsChanged = true;
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
    this._notifSettings.ActuationCondition.FormulaForAttribute = new FormulaForAttribute(this.rtbFormula.Text, this._spreadFormula, this._useOldAttrValue);
    this._notifSettings.ActuationCondition.ScriptID = this._scriptID;
    switch (this._notifSettings.NotifEventType)
    {
      case NotificationEventType.NextLCStep:
        if (!(this._notifSettings is LCStepAutoNotificationSettings notifSettings1))
          break;
        notifSettings1.LCStepID = this._lcStepID;
        notifSettings1.SchemeID = this._lcSchemeID;
        break;
      case NotificationEventType.NextLCLevel:
        if (!(this._notifSettings is LCLevelAutoNotificationSettings notifSettings2))
          break;
        notifSettings2.LCLevelID = this._lcLevelID;
        break;
      case NotificationEventType.Write:
        if (!(this._notifSettings is AttrChangingAutoNotificationSettings notifSettings3))
          break;
        notifSettings3.AttrIDs = new List<int>((IEnumerable<int>) this._attrIDs);
        break;
    }
  }

  private void btnAddAttrType_Click(object sender, EventArgs e)
  {
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true);
    attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[5]
    {
      FieldTypes.ftBlob,
      FieldTypes.ftMemo,
      FieldTypes.ftFile,
      FieldTypes.ftShortBlob,
      FieldTypes.ftExternalLink
    });
    if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
      return;
    foreach (int num in attributesSelectDlg.SelectedAttributesID)
      this._attrIDs.SafeAdd<int>(num);
    this.UpdateAttrListBox();
    this.IsChanged = true;
  }

  private void btnDeleteAttrType_Click(object sender, EventArgs e)
  {
    if (this.lvAttrTypes.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvAttrTypes.SelectedItems)
      this._attrIDs.Remove(Convert.ToInt32(selectedItem.Tag));
    this.UpdateAttrListBox();
    this.IsChanged = true;
  }

  private void lvAttrTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvAttrTypes.SelectedItems.Count == 0)
      this.btnDeleteAttrType.Enabled = false;
    else
      this.btnDeleteAttrType.Enabled = true;
  }

  private void lvAttrTypes_Leave(object sender, EventArgs e)
  {
    this.btnDeleteAttrType.Enabled = false;
  }

  private void lvObjTypes_ItemSelectionChanged(
    object sender,
    ListViewItemSelectionChangedEventArgs e)
  {
    int count = this.lvObjTypes.SelectedItems.Count;
  }

  private void lvObjTypes_ItemActivate(object sender, EventArgs e)
  {
    if (this.lvObjTypes.SelectedItems.Count <= 0)
      return;
    this.btnDeleteObjType.Enabled = true;
  }

  private void lvAttrTypes_ItemActivate(object sender, EventArgs e)
  {
    if (this.lvAttrTypes.SelectedItems.Count <= 0)
      return;
    this.btnDeleteAttrType.Enabled = true;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ActuationConditionForObjectCntrl));
    this.panel1 = new Panel();
    this.gbLCStep = new GroupBox();
    this.cbLCStep = new ComboBox();
    this.gbLCLevel = new GroupBox();
    this.cbLCLevel = new ComboBox();
    this.gbAttrTypes = new GroupBox();
    this.lvAttrTypes = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.btnAddAttrType = new ButtonItem();
    this.btnDeleteAttrType = new ButtonItem();
    this.gbScriptChoosing = new GroupBox();
    this.btnClearScript = new Button();
    this.btnChooseScript = new Button();
    this.tbChoosedScript = new TextBox();
    this.gbFormulaForAttr = new GroupBox();
    this.pnlFormula = new Panel();
    this.warningLabel = new Label();
    this.rtbFormula = new RichTextBox();
    this.cbSpreadFormula = new CheckBox();
    this.rbOldValues = new RadioButton();
    this.rbNewValues = new RadioButton();
    this.btnClearFormula = new Button();
    this.btnAddFormula = new Button();
    this.gbObjTypes = new GroupBox();
    this.lvObjTypes = new ListView();
    this.objectTypes = new ColumnHeader();
    this.tbUsers = new Intermech.Bars.ToolBar();
    this.btnAddObjType = new ButtonItem();
    this.btnDeleteObjType = new ButtonItem();
    this.panel1.SuspendLayout();
    this.gbLCStep.SuspendLayout();
    this.gbLCLevel.SuspendLayout();
    this.gbAttrTypes.SuspendLayout();
    this.gbScriptChoosing.SuspendLayout();
    this.gbFormulaForAttr.SuspendLayout();
    this.pnlFormula.SuspendLayout();
    this.gbObjTypes.SuspendLayout();
    this.SuspendLayout();
    this.panel1.AutoScroll = true;
    this.panel1.BackColor = SystemColors.Control;
    this.panel1.Controls.Add((Control) this.gbLCStep);
    this.panel1.Controls.Add((Control) this.gbLCLevel);
    this.panel1.Controls.Add((Control) this.gbAttrTypes);
    this.panel1.Controls.Add((Control) this.gbScriptChoosing);
    this.panel1.Controls.Add((Control) this.gbFormulaForAttr);
    this.panel1.Controls.Add((Control) this.gbObjTypes);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(0, 0);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(1018, 583);
    this.panel1.TabIndex = 4;
    this.gbLCStep.Controls.Add((Control) this.cbLCStep);
    this.gbLCStep.Location = new Point(3, 199);
    this.gbLCStep.Name = "gbLCStep";
    this.gbLCStep.Size = new Size(447, 51);
    this.gbLCStep.TabIndex = 11;
    this.gbLCStep.TabStop = false;
    this.gbLCStep.Text = "Шаг жизненного цикла";
    this.gbLCStep.Visible = false;
    this.cbLCStep.FormattingEnabled = true;
    this.cbLCStep.Location = new Point(3, 20);
    this.cbLCStep.Name = "cbLCStep";
    this.cbLCStep.Size = new Size(438, 21);
    this.cbLCStep.TabIndex = 0;
    this.cbLCStep.SelectedIndexChanged += new EventHandler(this.cbLCStep_SelectedIndexChanged);
    this.gbLCLevel.Controls.Add((Control) this.cbLCLevel);
    this.gbLCLevel.Location = new Point(3, 199);
    this.gbLCLevel.Name = "gbLCLevel";
    this.gbLCLevel.Size = new Size(447, 51);
    this.gbLCLevel.TabIndex = 9;
    this.gbLCLevel.TabStop = false;
    this.gbLCLevel.Text = "Уровень продвижения";
    this.gbLCLevel.Visible = false;
    this.cbLCLevel.FormattingEnabled = true;
    this.cbLCLevel.Location = new Point(3, 20);
    this.cbLCLevel.Name = "cbLCLevel";
    this.cbLCLevel.Size = new Size(435, 21);
    this.cbLCLevel.TabIndex = 0;
    this.cbLCLevel.SelectedIndexChanged += new EventHandler(this.cbLCLevel_SelectedIndexChanged_1);
    this.gbAttrTypes.Controls.Add((Control) this.lvAttrTypes);
    this.gbAttrTypes.Controls.Add((Control) this.toolBar1);
    this.gbAttrTypes.Location = new Point(3, 199);
    this.gbAttrTypes.Name = "gbAttrTypes";
    this.gbAttrTypes.Size = new Size(447, 197);
    this.gbAttrTypes.TabIndex = 10;
    this.gbAttrTypes.TabStop = false;
    this.gbAttrTypes.Text = "Выберите атрибуты, об изменении которых необходимо рассылать уведомления";
    this.lvAttrTypes.Activation = ItemActivation.OneClick;
    this.lvAttrTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.lvAttrTypes.Dock = DockStyle.Fill;
    this.lvAttrTypes.FullRowSelect = true;
    this.lvAttrTypes.Location = new Point(3, 40);
    this.lvAttrTypes.Name = "lvAttrTypes";
    this.lvAttrTypes.Size = new Size(441, 154);
    this.lvAttrTypes.TabIndex = 3;
    this.lvAttrTypes.UseCompatibleStateImageBehavior = false;
    this.lvAttrTypes.View = View.Details;
    this.lvAttrTypes.ItemActivate += new EventHandler(this.lvAttrTypes_ItemActivate);
    this.lvAttrTypes.SelectedIndexChanged += new EventHandler(this.lvAttrTypes_SelectedIndexChanged);
    this.lvAttrTypes.Leave += new EventHandler(this.lvAttrTypes_Leave);
    this.columnHeader1.Text = "Наименование";
    this.columnHeader1.Width = 437;
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("789fe93a-3b81-425b-a128-f7c0eb7be0ad");
    this.toolBar1.Hidden = false;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btnAddAttrType,
      (ToolbarItemBase) this.btnDeleteAttrType
    });
    this.toolBar1.Location = new Point(3, 16 /*0x10*/);
    this.toolBar1.Name = "toolBar1";
    this.toolBar1.Size = new Size(441, 24);
    this.toolBar1.TabIndex = 2;
    this.toolBar1.Text = "toolBar1";
    this.btnAddAttrType.BeginGroup = true;
    this.btnAddAttrType.CommandName = "btnAddObjType";
    this.btnAddAttrType.Image = (Image) componentResourceManager.GetObject("btnAddAttrType.Image");
    this.btnAddAttrType.ImageIndex = 0;
    this.btnAddAttrType.ToolTipText = "Добавить тип объекта";
    this.btnAddAttrType.Click += new EventHandler(this.btnAddAttrType_Click);
    this.btnDeleteAttrType.BeginGroup = true;
    this.btnDeleteAttrType.CommandName = "btnDeleteObjType";
    this.btnDeleteAttrType.Enabled = false;
    this.btnDeleteAttrType.Image = (Image) componentResourceManager.GetObject("btnDeleteAttrType.Image");
    this.btnDeleteAttrType.ToolTipText = "Удалить тип объекта";
    this.btnDeleteAttrType.Click += new EventHandler(this.btnDeleteAttrType_Click);
    this.gbScriptChoosing.Controls.Add((Control) this.btnClearScript);
    this.gbScriptChoosing.Controls.Add((Control) this.btnChooseScript);
    this.gbScriptChoosing.Controls.Add((Control) this.tbChoosedScript);
    this.gbScriptChoosing.Location = new Point(6, 268);
    this.gbScriptChoosing.Name = "gbScriptChoosing";
    this.gbScriptChoosing.Size = new Size(428, 53);
    this.gbScriptChoosing.TabIndex = 6;
    this.gbScriptChoosing.TabStop = false;
    this.gbScriptChoosing.Text = "Скрипт";
    this.btnClearScript.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnClearScript.Image = (Image) componentResourceManager.GetObject("btnClearScript.Image");
    this.btnClearScript.Location = new Point(391, 16 /*0x10*/);
    this.btnClearScript.Name = "btnClearScript";
    this.btnClearScript.Size = new Size(31 /*0x1F*/, 23);
    this.btnClearScript.TabIndex = 2;
    this.btnClearScript.UseVisualStyleBackColor = true;
    this.btnClearScript.Click += new EventHandler(this.btnClearScript_Click);
    this.btnChooseScript.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnChooseScript.Location = new Point(354, 17);
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
    this.tbChoosedScript.Size = new Size(342, 20);
    this.tbChoosedScript.TabIndex = 0;
    this.gbFormulaForAttr.AutoSize = true;
    this.gbFormulaForAttr.Controls.Add((Control) this.pnlFormula);
    this.gbFormulaForAttr.Location = new Point(465, 3);
    this.gbFormulaForAttr.Name = "gbFormulaForAttr";
    this.gbFormulaForAttr.Size = new Size(475, 247);
    this.gbFormulaForAttr.TabIndex = 5;
    this.gbFormulaForAttr.TabStop = false;
    this.gbFormulaForAttr.Text = "Формула на атрибуты объекта";
    this.pnlFormula.Controls.Add((Control) this.warningLabel);
    this.pnlFormula.Controls.Add((Control) this.rtbFormula);
    this.pnlFormula.Controls.Add((Control) this.cbSpreadFormula);
    this.pnlFormula.Controls.Add((Control) this.rbOldValues);
    this.pnlFormula.Controls.Add((Control) this.rbNewValues);
    this.pnlFormula.Controls.Add((Control) this.btnClearFormula);
    this.pnlFormula.Controls.Add((Control) this.btnAddFormula);
    this.pnlFormula.Dock = DockStyle.Fill;
    this.pnlFormula.Location = new Point(3, 16 /*0x10*/);
    this.pnlFormula.Name = "pnlFormula";
    this.pnlFormula.Size = new Size(469, 228);
    this.pnlFormula.TabIndex = 0;
    this.warningLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.warningLabel.AutoEllipsis = true;
    this.warningLabel.ForeColor = Color.Red;
    this.warningLabel.Location = new Point(6, 158);
    this.warningLabel.Name = "warningLabel";
    this.warningLabel.Size = new Size(457, 62);
    this.warningLabel.TabIndex = 17;
    this.warningLabel.Text = componentResourceManager.GetString("warningLabel.Text");
    this.rtbFormula.Enabled = false;
    this.rtbFormula.Location = new Point(7, 8);
    this.rtbFormula.Name = "rtbFormula";
    this.rtbFormula.Size = new Size(374, 76);
    this.rtbFormula.TabIndex = 16 /*0x10*/;
    this.rtbFormula.Text = "";
    this.cbSpreadFormula.AutoSize = true;
    this.cbSpreadFormula.Location = new Point(7, 137);
    this.cbSpreadFormula.Name = "cbSpreadFormula";
    this.cbSpreadFormula.Size = new Size(462, 18);
    this.cbSpreadFormula.TabIndex = 15;
    this.cbSpreadFormula.Text = "Распространить формулу на все объекты, собранные для генерации уведомления";
    this.cbSpreadFormula.UseCompatibleTextRendering = true;
    this.cbSpreadFormula.UseVisualStyleBackColor = true;
    this.cbSpreadFormula.CheckedChanged += new EventHandler(this.cbSpreadFormula_CheckedChanged);
    this.rbOldValues.AutoSize = true;
    this.rbOldValues.Location = new Point(8, 114);
    this.rbOldValues.Name = "rbOldValues";
    this.rbOldValues.Size = new Size(242, 17);
    this.rbOldValues.TabIndex = 14;
    this.rbOldValues.TabStop = true;
    this.rbOldValues.Text = "Использовать старые значения атрибутов";
    this.rbOldValues.UseVisualStyleBackColor = true;
    this.rbNewValues.AutoSize = true;
    this.rbNewValues.Checked = true;
    this.rbNewValues.Location = new Point(8, 90);
    this.rbNewValues.Name = "rbNewValues";
    this.rbNewValues.Size = new Size(237, 17);
    this.rbNewValues.TabIndex = 13;
    this.rbNewValues.TabStop = true;
    this.rbNewValues.Text = "Использовать новые значения атрибутов";
    this.rbNewValues.UseVisualStyleBackColor = true;
    this.rbNewValues.CheckedChanged += new EventHandler(this.rbNewValues_CheckedChanged);
    this.btnClearFormula.Image = (Image) componentResourceManager.GetObject("btnClearFormula.Image");
    this.btnClearFormula.Location = new Point(424, 8);
    this.btnClearFormula.Name = "btnClearFormula";
    this.btnClearFormula.Size = new Size(31 /*0x1F*/, 23);
    this.btnClearFormula.TabIndex = 12;
    this.btnClearFormula.UseVisualStyleBackColor = true;
    this.btnClearFormula.Click += new EventHandler(this.btnClearFormula_Click);
    this.btnAddFormula.Location = new Point(387, 8);
    this.btnAddFormula.Name = "btnAddFormula";
    this.btnAddFormula.Size = new Size(31 /*0x1F*/, 23);
    this.btnAddFormula.TabIndex = 11;
    this.btnAddFormula.Text = "...";
    this.btnAddFormula.UseVisualStyleBackColor = true;
    this.btnAddFormula.Click += new EventHandler(this.btnAddFormula_Click);
    this.gbObjTypes.Controls.Add((Control) this.lvObjTypes);
    this.gbObjTypes.Controls.Add((Control) this.tbUsers);
    this.gbObjTypes.Location = new Point(3, 3);
    this.gbObjTypes.Name = "gbObjTypes";
    this.gbObjTypes.Size = new Size(447, 186);
    this.gbObjTypes.TabIndex = 4;
    this.gbObjTypes.TabStop = false;
    this.gbObjTypes.Text = "Типы объектов, для которых будет срабатывать уведомление";
    this.lvObjTypes.Activation = ItemActivation.OneClick;
    this.lvObjTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.objectTypes
    });
    this.lvObjTypes.Dock = DockStyle.Fill;
    this.lvObjTypes.FullRowSelect = true;
    this.lvObjTypes.Location = new Point(3, 40);
    this.lvObjTypes.Name = "lvObjTypes";
    this.lvObjTypes.Size = new Size(441, 143);
    this.lvObjTypes.TabIndex = 3;
    this.lvObjTypes.UseCompatibleStateImageBehavior = false;
    this.lvObjTypes.View = View.Details;
    this.lvObjTypes.ItemActivate += new EventHandler(this.lvObjTypes_ItemActivate);
    this.lvObjTypes.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.lvObjTypes_ItemSelectionChanged);
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
    this.tbUsers.Size = new Size(441, 24);
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
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.Controls.Add((Control) this.panel1);
    this.Name = nameof (ActuationConditionForObjectCntrl);
    this.Size = new Size(1018, 583);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.gbLCStep.ResumeLayout(false);
    this.gbLCLevel.ResumeLayout(false);
    this.gbAttrTypes.ResumeLayout(false);
    this.gbScriptChoosing.ResumeLayout(false);
    this.gbScriptChoosing.PerformLayout();
    this.gbFormulaForAttr.ResumeLayout(false);
    this.pnlFormula.ResumeLayout(false);
    this.pnlFormula.PerformLayout();
    this.gbObjTypes.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
