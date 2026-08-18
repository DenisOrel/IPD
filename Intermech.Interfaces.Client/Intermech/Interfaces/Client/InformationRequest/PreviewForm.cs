// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.InformationRequest.PreviewForm
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client.InformationRequest;

public class PreviewForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox groupBox1;
  private Button closeBtn;

  public PreviewForm() => this.InitializeComponent();

  private void closeBtn_Click(object sender, EventArgs e) => this.Close();

  public void SetImage(Image previewImage)
  {
    PictureBox pictureBox1 = new PictureBox();
    pictureBox1.Dock = DockStyle.Fill;
    pictureBox1.Image = previewImage;
    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
    PictureBox pictureBox2 = pictureBox1;
    this.Controls.Add((Control) pictureBox2);
    pictureBox2.BringToFront();
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
    this.groupBox1 = new GroupBox();
    this.closeBtn = new Button();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.groupBox1.Controls.Add((Control) this.closeBtn);
    this.groupBox1.Dock = DockStyle.Bottom;
    this.groupBox1.Location = new Point(0, 484);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(782, 69);
    this.groupBox1.TabIndex = 0;
    this.groupBox1.TabStop = false;
    this.closeBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.closeBtn.DialogResult = DialogResult.Cancel;
    this.closeBtn.Location = new Point(680, 29);
    this.closeBtn.Name = "closeBtn";
    this.closeBtn.Size = new Size(90, 28);
    this.closeBtn.TabIndex = 0;
    this.closeBtn.Text = "Закрыть";
    this.closeBtn.UseVisualStyleBackColor = true;
    this.closeBtn.Click += new EventHandler(this.closeBtn_Click);
    this.AcceptButton = (IButtonControl) this.closeBtn;
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.closeBtn;
    this.ClientSize = new Size(782, 553);
    this.Controls.Add((Control) this.groupBox1);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MinimumSize = new Size(800, 600);
    this.Name = nameof (PreviewForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Предпросмотр";
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
