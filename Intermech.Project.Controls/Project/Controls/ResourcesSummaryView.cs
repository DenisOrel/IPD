// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ResourcesSummaryView
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Common;
using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.Interfaces;
using Intermech.Windows.Forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class ResourcesSummaryView : 
  IpsBaseUserControl,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IDesignModeControlsContainer,
  IArrowKeysNavigationSupported,
  ILastFocusedControlTracker,
  IContextAware,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2,
  IClientProjectContext,
  IResourceAssignmentsProjectContext,
  IResourcesSummaryProjectContext
{
  private bool _inAlignUserButtons;
  private bool _manualSelect;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _scalePanel;
  private ListView _usersView;
  private ResourcesGanttChart _chartView;
  private SplitContainer _splitContainer;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Panel ScalePanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._scalePanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ListView UsersView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._usersView.CheckInitializedIn<ListView>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ResourcesGanttChart ChartView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._chartView.CheckInitializedIn<ResourcesGanttChart>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public SplitContainer SplitContainer
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._splitContainer.CheckInitializedIn<SplitContainer>((object) this);
    }
  }

  public ResourcesSummaryView()
  {
    this.InitializeComponent();
    if (this.Project != null)
    {
      this.AddService<ClientProject>((ClientProject) this.Project);
      this.AddService<Intermech.Project.Project>((Intermech.Project.Project) this.Project);
    }
    this.AddService<ResourcesSummaryView>(this);
    this.AddService<ResourcesGanttChart>(this.ChartView);
    this.AddService<GanttChart>((GanttChart) this.ChartView);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.RemoveService<ResourcesGanttChart>();
      this.RemoveService<GanttChart>();
      this.RemoveService<ResourcesSummaryView>();
      this.RemoveService<ClientProject>();
      this.RemoveService<Intermech.Project.Project>();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  internal void SetProject([CanBeNull] ResourcesSummaryProject project)
  {
    int num = this.ChartView.Project != project ? 1 : 0;
    if (num != 0 && this.ChartView.Project != null && this.ServiceContainer != null)
    {
      this.RemoveService<ClientProject>();
      this.RemoveService<Intermech.Project.Project>();
    }
    this.ChartView.Project = project;
    if (num != 0 && this.Project != null && this.ServiceContainer != null)
    {
      this.AddService<ClientProject>((ClientProject) this.Project);
      this.AddService<Intermech.Project.Project>((Intermech.Project.Project) this.Project);
    }
    this.ChartView.ScalePanel = this._scalePanel;
    this.UsersView.Clear();
    this.UsersView.LargeImageList = Intermech.Client.Services.IconService.BigImageList;
    if (project?.UserInfos != null)
    {
      foreach (ResourceAssignmentsProject.UserInfo userInfo in project.UserInfos)
        this.UsersView.Items.Add(new ListViewItem(userInfo.Name, Intermech.Extensions.Icons.UserImageIndex)
        {
          Tag = (object) userInfo.Task
        });
    }
    this.AlignUserButtons();
    if (this.UsersView.Items.Count > 0)
      this.UsersView.Items[0].Selected = true;
    this.ChartView.ResetCaches();
    this.ChartView.Invalidate();
  }

  [CanBeNull]
  [Browsable(false)]
  [DefaultValue(null)]
  public ResourcesSummaryProject Project
  {
    get => this.ChartView.Project;
    set
    {
      if (this.ChartView.Project == value)
        return;
      this.SetProject(value);
    }
  }

  private void AlignUserButtons()
  {
    this._inAlignUserButtons = true;
    try
    {
      int num = 0;
      for (int index = 0; index < this.UsersView.Items.Count; ++index)
      {
        ListViewItem listViewItem = this.UsersView.Items[index];
        try
        {
          if (listViewItem.Bounds.Width > num)
            num = listViewItem.Bounds.Width;
        }
        catch
        {
          return;
        }
      }
      for (int index = 0; index < this.UsersView.Items.Count; ++index)
      {
        ListViewItem listViewItem = this.UsersView.Items[index];
        int x = this.UsersView.Width / 2 - num / 2;
        int y = 20 * (index + 1) + listViewItem.Bounds.Height * index;
        listViewItem.Position = new Point(0, 0);
        listViewItem.Position = new Point(x, y);
      }
    }
    finally
    {
      this._inAlignUserButtons = false;
    }
  }

  private void usersView_SizeChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.Disposing || this.IsDisposed || this._inAlignUserButtons)
      return;
    this.AlignUserButtons();
  }

  private void ScalePanel_SizeChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._scalePanel == null)
      return;
    this.UsersView.Width = this._scalePanel.Width - 60;
  }

  protected override void OnMouseWheel([NotNull] MouseEventArgs e)
  {
    base.OnMouseWheel(e);
    this.ChartView.HandleParentMouseWheel((Control) this, e);
  }

  [CanBeNull]
  public UserSummaryTask SelectedUserTask
  {
    get
    {
      return this.UsersView.SelectedItems.Cast<ListViewItem>().Select<ListViewItem, UserSummaryTask>((Func<ListViewItem, UserSummaryTask>) (li => li.Tag as UserSummaryTask)).FirstOrDefault<UserSummaryTask>();
    }
    set
    {
      if (this.SelectedUserTask == value)
        return;
      foreach (ListViewItem listViewItem in this.UsersView.Items)
      {
        if (listViewItem.Tag != null)
        {
          if (object.Equals(listViewItem.Tag, (object) value))
          {
            try
            {
              this._manualSelect = true;
              listViewItem.Selected = true;
              listViewItem.EnsureVisible();
              break;
            }
            finally
            {
              this._manualSelect = false;
            }
          }
        }
      }
    }
  }

  public event EventHandler SelectedUserTaskChanged;

  private void UsersView_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    ListViewItem listViewItem = (ListViewItem) null;
    if (this.UsersView.SelectedItems.Count > 0)
      listViewItem = this.UsersView.SelectedItems[0];
    this.ChartView.CurrentUserTask = listViewItem?.Tag as UserSummaryTask;
    if (this._manualSelect || this.SelectedUserTaskChanged == null)
      return;
    this.SelectedUserTaskChanged((object) this, (EventArgs) null);
  }

  /// <summary>Gets the project</summary>
  ClientProject IClientProjectContext.Project => (ClientProject) this.Project;

  [NotNull]
  ResourceAssignmentsProject IResourceAssignmentsProjectContext.Project
  {
    get => (ResourceAssignmentsProject) this.Project;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._scalePanel = new Panel();
    this._splitContainer = new SplitContainer();
    this._usersView = new ListView();
    this._chartView = new ResourcesGanttChart();
    this._scalePanel.SuspendLayout();
    this._splitContainer.Panel1.SuspendLayout();
    this._splitContainer.Panel2.SuspendLayout();
    this._splitContainer.SuspendLayout();
    this.SuspendLayout();
    this._scalePanel.BackColor = SystemColors.Window;
    this._scalePanel.BorderStyle = BorderStyle.Fixed3D;
    this._scalePanel.Controls.Add((Control) this._usersView);
    this._scalePanel.Dock = DockStyle.Fill;
    this._scalePanel.Location = new Point(0, 0);
    this._scalePanel.Name = "_scalePanel";
    this._scalePanel.Size = new Size(203, 537);
    this._scalePanel.TabIndex = 3;
    this._scalePanel.SizeChanged += new EventHandler(this.ScalePanel_SizeChanged);
    this._splitContainer.Dock = DockStyle.Fill;
    this._splitContainer.Location = new Point(0, 0);
    this._splitContainer.Name = "_splitContainer";
    this._splitContainer.Panel1.Controls.Add((Control) this._scalePanel);
    this._splitContainer.Panel2.Controls.Add((Control) this._chartView);
    this._splitContainer.Size = new Size(853, 537);
    this._splitContainer.SplitterDistance = 203;
    this._splitContainer.TabIndex = 4;
    this._usersView.Alignment = ListViewAlignment.Left;
    this._usersView.AutoArrange = false;
    this._usersView.BackColor = SystemColors.Window;
    this._usersView.BorderStyle = BorderStyle.None;
    this._usersView.Cursor = Cursors.Default;
    this._usersView.Dock = DockStyle.Left;
    this._usersView.HideSelection = false;
    this._usersView.LabelWrap = false;
    this._usersView.Location = new Point(0, 0);
    this._usersView.MultiSelect = false;
    this._usersView.Name = "_usersView";
    this._usersView.ShowGroups = false;
    this._usersView.Size = new Size(111, 533);
    this._usersView.TabIndex = 1;
    this._usersView.UseCompatibleStateImageBehavior = false;
    this._usersView.SelectedIndexChanged += new EventHandler(this.UsersView_SelectedIndexChanged);
    this._usersView.SizeChanged += new EventHandler(this.usersView_SizeChanged);
    this._chartView.BarWidth = -1f;
    this._chartView.CurrentUserTask = (UserSummaryTask) null;
    this._chartView.DayWidth = 60f;
    this._chartView.Dock = DockStyle.Fill;
    this._chartView.HighlightCriticalTasks = false;
    this._chartView.Location = new Point(0, 0);
    this._chartView.Name = "_chartView";
    this._chartView.NumericScaleType = NumericScaleType.Units;
    this._chartView.RectangleHeightPercent = 0.5f;
    this._chartView.RectangleRoundnessPercent = 0.0f;
    this._chartView.RowHeight = 22;
    this._chartView.ScalePanel = (Panel) null;
    this._chartView.ScaleType = ScaleType.Weeks;
    this._chartView.Size = new Size(646, 537);
    this._chartView.TabIndex = 2;
    this._chartView.UseNumericScaleValues = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._splitContainer);
    this.Name = nameof (ResourcesSummaryView);
    this.Size = new Size(853, 537);
    this._scalePanel.ResumeLayout(false);
    this._splitContainer.Panel1.ResumeLayout(false);
    this._splitContainer.Panel2.ResumeLayout(false);
    this._splitContainer.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
