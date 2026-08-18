// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.PagerControl
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Setup;

internal class PagerControl : UserControl, IPagerControl
{
  private HostControlSizeManager hostControlSizeManager;
  private PageDescriptor currentPage;
  private IPageControl currentPageControl;
  private IContainer components;
  private ToolStrip tsToolbar;
  private ToolStripComboBox ddPages;
  private ToolStripLabel tslPage;
  private Panel pnDockArea;

  public PagerControl()
  {
    this.InitializeComponent();
    this.hostControlSizeManager = new HostControlSizeManager();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1643);
  }

  public void Initialize(ICollection<PageDescriptor> pages)
  {
    if (pages == null)
      throw new ArgumentNullException(nameof (pages));
    this.Close();
    this.ddPages.BeginUpdate();
    try
    {
      foreach (object page in (IEnumerable<PageDescriptor>) pages)
        this.ddPages.Items.Add(page);
      if (this.ddPages.Items.Count <= 0)
        return;
      this.ddPages.SelectedIndex = 0;
    }
    finally
    {
      this.ddPages.EndUpdate();
    }
  }

  public PageDescriptor ActivePage => this.currentPage;

  public bool CanClose => this.currentPage != null && this.currentPageControl.CanClose;

  ToolStrip IPagerControl.Toolbar => this.tsToolbar;

  public void Close()
  {
    if (this.currentPage == null)
      return;
    if (!this.CanClose)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Client_170"));
    this.CloseCurrentPage();
    this.ddPages.Items.Clear();
    this.currentPage = (PageDescriptor) null;
    this.currentPageControl = (IPageControl) null;
    this.RaisePageChanged();
  }

  private void OnPageChanged(object sender, EventArgs e)
  {
    PageDescriptor selectedItem = (PageDescriptor) this.ddPages.SelectedItem;
    if (selectedItem == this.currentPage)
      return;
    if (this.TryChangePage(selectedItem))
      this.RaisePageChanged();
    else
      this.ddPages.SelectedItem = (object) this.currentPage;
  }

  private bool TryChangePage(PageDescriptor newPage)
  {
    this.SuspendLayout();
    try
    {
      if (this.currentPage != null)
      {
        if (!this.CanClose)
          return false;
        this.CloseCurrentPage();
      }
      this.OpenCurrentPage(newPage);
      return true;
    }
    finally
    {
      this.ResumeLayout();
    }
  }

  private void OpenCurrentPage(PageDescriptor pageInfo)
  {
    Control instance = (Control) Activator.CreateInstance(pageInfo.ControlType);
    instance.Dock = DockStyle.Fill;
    instance.Parent = (Control) this.pnDockArea;
    instance.Focus();
    this.currentPage = pageInfo;
    this.currentPageControl = (IPageControl) instance;
    this.hostControlSizeManager.ContentControl = instance;
    this.currentPageControl.DynamicContentChanged += new EventHandler(this.OnPageDynamicContentChanged);
    this.currentPageControl.Initialize((IPagerControl) this);
  }

  private void CloseCurrentPage()
  {
    this.hostControlSizeManager.ContentControl = (Control) null;
    this.currentPageControl.DynamicContentChanged -= new EventHandler(this.OnPageDynamicContentChanged);
    this.currentPageControl.Close();
    this.currentPageControl.Dispose();
    this.currentPage = (PageDescriptor) null;
    this.currentPageControl = (IPageControl) null;
  }

  private void OnPageDynamicContentChanged(object sender, EventArgs e)
  {
    this.RaisePageDynamicContentChanged();
  }

  private void RaisePageChanged()
  {
    if (this.PageChanged == null)
      return;
    this.PageChanged((object) this, EventArgs.Empty);
  }

  private void RaisePageDynamicContentChanged()
  {
    if (this.PageDynamicContentChanged == null)
      return;
    this.PageDynamicContentChanged((object) this, EventArgs.Empty);
  }

  public event EventHandler PageChanged;

  public event EventHandler PageDynamicContentChanged;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PagerControl));
    this.tsToolbar = new ToolStrip();
    this.tslPage = new ToolStripLabel();
    this.ddPages = new ToolStripComboBox();
    this.pnDockArea = new Panel();
    ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
    this.tsToolbar.SuspendLayout();
    this.SuspendLayout();
    toolStripSeparator.Name = "tssMainSeparator";
    componentResourceManager.ApplyResources((object) toolStripSeparator, "tssMainSeparator");
    this.tsToolbar.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tslPage,
      (ToolStripItem) this.ddPages,
      (ToolStripItem) toolStripSeparator
    });
    componentResourceManager.ApplyResources((object) this.tsToolbar, "tsToolbar");
    this.tsToolbar.Name = "tsToolbar";
    this.tslPage.Name = "tslPage";
    componentResourceManager.ApplyResources((object) this.tslPage, "tslPage");
    this.ddPages.DropDownStyle = ComboBoxStyle.DropDownList;
    this.ddPages.Name = "ddPages";
    componentResourceManager.ApplyResources((object) this.ddPages, "ddPages");
    this.ddPages.SelectedIndexChanged += new EventHandler(this.OnPageChanged);
    componentResourceManager.ApplyResources((object) this.pnDockArea, "pnDockArea");
    this.pnDockArea.Name = "pnDockArea";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnDockArea);
    this.Controls.Add((Control) this.tsToolbar);
    this.Name = nameof (PagerControl);
    this.tsToolbar.ResumeLayout(false);
    this.tsToolbar.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
