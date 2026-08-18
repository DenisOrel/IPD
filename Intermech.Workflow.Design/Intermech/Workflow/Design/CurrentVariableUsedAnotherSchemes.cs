// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.CurrentVariableUsedAnotherSchemes
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class CurrentVariableUsedAnotherSchemes : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button yesBtn;
  private Button noBtn;
  private Label label1;
  private ListBox listBox1;
  private Label label2;

  public CurrentVariableUsedAnotherSchemes(string labelText, string[] linesBox)
  {
    this.InitializeComponent();
    this.label1.Text = labelText;
    this.listBox1.Items.AddRange((object[]) linesBox);
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
    this.yesBtn = new Button();
    this.noBtn = new Button();
    this.label1 = new Label();
    this.listBox1 = new ListBox();
    this.label2 = new Label();
    this.SuspendLayout();
    this.yesBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.yesBtn.DialogResult = DialogResult.Yes;
    this.yesBtn.Location = new Point(316, 226);
    this.yesBtn.Name = "yesBtn";
    this.yesBtn.Size = new Size(75, 23);
    this.yesBtn.TabIndex = 0;
    this.yesBtn.Text = "Да";
    this.yesBtn.UseVisualStyleBackColor = true;
    this.noBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.noBtn.DialogResult = DialogResult.No;
    this.noBtn.Location = new Point(397, 226);
    this.noBtn.Name = "noBtn";
    this.noBtn.Size = new Size(75, 23);
    this.noBtn.TabIndex = 1;
    this.noBtn.Text = "Нет";
    this.noBtn.UseVisualStyleBackColor = true;
    this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.label1.Location = new Point(13, 13);
    this.label1.Name = "label1";
    this.label1.Size = new Size(459, 40);
    this.label1.TabIndex = 2;
    this.label1.Text = "label1";
    this.listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.listBox1.FormattingEnabled = true;
    this.listBox1.Location = new Point(13, 56);
    this.listBox1.Name = "listBox1";
    this.listBox1.SelectionMode = SelectionMode.None;
    this.listBox1.Size = new Size(459, 121);
    this.listBox1.TabIndex = 3;
    this.label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.label2.Location = new Point(10, 179);
    this.label2.Name = "label2";
    this.label2.Size = new Size(462, 44);
    this.label2.TabIndex = 2;
    this.label2.Text = "Изменение списка доступных значений повлечет изменение этих значений и во всех этих шаблонах/процессах, что может повлиять на их работу. Продолжить редактирование?";
    this.AcceptButton = (IButtonControl) this.yesBtn;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.noBtn;
    this.ClientSize = new Size(484, 261);
    this.ControlBox = false;
    this.Controls.Add((Control) this.listBox1);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.noBtn);
    this.Controls.Add((Control) this.yesBtn);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(500, 300);
    this.Name = nameof (CurrentVariableUsedAnotherSchemes);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Подтверждение";
    this.ResumeLayout(false);
  }
}
