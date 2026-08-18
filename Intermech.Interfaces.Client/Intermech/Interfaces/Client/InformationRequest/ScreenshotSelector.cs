// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.InformationRequest.ScreenshotSelector
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client.InformationRequest;

public class ScreenshotSelector : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ThumbnailViewerControl thumbnailViewerControl;
  private Label label1;
  private Button okBtn;
  private GroupBox groupBox1;

  public ScreenshotSelector() => this.InitializeComponent();

  public void SetScreenShot(List<byte[]> screensDataList)
  {
    this.thumbnailViewerControl.ImageList = new List<byte[]>();
    this.thumbnailViewerControl.AddImage(screensDataList);
  }

  public int SelectedScreenshotIndex => this.thumbnailViewerControl.SelectedScreenshotIndex;

  private void okBtn_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ScreenshotSelector));
    this.label1 = new Label();
    this.okBtn = new Button();
    this.groupBox1 = new GroupBox();
    this.thumbnailViewerControl = new ThumbnailViewerControl();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.label1.Dock = DockStyle.Top;
    this.label1.Location = new Point(0, 0);
    this.label1.Margin = new Padding(5, 5, 5, 5);
    this.label1.Name = "label1";
    this.label1.Padding = new Padding(5, 5, 5, 5);
    this.label1.Size = new Size(623, 38);
    this.label1.TabIndex = 1;
    this.label1.Text = "Выберите более подходящий снимок экрана. Двойное нажатие по снимку откроет окно предпросмотра данного изображения. Выбранный скриншот отмечается синим цветом:";
    this.okBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.okBtn.DialogResult = DialogResult.Cancel;
    this.okBtn.Location = new Point(536, 20);
    this.okBtn.Name = "okBtn";
    this.okBtn.Size = new Size(75, 23);
    this.okBtn.TabIndex = 2;
    this.okBtn.Text = "ОК";
    this.okBtn.UseVisualStyleBackColor = true;
    this.okBtn.Click += new EventHandler(this.okBtn_Click);
    this.groupBox1.Controls.Add((Control) this.okBtn);
    this.groupBox1.Dock = DockStyle.Bottom;
    this.groupBox1.Location = new Point(0, 386);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(623, 55);
    this.groupBox1.TabIndex = 3;
    this.groupBox1.TabStop = false;
    this.thumbnailViewerControl.AutoScroll = true;
    this.thumbnailViewerControl.Dock = DockStyle.Fill;
    this.thumbnailViewerControl.ImageList = (List<byte[]>) componentResourceManager.GetObject("thumbnailViewerControl.ImageList");
    this.thumbnailViewerControl.Location = new Point(0, 38);
    this.thumbnailViewerControl.Name = "thumbnailViewerControl";
    this.thumbnailViewerControl.Size = new Size(623, 348);
    this.thumbnailViewerControl.TabIndex = 0;
    this.AcceptButton = (IButtonControl) this.okBtn;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.okBtn;
    this.ClientSize = new Size(623, 441);
    this.Controls.Add((Control) this.thumbnailViewerControl);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.label1);
    this.MinimumSize = new Size(639, 479);
    this.Name = nameof (ScreenshotSelector);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Выбор снимка экрана";
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
