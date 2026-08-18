// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.UI.EditFirstPageNumberForm
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.UI;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.UI;

/// <summary> Диалог редактирования заголовка исполнения </summary>
public class EditFirstPageNumberForm : Form
{
  private string _productNumber = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOK;
  private Label label1;
  private Bevel bevel1;
  private NumericUpDown numericUpDown;

  public EditFirstPageNumberForm(int pageNumber)
  {
    this.InitializeComponent();
    this.PageNumber = pageNumber;
  }

  /// <summary> Номер страницы </summary>
  public int PageNumber
  {
    get => (int) this.numericUpDown.Value;
    set => this.numericUpDown.Value = (Decimal) value;
  }

  private void EditProductCaptionForm_Shown(object sender, EventArgs e)
  {
    this.numericUpDown.Focus();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditFirstPageNumberForm));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this.label1 = new Label();
    this.bevel1 = new Bevel();
    this.numericUpDown = new NumericUpDown();
    this.numericUpDown.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.bevel1, "bevel1");
    this.bevel1.Name = "bevel1";
    componentResourceManager.ApplyResources((object) this.numericUpDown, "numericUpDown");
    this.numericUpDown.Maximum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      0
    });
    this.numericUpDown.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDown.Name = "numericUpDown";
    this.numericUpDown.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this.numericUpDown);
    this.Controls.Add((Control) this.bevel1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditFirstPageNumberForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Shown += new EventHandler(this.EditProductCaptionForm_Shown);
    this.numericUpDown.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
