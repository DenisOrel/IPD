
// Type: Intermech.PropertyEditors.PropertyTabPageForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraGrid;
using Intermech.Holders;
using Intermech.Interfaces.Client;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for PropertyTabPageForm.</summary>
public class PropertyTabPageForm : PropertyBaseForm
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private TabControl tabControl;
  private bool _BlockOnTabChangeLocal;
  private TabPage tabPage1;
  private Panel panel;
  private BaseTabPage _LastTabPage;
  /// <summary>id хелпа дял активной закладки</summary>
  public string helpTopicID;

  public override IBaseTabPage LastTabPage => (IBaseTabPage) this._LastTabPage;

  public PropertyTabPageForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
  }

  /// <summary>Clean up any resources being used.</summary>
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
    this.HelpRequested += new HelpEventHandler(this.PropertyTabPageForm_HelpRequested);
    this.tabControl = new TabControl();
    this.tabPage1 = new TabPage();
    this.panel = new Panel();
    this.tabControl.SuspendLayout();
    this.SuspendLayout();
    this.tabControl.Controls.Add((Control) this.tabPage1);
    this.tabControl.Dock = DockStyle.Top;
    this.tabControl.Location = new Point(0, 0);
    this.tabControl.Name = "tabControl";
    this.tabControl.SelectedIndex = 0;
    this.tabControl.Size = new Size(876, 25);
    this.tabControl.TabIndex = 0;
    this.tabControl.SelectedIndexChanged += new EventHandler(this.tabControl_SelectedIndexChanged);
    this.tabPage1.Location = new Point(4, 22);
    this.tabPage1.Name = "tabPage1";
    this.tabPage1.Size = new Size(868, 0);
    this.tabPage1.TabIndex = 0;
    this.tabPage1.Text = "tabPage1";
    this.panel.Dock = DockStyle.Fill;
    this.panel.Location = new Point(0, 25);
    this.panel.Name = "panel";
    this.panel.Size = new Size(876, 403);
    this.panel.TabIndex = 1;
    this.ClientSize = new Size(876, 428);
    this.Controls.Add((Control) this.panel);
    this.Controls.Add((Control) this.tabControl);
    this.Name = nameof (PropertyTabPageForm);
    this.Text = nameof (PropertyTabPageForm);
    this.tabControl.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>нажили f1</summary>
  /// <param name="sender"></param>
  /// <param name="hlpevent"></param>
  private void PropertyTabPageForm_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    HelpProvidersClass.ShowHelpTopic(this.helpTopicID);
  }

  private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (TabControlProcessor.BlockTabPageChangedEvent || this._BlockOnTabChangeLocal)
      return;
    EventsHolder.TabControlPageOpeningArgs e1 = new EventsHolder.TabControlPageOpeningArgs(this.TabControl.TabPages[this.TabControl.SelectedIndex], false);
    EventsHolder.FireTabControlPageOpening(sender, this.instGuid, e1);
    if (e1.Cancel)
    {
      try
      {
        this._BlockOnTabChangeLocal = true;
        this.TabControl.SelectedTab = (TabPage) this.LastTabPage;
      }
      finally
      {
        this._BlockOnTabChangeLocal = false;
      }
    }
    else
      this.OpenTabPage(this.TabControl.TabPages[this.TabControl.SelectedIndex]);
  }

  public override void OpenTabPage(TabPage tabpage)
  {
    if (!(tabpage is IBaseTabPage baseTabPage))
      return;
    this._BlockOnTabChangeLocal = true;
    try
    {
      this.TabControl.SelectedTab = tabpage;
    }
    finally
    {
      this._BlockOnTabChangeLocal = false;
    }
    baseTabPage.DockToPanel(this.panel);
    this._LastTabPage = (BaseTabPage) tabpage;
    baseTabPage.TabPageProcessingForm.FillForm(this.Folder);
    this.helpTopicID = baseTabPage.TabPageProcessingForm.HelpTopicID;
  }

  public override void DefaultsOnLoad()
  {
    StatesController.Clear();
    for (int index = 0; index < this.tabControl.TabPages.Count; ++index)
    {
      bool aLoadState = false;
      bool aModifiedState = false;
      if (this.tabControl.TabPages[index] == TabPagesHolder.TabPages(this.instGuid).PropertyTabPage)
      {
        aLoadState = true;
        aModifiedState = this.Folder.InChange;
      }
      if (this.tabControl.TabPages[index] == TabPagesHolder.TabPages(this.instGuid).ListTabPage)
        aLoadState = true;
      StatesController.Add((object) this.tabControl.TabPages[index], aLoadState, aModifiedState);
    }
    if (this.LastTabPage != null && this.Folder.IsVirtualFolder)
      this._LastTabPage = (BaseTabPage) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage;
    if (this.LastTabPage != null && this.tabControl.TabPages.IndexOf((TabPage) this.LastTabPage) != -1)
    {
      this.OpenTabPage((TabPage) this.LastTabPage);
    }
    else
    {
      if (this.tabControl.TabPages.Count <= 0)
        return;
      this.OpenTabPage(this.tabControl.TabPages[0]);
    }
  }

  public override bool DefaultsOnSave()
  {
    bool flag = true;
    for (int index = 0; index < this.tabControl.TabPages.Count; ++index)
    {
      IBaseTabPage tabPage = this.tabControl.TabPages[index] as IBaseTabPage;
      flag = flag && tabPage.TabPageProcessingForm.SaveForm(this.Folder);
    }
    return flag;
  }

  public override void DefaultsOnLostFocus(IFolder folder)
  {
    for (int index = 0; index < this.tabControl.TabPages.Count; ++index)
      (this.tabControl.TabPages[index] as IBaseTabPage).TabPageProcessingForm.FormLostFocus(folder);
  }

  public override TabControl TabControl => this.tabControl;

  public override PropertyGrid PropertyGrid
  {
    get
    {
      return this.TabControl.TabPages.IndexOf((TabPage) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage) != -1 ? PropertyFormsHolder.PropertyForms(this.instGuid).PropertyForm.PropertyGrid : (PropertyGrid) null;
    }
  }

  public override GridControl GridControl
  {
    get
    {
      return this.TabControl.TabPages.IndexOf((TabPage) TabPagesHolder.TabPages(this.instGuid).ListTabPage) != -1 ? PropertyFormsHolder.PropertyForms(this.instGuid).ListForm.GridControl : (GridControl) null;
    }
  }
}
