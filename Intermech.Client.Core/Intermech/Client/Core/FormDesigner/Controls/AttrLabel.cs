
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrLabel
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Контрол для просмотра значений, которые нельзя редактировать (системные атрибуты и др.)
/// </summary>
[Designer(typeof (AttrLabelControlDesigner))]
[RefreshProperties(RefreshProperties.All)]
public class AttrLabel : AttrsControl, IImageFromLibrary, IExpertSystemCtrl
{
  private Size _sizeBeforeAutoSize = new Size(0, 0);
  /// <summary>Использование атрибута в экспертной системе</summary>
  private bool _useInExpertSystem;
  private ControlButton _btnCalc;
  private ControlButton _btnReCalc;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private IMLabel _imLb;

  /// <summary>Цвет фона элемента управления.</summary>
  public new Color BackColor
  {
    get => this._imLb.BackColor;
    set => this._imLb.BackColor = value;
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [DefaultValue(BorderStyle.None)]
  public new BorderStyle BorderStyle
  {
    get => this._imLb.BorderStyle;
    set
    {
      this._imLb.BorderStyle = value;
      if (!this.AutoSize)
        return;
      this.UpdateAutoSize();
    }
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => this._imLb.Font;
    set => this._imLb.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  public new Color ForeColor
  {
    get => this._imLb.ForeColor;
    set => this._imLb.ForeColor = value;
  }

  /// <summary>Изображение, отображаемое в элементе управления.</summary>
  public Image Image
  {
    get => this._imLb.Image;
    set => this._imLb.Image = value;
  }

  /// <summary>
  /// Выравнивание изображения, отображаемого в элементе управления.
  /// </summary>
  [DefaultValue(ContentAlignment.MiddleCenter)]
  public ContentAlignment ImageAlign
  {
    get => this._imLb.ImageAlign;
    set => this._imLb.ImageAlign = value;
  }

  /// <summary>Текст, связанный с элементом управления.</summary>
  public override string Text
  {
    get => this._imLb.Text;
    set
    {
      this._imLb.Text = value;
      if (!this.AutoSize)
        return;
      this.UpdateAutoSize();
    }
  }

  /// <summary>Выравнивание текста в элементе управления.</summary>
  [DefaultValue(ContentAlignment.TopLeft)]
  public ContentAlignment TextAlign
  {
    get => this._imLb.TextAlign;
    set => this._imLb.TextAlign = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public override AnchorStyles Anchor
  {
    get => base.Anchor;
    set => base.Anchor = value;
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
    get => this._imLb.Padding;
    set
    {
      this._imLb.Padding = value;
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

  /// <summary>Конструктор.</summary>
  public AttrLabel()
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
  /// Label не может быть ReadOnly (назначение label - отображение текста, поэтому нелогично ставить еще и Enabled == false).
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_imLb_EnabledChanged(object sender, EventArgs e)
  {
    if (this.Enabled || this.Parent == null || !this.Parent.Enabled)
      return;
    this.Enabled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_imLb_Paint(object sender, PaintEventArgs e)
  {
    if (!this.IsDesignMode)
      return;
    Color backColor = this._imLb.BackColor;
    int int32_1 = Convert.ToInt32((double) backColor.R * 0.5);
    int int32_2 = Convert.ToInt32((double) backColor.G * 0.5);
    int int32_3 = Convert.ToInt32((double) backColor.B * 0.5);
    int green = int32_2;
    int blue = int32_3;
    using (Pen pen = new Pen(Color.FromArgb(int32_1, green, blue), 1f))
    {
      pen.DashStyle = DashStyle.Dash;
      e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this._imLb.Width - 1, this._imLb.Height - 1));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_imLb_TextChanged(object sender, EventArgs e)
  {
    if (this.IsDesignMode)
      return;
    this.Modified = true;
  }

  /// <summary>Guid атрибута и типа объекта/связи.</summary>
  public override AttributeInfo AttributeInfo
  {
    get => base.AttributeInfo;
    set
    {
      base.AttributeInfo = value;
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>Значение атрибута.</summary>
  public override AttributeValues Values
  {
    get => this._attrValues;
    set
    {
      this._attrValues = value;
      if (value != null)
      {
        object[] values = value.Values;
        object[] descriptions = value.Descriptions;
        List<string> stringList = new List<string>();
        if (value.AttributeGuid == new Guid("cad00811-306c-11d8-b4e9-00304f19f545"))
        {
          if (values != null && values.Length != 0)
          {
            long result = 0;
            if (long.TryParse(Convert.ToString(values[0]), out result) && result > 0L && descriptions != null && descriptions.Length != 0)
            {
              string str = Convert.ToString(descriptions[0]);
              if (!string.IsNullOrEmpty(str))
                stringList.Add(str);
            }
          }
        }
        else if (this._possibleValues != null)
        {
          EnumerableRowCollection<DataRow> source = this._possibleValues.AsEnumerable();
          for (int index = 0; index < values.Length; ++index)
          {
            object objValue = values[index];
            if (objValue != null && objValue != DBNull.Value)
            {
              DataRow dataRow = source.FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => x[this._colKey].Equals(objValue)));
              if (dataRow != null)
                stringList.Add(Convert.ToString(dataRow[this._colDesc]));
            }
          }
        }
        else if (!string.IsNullOrEmpty(Convert.ToString(values[0])))
        {
          if (descriptions != null)
          {
            foreach (object obj in descriptions)
              stringList.Add(Convert.ToString(obj));
          }
          if (string.IsNullOrEmpty(string.Join("\r\n", stringList.ToArray())))
          {
            stringList.Clear();
            foreach (object obj in values)
              stringList.Add(Convert.ToString(obj));
          }
        }
        this.Text = string.Join("\r\n", stringList.ToArray());
        this._buttons.Enabled = true;
        this.Invalidate();
      }
      else
        this.Text = string.Empty;
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>
  /// Ссылка на изображение, хранящееся в библиотеке изображений.
  /// </summary>
  [Browsable(false)]
  [DefaultValue(typeof (Guid), "00000000-0000-0000-0000-000000000000")]
  public Guid ImageFromLibrary
  {
    get => this._imLb.ImageFromLibrary;
    set => this._imLb.ImageFromLibrary = value;
  }

  /// <summary>Наименование изображения.</summary>
  [Browsable(false)]
  public string ImageFromLibraryName => this._imLb.ImageFromLibraryName;

  /// <summary>ID объекта "библиотечное изображение".</summary>
  [Browsable(false)]
  public long ImageFromLibraryID => this._imLb.ImageFromLibraryID;

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
    this.Text = text;
  }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected override object[] GetValues
  {
    get
    {
      return new object[1]
      {
        this._attrValues == null ? (object) DBNull.Value : this._attrValues.Value
      };
    }
  }

  /// <summary>Проверка доступности кнопок.</summary>
  private void CheckAccessibilityButtons()
  {
    if (this._btnCalc != null)
      this._btnCalc.Enabled = !this.IsDesignMode ? (this._btnReCalc.Enabled = this._attrValues != null) : (this._btnReCalc.Enabled = this.AttributeInfo != null);
    this.Invalidate();
  }

  /// <summary>Пересчет размеров контрола при авторазмере.</summary>
  private void UpdateAutoSize()
  {
    Point location = this.Location;
    this._imLb.Dock = DockStyle.None;
    this._imLb.AutoSize = true;
    base.Size = new Size(this._imLb.Width + this._buttons.Width, Math.Max(this._imLb.Height, this._buttons.Height));
    this.Location = location;
    this._imLb.AutoSize = false;
    this._imLb.Dock = DockStyle.Fill;
  }

  /// <summary>Необходимость сериализации свойства BackColor.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeBackColor() => !base.BackColor.Equals((object) this._imLb.BackColor);

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont() => !base.Font.Equals((object) this._imLb.Font);

  /// <summary>Необходимость сериализации свойства ForeColor.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeForeColor() => !base.ForeColor.Equals((object) this._imLb.ForeColor);

  /// <summary>Необходимость сериализации свойства Padding.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializePadding() => this._imLb.Padding.All != 0;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._imLb.EnabledChanged -= new EventHandler(this.On_imLb_EnabledChanged);
      this._imLb.TextChanged -= new EventHandler(this.On_imLb_TextChanged);
      this._imLb.Paint -= new PaintEventHandler(this.On_imLb_Paint);
      if (this._btnCalc != null && !this.IsDesignMode)
      {
        this._btnCalc.Click -= new EventHandler(this.On_expBtn_CalcClick);
        this._btnReCalc.Click -= new EventHandler(this.On_expBtn_ReCalcClick);
      }
      this._imLb.Dispose();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrLabel));
    this._imLb = new IMLabel();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._imLb, "_imLb");
    this._imLb.Name = "_imLb";
    this._imLb.EnabledChanged += new EventHandler(this.On_imLb_EnabledChanged);
    this._imLb.TextChanged += new EventHandler(this.On_imLb_TextChanged);
    this._imLb.Paint += new PaintEventHandler(this.On_imLb_Paint);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._imLb);
    this.Name = nameof (AttrLabel);
    this.ResumeLayout(false);
  }
}
