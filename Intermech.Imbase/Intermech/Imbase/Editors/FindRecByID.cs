// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.FindRecByID
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class FindRecByID : Form
{
  private static string LastValue = string.Empty;
  private TableEditor _editor;
  private IContainer components;
  private Button _findButton;
  private Button _cancelButton;
  private TextBox textBox1;
  private Label label1;

  internal static void Execute(TableEditor editor)
  {
    using (FindRecByID findRecById = new FindRecByID())
    {
      findRecById.SetData(editor);
      int num = (int) findRecById.ShowDialog();
    }
  }

  public FindRecByID() => this.InitializeComponent();

  private void SetData(TableEditor editor)
  {
    this._editor = editor;
    this.textBox1.Text = FindRecByID.LastValue;
  }

  private void _findButton_Click(object sender, EventArgs e)
  {
    string text = this.textBox1.Text;
    this._editor.GotoId(text);
    FindRecByID.LastValue = text;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this._findButton = new Button();
    this._cancelButton = new Button();
    this.textBox1 = new TextBox();
    this.label1 = new Label();
    this.SuspendLayout();
    this._findButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._findButton.Location = new Point(111, 66);
    this._findButton.Name = "_findButton";
    this._findButton.Size = new Size(111, 23);
    this._findButton.TabIndex = 0;
    this._findButton.Text = "Перейти к записи";
    this._findButton.UseVisualStyleBackColor = true;
    this._findButton.Click += new EventHandler(this._findButton_Click);
    this._cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Location = new Point(228, 66);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 1;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this.textBox1.Location = new Point(12, 25);
    this.textBox1.Name = "textBox1";
    this.textBox1.Size = new Size(291, 20);
    this.textBox1.TabIndex = 2;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(165, 13);
    this.label1.TabIndex = 3;
    this.label1.Text = "Идентификатор (код или GUID)";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.ClientSize = new Size(315, 101);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.textBox1);
    this.Controls.Add((Control) this._cancelButton);
    this.Controls.Add((Control) this._findButton);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FindRecByID);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Поик записи по ID";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
