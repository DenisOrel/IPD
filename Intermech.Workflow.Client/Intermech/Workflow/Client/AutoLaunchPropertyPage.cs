// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoLaunchPropertyPage
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class AutoLaunchPropertyPage : UserControl, IPropertyPage, IPropertyPageSearchOptionEvents
{
  private bool _isAdmin;
  private bool _modified;
  private IContainer components;
  private ImageList cmdsIL;
  private Panel panel5;
  private Label label9;
  private EnhListView View;
  private ColumnHeader TypeColumn;
  private ColumnHeader SchemeColumn;
  private ToolBar ToolBar;
  private AutoSizeLabel CapLabel;
  private ToolBarButton AddButton;
  private ToolBarButton EditButton;
  private ToolBarButton DeleteButton;
  private ToolBarButton delButton;
  private ToolBarButton toolBarButton2;
  private ColumnHeader ProcessPriorityColumn;

  public AutoLaunchPropertyPage() => this.InitializeComponent();

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Control;

  public object Control => (object) this;

  public string PageName => LocalizationHolder.GetString("SettingsPropertyPageAutoLaunch");

  public void Apply()
  {
    if (!this.Modified)
      return;
    AutoLaunchSettings.All.Clear();
    foreach (ListViewItem listViewItem in this.View.Items)
      AutoLaunchSettings.All.Add((AutoLaunchInfo) listViewItem.Tag);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      AutoLaunchSettings.All.Save(sessionKeeper.Session);
      if (sessionKeeper.Session.GetCustomService(typeof (IRouterService)) is IRouterService customService)
        customService.ReloadSettings(SettingsGroup.AutoLaunch);
    }
    this.Modified = false;
  }

  public void Cancel() => this.Modified = false;

  public string HelpTopicID => "";

  public string HeaderText => this.PageName;

  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  private void FillView()
  {
    this.View.SmallImageList = BaseHolder.IconService.ImageList;
    this.View.SubitemImages = BaseHolder.IconService.ImageList;
    ICurrentUserAndRole service = ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._isAdmin = service != null ? service.IsAdmin : sessionKeeper.Session.IsAdmin;
      if (!AutoLaunchSettings.All.Loaded)
        AutoLaunchSettings.All.Load(sessionKeeper.Session, true);
      if (!this._isAdmin)
        this.View.ForeColor = SystemColors.GrayText;
    }
    this.View.Items.Clear();
    foreach (AutoLaunchInfo info in (List<AutoLaunchInfo>) AutoLaunchSettings.All)
      this.AddItem(info);
    if (this.View.Items.Count > 0)
      this.View.Items[0].Selected = true;
    this.UpdateEnabled();
  }

  private ListViewItem SelectedItem
  {
    get => this.View.SelectedItems.Count > 0 ? this.View.SelectedItems[0] : (ListViewItem) null;
  }

  private ListViewItem FindItem(AutoLaunchInfo info)
  {
    foreach (ListViewItem listViewItem in this.View.Items)
    {
      if (info.Equals(listViewItem.Tag))
        return listViewItem;
    }
    return (ListViewItem) null;
  }

  private ListViewItem AddItem(AutoLaunchInfo info)
  {
    ListViewItem li = new ListViewItem();
    this.FillItem(li, info);
    this.View.Items.Add(li);
    li.Selected = true;
    return li;
  }

  private void FillItem(ListViewItem li, AutoLaunchInfo info)
  {
    if (li.SubItems.Count > 0)
      li.SubItems.Clear();
    li.Text = info.TypeName;
    ImageListViewSubItem imageListViewSubItem = new ImageListViewSubItem(info.SchemeName, Holder.SchemeImageIndex);
    li.SubItems.Add((ListViewItem.ListViewSubItem) imageListViewSubItem);
    ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem()
    {
      Text = info.ProcessPriorityName
    };
    li.SubItems.Add(listViewSubItem);
    li.Tag = (object) info;
    li.ImageIndex = BaseHolder.IconService.IndexOf(4, info.TypeID);
  }

  public bool Modified
  {
    get => this._modified;
    set
    {
      if (this._modified == value)
        return;
      this._modified = value;
      EventHandler changed = this.Changed;
      if (!this._modified || changed == null)
        return;
      changed((object) this, (EventArgs) null);
    }
  }

  private void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
  {
    if (e == null || sender == null || !e.Button.Enabled)
      return;
    if (Convert.ToInt32(e.Button.Tag) == 2)
    {
      this.View.SaveSelectedPos();
      this.View.Items.Remove(this.SelectedItem);
      this.View.RestoreSelectedPos();
      this.Modified = true;
    }
    else
    {
      if (Convert.ToInt32(e.Button.Tag) == sc_21612.ssp_workflow_21613(1513969865))
      {
        if (this.View.SelectedItems.Count <= 0)
          return;
        sender = (object) (this.View.SelectedItems[0].Tag as AutoLaunchInfo);
      }
      bool flag = !(sender is AutoLaunchInfo);
      using (AutoLaunchSetupForm autoLaunchSetupForm = new AutoLaunchSetupForm())
      {
        AutoLaunchInfo autoLaunchInfo = (AutoLaunchInfo) null;
        if (!flag)
        {
          autoLaunchInfo = (AutoLaunchInfo) sender;
          autoLaunchSetupForm.LaunchInfo = autoLaunchInfo;
        }
        if (autoLaunchSetupForm.ShowDialog() != DialogResult.OK)
          return;
        if (flag)
        {
          if (this.FindItem(autoLaunchSetupForm.LaunchInfo) != null)
            return;
          this.AddItem(autoLaunchSetupForm.LaunchInfo).Tag = (object) autoLaunchSetupForm.LaunchInfo;
          this.Modified = true;
        }
        else
        {
          if (autoLaunchInfo == null || autoLaunchSetupForm.LaunchInfo.Equals((object) autoLaunchInfo))
            return;
          ListViewItem selectedItem = this.SelectedItem;
          if (selectedItem != null)
            this.FillItem(selectedItem, autoLaunchSetupForm.LaunchInfo);
          this.Modified = true;
        }
      }
    }
  }

  private void View_DoubleClick(object sender, EventArgs e)
  {
    this.ToolBar_ButtonClick((object) this.EditButton, new ToolBarButtonClickEventArgs(this.EditButton));
  }

  private void UpdateEnabled()
  {
    this.AddButton.Enabled = this._isAdmin;
    this.EditButton.Enabled = this._isAdmin && this.SelectedItem != null;
    this.DeleteButton.Enabled = this._isAdmin && this.SelectedItem != null;
  }

  private void View_SelectedIndexChanged(object sender, EventArgs e) => this.UpdateEnabled();

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.FillView();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoLaunchPropertyPage));
    this.cmdsIL = new ImageList(this.components);
    this.panel5 = new Panel();
    this.ToolBar = new ToolBar();
    this.AddButton = new ToolBarButton();
    this.EditButton = new ToolBarButton();
    this.DeleteButton = new ToolBarButton();
    this.label9 = new Label();
    this.toolBarButton2 = new ToolBarButton();
    this.View = new EnhListView();
    this.TypeColumn = new ColumnHeader();
    this.SchemeColumn = new ColumnHeader();
    this.CapLabel = new AutoSizeLabel();
    this.ProcessPriorityColumn = new ColumnHeader();
    this.panel5.SuspendLayout();
    this.SuspendLayout();
    this.cmdsIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("cmdsIL.ImageStream");
    this.cmdsIL.TransparentColor = Color.Fuchsia;
    this.cmdsIL.Images.SetKeyName(0, "add.ico");
    this.cmdsIL.Images.SetKeyName(1, "del.ico");
    this.cmdsIL.Images.SetKeyName(2, "answer.ico");
    this.panel5.BorderStyle = BorderStyle.Fixed3D;
    this.panel5.Controls.Add((System.Windows.Forms.Control) this.View);
    this.panel5.Controls.Add((System.Windows.Forms.Control) this.ToolBar);
    this.panel5.Controls.Add((System.Windows.Forms.Control) this.label9);
    this.panel5.Dock = DockStyle.Fill;
    this.panel5.Location = new Point(13, 27);
    this.panel5.Margin = new Padding(4, 4, 4, 4);
    this.panel5.Name = "panel5";
    this.panel5.Size = new Size(762, 414);
    this.panel5.TabIndex = 2;
    this.ToolBar.Buttons.AddRange(new ToolBarButton[3]
    {
      this.AddButton,
      this.EditButton,
      this.DeleteButton
    });
    this.ToolBar.ButtonSize = new Size(22, 22);
    this.ToolBar.Divider = false;
    this.ToolBar.Dock = DockStyle.Right;
    this.ToolBar.DropDownArrows = true;
    this.ToolBar.ImageList = this.cmdsIL;
    this.ToolBar.ImeMode = ImeMode.NoControl;
    this.ToolBar.Location = new Point(733, 0);
    this.ToolBar.Margin = new Padding(4, 4, 4, 4);
    this.ToolBar.Name = "ToolBar";
    this.ToolBar.ShowToolTips = true;
    this.ToolBar.Size = new Size(22, 410);
    this.ToolBar.TabIndex = 6;
    this.ToolBar.TextAlign = ToolBarTextAlign.Right;
    this.ToolBar.ButtonClick += new ToolBarButtonClickEventHandler(this.ToolBar_ButtonClick);
    this.AddButton.ImageIndex = 0;
    this.AddButton.Name = "AddButton";
    this.AddButton.Tag = (object) "1";
    this.AddButton.ToolTipText = "Добавить строку";
    this.EditButton.ImageIndex = 2;
    this.EditButton.Name = "EditButton";
    this.EditButton.Tag = (object) "3";
    this.EditButton.ToolTipText = "Редактировать строку";
    this.DeleteButton.ImageIndex = 1;
    this.DeleteButton.Name = "DeleteButton";
    this.DeleteButton.Tag = (object) "2";
    this.DeleteButton.ToolTipText = "Удалить строку";
    this.label9.BorderStyle = BorderStyle.Fixed3D;
    this.label9.Dock = DockStyle.Right;
    this.label9.ImeMode = ImeMode.NoControl;
    this.label9.Location = new Point(755, 0);
    this.label9.Margin = new Padding(4, 0, 4, 0);
    this.label9.Name = "label9";
    this.label9.Size = new Size(3, 410);
    this.label9.TabIndex = 7;
    this.label9.Text = "label9";
    this.toolBarButton2.ImageIndex = 1;
    this.toolBarButton2.Name = "toolBarButton2";
    this.toolBarButton2.Tag = (object) "2";
    this.toolBarButton2.ToolTipText = "Удалить строку";
    this.View.AllowManualSorting = true;
    this.View.BorderStyle = BorderStyle.None;
    this.View.Columns.AddRange(new ColumnHeader[3]
    {
      this.TypeColumn,
      this.SchemeColumn,
      this.ProcessPriorityColumn
    });
    this.View.Dock = DockStyle.Fill;
    this.View.FullRowSelect = true;
    this.View.HideSelection = false;
    this.View.Location = new Point(0, 0);
    this.View.Margin = new Padding(4);
    this.View.MultiSelect = false;
    this.View.Name = "View";
    this.View.OwnerDraw = true;
    this.View.RadioGroups = false;
    this.View.Size = new Size(733, 410);
    this.View.SortColumn = 0;
    this.View.SubitemImages = (ImageList) null;
    this.View.TabIndex = 5;
    this.View.UseCompatibleStateImageBehavior = false;
    this.View.View = System.Windows.Forms.View.Details;
    this.View.SelectedIndexChanged += new EventHandler(this.View_SelectedIndexChanged);
    this.View.DoubleClick += new EventHandler(this.View_DoubleClick);
    this.TypeColumn.Text = "Тип объектов";
    this.TypeColumn.Width = 250;
    this.SchemeColumn.Text = "Процесс по шаблону";
    this.SchemeColumn.Width = 250;
    this.CapLabel.Dock = DockStyle.Top;
    this.CapLabel.Location = new Point(13, 12);
    this.CapLabel.Margin = new Padding(4, 0, 4, 0);
    this.CapLabel.Name = "CapLabel";
    this.CapLabel.Padding = new Padding(0, 0, 0, 12);
    this.CapLabel.Size = new Size(762, 15);
    this.CapLabel.TabIndex = 5;
    this.CapLabel.Text = "При создании объектов/версий указанных типов запускать процессы на основе выбранных шаблонов:";
    this.ProcessPriorityColumn.Text = "Приоритет процесса";
    this.ProcessPriorityColumn.Width = 200;
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.panel5);
    this.Controls.Add((System.Windows.Forms.Control) this.CapLabel);
    this.Margin = new Padding(4, 4, 4, 4);
    this.Name = nameof (AutoLaunchPropertyPage);
    this.Padding = new Padding(13, 12, 13, 12);
    this.Size = new Size(788, 453);
    this.panel5.ResumeLayout(false);
    this.panel5.PerformLayout();
    this.ResumeLayout(false);
  }
}
