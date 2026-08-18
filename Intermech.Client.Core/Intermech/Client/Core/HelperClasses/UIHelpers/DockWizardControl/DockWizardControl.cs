
// Type: Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Docking;
using Intermech.Localization;
using Intermech.UI.Winforms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl;

/// <summary>
/// Реализует базу для создания мастеров в виде DockControl
/// </summary>
public class DockWizardControl : DockControl, IWizard
{
  private static readonly string NextCaption = LocalizationHolder.rm.GetString("Client.Core_1686");
  private static readonly string FinishCaption = LocalizationHolder.rm.GetString("Client.Core_1687");
  private Image _defaultPageImage;
  private readonly Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl.WizardPages _pages;
  private ProposedPage _activePage;
  private readonly List<ProposedPage> _path;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel pnFooter;
  private Button btCancel;
  private Button btBackward;
  private Button btForward;
  private Panel pnHeader;
  private Label lbDescription;
  private Label lbCaption;
  private PictureBox pbImage;
  private Panel pnPage;

  /// <summary>Создает мастера.</summary>
  public DockWizardControl()
  {
    this.InitializeComponent();
    this._pages = new Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl.WizardPages(this);
    this._path = new List<ProposedPage>();
  }

  /// <summary>
  /// Возвращает или устанавливает изображение для страниц мастера, используемое в том случае,
  /// если страница не имеет своего изображения.
  /// </summary>
  public Image DefaultPageImage
  {
    get => this._defaultPageImage;
    set => this._defaultPageImage = value;
  }

  /// <summary>
  /// Возвращает страницу, с которой начинается работа мастера.
  /// </summary>
  /// <returns>Страница мастера</returns>
  protected virtual ProposedPage GetFirstPage()
  {
    return this._pages.Count <= 0 ? (ProposedPage) null : new ProposedPage(this._pages[0], true, this._pages.Count == 1);
  }

  /// <summary>
  /// Возвращает предыдущую страницу по отношению к текущей.
  /// </summary>
  /// <returns>Страница мастера</returns>
  protected virtual ProposedPage GetPreviousPage()
  {
    return this._path.Count <= 0 ? (ProposedPage) null : this._path[this._path.Count - 1];
  }

  /// <summary>Возвращает следующую страницу по отношению к текущей.</summary>
  /// <returns>Страница мастера</returns>
  protected virtual ProposedPage GetNextPage()
  {
    int num = this._pages.IndexOf(this.ActivePage);
    if (num < 0)
      return (ProposedPage) null;
    int index = num + 1;
    return new ProposedPage(this._pages[index], false, index + 1 == this._pages.Count);
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
      this.pbImage.Image = this._defaultPageImage;
      this.btBackward.Enabled = false;
      this.btForward.Text = Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl.NextCaption;
      this.btForward.Enabled = false;
    }
    this._activePage = proposed;
    if (page == null)
      return;
    if (page.Control.GetControlStyle(ControlStyles.SupportsTransparentBackColor))
      page.Control.BackColor = Color.Transparent;
    page.Control.Dock = DockStyle.Fill;
    this.pnPage.Controls.Add(page.Control);
    this.lbCaption.Text = page.Caption;
    this.lbDescription.Text = page.Description;
    this.pbImage.Image = page.Image != null ? page.Image : this._defaultPageImage;
    this.btBackward.Enabled = !proposed.FirstPage;
    this.btForward.Enabled = false;
    this.btForward.Text = proposed.FinishPage ? Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl.FinishCaption : Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl.NextCaption;
    this.btForward.Visible = this.ShowFinishButton || !proposed.FinishPage;
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
    this.ChangePage(this.GetFirstPage() ?? throw this.NewBadMethodException(LocalizationHolder.rm.GetString("Client.Core_1681"), "GetFirstPage"), false);
  }

  private void OnBackwardClick(object sender, EventArgs e) => this.GotoPreviousPage();

  private void OnForwardClick(object sender, EventArgs e) => this.GotoNextPage();

  private void btCancel_Click(object sender, EventArgs e) => this.Close();

  private void DockWizardControl_BeforeFirstShown(object sender, EventArgs e)
  {
    this.OnWizardShown(sender, e);
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
    return new Exception(string.Format(LocalizationHolder.rm.GetString("Client.Core_1684"), (object) what, (object) method));
  }

  /// <summary>Возвращает коллекцию страниц мастера.</summary>
  public IList<IWizardPage> Pages => (IList<IWizardPage>) this._pages;

  /// <summary>Возвращает активную страницу в мастере.</summary>
  public IWizardPage ActivePage
  {
    get => this._activePage == null ? (IWizardPage) null : this._activePage.Page;
  }

  /// <summary>Перейти на след. закладку</summary>
  public void GotoNextPage()
  {
    if (!this._activePage.Page.ReallyComplete)
      return;
    this._activePage.Page.DoMagic();
    if (this._activePage.FinishPage)
    {
      this.Close();
    }
    else
    {
      ProposedPage nextPage = this.GetNextPage();
      if (nextPage == null)
        throw this.NewBadMethodException(LocalizationHolder.rm.GetString("Client.Core_1683"), "GetNextPage");
      this._path.Add(this._activePage);
      this.ChangePage(nextPage, false);
    }
  }

  /// <summary>Вернуться на предыдущую закладку</summary>
  public void GotoPreviousPage()
  {
    ProposedPage previousPage = this.GetPreviousPage();
    if (previousPage == null)
      throw this.NewBadMethodException(LocalizationHolder.rm.GetString("Client.Core_1682"), "GetPreviousPage");
    this._path.RemoveAt(this._path.Count - 1);
    this.ChangePage(previousPage, true);
  }

  /// <summary>Отображение панели с заголовком</summary>
  public bool ShowHeaderPanel
  {
    get => this.pnHeader.Visible;
    set => this.pnHeader.Visible = value;
  }

  /// <summary>Отображение кнопки "Отмена"</summary>
  public bool ShowCancelButton
  {
    get => this.btCancel.Visible;
    set => this.btCancel.Visible = value;
  }

  /// <summary>Отображение кнопки "Готово"</summary>
  public bool ShowFinishButton { get; set; } = true;

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
    this.pnFooter = new Panel();
    this.btCancel = new Button();
    this.btBackward = new Button();
    this.btForward = new Button();
    this.pnHeader = new Panel();
    this.lbDescription = new Label();
    this.lbCaption = new Label();
    this.pbImage = new PictureBox();
    this.pnPage = new Panel();
    this.pnFooter.SuspendLayout();
    this.pnHeader.SuspendLayout();
    ((ISupportInitialize) this.pbImage).BeginInit();
    this.SuspendLayout();
    this.pnFooter.BackColor = Color.Transparent;
    this.pnFooter.Controls.Add((Control) this.btCancel);
    this.pnFooter.Controls.Add((Control) this.btBackward);
    this.pnFooter.Controls.Add((Control) this.btForward);
    this.pnFooter.Dock = DockStyle.Bottom;
    this.pnFooter.Location = new Point(0, 310);
    this.pnFooter.Margin = new Padding(0);
    this.pnFooter.Name = "pnFooter";
    this.pnFooter.Size = new Size(647, 55);
    this.pnFooter.TabIndex = 3;
    this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.ImeMode = ImeMode.NoControl;
    this.btCancel.Location = new Point(560, 20);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(75, 23);
    this.btCancel.TabIndex = 2;
    this.btCancel.Text = "Отмена";
    this.btCancel.UseVisualStyleBackColor = true;
    this.btCancel.Click += new EventHandler(this.btCancel_Click);
    this.btBackward.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btBackward.Enabled = false;
    this.btBackward.ImeMode = ImeMode.NoControl;
    this.btBackward.Location = new Point(398, 20);
    this.btBackward.Name = "btBackward";
    this.btBackward.Size = new Size(75, 23);
    this.btBackward.TabIndex = 0;
    this.btBackward.Text = "Назад <";
    this.btBackward.UseVisualStyleBackColor = true;
    this.btBackward.Click += new EventHandler(this.OnBackwardClick);
    this.btForward.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btForward.Enabled = false;
    this.btForward.ImeMode = ImeMode.NoControl;
    this.btForward.Location = new Point(479, 20);
    this.btForward.Name = "btForward";
    this.btForward.Size = new Size(75, 23);
    this.btForward.TabIndex = 1;
    this.btForward.Text = "Далее >";
    this.btForward.UseVisualStyleBackColor = true;
    this.btForward.Click += new EventHandler(this.OnForwardClick);
    this.pnHeader.BackColor = SystemColors.ControlLightLight;
    this.pnHeader.Controls.Add((Control) this.lbDescription);
    this.pnHeader.Controls.Add((Control) this.lbCaption);
    this.pnHeader.Controls.Add((Control) this.pbImage);
    this.pnHeader.Dock = DockStyle.Top;
    this.pnHeader.Location = new Point(0, 0);
    this.pnHeader.Margin = new Padding(0);
    this.pnHeader.Name = "pnHeader";
    this.pnHeader.Size = new Size(647, 67);
    this.pnHeader.TabIndex = 4;
    this.lbDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbDescription.ImeMode = ImeMode.NoControl;
    this.lbDescription.Location = new Point(32 /*0x20*/, 32 /*0x20*/);
    this.lbDescription.Margin = new Padding(0);
    this.lbDescription.Name = "lbDescription";
    this.lbDescription.Size = new Size(552, 30);
    this.lbDescription.TabIndex = 1;
    this.lbCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbCaption.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
    this.lbCaption.ImeMode = ImeMode.NoControl;
    this.lbCaption.Location = new Point(12, 9);
    this.lbCaption.Name = "lbCaption";
    this.lbCaption.Size = new Size(557, 23);
    this.lbCaption.TabIndex = 0;
    this.lbCaption.TextAlign = ContentAlignment.MiddleLeft;
    this.pbImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.pbImage.ImeMode = ImeMode.NoControl;
    this.pbImage.Location = new Point(587, 9);
    this.pbImage.Name = "pbImage";
    this.pbImage.Size = new Size(48 /*0x30*/, 48 /*0x30*/);
    this.pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
    this.pbImage.TabIndex = 0;
    this.pbImage.TabStop = false;
    this.pnPage.BackColor = Color.Transparent;
    this.pnPage.Dock = DockStyle.Fill;
    this.pnPage.Location = new Point(0, 67);
    this.pnPage.Margin = new Padding(0);
    this.pnPage.Name = "pnPage";
    this.pnPage.Size = new Size(647, 243);
    this.pnPage.TabIndex = 5;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnPage);
    this.Controls.Add((Control) this.pnHeader);
    this.Controls.Add((Control) this.pnFooter);
    this.Name = nameof (DockWizardControl);
    this.Size = new Size(647, 365);
    this.BeforeFirstShown += new EventHandler(this.DockWizardControl_BeforeFirstShown);
    this.pnFooter.ResumeLayout(false);
    this.pnHeader.ResumeLayout(false);
    ((ISupportInitialize) this.pbImage).EndInit();
    this.ResumeLayout(false);
  }

  private class WizardPages : Collection<IWizardPage>
  {
    private Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl _owner;

    public WizardPages(Intermech.Client.Core.HelperClasses.UIHelpers.DockWizardControl.DockWizardControl owner)
    {
      this._owner = owner;
    }

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
      item.Wizard = (IWizard) this._owner;
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
      item.Wizard = (IWizard) this._owner;
    }

    private void CheckItem(IWizardPage item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item), LocalizationHolder.rm.GetString("Client.Core_1685"));
    }
  }
}
