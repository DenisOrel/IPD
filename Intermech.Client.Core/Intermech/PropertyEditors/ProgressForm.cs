
// Type: Intermech.PropertyEditors.ProgressForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>
/// Форма для отображения информации с индикатором прогресса
/// </summary>
/// <summary>
/// Форма для показа прогресс-бара (чтобы бедный юзер не соскучился)
/// </summary>
public class ProgressForm : Form
{
  /// <summary>Можно ли закрыть форму</summary>
  public bool CanCloseForm;
  /// <summary>Требуется для дизайнера форм</summary>
  private IContainer components;
  private Button btnCancel;
  private Label lbPromt;
  private ProgressBar bar;

  /// <summary>
  /// Статический метод для вызова формы. Создаёт, заполняет и показывает (с полной перерисовкой) форму
  /// </summary>
  /// <param name="FormCaption">Заголовок формы</param>
  /// <param name="FormPromt">Поясняющий текст</param>
  /// <param name="Value">Текущее значение прогресс-бара</param>
  /// <param name="ValueMax">Максимальное значение прогресс-бара</param>
  /// <param name="ShowCancelButton">true, если надо показать кнопку "Отмена"</param>
  /// <param name="CancelButtonCaption">Заголовок кнопки "Отмена"</param>
  /// <param name="CancelButtonHandler">Обработчик события для кнопки "Отмена"</param>
  /// <returns>Вернёт ссылку на вновь созданную и показанную форму</returns>
  public static ProgressForm Execute(
    string FormCaption,
    string FormPromt,
    int Value,
    int ValueMax,
    bool ShowCancelButton,
    string CancelButtonCaption,
    EventHandler CancelButtonHandler)
  {
    ProgressForm progressForm = new ProgressForm(FormCaption, FormPromt, Value, ValueMax, ShowCancelButton, CancelButtonCaption, CancelButtonHandler);
    progressForm.Show();
    progressForm.Invalidate();
    progressForm.Update();
    return progressForm;
  }

  /// <summary>Пересчитать размер формы для указанного текста</summary>
  /// <param name="text">Текст</param>
  /// <param name="ShowCancelButton">Показывать ли кнопку</param>
  private void ResizeForm(string text, bool ShowCancelButton)
  {
    Size size1 = this.CalculateTextBounds((Control) this, text).ToSize();
    int height1 = this.ClientRectangle.Height;
    int num1 = this.Bounds.Width - this.ClientRectangle.Width;
    int num2 = this.Bounds.Height - height1;
    int num3 = this.btnCancel.Height;
    int height2 = this.bar.Height;
    if (!ShowCancelButton)
      num3 = 0;
    Size size2 = new Size(size1.Width + 10 + num1, size1.Height + height2 + num3 + 20 + num2);
    if (this.Size != size2)
      this.Size = size2;
    this.lbPromt.Text = text;
    this.lbPromt.Location = new Point(5, 5);
    this.lbPromt.Size = size1;
    ProgressBar bar = this.bar;
    Rectangle clientRectangle = this.ClientRectangle;
    int num4 = clientRectangle.Height - height2 - num3 - 10;
    bar.Top = num4;
    Button btnCancel1 = this.btnCancel;
    clientRectangle = this.ClientRectangle;
    int num5 = (clientRectangle.Width - this.btnCancel.Width) / 2;
    btnCancel1.Left = num5;
    Button btnCancel2 = this.btnCancel;
    clientRectangle = this.ClientRectangle;
    int num6 = clientRectangle.Height - this.btnCancel.Height - 5;
    btnCancel2.Top = num6;
  }

  /// <summary>Конструктор</summary>
  /// <param name="FormCaption">Заголовок формы</param>
  /// <param name="FormPromt">Поясняющий текст</param>
  /// <param name="Value">Текущее значение прогресс-бара</param>
  /// <param name="ValueMax">Максимальное значение прогресс-бара</param>
  /// <param name="ShowCancelButton">true, если надо показать кнопку "Отмена"</param>
  /// <param name="CancelButtonCaption">Заголовок кнопки "Отмена"</param>
  /// <param name="CancelButtonHandler">Обработчик события для кнопки "Отмена"</param>
  public ProgressForm(
    string FormCaption,
    string FormPromt,
    int Value,
    int ValueMax,
    bool ShowCancelButton,
    string CancelButtonCaption,
    EventHandler CancelButtonHandler)
  {
    this.InitializeComponent();
    this.ResizeForm(FormPromt, ShowCancelButton);
    this.Text = FormCaption;
    this.btnCancel.Text = CancelButtonCaption;
    this.btnCancel.Visible = ShowCancelButton;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Click += CancelButtonHandler;
    this.bar.Minimum = 0;
    if (ValueMax > 0)
      this.bar.Maximum = ValueMax;
    if (this.bar.Maximum < Value)
      return;
    this.bar.Value = Value;
  }

  /// <summary>Значение для прогресс-бара</summary>
  public int ProgressValue
  {
    get => this.bar.Value;
    set => this.SetProgressValue(value);
  }

  /// <summary>Максимальное значение для прогресс-бара</summary>
  public int Maximum
  {
    get => this.bar.Maximum;
    set => this.bar.Maximum = value;
  }

  /// <summary>Установить новое значение для прогресс-бара</summary>
  /// <param name="Value">Значение для прогресс-бара</param>
  public void SetProgressValue(int Value)
  {
    if (this.bar.Maximum < Value)
      return;
    this.bar.Value = Value;
  }

  /// <summary>Установить новое значение для прогресс-бара, текст</summary>
  /// <param name="Value">Значение для прогресс-бара</param>
  /// <param name="text">Текст</param>
  public void SetProgressValue(int Value, string text)
  {
    this.ResizeForm(text, this.btnCancel.Visible);
    try
    {
      if (this.bar.Maximum < Value)
        return;
      this.bar.Value = Value;
    }
    finally
    {
      this.Invalidate();
      this.Update();
    }
  }

  /// <summary>Рассчитать ширину и высоту текста</summary>
  /// <param name="control">Контрол</param>
  /// <param name="text">Текст</param>
  /// <returns>Ширина и высота текста</returns>
  private SizeF CalculateTextBounds(Control control, string text)
  {
    using (Graphics graphics = this.CreateGraphics())
    {
      int width = Screen.PrimaryScreen.WorkingArea.Width / 100 * 50;
      return graphics.MeasureString(text, control.Font, width, StringFormat.GenericDefault);
    }
  }

  /// <summary>Можно ли закрывать форму</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.btnCancel.Visible)
      return;
    e.Cancel = !this.CanCloseForm || this.bar.Value == this.bar.Maximum;
  }

  /// <summary>Уберём за собой мусор</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProgressForm));
    this.btnCancel = new Button();
    this.lbPromt = new Label();
    this.bar = new ProgressBar();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Hand;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.lbPromt.FlatStyle = FlatStyle.System;
    componentResourceManager.ApplyResources((object) this.lbPromt, "lbPromt");
    this.lbPromt.Name = "lbPromt";
    componentResourceManager.ApplyResources((object) this.bar, "bar");
    this.bar.Name = "bar";
    this.bar.Style = ProgressBarStyle.Continuous;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.bar);
    this.Controls.Add((Control) this.lbPromt);
    this.Controls.Add((Control) this.btnCancel);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ProgressForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.TopMost = true;
    this.FormClosing += new FormClosingEventHandler(this.ProgressForm_FormClosing);
    this.ResumeLayout(false);
  }
}
