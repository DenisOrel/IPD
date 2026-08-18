
// Type: Intermech.PdfPrintCenter.Controls.PdfViewer.AdvToolbarPdfViewer




using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using Intermech.PdfPrintCenter.Properties;
using Intermech.PdfPrintCenter.Utils;
using Intermech.PdfPrintCenter.Utils.UtilMethods;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.Controls.PdfViewer
{
    internal class AdvToolbarPdfViewer : UserControl
    {
      private const string Slash = " / ";
      private PrintCenterNode _node;
      private IPdfPageProducer _layout;
      private IContainer components;
      private ToolStrip toolStripPdfViewer;
      private ToolStripButton toolStripButtonFirstPage;
      private ToolStripButton toolStripButtonPreviousPage;
      private ToolStripTextBox toolStripTextBoxPage;
      private ToolStripLabel toolStripLabelPages;
      private ToolStripButton toolStripButtonNextPage;
      private ToolStripButton toolStripButtonLastPage;
      private ToolStripSeparator toolStripSeparator;
      private ToolStripButton toolStripButtonZoom;
      private ToolStripButton toolStripButtonUnzoom;
      private ToolStripButton toolStripButtonTilePage;
      private PdfiumViewer.PdfViewer pdfViewer;

      public AdvToolbarPdfViewer()
      {
        this.InitializeComponent();
        this.InitializeHandlers();
        this.InitializeToolStrip();
        this.InitializeRenderer();
      }

      public IPdfDocument Document
      {
        get => this.pdfViewer.Document;
        set => this.pdfViewer.Document = value;
      }

      private bool IsFullDocument => this.toolStripTextBoxPage.Enabled;

      public void SetSize()
      {
        this.pdfViewer.Width = this.Width - 2;
        this.pdfViewer.Height = this.Height - this.toolStripPdfViewer.Height - 2;
      }

      public void ShowDocument(
        PrintCenterNode node,
        bool showWithLayout = true,
        Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermarkSettings = null)
      {
        this._node = (PrintCenterNode) null;
        this._layout = (IPdfPageProducer) null;
        switch (node)
        {
          case WorkspaceObjectTreeNode _:
          case WorkspacePagesTreeNode _:
          case PrintQueuePagesNode _:
            if (!File.Exists(node.FilePath))
              break;
            this._node = node;
            this.pdfViewer.Document?.Dispose();
            PdfDocument document = PdfDocument.Load(node.FilePath);
            this.pdfViewer.Renderer.Show();
            this.toolStripLabelPages.Text = " / " + document.PageCount.ToString();
            List<PageInterval> pages1 = new List<PageInterval>();
            if (node.Pages != null)
            {
              pages1 = PageIntervalsUtils.GetPages(node.Pages);
              document.GetCorrectPages(pages1);
            }
            if (node is PrintQueuePagesNode && node.Parent is LayoutNode parent)
            {
              this._layout = showWithLayout ? parent.Layout : (IPdfPageProducer) new LayoutAsItIs();
              List<PageInterval> pages2 = pages1;
              document = PdfDocumentUtils.MakePdfDocumentWithChosenLayout(node.FilePath, pages2, this._layout, watermarkSettings);
              if (document == null)
              {
                this.HideContent();
                break;
              }
              this.toolStripLabelPages.Text = " / " + document.PageCount.ToString();
            }
            this.toolStripTextBoxPage.Enabled = node is WorkspaceObjectTreeNode;
            this.pdfViewer.Document = (IPdfDocument) document;
            this.pdfViewer.ZoomMode = PdfViewerZoomMode.FitBest;
            this.pdfViewer.Renderer.Zoom = 1.0;
            this.UpdateViewer();
            break;
          default:
            this.HideContent();
            break;
        }
      }

      public void HideContent()
      {
        this.pdfViewer.Renderer.Hide();
        this.EnableLeafButtons(false);
        this.EnableZoomButtons(false);
        this.toolStripTextBoxPage.Text = "";
        this.toolStripTextBoxPage.Enabled = false;
        this.toolStripLabelPages.Text = " / ";
      }

      private void InitializeHandlers()
      {
        this.pdfViewer.Renderer.MouseMove += new MouseEventHandler(this.OnMouseMoveDocument);
        this.pdfViewer.Renderer.MouseWheel += new MouseEventHandler(this.OnMouseMoveDocument);
        this.pdfViewer.Renderer.Scroll += new ScrollEventHandler(this.OnScrollDocument);
      }

      private void InitializeToolStrip()
      {
        this.EnableLeafButtons(false);
        this.EnableZoomButtons(false);
      }

      private void InitializeRenderer()
      {
        this.toolStripPdfViewer.Renderer = (ToolStripRenderer) new BordersToolStripRenderer();
      }

      private void OnMouseMoveDocument(object sender, MouseEventArgs e) => this.UpdateViewer();

      private void OnScrollDocument(object sender, ScrollEventArgs e) => this.UpdateViewer();

      private void ToolStripButtonFirstPage_Click(object sender, EventArgs e)
      {
        this.pdfViewer.Renderer.Page = 0;
        this.UpdateViewer();
      }

      private void ToolStripButtonPreviousPage_Click(object sender, EventArgs e)
      {
        if (this.pdfViewer.Renderer.Page <= 0)
          return;
        --this.pdfViewer.Renderer.Page;
        this.UpdateViewer();
      }

      private void ToolStripButtonNextPage_Click(object sender, EventArgs e)
      {
        if (this.pdfViewer.Renderer.Page >= this.Document.PageCount - 1)
          return;
        ++this.pdfViewer.Renderer.Page;
        this.UpdateViewer();
      }

      private void ToolStripButtonLastPage_Click(object sender, EventArgs e)
      {
        this.pdfViewer.Renderer.Page = this.Document.PageCount - 1;
        this.UpdateViewer();
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

      private void ToolStripButtonZoom_Click(object sender, EventArgs e)
      {
        this.pdfViewer.Renderer.ZoomIn();
      }

      private void ToolStripButtonUnzoom_Click(object sender, EventArgs e)
      {
        this.pdfViewer.Renderer.ZoomOut();
      }

      private void ToolStripButtonTilePage_Click(object sender, EventArgs e)
      {
        int page = this.pdfViewer.Renderer.Page;
        this.pdfViewer.ZoomMode = PdfViewerZoomMode.FitBest;
        this.pdfViewer.Renderer.Zoom = 1.0;
        this.pdfViewer.Renderer.Page = page;
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
        this.toolStripButtonZoom.Enabled = value;
        this.toolStripButtonUnzoom.Enabled = value;
        this.toolStripButtonTilePage.Enabled = value;
      }

      private void SetPageToTextBox()
      {
        if (this.IsFullDocument || this._layout != null || !(this._layout is LayoutAsItIs) && this._layout != null)
        {
          this.toolStripTextBoxPage.Text = (this.pdfViewer.Renderer.Page + 1).ToString();
        }
        else
        {
          List<PageInterval> pages = PageIntervalsUtils.GetPages(this._node.Pages);
          int num1 = this.pdfViewer.Renderer.Page + 1;
          int num2 = 0;
          foreach (PageInterval pageInterval in pages)
          {
            num1 -= pageInterval.End - pageInterval.Begin + 1;
            if (num1 <= 0)
            {
              for (int end = pageInterval.End; end >= pageInterval.Begin; --end)
              {
                if (num1 == 0)
                {
                  num2 = end;
                  break;
                }
                ++num1;
              }
              break;
            }
          }
          this.toolStripTextBoxPage.Text = num2.ToString();
        }
      }

      private void TrySetNewPage()
      {
        int result;
        if (int.TryParse(this.toolStripTextBoxPage.Text, out result) && result >= 1 && result <= this.Document.PageCount)
          this.pdfViewer.Renderer.Page = result - 1;
        else
          this.toolStripTextBoxPage.Text = (this.pdfViewer.Renderer.Page + 1).ToString();
      }

      private void UpdateViewer()
      {
        if (this.Document == null)
          return;
        this.SetPageToTextBox();
        this.CheckButtons();
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.pdfViewer = new PdfiumViewer.PdfViewer();
        this.toolStripPdfViewer = new ToolStrip();
        this.toolStripButtonFirstPage = new ToolStripButton();
        this.toolStripButtonPreviousPage = new ToolStripButton();
        this.toolStripTextBoxPage = new ToolStripTextBox();
        this.toolStripLabelPages = new ToolStripLabel();
        this.toolStripButtonNextPage = new ToolStripButton();
        this.toolStripButtonLastPage = new ToolStripButton();
        this.toolStripSeparator = new ToolStripSeparator();
        this.toolStripButtonTilePage = new ToolStripButton();
        this.toolStripButtonUnzoom = new ToolStripButton();
        this.toolStripButtonZoom = new ToolStripButton();
        this.toolStripPdfViewer.SuspendLayout();
        this.SuspendLayout();
        this.pdfViewer.Location = new Point(0, 25);
        this.pdfViewer.Name = "pdfViewer";
        this.pdfViewer.ShowBookmarks = false;
        this.pdfViewer.ShowToolbar = false;
        this.pdfViewer.Size = new Size(569, 430);
        this.pdfViewer.TabIndex = 1;
        this.toolStripPdfViewer.BackColor = Color.Transparent;
        this.toolStripPdfViewer.GripMargin = new Padding(0);
        this.toolStripPdfViewer.Items.AddRange(new ToolStripItem[10]
        {
          (ToolStripItem) this.toolStripButtonFirstPage,
          (ToolStripItem) this.toolStripButtonPreviousPage,
          (ToolStripItem) this.toolStripTextBoxPage,
          (ToolStripItem) this.toolStripLabelPages,
          (ToolStripItem) this.toolStripButtonNextPage,
          (ToolStripItem) this.toolStripButtonLastPage,
          (ToolStripItem) this.toolStripSeparator,
          (ToolStripItem) this.toolStripButtonTilePage,
          (ToolStripItem) this.toolStripButtonUnzoom,
          (ToolStripItem) this.toolStripButtonZoom
        });
        this.toolStripPdfViewer.Location = new Point(0, 0);
        this.toolStripPdfViewer.Name = "toolStripPdfViewer";
        this.toolStripPdfViewer.Size = new Size(591, 25);
        this.toolStripPdfViewer.TabIndex = 0;
        this.toolStripPdfViewer.Text = "toolStripPdfViewer";
        this.toolStripButtonFirstPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
        this.toolStripButtonFirstPage.Image = (Image) Resources.PNG_First;
        this.toolStripButtonFirstPage.ImageTransparentColor = Color.Magenta;
        this.toolStripButtonFirstPage.Name = "toolStripButtonFirstPage";
        this.toolStripButtonFirstPage.Size = new Size(23, 22);
        this.toolStripButtonFirstPage.Text = "Первая страница";
        this.toolStripButtonFirstPage.Click += new EventHandler(this.ToolStripButtonFirstPage_Click);
        this.toolStripButtonPreviousPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
        this.toolStripButtonPreviousPage.Image = (Image) Resources.PNG_Previous;
        this.toolStripButtonPreviousPage.ImageTransparentColor = Color.Magenta;
        this.toolStripButtonPreviousPage.Name = "toolStripButtonPreviousPage";
        this.toolStripButtonPreviousPage.Size = new Size(23, 22);
        this.toolStripButtonPreviousPage.Text = "Предыдущая страница";
        this.toolStripButtonPreviousPage.Click += new EventHandler(this.ToolStripButtonPreviousPage_Click);
        this.toolStripTextBoxPage.Enabled = false;
        this.toolStripTextBoxPage.Name = "toolStripTextBoxPage";
        this.toolStripTextBoxPage.Size = new Size(30, 25);
        this.toolStripTextBoxPage.LostFocus += new EventHandler(this.ToolStripTextBoxPage_LostFocus);
        this.toolStripTextBoxPage.KeyPress += new KeyPressEventHandler(this.ToolStripTextBoxPage_KeyPress);
        this.toolStripLabelPages.AutoSize = false;
        this.toolStripLabelPages.Name = "toolStripLabelPages";
        this.toolStripLabelPages.Size = new Size(42, 22);
        this.toolStripLabelPages.Text = " /";
        this.toolStripLabelPages.TextAlign = ContentAlignment.MiddleLeft;
        this.toolStripButtonNextPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
        this.toolStripButtonNextPage.Image = (Image) Resources.PNG_Next;
        this.toolStripButtonNextPage.ImageTransparentColor = Color.Magenta;
        this.toolStripButtonNextPage.Name = "toolStripButtonNextPage";
        this.toolStripButtonNextPage.Size = new Size(23, 22);
        this.toolStripButtonNextPage.Text = "Следующая страница";
        this.toolStripButtonNextPage.Click += new EventHandler(this.ToolStripButtonNextPage_Click);
        this.toolStripButtonLastPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
        this.toolStripButtonLastPage.Image = (Image) Resources.PNG_Last;
        this.toolStripButtonLastPage.ImageTransparentColor = Color.Magenta;
        this.toolStripButtonLastPage.Name = "toolStripButtonLastPage";
        this.toolStripButtonLastPage.Size = new Size(23, 22);
        this.toolStripButtonLastPage.Text = "Последняя страница";
        this.toolStripButtonLastPage.Click += new EventHandler(this.ToolStripButtonLastPage_Click);
        this.toolStripSeparator.Name = "toolStripSeparator";
        this.toolStripSeparator.Size = new Size(6, 25);
        this.toolStripButtonTilePage.Alignment = ToolStripItemAlignment.Right;
        this.toolStripButtonTilePage.DisplayStyle = ToolStripItemDisplayStyle.Image;
        this.toolStripButtonTilePage.Image = (Image) Resources.PNG_ShowAll;
        this.toolStripButtonTilePage.ImageTransparentColor = Color.Magenta;
        this.toolStripButtonTilePage.Name = "toolStripButtonTilePage";
        this.toolStripButtonTilePage.Size = new Size(23, 22);
        this.toolStripButtonTilePage.Text = "По ширине страницы";
        this.toolStripButtonTilePage.Click += new EventHandler(this.ToolStripButtonTilePage_Click);
        this.toolStripButtonUnzoom.Alignment = ToolStripItemAlignment.Right;
        this.toolStripButtonUnzoom.DisplayStyle = ToolStripItemDisplayStyle.Image;
        this.toolStripButtonUnzoom.Image = (Image) Resources.PNG_Away;
        this.toolStripButtonUnzoom.ImageTransparentColor = Color.Magenta;
        this.toolStripButtonUnzoom.Name = "toolStripButtonUnzoom";
        this.toolStripButtonUnzoom.Size = new Size(23, 22);
        this.toolStripButtonUnzoom.Text = "Отдалить";
        this.toolStripButtonUnzoom.Click += new EventHandler(this.ToolStripButtonUnzoom_Click);
        this.toolStripButtonZoom.Alignment = ToolStripItemAlignment.Right;
        this.toolStripButtonZoom.DisplayStyle = ToolStripItemDisplayStyle.Image;
        this.toolStripButtonZoom.Image = (Image) Resources.PNG_Closer;
        this.toolStripButtonZoom.ImageTransparentColor = Color.Magenta;
        this.toolStripButtonZoom.Name = "toolStripButtonZoom";
        this.toolStripButtonZoom.Size = new Size(23, 22);
        this.toolStripButtonZoom.Text = "Приблизить";
        this.toolStripButtonZoom.Click += new EventHandler(this.ToolStripButtonZoom_Click);
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = SystemColors.AppWorkspace;
        this.BorderStyle = BorderStyle.FixedSingle;
        this.Controls.Add((Control) this.pdfViewer);
        this.Controls.Add((Control) this.toolStripPdfViewer);
        this.Name = nameof (AdvToolbarPdfViewer);
        this.Size = new Size(591, 453);
        this.toolStripPdfViewer.ResumeLayout(false);
        this.toolStripPdfViewer.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
      }
    }
}
