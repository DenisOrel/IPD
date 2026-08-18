
// Type: Intermech.Redline.RedNoteEditForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Map;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Redline;

public class RedNoteEditForm : Form
{
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
  private EnumComboBox<Intermech.Interfaces.IRedNoteStyle> RedNoteStyleBox = new EnumComboBox<Intermech.Interfaces.IRedNoteStyle>();
  private FloatNumericUpDown FacetBox = new FloatNumericUpDown();
  /// <summary>Есть изменения</summary>
  private bool isChanged;
  /// <summary>изменились настйроки</summary>
  private bool isLoad = true;
  private MapObject loadObj;
  private string noteText = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOk;
  private Button btnCancel;
  private TextBox textBox;
  private Label labelRelativeId;
  private Label labelUser;
  private GroupBox grBoxNote;
  private ComboBox comboBox_NoteStyle;
  private NumericUpDown numericUpDown_Facet;
  private NumericUpDown numericUpDown_FontSize;
  private FontComboBox comboBox_FontName;
  private Label label_TextColor;
  private Intermech.Bars.ToolBar toolBarTextColor;
  private Label label_TextAlpha;
  private NumericUpDown numericUpDown_TextAlpha;
  private GroupBox grBoxFill;
  private Label label_BrushColor;
  private Intermech.Bars.ToolBar toolBarBrushColor;
  private Label label_BrushAlpha;
  private NumericUpDown numericUpDown_BrushAlpha;
  private GroupBox grBoxPen;
  private Intermech.Bars.ToolBar toolBarPenColor;
  private Label label_PenColor;
  private Label label_PenAlpha;
  private NumericUpDown numericUpDown_PenAlpha;
  private Label label_PenThickness;
  private ComboBox comboBox_PenThickness;

  public RedNoteEditForm(MapRedNote note, float factor)
  {
    this.InitializeComponent();
    this.labelUser.Text = "Создал : " + note.Layer.Identifier.ToString();
    this.labelRelativeId.Text = "ID элемента : " + (note.RelativeId ?? string.Empty);
    this.label_PenColor.Text = LocalizationHolder.rm.GetString("Client.Core_1617");
    this.label_PenAlpha.Text = LocalizationHolder.rm.GetString("Client.Core_1618");
    this.label_PenThickness.Text = LocalizationHolder.rm.GetString("Client.Core_1619");
    this.label_BrushColor.Text = LocalizationHolder.rm.GetString("Client.Core_1617");
    this.label_BrushAlpha.Text = LocalizationHolder.rm.GetString("Client.Core_1618");
    this.label_TextColor.Text = LocalizationHolder.rm.GetString("Client.Core_1617");
    this.label_TextAlpha.Text = LocalizationHolder.rm.GetString("Client.Core_1618");
    this._property.NoteStyle.Value = note.NoteStyle.GetName<Intermech.Map.IRedNoteStyle>().ToEnum<Intermech.Interfaces.IRedNoteStyle>();
    this.InitializeRedPropertyBox();
    this.NoteText = note.Text;
    this.textBox.Text = this.NoteText;
  }

  private void InitializeRedPropertyBox()
  {
    this.isChanged = false;
    this.isLoad = true;
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
    this._property.NoteStyle.ValueChanged += (EventHandler<EventArgs<Intermech.Interfaces.IRedNoteStyle>>) ((sender, e) => this.OnChanged());
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
  public void LoadSettgins(MapObject obj)
  {
    bool isLoad = this.isLoad;
    this.isChanged = false;
    this.isLoad = true;
    if (obj is MapRedNote)
    {
      MapRedNote mapRedNote = obj as MapRedNote;
      this._property.PenColor.Value = Color.FromArgb((int) byte.MaxValue, mapRedNote.Pen.Color);
      this._property.PenAlpha.Value = (int) mapRedNote.Pen.Color.A;
      this._property.PenThickness.Value = mapRedNote.Pen.Width;
      Color color;
      if (mapRedNote.Brush is SolidBrush)
      {
        this._property.BrushColor.Value = Color.FromArgb((int) byte.MaxValue, (mapRedNote.Brush as SolidBrush).Color);
        Rclass<int> brushAlpha = this._property.BrushAlpha;
        color = (mapRedNote.Brush as SolidBrush).Color;
        int a = (int) color.A;
        brushAlpha.Value = a;
      }
      else
      {
        this._property.BrushColor.Value = Color.Empty;
        this._property.BrushAlpha.Value = (int) byte.MaxValue;
      }
      this._property.TextColor.Value = Color.FromArgb((int) byte.MaxValue, mapRedNote.TextColor);
      Rclass<int> textAlpha = this._property.TextAlpha;
      color = mapRedNote.TextColor;
      int a1 = (int) color.A;
      textAlpha.Value = a1;
      this._property.FontName.Value = mapRedNote.FontName;
      this._property.FontSize.Value = mapRedNote.FontSize;
      this._property.NoteStyle.Value = mapRedNote.NoteStyle.GetName<Intermech.Map.IRedNoteStyle>().ToEnum<Intermech.Interfaces.IRedNoteStyle>();
      this._property.Facet.Value = mapRedNote.Facet;
      this._property.NoteArrow.Value = mapRedNote.NoteArrow.GetName<Intermech.Map.IRedArrowStyle>().ToEnum<Intermech.Interfaces.IRedArrowStyle>();
      this._property.ArrowSize.Value = mapRedNote.ArrowSize;
    }
    this.loadObj = obj;
    this.isChanged = false;
    this.isLoad = isLoad;
  }

  /// <summary>Сохранение изменений.</summary>
  public void Apply(MapObject obj)
  {
    if (this.isChanged && obj is MapRedNote)
    {
      MapRedNote mapRedNote = obj as MapRedNote;
      mapRedNote.Pen = new Pen(this._property.PenColorAlpha, (float) this._property.PenThickness);
      mapRedNote.Pen.DashStyle = DashStyle.Solid;
      if ((Color) this._property.BrushColor != Color.Empty)
        mapRedNote.Brush = (Brush) new SolidBrush(this._property.BrushColorAlpha);
      mapRedNote.TextColor = this._property.TextColorAlpha;
      mapRedNote.FontName = (string) this._property.FontName;
      mapRedNote.FontSize = (float) this._property.FontSize;
      mapRedNote.NoteStyle = this._property.NoteStyle.Value.GetName<Intermech.Interfaces.IRedNoteStyle>().ToEnum<Intermech.Map.IRedNoteStyle>();
      mapRedNote.Facet = (float) this._property.Facet;
      mapRedNote.NoteArrow = this._property.NoteArrow.Value.GetName<Intermech.Interfaces.IRedArrowStyle>().ToEnum<Intermech.Map.IRedArrowStyle>();
      mapRedNote.ArrowSize = (float) this._property.ArrowSize;
    }
    this.isChanged = this.isLoad = false;
  }

  public string NoteText
  {
    get => this.noteText;
    set => this.noteText = value;
  }

  private void btnOk_Click(object sender, EventArgs e) => this.NoteText = this.textBox.Text;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        this.RedNoteStyleBox = (EnumComboBox<Intermech.Interfaces.IRedNoteStyle>) null;
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
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.textBox = new TextBox();
    this.labelRelativeId = new Label();
    this.labelUser = new Label();
    this.grBoxNote = new GroupBox();
    this.comboBox_NoteStyle = new ComboBox();
    this.numericUpDown_Facet = new NumericUpDown();
    this.numericUpDown_FontSize = new NumericUpDown();
    this.comboBox_FontName = new FontComboBox();
    this.label_TextColor = new Label();
    this.toolBarTextColor = new Intermech.Bars.ToolBar();
    this.label_TextAlpha = new Label();
    this.numericUpDown_TextAlpha = new NumericUpDown();
    this.grBoxFill = new GroupBox();
    this.label_BrushColor = new Label();
    this.toolBarBrushColor = new Intermech.Bars.ToolBar();
    this.label_BrushAlpha = new Label();
    this.numericUpDown_BrushAlpha = new NumericUpDown();
    this.grBoxPen = new GroupBox();
    this.toolBarPenColor = new Intermech.Bars.ToolBar();
    this.label_PenColor = new Label();
    this.label_PenAlpha = new Label();
    this.numericUpDown_PenAlpha = new NumericUpDown();
    this.label_PenThickness = new Label();
    this.comboBox_PenThickness = new ComboBox();
    this.grBoxNote.SuspendLayout();
    this.numericUpDown_Facet.BeginInit();
    this.numericUpDown_FontSize.BeginInit();
    this.numericUpDown_TextAlpha.BeginInit();
    this.grBoxFill.SuspendLayout();
    this.numericUpDown_BrushAlpha.BeginInit();
    this.grBoxPen.SuspendLayout();
    this.numericUpDown_PenAlpha.BeginInit();
    this.SuspendLayout();
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Location = new Point(431, 516);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(121, 27);
    this.btnOk.TabIndex = 6;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(558, 516);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 7;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.textBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.textBox.Location = new Point(15, 291);
    this.textBox.Multiline = true;
    this.textBox.Name = "textBox";
    this.textBox.Size = new Size(665, 219);
    this.textBox.TabIndex = 10;
    this.labelRelativeId.AutoSize = true;
    this.labelRelativeId.Location = new Point(12, 266);
    this.labelRelativeId.Name = "labelRelativeId";
    this.labelRelativeId.Size = new Size(55, 13);
    this.labelRelativeId.TabIndex = 11;
    this.labelRelativeId.Text = "RelativeId";
    this.labelUser.AutoSize = true;
    this.labelUser.Location = new Point(12, 241);
    this.labelUser.Name = "labelUser";
    this.labelUser.Size = new Size(29, 13);
    this.labelUser.TabIndex = 12;
    this.labelUser.Text = "User";
    this.grBoxNote.Controls.Add((Control) this.comboBox_NoteStyle);
    this.grBoxNote.Controls.Add((Control) this.numericUpDown_Facet);
    this.grBoxNote.Controls.Add((Control) this.numericUpDown_FontSize);
    this.grBoxNote.Controls.Add((Control) this.comboBox_FontName);
    this.grBoxNote.Controls.Add((Control) this.label_TextColor);
    this.grBoxNote.Controls.Add((Control) this.toolBarTextColor);
    this.grBoxNote.Controls.Add((Control) this.label_TextAlpha);
    this.grBoxNote.Controls.Add((Control) this.numericUpDown_TextAlpha);
    this.grBoxNote.Location = new Point(15, 139);
    this.grBoxNote.Name = "grBoxNote";
    this.grBoxNote.Size = new Size(552, 87);
    this.grBoxNote.TabIndex = 19;
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
    this.comboBox_FontName.NonReadableFonts = new string[18]
    {
      "CommercialPi BT",
      "GreekC",
      "GreekS",
      "Marlett",
      "Monotype Corsiva",
      "MS Outlook",
      "Nokia PC Composer",
      "UniversalMath1 BT",
      "Symusic",
      "Symeteo",
      "Symbol",
      "Symath",
      "Symap",
      "Syastro",
      "Webdings",
      "Wingdings",
      "Wingdings 2",
      "Wingdings 3"
    };
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
    this.grBoxFill.Controls.Add((Control) this.label_BrushColor);
    this.grBoxFill.Controls.Add((Control) this.toolBarBrushColor);
    this.grBoxFill.Controls.Add((Control) this.label_BrushAlpha);
    this.grBoxFill.Controls.Add((Control) this.numericUpDown_BrushAlpha);
    this.grBoxFill.Location = new Point(15, 76);
    this.grBoxFill.Name = "grBoxFill";
    this.grBoxFill.Size = new Size(552, 56);
    this.grBoxFill.TabIndex = 18;
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
    this.grBoxPen.Controls.Add((Control) this.toolBarPenColor);
    this.grBoxPen.Controls.Add((Control) this.label_PenColor);
    this.grBoxPen.Controls.Add((Control) this.label_PenAlpha);
    this.grBoxPen.Controls.Add((Control) this.numericUpDown_PenAlpha);
    this.grBoxPen.Controls.Add((Control) this.label_PenThickness);
    this.grBoxPen.Controls.Add((Control) this.comboBox_PenThickness);
    this.grBoxPen.Location = new Point(15, 12);
    this.grBoxPen.Name = "grBoxPen";
    this.grBoxPen.Size = new Size(552, 57);
    this.grBoxPen.TabIndex = 17;
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
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(695, 555);
    this.Controls.Add((Control) this.grBoxNote);
    this.Controls.Add((Control) this.grBoxFill);
    this.Controls.Add((Control) this.grBoxPen);
    this.Controls.Add((Control) this.labelUser);
    this.Controls.Add((Control) this.labelRelativeId);
    this.Controls.Add((Control) this.textBox);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.btnCancel);
    this.Name = nameof (RedNoteEditForm);
    this.Text = "Свойства комментария";
    this.grBoxNote.ResumeLayout(false);
    this.grBoxNote.PerformLayout();
    this.numericUpDown_Facet.EndInit();
    this.numericUpDown_FontSize.EndInit();
    this.numericUpDown_TextAlpha.EndInit();
    this.grBoxFill.ResumeLayout(false);
    this.grBoxFill.PerformLayout();
    this.numericUpDown_BrushAlpha.EndInit();
    this.grBoxPen.ResumeLayout(false);
    this.grBoxPen.PerformLayout();
    this.numericUpDown_PenAlpha.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
