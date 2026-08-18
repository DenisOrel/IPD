// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.FindReplaceForm
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Document.Model.FindReplace;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

internal class FindReplaceForm : Form
{
  protected System.Windows.Forms.TabControl _tabControlFindOrReplace;
  private System.Windows.Forms.TabPage _tabPageFind;
  private IContainer components;
  private ImageList _imageList;
  private UserControlFindReplace ControlFindReplace;
  private System.Windows.Forms.TabPage _tabPageReplace;
  private static FindReplaceForm instance;
  private FindReplaceManager findReplaceManager;
  private bool replaceMode = true;

  protected override void OnGotFocus(EventArgs e) => base.OnGotFocus(e);

  protected override void OnVisibleChanged(EventArgs e)
  {
    base.OnVisibleChanged(e);
    if (!this.Visible)
      return;
    if (this.ParentForm != null)
    {
      this.ParentForm.CancelButton = (IButtonControl) this.ControlFindReplace._btnClose;
      this.ParentForm.AcceptButton = (IButtonControl) this.ControlFindReplace._btnFindNext;
    }
    else
    {
      this.CancelButton = (IButtonControl) this.ControlFindReplace._btnClose;
      this.AcceptButton = (IButtonControl) this.ControlFindReplace._btnFindNext;
    }
    this.ControlFindReplace._comboBoxFindText.Focus();
    this.ActiveControl = (Control) this.ControlFindReplace._comboBoxFindText;
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    bool flag = base.ProcessCmdKey(ref msg, keyData);
    if (keyData == Keys.Escape)
    {
      this.Close();
      flag = true;
    }
    return flag;
  }

  public static FindReplaceForm Execute(FindReplaceManager manager, bool findWithReplace)
  {
    if (FindReplaceForm.instance != null)
      FindReplaceForm.instance.Close();
    FindReplaceForm findReplaceForm1 = new FindReplaceForm(manager);
    FindReplaceForm.instance = findReplaceForm1;
    findReplaceForm1.Show();
    findReplaceForm1.ReplaceMode = findWithReplace;
    findReplaceForm1.ReplaceMode = findWithReplace;
    FindReplaceForm findReplaceForm2 = findReplaceForm1;
    Size size1 = findReplaceForm1.Size;
    int width = size1.Width + 26;
    size1 = findReplaceForm1.Size;
    int height = size1.Height;
    Size size2 = new Size(width, height);
    findReplaceForm2.Size = size2;
    return findReplaceForm1;
  }

  protected override void OnClosed(EventArgs e)
  {
    if (this.findReplaceManager != null && this.findReplaceManager.DocumentControl != null && this.findReplaceManager.DocumentControl.DocumentManager != null && this.findReplaceManager.DocumentControl.DocumentManager.CommandManager != null)
      this.findReplaceManager.DocumentControl.DocumentManager.CommandManager.ActiveTarget = (ICommandTarget) this.findReplaceManager.DocumentControl.DocumentEditorForm;
    this.FindReplaceManager = (FindReplaceManager) null;
    base.OnClosed(e);
    DocumentMenuHelper.DockManager.DockControlActivated -= new DockControlEventHandler(this.DockManager_DockControlActivated);
  }

  public FindReplaceForm(FindReplaceManager manager)
  {
    this.InitializeComponent();
    this.TopMost = true;
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.Text = "Найти";
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.FindReplaceManager = manager;
    this.MinimumSize = new Size(645, 180);
    DocumentMenuHelper.DockManager.DockControlActivated += new DockControlEventHandler(this.DockManager_DockControlActivated);
  }

  private void DockManager_DockControlActivated(object sender, DockControlEventArgs e)
  {
    if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null)
      return;
    this.FindReplaceManager = DocumentMenuHelper.ActiveImDocumentEditorFormBase.FindReplaceManager;
  }

  public FindReplaceManager FindReplaceManager
  {
    get => this.findReplaceManager;
    set
    {
      if (this.findReplaceManager == value)
        return;
      if (this.findReplaceManager != null && this.findReplaceManager.DocumentControl != null)
        this.findReplaceManager.DocumentControl.DocumentEditorForm.Closing -= new CancelEventHandler(this.DocumentEditorForm_Closing);
      this.findReplaceManager = value;
      if (this.findReplaceManager != null && this.findReplaceManager.DocumentControl != null)
        this.findReplaceManager.DocumentControl.DocumentEditorForm.Closing += new CancelEventHandler(this.DocumentEditorForm_Closing);
      this.ControlFindReplace.FindReplaceManager = this.findReplaceManager;
      if (this.findReplaceManager == null)
        return;
      this.ControlFindReplace.FromManager();
      if (this.findReplaceManager.DocumentControl != null && this.findReplaceManager.DocumentControl.ReadOnly)
        this._tabControlFindOrReplace.TabPages.Remove(this._tabPageReplace);
      else
        this._tabPageReplace.Visible = true;
    }
  }

  private void DocumentEditorForm_Closing(object sender, CancelEventArgs e)
  {
    if (e.Cancel)
      return;
    this.Close();
  }

  private void DocumentEditorForm_Closed(object sender, EventArgs e)
  {
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FindReplaceForm));
    this._tabControlFindOrReplace = new System.Windows.Forms.TabControl();
    this._tabPageFind = new System.Windows.Forms.TabPage();
    this.ControlFindReplace = new UserControlFindReplace();
    this._tabPageReplace = new System.Windows.Forms.TabPage();
    this._imageList = new ImageList(this.components);
    this._tabControlFindOrReplace.SuspendLayout();
    this._tabPageFind.SuspendLayout();
    this.SuspendLayout();
    this._tabControlFindOrReplace.Controls.Add((Control) this._tabPageFind);
    this._tabControlFindOrReplace.Controls.Add((Control) this._tabPageReplace);
    componentResourceManager.ApplyResources((object) this._tabControlFindOrReplace, "_tabControlFindOrReplace");
    this._tabControlFindOrReplace.Name = "_tabControlFindOrReplace";
    this._tabControlFindOrReplace.SelectedIndex = 0;
    this._tabControlFindOrReplace.SelectedIndexChanged += new EventHandler(this._tabControlFindOrReplace_SelectedIndexChanged);
    this._tabPageFind.Controls.Add((Control) this.ControlFindReplace);
    componentResourceManager.ApplyResources((object) this._tabPageFind, "_tabPageFind");
    this._tabPageFind.Name = "_tabPageFind";
    this._tabPageFind.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.ControlFindReplace, "ControlFindReplace");
    this.ControlFindReplace.BackColor = SystemColors.Window;
    this.ControlFindReplace.MatchCase = false;
    this.ControlFindReplace.MatchWholeWord = false;
    this.ControlFindReplace.Name = "ControlFindReplace";
    this.ControlFindReplace.PossibleSearchPlaces = new string[0];
    this.ControlFindReplace.SearchDirrection = SearchDirrection.EntireDocSearch;
    this.ControlFindReplace.SelectedSearchPlace = -1;
    componentResourceManager.ApplyResources((object) this._tabPageReplace, "_tabPageReplace");
    this._tabPageReplace.Name = "_tabPageReplace";
    this._tabPageReplace.UseVisualStyleBackColor = true;
    this._imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imageList.ImageStream");
    this._imageList.TransparentColor = Color.Transparent;
    this._imageList.Images.SetKeyName(0, "DropDownParams2.gif");
    this._imageList.Images.SetKeyName(1, "DropUpParams2.gif");
    this.Controls.Add((Control) this._tabControlFindOrReplace);
    this.MinimumSize = new Size(645, 324);
    this.Name = nameof (FindReplaceForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this._tabControlFindOrReplace.ResumeLayout(false);
    this._tabPageFind.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public bool ReplaceMode
  {
    get => this.replaceMode;
    set
    {
      this.replaceMode = (this.findReplaceManager.DocumentControl == null || !this.findReplaceManager.DocumentControl.ReadOnly) && value;
      if (this.replaceMode)
      {
        this.ControlFindReplace.Parent = (Control) this._tabPageReplace;
        this._tabControlFindOrReplace.SelectedTab = this._tabPageReplace;
      }
      else
      {
        this.ControlFindReplace.Parent = (Control) this._tabPageFind;
        this._tabControlFindOrReplace.SelectedTab = this._tabPageFind;
      }
      this.ControlFindReplace.IsReplaceMode = this.replaceMode;
    }
  }

  private void _tabControlFindOrReplace_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.ReplaceMode = this._tabControlFindOrReplace.SelectedIndex != 0;
  }
}
