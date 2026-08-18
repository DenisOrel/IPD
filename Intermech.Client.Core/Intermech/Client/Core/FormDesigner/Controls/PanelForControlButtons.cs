
// Type: Intermech.Client.Core.FormDesigner.Controls.PanelForControlButtons
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Класс для отрисовки кнопок контрола.</summary>
public class PanelForControlButtons : List<ControlButton>
{
  /// <summary>Доступность кнопок на панели</summary>
  private bool _enabled = true;
  /// <summary>Кнопка, на которую наведена мышь</summary>
  private ControlButton _hotBtn;

  /// <summary>Область отрисовки кнопок.</summary>
  public Rectangle Bounds { get; set; }

  /// <summary>Доступность кнопок на панели.</summary>
  public bool Enabled
  {
    get => this._enabled;
    set => this.ForEach((Action<ControlButton>) (x => x.Enabled = value));
  }

  /// <summary>Высота панели.</summary>
  public int Height => FormDesignerUtils.ButtonSize.Height;

  /// <summary>Подсказка для текущей кнопки.</summary>
  public string Hint => this._hotBtn == null ? string.Empty : this._hotBtn.Hint;

  /// <summary>Расположение кнопок.</summary>
  public bool RightButtons { get; set; }

  /// <summary>Ширина панели.</summary>
  public int Width => this.Count * FormDesignerUtils.ButtonSize.Width;

  /// <summary>Конструктор.</summary>
  public PanelForControlButtons()
    : base(4)
  {
    this.Bounds = Rectangle.Empty;
    this.RightButtons = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="p"></param>
  /// <returns></returns>
  private ControlButton GetButton(Point p)
  {
    return this.ElementAtOrDefault<ControlButton>(this.GetButtonIndex(p));
  }

  /// <summary>Получение индекса кнопки.</summary>
  /// <param name="p">Координаты</param>
  /// <returns>Индекс кнопки</returns>
  private int GetButtonIndex(Point p)
  {
    return !this.Bounds.Contains(p) ? -1 : (p.X - this.Bounds.X - 1) / FormDesignerUtils.ButtonSize.Width;
  }

  /// <summary>Установить/снять выделение кнопки.</summary>
  /// <param name="index">Индекс кнопки</param>
  private void SetHotButton(ControlButton btn)
  {
    if (this._hotBtn != null)
      this._hotBtn.Hot = false;
    this._hotBtn = (ControlButton) null;
    if (btn == null || btn.State == PushButtonState.Disabled)
      return;
    btn.Hot = true;
    this._hotBtn = btn;
  }

  /// <summary>Добавление кнопки.</summary>
  /// <param name="button">Кнопка</param>
  /// <param name="needSort">Необходимость сортировки массива после вставки элемента</param>
  public void AddButton(ControlButton button, bool needSort = false)
  {
    if (!this.Contains(button))
      this.Add(button);
    if (!needSort)
      return;
    this.Sort((Comparison<ControlButton>) ((x, y) => x.Order.CompareTo(y.Order)));
  }

  /// <summary>Добавление кнопок.</summary>
  /// <param name="buttons">Список кнопок</param>
  /// <param name="needSort">Необходимость сортировки массива после вставки элемента</param>
  public void AddButtons(List<ControlButton> buttons, bool needSort = false)
  {
    foreach (ControlButton button in buttons)
    {
      if (!this.Contains(button))
        this.Add(button);
    }
    if (!needSort)
      return;
    this.Sort((Comparison<ControlButton>) ((x, y) => x.Order.CompareTo(y.Order)));
  }

  /// <summary>Удаление кнопок.</summary>
  /// <param name="buttons">Список кнопок</param>
  public void RemoveButtons(List<ControlButton> buttons)
  {
    buttons.ForEach((Action<ControlButton>) (x => this.Remove(x)));
  }

  /// <summary>Нажатие мыши по панели с кнопками.</summary>
  /// <param name="e"></param>
  public void MouseDown(MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Left)
      return;
    ControlButton button = this.GetButton(e.Location);
    if (button == null || button.State == PushButtonState.Disabled)
      return;
    button.OnClick();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public void MouseLeave(EventArgs e) => this.SetHotButton((ControlButton) null);

  /// <summary>Движение мыши над панелью с кнопками.</summary>
  /// <param name="e"></param>
  public void MouseMove(MouseEventArgs e) => this.SetHotButton(this.GetButton(e.Location));
}
