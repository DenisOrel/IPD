
// Type: Intermech.Navigator.Conditions.TextBoxButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

public class TextBoxButton : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Button bOpenDialog;
  protected MaskedTextBox tbText;

  /// <summary>
  /// Событие, возникает при нажатии на кнопку открытия диалога
  /// </summary>
  public event OnOpenDialogEventHandler OnOpenDialog;

  public event EventHandler OnDeleteKey;

  /// <summary>Пока сделано так</summary>
  public bool ValueChangedFromDialog { get; protected set; }

  /// <summary>
  /// Конструктор по умолчанию, нужен для добавления контрола из toolbox-a
  /// </summary>
  public TextBoxButton() => this.InitializeComponent();

  /// <summary>Конструктор</summary>
  /// <param name="textBoxReadOnly">Текст в тектовом поле нельзя редактировать</param>
  /// <param name="initText"></param>
  public TextBoxButton(bool textBoxReadOnly, string initText)
    : this()
  {
    this.tbText.ReadOnly = textBoxReadOnly;
    this.tbText.BackColor = SystemColors.Window;
    this.tbText.Text = initText;
  }

  private void OnOpenDialog_Click(object sender, EventArgs e)
  {
    this.OpenDialog_Click((object) e, new OnOpenDialogEventArgs()
    {
      Multiselect = false
    });
  }

  public virtual void OpenDialog_Click(object sender, OnOpenDialogEventArgs e)
  {
    this.ValueChangedFromDialog = this.OnOpenDialog != null && this.OnOpenDialog((object) this, e);
  }

  private void OnSetText(string text) => this.tbText.Text = text;

  /// <summary>Установить текст в текстовом поле</summary>
  /// <param name="text"></param>
  public void SetText(string text) => this.OnSetText(text);

  /// <summary>Получить текст из textbox</summary>
  /// <returns></returns>
  public override string Text
  {
    get => this.tbText.Text;
    set => this.tbText.Text = value;
  }

  private void Text_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete)
      return;
    EventHandler onDeleteKey = this.OnDeleteKey;
    if (onDeleteKey == null)
      return;
    onDeleteKey((object) this, new EventArgs());
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TextBoxButton));
    this.bOpenDialog = new Button();
    this.tbText = new MaskedTextBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.bOpenDialog, "bOpenDialog");
    this.bOpenDialog.Name = "bOpenDialog";
    this.bOpenDialog.UseVisualStyleBackColor = true;
    this.bOpenDialog.Click += new EventHandler(this.OnOpenDialog_Click);
    componentResourceManager.ApplyResources((object) this.tbText, "tbText");
    this.tbText.BackColor = SystemColors.Window;
    this.tbText.Name = "tbText";
    this.tbText.KeyDown += new KeyEventHandler(this.Text_KeyDown);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tbText);
    this.Controls.Add((Control) this.bOpenDialog);
    this.Name = nameof (TextBoxButton);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private delegate void SetTextHandler(string text);
}
