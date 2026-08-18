// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SelectAVSDocumentForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.AVS;

public class SelectAVSDocumentForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOk;
  public RadioButton rbSingle;
  public RadioButton rbGroupA;
  public RadioButton rbGroupB;
  public RadioButton rbMirror;
  public RadioButton rbGroupV;
  public RadioButton rbGroupG;

  public SelectAVSDocumentForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1515);
  }

  public SelectAVSDocumentForm(AVSDocumentForm _specificationForm, AVSDocumentType docType)
  {
    this.InitializeComponent();
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control != null && control is RadioButton && control.Tag != null && (AVSDocumentForm) Convert.ToInt32(control.Tag) == _specificationForm)
        ((RadioButton) control).Checked = true;
    }
    List<AVSDocumentForm> avsDocumentFormList = new List<AVSDocumentForm>((IEnumerable<AVSDocumentForm>) AVSDocumentsSettings.GetAllowableDocumentForm(docType));
    this.rbGroupA.Enabled = avsDocumentFormList.Contains(AVSDocumentForm.A);
    this.rbGroupB.Enabled = avsDocumentFormList.Contains(AVSDocumentForm.B);
    this.rbMirror.Enabled = avsDocumentFormList.Contains(AVSDocumentForm.Mirror);
    this.rbGroupV.Enabled = avsDocumentFormList.Contains(AVSDocumentForm.V);
    this.rbGroupG.Enabled = avsDocumentFormList.Contains(AVSDocumentForm.G);
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1515);
  }

  public AVSDocumentForm SelectedSpecificationForm
  {
    get
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control != null && control is RadioButton && ((RadioButton) control).Checked && control.Tag != null)
          return (AVSDocumentForm) Convert.ToInt32(control.Tag);
      }
      return AVSDocumentForm.Single;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectAVSDocumentForm));
    this._btnCancel = new Button();
    this._btnOk = new Button();
    this.rbSingle = new RadioButton();
    this.rbGroupA = new RadioButton();
    this.rbGroupB = new RadioButton();
    this.rbMirror = new RadioButton();
    this.rbGroupV = new RadioButton();
    this.rbGroupG = new RadioButton();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOk, "_btnOk");
    this._btnOk.DialogResult = DialogResult.OK;
    this._btnOk.Name = "_btnOk";
    this._btnOk.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbSingle, "rbSingle");
    this.rbSingle.Checked = true;
    this.rbSingle.Name = "rbSingle";
    this.rbSingle.TabStop = true;
    this.rbSingle.Tag = (object) "0";
    this.rbSingle.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbGroupA, "rbGroupA");
    this.rbGroupA.Name = "rbGroupA";
    this.rbGroupA.Tag = (object) "1";
    this.rbGroupA.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbGroupB, "rbGroupB");
    this.rbGroupB.Name = "rbGroupB";
    this.rbGroupB.Tag = (object) "2";
    this.rbGroupB.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbMirror, "rbMirror");
    this.rbMirror.Name = "rbMirror";
    this.rbMirror.Tag = (object) "3";
    this.rbMirror.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbGroupV, "rbGroupV");
    this.rbGroupV.Name = "rbGroupV";
    this.rbGroupV.Tag = (object) "4";
    this.rbGroupV.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rbGroupG, "rbGroupG");
    this.rbGroupG.Name = "rbGroupG";
    this.rbGroupG.Tag = (object) "5";
    this.rbGroupG.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this._btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this.rbGroupG);
    this.Controls.Add((Control) this.rbGroupV);
    this.Controls.Add((Control) this.rbMirror);
    this.Controls.Add((Control) this.rbGroupB);
    this.Controls.Add((Control) this.rbGroupA);
    this.Controls.Add((Control) this.rbSingle);
    this.Controls.Add((Control) this._btnOk);
    this.Controls.Add((Control) this._btnCancel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = "SelectSpecificationForm";
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
