
// Type: Intermech.Search.UI.MessageControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Search.UI;

public class MessageControl : UserControl
{
  private _MessageType _type;
  private IconSize _iconSize = IconSize.Middle;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PictureBox _pictureBox;
  private Label _label;
  private TableLayoutPanel tableLayoutPanel1;

  public MessageControl()
  {
    this.InitializeComponent();
    this.UpdateControls();
  }

  [DefaultValue(IconSize.Middle)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public IconSize IconSize
  {
    get => this._iconSize;
    set
    {
      if (this._iconSize == value)
        return;
      this._iconSize = value;
      this.UpdateControls();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Editor(typeof (MultilineStringEditor), typeof (UITypeEditor))]
  public override string Text
  {
    get => this._label.Text;
    set => this._label.Text = value;
  }

  [DefaultValue(_MessageType.Information)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public _MessageType Type
  {
    get => this._type;
    set
    {
      if (this._type == value)
        return;
      this._type = value;
      this.UpdateControls();
    }
  }

  private void UpdateControls()
  {
    if (this._type == _MessageType.Error)
    {
      this.BackColor = Color.LightPink;
      if (this._iconSize == IconSize.Large)
        this._pictureBox.Image = (Image) UIResources.Error_64x64;
      else if (this._iconSize == IconSize.Middle)
      {
        this._pictureBox.Image = (Image) UIResources.Error_32x32;
      }
      else
      {
        if (this._iconSize != IconSize.Small)
          return;
        this._pictureBox.Image = (Image) UIResources.Error_16x16;
      }
    }
    else if (this._type == _MessageType.Information)
    {
      this.BackColor = Color.LightBlue;
      if (this._iconSize == IconSize.Large)
        this._pictureBox.Image = (Image) UIResources.Information_64x64;
      else if (this._iconSize == IconSize.Middle)
      {
        this._pictureBox.Image = (Image) UIResources.Information_32x32;
      }
      else
      {
        if (this._iconSize != IconSize.Small)
          return;
        this._pictureBox.Image = (Image) UIResources.Information_16x16;
      }
    }
    else if (this._type == _MessageType.Success)
    {
      this.BackColor = Color.LightGreen;
      if (this._iconSize == IconSize.Large)
        this._pictureBox.Image = (Image) UIResources.Success_64x64;
      else if (this._iconSize == IconSize.Middle)
      {
        this._pictureBox.Image = (Image) UIResources.Success_32x32;
      }
      else
      {
        if (this._iconSize != IconSize.Small)
          return;
        this._pictureBox.Image = (Image) UIResources.Success_16x16;
      }
    }
    else
    {
      if (this._type != _MessageType.Warning)
        return;
      this.BackColor = Color.LightYellow;
      if (this._iconSize == IconSize.Large)
        this._pictureBox.Image = (Image) UIResources.Warning_64x64;
      else if (this._iconSize == IconSize.Middle)
      {
        this._pictureBox.Image = (Image) UIResources.Warning_32x32;
      }
      else
      {
        if (this._iconSize != IconSize.Small)
          return;
        this._pictureBox.Image = (Image) UIResources.Warning_16x16;
      }
    }
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
    this._pictureBox = new PictureBox();
    this._label = new Label();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this._pictureBox.Dock = DockStyle.Fill;
    this._pictureBox.Location = new Point(23, 17);
    this._pictureBox.Name = "_pictureBox";
    this._pictureBox.Size = new Size(47, 47);
    this._pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
    this._pictureBox.TabIndex = 0;
    this._pictureBox.TabStop = false;
    this._label.AutoSize = true;
    this._label.Dock = DockStyle.Fill;
    this._label.Location = new Point(103, 23);
    this._label.Name = "_label";
    this._label.Size = new Size(35, 13);
    this._label.TabIndex = 1;
    this._label.Text = "label1";
    this._label.TextAlign = ContentAlignment.MiddleLeft;
    this.tableLayoutPanel1.ColumnCount = 2;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 64f));
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this._label, 1, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._pictureBox, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 1;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(564, (int) byte.MaxValue);
    this.tableLayoutPanel1.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.ControlLightLight;
    this.BorderStyle = BorderStyle.FixedSingle;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (MessageControl);
    this.Size = new Size(564, (int) byte.MaxValue);
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
