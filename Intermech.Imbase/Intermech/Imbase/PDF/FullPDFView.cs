// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.PDF.FullPDFView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using PdfiumViewer;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.PDF;

public class FullPDFView : Form
{
  private IContainer components;
  private ToolbarPdfViewer pdfViewer;

  public FullPDFView() => this.InitializeComponent();

  public IPdfDocument Document
  {
    get => this.pdfViewer.Document;
    set => this.pdfViewer.Document = value;
  }

  internal static void ShowPdf(long objectId)
  {
    IPdfDocument orLoadPdfDocument = PDFCache.GetOrLoadPdfDocument(objectId);
    if (orLoadPdfDocument == null)
      return;
    new FullPDFView() { Document = orLoadPdfDocument }.Visible = true;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.pdfViewer = new ToolbarPdfViewer();
    this.SuspendLayout();
    this.pdfViewer.Dock = DockStyle.Fill;
    this.pdfViewer.Location = new Point(0, 0);
    this.pdfViewer.Name = "pdfViewer";
    this.pdfViewer.Size = new Size(800, 450);
    this.pdfViewer.TabIndex = 2;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(800, 450);
    this.Controls.Add((Control) this.pdfViewer);
    this.Name = nameof (FullPDFView);
    this.Text = nameof (FullPDFView);
    this.TopMost = true;
    this.ResumeLayout(false);
  }
}
