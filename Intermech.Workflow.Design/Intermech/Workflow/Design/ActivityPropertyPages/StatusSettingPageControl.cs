// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.StatusSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class StatusSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox LCGroupBox;
  private Panel LCPanel2;
  private EnhListView LCView2;
  private ColumnHeader columnHeader9;
  private ColumnHeader columnHeader10;
  private Label label15;
  private ToolBar LCStepToolBar2;
  private ToolBarButton toolBarButton5;
  private ToolBarButton toolBarButton6;
  private ToolBarButton toolBarButton7;
  private Label LCLabel2;
  private Panel LCPanel1;
  private EnhListView LCView;
  private ColumnHeader columnHeader7;
  private ColumnHeader columnHeader8;
  private Label label9;
  private ToolBar LCStepToolBar;
  private ToolBarButton toolBarButton1;
  private ToolBarButton toolBarButton4;
  private ToolBarButton toolBarButton2;
  private Label LCLabel1;
  private ImageList cmdsIL;
  private GroupBox processLCDirectionGroup;
  private ComboBox processLCDirectionComboBox;

  public StatusSettingPageControl() => this.InitializeComponent();

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

  public bool LoadStatusSettingPageControl(ActivitySettings settings, IDBObject activityObject)
  {
    this._settings = settings;
    bool flag = false;
    if (settings.ActivityType == wfConsts.SchemesTypeID || settings.ActivityType == wfConsts.ProcessesTypeID)
    {
      flag = true;
    }
    else
    {
      IDBAttribute attributeById = activityObject.GetAttributeByID(wfConsts.AttrLCConfigAttrID);
      if (attributeById != null && !attributeById.IsNull)
      {
        settings.LcInfoList = new LCInfoList();
        settings.LcInfoList.Load(attributeById);
      }
      this.InitializeProcessLCDirection((ProcessLCDirection) settings.ExtProperties.ReadInteger("ProcessLCDirection", 0L));
    }
    return flag;
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    if (this._settings != null && this._settings.ActivityType == wfConsts.LifeCycleTypeID)
    {
      this.LCLabel1.Visible = false;
      this.LCLabel2.Visible = false;
      this.LCPanel2.Visible = false;
      this.processLCDirectionGroup.Visible = false;
      this.LCPanel1.Dock = DockStyle.Fill;
    }
    else
      this.LCGroupBox_Resize((object) null, (EventArgs) null);
    this.FillLCSteps();
  }

  private void LCListNeeded()
  {
    if (this._settings.LcInfoList != null)
      return;
    this._settings.LcInfoList = new LCInfoList();
  }

  private void LCStepToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (sender == null)
      return;
    EnhListView enhListView = this.LCView;
    LCExec exec = LCExec.Before;
    if (sender == this.LCStepToolBar2)
    {
      enhListView = this.LCView2;
      exec = LCExec.After;
    }
    if (e != null && Convert.ToInt32(e.Button.Tag) == 2)
    {
      if (enhListView.SelectedItems.Count > 0)
      {
        this.LCListNeeded();
        this._settings.LcInfoList.Remove(enhListView.SelectedItems[0].Tag as LCInfo);
        this._settings.LcInfoList.Modified = true;
      }
      this.FillLCSteps();
    }
    else
    {
      if (e != null && Convert.ToInt32(e.Button.Tag) == sc_21982.ssp_workflow_21983(1465130653))
      {
        if (enhListView.SelectedItems.Count <= 0)
          return;
        sender = (object) (enhListView.SelectedItems[0].Tag as LCInfo);
      }
      bool flag = !(sender is LCInfo);
      using (LCStepSetupForm f = new LCStepSetupForm())
      {
        LCInfo lcInfo = (LCInfo) null;
        if (!flag)
        {
          lcInfo = (LCInfo) sender;
          f.LCInfo = lcInfo;
        }
        if (f.ShowDialog() != DialogResult.OK)
          return;
        this.LCListNeeded();
        if (flag && this._settings.LcInfoList.Count<LCInfo>((Func<LCInfo, bool>) (x => x.ObjectType == f.LCInfo.ObjectType && x.ExecTime == exec)) == 0)
        {
          f.LCInfo.ExecTime = exec;
          this._settings.LcInfoList.Add(f.LCInfo);
          this._settings.LcInfoList.Modified = true;
        }
        else if (lcInfo != null && !f.LCInfo.Equals((object) lcInfo))
        {
          lcInfo.Assign(f.LCInfo);
          this._settings.LcInfoList.Modified = true;
        }
        this.FillLCSteps();
      }
    }
  }

  private void FillLCSteps()
  {
    this.FillLCSteps(this.LCView, LCExec.Before);
    this.FillLCSteps(this.LCView2, LCExec.After);
  }

  private void FillLCSteps(EnhListView view, LCExec exec)
  {
    if (this._settings?.LcInfoList == null)
      return;
    view.BeginUpdate();
    try
    {
      view.Items.Clear();
      view.SmallImageList = BaseHolder.IconService.ImageList;
      view.SubitemImages = BaseHolder.IconService.ImageList;
      foreach (LCInfo lcInfo in (List<LCInfo>) this._settings.LcInfoList)
      {
        if (lcInfo.ExecTime == exec)
        {
          ListViewItem listViewItem = view.Items.Add(lcInfo.TypeName);
          listViewItem.ImageIndex = BaseHolder.IconService.IndexOf(4, lcInfo.ObjectType);
          int imageIndex = -1;
          try
          {
            imageIndex = BaseHolder.IconService.IndexOf(8, lcInfo.LevelID);
          }
          catch
          {
          }
          string str = $" ({LocalizationHolder.rm.GetString("LC" + lcInfo.Kind.ToString())})";
          listViewItem.SubItems.Add((ListViewItem.ListViewSubItem) new ImageListViewSubItem(lcInfo.StepName + str, imageIndex));
          listViewItem.Tag = (object) lcInfo;
        }
      }
      if (view.Items.Count > 0)
        view.Items[0].Selected = true;
      view.HighlightInvalidItems();
    }
    finally
    {
      view.EndUpdate();
    }
  }

  private void LCView_DoubleClick(object sender, EventArgs e)
  {
    EnhListView enhListView = sender as EnhListView;
    if (this.ReadOnly || enhListView == null || enhListView.SelectedItems.Count <= 0)
      return;
    this.LCStepToolBar_ButtonClick((object) (enhListView.SelectedItems[0].Tag as LCInfo), (ToolBarButtonClickEventArgs) null);
  }

  private void LCGroupBox_Resize(object sender, EventArgs e)
  {
    if (!this.LCPanel2.Visible)
      return;
    int num1 = this.LCGroupBox.ClientSize.Height - this.LCLabel1.Height - this.LCLabel2.Height - this.processLCDirectionGroup.Height - this.LCGroupBox.Padding.Top - this.LCGroupBox.Padding.Bottom - 5;
    int num2 = (this.LCGroupBox.DisplayRectangle.Height - this.LCLabel1.Height - this.LCLabel2.Height - this.processLCDirectionGroup.Height) / 2;
    this.LCPanel1.Height = num2;
    this.LCPanel2.Height = num2;
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    if (this._settings.LcInfoList != null && this._settings.LcInfoList.Modified)
    {
      modified = true;
      IDBAttribute attributeById = activityToSave.GetAttributeByID(wfConsts.AttrLCConfigAttrID);
      if (attributeById != null)
        this._settings.LcInfoList.Save(attributeById);
      else
        activityToSave.Attributes.AddAttribute(wfConsts.AttrLCConfigAttrID, false, new object[1]
        {
          (object) this._settings.LcInfoList.AsString
        });
    }
    if (this.processLCDirectionComboBox.SelectedItem != null && this._settings.ExtProperties.Write("ProcessLCDirection", (long) ((StatusSettingPageControl.LCDirectionItem) this.processLCDirectionComboBox.SelectedItem).Value, ExtPropertiesFlag.RemoteSubprocess))
      modified = true;
    return modified;
  }

  private void InitializeProcessLCDirection(ProcessLCDirection processLcDirection = ProcessLCDirection.All)
  {
    this.processLCDirectionComboBox.Items.Clear();
    int num = 0;
    foreach (ProcessLCDirection processLcDirection1 in Enum.GetValues(typeof (ProcessLCDirection)))
    {
      this.processLCDirectionComboBox.Items.Add((object) new StatusSettingPageControl.LCDirectionItem(processLcDirection1));
      if (processLcDirection1.Equals((object) processLcDirection))
        num = this.processLCDirectionComboBox.Items.Count - 1;
    }
    this.processLCDirectionComboBox.SelectedIndex = num;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (StatusSettingPageControl));
    this.LCGroupBox = new GroupBox();
    this.LCPanel2 = new Panel();
    this.LCView2 = new EnhListView();
    this.columnHeader9 = new ColumnHeader();
    this.columnHeader10 = new ColumnHeader();
    this.label15 = new Label();
    this.LCStepToolBar2 = new ToolBar();
    this.toolBarButton5 = new ToolBarButton();
    this.toolBarButton6 = new ToolBarButton();
    this.toolBarButton7 = new ToolBarButton();
    this.cmdsIL = new ImageList(this.components);
    this.LCLabel2 = new Label();
    this.LCPanel1 = new Panel();
    this.LCView = new EnhListView();
    this.columnHeader7 = new ColumnHeader();
    this.columnHeader8 = new ColumnHeader();
    this.label9 = new Label();
    this.LCStepToolBar = new ToolBar();
    this.toolBarButton1 = new ToolBarButton();
    this.toolBarButton4 = new ToolBarButton();
    this.toolBarButton2 = new ToolBarButton();
    this.LCLabel1 = new Label();
    this.processLCDirectionGroup = new GroupBox();
    this.processLCDirectionComboBox = new ComboBox();
    this.LCGroupBox.SuspendLayout();
    this.LCPanel2.SuspendLayout();
    this.LCPanel1.SuspendLayout();
    this.processLCDirectionGroup.SuspendLayout();
    this.SuspendLayout();
    this.LCGroupBox.BackColor = SystemColors.ControlLightLight;
    this.LCGroupBox.Controls.Add((Control) this.LCPanel2);
    this.LCGroupBox.Controls.Add((Control) this.processLCDirectionGroup);
    this.LCGroupBox.Controls.Add((Control) this.LCLabel2);
    this.LCGroupBox.Controls.Add((Control) this.LCPanel1);
    this.LCGroupBox.Controls.Add((Control) this.LCLabel1);
    this.LCGroupBox.Dock = DockStyle.Fill;
    this.LCGroupBox.Location = new Point(0, 0);
    this.LCGroupBox.Name = "LCGroupBox";
    this.LCGroupBox.Padding = new Padding(11, 7, 11, 11);
    this.LCGroupBox.Size = new Size(610, 497);
    this.LCGroupBox.TabIndex = 4;
    this.LCGroupBox.TabStop = false;
    this.LCGroupBox.Text = "Изменить статус вложений";
    this.LCGroupBox.Resize += new EventHandler(this.LCGroupBox_Resize);
    this.LCPanel2.BorderStyle = BorderStyle.Fixed3D;
    this.LCPanel2.Controls.Add((Control) this.LCView2);
    this.LCPanel2.Controls.Add((Control) this.label15);
    this.LCPanel2.Controls.Add((Control) this.LCStepToolBar2);
    this.LCPanel2.Dock = DockStyle.Top;
    this.LCPanel2.Location = new Point(11, 253);
    this.LCPanel2.Name = "LCPanel2";
    this.LCPanel2.Size = new Size(588, 170);
    this.LCPanel2.TabIndex = 4;
    this.LCView2.AllowManualSorting = true;
    this.LCView2.BorderStyle = BorderStyle.None;
    this.LCView2.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader9,
      this.columnHeader10
    });
    this.LCView2.Dock = DockStyle.Fill;
    this.LCView2.FullRowSelect = true;
    this.LCView2.HideSelection = false;
    this.LCView2.Location = new Point(0, 0);
    this.LCView2.MultiSelect = false;
    this.LCView2.Name = "LCView2";
    this.LCView2.OwnerDraw = true;
    this.LCView2.RadioGroups = false;
    this.LCView2.Size = new Size(560, 166);
    this.LCView2.SortColumn = 0;
    this.LCView2.SubitemImages = (ImageList) null;
    this.LCView2.TabIndex = 5;
    this.LCView2.UseCompatibleStateImageBehavior = false;
    this.LCView2.View = View.Details;
    this.LCView2.DoubleClick += new EventHandler(this.LCView_DoubleClick);
    this.columnHeader9.Text = "Тип объектов";
    this.columnHeader9.Width = 137;
    this.columnHeader10.Text = "Статус";
    this.columnHeader10.Width = 174;
    this.label15.BorderStyle = BorderStyle.Fixed3D;
    this.label15.Dock = DockStyle.Right;
    this.label15.ImeMode = ImeMode.NoControl;
    this.label15.Location = new Point(560, 0);
    this.label15.Name = "label15";
    this.label15.Size = new Size(2, 166);
    this.label15.TabIndex = 7;
    this.label15.Text = "label15";
    this.LCStepToolBar2.Appearance = ToolBarAppearance.Flat;
    this.LCStepToolBar2.Buttons.AddRange(new ToolBarButton[3]
    {
      this.toolBarButton5,
      this.toolBarButton6,
      this.toolBarButton7
    });
    this.LCStepToolBar2.ButtonSize = new Size(22, 22);
    this.LCStepToolBar2.Divider = false;
    this.LCStepToolBar2.Dock = DockStyle.Right;
    this.LCStepToolBar2.DropDownArrows = true;
    this.LCStepToolBar2.ImageList = this.cmdsIL;
    this.LCStepToolBar2.ImeMode = ImeMode.NoControl;
    this.LCStepToolBar2.Location = new Point(562, 0);
    this.LCStepToolBar2.Name = "LCStepToolBar2";
    this.LCStepToolBar2.ShowToolTips = true;
    this.LCStepToolBar2.Size = new Size(22, 166);
    this.LCStepToolBar2.TabIndex = 6;
    this.LCStepToolBar2.TextAlign = ToolBarTextAlign.Right;
    this.LCStepToolBar2.ButtonClick += new ToolBarButtonClickEventHandler(this.LCStepToolBar_ButtonClick);
    this.toolBarButton5.ImageIndex = 0;
    this.toolBarButton5.Name = "toolBarButton5";
    this.toolBarButton5.Tag = (object) "1";
    this.toolBarButton5.ToolTipText = "Добавить строку";
    this.toolBarButton6.ImageIndex = 2;
    this.toolBarButton6.Name = "toolBarButton6";
    this.toolBarButton6.Tag = (object) "3";
    this.toolBarButton6.ToolTipText = "Редактировать строку";
    this.toolBarButton7.ImageIndex = 1;
    this.toolBarButton7.Name = "toolBarButton7";
    this.toolBarButton7.Tag = (object) "2";
    this.toolBarButton7.ToolTipText = "Удалить строку";
    this.cmdsIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("cmdsIL.ImageStream");
    this.cmdsIL.TransparentColor = Color.Fuchsia;
    this.cmdsIL.Images.SetKeyName(0, "add.ico");
    this.cmdsIL.Images.SetKeyName(1, "del.ico");
    this.cmdsIL.Images.SetKeyName(2, "answer.ico");
    this.LCLabel2.Dock = DockStyle.Top;
    this.LCLabel2.ImeMode = ImeMode.NoControl;
    this.LCLabel2.Location = new Point(11, 219);
    this.LCLabel2.Name = "LCLabel2";
    this.LCLabel2.Padding = new Padding(0, 10, 0, 0);
    this.LCLabel2.Size = new Size(588, 34);
    this.LCLabel2.TabIndex = 5;
    this.LCLabel2.Text = "После выполнения действия:";
    this.LCPanel1.BorderStyle = BorderStyle.Fixed3D;
    this.LCPanel1.Controls.Add((Control) this.LCView);
    this.LCPanel1.Controls.Add((Control) this.label9);
    this.LCPanel1.Controls.Add((Control) this.LCStepToolBar);
    this.LCPanel1.Dock = DockStyle.Top;
    this.LCPanel1.Location = new Point(11, 49);
    this.LCPanel1.Name = "LCPanel1";
    this.LCPanel1.Size = new Size(588, 170);
    this.LCPanel1.TabIndex = 2;
    this.LCView.AllowManualSorting = true;
    this.LCView.BorderStyle = BorderStyle.None;
    this.LCView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader7,
      this.columnHeader8
    });
    this.LCView.Dock = DockStyle.Fill;
    this.LCView.FullRowSelect = true;
    this.LCView.HideSelection = false;
    this.LCView.Location = new Point(0, 0);
    this.LCView.MultiSelect = false;
    this.LCView.Name = "LCView";
    this.LCView.OwnerDraw = true;
    this.LCView.RadioGroups = false;
    this.LCView.Size = new Size(560, 166);
    this.LCView.SortColumn = 0;
    this.LCView.SubitemImages = (ImageList) null;
    this.LCView.TabIndex = 5;
    this.LCView.UseCompatibleStateImageBehavior = false;
    this.LCView.View = View.Details;
    this.LCView.DoubleClick += new EventHandler(this.LCView_DoubleClick);
    this.columnHeader7.Text = "Тип объектов";
    this.columnHeader7.Width = 137;
    this.columnHeader8.Text = "Статус";
    this.columnHeader8.Width = 174;
    this.label9.BorderStyle = BorderStyle.Fixed3D;
    this.label9.Dock = DockStyle.Right;
    this.label9.ImeMode = ImeMode.NoControl;
    this.label9.Location = new Point(560, 0);
    this.label9.Name = "label9";
    this.label9.Size = new Size(2, 166);
    this.label9.TabIndex = 7;
    this.label9.Text = "label9";
    this.LCStepToolBar.Appearance = ToolBarAppearance.Flat;
    this.LCStepToolBar.Buttons.AddRange(new ToolBarButton[3]
    {
      this.toolBarButton1,
      this.toolBarButton4,
      this.toolBarButton2
    });
    this.LCStepToolBar.ButtonSize = new Size(22, 22);
    this.LCStepToolBar.Divider = false;
    this.LCStepToolBar.Dock = DockStyle.Right;
    this.LCStepToolBar.DropDownArrows = true;
    this.LCStepToolBar.ImageList = this.cmdsIL;
    this.LCStepToolBar.ImeMode = ImeMode.NoControl;
    this.LCStepToolBar.Location = new Point(562, 0);
    this.LCStepToolBar.Name = "LCStepToolBar";
    this.LCStepToolBar.ShowToolTips = true;
    this.LCStepToolBar.Size = new Size(22, 166);
    this.LCStepToolBar.TabIndex = 6;
    this.LCStepToolBar.TextAlign = ToolBarTextAlign.Right;
    this.LCStepToolBar.ButtonClick += new ToolBarButtonClickEventHandler(this.LCStepToolBar_ButtonClick);
    this.toolBarButton1.ImageIndex = 0;
    this.toolBarButton1.Name = "toolBarButton1";
    this.toolBarButton1.Tag = (object) "1";
    this.toolBarButton1.ToolTipText = "Добавить строку";
    this.toolBarButton4.ImageIndex = 2;
    this.toolBarButton4.Name = "toolBarButton4";
    this.toolBarButton4.Tag = (object) "3";
    this.toolBarButton4.ToolTipText = "Редактировать строку";
    this.toolBarButton2.ImageIndex = 1;
    this.toolBarButton2.Name = "toolBarButton2";
    this.toolBarButton2.Tag = (object) "2";
    this.toolBarButton2.ToolTipText = "Удалить строку";
    this.LCLabel1.Dock = DockStyle.Top;
    this.LCLabel1.ImeMode = ImeMode.NoControl;
    this.LCLabel1.Location = new Point(11, 22);
    this.LCLabel1.Name = "LCLabel1";
    this.LCLabel1.Padding = new Padding(0, 3, 0, 0);
    this.LCLabel1.Size = new Size(588, 27);
    this.LCLabel1.TabIndex = 3;
    this.LCLabel1.Text = "Перед выполнением действия:";
    this.processLCDirectionGroup.Controls.Add((Control) this.processLCDirectionComboBox);
    this.processLCDirectionGroup.Dock = DockStyle.Top;
    this.processLCDirectionGroup.Location = new Point(11, 423);
    this.processLCDirectionGroup.Name = "processLCDirectionGroup";
    this.processLCDirectionGroup.Size = new Size(588, 59);
    this.processLCDirectionGroup.TabIndex = 6;
    this.processLCDirectionGroup.TabStop = false;
    this.processLCDirectionGroup.Text = "Выполнять перевод";
    this.processLCDirectionComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.processLCDirectionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.processLCDirectionComboBox.FormattingEnabled = true;
    this.processLCDirectionComboBox.Items.AddRange(new object[3]
    {
      (object) "Всегда",
      (object) "Только вперед",
      (object) "Только назад"
    });
    this.processLCDirectionComboBox.Location = new Point(6, 21);
    this.processLCDirectionComboBox.MaxDropDownItems = 4;
    this.processLCDirectionComboBox.Name = "processLCDirectionComboBox";
    this.processLCDirectionComboBox.Size = new Size(576, 24);
    this.processLCDirectionComboBox.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.LCGroupBox);
    this.Name = nameof (StatusSettingPageControl);
    this.Size = new Size(610, 497);
    this.LCGroupBox.ResumeLayout(false);
    this.LCPanel2.ResumeLayout(false);
    this.LCPanel2.PerformLayout();
    this.LCPanel1.ResumeLayout(false);
    this.LCPanel1.PerformLayout();
    this.processLCDirectionGroup.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class LCDirectionItem
  {
    public ProcessLCDirection Value { get; private set; }

    public LCDirectionItem(ProcessLCDirection value) => this.Value = value;

    public override string ToString() => EnumDescConverter.GetEnumDescription((Enum) this.Value);
  }
}
