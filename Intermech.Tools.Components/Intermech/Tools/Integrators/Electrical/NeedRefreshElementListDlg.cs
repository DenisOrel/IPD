// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.NeedRefreshElementListDlg
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

internal class NeedRefreshElementListDlg : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private Button bRecreate;
  private Button bCancel;

  public NeedRefreshElementListDlg() => this.InitializeComponent();

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
    this.bRecreate = new Button();
    this.bCancel = new Button();
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Location = new Point(21, 27);
    this.label1.Name = "label1";
    this.label1.Size = new Size(323, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Необходимо пересоздать существующие перечни элементов!";
    this.bRecreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bRecreate.DialogResult = DialogResult.OK;
    this.bRecreate.Location = new Point(94, 65);
    this.bRecreate.Name = "bRecreate";
    this.bRecreate.Size = new Size(121, 27);
    this.bRecreate.TabIndex = 3;
    this.bRecreate.Text = "Пересоздать";
    this.bRecreate.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(221, 65);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 4;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bRecreate;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(374, 111);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bRecreate);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MinimumSize = new Size(390, 150);
    this.Name = nameof (NeedRefreshElementListDlg);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Предупреждение";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
