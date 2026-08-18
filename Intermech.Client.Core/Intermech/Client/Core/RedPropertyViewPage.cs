
// Type: Intermech.Client.Core.RedPropertyViewPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Закладка для окна "Настройки", управляющая свойствами пометок для просмотра</summary>
public class RedPropertyViewPage : UserControl, IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>Контейнер сервисов</summary>
  private System.IServiceProvider _provider;
  /// <summary>свойства пометок для просмотра</summary>
  private RedProperty _property = new RedProperty();
  private ColorMenuBox PenColorBox = new ColorMenuBox();
  private AlphaNumericUpDown PenAlphaBox = new AlphaNumericUpDown();
  private ThicknessComboBox ThicknessBox = new ThicknessComboBox();
  private ColorMenuBox BrushColorBox = new ColorMenuBox();
  private AlphaNumericUpDown BrushAlphaBox = new AlphaNumericUpDown();
  private FontNameBox fontNameBox = new FontNameBox();
  private FloatNumericUpDown fontSizeBox = new FloatNumericUpDown();
  private ColorMenuBox TextColorBox = new ColorMenuBox();
  private AlphaNumericUpDown TextAlphaBox = new AlphaNumericUpDown();
  private EnumComboBox<IRedNoteStyle> RedNoteStyleBox = new EnumComboBox<IRedNoteStyle>();
  private FloatNumericUpDown FacetBox = new FloatNumericUpDown();
  /// <summary>Есть изменения</summary>
  private bool isChanged;
  /// <summary>изменились настйроки</summary>
  private bool isLoad = true;
  /// <summary>Требуется переменная конструктора.</summary>
  private IContainer components;
  private GroupBox grBoxPen;
  private Label label_PenColor;
  private Intermech.Bars.ToolBar toolBarPenColor;
  private Label label_PenAlpha;
  private NumericUpDown numericUpDown_PenAlpha;
  private Label label_PenThickness;
  private ComboBox comboBox_PenThickness;
  private GroupBox grBoxFill;
  private Label label_BrushColor;
  private Intermech.Bars.ToolBar toolBarBrushColor;
  private Label label_BrushAlpha;
  private NumericUpDown numericUpDown_BrushAlpha;
  private GroupBox grBoxNote;
  private Label label_TextColor;
  private Intermech.Bars.ToolBar toolBarTextColor;
  private Label label_TextAlpha;
  private NumericUpDown numericUpDown_TextAlpha;
  private FontComboBox comboBox_FontName;
  private NumericUpDown numericUpDown_FontSize;
  private NumericUpDown numericUpDown_Facet;
  private ComboBox comboBox_NoteStyle;

  /// <summary>конструктор</summary>
  /// <param name="provider">Контейнер сервисов</param>
  public RedPropertyViewPage(System.IServiceProvider provider)
  {
    this.InitializeComponent();
    this.label_PenColor.Text = LocalizationHolder.rm.GetString("Client.Core_1617");
    this.label_PenAlpha.Text = LocalizationHolder.rm.GetString("Client.Core_1618");
    this.label_PenThickness.Text = LocalizationHolder.rm.GetString("Client.Core_1619");
    this.label_BrushColor.Text = LocalizationHolder.rm.GetString("Client.Core_1617");
    this.label_BrushAlpha.Text = LocalizationHolder.rm.GetString("Client.Core_1618");
    this.label_TextColor.Text = LocalizationHolder.rm.GetString("Client.Core_1617");
    this.label_TextAlpha.Text = LocalizationHolder.rm.GetString("Client.Core_1618");
    if (!(ServicesManager.GetService(typeof (IRedService)) is IRedService))
      ServicesManager.AddService(typeof (IRedService), (object) new RedService());
    this._provider = provider;
    if (this._provider.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service)
      service.AddPage(this.PageName, (IPropertyPage) this);
    this.InitializeRedPropertyBox();
  }

  private void InitializeRedPropertyBox()
  {
    this.isChanged = false;
    this.isLoad = true;
    this.LoadSettgins();
    this.PenColorBox.Initialize_Pen(this.toolBarPenColor, this._property.PenColor);
    this._property.PenColor.ValueChanged += (EventHandler<EventArgs<Color>>) ((sender, e) => this.OnChanged());
    this.PenAlphaBox.Initialize(this.numericUpDown_PenAlpha, this._property.PenAlpha);
    this._property.PenAlpha.ValueChanged += (EventHandler<EventArgs<int>>) ((sender, e) => this.OnChanged());
    this.ThicknessBox.Initialize(this.comboBox_PenThickness, this._property.PenThickness);
    this._property.PenThickness.ValueChanged += (EventHandler<EventArgs<float>>) ((sender, e) => this.OnChanged());
    this.BrushColorBox.Initialize_Fill(this.toolBarBrushColor, this._property.BrushColor);
    this._property.BrushColor.ValueChanged += (EventHandler<EventArgs<Color>>) ((sender, e) => this.OnChanged());
    this.BrushAlphaBox.Initialize(this.numericUpDown_BrushAlpha, this._property.BrushAlpha);
    this._property.BrushAlpha.ValueChanged += (EventHandler<EventArgs<int>>) ((sender, e) => this.OnChanged());
    this.fontNameBox.Initialize((ComboBox) this.comboBox_FontName, this._property.FontName);
    this._property.FontName.ValueChanged += (EventHandler<EventArgs<string>>) ((sender, e) => this.OnChanged());
    this.fontSizeBox.Initialize(this.numericUpDown_FontSize, this._property.FontSize);
    this._property.FontSize.ValueChanged += (EventHandler<EventArgs<float>>) ((sender, e) => this.OnChanged());
    this.TextColorBox.Initialize_Text(this.toolBarTextColor, this._property.TextColor);
    this._property.TextColor.ValueChanged += (EventHandler<EventArgs<Color>>) ((sender, e) => this.OnChanged());
    this.TextAlphaBox.Initialize(this.numericUpDown_TextAlpha, this._property.TextAlpha);
    this._property.TextAlpha.ValueChanged += (EventHandler<EventArgs<int>>) ((sender, e) => this.OnChanged());
    this.RedNoteStyleBox.Initialize(this.comboBox_NoteStyle, this._property.NoteStyle);
    this._property.NoteStyle.ValueChanged += (EventHandler<EventArgs<IRedNoteStyle>>) ((sender, e) => this.OnChanged());
    this.FacetBox.Initialize(this.numericUpDown_Facet, this._property.Facet);
    this._property.Facet.ValueChanged += (EventHandler<EventArgs<float>>) ((sender, e) => this.OnChanged());
    this.isChanged = this.isLoad = false;
  }

  /// <summary>Событие изменения на закладке</summary>
  public event EventHandler Changed;

  /// <summary>Событие будет дёргаться при необходимости</summary>
  private void OnChanged()
  {
    if (this.isLoad)
      return;
    this.isChanged = true;
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  /// <summary>заполнить информацией свойства пометок</summary>
  private void LoadSettgins()
  {
    bool isLoad = this.isLoad;
    this.isChanged = false;
    this.isLoad = true;
    RedService.ReadSettings((IRedProperty) this._property);
    this.isChanged = false;
    this.isLoad = isLoad;
  }

  /// <summary>Отмена изменений.</summary>
  public void Cancel()
  {
    this.LoadSettgins();
    this.isChanged = this.isLoad = false;
  }

  /// <summary>Сохранение изменений.</summary>
  public void Apply()
  {
    if (this.isChanged)
    {
      using (new SessionKeeper())
        ((IRedService) ServicesManager.GetService(typeof (IRedService)))?.ChangeSettings((IRedProperty) this._property);
    }
    this.isChanged = this.isLoad = false;
  }

  /// <summary>Тип страницы.</summary>
  public PropertyPageType Type => PropertyPageType.Control;

  /// <summary>Контрол, который будет размещён на главной форме настроек</summary>
  public object Control => (object) this;

  /// <summary>Название странички в главной форме настроек</summary>
  public string PageName => "Система\\Красный карандаш\\Внутренние замечания";

  /// <summary>id раздела справки для данного элемента управления.</summary>
  public string HelpTopicID => string.Empty;

  /// <summary>Текст заголовка (пустое значение - заголовок не отображается)</summary>
  public string HeaderText => "Внутренние замечания";

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  /// <summary>Освободить все используемые ресурсы.</summary>
  /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      if (this.PenColorBox != null)
      {
        this.PenColorBox.Dispose();
        this.PenColorBox = (ColorMenuBox) null;
      }
      if (this.PenAlphaBox != null)
      {
        this.PenAlphaBox.Dispose();
        this.PenAlphaBox = (AlphaNumericUpDown) null;
      }
      if (this.ThicknessBox != null)
      {
        this.ThicknessBox.Dispose();
        this.ThicknessBox = (ThicknessComboBox) null;
      }
      if (this.BrushColorBox != null)
      {
        this.BrushColorBox.Dispose();
        this.BrushColorBox = (ColorMenuBox) null;
      }
      if (this.BrushAlphaBox != null)
      {
        this.BrushAlphaBox.Dispose();
        this.BrushAlphaBox = (AlphaNumericUpDown) null;
      }
      if (this.fontNameBox != null)
      {
        this.fontNameBox.Dispose();
        this.fontNameBox = (FontNameBox) null;
      }
      if (this.fontSizeBox != null)
      {
        this.fontSizeBox.Dispose();
        this.fontSizeBox = (FloatNumericUpDown) null;
      }
      if (this.TextColorBox != null)
      {
        this.TextColorBox.Dispose();
        this.TextColorBox = (ColorMenuBox) null;
      }
      if (this.TextAlphaBox != null)
      {
        this.TextAlphaBox.Dispose();
        this.TextAlphaBox = (AlphaNumericUpDown) null;
      }
      if (this.RedNoteStyleBox != null)
      {
        this.RedNoteStyleBox.Dispose();
        this.RedNoteStyleBox = (EnumComboBox<IRedNoteStyle>) null;
      }
      if (this.FacetBox != null)
      {
        this.FacetBox.Dispose();
        this.FacetBox = (FloatNumericUpDown) null;
      }
      this._property = (RedProperty) null;
      this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Обязательный метод для поддержки конструктора - не изменяйте
  /// содержимое данного метода при помощи редактора кода.
  /// </summary>
  private void InitializeComponent()
  {
    this.grBoxPen = new GroupBox();
    this.toolBarPenColor = new Intermech.Bars.ToolBar();
    this.label_PenColor = new Label();
    this.label_PenAlpha = new Label();
    this.numericUpDown_PenAlpha = new NumericUpDown();
    this.label_PenThickness = new Label();
    this.comboBox_PenThickness = new ComboBox();
    this.grBoxFill = new GroupBox();
    this.label_BrushColor = new Label();
    this.toolBarBrushColor = new Intermech.Bars.ToolBar();
    this.label_BrushAlpha = new Label();
    this.numericUpDown_BrushAlpha = new NumericUpDown();
    this.grBoxNote = new GroupBox();
    this.comboBox_NoteStyle = new ComboBox();
    this.numericUpDown_Facet = new NumericUpDown();
    this.numericUpDown_FontSize = new NumericUpDown();
    this.comboBox_FontName = new FontComboBox();
    this.label_TextColor = new Label();
    this.toolBarTextColor = new Intermech.Bars.ToolBar();
    this.label_TextAlpha = new Label();
    this.numericUpDown_TextAlpha = new NumericUpDown();
    this.grBoxPen.SuspendLayout();
    this.numericUpDown_PenAlpha.BeginInit();
    this.grBoxFill.SuspendLayout();
    this.numericUpDown_BrushAlpha.BeginInit();
    this.grBoxNote.SuspendLayout();
    this.numericUpDown_Facet.BeginInit();
    this.numericUpDown_FontSize.BeginInit();
    this.numericUpDown_TextAlpha.BeginInit();
    this.SuspendLayout();
    this.grBoxPen.Controls.Add((System.Windows.Forms.Control) this.toolBarPenColor);
    this.grBoxPen.Controls.Add((System.Windows.Forms.Control) this.label_PenColor);
    this.grBoxPen.Controls.Add((System.Windows.Forms.Control) this.label_PenAlpha);
    this.grBoxPen.Controls.Add((System.Windows.Forms.Control) this.numericUpDown_PenAlpha);
    this.grBoxPen.Controls.Add((System.Windows.Forms.Control) this.label_PenThickness);
    this.grBoxPen.Controls.Add((System.Windows.Forms.Control) this.comboBox_PenThickness);
    this.grBoxPen.Location = new Point(12, 14);
    this.grBoxPen.Name = "grBoxPen";
    this.grBoxPen.Size = new Size(552, 57);
    this.grBoxPen.TabIndex = 1;
    this.grBoxPen.TabStop = false;
    this.grBoxPen.Text = "Линии";
    this.toolBarPenColor.BackgroundImageLayout = ImageLayout.None;
    this.toolBarPenColor.Closable = false;
    this.toolBarPenColor.Dock = DockStyle.None;
    this.toolBarPenColor.FullMenus = true;
    this.toolBarPenColor.Guid = new Guid("a81efcef-fc5b-4bb9-a268-d0907f8ba462");
    this.toolBarPenColor.Hidden = false;
    this.toolBarPenColor.Location = new Point(47, 15);
    this.toolBarPenColor.Name = "toolBarPenColor";
    this.toolBarPenColor.Size = new Size(55, 18);
    this.toolBarPenColor.TabIndex = 3;
    this.toolBarPenColor.Text = "";
    this.label_PenColor.AutoSize = true;
    this.label_PenColor.Location = new Point(6, 21);
    this.label_PenColor.Name = "label_PenColor";
    this.label_PenColor.Size = new Size(35, 13);
    this.label_PenColor.TabIndex = 2;
    this.label_PenColor.Text = "Цвет:";
    this.label_PenAlpha.AutoSize = true;
    this.label_PenAlpha.Location = new Point(108, 21);
    this.label_PenAlpha.Name = "label_PenAlpha";
    this.label_PenAlpha.Size = new Size(82, 13);
    this.label_PenAlpha.TabIndex = 4;
    this.label_PenAlpha.Text = "Прозрачность:";
    this.numericUpDown_PenAlpha.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.numericUpDown_PenAlpha.Location = new Point(196, 19);
    this.numericUpDown_PenAlpha.Maximum = new Decimal(new int[4]
    {
      (int) byte.MaxValue,
      0,
      0,
      0
    });
    this.numericUpDown_PenAlpha.Name = "numericUpDown_PenAlpha";
    this.numericUpDown_PenAlpha.Size = new Size(54, 20);
    this.numericUpDown_PenAlpha.TabIndex = 5;
    this.numericUpDown_PenAlpha.Value = new Decimal(new int[4]
    {
      (int) byte.MaxValue,
      0,
      0,
      0
    });
    this.label_PenThickness.AutoSize = true;
    this.label_PenThickness.Location = new Point(265, 21);
    this.label_PenThickness.Name = "label_PenThickness";
    this.label_PenThickness.Size = new Size(78, 13);
    this.label_PenThickness.TabIndex = 6;
    this.label_PenThickness.Text = "Толщина(мм):";
    this.comboBox_PenThickness.AutoCompleteSource = AutoCompleteSource.ListItems;
    this.comboBox_PenThickness.FormattingEnabled = true;
    this.comboBox_PenThickness.Location = new Point(349, 18);
    this.comboBox_PenThickness.Name = "comboBox_PenThickness";
    this.comboBox_PenThickness.Size = new Size(174, 21);
    this.comboBox_PenThickness.TabIndex = 7;
    this.comboBox_PenThickness.Tag = (object) "";
    this.grBoxFill.Controls.Add((System.Windows.Forms.Control) this.label_BrushColor);
    this.grBoxFill.Controls.Add((System.Windows.Forms.Control) this.toolBarBrushColor);
    this.grBoxFill.Controls.Add((System.Windows.Forms.Control) this.label_BrushAlpha);
    this.grBoxFill.Controls.Add((System.Windows.Forms.Control) this.numericUpDown_BrushAlpha);
    this.grBoxFill.Location = new Point(12, 78);
    this.grBoxFill.Name = "grBoxFill";
    this.grBoxFill.Size = new Size(552, 56);
    this.grBoxFill.TabIndex = 8;
    this.grBoxFill.TabStop = false;
    this.grBoxFill.Text = "Заливка";
    this.label_BrushColor.AutoSize = true;
    this.label_BrushColor.Location = new Point(6, 19);
    this.label_BrushColor.Name = "label_BrushColor";
    this.label_BrushColor.Size = new Size(35, 13);
    this.label_BrushColor.TabIndex = 9;
    this.label_BrushColor.Text = "Цвет:";
    this.toolBarBrushColor.BackgroundImageLayout = ImageLayout.None;
    this.toolBarBrushColor.Closable = false;
    this.toolBarBrushColor.Dock = DockStyle.None;
    this.toolBarBrushColor.FullMenus = true;
    this.toolBarBrushColor.Guid = new Guid("af11354d-90fd-4a15-ac5b-776e5629270f");
    this.toolBarBrushColor.Hidden = false;
    this.toolBarBrushColor.Location = new Point(47, 15);
    this.toolBarBrushColor.Name = "toolBarBrushColor";
    this.toolBarBrushColor.Size = new Size(55, 18);
    this.toolBarBrushColor.TabIndex = 10;
    this.toolBarBrushColor.Text = "";
    this.label_BrushAlpha.AutoSize = true;
    this.label_BrushAlpha.Location = new Point(108, 19);
    this.label_BrushAlpha.Name = "label_BrushAlpha";
    this.label_BrushAlpha.Size = new Size(82, 13);
    this.label_BrushAlpha.TabIndex = 11;
    this.label_BrushAlpha.Text = "Прозрачность:";
    this.numericUpDown_BrushAlpha.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.numericUpDown_BrushAlpha.Location = new Point(196, 17);
    this.numericUpDown_BrushAlpha.Maximum = new Decimal(new int[4]
    {
      (int) byte.MaxValue,
      0,
      0,
      0
    });
    this.numericUpDown_BrushAlpha.Name = "numericUpDown_BrushAlpha";
    this.numericUpDown_BrushAlpha.Size = new Size(54, 20);
    this.numericUpDown_BrushAlpha.TabIndex = 12;
    this.numericUpDown_BrushAlpha.Value = new Decimal(new int[4]
    {
      (int) byte.MaxValue,
      0,
      0,
      0
    });
    this.grBoxNote.Controls.Add((System.Windows.Forms.Control) this.comboBox_NoteStyle);
    this.grBoxNote.Controls.Add((System.Windows.Forms.Control) this.numericUpDown_Facet);
    this.grBoxNote.Controls.Add((System.Windows.Forms.Control) this.numericUpDown_FontSize);
    this.grBoxNote.Controls.Add((System.Windows.Forms.Control) this.comboBox_FontName);
    this.grBoxNote.Controls.Add((System.Windows.Forms.Control) this.label_TextColor);
    this.grBoxNote.Controls.Add((System.Windows.Forms.Control) this.toolBarTextColor);
    this.grBoxNote.Controls.Add((System.Windows.Forms.Control) this.label_TextAlpha);
    this.grBoxNote.Controls.Add((System.Windows.Forms.Control) this.numericUpDown_TextAlpha);
    this.grBoxNote.Location = new Point(12, 141);
    this.grBoxNote.Name = "grBoxNote";
    this.grBoxNote.Size = new Size(552, 87);
    this.grBoxNote.TabIndex = 13;
    this.grBoxNote.TabStop = false;
    this.grBoxNote.Text = "Текст";
    this.comboBox_NoteStyle.FormattingEnabled = true;
    this.comboBox_NoteStyle.Location = new Point(268, 15);
    this.comboBox_NoteStyle.Name = "comboBox_NoteStyle";
    this.comboBox_NoteStyle.Size = new Size(152, 21);
    this.comboBox_NoteStyle.TabIndex = 20;
    this.numericUpDown_Facet.DecimalPlaces = 1;
    this.numericUpDown_Facet.Increment = new Decimal(new int[4]
    {
      1,
      0,
      0,
      65536 /*0x010000*/
    });
    this.numericUpDown_Facet.Location = new Point(446, 16 /*0x10*/);
    this.numericUpDown_Facet.Maximum = new Decimal(new int[4]
    {
      15,
      0,
      0,
      0
    });
    this.numericUpDown_Facet.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      65536 /*0x010000*/
    });
    this.numericUpDown_Facet.Name = "numericUpDown_Facet";
    this.numericUpDown_Facet.Size = new Size(54, 20);
    this.numericUpDown_Facet.TabIndex = 19;
    this.numericUpDown_Facet.Value = new Decimal(new int[4]
    {
      4,
      0,
      0,
      0
    });
    this.numericUpDown_FontSize.DecimalPlaces = 1;
    this.numericUpDown_FontSize.Increment = new Decimal(new int[4]
    {
      1,
      0,
      0,
      65536 /*0x010000*/
    });
    this.numericUpDown_FontSize.Location = new Point(196, 54);
    this.numericUpDown_FontSize.Maximum = new Decimal(new int[4]
    {
      15,
      0,
      0,
      0
    });
    this.numericUpDown_FontSize.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      65536 /*0x010000*/
    });
    this.numericUpDown_FontSize.Name = "numericUpDown_FontSize";
    this.numericUpDown_FontSize.Size = new Size(54, 20);
    this.numericUpDown_FontSize.TabIndex = 19;
    this.numericUpDown_FontSize.Value = new Decimal(new int[4]
    {
      5,
      0,
      0,
      0
    });
    this.comboBox_FontName.DrawMode = DrawMode.OwnerDrawFixed;
    this.comboBox_FontName.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBox_FontName.ForeColor = Color.Black;
    this.comboBox_FontName.FormattingEnabled = true;
    this.comboBox_FontName.Location = new Point(9, 54);
    this.comboBox_FontName.Name = "comboBox_FontName";
    this.comboBox_FontName.Size = new Size(176 /*0xB0*/, 21);
    this.comboBox_FontName.TabIndex = 18;
    this.label_TextColor.AutoSize = true;
    this.label_TextColor.Location = new Point(6, 16 /*0x10*/);
    this.label_TextColor.Name = "label_TextColor";
    this.label_TextColor.Size = new Size(35, 13);
    this.label_TextColor.TabIndex = 14;
    this.label_TextColor.Text = "Цвет:";
    this.toolBarTextColor.BackgroundImageLayout = ImageLayout.None;
    this.toolBarTextColor.Closable = false;
    this.toolBarTextColor.Dock = DockStyle.None;
    this.toolBarTextColor.FullMenus = true;
    this.toolBarTextColor.Guid = new Guid("d220aaa0-bda1-456e-a0e5-205378c38ae4");
    this.toolBarTextColor.Hidden = false;
    this.toolBarTextColor.Location = new Point(47, 15);
    this.toolBarTextColor.Name = "toolBarTextColor";
    this.toolBarTextColor.Size = new Size(55, 18);
    this.toolBarTextColor.TabIndex = 15;
    this.toolBarTextColor.Text = "";
    this.label_TextAlpha.AutoSize = true;
    this.label_TextAlpha.Location = new Point(108, 16 /*0x10*/);
    this.label_TextAlpha.Name = "label_TextAlpha";
    this.label_TextAlpha.Size = new Size(82, 13);
    this.label_TextAlpha.TabIndex = 16 /*0x10*/;
    this.label_TextAlpha.Text = "Прозрачность:";
    this.numericUpDown_TextAlpha.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.numericUpDown_TextAlpha.Location = new Point(196, 16 /*0x10*/);
    this.numericUpDown_TextAlpha.Maximum = new Decimal(new int[4]
    {
      (int) byte.MaxValue,
      0,
      0,
      0
    });
    this.numericUpDown_TextAlpha.Name = "numericUpDown_TextAlpha";
    this.numericUpDown_TextAlpha.Size = new Size(54, 20);
    this.numericUpDown_TextAlpha.TabIndex = 17;
    this.numericUpDown_TextAlpha.Value = new Decimal(new int[4]
    {
      (int) byte.MaxValue,
      0,
      0,
      0
    });
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.grBoxNote);
    this.Controls.Add((System.Windows.Forms.Control) this.grBoxFill);
    this.Controls.Add((System.Windows.Forms.Control) this.grBoxPen);
    this.Name = nameof (RedPropertyViewPage);
    this.Size = new Size(583, 445);
    this.grBoxPen.ResumeLayout(false);
    this.grBoxPen.PerformLayout();
    this.numericUpDown_PenAlpha.EndInit();
    this.grBoxFill.ResumeLayout(false);
    this.grBoxFill.PerformLayout();
    this.numericUpDown_BrushAlpha.EndInit();
    this.grBoxNote.ResumeLayout(false);
    this.grBoxNote.PerformLayout();
    this.numericUpDown_Facet.EndInit();
    this.numericUpDown_FontSize.EndInit();
    this.numericUpDown_TextAlpha.EndInit();
    this.ResumeLayout(false);
  }
}
