// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.CreateMultiplyProductsForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Диалог редактирования заголовка исполнения </summary>
public class CreateMultiplyProductsForm : Form
{
  private string _productNumber = string.Empty;
  private int _lockUpdateVisualControlsCounter;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOK;
  private Label label1;
  private TextBox _textBoxMainPart;
  private TextBox startNumber;
  private Label label2;
  private TextBox endNumber;

  public CreateMultiplyProductsForm() => this.InitializeComponent();

  protected override void OnClosing(CancelEventArgs e)
  {
    if (this.DialogResult == DialogResult.OK)
    {
      string productNumber = this.ProductNumber;
      string endProductNumber = this.EndProductNumber;
      string empty = string.Empty;
      int result;
      ref int local = ref result;
      int num1;
      if (int.TryParse(productNumber, out local))
      {
        num1 = result;
      }
      else
      {
        empty += "Начальный индекс не является числом; ";
        num1 = -1;
      }
      int num2;
      if (int.TryParse(endProductNumber, out result))
      {
        num2 = result;
      }
      else
      {
        empty += "Конечный индекс не является числом; ";
        num2 = -1;
      }
      if (num2 < num1 && empty == string.Empty)
        empty += "Конечный индекс меньше начального; ";
      if (empty != string.Empty)
      {
        int num3 = (int) MessageBox.Show(string.Format(empty, (object) "Создание исполнения", (object) MessageBoxButtons.OK, (object) MessageBoxIcon.Hand));
        e.Cancel = true;
      }
    }
    base.OnClosing(e);
  }

  public string GetDesignation(string basePart, string startNumber)
  {
    return !startNumber.Trim().Equals(string.Empty) ? $"{basePart.Trim()}-{startNumber.Trim()}" : basePart.Trim();
  }

  /// <summary> Заголовок исполнения </summary>
  public string ProductBaseCaption
  {
    get => this._textBoxMainPart.Text.Trim();
    set
    {
      this.LockUpdateVisualControls();
      try
      {
        this._textBoxMainPart.Text = value;
      }
      finally
      {
        this.UnlockUpdateVisualControls();
      }
    }
  }

  /// <summary> Номер исполнения </summary>
  public string ProductNumber
  {
    get => this.startNumber.Text;
    set => this.startNumber.Text = value;
  }

  /// <summary> Номер исполнения </summary>
  public string EndProductNumber
  {
    get => this.endNumber.Text;
    set => this.endNumber.Text = value;
  }

  /// <summary> Номер исполнения </summary>
  public int ProductNumberValue
  {
    get
    {
      int result;
      return !int.TryParse(this.ProductNumber, out result) ? -1 : result;
    }
  }

  /// <summary> Номер исполнения </summary>
  public int EndProductNumberValue
  {
    get
    {
      int result;
      return !int.TryParse(this.EndProductNumber, out result) ? -1 : result;
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
      this._btnOK.Enabled = this._textBoxMainPart.Text != "" && this.startNumber.Text != "" && this.endNumber.Text != "";
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
      this.startNumber.Focus();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CreateMultiplyProductsForm));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this.label1 = new Label();
    this._textBoxMainPart = new TextBox();
    this.startNumber = new TextBox();
    this.label2 = new Label();
    this.endNumber = new TextBox();
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
    componentResourceManager.ApplyResources((object) this.startNumber, "startNumber");
    this.startNumber.Name = "startNumber";
    this.startNumber.TextChanged += new EventHandler(this._textBoxNumber_TextChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.endNumber, "endNumber");
    this.endNumber.Name = "endNumber";
    this.endNumber.TextChanged += new EventHandler(this._textBoxNumber_TextChanged);
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCancel;
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.endNumber);
    this.Controls.Add((Control) this.startNumber);
    this.Controls.Add((Control) this._textBoxMainPart);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CreateMultiplyProductsForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Shown += new EventHandler(this.EditProductCaptionForm_Shown);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
