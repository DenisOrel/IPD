
// Type: Intermech.Navigator.DBObjects.IndexSearchOptionsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Форма, позволяющая указать опции поиска</summary>
public class IndexSearchOptionsForm : Form
{
  /// <summary>Опции поиска</summary>
  private GlobalIndexSearchOptions options;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOK;
  private Label lbPromt;
  private Button btnCancel;
  private CheckBox cbOrderByRelevance;
  private CheckBox cbSubstringSearch;
  private CheckBox cbStemmedWords;

  /// <summary>Создать экземпляр класса</summary>
  public IndexSearchOptionsForm() => this.InitializeComponent();

  /// <summary>
  /// Создать экземпляр класса, заполнить элементы управдения, указать координаты и размеры окна
  /// </summary>
  /// <param name="options">Опции поиска</param>
  /// <param name="formBounds">Размер и границы формы</param>
  public IndexSearchOptionsForm(GlobalIndexSearchOptions options, Rectangle formBounds)
    : this()
  {
    this.Bounds = formBounds;
    this.options = options;
    this.FillCheckBoxex();
    this.btnOK.Top = this.ClientSize.Height - this.btnOK.Height - 10;
    this.btnCancel.Top = this.btnOK.Top;
  }

  /// <summary>Вызвать форму как модальное окно</summary>
  /// <param name="options">Опции поиска</param>
  /// <param name="formBounds">Размер и границы формы</param>
  /// <returns>Результ вызова формы</returns>
  public static DialogResult Execute(ref GlobalIndexSearchOptions options, Rectangle formBounds)
  {
    using (IndexSearchOptionsForm searchOptionsForm = new IndexSearchOptionsForm(options, formBounds))
    {
      int num = (int) searchOptionsForm.ShowDialog();
      if (num == 1)
      {
        searchOptionsForm.CaptureChanges((object) null, (EventArgs) null);
        options = searchOptionsForm.options;
        UISettings.SearchInIndexSubstring = searchOptionsForm.cbSubstringSearch.Checked;
      }
      return (DialogResult) num;
    }
  }

  /// <summary>Установить значения чек-боксам</summary>
  private void FillCheckBoxex()
  {
    this.cbOrderByRelevance.Checked = (this.options & GlobalIndexSearchOptions.OrderByRelevance) == GlobalIndexSearchOptions.OrderByRelevance;
    this.cbSubstringSearch.Checked = (this.options & GlobalIndexSearchOptions.SubstringSearch) == GlobalIndexSearchOptions.SubstringSearch;
    this.cbStemmedWords.Checked = (this.options & GlobalIndexSearchOptions.StemmedWords) == GlobalIndexSearchOptions.StemmedWords;
    this.cbOrderByRelevance.CheckedChanged += new EventHandler(this.CaptureChanges);
    this.cbSubstringSearch.CheckedChanged += new EventHandler(this.CaptureChanges);
    this.cbStemmedWords.CheckedChanged += new EventHandler(this.CaptureChanges);
    this.UpdateControls();
  }

  /// <summary>Обновить контролы</summary>
  private void UpdateControls()
  {
    this.btnOK.Enabled = true;
    this.btnCancel.Enabled = true;
  }

  /// <summary>Отпущена клавиша</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ContextSelectionForm_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyData == Keys.Escape)
      this.DialogResult = DialogResult.Cancel;
    if (e.KeyData != Keys.Return)
      return;
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>Захватить изменения из чек-боксов</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void CaptureChanges(object sender, EventArgs e)
  {
    this.options = GlobalIndexSearchOptions.None;
    if (this.cbOrderByRelevance.Checked)
      this.options |= GlobalIndexSearchOptions.OrderByRelevance;
    if (this.cbSubstringSearch.Checked)
      this.options |= GlobalIndexSearchOptions.SubstringSearch;
    if (!this.cbStemmedWords.Checked)
      return;
    this.options |= GlobalIndexSearchOptions.StemmedWords;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IndexSearchOptionsForm));
    this.btnOK = new Button();
    this.lbPromt = new Label();
    this.btnCancel = new Button();
    this.cbOrderByRelevance = new CheckBox();
    this.cbSubstringSearch = new CheckBox();
    this.cbStemmedWords = new CheckBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Cursor = Cursors.Hand;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    componentResourceManager.ApplyResources((object) this.lbPromt, "lbPromt");
    this.lbPromt.Name = "lbPromt";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Hand;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.cbOrderByRelevance, "cbOrderByRelevance");
    this.cbOrderByRelevance.Name = "cbOrderByRelevance";
    this.cbOrderByRelevance.Tag = (object) "1";
    this.cbOrderByRelevance.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbSubstringSearch, "cbSubstringSearch");
    this.cbSubstringSearch.Checked = true;
    this.cbSubstringSearch.CheckState = CheckState.Checked;
    this.cbSubstringSearch.Name = "cbSubstringSearch";
    this.cbSubstringSearch.Tag = (object) "2";
    this.cbSubstringSearch.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbStemmedWords, "cbStemmedWords");
    this.cbStemmedWords.Name = "cbStemmedWords";
    this.cbStemmedWords.Tag = (object) "4";
    this.cbStemmedWords.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ControlBox = false;
    this.Controls.Add((Control) this.cbStemmedWords);
    this.Controls.Add((Control) this.cbSubstringSearch);
    this.Controls.Add((Control) this.cbOrderByRelevance);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.lbPromt);
    this.Controls.Add((Control) this.btnCancel);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (IndexSearchOptionsForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
