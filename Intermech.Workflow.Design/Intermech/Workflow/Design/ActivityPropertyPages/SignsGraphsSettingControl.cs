// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.SignsGraphsSettingControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.PropertyEditors;
using Intermech.Signs.Interfaces;
using Intermech.Workflow.Briefcase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class SignsGraphsSettingControl : UserControl
{
  private bool _readOnly;
  private List<Guid> _signDTGuids = new List<Guid>();
  private List<string> _signDTNames = new List<string>();
  private ArrayList _signDTIDs = new ArrayList();
  private bool _signDTModified;
  private ActivitySettings _settings;
  private IMSAttributeType graphsAttrType;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox WhatSignGB;
  private EnhListView SignTypesView;
  private ColumnHeader columnHeader6;
  private Panel panel4;
  private ToolBar SignObjectTypesBar;
  private ToolBarButton AddObjTypesButton;
  private ToolBarButton DeleteObjTypesButton;
  private RadioButton DTypesRB2;
  private RadioButton DTypesRB1;
  private ComboBox WhatToSignCombo;
  private Panel ApproveVSpacer;
  private GroupBox RanksPanel;
  private EnhListView SignGraphView;
  private ColumnHeader columnHeader3;
  private ColumnHeader columnHeader4;
  private ColumnHeader columnHeader5;
  private Panel panel8;
  private ToolBar GraphsBar;
  private ToolBarButton toolBarButton3;
  private ToolBarButton DeleteSignsButton;
  private RadioButton SignAsGraphRadioButton;
  private RadioButton SignAsUserRadioButton;
  private CheckBox PersonalSignsCheckBox;
  private ImageList cmdsIL;
  private Panel signGraphOptionsPanel;

  public SignsGraphsSettingControl() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!this._readOnly)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, value);
    }
  }

  public void LoadSignsGraphsSettingControl(
    ActivitySettings settings,
    IDBAttribute attr,
    IDBObject activityObject,
    IUserSession activitySession)
  {
    this._settings = settings;
    settings.RequiredSigns = new RequiredSigns(attr);
    this.FillSignGraphs();
    settings.RequiredSigns.Modified = false;
    this.PersonalSignsCheckBox.Checked = this.SignAsGraphRadioButton.Checked && settings.ExtProperties.ReadBool("PersonalSigns");
    attr = activityObject.GetAttributeByID(wfConsts.AttrObjectTypesID);
    if (attr != null)
    {
      if (!attr.IsNull)
      {
        foreach (object obj in attr.Values)
        {
          if (!(obj.ToString() == ""))
          {
            Guid guid = new Guid(obj.ToString());
            IDBObjectType objectType = activitySession.GetObjectType(guid, false);
            if (objectType != null)
            {
              this.DTypesRB2.Checked = true;
              this._signDTGuids.Add(guid);
              this._signDTIDs.Add((object) objectType.ObjectType);
              this._signDTNames.Add(objectType.ObjectTypeName);
            }
            else
            {
              SimpleBriefcase globalBriefcase = BriefcaseAccessor.GlobalBriefcase;
              if (globalBriefcase != null)
              {
                this._signDTGuids.Add(guid);
                this._signDTIDs.Add((object) 0);
                MapperObject mapperObject = globalBriefcase.Map.Get(Domain.ObjectTypes, guid);
                this._signDTNames.Add("?? " + (mapperObject == null ? guid.ToString() : mapperObject.Caption));
              }
              this._signDTModified = true;
            }
          }
          else
            break;
        }
      }
      this.FillSignDocTypes();
    }
    this.DTypesRB_CheckedChanged((object) null, (EventArgs) null);
    attr = activityObject.GetAttributeByID(wfConsts.AttrWhatToSignID);
    if (attr == null)
      return;
    this.WhatToSignCombo.SelectedIndex = (int) attr.AsInteger;
  }

  private void SignObjectTypesBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (e.Button == this.AddObjTypesButton)
    {
      this.ChooseSignDocTypes();
    }
    else
    {
      ListViewItem listViewItem = (ListViewItem) null;
      if (this.SignTypesView.SelectedItems.Count > 0)
        listViewItem = this.SignTypesView.SelectedItems[0];
      if (listViewItem == null)
        return;
      int int32 = Convert.ToInt32(listViewItem.Tag);
      this._signDTIDs.RemoveAt(int32);
      this._signDTGuids.RemoveAt(int32);
      this._signDTNames.RemoveAt(int32);
      this._signDTModified = true;
      this.FillSignDocTypes();
    }
  }

  private void SignTypesView_SelectedIndexChanged(object sender, EventArgs e)
  {
    ListViewItem listViewItem = (ListViewItem) null;
    if (this.SignTypesView.SelectedItems.Count > 0)
      listViewItem = this.SignTypesView.SelectedItems[0];
    this.DeleteObjTypesButton.Enabled = listViewItem != null;
  }

  private void DTypesRB_CheckedChanged(object sender, EventArgs e)
  {
    bool flag = this.DTypesRB2.Checked;
    this.SignTypesView.Enabled = flag;
    this.SignObjectTypesBar.Enabled = flag;
    if (sender == null)
      return;
    this._signDTModified = true;
  }

  private void FillSignDocTypes()
  {
    this.SignTypesView.BeginUpdate();
    try
    {
      this.SignTypesView.SaveSelectedPos();
      this.SignTypesView.SmallImageList = BaseHolder.IconService.ImageList;
      this.SignTypesView.Items.Clear();
      for (int index = 0; index < this._signDTNames.Count; ++index)
      {
        ListViewItem listViewItem = this.SignTypesView.Items.Add(this._signDTNames[index]);
        listViewItem.Tag = (object) index;
        listViewItem.ImageIndex = BaseHolder.IconService.IndexOf(4, Convert.ToInt32(this._signDTIDs[index]));
      }
    }
    finally
    {
      this.SignTypesView.RestoreSelectedPos();
      this.SignTypesView.EndUpdate();
    }
  }

  private bool ChooseSignDocTypes()
  {
    using (SelectorForm selectorForm = new SelectorForm(LocalizationHolder.rm.GetString("ObjectTypes"), 4, false))
    {
      if (selectorForm.ShowDialog() == DialogResult.OK)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          foreach (object id in selectorForm.IDList)
          {
            int int32 = Convert.ToInt32(id);
            if (!this._signDTIDs.Contains((object) int32))
            {
              IDBObjectType objectType = sessionKeeper.Session.GetObjectType(int32);
              if (objectType != null)
              {
                this._signDTIDs.Add((object) int32);
                this._signDTGuids.Add(objectType.PropertiesStructure.ObjectTypeGuid);
                this._signDTNames.Add(objectType.ObjectTypeName);
              }
              this._signDTModified = true;
            }
          }
        }
        this.FillSignDocTypes();
        return true;
      }
    }
    return false;
  }

  private void SignAsGraphRadioButton_Click(object sender, EventArgs e)
  {
    if (this.SignGraphView.Items.Count != 0 || this.ChooseRequiredSigns())
      return;
    this.SignAsUserRadioButton.Checked = true;
  }

  private void DTypesRB2_Click(object sender, EventArgs e)
  {
    if (this.ChooseSignDocTypes())
      return;
    this.DTypesRB1.Checked = true;
  }

  public void FillSignGraphs()
  {
    this.SignGraphView.BeginUpdate();
    try
    {
      this.SignGraphView.Items.Clear();
      if (this._settings.RequiredSigns != null)
      {
        bool flag = false;
        int num = 0;
        foreach (string graphs1 in this._settings.RequiredSigns.GraphsSet)
        {
          GraphsCollection graphs2 = this._settings.RequiredSigns.GraphsSet[graphs1];
          if (graphs2 != null)
          {
            int int32 = Convert.ToInt32(graphs1);
            if (int32 > num)
              num = int32;
            if (flag)
              this.SignGraphView.Items.Add(LocalizationHolder.rm.GetString("Workflow.Design_17")).Tag = (object) graphs1;
            foreach (GraphClass graphClass in graphs2)
            {
              ListViewItem listViewItem = this.SignGraphView.Items.Add("");
              listViewItem.SubItems.Add(MiscFunx.GetSignGraphCaption(graphClass.Value, ref this.graphsAttrType));
              listViewItem.Tag = (object) graphs1;
              CheckBoxListViewSubItem boxListViewSubItem = new CheckBoxListViewSubItem();
              boxListViewSubItem.Tag = (object) graphClass;
              boxListViewSubItem.Checked = graphClass.StrongCheck;
              boxListViewSubItem.OnClick += new EventHandler(this.StrongSign_CheckedChanged);
              listViewItem.SubItems.Add((ListViewItem.ListViewSubItem) boxListViewSubItem);
            }
            flag = true;
          }
        }
        this._settings.SignsGroupID = num;
      }
    }
    finally
    {
      this.SignGraphView.EndUpdate();
    }
    if (this.SignGraphView.Items.Count > 0)
      this.SignAsGraphRadioButton.Checked = true;
    this.SignAsUserRadioButton_CheckedChanged((object) null, (EventArgs) null);
  }

  private void StrongSign_CheckedChanged(object sender, EventArgs e)
  {
    if (this._settings.RequiredSigns == null)
      return;
    this._settings.RequiredSigns.Modified = true;
  }

  private void SignGraphView_SelectedIndexChanged(object sender, EventArgs e)
  {
    ListViewItem listViewItem = (ListViewItem) null;
    if (this.SignGraphView.SelectedItems.Count > 0)
      listViewItem = this.SignGraphView.SelectedItems[0];
    this.DeleteSignsButton.Enabled = listViewItem != null && listViewItem.SubItems.Count > 1;
  }

  private bool ChooseRequiredSigns()
  {
    using (AddSignGraphsForm addSignGraphsForm = new AddSignGraphsForm())
    {
      addSignGraphsForm.NewGroupBox.Visible = this.SignGraphView.Items.Count > 0;
      if (addSignGraphsForm.ShowDialog() == DialogResult.OK)
      {
        GraphInfoList selected = addSignGraphsForm.Selected;
        if (selected.Count > sc_21977.ssp_workflow_21978(1457548502))
        {
          if (addSignGraphsForm.NewGroupBox.Checked)
            ++this._settings.SignsGroupID;
          else if (this.SignGraphView.SelectedItems.Count > 0)
            this._settings.SignsGroupID = Convert.ToInt32(this.SignGraphView.SelectedItems[0].Tag);
          foreach (GraphInfo graphInfo in (List<GraphInfo>) selected)
            this._settings.RequiredSigns.Add(graphInfo.GraphVal, graphInfo.StrongSign, this._settings.SignsGroupID);
          this.FillSignGraphs();
        }
        return this.SignGraphView.Items.Count > 0;
      }
    }
    return false;
  }

  private void GraphsBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (this._settings.RequiredSigns == null)
      return;
    switch (Convert.ToInt32(e.Button.Tag))
    {
      case 1:
        this.ChooseRequiredSigns();
        break;
      case 2:
        if (this.SignGraphView.SelectedItems.Count <= 0)
          break;
        ListViewItem selectedItem = this.SignGraphView.SelectedItems[0];
        int num = -1;
        if (selectedItem.SubItems.Count > 1 && selectedItem.SubItems[2] is CheckBoxListViewSubItem)
          num = this._settings.RequiredSigns.Delete(selectedItem.SubItems[2].Tag as GraphClass, Convert.ToInt32(selectedItem.Tag));
        if (num == 0 && selectedItem.Index > 0)
          selectedItem.ListView.Items.RemoveAt(selectedItem.Index - 1);
        this.SignGraphView.SaveSelectedPos();
        selectedItem.Remove();
        this.SignGraphView.RestoreSelectedPos();
        break;
    }
  }

  private void SignAsUserRadioButton_CheckedChanged(object sender, EventArgs e)
  {
    bool flag = this.SignAsGraphRadioButton.Checked && !this.ReadOnly;
    this.SignGraphView.Enabled = flag;
    this.GraphsBar.Enabled = flag;
    if (!flag)
      this.PersonalSignsCheckBox.Checked = false;
    this.PersonalSignsCheckBox.Enabled = flag;
    if (sender == null)
      return;
    this._settings.RequiredSigns.Modified = true;
  }

  private void UpdateRequiredStrongSigns()
  {
    foreach (ListViewItem listViewItem in this.SignGraphView.Items)
    {
      if (listViewItem.SubItems.Count > 1 && listViewItem.SubItems[2] is CheckBoxListViewSubItem)
      {
        CheckBoxListViewSubItem subItem = (CheckBoxListViewSubItem) listViewItem.SubItems[2];
        (subItem.Tag as GraphClass).StrongCheck = subItem.Checked;
      }
    }
  }

  public void SetVisibleGroup(bool visibleValue)
  {
    this.RanksPanel.Visible = visibleValue;
    this.WhatSignGB.Visible = visibleValue;
  }

  public void ResizeControl(int approveCheckGBHeight)
  {
    this.RanksPanel.Height = (this.Height - this.ApproveVSpacer.Height - this.PersonalSignsCheckBox.Height - approveCheckGBHeight) / 2;
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    if (this._settings.RequiredSigns != null)
    {
      if (this._settings.RequiredSigns.Modified)
      {
        modified = true;
        IDBAttribute byId = activityToSave.Attributes.FindByID(wfConsts.AttrRequiredSignsID);
        if (byId != null)
        {
          if (this.SignAsGraphRadioButton.Checked)
          {
            this.UpdateRequiredStrongSigns();
            this._settings.RequiredSigns.Save(byId);
          }
          else
            byId.Clear();
        }
      }
      IDBAttribute attributeById = activityToSave.GetAttributeByID(wfConsts.AttrWhatToSignID);
      if (attributeById != null && (long) this.WhatToSignCombo.SelectedIndex != attributeById.AsInteger)
      {
        modified = true;
        attributeById.AsInteger = (long) this.WhatToSignCombo.SelectedIndex;
      }
      this._settings.ExtProperties.WriteBool("PersonalSigns", this.PersonalSignsCheckBox.Checked, ExtPropertiesFlag.Approve);
    }
    if (this._signDTModified)
    {
      IDBAttribute attributeById = activityToSave.GetAttributeByID(wfConsts.AttrObjectTypesID);
      if (attributeById != null)
      {
        if (this.DTypesRB1.Checked)
        {
          attributeById.ClearValues();
        }
        else
        {
          object[] objArray = new object[this._signDTGuids.Count];
          for (int index = 0; index < this._signDTGuids.Count; ++index)
            objArray[index] = (object) this._signDTGuids[index];
          if (objArray.Length == 0)
            attributeById.ClearValues();
          else
            attributeById.Values = objArray;
        }
        modified = true;
      }
    }
    return modified;
  }

  public int RanksPanelHeight
  {
    get => this.RanksPanel.Height;
    set => this.RanksPanel.Height = value;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SignsGraphsSettingControl));
    this.WhatSignGB = new GroupBox();
    this.SignTypesView = new EnhListView();
    this.columnHeader6 = new ColumnHeader();
    this.panel4 = new Panel();
    this.SignObjectTypesBar = new ToolBar();
    this.AddObjTypesButton = new ToolBarButton();
    this.DeleteObjTypesButton = new ToolBarButton();
    this.cmdsIL = new ImageList(this.components);
    this.DTypesRB2 = new RadioButton();
    this.DTypesRB1 = new RadioButton();
    this.WhatToSignCombo = new ComboBox();
    this.ApproveVSpacer = new Panel();
    this.RanksPanel = new GroupBox();
    this.SignGraphView = new EnhListView();
    this.columnHeader3 = new ColumnHeader();
    this.columnHeader4 = new ColumnHeader();
    this.columnHeader5 = new ColumnHeader();
    this.signGraphOptionsPanel = new Panel();
    this.SignAsUserRadioButton = new RadioButton();
    this.SignAsGraphRadioButton = new RadioButton();
    this.panel8 = new Panel();
    this.GraphsBar = new ToolBar();
    this.toolBarButton3 = new ToolBarButton();
    this.DeleteSignsButton = new ToolBarButton();
    this.PersonalSignsCheckBox = new CheckBox();
    this.WhatSignGB.SuspendLayout();
    this.panel4.SuspendLayout();
    this.RanksPanel.SuspendLayout();
    this.signGraphOptionsPanel.SuspendLayout();
    this.panel8.SuspendLayout();
    this.SuspendLayout();
    this.WhatSignGB.Controls.Add((Control) this.SignTypesView);
    this.WhatSignGB.Controls.Add((Control) this.panel4);
    this.WhatSignGB.Controls.Add((Control) this.DTypesRB2);
    this.WhatSignGB.Controls.Add((Control) this.DTypesRB1);
    this.WhatSignGB.Controls.Add((Control) this.WhatToSignCombo);
    this.WhatSignGB.Dock = DockStyle.Fill;
    this.WhatSignGB.Location = new Point(0, 228);
    this.WhatSignGB.Name = "WhatSignGB";
    this.WhatSignGB.Padding = new Padding(11, 11, 11, 5);
    this.WhatSignGB.Size = new Size(650, 244);
    this.WhatSignGB.TabIndex = 20;
    this.WhatSignGB.TabStop = false;
    this.WhatSignGB.Text = "Подписывать";
    this.SignTypesView.AllowManualSorting = true;
    this.SignTypesView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.SignTypesView.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader6
    });
    this.SignTypesView.FullRowSelect = true;
    this.SignTypesView.HideSelection = false;
    this.SignTypesView.Location = new Point(13, 118);
    this.SignTypesView.Name = "SignTypesView";
    this.SignTypesView.OwnerDraw = true;
    this.SignTypesView.RadioGroups = false;
    this.SignTypesView.Size = new Size(624, 118);
    this.SignTypesView.SortColumn = 0;
    this.SignTypesView.Sorting = SortOrder.Ascending;
    this.SignTypesView.SubitemImages = (ImageList) null;
    this.SignTypesView.TabIndex = 4;
    this.SignTypesView.UseCompatibleStateImageBehavior = false;
    this.SignTypesView.View = View.Details;
    this.SignTypesView.SelectedIndexChanged += new EventHandler(this.SignTypesView_SelectedIndexChanged);
    this.columnHeader6.Text = "Тип";
    this.columnHeader6.Width = 229;
    this.panel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.panel4.Controls.Add((Control) this.SignObjectTypesBar);
    this.panel4.Location = new Point(228, 86);
    this.panel4.Name = "panel4";
    this.panel4.Size = new Size(78, 26);
    this.panel4.TabIndex = 8;
    this.SignObjectTypesBar.Appearance = ToolBarAppearance.Flat;
    this.SignObjectTypesBar.AutoSize = false;
    this.SignObjectTypesBar.Buttons.AddRange(new ToolBarButton[2]
    {
      this.AddObjTypesButton,
      this.DeleteObjTypesButton
    });
    this.SignObjectTypesBar.ButtonSize = new Size(22, 22);
    this.SignObjectTypesBar.Divider = false;
    this.SignObjectTypesBar.Dock = DockStyle.None;
    this.SignObjectTypesBar.DropDownArrows = true;
    this.SignObjectTypesBar.ImageList = this.cmdsIL;
    this.SignObjectTypesBar.ImeMode = ImeMode.NoControl;
    this.SignObjectTypesBar.Location = new Point(0, 0);
    this.SignObjectTypesBar.Name = "SignObjectTypesBar";
    this.SignObjectTypesBar.ShowToolTips = true;
    this.SignObjectTypesBar.Size = new Size(67, 30);
    this.SignObjectTypesBar.TabIndex = 8;
    this.SignObjectTypesBar.ButtonClick += new ToolBarButtonClickEventHandler(this.SignObjectTypesBar_ButtonClick);
    this.AddObjTypesButton.ImageIndex = 0;
    this.AddObjTypesButton.Name = "AddObjTypesButton";
    this.AddObjTypesButton.Tag = (object) "1";
    this.AddObjTypesButton.ToolTipText = "Добавить типы объектов";
    this.DeleteObjTypesButton.Enabled = false;
    this.DeleteObjTypesButton.ImageIndex = 1;
    this.DeleteObjTypesButton.Name = "DeleteObjTypesButton";
    this.DeleteObjTypesButton.ToolTipText = "Исключить выбранный тип объектов";
    this.cmdsIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("cmdsIL.ImageStream");
    this.cmdsIL.TransparentColor = Color.Fuchsia;
    this.cmdsIL.Images.SetKeyName(0, "add.ico");
    this.cmdsIL.Images.SetKeyName(1, "del.ico");
    this.cmdsIL.Images.SetKeyName(2, "answer.ico");
    this.DTypesRB2.AutoSize = true;
    this.DTypesRB2.ImeMode = ImeMode.NoControl;
    this.DTypesRB2.Location = new Point(13, 89);
    this.DTypesRB2.Name = "DTypesRB2";
    this.DTypesRB2.Padding = new Padding(0, 0, 0, 3);
    this.DTypesRB2.Size = new Size(183, 24);
    this.DTypesRB2.TabIndex = 2;
    this.DTypesRB2.Text = "Только объекты типов:";
    this.DTypesRB2.CheckedChanged += new EventHandler(this.DTypesRB_CheckedChanged);
    this.DTypesRB2.Click += new EventHandler(this.DTypesRB2_Click);
    this.DTypesRB1.AutoSize = true;
    this.DTypesRB1.Checked = true;
    this.DTypesRB1.ImeMode = ImeMode.NoControl;
    this.DTypesRB1.Location = new Point(13, 63 /*0x3F*/);
    this.DTypesRB1.Name = "DTypesRB1";
    this.DTypesRB1.Padding = new Padding(0, 3, 0, 0);
    this.DTypesRB1.Size = new Size(223, 24);
    this.DTypesRB1.TabIndex = 1;
    this.DTypesRB1.TabStop = true;
    this.DTypesRB1.Text = "Все прикрепленные объекты";
    this.DTypesRB1.CheckedChanged += new EventHandler(this.DTypesRB_CheckedChanged);
    this.WhatToSignCombo.Dock = DockStyle.Top;
    this.WhatToSignCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.WhatToSignCombo.ItemHeight = 16 /*0x10*/;
    this.WhatToSignCombo.Items.AddRange(new object[3]
    {
      (object) "Объекты и извещения",
      (object) "Только объекты",
      (object) "Только извещения"
    });
    this.WhatToSignCombo.Location = new Point(11, 26);
    this.WhatToSignCombo.Name = "WhatToSignCombo";
    this.WhatToSignCombo.Size = new Size(628, 24);
    this.WhatToSignCombo.TabIndex = 0;
    this.ApproveVSpacer.Dock = DockStyle.Top;
    this.ApproveVSpacer.Location = new Point(0, 222);
    this.ApproveVSpacer.Name = "ApproveVSpacer";
    this.ApproveVSpacer.Size = new Size(650, 6);
    this.ApproveVSpacer.TabIndex = 19;
    this.RanksPanel.BackColor = Color.Transparent;
    this.RanksPanel.Controls.Add((Control) this.SignGraphView);
    this.RanksPanel.Controls.Add((Control) this.signGraphOptionsPanel);
    this.RanksPanel.Controls.Add((Control) this.PersonalSignsCheckBox);
    this.RanksPanel.Dock = DockStyle.Top;
    this.RanksPanel.Location = new Point(0, 0);
    this.RanksPanel.Name = "RanksPanel";
    this.RanksPanel.Padding = new Padding(11, 5, 11, 11);
    this.RanksPanel.Size = new Size(650, 222);
    this.RanksPanel.TabIndex = 18;
    this.RanksPanel.TabStop = false;
    this.RanksPanel.Text = "Графы для подписи";
    this.SignGraphView.AllowManualSorting = true;
    this.SignGraphView.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnHeader3,
      this.columnHeader4,
      this.columnHeader5
    });
    this.SignGraphView.Dock = DockStyle.Fill;
    this.SignGraphView.FullRowSelect = true;
    this.SignGraphView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.SignGraphView.HideSelection = false;
    this.SignGraphView.Location = new Point(11, 84);
    this.SignGraphView.MultiSelect = false;
    this.SignGraphView.Name = "SignGraphView";
    this.SignGraphView.OwnerDraw = true;
    this.SignGraphView.RadioGroups = false;
    this.SignGraphView.Size = new Size(628, 103);
    this.SignGraphView.SortColumn = 0;
    this.SignGraphView.SubitemImages = (ImageList) null;
    this.SignGraphView.TabIndex = 8;
    this.SignGraphView.UseCompatibleStateImageBehavior = false;
    this.SignGraphView.View = View.Details;
    this.SignGraphView.SelectedIndexChanged += new EventHandler(this.SignGraphView_SelectedIndexChanged);
    this.columnHeader3.Text = "";
    this.columnHeader3.Width = 39;
    this.columnHeader4.Text = "Графа";
    this.columnHeader4.Width = 193;
    this.columnHeader5.Text = "Строгий контроль";
    this.columnHeader5.TextAlign = HorizontalAlignment.Center;
    this.columnHeader5.Width = 120;
    this.signGraphOptionsPanel.Controls.Add((Control) this.SignAsUserRadioButton);
    this.signGraphOptionsPanel.Controls.Add((Control) this.SignAsGraphRadioButton);
    this.signGraphOptionsPanel.Controls.Add((Control) this.panel8);
    this.signGraphOptionsPanel.Dock = DockStyle.Top;
    this.signGraphOptionsPanel.Location = new Point(11, 20);
    this.signGraphOptionsPanel.Name = "signGraphOptionsPanel";
    this.signGraphOptionsPanel.Size = new Size(628, 64 /*0x40*/);
    this.signGraphOptionsPanel.TabIndex = 10;
    this.SignAsUserRadioButton.AutoSize = true;
    this.SignAsUserRadioButton.Checked = true;
    this.SignAsUserRadioButton.ImeMode = ImeMode.NoControl;
    this.SignAsUserRadioButton.Location = new Point(3, 3);
    this.SignAsUserRadioButton.Name = "SignAsUserRadioButton";
    this.SignAsUserRadioButton.Size = new Size(201, 21);
    this.SignAsUserRadioButton.TabIndex = 0;
    this.SignAsUserRadioButton.TabStop = true;
    this.SignAsUserRadioButton.Text = "Подписать в любой графе";
    this.SignAsUserRadioButton.CheckedChanged += new EventHandler(this.SignAsUserRadioButton_CheckedChanged);
    this.SignAsGraphRadioButton.AutoSize = true;
    this.SignAsGraphRadioButton.ImeMode = ImeMode.NoControl;
    this.SignAsGraphRadioButton.Location = new Point(3, 28);
    this.SignAsGraphRadioButton.Name = "SignAsGraphRadioButton";
    this.SignAsGraphRadioButton.Padding = new Padding(0, 0, 0, 3);
    this.SignAsGraphRadioButton.Size = new Size(165, 24);
    this.SignAsGraphRadioButton.TabIndex = 1;
    this.SignAsGraphRadioButton.Tag = (object) "1";
    this.SignAsGraphRadioButton.Text = "Подписать в графах:";
    this.SignAsGraphRadioButton.CheckedChanged += new EventHandler(this.SignAsUserRadioButton_CheckedChanged);
    this.SignAsGraphRadioButton.Click += new EventHandler(this.SignAsGraphRadioButton_Click);
    this.panel8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.panel8.Controls.Add((Control) this.GraphsBar);
    this.panel8.Location = new Point(217, 28);
    this.panel8.Name = "panel8";
    this.panel8.Size = new Size(78, 27);
    this.panel8.TabIndex = 7;
    this.GraphsBar.Appearance = ToolBarAppearance.Flat;
    this.GraphsBar.AutoSize = false;
    this.GraphsBar.Buttons.AddRange(new ToolBarButton[2]
    {
      this.toolBarButton3,
      this.DeleteSignsButton
    });
    this.GraphsBar.ButtonSize = new Size(22, 22);
    this.GraphsBar.Divider = false;
    this.GraphsBar.Dock = DockStyle.None;
    this.GraphsBar.DropDownArrows = true;
    this.GraphsBar.ImageList = this.cmdsIL;
    this.GraphsBar.ImeMode = ImeMode.NoControl;
    this.GraphsBar.Location = new Point(0, 0);
    this.GraphsBar.Name = "GraphsBar";
    this.GraphsBar.ShowToolTips = true;
    this.GraphsBar.Size = new Size(67, 30);
    this.GraphsBar.TabIndex = 8;
    this.GraphsBar.ButtonClick += new ToolBarButtonClickEventHandler(this.GraphsBar_ButtonClick);
    this.toolBarButton3.ImageIndex = 0;
    this.toolBarButton3.Name = "toolBarButton3";
    this.toolBarButton3.Tag = (object) "1";
    this.toolBarButton3.ToolTipText = "Добавить графы";
    this.DeleteSignsButton.Enabled = false;
    this.DeleteSignsButton.ImageIndex = 1;
    this.DeleteSignsButton.Name = "DeleteSignsButton";
    this.DeleteSignsButton.Tag = (object) "2";
    this.DeleteSignsButton.ToolTipText = "Удалить выбранную графу";
    this.PersonalSignsCheckBox.AutoSize = true;
    this.PersonalSignsCheckBox.Dock = DockStyle.Bottom;
    this.PersonalSignsCheckBox.ImeMode = ImeMode.NoControl;
    this.PersonalSignsCheckBox.Location = new Point(11, 187);
    this.PersonalSignsCheckBox.Name = "PersonalSignsCheckBox";
    this.PersonalSignsCheckBox.Padding = new Padding(0, 3, 0, 0);
    this.PersonalSignsCheckBox.Size = new Size(628, 24);
    this.PersonalSignsCheckBox.TabIndex = 9;
    this.PersonalSignsCheckBox.Text = "Требовать персональные подписи исполнителей";
    this.PersonalSignsCheckBox.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.WhatSignGB);
    this.Controls.Add((Control) this.ApproveVSpacer);
    this.Controls.Add((Control) this.RanksPanel);
    this.Name = nameof (SignsGraphsSettingControl);
    this.Size = new Size(650, 472);
    this.WhatSignGB.ResumeLayout(false);
    this.WhatSignGB.PerformLayout();
    this.panel4.ResumeLayout(false);
    this.RanksPanel.ResumeLayout(false);
    this.RanksPanel.PerformLayout();
    this.signGraphOptionsPanel.ResumeLayout(false);
    this.signGraphOptionsPanel.PerformLayout();
    this.panel8.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
