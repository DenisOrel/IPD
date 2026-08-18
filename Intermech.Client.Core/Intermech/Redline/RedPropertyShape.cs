
// Type: Intermech.Redline.RedPropertyShape
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Map;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Redline;

public class RedPropertyShape : Form
{
  /// <summary>свойства пометок для просмотра</summary>
  private RedProperty _property = new RedProperty();
  private ColorMenuBox PenColorBox = new ColorMenuBox();
  private AlphaNumericUpDown PenAlphaBox = new AlphaNumericUpDown();
  private ThicknessComboBox ThicknessBox = new ThicknessComboBox();
  private ColorMenuBox BrushColorBox = new ColorMenuBox();
  private AlphaNumericUpDown BrushAlphaBox = new AlphaNumericUpDown();
  /// <summary>Есть изменения</summary>
  private bool isChanged;
  /// <summary>изменились настйроки</summary>
  private bool isLoad = true;
  private MapObject loadObj;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
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
  private Button btnOk;
  private Button btnCancel;

  public RedPropertyShape()
  {
    this.InitializeComponent();
    this.isChanged = false;
    this.isLoad = true;
    this.label_PenColor.Text = LocalizationHolder.rm.GetString("Client.Core_1617");
    this.label_PenAlpha.Text = LocalizationHolder.rm.GetString("Client.Core_1618");
    this.label_PenThickness.Text = LocalizationHolder.rm.GetString("Client.Core_1619");
    this.label_BrushColor.Text = LocalizationHolder.rm.GetString("Client.Core_1617");
    this.label_BrushAlpha.Text = LocalizationHolder.rm.GetString("Client.Core_1618");
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
    if (obj is MapShape)
    {
      MapShape mapShape = obj as MapShape;
      this._property.PenColor.Value = Color.FromArgb((int) byte.MaxValue, mapShape.Pen.Color);
      this._property.PenAlpha.Value = (int) mapShape.Pen.Color.A;
      this._property.PenThickness.Value = mapShape.Pen.Width;
      if (mapShape.Brush != null)
      {
        this._property.BrushColor.Value = Color.FromArgb((int) byte.MaxValue, (mapShape.Brush as SolidBrush).Color);
        this._property.BrushAlpha.Value = (int) (mapShape.Brush as SolidBrush).Color.A;
      }
      else
      {
        this._property.BrushColor.Value = Color.Empty;
        this._property.BrushAlpha.Value = (int) byte.MaxValue;
      }
    }
    this.loadObj = obj;
    this.isChanged = false;
    this.isLoad = isLoad;
  }

  /// <summary>Сохранение изменений.</summary>
  public void Apply(MapObject obj)
  {
    if (this.isChanged && obj is MapShape)
    {
      MapShape mapShape = obj as MapShape;
      mapShape.Pen = new Pen(this._property.PenColorAlpha, (float) this._property.PenThickness);
      mapShape.Brush = !((Color) this._property.BrushColor != Color.Empty) ? (Brush) null : (Brush) new SolidBrush(this._property.BrushColorAlpha);
    }
    this.isChanged = this.isLoad = false;
  }

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
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.grBoxFill.SuspendLayout();
    this.numericUpDown_BrushAlpha.BeginInit();
    this.grBoxPen.SuspendLayout();
    this.numericUpDown_PenAlpha.BeginInit();
    this.SuspendLayout();
    this.grBoxFill.Controls.Add((Control) this.label_BrushColor);
    this.grBoxFill.Controls.Add((Control) this.toolBarBrushColor);
    this.grBoxFill.Controls.Add((Control) this.label_BrushAlpha);
    this.grBoxFill.Controls.Add((Control) this.numericUpDown_BrushAlpha);
    this.grBoxFill.Location = new Point(12, 76);
    this.grBoxFill.Name = "grBoxFill";
    this.grBoxFill.Size = new Size(552, 56);
    this.grBoxFill.TabIndex = 17;
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
    this.grBoxPen.Location = new Point(12, 12);
    this.grBoxPen.Name = "grBoxPen";
    this.grBoxPen.Size = new Size(552, 57);
    this.grBoxPen.TabIndex = 16 /*0x10*/;
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
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Location = new Point(318, 148);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(121, 27);
    this.btnOk.TabIndex = 18;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(445, 148);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 19;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(575, 187);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.grBoxFill);
    this.Controls.Add((Control) this.grBoxPen);
    this.Name = nameof (RedPropertyShape);
    this.Text = "Свойства пометок";
    this.grBoxFill.ResumeLayout(false);
    this.grBoxFill.PerformLayout();
    this.numericUpDown_BrushAlpha.EndInit();
    this.grBoxPen.ResumeLayout(false);
    this.grBoxPen.PerformLayout();
    this.numericUpDown_PenAlpha.EndInit();
    this.ResumeLayout(false);
  }
}
