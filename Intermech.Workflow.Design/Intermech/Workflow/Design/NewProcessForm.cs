// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.NewProcessForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for NewProcessForm.</summary>
public class NewProcessForm : Form
{
  private GroupBox propBox;
  private GroupBox AttachGroupBox;
  private Panel panel3;
  private TabPage DescPage;
  private TabPage AttachmentsPage;
  private TabPage SchemesPage;
  private IContainer components;
  private Button CancelBtn;
  private Button NextBtn;
  public TabControl Wizard;
  private Button PrevBtn;
  private ImageList AnswerIL;
  private Panel panel1;
  private Label nameLabel;
  private Panel panel8;
  private Label label2;
  private ComboBoxEx PriorityBox;
  public TextBox ProcessNameEdit;
  private WizardHandler wizardHandler;
  private long _schemeID;
  private PageViewsManager pageViewsManager;
  private Splitter schemesSplitter;
  private SchemesTreeView schemesView;
  private TreeViewsBridge treeViewsBridge1;
  private AttachmentsView attachsView;
  private TabPage FormPage;
  private GroupBox formBox;
  private TabPage PreCheckInPage;
  private GroupBox groupBox2;
  private TabPage CheckInPage;
  private GroupBox CheckInGroupBox;
  private string _captionPrefix;
  private PictureBox WarnBox;
  private Panel ClientPanel;
  private Panel BottomPanel;
  private Panel panel13;
  public TextBox MessageTextEdit;
  private ToolBar MsgBar;
  private ToolBarButton toolBarButton1;
  private ToolBarButton toolBarButton2;
  private Label label5;
  private Label label1;
  private LinkLabel showBaseVersion;
  public long SchemeRootGroupID;
  private CheckInObjectsForm _checkInForm;
  private IProcess _createdProcess;
  private RemoteLock _remoteLock = new RemoteLock();
  private FormDesignerView _formControl;
  public AttachmentList Attachments = new AttachmentList();
  private bool _checkInInProgress;
  private string _schemeName = "";
  private SchemeStatus _schemeStatus = SchemeStatus.Invalid;
  private bool _schemeIsDebug;
  private long _formID;
  private ISelectedItemsHost _selhost;
  private NodeColumnCollection _defColumn = new NodeColumnCollection();

  public NewProcessForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 823);
    this.attachsView.BackColor = SystemColors.Window;
    this._captionPrefix = this.Text;
    this.wizardHandler = new WizardHandler(this.Wizard, this.PrevBtn, this.NextBtn, this.CancelBtn);
    wfFunx.RegisterLoadSaveCommands(this.MsgBar, this.MessageTextEdit);
    this.wizardHandler.BeforeSelectionChanged += new WizardHandler.SelectionChangedHandler(this.wizardHandler_BeforeSelectionChanged);
    this.wizardHandler.AfterSelectionChanged += new WizardHandler.SelectionChangedHandler(this.wizardHandler_AfterSelectionChanged);
    if (this.WarnBox.Image is Bitmap)
      ((Bitmap) this.WarnBox.Image).MakeTransparent(Color.Fuchsia);
    this.FormClosing += new FormClosingEventHandler(this.NewProcessForm_FormClosing);
    this.attachsView.ItemsChanged += new EventHandler(this.attachsView_ItemsChanged);
    if (!this.DesignMode)
    {
      this.Icon = Holder.SchemeIcon;
      this._checkInForm = new CheckInObjectsForm();
      this._checkInForm.TopLevel = false;
      this._checkInForm.FormBorderStyle = FormBorderStyle.None;
      this._checkInForm.Parent = (Control) this.CheckInGroupBox;
      this._checkInForm.Embedded = true;
      this._checkInForm.Dock = DockStyle.Fill;
      this._checkInForm.Visible = true;
    }
    this.showBaseVersion.Visible = Holder.IsAdmin;
  }

  private void attachsView_ItemsChanged(object sender, EventArgs e) => this.UpdateAttachments();

  private void NewProcessForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.DialogResult == DialogResult.Abort)
      e.Cancel = true;
    if (this.DialogResult != DialogResult.OK || this._createdProcess == null)
      return;
    this._createdProcess.Name = this.ProcessNameEdit.Text;
    if (this._createdProcess.IsCreationMode)
      this._createdProcess.CommitCreation(false);
    IActivity startActivity = this._createdProcess.StartActivity;
    if (startActivity == null)
      throw new Exception(LocalizationHolder.rm.GetString("ErrStartActivityNotFound"));
    try
    {
      this._remoteLock.Add((object) startActivity);
      wfFunx.ExecClientScript(ScriptKind.BeforeExec, startActivity);
      this.SaveFormData(startActivity.ObjectID);
      this._createdProcess.Priority = this.Priority;
      if (this.MessageTextEdit.Text != "")
        startActivity.MessageText = this.MessageTextEdit.Text;
      this.Attachments.Save((IDBObject) startActivity);
      startActivity.Changed(ActivityChanged.Variables | ActivityChanged.Attachments);
      wfFunx.ExecClientScript(ScriptKind.AfterExec, startActivity);
      this._createdProcess.StartProcess();
    }
    finally
    {
      this._remoteLock.Remove((object) startActivity);
    }
  }

  private int PageIndex(TabPage p) => this.Wizard.TabPages.IndexOf(p);

  private void UpdateAttachments()
  {
    if (this._checkInInProgress)
      return;
    if (this.Attachments.WorkCopies.Count <= 0)
    {
      this.Wizard.TabPages.Remove(this.PreCheckInPage);
      this.Wizard.TabPages.Remove(this.CheckInPage);
    }
    else if (this.Wizard.TabPages.IndexOf(this.PreCheckInPage) == -1)
    {
      int index = this.PageIndex(this.AttachmentsPage) + 1;
      this.Wizard.TabPages.Insert(index, this.CheckInPage);
      this.Wizard.TabPages.Insert(index, this.PreCheckInPage);
    }
    this.wizardHandler.UpdateButtons();
  }

  private bool wizardHandler_BeforeSelectionChanged(ref int NewIndex)
  {
    if (this.PageIndex(this.PreCheckInPage) == NewIndex && this.Wizard.SelectedIndex == this.PageIndex(this.CheckInPage))
      NewIndex = this.PageIndex(this.AttachmentsPage);
    if (this.PageIndex(this.DescPage) == NewIndex)
    {
      if (this._schemeStatus == SchemeStatus.Invalid)
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Workflow.Design_76"), (object) this.SchemeName), LocalizationHolder.rm.GetString("Workflow.Design_77"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        --NewIndex;
      }
    }
    else if (this.PageIndex(this.FormPage) == NewIndex)
    {
      if (this._formControl == null && this._createdProcess != null)
      {
        this._formControl = new FormDesignerView(this._createdProcess.ObjectID, this.FormID);
        this._formControl.Parent = (Control) this.formBox;
        this._formControl.Dock = DockStyle.Fill;
        this._formControl.LoadForm();
        if (this._formControl.MinimumSize.Height > 0 || this._formControl.MinimumSize.Width > 0)
        {
          int num = Math.Max(this.MinimumSize.Height, this._formControl.MinimumSize.Height);
          this.MinimumSize = new Size(Math.Max(this.MinimumSize.Width, this._formControl.MinimumSize.Width) + 10, num + this.BottomPanel.Height * 3);
        }
      }
    }
    else if (this.Wizard.SelectedIndex == this.PageIndex(this.AttachmentsPage) && NewIndex > this.Wizard.SelectedIndex)
    {
      DialogResult dialogResult = DialogResult.Yes;
      if (this.Attachments.CheckOutByOtherUser.Count > 0)
        dialogResult = MessageBox.Show("Процесс содержит вложения, взятые другим пользователем на изменение. Это может привести к ошибкам в дальнейшей работе процесса. Хотите продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
      if (dialogResult == DialogResult.No)
      {
        this.Wizard.Refresh();
        this.NextBtn.Enabled = false;
        this.DialogResult = DialogResult.Abort;
        return false;
      }
    }
    return true;
  }

  public void SaveFormData(long objID) => this._formControl?.SaveForm(objID, true);

  private bool wizardHandler_AfterSelectionChanged(ref int NewIndex)
  {
    if (NewIndex == this.PageIndex(this.AttachmentsPage))
    {
      this.CreateProcess();
      if (this._createdProcess != null)
      {
        this.attachsView.ProcessID = this._createdProcess.ObjectID;
        this.attachsView.Load(this.Attachments);
      }
    }
    else if (this.PageIndex(this.DescPage) == NewIndex)
      this.ActiveControl = (Control) this.ProcessNameEdit;
    else if (this.PageIndex(this.CheckInPage) == NewIndex)
    {
      this.Wizard.Refresh();
      this._checkInInProgress = true;
      try
      {
        this._checkInForm.DoCheckIn(this.Attachments);
        if (this._checkInForm.DialogResult == DialogResult.Abort)
          this.NextBtn.Enabled = false;
      }
      finally
      {
        this._checkInInProgress = false;
      }
    }
    return true;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      this._remoteLock?.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NewProcessForm));
    this.CancelBtn = new Button();
    this.NextBtn = new Button();
    this.PrevBtn = new Button();
    this.Wizard = new TabControl();
    this.SchemesPage = new TabPage();
    this.pageViewsManager = new PageViewsManager();
    this.schemesSplitter = new Splitter();
    this.DescPage = new TabPage();
    this.propBox = new GroupBox();
    this.panel1 = new Panel();
    this.panel13 = new Panel();
    this.MessageTextEdit = new TextBox();
    this.label5 = new Label();
    this.MsgBar = new ToolBar();
    this.toolBarButton1 = new ToolBarButton();
    this.toolBarButton2 = new ToolBarButton();
    this.AnswerIL = new ImageList(this.components);
    this.panel8 = new Panel();
    this.label2 = new Label();
    this.ProcessNameEdit = new TextBox();
    this.nameLabel = new Label();
    this.AttachmentsPage = new TabPage();
    this.AttachGroupBox = new GroupBox();
    this.panel3 = new Panel();
    this.PreCheckInPage = new TabPage();
    this.groupBox2 = new GroupBox();
    this.label1 = new Label();
    this.WarnBox = new PictureBox();
    this.CheckInPage = new TabPage();
    this.CheckInGroupBox = new GroupBox();
    this.FormPage = new TabPage();
    this.formBox = new GroupBox();
    this.ClientPanel = new Panel();
    this.BottomPanel = new Panel();
    this.schemesView = new SchemesTreeView();
    this.PriorityBox = new ComboBoxEx();
    this.attachsView = new AttachmentsView();
    this.treeViewsBridge1 = new TreeViewsBridge(this.components);
    this.showBaseVersion = new LinkLabel();
    this.Wizard.SuspendLayout();
    this.SchemesPage.SuspendLayout();
    this.DescPage.SuspendLayout();
    this.propBox.SuspendLayout();
    this.panel1.SuspendLayout();
    this.panel13.SuspendLayout();
    this.panel8.SuspendLayout();
    this.AttachmentsPage.SuspendLayout();
    this.AttachGroupBox.SuspendLayout();
    this.panel3.SuspendLayout();
    this.PreCheckInPage.SuspendLayout();
    this.groupBox2.SuspendLayout();
    ((ISupportInitialize) this.WarnBox).BeginInit();
    this.CheckInPage.SuspendLayout();
    this.FormPage.SuspendLayout();
    this.ClientPanel.SuspendLayout();
    this.BottomPanel.SuspendLayout();
    this.schemesView.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.CancelBtn, "CancelBtn");
    this.CancelBtn.DialogResult = DialogResult.Cancel;
    this.CancelBtn.Name = "CancelBtn";
    componentResourceManager.ApplyResources((object) this.NextBtn, "NextBtn");
    this.NextBtn.Name = "NextBtn";
    componentResourceManager.ApplyResources((object) this.PrevBtn, "PrevBtn");
    this.PrevBtn.Name = "PrevBtn";
    this.Wizard.Controls.Add((Control) this.SchemesPage);
    this.Wizard.Controls.Add((Control) this.DescPage);
    this.Wizard.Controls.Add((Control) this.AttachmentsPage);
    this.Wizard.Controls.Add((Control) this.PreCheckInPage);
    this.Wizard.Controls.Add((Control) this.CheckInPage);
    this.Wizard.Controls.Add((Control) this.FormPage);
    componentResourceManager.ApplyResources((object) this.Wizard, "Wizard");
    this.Wizard.Name = "Wizard";
    this.Wizard.SelectedIndex = 0;
    this.Wizard.TabStop = false;
    this.SchemesPage.BackColor = SystemColors.ButtonFace;
    this.SchemesPage.Controls.Add((Control) this.pageViewsManager);
    this.SchemesPage.Controls.Add((Control) this.showBaseVersion);
    this.SchemesPage.Controls.Add((Control) this.schemesSplitter);
    this.SchemesPage.Controls.Add((Control) this.schemesView);
    componentResourceManager.ApplyResources((object) this.SchemesPage, "SchemesPage");
    this.SchemesPage.Name = "SchemesPage";
    this.pageViewsManager.ActiveViewPage = (IViewPage) null;
    this.pageViewsManager.AllowedViews = new string[1]
    {
      "ChildrenView"
    };
    this.pageViewsManager.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this.pageViewsManager, "pageViewsManager");
    this.pageViewsManager.Name = "pageViewsManager";
    componentResourceManager.ApplyResources((object) this.schemesSplitter, "schemesSplitter");
    this.schemesSplitter.Name = "schemesSplitter";
    this.schemesSplitter.TabStop = false;
    this.DescPage.BackColor = SystemColors.ButtonFace;
    this.DescPage.Controls.Add((Control) this.propBox);
    componentResourceManager.ApplyResources((object) this.DescPage, "DescPage");
    this.DescPage.Name = "DescPage";
    this.propBox.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.propBox, "propBox");
    this.propBox.Name = "propBox";
    this.propBox.TabStop = false;
    this.panel1.Controls.Add((Control) this.panel13);
    this.panel1.Controls.Add((Control) this.panel8);
    this.panel1.Controls.Add((Control) this.ProcessNameEdit);
    this.panel1.Controls.Add((Control) this.nameLabel);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.panel13.Controls.Add((Control) this.MessageTextEdit);
    this.panel13.Controls.Add((Control) this.label5);
    this.panel13.Controls.Add((Control) this.MsgBar);
    componentResourceManager.ApplyResources((object) this.panel13, "panel13");
    this.panel13.Name = "panel13";
    this.MessageTextEdit.AcceptsReturn = true;
    componentResourceManager.ApplyResources((object) this.MessageTextEdit, "MessageTextEdit");
    this.MessageTextEdit.Name = "MessageTextEdit";
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.MsgBar, "MsgBar");
    this.MsgBar.Buttons.AddRange(new ToolBarButton[2]
    {
      this.toolBarButton1,
      this.toolBarButton2
    });
    this.MsgBar.Divider = false;
    this.MsgBar.ImageList = this.AnswerIL;
    this.MsgBar.Name = "MsgBar";
    componentResourceManager.ApplyResources((object) this.toolBarButton1, "toolBarButton1");
    this.toolBarButton1.Name = "toolBarButton1";
    this.toolBarButton1.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.toolBarButton2, "toolBarButton2");
    this.toolBarButton2.Name = "toolBarButton2";
    this.toolBarButton2.Tag = (object) "2";
    this.AnswerIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("AnswerIL.ImageStream");
    this.AnswerIL.TransparentColor = Color.Fuchsia;
    this.AnswerIL.Images.SetKeyName(0, "открыть.png");
    this.AnswerIL.Images.SetKeyName(1, "сохранить.png");
    this.AnswerIL.Images.SetKeyName(2, "wflowpriority.bmp");
    this.AnswerIL.Images.SetKeyName(3, "wfhighpriority.bmp");
    this.panel8.Controls.Add((Control) this.PriorityBox);
    this.panel8.Controls.Add((Control) this.label2);
    componentResourceManager.ApplyResources((object) this.panel8, "panel8");
    this.panel8.Name = "panel8";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.ProcessNameEdit, "ProcessNameEdit");
    this.ProcessNameEdit.Name = "ProcessNameEdit";
    componentResourceManager.ApplyResources((object) this.nameLabel, "nameLabel");
    this.nameLabel.Name = "nameLabel";
    this.AttachmentsPage.BackColor = SystemColors.ButtonFace;
    this.AttachmentsPage.Controls.Add((Control) this.AttachGroupBox);
    componentResourceManager.ApplyResources((object) this.AttachmentsPage, "AttachmentsPage");
    this.AttachmentsPage.Name = "AttachmentsPage";
    this.AttachGroupBox.Controls.Add((Control) this.panel3);
    componentResourceManager.ApplyResources((object) this.AttachGroupBox, "AttachGroupBox");
    this.AttachGroupBox.Name = "AttachGroupBox";
    this.AttachGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Controls.Add((Control) this.attachsView);
    this.panel3.Name = "panel3";
    this.PreCheckInPage.BackColor = SystemColors.ButtonFace;
    this.PreCheckInPage.Controls.Add((Control) this.groupBox2);
    componentResourceManager.ApplyResources((object) this.PreCheckInPage, "PreCheckInPage");
    this.PreCheckInPage.Name = "PreCheckInPage";
    this.groupBox2.Controls.Add((Control) this.label1);
    this.groupBox2.Controls.Add((Control) this.WarnBox);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.WarnBox.BackColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.WarnBox, "WarnBox");
    this.WarnBox.Name = "WarnBox";
    this.WarnBox.TabStop = false;
    this.CheckInPage.BackColor = SystemColors.ButtonFace;
    this.CheckInPage.Controls.Add((Control) this.CheckInGroupBox);
    componentResourceManager.ApplyResources((object) this.CheckInPage, "CheckInPage");
    this.CheckInPage.Name = "CheckInPage";
    componentResourceManager.ApplyResources((object) this.CheckInGroupBox, "CheckInGroupBox");
    this.CheckInGroupBox.Name = "CheckInGroupBox";
    this.CheckInGroupBox.TabStop = false;
    this.FormPage.BackColor = SystemColors.ButtonFace;
    this.FormPage.Controls.Add((Control) this.formBox);
    componentResourceManager.ApplyResources((object) this.FormPage, "FormPage");
    this.FormPage.Name = "FormPage";
    componentResourceManager.ApplyResources((object) this.formBox, "formBox");
    this.formBox.Name = "formBox";
    this.formBox.TabStop = false;
    this.ClientPanel.Controls.Add((Control) this.Wizard);
    componentResourceManager.ApplyResources((object) this.ClientPanel, "ClientPanel");
    this.ClientPanel.Name = "ClientPanel";
    this.BottomPanel.Controls.Add((Control) this.CancelBtn);
    this.BottomPanel.Controls.Add((Control) this.NextBtn);
    this.BottomPanel.Controls.Add((Control) this.PrevBtn);
    componentResourceManager.ApplyResources((object) this.BottomPanel, "BottomPanel");
    this.BottomPanel.Name = "BottomPanel";
    this.schemesView.AllowDrop = true;
    this.schemesView.AllowMultiSelect = false;
    this.schemesView.AllowUserPinnedColumns = false;
    this.schemesView.DisableCheckedOutColumn = true;
    this.schemesView.DisableKeyDownEvents = true;
    componentResourceManager.ApplyResources((object) this.schemesView, "schemesView");
    this.schemesView.ImageList = (ImageList) null;
    this.schemesView.LineStyle = LineStyle.Dot;
    this.schemesView.Name = "schemesView";
    this.schemesView.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowEvenStyle.WordWrap");
    this.schemesView.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowOddStyle.WordWrap");
    this.schemesView.RowSelectedStyle.BackColor = SystemColors.Highlight;
    this.schemesView.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowSelectedStyle.WordWrap");
    this.schemesView.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.schemesView.RowStyle.BorderColor = SystemColors.Control;
    this.schemesView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.schemesView.RowStyle.BorderWidth = 1;
    this.schemesView.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowStyle.WordWrap");
    this.schemesView.SelectBeforeEdit = true;
    this.schemesView.ShowRootRow = false;
    this.schemesView.SuppressErrorMessages = true;
    componentResourceManager.ApplyResources((object) this.PriorityBox, "PriorityBox");
    this.PriorityBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.PriorityBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.PriorityBox.ImageList = this.AnswerIL;
    this.PriorityBox.Items.AddRange(new object[1]
    {
      (object) componentResourceManager.GetString("PriorityBox.Items")
    });
    this.PriorityBox.Name = "PriorityBox";
    componentResourceManager.ApplyResources((object) this.attachsView, "attachsView");
    this.attachsView.AllowCustomGroupValues = true;
    this.attachsView.CanAttach = true;
    this.attachsView.CanDetach = true;
    this.attachsView.Control = (object) this.attachsView;
    this.attachsView.DisableCheckedOutColumn = true;
    this.attachsView.DisableFiltration = true;
    this.attachsView.DisableKeyDownEvents = false;
    this.attachsView.DisablePacketsReading = true;
    this.attachsView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.attachsView.Name = "attachsView";
    this.attachsView.ProcessID = 0L;
    this.attachsView.ReadOnly = false;
    this.attachsView.Tag = (object) " ";
    this.attachsView.ViewContentType = ContentType.Folders;
    this.treeViewsBridge1.NavTreeView = (NavigatorTreeView) this.schemesView;
    this.treeViewsBridge1.UseDelay = false;
    this.treeViewsBridge1.ViewsManager = (IViewsManager) this.pageViewsManager;
    componentResourceManager.ApplyResources((object) this.showBaseVersion, "showBaseVersion");
    this.showBaseVersion.Name = "showBaseVersion";
    this.showBaseVersion.TabStop = true;
    this.showBaseVersion.LinkClicked += new LinkLabelLinkClickedEventHandler(this.showBaseVersion_LinkClicked);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackColor = SystemColors.Control;
    this.Controls.Add((Control) this.ClientPanel);
    this.Controls.Add((Control) this.BottomPanel);
    this.HelpButton = true;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (NewProcessForm);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.NewProcessForm_FormClosed);
    this.Load += new EventHandler(this.NewProcessForm_Load);
    this.VisibleChanged += new EventHandler(this.NewProcessForm_VisibleChanged);
    this.KeyDown += new KeyEventHandler(this.NewProcessForm_KeyDown);
    this.Wizard.ResumeLayout(false);
    this.SchemesPage.ResumeLayout(false);
    this.SchemesPage.PerformLayout();
    this.DescPage.ResumeLayout(false);
    this.propBox.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel13.ResumeLayout(false);
    this.panel13.PerformLayout();
    this.panel8.ResumeLayout(false);
    this.panel8.PerformLayout();
    this.AttachmentsPage.ResumeLayout(false);
    this.AttachGroupBox.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.PreCheckInPage.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    ((ISupportInitialize) this.WarnBox).EndInit();
    this.CheckInPage.ResumeLayout(false);
    this.FormPage.ResumeLayout(false);
    this.ClientPanel.ResumeLayout(false);
    this.BottomPanel.ResumeLayout(false);
    this.schemesView.EndInit();
    this.ResumeLayout(false);
  }

  private void NewProcessForm_Load(object sender, EventArgs e)
  {
    this.PriorityBox.Items.Clear();
    this.PriorityBox.Items.Add((object) new ComboBoxExItem(SimpleFuncs.GetEnumDescription((Enum) ProcessPriority.Low), 2));
    this.PriorityBox.SelectedIndex = this.PriorityBox.Items.Add((object) new ComboBoxExItem(SimpleFuncs.GetEnumDescription((Enum) ProcessPriority.Normal), -1));
    this.PriorityBox.Items.Add((object) new ComboBoxExItem(SimpleFuncs.GetEnumDescription((Enum) ProcessPriority.High), 3));
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    dictionary.Add("schemesView.Width", this.schemesView.Width.ToString());
    FormStorage.LoadLayout((Control) this, (IDictionary) dictionary);
    try
    {
      this.schemesView.Width = Convert.ToInt32(dictionary["schemesView.Width"]);
    }
    catch
    {
    }
    this.showBaseVersion.Text = Holder.ShowOnlyBaseVersionInStartProcess ? "Показать все версии..." : "Показать только базовые версии...";
  }

  public string SchemeName
  {
    get => this._schemeName;
    set
    {
      this._schemeName = value;
      this.Text = this._captionPrefix;
      if (!(value != ""))
        return;
      this.Text = $"{this.Text} - {value}";
    }
  }

  public void FillAdditionalInfos(string caption, string message)
  {
    if (caption != string.Empty)
      this.ProcessNameEdit.Text = caption;
    if (!(message != string.Empty))
      return;
    this.MessageTextEdit.Text = message;
  }

  protected void FillSchemeInfos()
  {
    if (this.SchemeID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject objectBaseVersionById = session.GetObject(this.SchemeID, false);
      if (objectBaseVersionById == null && this.SchemeID < 0L)
      {
        this._schemeID = -this._schemeID;
        objectBaseVersionById = session.GetObject(this.SchemeID, false);
        if (objectBaseVersionById == null)
        {
          this.SchemeID = 0L;
          return;
        }
      }
      IDBAttribute byId = objectBaseVersionById.Attributes.FindByID(wfConsts.AttrIsDebugID);
      if (byId != null)
        this._schemeIsDebug = byId.AsBoolean;
      if (GlobalMailSettings.Cfg.LaunchBaseSchemesOnly && !Holder.IsAdmin && !objectBaseVersionById.IsBaseVersion)
      {
        objectBaseVersionById = session.GetObjectBaseVersionByID(objectBaseVersionById.ID, true);
        this._schemeID = objectBaseVersionById.ObjectID;
      }
      this.ProcessNameEdit.Text = objectBaseVersionById.Caption;
      this.SchemeName = CaptionTransform.GetCaption(this.ProcessNameEdit.Text, (long) objectBaseVersionById.VersionID);
      IDBObject startActivity = (IDBObject) (objectBaseVersionById as IScheme).StartActivity;
      long num = 0;
      if (startActivity != null)
      {
        IDBAttribute attributeById = startActivity.GetAttributeByID(wfConsts.AttrFormID);
        if (attributeById != null)
          num = attributeById.AsInteger;
      }
      this.FormID = num;
      this._schemeStatus = SchemeStatus.Invalid;
      IDBAttribute attributeById1 = objectBaseVersionById.GetAttributeByID(wfConsts.AttrActivityStatusID);
      if (attributeById1 != null)
        this._schemeStatus = (SchemeStatus) attributeById1.AsInteger;
      if (this._formControl == null)
        return;
      this._formControl.Dispose();
      this._formControl = (FormDesignerView) null;
    }
  }

  private long FormID
  {
    get => this._formID;
    set
    {
      this._formID = value;
      if (this._formID == 0L)
      {
        this.Wizard.TabPages.Remove(this.FormPage);
      }
      else
      {
        if (this.Wizard.TabPages.IndexOf(this.FormPage) != -1)
          return;
        this.Wizard.TabPages.Add(this.FormPage);
      }
    }
  }

  private void SchemesSelectedItemsChanged(object sender, EventArgs e)
  {
    if (this._selhost == null)
      return;
    ISelectedItems selectedItems = this._selhost.SelectedItems;
    if (selectedItems.Count > 0 && selectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
      this.SchemeID = itemData.Value;
    else
      this.SchemeID = 0L;
  }

  private void SchemesViewDoubleClick(object sender, EventArgs e) => this.NextBtn.PerformClick();

  private void RefreshSchemes()
  {
    IDescriptor rootDescriptor = this.SchemeRootGroupID == 0L ? (IDescriptor) new TopObjectsDescriptor(Holder.CategorySchemesID, 0, LocalizationHolder.rm.GetString("Workflow.Design_144"), wfConsts.SchemeCategoriesID) : (IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(this.SchemeRootGroupID);
    ServiceContainer serviceContainer = new ServiceContainer();
    serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog));
    serviceContainer.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
    serviceContainer.AddService(typeof (ValidSchemesOnlyFlag), (object) new ValidSchemesOnlyFlag());
    serviceContainer.AddService(typeof (VersionsRule), (object) Holder.AllVersionsRule);
    serviceContainer.AddService(typeof (Form), (object) this);
    this.schemesView.Services = (System.IServiceProvider) serviceContainer;
    this.pageViewsManager.Services = (System.IServiceProvider) serviceContainer;
    this.schemesView.SetColumns(Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    this.schemesView.dontSavePath = this.SchemeRootGroupID != 0L;
    if (this.SchemeRootGroupID != 0L || !wfFunx.RestoreTreePath((NavigatorTreeView) this.schemesView))
      this.schemesView.Build(rootDescriptor);
    this.schemesView.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.schemesView_AfterFocusNode);
    this.schemesView.SelectedItemsChanged += new EventHandler(this.schemesView_SelectedItemsChanged);
    this.pageViewsManager.ActiveViewPageChanged += new EventHandler(this.pageViewsManager_ActiveViewPageChanged);
    IViewPage activeViewPage = this.pageViewsManager.ActiveViewPage;
    if (activeViewPage != null)
    {
      bool flag = false;
      if (activeViewPage.Control is ChildrenView control)
      {
        this._defColumn = control.GetNodeColumns();
        IColumnSchemes service = (IColumnSchemes) ApplicationServices.Container.GetService(typeof (IColumnSchemes));
        if (Holder.IsAdmin)
        {
          Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectColumnSchemeGuid;
          if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_OBJECT_ID))
          {
            this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID));
            flag = true;
          }
          if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.CAPTION))
          {
            this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION));
            flag = true;
          }
          if (!this._defColumn.ColumnIDExists((object) wfConsts.AttrIsDebugID))
          {
            this._defColumn.Add(service.CreateColumn(columnSchemeGuid, (object) wfConsts.AttrIsDebugID));
            flag = true;
          }
          if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_OWNER_ID))
          {
            this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OWNER_ID));
            flag = true;
          }
          if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_CHKOUT_BY))
          {
            this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_CHKOUT_BY));
            flag = true;
          }
        }
        else
        {
          if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_OBJECT_ID))
          {
            this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID));
            flag = true;
          }
          if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.CAPTION))
          {
            this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION));
            flag = true;
          }
          if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_OWNER_ID))
          {
            this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OWNER_ID));
            flag = true;
          }
          if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_CHKOUT_BY))
          {
            this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_CHKOUT_BY));
            flag = true;
          }
        }
        if (flag)
          control.SetColumns(this._defColumn, true);
      }
    }
    this.UpdateSelHost();
    this.SchemesSelectedItemsChanged((object) null, (EventArgs) null);
  }

  private void schemesView_SelectedItemsChanged(object sender, EventArgs e) => this.UpdateSelHost();

  private void schemesView_AfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    IViewPage activeViewPage = this.pageViewsManager.ActiveViewPage;
    if (activeViewPage == null)
      return;
    bool flag = false;
    if (!(activeViewPage.Control is ChildrenView control))
      return;
    this._defColumn = control.GetNodeColumns();
    IColumnSchemes service = (IColumnSchemes) ApplicationServices.Container.GetService(typeof (IColumnSchemes));
    if (Holder.IsAdmin)
    {
      Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectColumnSchemeGuid;
      if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_OBJECT_ID))
      {
        this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID));
        flag = true;
      }
      if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.CAPTION))
      {
        this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION));
        flag = true;
      }
      if (!this._defColumn.ColumnIDExists((object) wfConsts.AttrIsDebugID))
      {
        this._defColumn.Add(service.CreateColumn(columnSchemeGuid, (object) wfConsts.AttrIsDebugID));
        flag = true;
      }
      if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_OWNER_ID))
      {
        this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OWNER_ID));
        flag = true;
      }
      if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_CHKOUT_BY))
      {
        this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_CHKOUT_BY));
        flag = true;
      }
    }
    else
    {
      if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_OBJECT_ID))
      {
        this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID));
        flag = true;
      }
      if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.CAPTION))
      {
        this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION));
        flag = true;
      }
      if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_OWNER_ID))
      {
        this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OWNER_ID));
        flag = true;
      }
      if (!this._defColumn.ColumnIDExists((object) ObligatoryObjectAttributes.F_CHKOUT_BY))
      {
        this._defColumn.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_CHKOUT_BY));
        flag = true;
      }
      if (this._defColumn.ColumnIDExists((object) wfConsts.AttrIsDebugID))
      {
        NodeColumn[] byAttrId = this._defColumn.FindByAttrID(wfConsts.AttrIsDebugID);
        if (byAttrId.Length != 0)
        {
          this._defColumn.Remove(byAttrId[0]);
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    control.SetColumns(this._defColumn, true);
  }

  private void pageViewsManager_ActiveViewPageChanged(object sender, EventArgs e)
  {
  }

  private void UpdateSelHost()
  {
    this._selhost = (ISelectedItemsHost) null;
    if (this.pageViewsManager.ActiveViewPage == null)
      return;
    IView view = this.pageViewsManager.ActiveViewPage.View;
    this._selhost = view as ISelectedItemsHost;
    if (this._selhost != null)
    {
      this._selhost.SelectedItemsChanged -= new EventHandler(this.SchemesSelectedItemsChanged);
      this._selhost.SelectedItemsChanged += new EventHandler(this.SchemesSelectedItemsChanged);
    }
    if (!(view is SchemesView))
      return;
    SchemesView schemesView = view as SchemesView;
    schemesView.DisableDoubleClicks = true;
    schemesView.Grid.DoubleClick -= new EventHandler(this.SchemesViewDoubleClick);
    schemesView.Grid.DoubleClick += new EventHandler(this.SchemesViewDoubleClick);
  }

  public long SchemeID
  {
    get => this._schemeID;
    set
    {
      if (this._schemeID != value)
        this.DeleteProcess();
      this._schemeID = value;
      if (this._schemeID != 0L)
      {
        this.FillSchemeInfos();
        if (!this.Visible && this._schemeID != 0L)
        {
          this.Wizard.TabPages.Remove(this.SchemesPage);
          this.Wizard.SelectedIndex = 0;
          if (this._schemeStatus == SchemeStatus.Invalid)
            throw new HiddenStackException(string.Format(LocalizationHolder.rm.GetString("Workflow.Design_76"), (object) this.SchemeName));
        }
      }
      else
        this.SchemeName = "";
      this.UpdateButtons();
    }
  }

  private void UpdateButtons()
  {
    this.NextBtn.Enabled = this.Wizard.SelectedTab != this.SchemesPage || this._schemeID != 0L;
  }

  protected override void CreateHandle()
  {
    base.CreateHandle();
    this.UpdateButtons();
  }

  public ProcessPriority Priority => (ProcessPriority) (this.PriorityBox.SelectedIndex - 1);

  public long ProcessID => this._createdProcess != null ? this._createdProcess.ObjectID : 0L;

  private void NewProcessForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this.pageViewsManager.ActiveViewPage != null)
      this.pageViewsManager.ActiveViewPage.View.Deactivate((IView) null);
    FormStorage.SaveLayout((Control) this, (IDictionary) new Dictionary<string, string>()
    {
      {
        "schemesView.Width",
        this.schemesView.Width.ToString()
      }
    });
  }

  private void NewProcessForm_VisibleChanged(object sender, EventArgs e)
  {
    if (this._schemeID != 0L || !this.Visible)
      return;
    this.RefreshSchemes();
  }

  private void NewProcessForm_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Escape)
      return;
    this.DialogResult = DialogResult.Cancel;
  }

  internal void DeleteProcess()
  {
    this._createdProcess?.Delete(0L);
    this._createdProcess = (IProcess) null;
  }

  private void CreateProcess()
  {
    if (this._createdProcess != null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._createdProcess = sessionKeeper.Session.GetObjectCollection(wfConsts.ProcessesTypeID).Create(this.SchemeID) as IProcess;
      this._createdProcess.Name = this.ProcessNameEdit.Text;
      this._remoteLock.Add((object) this._createdProcess);
    }
  }

  private void showBaseVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
  {
    ChildrenView control = this.pageViewsManager.ActiveViewPage.Control as ChildrenView;
    if (Holder.ShowOnlyBaseVersionInStartProcess)
    {
      Holder.ShowOnlyBaseVersionInStartProcess = false;
      this.showBaseVersion.Text = "Показать только базовые версии...";
    }
    else
    {
      Holder.ShowOnlyBaseVersionInStartProcess = true;
      this.showBaseVersion.Text = "Показать все версии...";
    }
    int? count = new int?();
    control.ReloadItems(count);
  }
}
