
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrPassword
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
[Designer(typeof (AttrPasswordControlDesigner))]
[RefreshProperties(RefreshProperties.All)]
public class AttrPassword : AttrsControl
{
  private ControlButton _btnDots;
  private string _password = string.Empty;
  private bool _changedPass;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox _txt;

  /// <summary>Цвет фона элемента управления.</summary>
  [DefaultValue(typeof (Color), "Control")]
  public new Color BackColor
  {
    get => this._txt.BackColor;
    set => this._txt.BackColor = value;
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [DefaultValue(BorderStyle.Fixed3D)]
  public new BorderStyle BorderStyle
  {
    get => this._txt.BorderStyle;
    set => this._txt.BorderStyle = value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => this._txt.Font;
    set => this._txt.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [DefaultValue(typeof (Color), "WindowText")]
  public new Color ForeColor
  {
    get => this._txt.ForeColor;
    set => this._txt.ForeColor = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this._txt);
    set => this._toolTip.SetToolTip((Control) this._txt, value);
  }

  /// <summary>Текст, связанный с элементом управления.</summary>
  [DefaultValue("0123456789")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public override string Text
  {
    get => this._txt.Text;
    set => this._txt.Text = value;
  }

  /// <summary>Выравнивание текста в элементе управления.</summary>
  [DefaultValue(HorizontalAlignment.Left)]
  public HorizontalAlignment TextAlign
  {
    get => this._txt.TextAlign;
    set => this._txt.TextAlign = value;
  }

  /// <summary>Максимальная длина текста.</summary>
  [DefaultValue(32767 /*0x7FFF*/)]
  public int MaxLength
  {
    get => this._txt.MaxLength;
    set => this._txt.MaxLength = value;
  }

  /// <summary>Кодовый символ.</summary>
  [DefaultValue('\0')]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public char PasswordChar
  {
    get => this._txt.PasswordChar;
    set => this._txt.PasswordChar = value;
  }

  /// <summary>Использование системного кодового символа.</summary>
  [DefaultValue(false)]
  public bool UseSystemPasswordChar
  {
    get => this._txt.UseSystemPasswordChar;
    set => this._txt.UseSystemPasswordChar = value;
  }

  /// <summary>Отступы от краев в элементе управления.</summary>
  /// <remarks>Здесь нужно только для того, чтобы запреить сериализацию</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Padding Padding
  {
    get => base.Padding;
    private set => base.Padding = value;
  }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected override object[] GetValues
  {
    get
    {
      return !string.IsNullOrEmpty(this._password) ? new object[1]
      {
        (object) this._password
      } : new object[1]{ (object) DBNull.Value };
    }
  }

  /// <summary>Конструктор.</summary>
  public AttrPassword()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this._txt.GotFocus += new EventHandler(this.On_txt_GotFocus);
    this._txt.LostFocus += new EventHandler(this.On_txt_LostFocus);
    this._btnDots = new ControlButton("Dots", 0)
    {
      Enabled = false
    };
    this._btnDots.Click += new EventHandler(this.On_btn_Click);
    this.AddRightButton(this._btnDots);
    this.PasswordChar = ClientConsts.PasswordChar;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btn_Click(object sender, EventArgs e)
  {
    if (this.ParentInfo == null)
      return;
    using (PasswordDlg passwordDlg = new PasswordDlg(this.ParentInfo.ElementIdentifier, this.ParentInfo.ElementKind, this.AttributeInfo.AttributeGuid))
    {
      if (passwordDlg.ShowDialog() != DialogResult.OK || this._attrValues == null)
        return;
      this._password = passwordDlg.Password;
      this._changedPass = true;
      if (string.IsNullOrEmpty(this._password))
        this.Error = !this._disableNulls || !this.EnabledCtrl ? string.Empty : this._errMsg_NullValue;
      else
        this.Error = string.Empty;
      this.On_txt_TextChanged((object) this._txt, (EventArgs) null);
    }
  }

  /// <summary>Фокусирование текстового контрола.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_GotFocus(object sender, EventArgs e) => this.Error = string.Empty;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_LostFocus(object sender, EventArgs e)
  {
    if (this._attrValues == null)
      return;
    string empty = string.Empty;
    this.Error = !this._disableNulls || !this.EnabledCtrl || this._attrValues.Values[0] != DBNull.Value ? string.Empty : this._errMsg_NullValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_SizeChanged(object sender, EventArgs e)
  {
    this.Height = this._txt == null || this._txt.Height < 20 ? 22 : this._txt.Height + 2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_TextChanged(object sender, EventArgs e) => this.Modified = true;

  /// <summary>Доступность контрола.</summary>
  [DefaultValue(true)]
  public override bool EnabledCtrl
  {
    get => this._enabled;
    set
    {
      this._enabled = value;
      this._txt.ReadOnly = !value;
      this._btnDots.Enabled = value;
      if (this.Site != null && this.Site.DesignMode)
        return;
      Color color = this._txt.BackColor;
      int argb1 = color.ToArgb();
      color = SystemColors.Window;
      int argb2 = color.ToArgb();
      if (argb1 != argb2)
        return;
      this._txt.BackColor = SystemColors.Control;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLeaveControl(EventArgs e)
  {
    if (!this._changedPass)
      return;
    base.OnLeaveControl(e);
    this._changedPass = false;
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont() => !base.Font.Equals((object) this._txt.Font);

  private void _txt_KeyDown(object sender, KeyEventArgs e) => e.SuppressKeyPress = true;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._txt.SizeChanged -= new EventHandler(this.On_txt_SizeChanged);
      this._txt.KeyDown -= new KeyEventHandler(this._txt_KeyDown);
      this._txt.GotFocus -= new EventHandler(this.On_txt_GotFocus);
      this._txt.LostFocus -= new EventHandler(this.On_txt_LostFocus);
      if (this._btnDots != null && !this.IsDesignMode)
        this._btnDots.Click -= new EventHandler(this.On_btn_Click);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrPassword));
    this._txt = new TextBox();
    ((ISupportInitialize) this._err).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._txt, "_txt");
    this._txt.Name = "_txt";
    this._txt.ReadOnly = true;
    this._txt.SizeChanged += new EventHandler(this.On_txt_SizeChanged);
    this._txt.KeyDown += new KeyEventHandler(this._txt_KeyDown);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._txt);
    this._err.SetIconAlignment((Control) this, (ErrorIconAlignment) componentResourceManager.GetObject("$this.IconAlignment"));
    this._err.SetIconPadding((Control) this, (int) componentResourceManager.GetObject("$this.IconPadding"));
    this.Name = nameof (AttrPassword);
    ((ISupportInitialize) this._err).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
