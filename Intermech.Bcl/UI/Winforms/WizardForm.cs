
// Type: Intermech.UI.Winforms.WizardForm
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    /// <summary>
    /// Реализует базу для создания мастеров в виде диалогового окна.
    /// </summary>
    public class WizardForm : Form, IWizard
    {
      private static readonly string NextCaption = LocalizationHolder.rm.GetString("Client.Core_1500");
      private static readonly string FinishCaption = LocalizationHolder.rm.GetString("SR_1664");
      private Image defaultPageImage;
      private WizardPages pages;
      private ProposedPage activePage;
      private List<ProposedPage> path;
      /// <summary>Required designer variable.</summary>
      private IContainer components;
      private Panel pnHeader;
      private Label lbDescription;
      private Label lbCaption;
      private PictureBox pbImage;
      private Panel pnFooter;
      private Button btBackward;
      private Button btForward;
      private Button btCancel;
      private Panel pnTopLightLine;
      private Panel pnPage;
      private Panel pnBottomDarktLine;
      private Panel pnTopDarkLine;
      private Panel pnBottomLightLine;

      /// <summary>Создает мастера.</summary>
      public WizardForm()
      {
        this.InitializeComponent();
        this.pages = new WizardPages(this);
        this.path = new List<ProposedPage>();
      }

      /// <summary>
      /// Возвращает или устанавливает изображение для страниц мастера, используемое в том случае,
      /// если страница не имеет своего изображения.
      /// </summary>
      public Image DefaultPageImage
      {
        get => this.defaultPageImage;
        set => this.defaultPageImage = value;
      }

      /// <summary>
      /// Возвращает страницу, с которой начинается работа мастера.
      /// </summary>
      /// <returns>Страница мастера</returns>
      protected virtual ProposedPage GetFirstPage()
      {
        return this.pages.Count <= 0 ? (ProposedPage) null : new ProposedPage(this.pages[0], true, this.pages.Count == 1);
      }

      /// <summary>
      /// Возвращает предыдущую страницу по отношению к текущей.
      /// </summary>
      /// <returns>Страница мастера</returns>
      protected virtual ProposedPage GetPreviousPage()
      {
        return this.path.Count <= 0 ? (ProposedPage) null : this.path[this.path.Count - 1];
      }

      /// <summary>Возвращает следующую страницу по отношению к текущей.</summary>
      /// <returns>Страница мастера</returns>
      protected virtual ProposedPage GetNextPage()
      {
        int num = this.pages.IndexOf(this.ActivePage);
        if (num < 0)
          return (ProposedPage) null;
        int index = num + 1;
        return new ProposedPage(this.pages[index], false, index + 1 == this.pages.Count);
      }

      /// <summary>Выполняет переключение страниц мастера.</summary>
      /// <param name="proposed">Новая страница, на которую выполняется переключение</param>
      /// <param name="rollback">True, если передвижение осуществляется по кнопке "Назад"</param>
      protected void ChangePage(ProposedPage proposed, bool rollback)
      {
        IWizardPage activePage = this.ActivePage;
        IWizardPage page = proposed.Page;
        if (activePage != null)
        {
          activePage.PageComplete -= new EventHandler<PageCompleteEventArgs>(this.OnActivePageComplete);
          activePage.Deactivate(page, rollback);
          this.pnPage.Controls.Remove(activePage.Control);
          this.lbCaption.Text = string.Empty;
          this.lbDescription.Text = string.Empty;
          this.pbImage.Image = this.defaultPageImage;
          this.btBackward.Enabled = false;
          this.btForward.Text = WizardForm.NextCaption;
          this.btForward.Enabled = false;
        }
        this.activePage = proposed;
        if (page == null)
          return;
        if (page.Control.GetControlStyle(ControlStyles.SupportsTransparentBackColor))
          page.Control.BackColor = Color.Transparent;
        page.Control.Dock = DockStyle.Fill;
        this.pnPage.Controls.Add(page.Control);
        this.lbCaption.Text = page.Caption;
        this.lbDescription.Text = page.Description;
        this.pbImage.Image = page.Image != null ? page.Image : this.defaultPageImage;
        this.btBackward.Enabled = !proposed.FirstPage;
        this.btForward.Enabled = page.ReallyComplete;
        this.btForward.Text = proposed.FinishPage ? WizardForm.FinishCaption : WizardForm.NextCaption;
        page.PageComplete += new EventHandler<PageCompleteEventArgs>(this.OnActivePageComplete);
        page.Activate(activePage, rollback);
        page.Control.Focus();
      }

      private void OnActivePageComplete(object sender, PageCompleteEventArgs e)
      {
        this.btForward.Enabled = e.IsComplete;
      }

      private void OnWizardShown(object sender, EventArgs e)
      {
        if (this.DesignMode)
          return;
        this.ChangePage(this.GetFirstPage() ?? throw this.NewBadMethodException(LocalizationHolder.rm.GetString("SR_1659"), "GetFirstPage"), false);
      }

      private void OnBackwardClick(object sender, EventArgs e)
      {
        ProposedPage previousPage = this.GetPreviousPage();
        if (previousPage == null)
          throw this.NewBadMethodException(LocalizationHolder.rm.GetString("SR_1660"), "GetPreviousPage");
        this.path.RemoveAt(this.path.Count - 1);
        this.ChangePage(previousPage, true);
      }

      private void OnForwardClick(object sender, EventArgs e)
      {
        if (!this.activePage.Page.ReallyComplete)
          return;
        this.activePage.Page.DoMagic();
        if (this.activePage.FinishPage)
        {
          this.DialogResult = DialogResult.OK;
        }
        else
        {
          ProposedPage nextPage = this.GetNextPage();
          if (nextPage == null)
            throw this.NewBadMethodException(LocalizationHolder.rm.GetString("SR_1661"), "GetNextPage");
          this.path.Add(this.activePage);
          this.ChangePage(nextPage, false);
        }
      }

      /// <summary>
      /// Формирует и возвращает исключение, которое можно использовать при неправильной реализации методов
      /// этого класса в его наследниках.
      /// </summary>
      /// <param name="what">Описание произошедшего</param>
      /// <param name="method">В каком методе случилась неприятность</param>
      /// <returns>Исключительная ситуация</returns>
      private Exception NewBadMethodException(string what, string method)
      {
        return new Exception(string.Format(LocalizationHolder.rm.GetString("SR_1662"), (object) what, (object) method));
      }

      /// <summary>Возвращате коллекцию страниц мастера.</summary>
      public IList<IWizardPage> Pages => (IList<IWizardPage>) this.pages;

      /// <summary>Возвращает активную страницу в мастере.</summary>
      public IWizardPage ActivePage
      {
        get => this.activePage == null ? (IWizardPage) null : this.activePage.Page;
      }

      /// <summary>Отображение панели с заголовком</summary>
      public bool ShowHeaderPanel
      {
        get => this.pnHeader.Visible;
        set => this.pnHeader.Visible = value;
      }

      /// <summary>Clean up any resources being used.</summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
        {
          for (int index = 0; index < this.pages.Count; ++index)
          {
            if (this.pages[index] is IDisposable page)
              page.Dispose();
          }
          this.components.Dispose();
        }
        base.Dispose(disposing);
      }

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (WizardForm));
        this.pnHeader = new Panel();
        this.lbDescription = new Label();
        this.lbCaption = new Label();
        this.pbImage = new PictureBox();
        this.pnFooter = new Panel();
        this.btCancel = new Button();
        this.btBackward = new Button();
        this.btForward = new Button();
        this.pnTopLightLine = new Panel();
        this.pnPage = new Panel();
        this.pnBottomDarktLine = new Panel();
        this.pnTopDarkLine = new Panel();
        this.pnBottomLightLine = new Panel();
        this.pnHeader.SuspendLayout();
        ((ISupportInitialize) this.pbImage).BeginInit();
        this.pnFooter.SuspendLayout();
        this.SuspendLayout();
        this.pnHeader.BackColor = SystemColors.ControlLightLight;
        this.pnHeader.Controls.Add((Control) this.lbDescription);
        this.pnHeader.Controls.Add((Control) this.lbCaption);
        this.pnHeader.Controls.Add((Control) this.pbImage);
        componentResourceManager.ApplyResources((object) this.pnHeader, "pnHeader");
        this.pnHeader.Name = "pnHeader";
        componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
        this.lbDescription.Name = "lbDescription";
        componentResourceManager.ApplyResources((object) this.lbCaption, "lbCaption");
        this.lbCaption.Name = "lbCaption";
        componentResourceManager.ApplyResources((object) this.pbImage, "pbImage");
        this.pbImage.Name = "pbImage";
        this.pbImage.TabStop = false;
        this.pnFooter.BackColor = Color.Transparent;
        this.pnFooter.Controls.Add((Control) this.btCancel);
        this.pnFooter.Controls.Add((Control) this.btBackward);
        this.pnFooter.Controls.Add((Control) this.btForward);
        componentResourceManager.ApplyResources((object) this.pnFooter, "pnFooter");
        this.pnFooter.Name = "pnFooter";
        componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
        this.btCancel.DialogResult = DialogResult.Cancel;
        this.btCancel.Name = "btCancel";
        this.btCancel.UseVisualStyleBackColor = true;
        componentResourceManager.ApplyResources((object) this.btBackward, "btBackward");
        this.btBackward.Name = "btBackward";
        this.btBackward.UseVisualStyleBackColor = true;
        this.btBackward.Click += new EventHandler(this.OnBackwardClick);
        componentResourceManager.ApplyResources((object) this.btForward, "btForward");
        this.btForward.Name = "btForward";
        this.btForward.UseVisualStyleBackColor = true;
        this.btForward.Click += new EventHandler(this.OnForwardClick);
        this.pnTopLightLine.BackColor = SystemColors.ControlLightLight;
        componentResourceManager.ApplyResources((object) this.pnTopLightLine, "pnTopLightLine");
        this.pnTopLightLine.Name = "pnTopLightLine";
        this.pnPage.BackColor = Color.Transparent;
        componentResourceManager.ApplyResources((object) this.pnPage, "pnPage");
        this.pnPage.Name = "pnPage";
        this.pnBottomDarktLine.BackColor = SystemColors.ControlDark;
        this.pnBottomDarktLine.BorderStyle = BorderStyle.Fixed3D;
        componentResourceManager.ApplyResources((object) this.pnBottomDarktLine, "pnBottomDarktLine");
        this.pnBottomDarktLine.Name = "pnBottomDarktLine";
        this.pnTopDarkLine.BackColor = SystemColors.ControlDark;
        componentResourceManager.ApplyResources((object) this.pnTopDarkLine, "pnTopDarkLine");
        this.pnTopDarkLine.Name = "pnTopDarkLine";
        this.pnBottomLightLine.BackColor = SystemColors.ControlLightLight;
        componentResourceManager.ApplyResources((object) this.pnBottomLightLine, "pnBottomLightLine");
        this.pnBottomLightLine.Name = "pnBottomLightLine";
        this.AcceptButton = (IButtonControl) this.btForward;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = (IButtonControl) this.btCancel;
        this.Controls.Add((Control) this.pnPage);
        this.Controls.Add((Control) this.pnBottomDarktLine);
        this.Controls.Add((Control) this.pnBottomLightLine);
        this.Controls.Add((Control) this.pnFooter);
        this.Controls.Add((Control) this.pnTopLightLine);
        this.Controls.Add((Control) this.pnTopDarkLine);
        this.Controls.Add((Control) this.pnHeader);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = nameof (WizardForm);
        this.ShowInTaskbar = false;
        this.SizeGripStyle = SizeGripStyle.Show;
        this.Shown += new EventHandler(this.OnWizardShown);
        this.pnHeader.ResumeLayout(false);
        ((ISupportInitialize) this.pbImage).EndInit();
        this.pnFooter.ResumeLayout(false);
        this.ResumeLayout(false);
      }

      private class WizardPages : Collection<IWizardPage>
      {
        private WizardForm owner;

        public WizardPages(WizardForm owner) => this.owner = owner;

        protected override void ClearItems()
        {
          for (int index = 0; index < this.Items.Count; ++index)
            this.Items[index].Wizard = (IWizard) null;
          base.ClearItems();
        }

        protected override void InsertItem(int index, IWizardPage item)
        {
          this.CheckItem(item);
          base.InsertItem(index, item);
          item.Wizard = (IWizard) this.owner;
        }

        protected override void RemoveItem(int index)
        {
          IWizardPage wizardPage = this.Items[index];
          base.RemoveItem(index);
          wizardPage.Wizard = (IWizard) null;
        }

        protected override void SetItem(int index, IWizardPage item)
        {
          this.CheckItem(item);
          IWizardPage wizardPage = this.Items[index];
          base.SetItem(index, item);
          wizardPage.Wizard = (IWizard) null;
          item.Wizard = (IWizard) this.owner;
        }

        private void CheckItem(IWizardPage item)
        {
          if (item == null)
            throw new ArgumentNullException(nameof (item), LocalizationHolder.rm.GetString("SR_1663"));
        }
      }
    }
}
