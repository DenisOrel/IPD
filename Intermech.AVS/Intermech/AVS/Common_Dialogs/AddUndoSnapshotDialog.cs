// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.AddUndoSnapshotDialog
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class AddUndoSnapshotDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private Button bNo;
  private Button bYes;
  private TextBox tbName;

  public AddUndoSnapshotDialog() => this.InitializeComponent();

  public string SnapshotName
  {
    get => this.tbName.Text;
    set => this.tbName.Text = value;
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
    this.label1 = new Label();
    this.bNo = new Button();
    this.bYes = new Button();
    this.tbName = new TextBox();
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Location = new Point(6, 8);
    this.label1.Name = "label1";
    this.label1.Size = new Size(77, 13);
    this.label1.TabIndex = 11;
    this.label1.Text = "Комментарий";
    this.bNo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bNo.DialogResult = DialogResult.No;
    this.bNo.Location = new Point(300, 69);
    this.bNo.MaximumSize = new Size(150, 23);
    this.bNo.MinimumSize = new Size(75, 23);
    this.bNo.Name = "bNo";
    this.bNo.Size = new Size(75, 23);
    this.bNo.TabIndex = 10;
    this.bNo.Text = "Отмена";
    this.bNo.UseVisualStyleBackColor = true;
    this.bYes.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bYes.DialogResult = DialogResult.Yes;
    this.bYes.Location = new Point(219, 69);
    this.bYes.MaximumSize = new Size(150, 23);
    this.bYes.MinimumSize = new Size(75, 23);
    this.bYes.Name = "bYes";
    this.bYes.Size = new Size(75, 23);
    this.bYes.TabIndex = 9;
    this.bYes.Text = "OK";
    this.bYes.UseVisualStyleBackColor = true;
    this.tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbName.Location = new Point(6, 24);
    this.tbName.Name = "tbName";
    this.tbName.Size = new Size(369, 20);
    this.tbName.TabIndex = 8;
    this.AcceptButton = (IButtonControl) this.bYes;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bNo;
    this.ClientSize = new Size(382, 104);
    this.ControlBox = false;
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.bNo);
    this.Controls.Add((Control) this.bYes);
    this.Controls.Add((Control) this.tbName);
    this.MaximumSize = new Size(700, 220);
    this.MinimumSize = new Size(300, 120);
    this.Name = nameof (AddUndoSnapshotDialog);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Формирование данных для отката изменений";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
