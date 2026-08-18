// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.WaitFrom
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using Intermech.Archives.ScanDocums;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TwainScanner;

public class WaitFrom : Form
{
  private static ScanerDocumentService scanerService;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button button1;

  public WaitFrom() => this.InitializeComponent();

  protected override void OnShown(EventArgs e) => base.OnShown(e);

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.Hide();
    NamedPipesServer.Instance.Init();
  }

  protected override void OnActivated(EventArgs e)
  {
    this.Hide();
    base.OnActivated(e);
  }

  private static void NamedPipesServer_OnGetObjectData(object sender, EventArgs e)
  {
    string fileExt = Encoding.UTF8.GetString((byte[]) sender).Remove(0, 1);
    if (WaitFrom.scanerService == null)
    {
      WaitFrom.scanerService = new ScanerDocumentService();
      WaitFrom.scanerService.OnEndScaning += new EventHandler(WaitFrom.scanerService_OnEndScaning);
      WaitFrom.scanerService.OnImageTransfer += new EventHandler(WaitFrom.scanerService_OnImageTransfer);
    }
    WaitFrom.scanerService.AcquireDoc(fileExt);
  }

  private static void scanerService_OnImageTransfer(object sender, EventArgs e)
  {
    if (!(sender is byte[] buffer))
      return;
    new AsyncPipeClient().Send(buffer, "Intermech.Archives.ScanDocums.Client");
  }

  private static void scanerService_OnEndScaning(object sender, EventArgs e)
  {
  }

  private void button1_Click(object sender, EventArgs e)
  {
    string fileExt = "";
    if (WaitFrom.scanerService == null)
    {
      WaitFrom.scanerService = new ScanerDocumentService();
      WaitFrom.scanerService.OnEndScaning += new EventHandler(WaitFrom.scanerService_OnEndScaning);
      WaitFrom.scanerService.OnImageTransfer += new EventHandler(WaitFrom.scanerService_OnImageTransfer);
    }
    WaitFrom.scanerService.AcquireDoc(fileExt);
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
    this.button1 = new Button();
    this.SuspendLayout();
    this.button1.Location = new Point(60, 39);
    this.button1.Name = "button1";
    this.button1.Size = new Size(75, 23);
    this.button1.TabIndex = 0;
    this.button1.Text = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(284, 262);
    this.Controls.Add((Control) this.button1);
    this.Name = nameof (WaitFrom);
    this.Text = nameof (WaitFrom);
    this.ResumeLayout(false);
  }
}
