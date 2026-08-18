// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EntersInTaskConditionEditor
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator.Conditions;
using Intermech.Navigator.SelectionView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class EntersInTaskConditionEditor : ConditionForm
{
  private ConditionWorkflowTemplate value;
  private IContainer components;
  private Button bOK;
  private Button bCancel;
  private TextBoxButton tbbTemplate;
  private Label label1;
  private CheckedListBox clbActions;
  private Label label2;
  private Label label3;
  private CheckBox checkBox1;
  private Button bAdditionalConditions;
  private Panel panel1;
  private ComboBox cbActionType;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem miSelectAll;
  private ToolStripMenuItem miUnselectAll;
  private ToolStripMenuItem miInverseSelection;

  public EntersInTaskConditionEditor() => this.InitializeComponent();

  protected override void OnInitialized()
  {
    this.value = !(this.conditionStructure.Value is ConditionWorkflowTemplate) ? new ConditionWorkflowTemplate(0L, -1, (ConditionStructure[]) null, false) : (ConditionWorkflowTemplate) ((ConditionWorkflowTemplate) this.conditionStructure.Value).Clone();
    this.ReloadData();
    this.RefreshControls();
  }

  private void ReloadData()
  {
    if (this.value.TemplateObjectID != 0L)
      this.tbbTemplate.SetText(this.dataProvider.GetObjectCaption((object) this.value.TemplateObjectID));
    int num1 = 0;
    using (new SessionKeeper())
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(wfConsts.ActivitiesTypeID);
      childrenIdRecursive.RemoveAt(0);
      int num2 = 0;
      for (int index = 0; index < childrenIdRecursive.Count; ++index)
      {
        int num3 = childrenIdRecursive[index];
        IMSObjectType objectType = MetaDataHelper.GetObjectType(num3);
        if (objectType.VersionsMode != ObjectVersionModes.Abstract)
        {
          this.cbActionType.Items.Add((object) new ActivityTypeItem(num3, objectType.ObjectTypeName));
          if (this.value.ActivityTypeID != -1 && this.value.ActivityTypeID == num3)
            num1 = num2;
          ++num2;
        }
      }
      this.cbActionType.SelectedIndex = num1;
    }
    this.RefreshActivities();
    if (this.value.ActivitiesID != null)
    {
      for (int index = 0; index < this.clbActions.Items.Count; ++index)
      {
        if (!this.InActivitiesIDsArrray(this.clbActions.Items[index] as ActivityItem))
          this.clbActions.SetItemChecked(index, false);
      }
    }
    this.checkBox1.Checked = this.value.AllVersions;
  }

  private void RefreshControls()
  {
    this.bOK.Enabled = this.value.TemplateObjectID != 0L && this.value.ActivityTypeID != -1;
  }

  private void RefreshActivities()
  {
    this.clbActions.Items.Clear();
    if (this.value.TemplateObjectID == 0L)
      return;
    List<ActivityItem> items = new List<ActivityItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long id;
      this.GetActivityItemsFromTemplate(sessionKeeper.Session, items, this.value.TemplateObjectID, out id);
      if (this.value.AllVersions)
      {
        foreach (long objectVersion in sessionKeeper.Session.GetObjectVersions(id))
        {
          if (objectVersion != this.value.TemplateObjectID)
            this.GetActivityItemsFromTemplate(sessionKeeper.Session, items, objectVersion, out long _);
        }
      }
      if (items.Count <= 0)
        return;
      foreach (ActivityItem activityItem in items)
        this.clbActions.Items.Add((object) activityItem, this.value.ActivitiesID == null || this.InActivitiesIDsArrray(activityItem));
    }
  }

  private bool InActivitiesIDsArrray(ActivityItem item)
  {
    foreach (long objectId in item.ObjectIDs)
    {
      if (Array.IndexOf<long>(this.value.ActivitiesID, objectId) >= 0)
        return true;
    }
    return false;
  }

  private void GetActivityItemsFromTemplate(
    IUserSession session,
    List<ActivityItem> items,
    long templateID,
    out long id)
  {
    IScheme scheme = session.GetObject(templateID) as IScheme;
    id = scheme.ID;
    foreach (IActivity activity1 in scheme.Activities)
    {
      IActivity activity = activity1;
      if (this.value.ActivityTypeID == -1 || this.value.ActivityTypeID == activity.TypeID)
      {
        ActivityItem activityItem1 = items.Find((Predicate<ActivityItem>) (x => x.TypeID == activity.TypeID && x.Caption.Equals(activity.Caption)));
        if (activityItem1 == null)
        {
          ActivityItem activityItem2 = new ActivityItem(activity.ObjectID, activity.TypeID, activity.Caption);
          items.Add(activityItem2);
        }
        else
          activityItem1.ObjectIDs.Add(activity.ObjectID);
      }
    }
  }

  public override ConditionStructure Result
  {
    get
    {
      List<long> longList = new List<long>();
      if (this.clbActions.CheckedItems.Count != this.clbActions.Items.Count)
      {
        foreach (ActivityItem checkedItem in this.clbActions.CheckedItems)
          longList.AddRange((IEnumerable<long>) checkedItem.ObjectIDs);
      }
      this.value.ActivitiesID = longList.Count > 0 ? longList.ToArray() : (long[]) null;
      this.value.AllVersions = this.checkBox1.Checked;
      this.conditionStructure.Attribute = (object) 0;
      this.conditionStructure.RelationalOperator = RelationalOperators.Equal;
      this.conditionStructure.Value = (object) this.value;
      return this.conditionStructure;
    }
  }

  private bool tbbTemplate_OnOpenDialog(object sender, OnOpenDialogEventArgs e)
  {
    object aObject = (object) 0L;
    if (ValueRelationSelector.SelectObject(ref aObject, 0, (int[]) null, (object) wfConsts.SchemesTypeID, e.Multiselect))
    {
      this.dataProvider.GetObjectCaption(aObject);
      if (!object.Equals((object) this.value.TemplateObjectID, aObject))
      {
        this.value.TemplateObjectID = (long) aObject;
        this.tbbTemplate.SetText(this.dataProvider.GetObjectCaption(aObject));
        this.value.ActivitiesID = (long[]) null;
        this.RefreshActivities();
        this.RefreshControls();
        return true;
      }
    }
    return false;
  }

  private void cbActionType_SelectedIndexChanged(object sender, EventArgs e)
  {
    ActivityTypeItem selectedItem = this.cbActionType.SelectedItem as ActivityTypeItem;
    if (object.Equals((object) this.value.ActivityTypeID, (object) selectedItem.ID))
      return;
    this.value.ActivityTypeID = selectedItem.ID;
    this.value.ActivitiesID = (long[]) null;
    this.RefreshActivities();
    this.RefreshControls();
  }

  private void bAdditionalConditions_Click(object sender, EventArgs e)
  {
    SelectionForm selectionForm = new SelectionForm()
    {
      ParentMode = SelectionFormMode.InnerConditionsForm,
      ObjectTypeForInnerSelection = new int[1]
      {
        this.value.ActivityTypeID
      },
      ObjectAttributesOnlyConditions = true,
      ReadOnly = false
    };
    selectionForm.SelectionLoad(this.selectionID, new List<long>(), this.value.Conditions);
    if (selectionForm.ShowDialog() != DialogResult.OK)
      return;
    this.value.Conditions = selectionForm.Conditions;
  }

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
    this.value.AllVersions = this.checkBox1.Checked;
    this.RefreshActivities();
    this.RefreshControls();
  }

  private void InverseSelection_Click(object sender, EventArgs e)
  {
    for (int index = 0; index < this.clbActions.Items.Count; ++index)
      this.clbActions.SetItemChecked(index, !this.clbActions.GetItemChecked(index));
  }

  private void UnselectAll_Click(object sender, EventArgs e) => this.CheckActionItems(false);

  private void SelectAll_Click(object sender, EventArgs e) => this.CheckActionItems(true);

  private void CheckActionItems(bool check)
  {
    for (int index = 0; index < this.clbActions.Items.Count; ++index)
      this.clbActions.SetItemChecked(index, check);
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
    this.bOK = new Button();
    this.bCancel = new Button();
    this.tbbTemplate = new TextBoxButton();
    this.label1 = new Label();
    this.clbActions = new CheckedListBox();
    this.label2 = new Label();
    this.label3 = new Label();
    this.checkBox1 = new CheckBox();
    this.bAdditionalConditions = new Button();
    this.panel1 = new Panel();
    this.cbActionType = new ComboBox();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.miSelectAll = new ToolStripMenuItem();
    this.miUnselectAll = new ToolStripMenuItem();
    this.miInverseSelection = new ToolStripMenuItem();
    this.panel1.SuspendLayout();
    this.contextMenuStrip1.SuspendLayout();
    this.SuspendLayout();
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(169, 351);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 5;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(296, 351);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 6;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.tbbTemplate.AutoSize = true;
    this.tbbTemplate.Dock = DockStyle.Fill;
    this.tbbTemplate.Location = new Point(0, 0);
    this.tbbTemplate.Margin = new Padding(0);
    this.tbbTemplate.Name = "tbbTemplate";
    this.tbbTemplate.Size = new Size(377, 28);
    this.tbbTemplate.TabIndex = 0;
    this.tbbTemplate.OnOpenDialog += new OnOpenDialogEventHandler(this.tbbTemplate_OnOpenDialog);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(35, 25);
    this.label1.Name = "label1";
    this.label1.Size = new Size(97, 13);
    this.label1.TabIndex = 21;
    this.label1.Text = "Шаблон процесса";
    this.clbActions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.clbActions.CheckOnClick = true;
    this.clbActions.ContextMenuStrip = this.contextMenuStrip1;
    this.clbActions.FormattingEnabled = true;
    this.clbActions.Location = new Point(38, 174);
    this.clbActions.Name = "clbActions";
    this.clbActions.Size = new Size(377, 79);
    this.clbActions.TabIndex = 3;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(35, 98);
    this.label2.Name = "label2";
    this.label2.Size = new Size(76, 13);
    this.label2.TabIndex = 23;
    this.label2.Text = "Тип действий";
    this.label3.AutoSize = true;
    this.label3.Location = new Point(35, 158);
    this.label3.Name = "label3";
    this.label3.Size = new Size(57, 13);
    this.label3.TabIndex = 24;
    this.label3.Text = "Действия";
    this.checkBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.checkBox1.AutoSize = true;
    this.checkBox1.Location = new Point(284, 72);
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.Size = new Size(131, 17);
    this.checkBox1.TabIndex = 1;
    this.checkBox1.Text = "Все версии шаблона";
    this.checkBox1.UseVisualStyleBackColor = true;
    this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
    this.bAdditionalConditions.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bAdditionalConditions.Location = new Point(294, 299);
    this.bAdditionalConditions.Name = "bAdditionalConditions";
    this.bAdditionalConditions.Size = new Size(121, 27);
    this.bAdditionalConditions.TabIndex = 4;
    this.bAdditionalConditions.Text = "Доп. условия";
    this.bAdditionalConditions.UseVisualStyleBackColor = true;
    this.bAdditionalConditions.Click += new EventHandler(this.bAdditionalConditions_Click);
    this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.panel1.Controls.Add((Control) this.tbbTemplate);
    this.panel1.Location = new Point(38, 41);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(377, 28);
    this.panel1.TabIndex = 25;
    this.cbActionType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cbActionType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbActionType.FormattingEnabled = true;
    this.cbActionType.Location = new Point(38, 114);
    this.cbActionType.Name = "cbActionType";
    this.cbActionType.Size = new Size(377, 21);
    this.cbActionType.TabIndex = 26;
    this.cbActionType.SelectedIndexChanged += new EventHandler(this.cbActionType_SelectedIndexChanged);
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.miSelectAll,
      (ToolStripItem) this.miUnselectAll,
      (ToolStripItem) this.miInverseSelection
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size(222, 92);
    this.miSelectAll.Name = "miSelectAll";
    this.miSelectAll.ShortcutKeys = Keys.A | Keys.Control;
    this.miSelectAll.Size = new Size(221, 22);
    this.miSelectAll.Text = "Выделить все";
    this.miSelectAll.Click += new EventHandler(this.SelectAll_Click);
    this.miUnselectAll.Name = "miUnselectAll";
    this.miUnselectAll.Size = new Size(221, 22);
    this.miUnselectAll.Text = "Снять выделение";
    this.miUnselectAll.Click += new EventHandler(this.UnselectAll_Click);
    this.miInverseSelection.Name = "miInverseSelection";
    this.miInverseSelection.Size = new Size(221, 22);
    this.miInverseSelection.Text = "Инвертировать выделение";
    this.miInverseSelection.Click += new EventHandler(this.InverseSelection_Click);
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(429, 390);
    this.Controls.Add((Control) this.cbActionType);
    this.Controls.Add((Control) this.checkBox1);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.bAdditionalConditions);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.clbActions);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(445, 390);
    this.Name = nameof (EntersInTaskConditionEditor);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Условие \"Входит в действия процессов\"";
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.contextMenuStrip1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
