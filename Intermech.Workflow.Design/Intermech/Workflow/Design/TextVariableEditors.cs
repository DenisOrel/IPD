// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.TextVariableEditors
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class TextVariableEditors : Form
{
  private Button OKBtn;
  private Button CancelBtn;
  internal TextBox textValue;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public TextVariableEditors(string value)
  {
    this.InitializeComponent();
    this.textValue.Text = value;
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
    this.textValue = new TextBox();
    this.OKBtn = new Button();
    this.CancelBtn = new Button();
    this.SuspendLayout();
    this.textValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.textValue.Location = new Point(13, 13);
    this.textValue.Multiline = true;
    this.textValue.Name = "textValue";
    this.textValue.ScrollBars = ScrollBars.Vertical;
    this.textValue.Size = new Size(489, 195);
    this.textValue.TabIndex = 0;
    this.OKBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.OKBtn.DialogResult = DialogResult.OK;
    this.OKBtn.Location = new Point(346, 226);
    this.OKBtn.Name = "OKBtn";
    this.OKBtn.Size = new Size(75, 23);
    this.OKBtn.TabIndex = 1;
    this.OKBtn.Text = "OK";
    this.OKBtn.UseVisualStyleBackColor = true;
    this.CancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.CancelBtn.DialogResult = DialogResult.Cancel;
    this.CancelBtn.Location = new Point(427, 226);
    this.CancelBtn.Name = "CancelBtn";
    this.CancelBtn.Size = new Size(75, 23);
    this.CancelBtn.TabIndex = 2;
    this.CancelBtn.Text = "Отмена";
    this.CancelBtn.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancelBtn;
    this.ClientSize = new Size(514, 261);
    this.Controls.Add((Control) this.CancelBtn);
    this.Controls.Add((Control) this.OKBtn);
    this.Controls.Add((Control) this.textValue);
    this.MinimumSize = new Size(530, 300);
    this.Name = nameof (TextVariableEditors);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Редактирование содержимого переменной";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
