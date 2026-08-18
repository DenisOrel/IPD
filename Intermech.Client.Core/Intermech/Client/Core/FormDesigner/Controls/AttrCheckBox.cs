
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrCheckBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Контрол-редактор значений типа boolean.</summary>
[Designer(typeof (AttrCheckBoxControlDesigner))]
[RefreshProperties(RefreshProperties.All)]
public class AttrCheckBox : AttrsControl, IImageFromLibrary, IExpertSystemCtrl
{
  private Size _sizeBeforeAutoSize = new Size(0, 0);
  /// <summary>Использование атрибута в экспертной системе</summary>
  private bool _useInExpertSystem;
  private ControlButton _btnCalc;
  private ControlButton _btnReCalc;
  /// <summary>
  /// Ссылка на изображение, хранящееся в библиотеке изображений
  /// </summary>
  private Guid _imgFromLibGuid = Guid.Empty;
  /// <summary>ID объекта "библиотечное изображение"</summary>
  private long _imgFromLibraryID;
  private string _imgFromLibraryName = string.Empty;
  private Image _img;
  /// <summary>Это текст, который присваивается через свойство</summary>
  private string _propText = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox _chb;

  /// <summary>Цвет фона элемента управления.</summary>
  public new Color BackColor
  {
    get => this._chb.BackColor;
    set => this._chb.BackColor = value;
  }

  /// <summary>
  /// Фоновое изображение, выводимое на элементе управления.
  /// </summary>
  public new Image BackgroundImage
  {
    get => this._chb.BackgroundImage;
    set
    {
      this.ClearImgFromLibraryData();
      this._chb.BackgroundImage = value;
      this.Invalidate();
    }
  }

  /// <summary>Способ размещения фонового изображения.</summary>
  public new ImageLayout BackgroundImageLayout
  {
    get => this._chb.BackgroundImageLayout;
    set => this._chb.BackgroundImageLayout = value;
  }

  /// <summary>
  /// Выравнивание переключателя по горизонтали и по вертикали в элементе управления.
  /// </summary>
  [DefaultValue(ContentAlignment.MiddleLeft)]
  public ContentAlignment CheckAlign
  {
    get => this._chb.CheckAlign;
    set => this._chb.CheckAlign = value;
  }

  /// <summary>Выставление значения по умолчанию переключателю.</summary>
  [DefaultValue(false)]
  public bool Checked
  {
    get => this._chb.Checked;
    set => this._chb.Checked = value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => this._chb.Font;
    set => this._chb.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  public new Color ForeColor
  {
    get => this._chb.ForeColor;
    set => this._chb.ForeColor = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this._chb);
    set => this._toolTip.SetToolTip((Control) this._chb, value);
  }

  /// <summary>Текст, связанный с элементом управления.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public override string Text
  {
    get => this._chb.Text;
    set
    {
      string text = this._propText = value;
      if (string.IsNullOrEmpty(text) && this.IsDesignMode && this.AttributeInfo != null && this.AttributeInfo.AttributeGuid != Guid.Empty)
        text = MetaDataHelper.GetAttributeTypeName(base.AttributeInfo.AttributeGuid);
      this.SetText(text);
    }
  }

  /// <summary>Выравнивание текста в элементе управления.</summary>
  [DefaultValue(ContentAlignment.MiddleLeft)]
  public ContentAlignment TextAlign
  {
    get => this._chb.TextAlign;
    set => this._chb.TextAlign = value;
  }

  /// <summary>Авторазмер элемента управления.</summary>
  [DefaultValue(false)]
  public new bool AutoSize
  {
    get => base.AutoSize;
    set
    {
      if (value)
      {
        this._sizeBeforeAutoSize = this.Size;
        this.UpdateAutoSize();
        base.AutoSize = value;
      }
      else
      {
        base.AutoSize = value;
        this.Size = this._sizeBeforeAutoSize;
      }
    }
  }

  /// <summary>Отступы от краев в элементе управления.</summary>
  public new Padding Padding
  {
    get => this._chb.Padding;
    set
    {
      this._chb.Padding = value;
      if (!this.AutoSize)
        return;
      this.UpdateAutoSize();
    }
  }

  /// <summary>Размеры элемента управления.</summary>
  public new Size Size
  {
    get => base.Size;
    set
    {
      if (this.AutoSize)
        return;
      base.Size = value;
    }
  }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected override object[] GetValues
  {
    get => new object[1]{ (object) this._chb.Checked };
  }

  /// <summary>Конструктор.</summary>
  public AttrCheckBox()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_expBtn_CalcClick(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this.ParentInfo == null)
      return;
    ExpertSystem.Calculate(this.ParentInfo, this.AttributeInfo.AttributeGuid, this.DesForm);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_expBtn_ReCalcClick(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this.ParentInfo == null)
      return;
    ExpertSystem.ReCalculate(this.ParentInfo.ElementIdentifier, this.AttributeInfo.AttributeGuid, this.DesForm);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_chb_CheckedChanged(object sender, EventArgs e)
  {
    if (this._disableNulls)
      this.Error = this._chb.CheckState == CheckState.Indeterminate ? this._errMsg_NullValue : string.Empty;
    this.Modified = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_chb_Paint(object sender, PaintEventArgs e)
  {
    this.OnPaint(e);
    if (this._img == null)
      return;
    if (this.BackgroundImageLayout == ImageLayout.Tile)
    {
      using (TextureBrush textureBrush = new TextureBrush(this._img, WrapMode.Tile))
        e.Graphics.FillRectangle((Brush) textureBrush, this._chb.ClientRectangle);
    }
    else
    {
      Rectangle rect = this.CalcBackgroundImageRectangle(this._chb.ClientRectangle, this._img.Size, this._chb.BackgroundImageLayout);
      e.Graphics.DrawImage(this._img, rect);
    }
  }

  /// <summary>Guid атрибута и типа объекта/связи.</summary>
  public override AttributeInfo AttributeInfo
  {
    get => base.AttributeInfo;
    set
    {
      base.AttributeInfo = value;
      if (this.IsDesignMode && base.AttributeInfo != null && string.IsNullOrEmpty(this._propText))
        this.SetText(MetaDataHelper.GetAttributeTypeName(base.AttributeInfo.AttributeGuid));
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>Значение атрибута.</summary>
  public override AttributeValues Values
  {
    get => base.Values;
    set
    {
      base.Values = value;
      if (value != null)
      {
        if (string.IsNullOrEmpty(this._propText))
          this.SetText(MetaDataHelper.GetAttributeTypeName(value.AttributeGuid));
        this.Checked = value.Values[0] != DBNull.Value && value.Values[0] != null && Convert.ToBoolean(value.Values[0]);
      }
      else
      {
        this.Checked = false;
        if (string.IsNullOrEmpty(this._propText))
        {
          if (this.AttributeInfo != null && this.AttributeInfo.AttributeGuid != Guid.Empty)
            this.SetText(MetaDataHelper.GetAttributeTypeName(base.AttributeInfo.AttributeGuid));
          else
            this.SetText(string.Empty);
        }
      }
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>Доступность контрола.</summary>
  /// <remarks>Переопределено, чтобы можно было сделать доступными кнопки ЭС, в то время когда сам контрол недоступен</remarks>
  [DefaultValue(true)]
  public override bool EnabledCtrl
  {
    get => this._enabled;
    set => this._chb.Enabled = this._enabled = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public override IElementInfo ParentInfo
  {
    get => base.ParentInfo;
    set
    {
      if (value == null || value.ElementIdentifier == 0L)
        this.UseInExpertSystem = false;
      base.ParentInfo = value;
    }
  }

  /// <summary>
  /// Ссылка на изображение, хранящееся в библиотеке изображений.
  /// </summary>
  [Browsable(false)]
  [DefaultValue(typeof (Guid), "00000000-0000-0000-0000-000000000000")]
  public Guid ImageFromLibrary
  {
    get => this._imgFromLibGuid;
    set
    {
      if (!(this._imgFromLibGuid != value))
        return;
      if (this._chb.BackgroundImage != null)
      {
        this._chb.BackgroundImage.Dispose();
        this._chb.BackgroundImage = (Image) null;
      }
      this._imgFromLibraryID = value == Guid.Empty ? 0L : this._imgFromLibraryID;
      if (this._img != null)
      {
        this._img.Dispose();
        this._img = (Image) null;
      }
      this._img = this.GetImageFromLibrary(value, ref this._imgFromLibraryID, ref this._imgFromLibraryName);
      if (this._img != null)
        this._imgFromLibGuid = value;
      else
        this.ClearImgFromLibraryData();
      this._chb.Invalidate();
    }
  }

  /// <summary>Наименование изображения.</summary>
  [Browsable(false)]
  public string ImageFromLibraryName => this._imgFromLibraryName;

  /// <summary>ID объекта "библиотечное изображение".</summary>
  [Browsable(false)]
  public long ImageFromLibraryID => this._imgFromLibraryID;

  /// <summary>
  /// Возможность использовать атрибут в экспертной системе.
  /// </summary>
  [DefaultValue(false)]
  public bool UseInExpertSystem
  {
    get => this._useInExpertSystem;
    set
    {
      this._useInExpertSystem = value && ExpertSystem.IsExpertSystemExists();
      if (this._useInExpertSystem)
      {
        if (this._btnCalc == null)
        {
          this._btnCalc = new ControlButton("Calc", 1)
          {
            Enabled = false
          };
          this._btnReCalc = new ControlButton("ReCalc", 2)
          {
            Enabled = false
          };
          if (!this.IsDesignMode)
          {
            this._btnCalc.Click += new EventHandler(this.On_expBtn_CalcClick);
            this._btnReCalc.Click += new EventHandler(this.On_expBtn_ReCalcClick);
          }
        }
        this.AddRightButtons(new List<ControlButton>()
        {
          this._btnCalc,
          this._btnReCalc
        });
      }
      else if (this._btnCalc != null)
        this.RemoveRightButtons(new List<ControlButton>()
        {
          this._btnCalc,
          this._btnReCalc
        });
      if (this.AutoSize)
        this.UpdateAutoSize();
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected override void SetDesignText(string text)
  {
    base.SetDesignText(text);
    if (this._chb.Text == this._designText)
      this._chb.Text = text;
    this._designText = text;
  }

  /// <summary>Проверка доступности кнопок.</summary>
  private void CheckAccessibilityButtons()
  {
    if (this._btnCalc != null)
      this._btnCalc.Enabled = !this.IsDesignMode ? (this._btnReCalc.Enabled = this._attrValues != null) : (this._btnReCalc.Enabled = this.AttributeInfo != null);
    this.Invalidate();
  }

  /// <summary>
  /// Очистить данные, которые относятся к изображению подгружаемому из библиотеки.
  /// </summary>
  private void ClearImgFromLibraryData()
  {
    this._imgFromLibraryName = string.Empty;
    this._imgFromLibraryID = 0L;
    this._imgFromLibGuid = Guid.Empty;
    if (this._img == null)
      return;
    this._img.Dispose();
    this._img = (Image) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  private void SetText(string text)
  {
    this._chb.Text = text;
    if (!this.AutoSize)
      return;
    this.UpdateAutoSize();
  }

  /// <summary>Пересчет размеров контрола при авторазмере.</summary>
  private void UpdateAutoSize()
  {
    this._chb.Dock = DockStyle.None;
    this._chb.AutoSize = true;
    base.Size = new Size(this._chb.Width + this._buttons.Width, Math.Max(this._chb.Height, this._buttons.Height));
    this._chb.AutoSize = false;
    this._chb.Dock = DockStyle.Fill;
  }

  /// <summary>Необходимость сериализации свойства BackColor.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeBackColor() => !base.BackColor.Equals((object) this._chb.BackColor);

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont() => !base.Font.Equals((object) this._chb.Font);

  /// <summary>Необходимость сериализации свойства ForeColor.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeForeColor() => !base.ForeColor.Equals((object) this._chb.ForeColor);

  /// <summary>Необходимость сериализации свойства Padding.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializePadding() => this._chb.Padding.All != 0;

  /// <summary>Необходимость сериализации свойства Text.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeText()
  {
    return !string.IsNullOrEmpty(this._designText) ? this._chb.Text != this._designText : !string.IsNullOrEmpty(this._chb.Text);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._btnCalc != null && !this.IsDesignMode)
      {
        this._btnCalc.Click -= new EventHandler(this.On_expBtn_CalcClick);
        this._btnReCalc.Click -= new EventHandler(this.On_expBtn_ReCalcClick);
      }
      this._chb.CheckedChanged -= new EventHandler(this.On_chb_CheckedChanged);
      this._chb.Paint -= new PaintEventHandler(this.On_chb_Paint);
      if (this._img != null)
      {
        this._img.Dispose();
        this._img = (Image) null;
      }
      if (this._chb.BackgroundImage != null)
      {
        this._chb.BackgroundImage.Dispose();
        this._chb.BackgroundImage = (Image) null;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrCheckBox));
    this._chb = new CheckBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._chb, "_chb");
    this._chb.Name = "_chb";
    this._chb.UseVisualStyleBackColor = true;
    this._chb.CheckedChanged += new EventHandler(this.On_chb_CheckedChanged);
    this._chb.Paint += new PaintEventHandler(this.On_chb_Paint);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._chb);
    this.Name = nameof (AttrCheckBox);
    this.ResumeLayout(false);
  }
}
