// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.RemoteSettingsPropertyPage
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using Intermech.Project.Controls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

public class RemoteSettingsPropertyPage : UserControl, IPropertyPage
{
  private IContainer components;
  private GroupBox _remoteSchemesBox;
  private EnhDataGridView _remoteSchemesView;
  private DataGridViewTextBoxColumn _remoteSiteColumn;
  private DataGridViewComboBoxColumn _remoteSchemeColumn;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal GroupBox RemoteSchemesBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._remoteSchemesBox.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal EnhDataGridView RemoteSchemesView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._remoteSchemesView.CheckInitializedIn<EnhDataGridView>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal DataGridViewTextBoxColumn RemoteSiteColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._remoteSiteColumn.CheckInitializedIn<DataGridViewTextBoxColumn>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal DataGridViewComboBoxColumn RemoteSchemeColumn
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._remoteSchemeColumn.CheckInitializedIn<DataGridViewComboBoxColumn>((object) this);
    }
  }

  public RemoteSettingsPropertyPage() => this.InitializeComponent();

  private void RemoteSchemesView_DataError([CanBeNull] object sender, [NotNull] DataGridViewDataErrorEventArgs e)
  {
    e.Cancel = false;
  }

  private void RemoteSchemesView_EditingControlShowing(
    [CanBeNull] object sender,
    [NotNull] DataGridViewEditingControlShowingEventArgs e)
  {
    if (!(e.Control is ComboBox control))
      return;
    control.SelectedIndexChanged -= new EventHandler(this.RemoteSchemesView_ComboSelectedIndexChanged);
    control.SelectedIndexChanged += new EventHandler(this.RemoteSchemesView_ComboSelectedIndexChanged);
    control.DropDown += new EventHandler(RemoteSettingsPropertyPage.RemoteSchemesView_ComboDropDown);
    control.GotFocus += new EventHandler(RemoteSettingsPropertyPage.RemoteSchemesView_ComboDropDown);
  }

  private void RemoteSchemesView_ComboSelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.RemoteSchemesView.CurrentCell.Value = ((ComboBox) sender).SelectedItem;
  }

  private static void RemoteSchemesView_ComboDropDown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    ((System.Windows.Forms.Control) sender).BackColor = Color.White;
  }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Control;

  [NotNull]
  public object Control => (object) this;

  [NotNull]
  public string PageName => "Распределенный ImProject";

  public void Apply()
  {
    foreach (DataGridViewRow row in (IEnumerable) this.RemoteSchemesView.Rows)
    {
      if (row.Cells[0].Value is SiteInfo siteInfo && row.Cells[1].Value is ProcessTemplateInfo processTemplateInfo && RemoteSettings.SiteSchemes != null)
        RemoteSettings.SiteSchemes[siteInfo.Code] = processTemplateInfo.Guid;
    }
    RemoteSettings.SaveSettings();
  }

  public void Cancel()
  {
  }

  [NotNull]
  public string HelpTopicID => string.Empty;

  [CanBeNull]
  public string HeaderText => this.PageName;

  protected override void OnLoad([NotNull] EventArgs e)
  {
    base.OnLoad(e);
    this.RemoteSchemeColumn.ValueType = typeof (ProcessTemplateInfo);
    this.RemoteSchemesView.Rows.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IPortalConnector customService1 = sessionKeeper.Session.GetCustomService<IPortalConnector>(false);
      ISitesCacheService customService2 = sessionKeeper.Session.GetCustomService<ISitesCacheService>(false);
      if (customService2 == null)
        return;
      foreach (SiteInfo site in customService2.Sites)
      {
        if (site != null && site.ID != customService2.Info.ID)
        {
          ProcessTemplateInfo[] processTemplateInfoArray = (ProcessTemplateInfo[]) null;
          try
          {
            processTemplateInfoArray = customService1.GetProcessTemplates(site.GUID);
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show($"Ошибка получения списка опубликованных шаблонов процессов узла портала \"{site}\":\r\n\r\n{ex.Message}");
          }
          int index = this.RemoteSchemesView.Rows.Add((object) site, (object) string.Empty);
          if (processTemplateInfoArray != null)
          {
            DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell) this.RemoteSchemesView.Rows[index].Cells[1];
            cell.Items.AddRange((object[]) processTemplateInfoArray);
            Guid guid;
            if (RemoteSettings.SiteSchemes != null && RemoteSettings.SiteSchemes.TryGetValue(site.Code, out guid))
            {
              foreach (ProcessTemplateInfo processTemplateInfo in processTemplateInfoArray)
              {
                if (processTemplateInfo != null && processTemplateInfo.Guid == guid)
                {
                  cell.Value = (object) processTemplateInfo;
                  break;
                }
              }
            }
          }
        }
      }
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this._remoteSchemesBox = new GroupBox();
    this._remoteSchemesView = new EnhDataGridView();
    this._remoteSiteColumn = new DataGridViewTextBoxColumn();
    this._remoteSchemeColumn = new DataGridViewComboBoxColumn();
    this._remoteSchemesBox.SuspendLayout();
    ((ISupportInitialize) this._remoteSchemesView).BeginInit();
    this.SuspendLayout();
    this._remoteSchemesBox.Controls.Add((System.Windows.Forms.Control) this._remoteSchemesView);
    this._remoteSchemesBox.Dock = DockStyle.Top;
    this._remoteSchemesBox.Location = new Point(10, 10);
    this._remoteSchemesBox.Name = "_remoteSchemesBox";
    this._remoteSchemesBox.Padding = new Padding(10);
    this._remoteSchemesBox.Size = new Size(605, 238);
    this._remoteSchemesBox.TabIndex = 47;
    this._remoteSchemesBox.TabStop = false;
    this._remoteSchemesBox.Text = "Шаблоны публикации проектов ImProject";
    this._remoteSchemesView.AllowUserToAddRows = false;
    this._remoteSchemesView.AllowUserToDeleteRows = false;
    this._remoteSchemesView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
    this._remoteSchemesView.BackgroundColor = SystemColors.Window;
    this._remoteSchemesView.BorderStyle = BorderStyle.Fixed3D;
    this._remoteSchemesView.CellBorderStyle = DataGridViewCellBorderStyle.None;
    this._remoteSchemesView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this._remoteSchemesView.Columns.AddRange((DataGridViewColumn) this._remoteSiteColumn, (DataGridViewColumn) this._remoteSchemeColumn);
    this._remoteSchemesView.Dock = DockStyle.Top;
    this._remoteSchemesView.EnableHeadersVisualStyles = false;
    this._remoteSchemesView.Location = new Point(10, 23);
    this._remoteSchemesView.Name = "_remoteSchemesView";
    this._remoteSchemesView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
    this._remoteSchemesView.RowHeadersVisible = false;
    this._remoteSchemesView.RowHeadersWidth = 20;
    this._remoteSchemesView.ShowEditingIcon = false;
    this._remoteSchemesView.Size = new Size(585, 192 /*0xC0*/);
    this._remoteSchemesView.TabIndex = 13;
    this._remoteSchemesView.DataError += new DataGridViewDataErrorEventHandler(this.RemoteSchemesView_DataError);
    this._remoteSchemesView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.RemoteSchemesView_EditingControlShowing);
    this._remoteSiteColumn.HeaderText = "Узел";
    this._remoteSiteColumn.Name = "_remoteSiteColumn";
    this._remoteSiteColumn.ReadOnly = true;
    this._remoteSiteColumn.Width = 200;
    this._remoteSchemeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this._remoteSchemeColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
    this._remoteSchemeColumn.DisplayStyleForCurrentCellOnly = true;
    this._remoteSchemeColumn.HeaderText = "Шаблон";
    this._remoteSchemeColumn.Name = "_remoteSchemeColumn";
    this._remoteSchemeColumn.Resizable = DataGridViewTriState.True;
    this._remoteSchemeColumn.SortMode = DataGridViewColumnSortMode.Automatic;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this._remoteSchemesBox);
    this.Name = nameof (RemoteSettingsPropertyPage);
    this.Padding = new Padding(10);
    this.Size = new Size(625, 354);
    this._remoteSchemesBox.ResumeLayout(false);
    ((ISupportInitialize) this._remoteSchemesView).EndInit();
    this.ResumeLayout(false);
  }
}
