
// Type: Intermech.Client.Core.FormDesigner.Controls.ControlButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms.VisualStyles;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Кнопка.</summary>
public class ControlButton
{
  /// <summary>Наименование кнопки</summary>
  private string _name = string.Empty;
  /// <summary>Наименование кнопки в недоступном состоянии</summary>
  private string _nameDisabled = string.Empty;
  /// <summary>Доступность кнопки</summary>
  private bool _enabled = true;

  /// <summary>Доступность кнопки.</summary>
  public bool Enabled
  {
    get => this._enabled;
    set
    {
      this._enabled = value;
      this.State = this._enabled ? PushButtonState.Normal : PushButtonState.Disabled;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public string Hint { get; set; }

  /// <summary>Подсвеченное состояние кнопки.</summary>
  public bool Hot
  {
    get => this.State == PushButtonState.Hot;
    set
    {
      if (this.State == PushButtonState.Disabled)
        return;
      this.State = value ? PushButtonState.Hot : PushButtonState.Normal;
    }
  }

  /// <summary>Наименование кнопки.</summary>
  public string Name => !this.Enabled ? this._nameDisabled : this._name;

  /// <summary>Порядковый индекс.</summary>
  public int Order { get; private set; }

  /// <summary>Состояние для отрисовки.</summary>
  public PushButtonState State { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public object Tag { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="name">Наименование</param>
  /// <param name="order">Порядковый номер</param>
  public ControlButton(string name, int order)
  {
    this._name = name;
    this._nameDisabled = $"{name}Disabled";
    this.Order = order;
    this.State = PushButtonState.Normal;
    this.Hint = FormDesignerUtils.ButtonHints.ContainsKey(name) ? FormDesignerUtils.ButtonHints[name] : string.Empty;
  }

  /// <summary>Нажатие кнопки.</summary>
  public event EventHandler Click;

  /// <summary>Нажатие кнопки.</summary>
  public void OnClick()
  {
    if (this.Click == null || !this._enabled)
      return;
    this.Click((object) this, new EventArgs());
  }
}
