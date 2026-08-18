
// Type: Intermech.Client.Core.FormDesigner.Controls.IMDateTimeCtrl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Контрол-редактор даты/времени.</summary>
[Designer(typeof (IMDateTimeCtrlDesigner))]
public class IMDateTimeCtrl : UserControl, IDataFormatError
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private System.Windows.Forms.TextBox _txt;
  private ImageList _imgList;
  private ErrorProvider _err;
  private Color NormalBorder = Color.FromArgb(122, 122, 122);
  private Color HotBorder = Color.FromArgb(23, 23, 23);
  private Color FocusedBorder = Color.FromArgb(0, 120, 215);
  private Color PressedBorder = Color.FromArgb(0, 84, 153);
  private Color DisabledBorder = Color.FromArgb(204, 204, 204);
  private Color HotBackColor = Color.FromArgb(229, 241, 251);
  private Color PressedBackColor = Color.FromArgb(204, 228, 247);
  private Color DisabledBackColor = Color.FromArgb(240 /*0xF0*/, 240 /*0xF0*/, 240 /*0xF0*/);
  private IMDateTimeCtrl.ControlButton _btn;
  private Form _calendarForm;
  private Timer _timer;
  private bool _hot;
  private DateTime _dtValue = DateTime.MinValue;
  private string _strBeforeOpen = string.Empty;
  /// <summary>
  /// Custom значит формат атрибута из конфигуратора, Time - значение заданное пользователем в редакторе форм
  /// </summary>
  private DateTimePickerFormat _format = DateTimePickerFormat.Custom;
  /// <summary>
  /// Хранится формат, заданный пользователем в редакторе форм
  /// </summary>
  private string _customFormat = "dd.MM.yyyy  H:mm";
  private bool _lock;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._txt.SizeChanged -= new EventHandler(this.On_txt_SizeChanged);
      this._txt.TextChanged -= new EventHandler(this.On_txt_TextChanged);
      this._txt.Enter -= new EventHandler(this.On_txt_Enter);
      this._txt.Leave -= new EventHandler(this.On_txt_Leave);
      this._txt.MouseHover -= new EventHandler(this.On_txt_MouseHover);
      this._timer.Tick -= new EventHandler(this.On_timer_Tick);
      if (this._btn != null)
      {
        this._btn.Click -= new EventHandler(this.On_button_Click);
        this._btn.Dispose();
        this._btn = (IMDateTimeCtrl.ControlButton) null;
      }
      if (this._calendarForm != null)
      {
        this._calendarForm.Deactivate -= new EventHandler(this.On_form_Deactivate);
        if (this._calendarForm.Controls.Count > 0)
        {
          MonthCalendar control = this._calendarForm.Controls[0] as MonthCalendar;
          control.PreviewKeyDown -= new PreviewKeyDownEventHandler(this.On_calendar_PreviewKeyDown);
          control.KeyDown -= new KeyEventHandler(this.On_calendar_KeyDown);
          control.DateSelected -= new DateRangeEventHandler(this.On_calendar_DateSelected);
          control.DateChanged -= new DateRangeEventHandler(this.On_calendar_DateChanged);
          control.Dispose();
        }
        this._calendarForm.Dispose();
        this._calendarForm = (Form) null;
      }
      if (this.components != null)
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IMDateTimeCtrl));
    this._txt = new System.Windows.Forms.TextBox();
    this._imgList = new ImageList(this.components);
    this._err = new ErrorProvider(this.components);
    ((ISupportInitialize) this._err).BeginInit();
    this.SuspendLayout();
    this._txt.BorderStyle = BorderStyle.None;
    this._txt.Dock = DockStyle.Fill;
    this._txt.Location = new Point(3, 3);
    this._txt.Name = "_txt";
    this._txt.Size = new Size(113, 13);
    this._txt.TabIndex = 0;
    this._txt.SizeChanged += new EventHandler(this.On_txt_SizeChanged);
    this._txt.TextChanged += new EventHandler(this.On_txt_TextChanged);
    this._txt.Enter += new EventHandler(this.On_txt_Enter);
    this._txt.Leave += new EventHandler(this.On_txt_Leave);
    this._txt.MouseHover += new EventHandler(this.On_txt_MouseHover);
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Transparent;
    this._imgList.Images.SetKeyName(0, "PictEnabled.png");
    this._imgList.Images.SetKeyName(1, "PictDisabled.png");
    this._err.BlinkStyle = ErrorBlinkStyle.NeverBlink;
    this._err.ContainerControl = (ContainerControl) this;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Window;
    this.Controls.Add((Control) this._txt);
    this.DoubleBuffered = true;
    this._err.SetIconAlignment((Control) this, ErrorIconAlignment.MiddleLeft);
    this._err.SetIconPadding((Control) this, -16);
    this.Name = nameof (IMDateTimeCtrl);
    this.Padding = new Padding(3, 3, 34, 1);
    this.Size = new Size(150, 20);
    ((ISupportInitialize) this._err).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Текущий формат данных.</summary>
  protected string CurrentFormat { get; set; }

  /// <summary>Строка пользовательского формата даты/времени.</summary>
  [DefaultValue("dd.MM.yyyy  H:mm")]
  public string CustomFormat
  {
    get => this._customFormat;
    set
    {
      this._customFormat = value;
      this.UpdateCurrentFormat();
      if (!this.IsDesignMode)
        return;
      this.DateTimeValue = DateTime.Now;
    }
  }

  /// <summary>
  /// Формат даты и времени, отображаемых в элементе управления.
  /// </summary>
  [DefaultValue(DateTimePickerFormat.Custom)]
  public DateTimePickerFormat Format
  {
    get => this._format;
    set
    {
      this._format = value;
      this.UpdateCurrentFormat();
      if (!this.IsDesignMode)
        return;
      this.DateTimeValue = DateTime.Now;
    }
  }

  /// <summary>Расположение выпадающего календаря.</summary>
  [DefaultValue(LeftRightAlignment.Left)]
  public LeftRightAlignment DropDownAlign { get; set; }

  /// <summary>Находимся в редакторе форм.</summary>
  protected bool IsDesignMode => this.Site != null && this.Site.DesignMode;

  /// <summary>Введенное значение.</summary>
  protected DateTime DateTimeValue
  {
    get => this._dtValue;
    set
    {
      this._dtValue = value;
      this.TextValue = this.ConvertFromDateToString(this._dtValue);
    }
  }

  /// <summary>Строковое значение.</summary>
  protected string TextValue
  {
    get => this._txt.Text;
    set => this._txt.Text = value;
  }

  /// <summary>
  /// 
  /// </summary>
  private Form CalendarForm
  {
    get
    {
      if (this._calendarForm == null)
      {
        MonthCalendar monthCalendar1 = new MonthCalendar();
        monthCalendar1.Margin = new Padding(0);
        monthCalendar1.Location = new Point(-1, -1);
        monthCalendar1.TodayDate = DateTime.Now;
        MonthCalendar monthCalendar2 = monthCalendar1;
        monthCalendar2.PreviewKeyDown += new PreviewKeyDownEventHandler(this.On_calendar_PreviewKeyDown);
        monthCalendar2.KeyDown += new KeyEventHandler(this.On_calendar_KeyDown);
        monthCalendar2.DateSelected += new DateRangeEventHandler(this.On_calendar_DateSelected);
        monthCalendar2.DateChanged += new DateRangeEventHandler(this.On_calendar_DateChanged);
        this._calendarForm = new Form()
        {
          FormBorderStyle = FormBorderStyle.None,
          StartPosition = FormStartPosition.Manual,
          ShowInTaskbar = false
        };
        this._calendarForm.Controls.Add((Control) monthCalendar2);
        this._calendarForm.AutoSize = true;
        this._calendarForm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        this._calendarForm.Deactivate += new EventHandler(this.On_form_Deactivate);
      }
      return this._calendarForm;
    }
  }

  /// <summary>Конструктор.</summary>
  public IMDateTimeCtrl()
  {
    this.InitializeComponent();
    this._btn = new IMDateTimeCtrl.ControlButton(this);
    this._btn.Click += new EventHandler(this.On_button_Click);
    this.DropDownAlign = LeftRightAlignment.Left;
    this.DateTimeValue = DateTime.MinValue;
    this._timer = new Timer(this.components)
    {
      Interval = 200
    };
    this._timer.Tick += new EventHandler(this.On_timer_Tick);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_button_Click(object sender, EventArgs e)
  {
    this._strBeforeOpen = this.TextValue;
    this.Focus();
    this.OpenCalendar();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_form_Deactivate(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_Enter(object sender, EventArgs e)
  {
    this._err.SetError((Control) this, string.Empty);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_Leave(object sender, EventArgs e)
  {
    if (this._calendarForm == null || !this._calendarForm.Visible)
    {
      this.CheckTextValue();
      this.Invalidate();
      this.OnLeave();
    }
    else
    {
      if (this._calendarForm == null && !this._calendarForm.Visible)
        return;
      this.CloseCalendar(this._calendarForm);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_MouseHover(object sender, EventArgs e)
  {
    this._hot = true;
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_SizeChanged(object sender, EventArgs e)
  {
    if (this._txt.Height > 15)
    {
      int num = this._txt.Height == 16 /*0x10*/ ? 2 : 1;
      int left = this.Padding.Left;
      int top = num;
      Padding padding = this.Padding;
      int right = padding.Right;
      padding = this.Padding;
      int bottom = padding.Bottom;
      this.Padding = new Padding(left, top, right, bottom);
      if (this._txt.Height <= 17)
        return;
      this.Height = this._txt.Height + 2;
    }
    else
    {
      Padding padding = this.Padding;
      int left = padding.Left;
      padding = this.Padding;
      int right = padding.Right;
      padding = this.Padding;
      int bottom = padding.Bottom;
      this.Padding = new Padding(left, 3, right, bottom);
      this.Height = 20;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_TextChanged(object sender, EventArgs e) => this.OnTextChanged();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_calendar_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
  {
    e.IsInputKey = e.KeyCode == Keys.Tab;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_calendar_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Escape)
      return;
    this.TextValue = this._strBeforeOpen;
    this.CloseCalendar((sender as MonthCalendar).FindForm());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_calendar_DateSelected(object sender, DateRangeEventArgs e)
  {
    if (this._lock)
      return;
    MonthCalendar monthCalendar = sender as MonthCalendar;
    this.DateTimeValue = monthCalendar.SelectionStart;
    this.CloseCalendar(monthCalendar.FindForm());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_calendar_DateChanged(object sender, DateRangeEventArgs e)
  {
    if (this._lock)
      return;
    this.DateTimeValue = (sender as MonthCalendar).SelectionStart;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_timer_Tick(object sender, EventArgs e)
  {
    this._lock = false;
    this._timer.Stop();
    this._txt.Focus();
  }

  /// <summary>
  /// 
  /// </summary>
  public bool IsDataFormatError => !string.IsNullOrEmpty(this._err.GetError((Control) this));

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseHover(EventArgs e)
  {
    base.OnMouseHover(e);
    if (this.IsDesignMode)
      return;
    this._hot = true;
    if (this._btn.Bounds.Contains(this.PointToClient(Cursor.Position)))
      this._btn.MouseHover(e);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (this.IsDesignMode)
      return;
    if (this._btn.Bounds.Contains(this.PointToClient(Cursor.Position)))
      this._btn.MouseHover((EventArgs) e);
    else
      this.OnMouseLeave((EventArgs) e);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    if (this.IsDesignMode)
      return;
    this._hot = false;
    this._btn.MouseLeave(e);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    if (this._lock || this.IsDesignMode)
      return;
    if (this._btn.Bounds.Contains(this.PointToClient(Cursor.Position)))
      this._btn.MouseDown(e);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    this.DrawBorder(e.Graphics);
    this._btn.Bounds = new Rectangle(this.Width - this.Padding.Right, 0, this.Padding.Right, this.Height);
    this._btn.Draw(e.Graphics);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnSizeChanged(EventArgs e)
  {
    base.OnSizeChanged(e);
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnEnabledChanged(EventArgs e)
  {
    base.OnEnabledChanged(e);
    this.BackColor = this.Enabled ? SystemColors.Window : this.DisabledBackColor;
  }

  /// <summary>Отрисовка рамки котрола.</summary>
  /// <param name="g"></param>
  private void DrawBorder(Graphics g)
  {
    if (this.Enabled)
    {
      using (Pen pen = new Pen(this._txt.Focused || this._btn.State == PushButtonState.Pressed ? this.FocusedBorder : (this._hot ? this.HotBorder : this.NormalBorder)))
        g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
    }
    else
    {
      using (Pen pen = new Pen(this.DisabledBorder))
        g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
      using (Pen pen = new Pen(SystemColors.Window))
        g.DrawRectangle(pen, 1, 1, this.Width - 3, this.Height - 3);
    }
  }

  /// <summary>Открытие календаря.</summary>
  private void OpenCalendar()
  {
    Form calendarForm = this.CalendarForm;
    MonthCalendar control = calendarForm.Controls[0] as MonthCalendar;
    this._lock = true;
    try
    {
      DateTime dt;
      if (this.ConvertFromStringToDateTime(this.TextValue, out dt) && dt != DateTime.MinValue)
        control.SetDate(dt);
      else
        control.SetDate(DateTime.Now);
    }
    finally
    {
      this._lock = false;
    }
    if (!calendarForm.Visible)
    {
      calendarForm.Show((IWin32Window) this.ParentForm);
      Point screen = this.PointToScreen(new Point(0, 0));
      int x = this.DropDownAlign == LeftRightAlignment.Left ? screen.X : screen.X + this.Width - control.Width + 2;
      int y = screen.Y + this.Height;
      if (y + calendarForm.Height > SystemInformation.PrimaryMonitorSize.Height)
        y = screen.Y - calendarForm.Height;
      calendarForm.Location = new Point(x, y);
      control.Focus();
    }
    else
      calendarForm.Hide();
  }

  /// <summary>Закрытие календаря.</summary>
  /// <param name="form">Форма с календарем</param>
  private void CloseCalendar(Form form)
  {
    this._btn.State = PushButtonState.Normal;
    form?.Hide();
    this._txt.SelectionStart = this._txt.Text.Length;
    this.Invalidate();
  }

  /// <summary>
  /// Перевод даты в строковое значение, с учетом формата конвертации.
  /// </summary>
  /// <param name="DateTimeValue">Дата</param>
  /// <returns>Строковое значение даты</returns>
  private string ConvertFromDateToString(DateTime DateTimeValue)
  {
    string empty = string.Empty;
    if (DateTimeValue != DateTime.MinValue)
    {
      string format = this.CurrentFormat;
      if ((this.Format == DateTimePickerFormat.Custom || this.Format == DateTimePickerFormat.Time) && string.IsNullOrEmpty(format))
        format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern + (DateTimeValue.Hour > 0 || DateTimeValue.Minute > 0 || DateTimeValue.Second > 0 ? "  H:mm" : string.Empty);
      CultureInfo provider = string.IsNullOrEmpty(format) || this.Format == DateTimePickerFormat.Long ? CultureInfo.CurrentCulture : CultureInfo.InvariantCulture;
      empty = DateTimeValue.ToString(format, (IFormatProvider) provider);
    }
    return empty;
  }

  /// <summary>Проверка введенного тескта.</summary>
  protected void CheckTextValue()
  {
    string text = this._txt.Text;
    DateTime dt = DateTime.MinValue;
    bool dateTime = this.ConvertFromStringToDateTime(text, out dt);
    if (dateTime)
      this.DateTimeValue = dt;
    string str1 = string.Empty;
    if (!this.IsDesignMode && !this._txt.Focused)
    {
      if (!dateTime)
      {
        string str2 = this.ConvertFromDateToString(DateTime.Now);
        str1 = $"{LocalizationHolder.rm.GetString("Date_WrongFormat")} {str2}";
      }
      else
        str1 = this.CheckError();
    }
    this._err.SetError((Control) this, str1);
  }

  /// <summary>
  /// Конвертация введенного строкового значения в дату, с учетом указанного формата.
  /// </summary>
  /// <param name="text">Строковое значение даты</param>
  /// <param name="dt">Полученная дата</param>
  /// <returns>Результат конвертации</returns>
  protected bool ConvertFromStringToDateTime(string text, out DateTime dt)
  {
    bool dateTime = true;
    text = text.Trim();
    if (!string.IsNullOrEmpty(text))
    {
      if (!string.IsNullOrEmpty(this.CurrentFormat))
      {
        CultureInfo provider = this.Format == DateTimePickerFormat.Long ? CultureInfo.CurrentCulture : CultureInfo.InvariantCulture;
        dateTime = DateTime.TryParseExact(text, this.CurrentFormat, (IFormatProvider) provider, DateTimeStyles.None, out dt);
      }
      else
        dateTime = DateTime.TryParse(text, out dt);
    }
    else
      dt = DateTime.MinValue;
    return dateTime;
  }

  /// <summary>Проверка ошибок.</summary>
  /// <returns>Текст ошибки</returns>
  protected virtual string CheckError() => string.Empty;

  /// <summary>
  /// 
  /// </summary>
  protected virtual void OnLeave()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  protected virtual void OnTextChanged()
  {
  }

  /// <summary>Обновление текущего формата данных.</summary>
  protected virtual void UpdateCurrentFormat()
  {
    switch (this.Format)
    {
      case DateTimePickerFormat.Long:
        this.CurrentFormat = DateTimeFormatInfo.CurrentInfo.LongDatePattern;
        break;
      case DateTimePickerFormat.Short:
        this.CurrentFormat = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        break;
      case DateTimePickerFormat.Time:
        this.CurrentFormat = this._customFormat;
        break;
      case DateTimePickerFormat.Custom:
        this.CurrentFormat = string.Empty;
        break;
    }
  }

  /// <summary>Кнопка.</summary>
  private class ControlButton : IDisposable
  {
    private IMDateTimeCtrl _parent;
    private Image _pictEnabled;
    private Image _pictDisabled;

    /// <summary>Границы кнопки.</summary>
    public Rectangle Bounds { get; set; }

    /// <summary>Изображение на кнопке.</summary>
    private Image Img => !this._parent.Enabled ? this._pictDisabled : this._pictEnabled;

    /// <summary>Состояние кнопки.</summary>
    public PushButtonState State { get; set; }

    /// <summary>Конструктор.</summary>
    /// <param name="parent">Контрол</param>
    public ControlButton(IMDateTimeCtrl parent)
    {
      this._parent = parent;
      this.State = PushButtonState.Normal;
      this._pictEnabled = this._parent._imgList.Images[0];
      this._pictDisabled = this._parent._imgList.Images[1];
    }

    /// <summary>Нажатие кнопки.</summary>
    public event EventHandler Click;

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
      if (this._pictEnabled != null)
      {
        this._pictEnabled.Dispose();
        this._pictEnabled = (Image) null;
      }
      if (this._pictDisabled != null)
      {
        this._pictDisabled.Dispose();
        this._pictDisabled = (Image) null;
      }
      this._parent = (IMDateTimeCtrl) null;
    }

    /// <summary>Нажатие кнопки.</summary>
    protected virtual void OnClick(EventArgs e)
    {
      if (this.Click == null)
        return;
      this.Click((object) this, e);
    }

    /// <summary>Отрисовка кнопки.</summary>
    /// <param name="g"></param>
    public void Draw(Graphics g)
    {
      Color color1 = this._parent.NormalBorder;
      Color color2 = SystemColors.Window;
      if (this.State == PushButtonState.Hot)
      {
        color1 = this._parent.FocusedBorder;
        color2 = this._parent.HotBackColor;
      }
      else if (this.State == PushButtonState.Pressed)
      {
        color1 = this._parent.PressedBorder;
        color2 = this._parent.PressedBackColor;
      }
      using (Brush brush1 = (Brush) new SolidBrush(color2))
      {
        Graphics graphics = g;
        Brush brush2 = brush1;
        Rectangle bounds = this.Bounds;
        int x = bounds.X;
        bounds = this.Bounds;
        int y = bounds.Y + 1;
        bounds = this.Bounds;
        int width = bounds.Width - 1;
        bounds = this.Bounds;
        int height = bounds.Height - 2;
        Rectangle rect = new Rectangle(x, y, width, height);
        graphics.FillRectangle(brush2, rect);
      }
      if (this.State != PushButtonState.Normal)
      {
        using (Pen pen1 = new Pen(color1))
        {
          Graphics graphics = g;
          Pen pen2 = pen1;
          Rectangle bounds = this.Bounds;
          int x = bounds.X;
          bounds = this.Bounds;
          int y = bounds.Y;
          bounds = this.Bounds;
          int width = bounds.Width - 1;
          bounds = this.Bounds;
          int height = bounds.Height - 1;
          graphics.DrawRectangle(pen2, x, y, width, height);
        }
      }
      if (this.Img == null)
        return;
      Rectangle bounds1 = this.Bounds;
      int x1 = bounds1.X;
      bounds1 = this.Bounds;
      int num = bounds1.Width / 2 - this.Img.Size.Width / 2;
      int x2 = x1 + num;
      bounds1 = this.Bounds;
      int y1 = bounds1.Height / 2 - this.Img.Size.Height / 2;
      Rectangle rectangle = new Rectangle(new Point(x2, y1), this.Img.Size);
      g.DrawImage(this.Img, x2, y1);
    }

    /// <summary>Движение мыши над кнопкой.</summary>
    /// <param name="e"></param>
    public void MouseHover(EventArgs e)
    {
      if (this.State == PushButtonState.Pressed)
        return;
      this.State = PushButtonState.Hot;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    public void MouseLeave(EventArgs e)
    {
      if (this.State == PushButtonState.Pressed)
        return;
      this.State = PushButtonState.Normal;
    }

    /// <summary>Нажатие мыши по кнопке.</summary>
    /// <param name="e"></param>
    public void MouseDown(MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.State = this.State != PushButtonState.Pressed ? PushButtonState.Pressed : PushButtonState.Hot;
      this.OnClick(EventArgs.Empty);
    }
  }
}
