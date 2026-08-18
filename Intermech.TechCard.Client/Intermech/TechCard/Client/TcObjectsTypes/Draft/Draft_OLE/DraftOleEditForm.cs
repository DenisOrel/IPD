// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE.DraftOleEditForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Draft.Draft_OLE;

/// <summary>Форма редактирования OLE эскизов</summary>
/// <summary>
/// 
/// </summary>
public class DraftOleEditForm : Form
{
  /// <summary>Наименование эскиза</summary>
  private string _draftName = "";
  /// <summary>
  /// 
  /// </summary>
  private DialogResult _dialogResult = DialogResult.OK;
  /// <summary>OLE контрол</summary>
  private DraftOleControl _oleControl;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private MenuStrip msDraft;
  private ToolStripMenuItem tsmiFile;
  private ToolStripMenuItem tsmiOpenEditor;
  private ToolStripSeparator tsmiSep;
  private ToolStripMenuItem tsmiExit;
  private ContextMenuStrip contextMenuStrip;
  private ToolStripMenuItem ctsmiEdit;
  private ToolStripSeparator ctsmiSep;
  private ToolStripMenuItem tsmiMode;
  private ToolStripMenuItem tsmiModeClip;
  private ToolStripMenuItem tsmiModeStretch;
  private ToolStripMenuItem tsmiModeZoom;

  /// <summary>Инициализация параметров класса</summary>
  private void InitializeData()
  {
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      this.msDraft.ImageList = service.ImageList;
      this.contextMenuStrip.ImageList = service.ImageList;
      this.ctsmiEdit.ImageIndex = service.ImageIndex("imgDesktopObjectType");
      this.tsmiOpenEditor.Image = service.ImageList.Images[service.ImageIndex("imgDesktopObjectType")];
      this.tsmiModeClip.Image = service.ImageList.Images[service.ImageIndex("imgZoom1to1")];
      this.tsmiModeZoom.Image = service.ImageList.Images[service.ImageIndex("imgZoomAll")];
    }
    this.tsmiModeClip.Tag = (object) PictureBoxSizeMode.Normal;
    this.tsmiModeZoom.Tag = (object) PictureBoxSizeMode.Zoom;
    this.tsmiModeStretch.Tag = (object) PictureBoxSizeMode.StretchImage;
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    if (this._oleControl == null)
      return;
    this._oleControl.ReadOnly = false;
    this._oleControl.Parent = (Control) this;
    this._oleControl.Dock = DockStyle.Fill;
    this._oleControl.BringToFront();
    this._oleControl.Name = "oleControl";
    this._oleControl.ContextMenuStrip = this.contextMenuStrip;
  }

  /// <summary>Конструктор</summary>
  public DraftOleEditForm()
  {
    this._oleControl = new DraftOleControl();
    this.InitializeComponent();
    this.InitializeCustomControls();
    this.InitializeData();
  }

  /// <summary>Вызов редактора</summary>
  public void OpenEditor() => this._oleControl.OpenEditor();

  /// <summary>
  /// 
  /// </summary>
  public bool CreateObject() => this._oleControl.CallInsertDlg();

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._oleControl != null)
      {
        this._oleControl.Parent = (Control) null;
        this._oleControl.Dispose();
      }
      this._oleControl = (DraftOleControl) null;
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Наименование эскиза</summary>
  public string DraftName
  {
    get => this._draftName;
    set
    {
      this._draftName = value;
      this.Text = string.Format(LocalizationHolder.rm.GetString("TechCard.Client_179"), (object) value);
    }
  }

  /// <summary>Stream OLE объекта</summary>
  internal Stream OleStream
  {
    get => this._oleControl.OleStream;
    set => this._oleControl.OleStream = value;
  }

  /// <summary>Загрузка редактора при показе формы</summary>
  public bool NeedOpenEditor { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public bool NeedCreateObject { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiExit_Click(object sender, EventArgs e)
  {
    this.DialogResult = this._dialogResult;
    this.Close();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DraftOleEditForm_Activated(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiFile_Click(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiOpenEditor_Click(object sender, EventArgs e) => this.OpenEditor();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DraftOleEditForm_Shown(object sender, EventArgs e)
  {
    if (this.NeedCreateObject && !this.CreateObject())
    {
      this._dialogResult = DialogResult.Cancel;
    }
    else
    {
      if (!this.NeedOpenEditor)
        return;
      this.OpenEditor();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiEdit1_Click(object sender, EventArgs e) => this.OpenEditor();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ctsmiMode_Click(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiMode_SetClick(object sender, EventArgs e)
  {
    if (!(sender is ToolStripMenuItem toolStripMenuItem))
      return;
    this._oleControl.SizeMode = (PictureBoxSizeMode) toolStripMenuItem.Tag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DraftOleEditForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.Modal)
      return;
    this.DialogResult = this._dialogResult;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ctsmiMode_DropDownOpening(object sender, EventArgs e)
  {
    foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection) this.tsmiMode.DropDownItems)
      dropDownItem.Checked = (PictureBoxSizeMode) dropDownItem.Tag == this._oleControl.SizeMode;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DraftOleEditForm));
    this.msDraft = new MenuStrip();
    this.tsmiFile = new ToolStripMenuItem();
    this.tsmiOpenEditor = new ToolStripMenuItem();
    this.tsmiSep = new ToolStripSeparator();
    this.tsmiExit = new ToolStripMenuItem();
    this.tsmiMode = new ToolStripMenuItem();
    this.tsmiModeClip = new ToolStripMenuItem();
    this.tsmiModeStretch = new ToolStripMenuItem();
    this.tsmiModeZoom = new ToolStripMenuItem();
    this.contextMenuStrip = new ContextMenuStrip(this.components);
    this.ctsmiEdit = new ToolStripMenuItem();
    this.ctsmiSep = new ToolStripSeparator();
    this.msDraft.SuspendLayout();
    this.contextMenuStrip.SuspendLayout();
    this.SuspendLayout();
    this.msDraft.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiFile,
      (ToolStripItem) this.tsmiMode
    });
    componentResourceManager.ApplyResources((object) this.msDraft, "msDraft");
    this.msDraft.Name = "msDraft";
    this.tsmiFile.DropDownItems.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tsmiOpenEditor,
      (ToolStripItem) this.tsmiSep,
      (ToolStripItem) this.tsmiExit
    });
    this.tsmiFile.Name = "tsmiFile";
    componentResourceManager.ApplyResources((object) this.tsmiFile, "tsmiFile");
    this.tsmiFile.Click += new EventHandler(this.tsmiFile_Click);
    this.tsmiOpenEditor.Name = "tsmiOpenEditor";
    componentResourceManager.ApplyResources((object) this.tsmiOpenEditor, "tsmiOpenEditor");
    this.tsmiOpenEditor.Click += new EventHandler(this.tsmiOpenEditor_Click);
    this.tsmiSep.Name = "tsmiSep";
    componentResourceManager.ApplyResources((object) this.tsmiSep, "tsmiSep");
    this.tsmiExit.Name = "tsmiExit";
    componentResourceManager.ApplyResources((object) this.tsmiExit, "tsmiExit");
    this.tsmiExit.Click += new EventHandler(this.tsmiExit_Click);
    this.tsmiMode.DropDownItems.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tsmiModeClip,
      (ToolStripItem) this.tsmiModeStretch,
      (ToolStripItem) this.tsmiModeZoom
    });
    this.tsmiMode.Name = "tsmiMode";
    componentResourceManager.ApplyResources((object) this.tsmiMode, "tsmiMode");
    this.tsmiMode.DropDownOpening += new EventHandler(this.ctsmiMode_DropDownOpening);
    this.tsmiMode.Click += new EventHandler(this.ctsmiMode_Click);
    this.tsmiModeClip.CheckOnClick = true;
    this.tsmiModeClip.Name = "tsmiModeClip";
    componentResourceManager.ApplyResources((object) this.tsmiModeClip, "tsmiModeClip");
    this.tsmiModeClip.Click += new EventHandler(this.tsmiMode_SetClick);
    this.tsmiModeStretch.CheckOnClick = true;
    this.tsmiModeStretch.Name = "tsmiModeStretch";
    componentResourceManager.ApplyResources((object) this.tsmiModeStretch, "tsmiModeStretch");
    this.tsmiModeStretch.Click += new EventHandler(this.tsmiMode_SetClick);
    this.tsmiModeZoom.CheckOnClick = true;
    this.tsmiModeZoom.Name = "tsmiModeZoom";
    componentResourceManager.ApplyResources((object) this.tsmiModeZoom, "tsmiModeZoom");
    this.tsmiModeZoom.Click += new EventHandler(this.tsmiMode_SetClick);
    this.contextMenuStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.ctsmiEdit,
      (ToolStripItem) this.ctsmiSep
    });
    this.contextMenuStrip.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip, "contextMenuStrip");
    this.ctsmiEdit.Name = "ctsmiEdit";
    componentResourceManager.ApplyResources((object) this.ctsmiEdit, "ctsmiEdit");
    this.ctsmiEdit.Click += new EventHandler(this.tsmiEdit1_Click);
    this.ctsmiSep.Name = "ctsmiSep";
    componentResourceManager.ApplyResources((object) this.ctsmiSep, "ctsmiSep");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.msDraft);
    this.Name = nameof (DraftOleEditForm);
    this.Tag = (object) " ";
    this.Activated += new EventHandler(this.DraftOleEditForm_Activated);
    this.FormClosing += new FormClosingEventHandler(this.DraftOleEditForm_FormClosing);
    this.Shown += new EventHandler(this.DraftOleEditForm_Shown);
    this.msDraft.ResumeLayout(false);
    this.msDraft.PerformLayout();
    this.contextMenuStrip.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
