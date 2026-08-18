
// Type: Intermech.Client.Core.NewFolderNameDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class NewFolderNameDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox textBox1;
  private Button btOK;
  private Label label1;
  private Button btCancel;

  public NewFolderNameDialog() => this.InitializeComponent();

  public string FolderName => this.textBox1.Text;

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
    this.textBox1 = new TextBox();
    this.btOK = new Button();
    this.label1 = new Label();
    this.btCancel = new Button();
    this.SuspendLayout();
    this.textBox1.Location = new Point(23, 39);
    this.textBox1.Name = "textBox1";
    this.textBox1.Size = new Size(248, 20);
    this.textBox1.TabIndex = 0;
    this.btOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.ImeMode = ImeMode.NoControl;
    this.btOK.Location = new Point(23, 79);
    this.btOK.Name = "btOK";
    this.btOK.Size = new Size(121, 27);
    this.btOK.TabIndex = 2;
    this.btOK.Text = "OK";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(20, 23);
    this.label1.Name = "label1";
    this.label1.Size = new Size(98, 13);
    this.label1.TabIndex = 4;
    this.label1.Text = "Имя новой папки:";
    this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.ImeMode = ImeMode.NoControl;
    this.btCancel.Location = new Point(150, 79);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(121, 27);
    this.btCancel.TabIndex = 5;
    this.btCancel.Text = "Отмена";
    this.AcceptButton = (IButtonControl) this.btOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.ClientSize = new Size(283, 118);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btOK);
    this.Controls.Add((Control) this.textBox1);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = "NewFolderName";
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Создание новой папки";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
