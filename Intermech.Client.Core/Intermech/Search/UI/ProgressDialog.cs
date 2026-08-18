
// Type: Intermech.Search.UI.ProgressDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Search.UI;

/// <summary>Диалог с прогресс баром, надписью и кнопкой</summary>
public class ProgressDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ProgressBar _progressBar;
  private Button _button;
  private TableLayoutPanel tableLayoutPanel1;
  private Label _label;

  /// <summary>Конструктор</summary>
  public ProgressDialog() => this.InitializeComponent();

  /// <summary>Максимальное значение прогресс бара</summary>
  public int Maximum
  {
    get => this._progressBar.Maximum;
    set => this._progressBar.Maximum = value;
  }

  /// <summary>Минимальное значение прогресс бара</summary>
  public int Minimum
  {
    get => this._progressBar.Minimum;
    set => this._progressBar.Minimum = value;
  }

  /// <summary>Шаг прогресс бара</summary>
  public int Step
  {
    get => this._progressBar.Step;
    set => this._progressBar.Step = value;
  }

  /// <summary>Стиль прогресс бара</summary>
  public ProgressBarStyle Style
  {
    get => this._progressBar.Style;
    set => this._progressBar.Style = value;
  }

  /// <summary>Событие клик по кнопке</summary>
  public event EventHandler ButtonClick;

  /// <summary>Текст кнопки</summary>
  public string ButtonText
  {
    get => this._button.Text;
    set => this._button.Text = value;
  }

  /// <summary>Текст надписи</summary>
  public string LabelText
  {
    get => this._label.Text;
    set => this._label.Text = value;
  }

  private void ProgressDialog_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void ProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void Button_Click(object sender, EventArgs e) => this.OnButtonClick();

  private void OnButtonClick()
  {
    EventHandler buttonClick = this.ButtonClick;
    if (buttonClick == null)
      return;
    buttonClick((object) this, new EventArgs());
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProgressDialog));
    this._progressBar = new ProgressBar();
    this._button = new Button();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this._label = new Label();
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._progressBar, "_progressBar");
    this._progressBar.Name = "_progressBar";
    this._button.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this._button, "_button");
    this._button.Name = "_button";
    this._button.UseVisualStyleBackColor = true;
    this._button.Click += new EventHandler(this.Button_Click);
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this._label, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._button, 1, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this._progressBar, 0, 1);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    componentResourceManager.ApplyResources((object) this._label, "_label");
    this.tableLayoutPanel1.SetColumnSpan((Control) this._label, 2);
    this._label.Name = "_label";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._button;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ProgressDialog);
    this.ShowIcon = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.TopMost = true;
    this.FormClosing += new FormClosingEventHandler(this.ProgressDialog_FormClosing);
    this.Load += new EventHandler(this.ProgressDialog_Load);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
