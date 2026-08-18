// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.ToolSetupWindow
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class ToolSetupWindow : Form
{
  private IContainer components;
  private PagerControl pcPager;
  private Button btClose;
  private Panel pnPagerHost;

  public ToolSetupWindow()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1175);
    if (this.DesignMode)
      return;
    int num = this.IsAdmin() ? 1 : 0;
    List<PageDescriptor> pages = new List<PageDescriptor>();
    pages.Add(new PageDescriptor(LocalizationHolder.rm.GetString("Tools.Client_179"), typeof (LaunchActionsPage)));
    pages.Add(new PageDescriptor(LocalizationHolder.rm.GetString("Tools.Client_180"), typeof (IntegratorsPage)));
    if (num != 0)
      pages.Add(new PageDescriptor("Просмотр и печать файлов", typeof (StandaloneViewPage)));
    pages.Add(new PageDescriptor(LocalizationHolder.rm.GetString("Tools.Client_181"), typeof (ToolSecurityPage)));
    this.pcPager.Initialize((ICollection<PageDescriptor>) pages);
  }

  private bool IsAdmin()
  {
    return ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true).IsAdmin;
  }

  private void Form1_FormClosing(object sender, FormClosingEventArgs e)
  {
    e.Cancel = !this.pcPager.CanClose;
  }

  private void SetupForm_FormClosed(object sender, FormClosedEventArgs e) => this.pcPager.Close();

  private void btClose_Click(object sender, EventArgs e) => this.Close();

  private void OnPageChanged(object sender, EventArgs e)
  {
  }

  private void OnPageDynamicContentChanged(object sender, EventArgs e)
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ToolSetupWindow));
    this.btClose = new Button();
    this.pnPagerHost = new Panel();
    this.pcPager = new PagerControl();
    this.pnPagerHost.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btClose, "btClose");
    this.btClose.DialogResult = DialogResult.Cancel;
    this.btClose.Name = "btClose";
    this.btClose.UseVisualStyleBackColor = true;
    this.btClose.Click += new EventHandler(this.btClose_Click);
    componentResourceManager.ApplyResources((object) this.pnPagerHost, "pnPagerHost");
    this.pnPagerHost.Controls.Add((Control) this.pcPager);
    this.pnPagerHost.Name = "pnPagerHost";
    componentResourceManager.ApplyResources((object) this.pcPager, "pcPager");
    this.pcPager.Name = "pcPager";
    this.pcPager.PageChanged += new EventHandler(this.OnPageChanged);
    this.pcPager.PageDynamicContentChanged += new EventHandler(this.OnPageDynamicContentChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btClose;
    this.Controls.Add((Control) this.pnPagerHost);
    this.Controls.Add((Control) this.btClose);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ToolSetupWindow);
    this.SizeGripStyle = SizeGripStyle.Show;
    this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.SetupForm_FormClosed);
    this.pnPagerHost.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
