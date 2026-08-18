// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.PageNumberingDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Controls;
using Intermech.Interfaces.Document;
using Intermech.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>
/// Класс диалога для выбора количества дополнительных листов для вставки и стиля нумерации.
/// В режиме удаления позволяет управлять опциями удаления.
/// </summary>
public class PageNumberingDlg : Form
{
  private const byte MAX_ADDPAGE_COUNT = 100;
  private readonly bool isNumberingStyleStrictlyDefined;
  private PageNumExtensionStyle numberingStyle = PageNumExtensionStyle.Unknown;
  private readonly bool useAdditionMode = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnOK;
  private Label lblPageCount;
  private TextBox tbPageCount;
  private FlatButton btnDigitAfterDot;
  private FlatButton btnLetter;
  private Label lblNumberingStyle;
  private TabControlAdvanced tbcMainPanel;
  private TabPage tbpAdd;
  private TabPage tbpRemove;
  private Label label1;
  private RadioButton rbAll;
  private RadioButton rbCurrentRange;
  private RadioButton rbActivePage;

  public PageNumExtensionStyle NumberingStyle
  {
    get => this.numberingStyle;
    set
    {
      if (value == this.numberingStyle || this.isNumberingStyleStrictlyDefined)
        return;
      this.numberingStyle = value;
      this.UpdateControls();
    }
  }

  /// <summary>Количество листов для вставки</summary>
  public byte PageCount => Convert.ToByte(this.tbPageCount.Text);

  /// <summary>Тип выборки дополнительных листов для удаления</summary>
  public PageSelectionType PageSelection
  {
    get
    {
      if (this.rbActivePage.Checked)
        return PageSelectionType.ActivePage;
      if (this.rbCurrentRange.Checked)
        return PageSelectionType.CurrentRange;
      return !this.rbAll.Checked ? PageSelectionType.None : PageSelectionType.All;
    }
  }

  public PageNumberingDlg(string activePageNumber, bool useForAdditionMode = true)
  {
    this.useAdditionMode = useForAdditionMode;
    this.InitializeComponent();
    this.Text = (useForAdditionMode ? "Вставка" : "Удаление") + " дополнительных листов";
    this.tbcMainPanel.ShowTabHeaders = false;
    this.tbcMainPanel.SelectedTab = useForAdditionMode ? this.tbpAdd : this.tbpRemove;
    this.NumberingStyle = PageNumberingHelper.GetNumberingStyle(activePageNumber);
    if (this.NumberingStyle == PageNumExtensionStyle.None)
      this.NumberingStyle = useForAdditionMode ? PageNumExtensionStyle.DigitsAfterDot : this.NumberingStyle;
    else
      this.isNumberingStyleStrictlyDefined = true;
  }

  public PageNumberingDlg(PageNumExtensionStyle numberingStyle)
  {
    this.useAdditionMode = true;
    this.InitializeComponent();
    this.Text = "Нумерация дополнительных листов";
    this.tbcMainPanel.ShowTabHeaders = false;
    this.tbcMainPanel.SelectedTab = this.tbpAdd;
    if (numberingStyle == PageNumExtensionStyle.None)
    {
      this.NumberingStyle = PageNumExtensionStyle.DigitsAfterDot;
    }
    else
    {
      this.NumberingStyle = numberingStyle;
      this.isNumberingStyleStrictlyDefined = false;
    }
  }

  /// <summary>
  /// Показать диалог вставки доп. страниц, вернуть номера для новых страниц
  /// </summary>
  /// <param name="activePageNumber">Номер (с расширением, если есть) текущей страницы</param>
  /// <param name="pageNumbers">Получаемый список номеров страниц для вставки</param>
  /// <returns>Результат диалога</returns>
  public static DialogResult ExecuteAdd(string activePageNumber, out string[] pageNumbers)
  {
    PageNumberingDlg pageNumberingDlg = new PageNumberingDlg(activePageNumber);
    int num = (int) pageNumberingDlg.ShowDialog();
    if (num == 1)
    {
      pageNumbers = PageNumberingHelper.GetAdditionalPageNumbers(activePageNumber, pageNumberingDlg.PageCount, pageNumberingDlg.NumberingStyle);
      return (DialogResult) num;
    }
    pageNumbers = (string[]) null;
    return (DialogResult) num;
  }

  /// <summary>
  /// Показать диалог вставки доп. страниц, вернуть номера для новых страниц
  /// </summary>
  /// <param name="activePageNumber">Номер (с расширением, если есть) текущей страницы</param>
  /// <param name="pageNumbers">Получаемый список номеров страниц для вставки</param>
  /// <returns>Результат диалога</returns>
  public static DialogResult ExecuteRemove(
    string activePageNumber,
    out PageSelectionType pageSelectionType)
  {
    PageNumberingDlg pageNumberingDlg = new PageNumberingDlg(activePageNumber, false);
    int num = (int) pageNumberingDlg.ShowDialog();
    if (num == 1)
    {
      pageSelectionType = pageNumberingDlg.PageSelection;
      return (DialogResult) num;
    }
    pageSelectionType = PageSelectionType.None;
    return (DialogResult) num;
  }

  /// <summary>
  /// Обновить состояние элементов интерфейса в зависимости от режима
  /// </summary>
  protected void UpdateControls()
  {
    if (this.useAdditionMode)
    {
      byte result = 0;
      this.btnOK.Enabled = byte.TryParse(this.tbPageCount.Text, out result) && result > (byte) 0 && result <= (byte) 100 && this.NumberingStyle != PageNumExtensionStyle.None && this.NumberingStyle != PageNumExtensionStyle.Unknown;
      this.btnDigitAfterDot.FlatAppearance.BorderSize = this.numberingStyle == PageNumExtensionStyle.DigitsAfterDot ? 2 : 1;
      this.btnDigitAfterDot.FlatAppearance.BorderColor = this.numberingStyle == PageNumExtensionStyle.DigitsAfterDot ? SystemColors.Highlight : SystemColors.ControlDark;
      this.btnLetter.FlatAppearance.BorderSize = this.numberingStyle == PageNumExtensionStyle.Letter ? 2 : 1;
      this.btnLetter.FlatAppearance.BorderColor = this.numberingStyle == PageNumExtensionStyle.Letter ? SystemColors.Highlight : SystemColors.ControlDark;
    }
    else
    {
      this.rbCurrentRange.Enabled = this.rbCurrentRange.Checked = this.numberingStyle != 0;
      this.rbAll.Checked = !this.rbCurrentRange.Checked;
    }
  }

  private void tbProductNumber_TextChanged(object sender, EventArgs e) => this.UpdateControls();

  private void btnDigitAfterDot_Click(object sender, EventArgs e)
  {
    this.NumberingStyle = PageNumExtensionStyle.DigitsAfterDot;
    this.UpdateControls();
  }

  private void btnLetter_Click(object sender, EventArgs e)
  {
    this.NumberingStyle = PageNumExtensionStyle.Letter;
    this.UpdateControls();
  }

  private void tbPageCount_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
      return;
    e.Handled = true;
  }

  internal static DialogResult ExecuteChangeStyle(ref PageNumExtensionStyle numberingStyle)
  {
    PageNumberingDlg pageNumberingDlg = new PageNumberingDlg(numberingStyle);
    int num = (int) pageNumberingDlg.ShowDialog();
    if (num == 1)
    {
      numberingStyle = pageNumberingDlg.NumberingStyle;
      return (DialogResult) num;
    }
    numberingStyle = PageNumExtensionStyle.None;
    return (DialogResult) num;
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
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.tbcMainPanel = new TabControlAdvanced();
    this.tbpAdd = new TabPage();
    this.lblNumberingStyle = new Label();
    this.btnLetter = new FlatButton();
    this.btnDigitAfterDot = new FlatButton();
    this.tbPageCount = new TextBox();
    this.lblPageCount = new Label();
    this.tbpRemove = new TabPage();
    this.label1 = new Label();
    this.rbAll = new RadioButton();
    this.rbCurrentRange = new RadioButton();
    this.rbActivePage = new RadioButton();
    this.tbcMainPanel.SuspendLayout();
    this.tbpAdd.SuspendLayout();
    this.tbpRemove.SuspendLayout();
    this.SuspendLayout();
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.FlatStyle = FlatStyle.System;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(140, 119);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 3;
    this.btnCancel.Text = "О&тмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.FlatStyle = FlatStyle.System;
    this.btnOK.ImeMode = ImeMode.NoControl;
    this.btnOK.Location = new Point(15, 119);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(121, 27);
    this.btnOK.TabIndex = 2;
    this.btnOK.Text = "&ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.tbcMainPanel.Controls.Add((Control) this.tbpAdd);
    this.tbcMainPanel.Controls.Add((Control) this.tbpRemove);
    this.tbcMainPanel.Dock = DockStyle.Top;
    this.tbcMainPanel.Location = new Point(0, 0);
    this.tbcMainPanel.Name = "tbcMainPanel";
    this.tbcMainPanel.SelectedIndex = 0;
    this.tbcMainPanel.ShowTabHeaders = true;
    this.tbcMainPanel.Size = new Size(274, 120);
    this.tbcMainPanel.TabIndex = 9;
    this.tbpAdd.BackColor = SystemColors.Control;
    this.tbpAdd.Controls.Add((Control) this.lblNumberingStyle);
    this.tbpAdd.Controls.Add((Control) this.btnLetter);
    this.tbpAdd.Controls.Add((Control) this.btnDigitAfterDot);
    this.tbpAdd.Controls.Add((Control) this.tbPageCount);
    this.tbpAdd.Controls.Add((Control) this.lblPageCount);
    this.tbpAdd.Location = new Point(4, 22);
    this.tbpAdd.Name = "tbpAdd";
    this.tbpAdd.Padding = new Padding(3);
    this.tbpAdd.Size = new Size(272, 94);
    this.tbpAdd.TabIndex = 0;
    this.tbpAdd.Text = "Add";
    this.lblNumberingStyle.AutoSize = true;
    this.lblNumberingStyle.Location = new Point(9, 12);
    this.lblNumberingStyle.Name = "lblNumberingStyle";
    this.lblNumberingStyle.Size = new Size(133, 13);
    this.lblNumberingStyle.TabIndex = 8;
    this.lblNumberingStyle.Text = "Выбор стиля нумерации:";
    this.btnLetter.BackColor = SystemColors.Window;
    this.btnLetter.FlatAppearance.BorderColor = SystemColors.ControlDark;
    this.btnLetter.FlatAppearance.MouseDownBackColor = SystemColors.Window;
    this.btnLetter.FlatAppearance.MouseOverBackColor = SystemColors.Window;
    this.btnLetter.Location = new Point(138, 35);
    this.btnLetter.Name = "btnLetter";
    this.btnLetter.Size = new Size(121, 41);
    this.btnLetter.TabIndex = 7;
    this.btnLetter.Text = "Доп. литера          (3а, 3б, 3в, ...)";
    this.btnLetter.TextAlign = ContentAlignment.MiddleLeft;
    this.btnLetter.UseVisualStyleBackColor = false;
    this.btnLetter.Click += new EventHandler(this.btnLetter_Click);
    this.btnDigitAfterDot.BackColor = SystemColors.Window;
    this.btnDigitAfterDot.FlatAppearance.BorderColor = SystemColors.Highlight;
    this.btnDigitAfterDot.FlatAppearance.BorderSize = 2;
    this.btnDigitAfterDot.FlatAppearance.MouseDownBackColor = SystemColors.Window;
    this.btnDigitAfterDot.FlatAppearance.MouseOverBackColor = SystemColors.Window;
    this.btnDigitAfterDot.Location = new Point(12, 35);
    this.btnDigitAfterDot.Name = "btnDigitAfterDot";
    this.btnDigitAfterDot.Size = new Size(121, 41);
    this.btnDigitAfterDot.TabIndex = 6;
    this.btnDigitAfterDot.Text = "Цифра после точки (3.1, 3.2, 3.3, ...)";
    this.btnDigitAfterDot.TextAlign = ContentAlignment.MiddleLeft;
    this.btnDigitAfterDot.UseVisualStyleBackColor = false;
    this.btnDigitAfterDot.Click += new EventHandler(this.btnDigitAfterDot_Click);
    this.tbPageCount.Location = new Point(280, 24);
    this.tbPageCount.Name = "tbPageCount";
    this.tbPageCount.Size = new Size(247, 20);
    this.tbPageCount.TabIndex = 1;
    this.tbPageCount.Text = "1";
    this.tbPageCount.Visible = false;
    this.tbPageCount.TextChanged += new EventHandler(this.tbProductNumber_TextChanged);
    this.tbPageCount.KeyPress += new KeyPressEventHandler(this.tbPageCount_KeyPress);
    this.lblPageCount.AutoSize = true;
    this.lblPageCount.Location = new Point(280, 8);
    this.lblPageCount.Name = "lblPageCount";
    this.lblPageCount.Size = new Size(69, 13);
    this.lblPageCount.TabIndex = 0;
    this.lblPageCount.Text = "Количество:";
    this.lblPageCount.Visible = false;
    this.tbpRemove.BackColor = SystemColors.Control;
    this.tbpRemove.Controls.Add((Control) this.label1);
    this.tbpRemove.Controls.Add((Control) this.rbAll);
    this.tbpRemove.Controls.Add((Control) this.rbCurrentRange);
    this.tbpRemove.Controls.Add((Control) this.rbActivePage);
    this.tbpRemove.Location = new Point(4, 22);
    this.tbpRemove.Name = "tbpRemove";
    this.tbpRemove.Padding = new Padding(3);
    this.tbpRemove.Size = new Size(266, 94);
    this.tbpRemove.TabIndex = 1;
    this.tbpRemove.Text = "Remove";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(8, 12);
    this.label1.Name = "label1";
    this.label1.Size = new Size(199, 13);
    this.label1.TabIndex = 3;
    this.label1.Text = "Какие доп. листы требуется удалить?";
    this.rbAll.AutoSize = true;
    this.rbAll.Location = new Point(20, 60);
    this.rbAll.Name = "rbAll";
    this.rbAll.Size = new Size((int) sbyte.MaxValue, 17);
    this.rbAll.TabIndex = 2;
    this.rbAll.Text = "Во всем документе ";
    this.rbAll.UseVisualStyleBackColor = true;
    this.rbCurrentRange.AutoSize = true;
    this.rbCurrentRange.Checked = true;
    this.rbCurrentRange.Location = new Point(20, 37);
    this.rbCurrentRange.Name = "rbCurrentRange";
    this.rbCurrentRange.Size = new Size(147, 17);
    this.rbCurrentRange.TabIndex = 1;
    this.rbCurrentRange.TabStop = true;
    this.rbCurrentRange.Text = "Из текущего диапазона";
    this.rbCurrentRange.UseVisualStyleBackColor = true;
    this.rbActivePage.AutoSize = true;
    this.rbActivePage.Location = new Point(280, 37);
    this.rbActivePage.Name = "rbActivePage";
    this.rbActivePage.Size = new Size(110, 17);
    this.rbActivePage.TabIndex = 0;
    this.rbActivePage.Text = "Выбранный лист";
    this.rbActivePage.UseVisualStyleBackColor = true;
    this.rbActivePage.Visible = false;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(274, 156);
    this.Controls.Add((Control) this.tbcMainPanel);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MaximumSize = new Size(290, 195);
    this.MinimizeBox = false;
    this.MinimumSize = new Size(290, 195);
    this.Name = nameof (PageNumberingDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Дополнительный лист";
    this.tbcMainPanel.ResumeLayout(false);
    this.tbpAdd.ResumeLayout(false);
    this.tbpAdd.PerformLayout();
    this.tbpRemove.ResumeLayout(false);
    this.tbpRemove.PerformLayout();
    this.ResumeLayout(false);
  }
}
