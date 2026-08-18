
// Type: Intermech.Tools.CommonTasks.StandaloneViewOptionsEditorWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Mvp;
using Intermech.Mvp.Components;
using Intermech.Mvp.Winforms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Tools.CommonTasks;

/// <summary>
/// Реализация вида MVP для редактора опций просмотра по команде "Смотреть...".
/// </summary>
internal class StandaloneViewOptionsEditorWindow : 
  MvpWindow,
  IStandaloneViewOptionsEditorView,
  IView,
  IOperationConfirmationView
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox cbInjectSigns;
  private CheckBox cbInjectFileChecksum;
  private CheckBox cbInjectAttributes;
  private Button btOK;
  private CheckBox cbInjectSignNamesOnly;

  public StandaloneViewOptionsEditorWindow() => this.InitializeComponent();

  /// <summary>Значение переключателя "Разрешить запись подписей"</summary>
  public bool EnableInjectSigns
  {
    get => this.cbInjectSigns.Checked;
    set => this.cbInjectSigns.Checked = value;
  }

  /// <summary>
  /// Значение переключателя "Записывать только фамилию подписавшего"
  /// </summary>
  public bool InjectSignNamesOnly
  {
    get => this.cbInjectSignNamesOnly.Checked;
    set => this.cbInjectSignNamesOnly.Checked = value;
  }

  /// <summary>
  /// Значение переключателя "Разрешить запись контрольной суммы"
  /// </summary>
  public bool EnableInjectFileChecksum
  {
    get => this.cbInjectFileChecksum.Checked;
    set => this.cbInjectFileChecksum.Checked = value;
  }

  /// <summary>
  /// Значение переключателя "Разрешить запись атрибутов объекта"
  /// </summary>
  public bool EnableInjectAttributes
  {
    get => this.cbInjectAttributes.Checked;
    set => this.cbInjectAttributes.Checked = value;
  }

  /// <summary>
  /// Событие успешного подтвержения сделанных изменений или своего выбора пользователем.
  /// После этого события взаимодействие пользователя с видом заканчивается.
  /// </summary>
  public event EventHandler OperationConfirmed;

  private void cbInjectSigns_CheckedChanged(object sender, EventArgs e)
  {
    this.cbInjectSignNamesOnly.Enabled = this.cbInjectSigns.Checked;
  }

  private void btOK_Click(object sender, EventArgs e)
  {
    if (this.OperationConfirmed == null)
      return;
    this.OperationConfirmed((object) this, EventArgs.Empty);
  }

  private void Window_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.Handled || e.KeyCode != Keys.Escape)
      return;
    e.Handled = true;
    this.Close();
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
    this.cbInjectSigns = new CheckBox();
    this.cbInjectFileChecksum = new CheckBox();
    this.cbInjectAttributes = new CheckBox();
    this.btOK = new Button();
    this.cbInjectSignNamesOnly = new CheckBox();
    this.SuspendLayout();
    this.cbInjectSigns.AutoSize = true;
    this.cbInjectSigns.Location = new Point(11, 19);
    this.cbInjectSigns.Margin = new Padding(3, 3, 3, 8);
    this.cbInjectSigns.Name = "cbInjectSigns";
    this.cbInjectSigns.Size = new Size(249, 17);
    this.cbInjectSigns.TabIndex = 0;
    this.cbInjectSigns.Text = "Разрешить запись подписи объекта в файл";
    this.cbInjectSigns.UseVisualStyleBackColor = true;
    this.cbInjectSigns.CheckedChanged += new EventHandler(this.cbInjectSigns_CheckedChanged);
    this.cbInjectFileChecksum.AutoSize = true;
    this.cbInjectFileChecksum.Location = new Point(11, 75);
    this.cbInjectFileChecksum.Margin = new Padding(3, 3, 3, 8);
    this.cbInjectFileChecksum.Name = "cbInjectFileChecksum";
    this.cbInjectFileChecksum.Size = new Size(265, 17);
    this.cbInjectFileChecksum.TabIndex = 2;
    this.cbInjectFileChecksum.Text = "Разрешить запись контрольной суммы в файл";
    this.cbInjectFileChecksum.UseVisualStyleBackColor = true;
    this.cbInjectAttributes.AutoSize = true;
    this.cbInjectAttributes.Location = new Point(11, 103);
    this.cbInjectAttributes.Margin = new Padding(3, 3, 3, 8);
    this.cbInjectAttributes.Name = "cbInjectAttributes";
    this.cbInjectAttributes.Size = new Size(258, 17);
    this.cbInjectAttributes.TabIndex = 3;
    this.cbInjectAttributes.Text = "Разрешить запись атрибутов объекта в файл";
    this.cbInjectAttributes.UseVisualStyleBackColor = true;
    this.btOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Location = new Point(348, 146);
    this.btOK.Name = "btOK";
    this.btOK.Size = new Size(75, 25);
    this.btOK.TabIndex = 4;
    this.btOK.Text = "OK";
    this.btOK.UseVisualStyleBackColor = true;
    this.btOK.Click += new EventHandler(this.btOK_Click);
    this.cbInjectSignNamesOnly.AutoSize = true;
    this.cbInjectSignNamesOnly.Enabled = false;
    this.cbInjectSignNamesOnly.Location = new Point(31 /*0x1F*/, 47);
    this.cbInjectSignNamesOnly.Margin = new Padding(3, 3, 3, 8);
    this.cbInjectSignNamesOnly.Name = "cbInjectSignNamesOnly";
    this.cbInjectSignNamesOnly.Size = new Size(253, 17);
    this.cbInjectSignNamesOnly.TabIndex = 1;
    this.cbInjectSignNamesOnly.Text = "Записывать только фамилию подписавшего";
    this.cbInjectSignNamesOnly.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(434, 182);
    this.Controls.Add((Control) this.cbInjectSignNamesOnly);
    this.Controls.Add((Control) this.btOK);
    this.Controls.Add((Control) this.cbInjectAttributes);
    this.Controls.Add((Control) this.cbInjectFileChecksum);
    this.Controls.Add((Control) this.cbInjectSigns);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (StandaloneViewOptionsEditorWindow);
    this.Padding = new Padding(8, 16 /*0x10*/, 8, 8);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Опции просмотра";
    this.KeyUp += new KeyEventHandler(this.Window_KeyUp);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
