// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.EditProductCaptionForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Common_Dialogs;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Диалог редактирования заголовка исполнения </summary>
public class EditProductCaptionForm : BaseProductInfoDlg
{
  private string _productNumber = string.Empty;
  private int _lockUpdateVisualControlsCounter;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOK;
  private Label label1;
  private TextBox _textBoxMainPart;
  private TextBox _textBoxNumber;
  private Label label2;

  public EditProductCaptionForm() => this.InitializeComponent();

  /// <summary>Основная часть обозначения исполнения, без номера</summary>
  public string ProductDesignationBase
  {
    get => this._textBoxMainPart.Text;
    set
    {
      this.originDesignation = value;
      this._textBoxMainPart.Text = value;
    }
  }

  /// <summary>Номера исполнения в обозначении</summary>
  public override string ProductNumber
  {
    get => this._textBoxNumber.Text.Replace("-", string.Empty).Trim();
    set
    {
      if (value == "-")
        value = "";
      this.originNumber = value;
      this._textBoxNumber.Text = value;
    }
  }

  /// <summary>Полное обозначение исполнения</summary>
  public override string ProductCaption
  {
    get
    {
      return !string.IsNullOrWhiteSpace(this.ProductNumber) ? $"{this._textBoxMainPart.Text.Trim()}-{this.ProductNumber}" : this._textBoxMainPart.Text.Trim();
    }
  }

  public override string ProductDesignation
  {
    get => this.ProductDesignationBase;
    set
    {
      if (!string.IsNullOrEmpty(this.ProductNumber) && !string.IsNullOrWhiteSpace(value))
      {
        int startIndex = value.LastIndexOf(this.ProductNumber);
        if (startIndex != -1)
          value = value.Remove(startIndex).TrimEnd();
        if (value[value.Length - 1] == '-')
          value = value.Remove(value.Length - 1).TrimEnd();
      }
      this.ProductDesignationBase = value ?? "";
    }
  }

  /// <summary> Обновить состояние визуальных котролов  </summary>
  private void UpdateVisualControlsState()
  {
    if (this._lockUpdateVisualControlsCounter != 0)
      return;
    ++this._lockUpdateVisualControlsCounter;
    try
    {
      this._btnOK.Enabled = this._textBoxMainPart.Text != "" && this._textBoxNumber.Text != "";
    }
    finally
    {
      --this._lockUpdateVisualControlsCounter;
    }
  }

  /// <summary> Заблокировать обновление визуальных контролов </summary>
  private void LockUpdateVisualControls() => ++this._lockUpdateVisualControlsCounter;

  /// <summary> Разблокировать обновление визуальных контролов </summary>
  private void UnlockUpdateVisualControls() => this.UnlockUpdateVisualControls(true);

  /// <summary> Разблокировать обновление визуальных контролов </summary>
  private void UnlockUpdateVisualControls(bool updateControlsIfUnlocked)
  {
    if (this._lockUpdateVisualControlsCounter == 0)
    {
      if (!updateControlsIfUnlocked)
        return;
      this.UpdateVisualControlsState();
    }
    else
    {
      --this._lockUpdateVisualControlsCounter;
      if (this._lockUpdateVisualControlsCounter != 0 || !updateControlsIfUnlocked)
        return;
      this.UpdateVisualControlsState();
    }
  }

  /// <summary>  </summary>
  private void _textBoxMainPart_TextChanged(object sender, EventArgs e)
  {
    this.UpdateVisualControlsState();
  }

  /// <summary>  </summary>
  private void _textBoxNumber_TextChanged(object sender, EventArgs e)
  {
    this.UpdateVisualControlsState();
  }

  private void EditProductCaptionForm_Shown(object sender, EventArgs e)
  {
    if (this._productNumber.Trim().Equals(string.Empty))
      this._textBoxMainPart.Focus();
    else
      this._textBoxNumber.Focus();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditProductCaptionForm));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this.label1 = new Label();
    this._textBoxMainPart = new TextBox();
    this._textBoxNumber = new TextBox();
    this.label2 = new Label();
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
    componentResourceManager.ApplyResources((object) this._textBoxMainPart, "_textBoxMainPart");
    this._textBoxMainPart.Name = "_textBoxMainPart";
    this._textBoxMainPart.TextChanged += new EventHandler(this._textBoxMainPart_TextChanged);
    componentResourceManager.ApplyResources((object) this._textBoxNumber, "_textBoxNumber");
    this._textBoxNumber.Name = "_textBoxNumber";
    this._textBoxNumber.TextChanged += new EventHandler(this._textBoxNumber_TextChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this._textBoxNumber);
    this.Controls.Add((Control) this._textBoxMainPart);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditProductCaptionForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Shown += new EventHandler(this.EditProductCaptionForm_Shown);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
