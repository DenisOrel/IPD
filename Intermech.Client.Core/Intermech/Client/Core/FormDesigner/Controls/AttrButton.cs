
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Контрол "Кнопка" для применения/отмены изменений.</summary>
public class AttrButton : Button, IBaseDesForm
{
  private DesForm _parentForm;
  private Point _savedLocation = new Point(0, 0);
  private Size _savedSize = new Size(0, 0);
  private bool _bWidthIncr;
  private bool _bHeightIncr;
  /// <summary>Текстовая подсказка</summary>
  private string _hint = string.Empty;

  /// <summary>
  /// Действие кнопки, назначенное при проектировании форм (оставлено в целях совместимости only).
  /// </summary>
  /// <remarks>
  /// Данное свойство необходимо только для поддержки загрузки старых версий форм редактирования, у которых еще не было свойства FormDesignerAction
  /// </remarks>
  [RefreshProperties(RefreshProperties.All)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Obsolete("Больше в коде не использовать! Пользоваться FormDesignerAction!")]
  public AttrButtonAction Action
  {
    get
    {
      if (this.FormDesignerAction == ActionInfo.ApplyAction)
        return AttrButtonAction.Apply;
      return this.FormDesignerAction != ActionInfo.CancelAction ? AttrButtonAction.None : AttrButtonAction.Cancel;
    }
    set
    {
      switch (value)
      {
        case AttrButtonAction.Cancel:
          this.FormDesignerAction = ActionInfo.CancelAction;
          break;
        case AttrButtonAction.Apply:
          this.FormDesignerAction = ActionInfo.ApplyAction;
          break;
        case AttrButtonAction.Calc:
        case AttrButtonAction.ReCalc:
          if (ServicesManager.GetService(typeof (IFormDesignerActionManager)) is IFormDesignerActionManager service)
          {
            string strB = value == AttrButtonAction.Calc ? "F93C7B60-BFBD-4213-A6FE-DC5CDD252D25" : "DCB9C7B7-6BD4-4a85-B651-533E220310C7";
            foreach (FormDesignerAction formDesignerAction in (IEnumerable<FormDesignerAction>) service)
            {
              if (string.Compare(Convert.ToString((object) formDesignerAction.ActionGuid), strB, true) == 0)
              {
                this.FormDesignerAction = formDesignerAction;
                return;
              }
            }
          }
          this.FormDesignerAction = ActionInfo.NoneAction;
          break;
        default:
          this.FormDesignerAction = ActionInfo.NoneAction;
          break;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(false)]
  public bool AlwaysEnabled { get; set; }

  /// <summary>Цвет фона элемента управления.</summary>
  [DefaultValue(typeof (Color), "Control")]
  public new Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  /// <summary>Расположение картинки.</summary>
  /// <remarks>Раньше было доступно. Перегружено для того, чтобы не сериализовать старые значения (т.к. теперь не используется)</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override Image BackgroundImage
  {
    get => base.BackgroundImage;
    set => this.Image = this.Image ?? value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => base.Font;
    set => base.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  public new Color ForeColor
  {
    get => base.ForeColor;
    set => base.ForeColor = value;
  }

  /// <summary>Действие по нажатию кнопки.</summary>
  [RefreshProperties(RefreshProperties.All)]
  public FormDesignerAction FormDesignerAction { get; set; }

  /// <summary>Параметры действий.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [RefreshProperties(RefreshProperties.All)]
  public IFormDesignerActionParams FormDesignerActionParams
  {
    get => this.FormDesignerAction.ActionParams;
    set => this.FormDesignerAction.ActionParams = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._hint;
    set
    {
      this._hint = value;
      if (this._parentForm == null)
        return;
      this._parentForm.ToolTip.SetToolTip((Control) this, value);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue("")]
  public new object Tag
  {
    get => base.Tag;
    set => base.Tag = value;
  }

  /// <summary>Установка текста для кнопки.</summary>
  public override string Text
  {
    get => base.Text;
    set => base.Text = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <remarks>Для того, чтобы не сериализовать значение (т.к. не используется)</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new bool UseVisualStyleBackColor
  {
    get => base.UseVisualStyleBackColor;
    set => base.UseVisualStyleBackColor = value;
  }

  /// <summary>Конструктор.</summary>
  public AttrButton()
  {
    this.Name = string.Empty;
    this.FormDesignerAction = FormDesignerAction.Empty;
    this._savedSize = this.Size;
    this.Click += new EventHandler(this.OnAttrButton_Click);
    this.Move += new EventHandler(this.OnAttrButton_Move);
    this.SizeChanged += new EventHandler(this.OnAttrButton_SizeChanged);
  }

  /// <summary>Нажатие кнопки.</summary>
  private void OnAttrButton_Click(object sender, EventArgs e)
  {
    if (!(ServicesManager.GetService(typeof (IFormDesignerActionManager)) is IFormDesignerActionManager service))
      return;
    service.GetAction((object) this.FormDesignerAction)?.ButtonPressed((object) this, (object) this._parentForm);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnAttrButton_Move(object sender, EventArgs e)
  {
    if (this.AutoSize && (Cursor.Current == Cursors.SizeAll || Cursor.Current == Cursors.SizeNS || Cursor.Current == Cursors.SizeWE || Cursor.Current == Cursors.SizeNESW || Cursor.Current == Cursors.SizeNWSE))
    {
      int num1 = this.Location.X;
      int num2 = num1.CompareTo(this._savedLocation.X);
      num1 = this.Location.Y;
      int num3 = num1.CompareTo(this._savedLocation.Y);
      if (num2 == 1 && this._bWidthIncr || num2 == -1 && !this._bWidthIncr || num3 == 1 && this._bHeightIncr || num3 == -1 && !this._bHeightIncr)
        this.Location = this._savedLocation;
      else
        this._savedLocation = this.Location;
    }
    else
    {
      if (!(this._savedLocation != this.Location))
        return;
      this._savedLocation = this.Location;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnAttrButton_SizeChanged(object sender, EventArgs e)
  {
    this._bWidthIncr = this._savedSize.Width < this.Size.Width;
    this._bHeightIncr = this._savedSize.Height < this.Size.Height;
    this._savedSize = this.Size;
  }

  /// <summary>Устанавливает родительскую форму.</summary>
  public DesForm DesForm
  {
    set
    {
      this._parentForm = value;
      if (this._parentForm == null)
        return;
      this._parentForm.ToolTip.SetToolTip((Control) this, this._hint);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._parentForm = (DesForm) null;
      this.Click -= new EventHandler(this.OnAttrButton_Click);
      this.Move -= new EventHandler(this.OnAttrButton_Move);
      this.SizeChanged -= new EventHandler(this.OnAttrButton_SizeChanged);
      this.ImageList = (ImageList) null;
      if (base.BackgroundImage != null)
      {
        base.BackgroundImage.Dispose();
        base.BackgroundImage = (Image) null;
      }
      if (this.Image != null)
      {
        this.Image.Dispose();
        this.Image = (Image) null;
      }
    }
    base.Dispose(disposing);
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont()
  {
    return this.Parent != null && !this.Parent.Font.Equals((object) this.Font);
  }

  /// <summary>Необходимость сериализации свойства ForeColor.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeForeColor()
  {
    return this.Parent != null && !this.Parent.ForeColor.Equals((object) this.ForeColor);
  }

  /// <summary>
  /// Необходимость сериализации свойства FormDesignerAction.
  /// </summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFormDesignerAction()
  {
    return this.FormDesignerAction.ActionGuid != Guid.Empty;
  }
}
