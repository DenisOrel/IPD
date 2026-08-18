
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.LayerView




using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.ComparisonPlugins.PDFComparison.UI
{
    public class LayerView : UserControl, ILayerView
    {
      private IContainer components;
      private TextBox textBoxFileCaption;
      private Button buttonNextPage;
      private Button buttonPrevPage;
      private Button buttonOpen;
      private TextBox textBoxPages;
      private Label label1;
      private TextBox textBoxColor;

      public event EventHandler ClickOpenButton;

      public event EventHandler ClickNextPageButton;

      public event EventHandler ClickPrevPageButton;

      public event EventHandler ChangedPageNumber;

      public int PageNumber { get; private set; }

      private int pageCount { get; set; }

      public LayerView()
      {
        this.InitializeComponent();
        this.PageNumber = 0;
        this.pageCount = 0;
        this.UpdateTextPageControl();
      }

      public void SetColor(Color color) => this.textBoxColor.BackColor = color;

      public void UpdateUI(string fileCaption, int pageNumber, int pageCount)
      {
        this.textBoxFileCaption.Text = fileCaption;
        this.PageNumber = pageNumber;
        this.pageCount = pageCount;
        this.UpdateTextPageControl();
      }

      private void UpdateTextPageControl()
      {
        this.textBoxPages.Text = $"{this.PageNumber}/{this.pageCount}";
      }

      private void buttonOpen_Click(object sender, EventArgs e)
      {
        EventHandler clickOpenButton = this.ClickOpenButton;
        if (clickOpenButton == null)
          return;
        clickOpenButton((object) null, EventArgs.Empty);
      }

      private void buttonPrevPage_Click(object sender, EventArgs e)
      {
        EventHandler clickPrevPageButton = this.ClickPrevPageButton;
        if (clickPrevPageButton == null)
          return;
        clickPrevPageButton((object) null, EventArgs.Empty);
      }

      private void buttonNextPage_Click(object sender, EventArgs e)
      {
        EventHandler clickNextPageButton = this.ClickNextPageButton;
        if (clickNextPageButton == null)
          return;
        clickNextPageButton((object) null, EventArgs.Empty);
      }

      private void textBoxPages_Enter(object sender, EventArgs e)
      {
        this.textBoxPages.Text = string.Empty;
      }

      private void textBoxPages_KeyDown(object sender, KeyEventArgs e)
      {
        if (e.KeyData != Keys.Return)
          return;
        int result;
        int.TryParse(this.textBoxPages.Text, out result);
        this.PageNumber = result;
        EventHandler changedPageNumber = this.ChangedPageNumber;
        if (changedPageNumber == null)
          return;
        changedPageNumber((object) null, EventArgs.Empty);
      }

      private void textBoxPages_KeyPress(object sender, KeyPressEventArgs e)
      {
        FormHelper.CheckEnterFormat(e);
      }

      private void textBoxPages_Leave(object sender, EventArgs e) => this.UpdateTextPageControl();

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.label1 = new Label();
        this.textBoxColor = new TextBox();
        this.textBoxPages = new TextBox();
        this.buttonNextPage = new Button();
        this.buttonPrevPage = new Button();
        this.buttonOpen = new Button();
        this.textBoxFileCaption = new TextBox();
        this.SuspendLayout();
        this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.label1.AutoSize = true;
        this.label1.Location = new Point(524, 6);
        this.label1.Name = "label1";
        this.label1.Size = new Size(35, 13);
        this.label1.TabIndex = 3;
        this.label1.Text = "Цвет:";
        this.textBoxColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.textBoxColor.Cursor = Cursors.No;
        this.textBoxColor.Location = new Point(562, 3);
        this.textBoxColor.Name = "textBoxColor";
        this.textBoxColor.ReadOnly = true;
        this.textBoxColor.Size = new Size(24, 20);
        this.textBoxColor.TabIndex = 2;
        this.textBoxPages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.textBoxPages.Location = new Point(473, 3);
        this.textBoxPages.Name = "textBoxPages";
        this.textBoxPages.Size = new Size(45, 20);
        this.textBoxPages.TabIndex = 2;
        this.textBoxPages.TextAlign = HorizontalAlignment.Center;
        this.textBoxPages.Enter += new EventHandler(this.textBoxPages_Enter);
        this.textBoxPages.KeyDown += new KeyEventHandler(this.textBoxPages_KeyDown);
        this.textBoxPages.KeyPress += new KeyPressEventHandler(this.textBoxPages_KeyPress);
        this.textBoxPages.Leave += new EventHandler(this.textBoxPages_Leave);
        this.buttonNextPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonNextPage.FlatStyle = FlatStyle.System;
        this.buttonNextPage.Location = new Point(443, 1);
        this.buttonNextPage.Name = "buttonNextPage";
        this.buttonNextPage.Size = new Size(24, 23);
        this.buttonNextPage.TabIndex = 1;
        this.buttonNextPage.Text = ">";
        this.buttonNextPage.UseVisualStyleBackColor = true;
        this.buttonNextPage.Click += new EventHandler(this.buttonNextPage_Click);
        this.buttonPrevPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonPrevPage.FlatStyle = FlatStyle.System;
        this.buttonPrevPage.Location = new Point(413, 1);
        this.buttonPrevPage.Name = "buttonPrevPage";
        this.buttonPrevPage.Size = new Size(24, 23);
        this.buttonPrevPage.TabIndex = 1;
        this.buttonPrevPage.Text = "<";
        this.buttonPrevPage.UseVisualStyleBackColor = true;
        this.buttonPrevPage.Click += new EventHandler(this.buttonPrevPage_Click);
        this.buttonOpen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonOpen.FlatStyle = FlatStyle.System;
        this.buttonOpen.Location = new Point(383, 1);
        this.buttonOpen.Name = "buttonOpen";
        this.buttonOpen.Size = new Size(24, 23);
        this.buttonOpen.TabIndex = 1;
        this.buttonOpen.Text = "...";
        this.buttonOpen.UseVisualStyleBackColor = true;
        this.buttonOpen.Click += new EventHandler(this.buttonOpen_Click);
        this.textBoxFileCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.textBoxFileCaption.Location = new Point(0, 3);
        this.textBoxFileCaption.Name = "textBoxFileCaption";
        this.textBoxFileCaption.ReadOnly = true;
        this.textBoxFileCaption.Size = new Size(377, 20);
        this.textBoxFileCaption.TabIndex = 0;
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Controls.Add((Control) this.textBoxColor);
        this.Controls.Add((Control) this.label1);
        this.Controls.Add((Control) this.textBoxFileCaption);
        this.Controls.Add((Control) this.textBoxPages);
        this.Controls.Add((Control) this.buttonOpen);
        this.Controls.Add((Control) this.buttonNextPage);
        this.Controls.Add((Control) this.buttonPrevPage);
        this.Name = nameof (LayerView);
        this.Size = new Size(594, 26);
        this.ResumeLayout(false);
        this.PerformLayout();
      }
    }
}
