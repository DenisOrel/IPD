
// Type: Intermech.PropertyEditors.ConfigurationForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class ConfigurationForm : TabPageForm
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListBox lbConfig;
  private ContextMenuStrip contextMenuStrip;
  private ToolStripMenuItem cmiCopy;

  public ConfigurationForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.instGuid = aInstGuid;
  }

  public override void FillForm(IFolder folder)
  {
    this._folder = folder as CustomFolder;
    this.lbConfig.Items.Clear();
    this.lbConfig.Items.Add((object) "<appSettings>");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (object obj in (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).GetServerConfigInfo())
        this.lbConfig.Items.Add(obj);
    }
    this.lbConfig.Items.Add((object) "</appSettings>");
  }

  public override bool SaveForm(IFolder folder) => true;

  public override void FormLostFocus(IFolder folder)
  {
  }

  /// <summary>id раздела справки</summary>
  public override string HelpTopicID => this._folder == null ? base.HelpTopicID : "1060";

  private void cmiCopy_Click(object sender, EventArgs e)
  {
    if (this.lbConfig.SelectedIndex == -1)
      return;
    string text = this.lbConfig.Items[this.lbConfig.SelectedIndex].ToString();
    int num = 0;
    while (true)
    {
      try
      {
        ++num;
        if (num > 10)
          break;
        Clipboard.SetText(text);
      }
      catch
      {
      }
    }
  }

  private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
  {
    if (this.lbConfig.SelectedIndex != -1)
      return;
    e.Cancel = true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConfigurationForm));
    this.lbConfig = new ListBox();
    this.contextMenuStrip = new ContextMenuStrip(this.components);
    this.cmiCopy = new ToolStripMenuItem();
    this.contextMenuStrip.SuspendLayout();
    this.SuspendLayout();
    this.lbConfig.ContextMenuStrip = this.contextMenuStrip;
    componentResourceManager.ApplyResources((object) this.lbConfig, "lbConfig");
    this.lbConfig.FormattingEnabled = true;
    this.lbConfig.Name = "lbConfig";
    this.contextMenuStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.cmiCopy
    });
    this.contextMenuStrip.Name = "contextMenuStrip";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip, "contextMenuStrip");
    this.contextMenuStrip.Opening += new CancelEventHandler(this.contextMenuStrip_Opening);
    this.cmiCopy.Name = "cmiCopy";
    componentResourceManager.ApplyResources((object) this.cmiCopy, "cmiCopy");
    this.cmiCopy.Click += new EventHandler(this.cmiCopy_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.lbConfig);
    this.Name = nameof (ConfigurationForm);
    this.Tag = (object) "   ";
    this.contextMenuStrip.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
