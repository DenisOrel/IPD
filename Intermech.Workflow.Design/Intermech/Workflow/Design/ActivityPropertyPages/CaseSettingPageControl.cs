// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.CaseSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Workflow;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class CaseSettingPageControl : UserControl
{
  private ButtonEdit _caseEdit;
  private ConditionInfo _selectedExpertCondition;
  private ExpressionInfo _selectedExpressionCondition;
  private ActivitySettings _settings;
  private bool _expressionConditionsModified;
  private WorkflowNode _activityNode;
  public Dictionary<long, LinkKind> CaseLinksWithModifiedLinkType = new Dictionary<long, LinkKind>();
  private bool _readOnly;
  /// <summary>
  /// Дополнительная колонка для списка, будет добавлять по мере надобности и удаляться так же
  /// </summary>
  private ColumnHeader _objectTypeColumn;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListView CondsView;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private Panel panel3;
  private CheckBox CaseFilterCheckBox;
  private Panel SepPanel;
  private Button ValidateCaseButton;
  private Button changeObjectTypeInExpression;
  private ImageList MiscIL;
  private CheckBox useExpertSystemCheckBox;

  public CaseSettingPageControl()
  {
    this.InitializeComponent();
    this._objectTypeColumn = new ColumnHeader()
    {
      Text = "Тип объекта",
      Name = "ObjectTypeColumn",
      Width = 150,
      DisplayIndex = 0
    };
  }

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!value)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, (value ? 1 : 0) != 0, new List<Control>((IEnumerable<Control>) new Control[1]
      {
        (Control) this.ValidateCaseButton
      }));
    }
  }

  public bool LoadCaseSettingPageControl(
    ActivitySettings settings,
    IDBObject activityObject,
    WorkflowNode activityNode)
  {
    bool flag1 = false;
    this._settings = settings;
    this._activityNode = activityNode;
    if (settings.ActivityType == wfConsts.CaseTypeID)
    {
      this.CaseFilterCheckBox.CheckedChanged -= new EventHandler(this.CaseFilterCheckBox_CheckedChanged);
      this.CaseFilterCheckBox.Checked = settings.ActivityFlags.HasFlag((Enum) ActivityFlags.FilterObjects);
      this.CaseFilterCheckBox.CheckedChanged += new EventHandler(this.CaseFilterCheckBox_CheckedChanged);
      bool flag2 = false;
      IDBAttribute byId1 = activityObject.Attributes.FindByID(wfConsts.AttrConditionID);
      if (byId1 != null)
      {
        settings.ExpertConditions = new ConditionList(byId1);
        bool DefaultValue = settings.ExpertConditions != null && !settings.ExpertConditions.IsEmpty;
        this.useExpertSystemCheckBox.CheckedChanged -= new EventHandler(this.useExpertSystemCheckBox_CheckedChanged);
        this.useExpertSystemCheckBox.Checked = settings.ExtProperties.Ini.ReadBoolean("Props", "useExpertSystem", DefaultValue);
        this.useExpertSystemCheckBox.CheckedChanged += new EventHandler(this.useExpertSystemCheckBox_CheckedChanged);
        IDBAttribute byId2 = activityObject.Attributes.FindByID(wfConsts.AttrConditionFormulaID);
        settings.ExpressionConditions = new ObservableCollection<ExpressionInfo>((IEnumerable<ExpressionInfo>) MiscFunx.GetExpressionListFromAttr(byId2));
        settings.ExpressionConditions.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ExpressionConditions_CollectionChanged);
        this.RefreshCaseConditions();
        flag2 = true;
      }
      else
      {
        IDBAttribute byId3 = activityObject.Attributes.FindByID(wfConsts.AttrConditionFormulaID);
        if (byId3 != null)
        {
          settings.ExpressionConditions = new ObservableCollection<ExpressionInfo>((IEnumerable<ExpressionInfo>) MiscFunx.GetExpressionListFromAttr(byId3));
          this.RefreshCaseConditions();
          flag2 = true;
        }
      }
      if (!flag2)
        flag1 = true;
    }
    else
      flag1 = true;
    return flag1;
  }

  private void CaseFilterCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.RefreshCaseConditions();
  }

  private void RefreshCaseConditions()
  {
    bool flag = this.CaseFilterCheckBox.Checked;
    if (!this.useExpertSystemCheckBox.Checked)
    {
      this.changeObjectTypeInExpression.Visible = flag;
      this.CondsView.Items.Clear();
      if (flag)
      {
        if (!this.CondsView.Columns.Contains(this._objectTypeColumn))
          this.CondsView.Columns.Insert(0, this._objectTypeColumn);
      }
      else if (this.CondsView.Columns.Contains(this._objectTypeColumn))
        this.CondsView.Columns.Remove(this._objectTypeColumn);
      for (int index = 0; index < this._settings.ExpressionConditions.Count; ++index)
      {
        ExpressionInfo expressionCondition = this._settings.ExpressionConditions[index];
        ListViewItem listViewItem = flag ? this.CondsView.Items.Add(expressionCondition.ObjectTypeName) : this.CondsView.Items.Add(expressionCondition.ToString());
        listViewItem.Tag = (object) expressionCondition;
        if (this._caseEdit != null && this._caseEdit.Visible && this._selectedExpressionCondition == expressionCondition)
          this._caseEdit.Text = expressionCondition.ToString();
        listViewItem.ImageIndex = MiscFunx.VerifyExpression(expressionCondition.FormulaForLink, this._settings.ActivityAllAttributeValues.ToArray(), flag) is bool ? 1 : 0;
        string text = "?";
        int num = 1;
        if (this._settings.ActivityObjectID < 0L)
          num = -1;
        if (flag)
          listViewItem.SubItems.Add(expressionCondition.ToString());
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute objectAttribute = sessionKeeper.Session.GetObjectAttribute((long) num * expressionCondition.LinkID, (object) wfConsts.AttrToActivityID, false, false);
          if (objectAttribute != null)
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo((long) num * objectAttribute.AsInteger);
            if (!objectInfo.Empty)
              text = objectInfo.Caption;
          }
        }
        if (text == "?")
        {
          this.CondsView.Items.Remove(listViewItem);
          this._settings.ExpressionConditions.RemoveAt(index);
          --index;
        }
        else
          listViewItem.SubItems.Add(text);
      }
    }
    else
    {
      this.changeObjectTypeInExpression.Visible = false;
      this.CondsView.Items.Clear();
      if (this.CondsView.Columns.Contains(this._objectTypeColumn))
        this.CondsView.Columns.Remove(this._objectTypeColumn);
      Guid empty = Guid.Empty;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IExpertServer customService = sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer;
        Guid sessionGuid = sessionKeeper.Session.SessionGUID;
        int num1 = customService.StartTask(sessionGuid);
        try
        {
          for (int index = 0; index < this._settings.ExpertConditions.Count; ++index)
          {
            ListViewItem listViewItem = this.CondsView.Items.Add(this._settings.ExpertConditions[index].ToString());
            listViewItem.Tag = (object) this._settings.ExpertConditions[index];
            if (this._caseEdit != null && this._caseEdit.Visible && this._selectedExpertCondition == this._settings.ExpertConditions[index])
              this._caseEdit.Text = this._settings.ExpertConditions[index].ToString();
            listViewItem.ImageIndex = !MiscFunx.VerifyFormula(customService, num1, this._settings.ObjectIDwithVars, this._settings.ExpertConditions[index].ExpertFormula, flag) ? 0 : 1;
            string text = "?";
            int num2 = 1;
            if (this._settings.ActivityObjectID < 0L)
              num2 = -1;
            IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID((long) num2 * this._settings.ExpertConditions[index].LinkID, wfConsts.AttrToActivityID);
            if (objectAttributeById != null)
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo((long) num2 * objectAttributeById.AsInteger);
              if (!objectInfo.Empty)
                text = objectInfo.Caption;
            }
            if (text == "?")
            {
              this.CondsView.Items.Remove(listViewItem);
              this._settings.ExpertConditions.RemoveAt(index);
              --index;
            }
            else
              listViewItem.SubItems.Add(text);
          }
        }
        finally
        {
          customService.EndTask(num1);
        }
      }
    }
  }

  private void CondsView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
  {
    ListViewItem listViewItem = this.CondsView.Items[e.Item];
    Rectangle rectangle = listViewItem.GetBounds(ItemBoundsPortion.Icon);
    Rectangle bounds = listViewItem.GetBounds(ItemBoundsPortion.Entire);
    rectangle = new Rectangle(rectangle.Right, rectangle.Top, bounds.Width - rectangle.Right, bounds.Height - rectangle.Top);
    if (this._caseEdit == null)
    {
      ButtonEdit buttonEdit = new ButtonEdit();
      buttonEdit.Parent = (Control) this.CondsView;
      this._caseEdit = buttonEdit;
      this._caseEdit.Leave += new EventHandler(this._caseEdit_Leave);
      this._caseEdit.Properties.BorderStyle = BorderStyles.Simple;
      this._caseEdit.Properties.ReadOnly = true;
      this._caseEdit.ButtonClick += new ButtonPressedEventHandler(this._caseEdit_ButtonClick);
    }
    if (this.useExpertSystemCheckBox.Checked)
    {
      this._selectedExpertCondition = listViewItem.Tag as ConditionInfo;
      this._caseEdit.Text = this._selectedExpertCondition == null ? string.Empty : this._selectedExpertCondition.ToString();
    }
    else
    {
      this._selectedExpressionCondition = listViewItem.Tag as ExpressionInfo;
      this._caseEdit.Text = this._selectedExpressionCondition == null ? string.Empty : this._selectedExpressionCondition.ToString();
    }
    this._caseEdit.Bounds = rectangle;
    this._caseEdit.Visible = true;
    this._caseEdit.Focus();
    this._caseEdit.DeselectAll();
  }

  private void _caseEdit_Leave(object sender, EventArgs e) => this._caseEdit.Visible = false;

  private void _caseEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (!this.useExpertSystemCheckBox.Checked)
    {
      if (this._selectedExpressionCondition == null)
        this._selectedExpressionCondition = new ExpressionInfo(-1, Guid.Empty, -1L, string.Empty);
      LinkKind linkKind1 = this._selectedExpressionCondition.ElseLink ? LinkKind.False : LinkKind.True;
      List<Intermech.Expressions.Variable> variables = new List<Intermech.Expressions.Variable>(0);
      if (this.CaseFilterCheckBox.Checked)
      {
        if (this._selectedExpressionCondition.ObjectTypeForLink != -1)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            BasicAttributeProperties[] enabledAttributes = sessionKeeper.Session.GetObjectType(this._selectedExpressionCondition.ObjectTypeForLink).Attributes.GetEnabledAttributes(true);
            variables.AddRange((IEnumerable<Intermech.Expressions.Variable>) MiscFunx.ConvertBasicAttributePropertiesToVariable(enabledAttributes));
          }
        }
        else if (this._activityNode?.View != null && this._activityNode.View.AllObjectsAttributes.Count > 0)
          variables.AddRange((IEnumerable<Intermech.Expressions.Variable>) this._activityNode.View.AllObjectsAttributes);
        else
          variables.AddRange((IEnumerable<Intermech.Expressions.Variable>) MiscFunx.GetAllAttributesVariables());
      }
      LinkKind linkKind2 = CaseLinkForm.QueryLinkKind(ref this._selectedExpressionCondition, variables, this._settings.ActivityExpressionAttributes, this._settings.ActivityAllAttributeValues.ToArray());
      if (linkKind2 == LinkKind.Backward)
        return;
      if (linkKind2 != linkKind1)
      {
        if (this.CaseLinksWithModifiedLinkType.ContainsKey(this._selectedExpressionCondition.LinkID))
          this.CaseLinksWithModifiedLinkType[this._selectedExpressionCondition.LinkID] = linkKind2;
        else
          this.CaseLinksWithModifiedLinkType.Add(this._selectedExpressionCondition.LinkID, linkKind2);
      }
      this._expressionConditionsModified = true;
      this.RefreshCaseConditions();
    }
    else
    {
      if (this._selectedExpertCondition == null)
        this._selectedExpertCondition = new ConditionInfo();
      LinkKind linkKind3 = this._selectedExpertCondition.ExpertFormula != null ? LinkKind.True : LinkKind.False;
      LinkKind linkKind4 = CaseLinkForm.QueryLinkKind(ref this._selectedExpertCondition.ExpertFormula, this._settings.ProcessID, this.CaseFilterCheckBox.Checked);
      if (linkKind4 == LinkKind.Backward)
        return;
      if (linkKind4 != linkKind3)
      {
        if (this.CaseLinksWithModifiedLinkType.ContainsKey(this._selectedExpertCondition.LinkID))
          this.CaseLinksWithModifiedLinkType[this._selectedExpertCondition.LinkID] = linkKind4;
        else
          this.CaseLinksWithModifiedLinkType.Add(this._selectedExpertCondition.LinkID, linkKind4);
      }
      this._settings.ExpertConditions.Modified = true;
      this.RefreshCaseConditions();
    }
  }

  private void CondsView_DoubleClick(object sender, EventArgs e)
  {
    if (this.CondsView.SelectedItems.Count <= 0 || !this.CondsView.LabelEdit)
      return;
    this.CondsView_BeforeLabelEdit((object) null, new LabelEditEventArgs(this.CondsView.SelectedIndices[0], ""));
  }

  private void ValidateCaseButton_Click(object sender, EventArgs e)
  {
    if (this.CondsView.SelectedItems.Count <= 0)
      return;
    if (this.CondsView.SelectedItems[0].Tag is ConditionInfo tag1)
    {
      wfFunx.ValidateFormulaDialog(this._settings.ObjectIDwithVars, tag1.ExpertFormula, this.CaseFilterCheckBox.Checked);
    }
    else
    {
      if (!(this.CondsView.SelectedItems[0].Tag is ExpressionInfo tag))
        return;
      int num = (int) MessageBox.Show(MiscFunx.VerifyExpressionFormula(tag.ToString(), this._settings.ActivityAllAttributeValues.ToArray()), LocalizationHolder.rm.GetString("Workflow.Design_119"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  private void ChangeExpressionType_Click(object sender, EventArgs e)
  {
    if (this.CondsView.SelectedItems.Count <= 0 || !(this.CondsView.SelectedItems[0].Tag is ExpressionInfo tag))
      return;
    int num = -1;
    List<int> applicableAttachmentTypes = wfFunx.GetApplicableAttachmentTypes(wfConsts.ActivitiesTypeID, wfConsts.AttachmentRelationTypeID);
    new AllowedTypes(this._settings.ProcessID).Filter(applicableAttachmentTypes);
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Любой тип объекта", typeof (ObjectTypeFolder), false)
    {
      SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(applicableAttachmentTypes.ToArray(), true, true)
    };
    if (selectorForm.ShowDialog() != DialogResult.OK)
      return;
    if (selectorForm.IDList.Count > 0)
      num = Convert.ToInt32(selectorForm.IDList[0]);
    int index = this._settings.ExpressionConditions.IndexOf(tag);
    if (index != -1)
    {
      this._settings.ExpressionConditions[index].ObjectTypeForLink = num;
      this._expressionConditionsModified = true;
    }
    this.RefreshCaseConditions();
  }

  private void ExpressionConditions_CollectionChanged(
    object sender,
    NotifyCollectionChangedEventArgs e)
  {
    this._expressionConditionsModified = true;
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    IDBAttribute attr = activityToSave.Attributes.AddAttribute(wfConsts.AttrConditionFormulaID, false);
    IDBAttribute byId = activityToSave.Attributes.FindByID(wfConsts.AttrConditionID);
    if (this.useExpertSystemCheckBox.Checked)
    {
      if (this._settings.ExpertConditions.Modified && byId != null)
      {
        modified = true;
        this._settings.ExpertConditions.Save(byId);
      }
      attr.Clear();
    }
    else
    {
      if (this._expressionConditionsModified)
      {
        modified = true;
        MiscFunx.ExpressionsToAttribute(new List<ExpressionInfo>((IEnumerable<ExpressionInfo>) this._settings.ExpressionConditions), attr);
      }
      byId.Clear();
    }
    if (this.CaseFilterCheckBox.Checked)
      this._settings.ActivityFlags |= ActivityFlags.FilterObjects;
    this._settings.ExtProperties.Ini.WriteBoolean("Props", "useExpertSystem", this.useExpertSystemCheckBox.Checked);
    return modified;
  }

  private void useExpertSystemCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    if (MessageBox.Show($"Внимание! {(this.useExpertSystemCheckBox.Checked ? "Включение" : "Отключение")} опции приведёт к удалению уже созданных формул. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
      if (this.useExpertSystemCheckBox.Checked)
      {
        if (this._settings.ExpressionConditions != null)
        {
          this._settings.ExpertConditions = new ConditionList();
          foreach (ExpressionInfo expressionCondition in (Collection<ExpressionInfo>) this._settings.ExpressionConditions)
          {
            TempFormula tf = new TempFormula();
            tf.Init();
            if (expressionCondition.ElseLink)
              tf = (TempFormula) null;
            this._settings.ExpertConditions.Add(expressionCondition.LinkID, tf);
          }
          this._settings.ExpressionConditions.Clear();
        }
      }
      else if (this._settings.ExpertConditions != null)
      {
        if (this._settings.ExpressionConditions == null)
        {
          this._settings.ExpressionConditions = new ObservableCollection<ExpressionInfo>();
          this._settings.ExpressionConditions.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ExpressionConditions_CollectionChanged);
        }
        foreach (ConditionInfo expertCondition in this._settings.ExpertConditions)
        {
          ExpressionInfo expressionInfo = new ExpressionInfo(-1, Guid.Empty, expertCondition.LinkID, string.Empty);
          if (expertCondition.ExpertFormula == null)
            expressionInfo.ElseLink = true;
          this._settings.ExpressionConditions.Add(expressionInfo);
        }
        this._settings.ExpertConditions.Clear();
        this._settings.ExpertConditions.Modified = true;
      }
      this.RefreshCaseConditions();
    }
    else
    {
      this.useExpertSystemCheckBox.CheckedChanged -= new EventHandler(this.useExpertSystemCheckBox_CheckedChanged);
      this.useExpertSystemCheckBox.Checked = !this.useExpertSystemCheckBox.Checked;
      this.useExpertSystemCheckBox.CheckedChanged += new EventHandler(this.useExpertSystemCheckBox_CheckedChanged);
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CaseSettingPageControl));
    this.CondsView = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.MiscIL = new ImageList(this.components);
    this.panel3 = new Panel();
    this.CaseFilterCheckBox = new CheckBox();
    this.SepPanel = new Panel();
    this.ValidateCaseButton = new Button();
    this.changeObjectTypeInExpression = new Button();
    this.useExpertSystemCheckBox = new CheckBox();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    this.CondsView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    this.CondsView.Dock = DockStyle.Fill;
    this.CondsView.FullRowSelect = true;
    this.CondsView.HideSelection = false;
    this.CondsView.LabelEdit = true;
    this.CondsView.Location = new Point(0, 0);
    this.CondsView.Name = "CondsView";
    this.CondsView.Size = new Size(741, 342);
    this.CondsView.SmallImageList = this.MiscIL;
    this.CondsView.TabIndex = 5;
    this.CondsView.UseCompatibleStateImageBehavior = false;
    this.CondsView.View = View.Details;
    this.CondsView.BeforeLabelEdit += new LabelEditEventHandler(this.CondsView_BeforeLabelEdit);
    this.CondsView.DoubleClick += new EventHandler(this.CondsView_DoubleClick);
    this.columnHeader1.Text = "Если..";
    this.columnHeader1.Width = (int) sbyte.MaxValue;
    this.columnHeader2.Text = "Перейти к..";
    this.columnHeader2.Width = 181;
    this.MiscIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("MiscIL.ImageStream");
    this.MiscIL.TransparentColor = Color.Fuchsia;
    this.MiscIL.Images.SetKeyName(0, "");
    this.MiscIL.Images.SetKeyName(1, "");
    this.panel3.AutoSize = true;
    this.panel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.panel3.BackColor = Color.Transparent;
    this.panel3.Controls.Add((Control) this.useExpertSystemCheckBox);
    this.panel3.Controls.Add((Control) this.CaseFilterCheckBox);
    this.panel3.Controls.Add((Control) this.SepPanel);
    this.panel3.Controls.Add((Control) this.ValidateCaseButton);
    this.panel3.Controls.Add((Control) this.changeObjectTypeInExpression);
    this.panel3.Dock = DockStyle.Bottom;
    this.panel3.Location = new Point(0, 342);
    this.panel3.Name = "panel3";
    this.panel3.Padding = new Padding(0, 10, 0, 0);
    this.panel3.Size = new Size(741, 119);
    this.panel3.TabIndex = 6;
    this.CaseFilterCheckBox.AutoSize = true;
    this.CaseFilterCheckBox.Dock = DockStyle.Top;
    this.CaseFilterCheckBox.ImeMode = ImeMode.NoControl;
    this.CaseFilterCheckBox.Location = new Point(0, 77);
    this.CaseFilterCheckBox.Name = "CaseFilterCheckBox";
    this.CaseFilterCheckBox.Size = new Size(741, 21);
    this.CaseFilterCheckBox.TabIndex = 5;
    this.CaseFilterCheckBox.Text = "Фильтровать вложенные объекты в зависимости от условий. В условиях можно использовать параметры объектов.";
    this.CaseFilterCheckBox.UseVisualStyleBackColor = true;
    this.SepPanel.Dock = DockStyle.Top;
    this.SepPanel.Location = new Point(0, 62);
    this.SepPanel.Name = "SepPanel";
    this.SepPanel.Size = new Size(741, 15);
    this.SepPanel.TabIndex = 7;
    this.ValidateCaseButton.Dock = DockStyle.Top;
    this.ValidateCaseButton.ImeMode = ImeMode.NoControl;
    this.ValidateCaseButton.Location = new Point(0, 36);
    this.ValidateCaseButton.Name = "ValidateCaseButton";
    this.ValidateCaseButton.Size = new Size(741, 26);
    this.ValidateCaseButton.TabIndex = 4;
    this.ValidateCaseButton.Text = "&Вычислить выбранное выражение";
    this.ValidateCaseButton.Click += new EventHandler(this.ValidateCaseButton_Click);
    this.changeObjectTypeInExpression.Dock = DockStyle.Top;
    this.changeObjectTypeInExpression.ImeMode = ImeMode.NoControl;
    this.changeObjectTypeInExpression.Location = new Point(0, 10);
    this.changeObjectTypeInExpression.Name = "changeObjectTypeInExpression";
    this.changeObjectTypeInExpression.Size = new Size(741, 26);
    this.changeObjectTypeInExpression.TabIndex = 8;
    this.changeObjectTypeInExpression.Text = "&Изменить тип объекта";
    this.changeObjectTypeInExpression.Visible = false;
    this.changeObjectTypeInExpression.Click += new EventHandler(this.ChangeExpressionType_Click);
    this.useExpertSystemCheckBox.AutoSize = true;
    this.useExpertSystemCheckBox.Dock = DockStyle.Bottom;
    this.useExpertSystemCheckBox.Location = new Point(0, 98);
    this.useExpertSystemCheckBox.Name = "useExpertSystemCheckBox";
    this.useExpertSystemCheckBox.Size = new Size(741, 21);
    this.useExpertSystemCheckBox.TabIndex = 9;
    this.useExpertSystemCheckBox.Text = "Использовать формулы экспертной системы";
    this.useExpertSystemCheckBox.UseVisualStyleBackColor = true;
    this.useExpertSystemCheckBox.CheckedChanged += new EventHandler(this.useExpertSystemCheckBox_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.CondsView);
    this.Controls.Add((Control) this.panel3);
    this.Name = nameof (CaseSettingPageControl);
    this.Size = new Size(741, 461);
    this.panel3.ResumeLayout(false);
    this.panel3.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
