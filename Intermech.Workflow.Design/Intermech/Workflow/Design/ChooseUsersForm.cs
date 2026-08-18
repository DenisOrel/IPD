// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ChooseUsersForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class ChooseUsersForm : FormEx
{
  private long _processID;
  private IUserSession _session;
  private VarList _vars;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button OkButton;
  private Button CancButton;
  private Panel Panel2;
  private Label label1;
  private ComboBoxEx FilterBox;
  private Panel panel1;
  private EnhListView VarListView;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader3;
  private UsersTreeView RanksView;

  public ChooseUsersForm() => this.InitializeComponent();

  public ChooseUsersForm(long processID, IUserSession session)
    : this()
  {
    this._processID = processID;
    this._session = session;
  }

  private void ChooseUsersForm_Load(object sender, EventArgs e)
  {
    UsersGroupsDescriptor groupsDescriptor = new UsersGroupsDescriptor();
    using (ServiceContainer serviceContainer = new ServiceContainer())
    {
      serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService());
      serviceContainer.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
      this.FilterBox.ImageList = Holder.UsersImageList;
      if (this.FilterBox.Items.Count >= 3)
      {
        this.FilterBox.Items[0] = (object) new ComboBoxExItem(this.FilterBox.Items[0].ToString(), 1);
        this.FilterBox.Items[1] = (object) new ComboBoxExItem(this.FilterBox.Items[1].ToString(), 2);
        this.FilterBox.Items[2] = (object) new ComboBoxExItem(this.FilterBox.Items[2].ToString(), 3);
      }
      this.FilterBox.SelectedIndex = sc_21782.ssp_workflow_21783(2003939610);
    }
  }

  private UsersViewMode ViewMode => (UsersViewMode) this.FilterBox.SelectedIndex;

  public ParticipantList Participants
  {
    get
    {
      ParticipantList participants = (ParticipantList) null;
      switch (this.ViewMode)
      {
        case UsersViewMode.Groups:
        case UsersViewMode.Ranks:
          participants = this.RanksView.SelectedParticipants;
          break;
        case UsersViewMode.Variables:
          participants = new ParticipantList();
          for (int index = 0; index < this.VarListView.SelectedItems.Count; ++index)
            participants.AddParticipant(ParticipantKind.Variable, (long) Convert.ToInt32(this.VarListView.SelectedItems[index].Tag));
          break;
      }
      return participants;
    }
  }

  private void FillVariables()
  {
    if (this._vars != null)
      return;
    IDBObject src = this._session.GetObject(this._processID);
    this._vars = new VarList(src, false, false);
    this._vars.AddSystemVariables(src);
    this.VarListView.SmallImageList = Holder.UsersImageList;
    for (int index = this._vars.Count - 1; index >= 0; --index)
    {
      if (this._vars[index].VarType == VarType.ParticipantList)
      {
        ListViewItem listViewItem = this.VarListView.Items.Add(this._vars[index].Name);
        listViewItem.ImageIndex = 2;
        listViewItem.Tag = (object) this._vars[index].AttrTypeID;
        listViewItem.SubItems.Add(this._vars[index].UserValue);
      }
    }
    if (this.VarListView.Items.Count <= 0)
      return;
    this.VarListView.Items[0].Selected = true;
  }

  private void FilterBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    UsersViewMode viewMode = this.ViewMode;
    this.SuspendLayout();
    try
    {
      if (viewMode == UsersViewMode.Variables)
      {
        this.FillVariables();
        this.VarListView.Dock = DockStyle.Fill;
      }
      else
      {
        this.RanksView.Init(viewMode == UsersViewMode.Groups, viewMode == UsersViewMode.Ranks);
        this.RanksView.Dock = DockStyle.Fill;
      }
      this.VarListView.Visible = viewMode == UsersViewMode.Variables;
      this.RanksView.Visible = viewMode != UsersViewMode.Variables;
    }
    finally
    {
      this.ResumeLayout();
    }
  }

  private void VarListView_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.DialogResult = DialogResult.OK;
  }

  private void usersView_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.DialogResult = DialogResult.OK;
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
    this.OkButton = new Button();
    this.CancButton = new Button();
    this.Panel2 = new Panel();
    this.label1 = new Label();
    this.panel1 = new Panel();
    this.FilterBox = new ComboBoxEx();
    this.RanksView = new UsersTreeView();
    this.VarListView = new EnhListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.Panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.OkButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Location = new Point(280, 13);
    this.OkButton.Margin = new Padding(2);
    this.OkButton.Name = "OkButton";
    this.OkButton.Size = new Size(73, 23);
    this.OkButton.TabIndex = 3;
    this.OkButton.Text = "OK";
    this.CancButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Location = new Point(357, 13);
    this.CancButton.Margin = new Padding(2);
    this.CancButton.Name = "CancButton";
    this.CancButton.Size = new Size(73, 23);
    this.CancButton.TabIndex = 4;
    this.CancButton.Text = "Отмена";
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    this.Panel2.Dock = DockStyle.Bottom;
    this.Panel2.Location = new Point(10, 320);
    this.Panel2.Margin = new Padding(2);
    this.Panel2.Name = "Panel2";
    this.Panel2.Size = new Size(430, 46);
    this.Panel2.TabIndex = 4;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(2, 13);
    this.label1.Margin = new Padding(2, 0, 2, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(50, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Фильтр:";
    this.panel1.Controls.Add((Control) this.FilterBox);
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Dock = DockStyle.Top;
    this.panel1.Location = new Point(10, 10);
    this.panel1.Margin = new Padding(2);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(430, 40);
    this.panel1.TabIndex = 5;
    this.FilterBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.FilterBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.FilterBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.FilterBox.FormattingEnabled = true;
    this.FilterBox.ImageList = (ImageList) null;
    this.FilterBox.ItemHeight = 18;
    this.FilterBox.Items.AddRange(new object[3]
    {
      (object) "Группы и пользователи",
      (object) "Переменные",
      (object) "Должности"
    });
    this.FilterBox.Location = new Point(63 /*0x3F*/, 8);
    this.FilterBox.Margin = new Padding(2);
    this.FilterBox.Name = "FilterBox";
    this.FilterBox.Size = new Size(367, 24);
    this.FilterBox.TabIndex = 1;
    this.FilterBox.SelectedIndexChanged += new EventHandler(this.FilterBox_SelectedIndexChanged);
    this.RanksView.DstVariable = new Guid("00000000-0000-0000-0000-000000000000");
    this.RanksView.FullRowSelect = true;
    this.RanksView.GroupsOnly = false;
    this.RanksView.HideSelection = false;
    this.RanksView.ImageIndex = 0;
    this.RanksView.ItemHeight = 18;
    this.RanksView.Location = new Point(10, 61);
    this.RanksView.Modified = false;
    this.RanksView.Multiselect = true;
    this.RanksView.Name = "RanksView";
    this.RanksView.RequiresValue = true;
    this.RanksView.SelectedImageIndex = 0;
    this.RanksView.Size = new Size(155, 155);
    this.RanksView.Sorted = true;
    this.RanksView.SrcVariable = new Guid("00000000-0000-0000-0000-000000000000");
    this.RanksView.TabIndex = 9;
    this.VarListView.AllowManualSorting = true;
    this.VarListView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader3
    });
    this.VarListView.FullRowSelect = true;
    this.VarListView.HideSelection = false;
    this.VarListView.Location = new Point(170, 61);
    this.VarListView.Margin = new Padding(2);
    this.VarListView.Name = "VarListView";
    this.VarListView.OwnerDraw = true;
    this.VarListView.RadioGroups = false;
    this.VarListView.Size = new Size(210, 151);
    this.VarListView.SortColumn = 0;
    this.VarListView.SubitemImages = (ImageList) null;
    this.VarListView.TabIndex = 8;
    this.VarListView.UseCompatibleStateImageBehavior = false;
    this.VarListView.View = View.Details;
    this.VarListView.Visible = false;
    this.VarListView.MouseDoubleClick += new MouseEventHandler(this.VarListView_MouseDoubleClick);
    this.columnHeader1.Text = "Название";
    this.columnHeader1.Width = 180;
    this.columnHeader3.Text = "Значение";
    this.columnHeader3.Width = 170;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(450, 366);
    this.Controls.Add((Control) this.RanksView);
    this.Controls.Add((Control) this.VarListView);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.Panel2);
    this.KeyPreview = true;
    this.Margin = new Padding(2);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ChooseUsersForm);
    this.Padding = new Padding(10, 10, 10, 0);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор пользователей";
    this.Load += new EventHandler(this.ChooseUsersForm_Load);
    this.Panel2.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
