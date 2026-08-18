
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.ImViewerFileSateInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

public class ImViewerFileSateInfo : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label lblFileSateInfo;

  public ImViewerFileSateInfo() => this.InitializeComponent();

  public string ShowText
  {
    get => this.lblFileSateInfo.Text;
    set => this.lblFileSateInfo.Text = value;
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
    this.lblFileSateInfo = new Label();
    this.SuspendLayout();
    this.lblFileSateInfo.Dock = DockStyle.Fill;
    this.lblFileSateInfo.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.lblFileSateInfo.ForeColor = Color.Red;
    this.lblFileSateInfo.Location = new Point(0, 0);
    this.lblFileSateInfo.Name = "lblFileSateInfo";
    this.lblFileSateInfo.Size = new Size(564, 40);
    this.lblFileSateInfo.TabIndex = 0;
    this.lblFileSateInfo.TextAlign = ContentAlignment.MiddleCenter;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.lblFileSateInfo);
    this.Name = nameof (ImViewerFileSateInfo);
    this.Size = new Size(564, 40);
    this.ResumeLayout(false);
  }
}
