// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WarningControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class WarningControl : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PictureBox PictureBox;
  private Label Label;

  public WarningControl()
  {
    this.InitializeComponent();
    this.PictureBox.BackgroundImage = Holder.WarningImage;
  }

  public new string Text
  {
    get => this.Label.Text;
    set => this.Label.Text = value;
  }

  public static WarningControl Show(Control c, string Text = "")
  {
    Control control = c.Parent ?? c;
    WarningControl warningControl = new WarningControl();
    warningControl.Dock = DockStyle.Top;
    warningControl.Top = 0;
    warningControl.Parent = control;
    if (Text != "")
      warningControl.Text = Text;
    return warningControl;
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
    this.PictureBox = new PictureBox();
    this.Label = new Label();
    ((ISupportInitialize) this.PictureBox).BeginInit();
    this.SuspendLayout();
    this.PictureBox.BackgroundImageLayout = ImageLayout.Center;
    this.PictureBox.Dock = DockStyle.Left;
    this.PictureBox.Location = new Point(0, 0);
    this.PictureBox.Name = "PictureBox";
    this.PictureBox.Size = new Size(42, 52);
    this.PictureBox.TabIndex = 0;
    this.PictureBox.TabStop = false;
    this.Label.Dock = DockStyle.Fill;
    this.Label.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.Label.Location = new Point(42, 0);
    this.Label.Name = "Label";
    this.Label.Size = new Size(590, 52);
    this.Label.TabIndex = 1;
    this.Label.Text = "Внимание! Задание ещё не взято вами в работу.";
    this.Label.TextAlign = ContentAlignment.MiddleLeft;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Info;
    this.Controls.Add((Control) this.Label);
    this.Controls.Add((Control) this.PictureBox);
    this.Name = "WOWarningControl";
    this.Size = new Size(632, 52);
    ((ISupportInitialize) this.PictureBox).EndInit();
    this.ResumeLayout(false);
  }
}
