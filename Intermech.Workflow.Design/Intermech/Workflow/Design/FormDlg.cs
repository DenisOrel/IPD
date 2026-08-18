// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.FormDlg
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Client.Core;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Controls;
using Intermech.Interfaces.Workflow;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>
/// 
/// </summary>
public class FormDlg : FormEx
{
  private bool _editMode;
  private FormDesignerView _formControl;
  private Size _minimumSize = new Size(250, 250);
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel bottomPanel;
  private Button okButton;
  private Label label1;

  /// <summary>Конструктор.</summary>
  public FormDlg(bool isBack)
  {
    this.InitializeComponent();
    this.okButton.ImageList = BaseHolder.NamedList.ImageList;
    this.okButton.ImageIndex = BaseHolder.NamedList.ImageIndex(isBack ? "wfBack" : "wfNext");
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Конструктор.</summary>
  /// <param name="objid"></param>
  /// <param name="formid"></param>
  /// <param name="editMode"></param>
  public FormDlg(long objid, long formid, bool editMode, bool isBack)
    : this(isBack)
  {
    this._editMode = editMode;
    this.bottomPanel.Visible = editMode;
    this._formControl = new FormDesignerView(objid, formid);
    this._formControl.Parent = (Control) this;
    this._formControl.LoadForm();
    if (this._formControl.MinimumSize.Height > 0 || this._formControl.MinimumSize.Width > 0)
    {
      Size minimumSize = this._formControl.MinimumSize;
      int width = minimumSize.Width + 10;
      minimumSize = this._formControl.MinimumSize;
      int height = minimumSize.Height + this.bottomPanel.Height * 2;
      this._minimumSize = new Size(width, height);
    }
    this._formControl.Dock = DockStyle.Fill;
    this._formControl.BringToFront();
    this.ActiveControl = (Control) this._formControl;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objid"></param>
  /// <param name="formid"></param>
  public static void ViewForm(long objid, long formid)
  {
    using (FormDlg formDlg = new FormDlg(objid, formid, false, false))
    {
      formDlg.Text = LocalizationHolder.rm.GetString(sc_21814.ssp_workflow_21815());
      int num = (int) formDlg.ShowDialog();
    }
  }

  public static void ViewForm(long objid, long formid, bool editMode)
  {
    using (FormDlg formDlg = new FormDlg(objid, formid, editMode, false))
    {
      formDlg.okButton.Text = "Сохранить";
      formDlg.okButton.ImageList = (ImageList) null;
      formDlg.okButton.TextAlign = ContentAlignment.MiddleCenter;
      formDlg.Text = LocalizationHolder.rm.GetString(sc_21814.ssp_workflow_21816());
      int num = (int) formDlg.ShowDialog();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objid"></param>
  /// <param name="formid"></param>
  /// <returns></returns>
  public static bool EditForm(long objid, long formid, bool isBack)
  {
    using (FormDlg formDlg = new FormDlg(objid, formid, true, isBack))
      return formDlg.ShowDialog() == DialogResult.OK;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormDlg_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormDlg_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.DialogResult != DialogResult.OK)
      return;
    try
    {
      if (!this._editMode)
        return;
      this._formControl.SaveForm();
    }
    catch (Exception ex)
    {
      wfFunx.SayError(ex.Message);
      e.Cancel = true;
    }
  }

  public override Size MinimumSize => this._minimumSize;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormDlg));
    this.bottomPanel = new Panel();
    this.okButton = new Button();
    this.label1 = new Label();
    this.bottomPanel.SuspendLayout();
    this.SuspendLayout();
    this.bottomPanel.Controls.Add((Control) this.okButton);
    this.bottomPanel.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.bottomPanel, "bottomPanel");
    this.bottomPanel.Name = "bottomPanel";
    componentResourceManager.ApplyResources((object) this.okButton, "okButton");
    this.okButton.DialogResult = DialogResult.OK;
    this.okButton.Name = "okButton";
    this.okButton.UseVisualStyleBackColor = true;
    this.label1.BorderStyle = BorderStyle.Fixed3D;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.AcceptButton = (IButtonControl) this.okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.bottomPanel);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormDlg);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.FormClosed += new FormClosedEventHandler(this.FormDlg_FormClosed);
    this.FormClosing += new FormClosingEventHandler(this.FormDlg_FormClosing);
    this.bottomPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
