// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.PDF.ToolbarPdfViewer
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using PdfiumViewer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.PDF;

internal class ToolbarPdfViewer : UserControl
{
  private bool _loading;
  private IContainer components;
  private ToolStrip toolStripPdfViewer;
  private ToolStripButton toolStripButtonFirstPage;
  private ToolStripButton toolStripButtonPreviousPage;
  private ToolStripLabel toolStripLabelTotalPages;
  private ToolStripButton toolStripButtonNextPage;
  private ToolStripButton toolStripButtonLastPage;
  private ToolStripSeparator toolStripSeparator;
  private ToolStripButton toolStripButtonZoomIn;
  private ToolStripButton toolStripButtonZoomOut;
  private ToolStripButton toolStripButtonTilePage;
  private PdfViewer pdfViewer;
  private ToolStripComboBox toolStripComboBoxPage;

  public ToolbarPdfViewer()
  {
    this.InitializeComponent();
    this.InitializeHandlers();
    this.InitializeToolStrip();
    this.InitializeRenderer();
  }

  public IPdfDocument Document
  {
    get => this.pdfViewer.Document;
    set => this.ShowDocument(value);
  }

  private bool IsFullDocument => this.toolStripComboBoxPage.Enabled;

  public void ShowDocument(IPdfDocument document)
  {
    this.pdfViewer.Document = document;
    this.BeginLoad();
    try
    {
      if (document == null)
        return;
      this.pdfViewer.ZoomMode = PdfViewerZoomMode.FitBest;
      this.pdfViewer.Renderer.Zoom = 1.0;
      this.toolStripLabelTotalPages.Text = " / " + (object) document.PageCount;
      this.FillComboBox(document.PageCount);
      this.UpdateViewer();
    }
    finally
    {
      this.EndLoad();
    }
  }

  private void BeginLoad() => this._loading = true;

  private void EndLoad() => this._loading = false;

  private void FillComboBox(int pageCount)
  {
    ComboBox.ObjectCollection items = this.toolStripComboBoxPage.Items;
    items.Clear();
    for (int index = 0; index < pageCount; ++index)
      items.Add((object) (index + 1));
  }

  private void CheckButtons()
  {
    if (this.pdfViewer.Document.PageCount == 1)
    {
      this.EnableLeafButtons(false);
      this.EnableZoomButtons(true);
    }
    else
    {
      this.EnableLeafButtons(true);
      this.EnableZoomButtons(true);
      if (this.pdfViewer.Renderer.Page == 0)
      {
        this.toolStripButtonFirstPage.Enabled = false;
        this.toolStripButtonPreviousPage.Enabled = false;
      }
      else
      {
        if (this.pdfViewer.Renderer.Page != this.pdfViewer.Document.PageCount - 1)
          return;
        this.toolStripButtonNextPage.Enabled = false;
        this.toolStripButtonLastPage.Enabled = false;
      }
    }
  }

  private void EnableLeafButtons(bool value)
  {
    this.toolStripButtonFirstPage.Enabled = value;
    this.toolStripButtonPreviousPage.Enabled = value;
    this.toolStripButtonNextPage.Enabled = value;
    this.toolStripButtonLastPage.Enabled = value;
  }

  private void EnableZoomButtons(bool value)
  {
    this.toolStripButtonZoomIn.Enabled = value;
    this.toolStripButtonZoomOut.Enabled = value;
    this.toolStripButtonTilePage.Enabled = value;
  }

  private void InitializeHandlers()
  {
    this.pdfViewer.Renderer.MouseMove += new MouseEventHandler(this.OnMouseMoveDocument);
    this.pdfViewer.Renderer.MouseWheel += new MouseEventHandler(this.OnMouseMoveDocument);
    this.pdfViewer.Renderer.Scroll += new ScrollEventHandler(this.OnScrollDocument);
  }

  private void UnInitializeHandlers()
  {
    this.pdfViewer.Renderer.MouseMove -= new MouseEventHandler(this.OnMouseMoveDocument);
    this.pdfViewer.Renderer.MouseWheel -= new MouseEventHandler(this.OnMouseMoveDocument);
    this.pdfViewer.Renderer.Scroll -= new ScrollEventHandler(this.OnScrollDocument);
  }

  private void InitializeRenderer()
  {
    this.toolStripPdfViewer.Renderer = (ToolStripRenderer) new BordersToolStripRenderer();
  }

  private void InitializeToolStrip()
  {
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service)
    {
      this.toolStripPdfViewer.ImageList = service.ImageList;
      this.toolStripButtonFirstPage.ImageIndex = service.ImageIndex("imgPageFirst");
      this.toolStripButtonPreviousPage.ImageIndex = service.ImageIndex("imgPagePrev");
      this.toolStripButtonNextPage.ImageIndex = service.ImageIndex("imgPageNext");
      this.toolStripButtonLastPage.ImageIndex = service.ImageIndex("imgPageLast");
      this.toolStripButtonTilePage.ImageIndex = service.ImageIndex("imgZoomAll");
      this.toolStripButtonZoomOut.ImageIndex = service.ImageIndex("imgZoomOut");
      this.toolStripButtonZoomIn.ImageIndex = service.ImageIndex("imgZoomIn");
    }
    this.EnableLeafButtons(false);
    this.EnableZoomButtons(false);
  }

  private void OnMouseMoveDocument(object sender, MouseEventArgs e) => this.UpdateViewer();

  private void OnScrollDocument(object sender, ScrollEventArgs e) => this.UpdateViewer();

  private void SetPageToTextBox()
  {
    this.toolStripComboBoxPage.Text = (this.pdfViewer.Renderer.Page + 1).ToString();
  }

  private void ToolStripButtonFirstPage_Click(object sender, EventArgs e)
  {
    this.pdfViewer.Renderer.Page = 0;
    this.UpdateViewer();
  }

  private void ToolStripButtonLastPage_Click(object sender, EventArgs e)
  {
    this.pdfViewer.Renderer.Page = this.Document.PageCount - 1;
    this.UpdateViewer();
  }

  private void ToolStripButtonNextPage_Click(object sender, EventArgs e)
  {
    if (this.pdfViewer.Renderer.Page >= this.Document.PageCount - 1)
      return;
    ++this.pdfViewer.Renderer.Page;
    this.UpdateViewer();
  }

  private void ToolStripButtonPreviousPage_Click(object sender, EventArgs e)
  {
    if (this.pdfViewer.Renderer.Page <= 0)
      return;
    --this.pdfViewer.Renderer.Page;
    this.UpdateViewer();
  }

  private void ToolStripButtonTilePage_Click(object sender, EventArgs e)
  {
    int page = this.pdfViewer.Renderer.Page;
    this.pdfViewer.ZoomMode = PdfViewerZoomMode.FitBest;
    this.pdfViewer.Renderer.Zoom = 1.0;
    this.pdfViewer.Renderer.Page = page;
  }

  private void ToolStripButtonUnzoom_Click(object sender, EventArgs e)
  {
    this.pdfViewer.Renderer.ZoomOut();
  }

  private void ToolStripButtonZoom_Click(object sender, EventArgs e)
  {
    this.pdfViewer.Renderer.ZoomIn();
  }

  private void ToolStripTextBoxPage_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar == '\r')
    {
      this.TrySetNewPage();
    }
    else
    {
      if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
        return;
      e.Handled = true;
    }
  }

  private void ToolStripTextBoxPage_LostFocus(object sender, EventArgs e) => this.TrySetNewPage();

  private void TrySetNewPage()
  {
    int result;
    if (int.TryParse(this.toolStripComboBoxPage.Text, out result) && result >= 1 && result <= this.Document.PageCount)
      this.pdfViewer.Renderer.Page = result - 1;
    else
      this.toolStripComboBoxPage.Text = (this.pdfViewer.Renderer.Page + 1).ToString();
  }

  private void UpdateViewer()
  {
    if (this.Document == null)
      return;
    this.SetPageToTextBox();
    this.CheckButtons();
  }

  private void toolStripComboBoxPage_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.toolStripComboBoxPage.SelectedItem == null)
      return;
    this.pdfViewer.Renderer.Page = Convert.ToInt32(this.toolStripComboBoxPage.SelectedItem) - 1;
    this.UpdateViewer();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.UnInitializeHandlers();
      this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.pdfViewer = new PdfViewer();
    this.toolStripPdfViewer = new ToolStrip();
    this.toolStripButtonFirstPage = new ToolStripButton();
    this.toolStripButtonPreviousPage = new ToolStripButton();
    this.toolStripComboBoxPage = new ToolStripComboBox();
    this.toolStripLabelTotalPages = new ToolStripLabel();
    this.toolStripButtonNextPage = new ToolStripButton();
    this.toolStripButtonLastPage = new ToolStripButton();
    this.toolStripSeparator = new ToolStripSeparator();
    this.toolStripButtonTilePage = new ToolStripButton();
    this.toolStripButtonZoomOut = new ToolStripButton();
    this.toolStripButtonZoomIn = new ToolStripButton();
    this.toolStripPdfViewer.SuspendLayout();
    this.SuspendLayout();
    this.pdfViewer.Dock = DockStyle.Fill;
    this.pdfViewer.Location = new Point(0, 25);
    this.pdfViewer.Name = "pdfViewer";
    this.pdfViewer.ShowBookmarks = false;
    this.pdfViewer.ShowToolbar = false;
    this.pdfViewer.Size = new Size(591, 428);
    this.pdfViewer.TabIndex = 1;
    this.toolStripPdfViewer.BackColor = Color.Transparent;
    this.toolStripPdfViewer.GripMargin = new Padding(0);
    this.toolStripPdfViewer.Items.AddRange(new ToolStripItem[10]
    {
      (ToolStripItem) this.toolStripButtonFirstPage,
      (ToolStripItem) this.toolStripButtonPreviousPage,
      (ToolStripItem) this.toolStripComboBoxPage,
      (ToolStripItem) this.toolStripLabelTotalPages,
      (ToolStripItem) this.toolStripButtonNextPage,
      (ToolStripItem) this.toolStripButtonLastPage,
      (ToolStripItem) this.toolStripSeparator,
      (ToolStripItem) this.toolStripButtonTilePage,
      (ToolStripItem) this.toolStripButtonZoomOut,
      (ToolStripItem) this.toolStripButtonZoomIn
    });
    this.toolStripPdfViewer.Location = new Point(0, 0);
    this.toolStripPdfViewer.Name = "toolStripPdfViewer";
    this.toolStripPdfViewer.Size = new Size(591, 25);
    this.toolStripPdfViewer.TabIndex = 0;
    this.toolStripPdfViewer.Text = "toolStripPdfViewer";
    this.toolStripButtonFirstPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonFirstPage.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonFirstPage.Name = "toolStripButtonFirstPage";
    this.toolStripButtonFirstPage.Size = new Size(23, 22);
    this.toolStripButtonFirstPage.Text = "Первая страница";
    this.toolStripButtonFirstPage.Click += new EventHandler(this.ToolStripButtonFirstPage_Click);
    this.toolStripButtonPreviousPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonPreviousPage.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonPreviousPage.Name = "toolStripButtonPreviousPage";
    this.toolStripButtonPreviousPage.Size = new Size(23, 22);
    this.toolStripButtonPreviousPage.Text = "Предыдущая страница";
    this.toolStripButtonPreviousPage.Click += new EventHandler(this.ToolStripButtonPreviousPage_Click);
    this.toolStripComboBoxPage.DropDownStyle = ComboBoxStyle.DropDownList;
    this.toolStripComboBoxPage.Name = "toolStripComboBoxPage";
    this.toolStripComboBoxPage.Size = new Size(75, 25);
    this.toolStripComboBoxPage.SelectedIndexChanged += new EventHandler(this.toolStripComboBoxPage_SelectedIndexChanged);
    this.toolStripLabelTotalPages.AutoSize = false;
    this.toolStripLabelTotalPages.Name = "toolStripLabelTotalPages";
    this.toolStripLabelTotalPages.Size = new Size(42, 22);
    this.toolStripLabelTotalPages.Text = " /";
    this.toolStripLabelTotalPages.TextAlign = ContentAlignment.MiddleLeft;
    this.toolStripButtonNextPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonNextPage.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonNextPage.Name = "toolStripButtonNextPage";
    this.toolStripButtonNextPage.Size = new Size(23, 22);
    this.toolStripButtonNextPage.Text = "Следующая страница";
    this.toolStripButtonNextPage.Click += new EventHandler(this.ToolStripButtonNextPage_Click);
    this.toolStripButtonLastPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonLastPage.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonLastPage.Name = "toolStripButtonLastPage";
    this.toolStripButtonLastPage.Size = new Size(23, 22);
    this.toolStripButtonLastPage.Text = "Последняя страница";
    this.toolStripButtonLastPage.Click += new EventHandler(this.ToolStripButtonLastPage_Click);
    this.toolStripSeparator.Name = "toolStripSeparator";
    this.toolStripSeparator.Size = new Size(6, 25);
    this.toolStripButtonTilePage.Alignment = ToolStripItemAlignment.Right;
    this.toolStripButtonTilePage.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonTilePage.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonTilePage.Name = "toolStripButtonTilePage";
    this.toolStripButtonTilePage.Size = new Size(23, 22);
    this.toolStripButtonTilePage.Text = "По ширине страницы";
    this.toolStripButtonTilePage.Click += new EventHandler(this.ToolStripButtonTilePage_Click);
    this.toolStripButtonZoomOut.Alignment = ToolStripItemAlignment.Right;
    this.toolStripButtonZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonZoomOut.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
    this.toolStripButtonZoomOut.Size = new Size(23, 22);
    this.toolStripButtonZoomOut.Text = "Отдалить";
    this.toolStripButtonZoomOut.Click += new EventHandler(this.ToolStripButtonUnzoom_Click);
    this.toolStripButtonZoomIn.Alignment = ToolStripItemAlignment.Right;
    this.toolStripButtonZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.toolStripButtonZoomIn.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonZoomIn.Name = "toolStripButtonZoomIn";
    this.toolStripButtonZoomIn.Size = new Size(23, 22);
    this.toolStripButtonZoomIn.Text = "Приблизить";
    this.toolStripButtonZoomIn.Click += new EventHandler(this.ToolStripButtonZoom_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.AppWorkspace;
    this.BorderStyle = BorderStyle.FixedSingle;
    this.Controls.Add((Control) this.pdfViewer);
    this.Controls.Add((Control) this.toolStripPdfViewer);
    this.Name = nameof (ToolbarPdfViewer);
    this.Size = new Size(591, 453);
    this.toolStripPdfViewer.ResumeLayout(false);
    this.toolStripPdfViewer.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
