
// Type: Intermech.Controls.IMMessageBoxButton
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary>Кнопка, размещаемая в окне IMMessageBox</summary>
public class IMMessageBoxButton
{
  /// <summary>Надпись на кнопке</summary>
  public string Caption;
  /// <summary>Возвращаемый кнопкой результат</summary>
  public DialogResult MessageResult;
  /// <summary>Возвращаемый кнопкой результат</summary>
  public DialogResultAdv MessageResultAdv;
  /// <summary>Дополнительные данные</summary>
  public object Tag;
  /// <summary>
  /// Признак, что это кнопка по умолчанию. Если таких кнопок несколько, то будет использована первая из них с этим признаком.
  /// </summary>
  public bool IsDefaultButton;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="caption">Заголовок кнопки</param>
  /// <param name="messageResult">Результат кнопки</param>
  public IMMessageBoxButton(string caption, DialogResult messageResult)
  {
    this.Caption = caption;
    this.MessageResult = messageResult;
    this.MessageResultAdv = DialogResultAdv.None;
    this.Tag = (object) null;
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="caption">Заголовок кнопки</param>
  /// <param name="messageResult">Результат кнопки</param>
  public IMMessageBoxButton(string caption, DialogResultAdv messageResult)
  {
    this.Caption = caption;
    this.MessageResult = DialogResult.None;
    this.MessageResultAdv = messageResult;
    this.Tag = (object) this;
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="caption">Заголовок кнопки</param>
  /// <param name="tag">Дополнительные данные</param>
  public IMMessageBoxButton(string caption, object tag)
  {
    this.Caption = caption;
    this.MessageResult = DialogResult.OK;
    this.MessageResultAdv = DialogResultAdv.None;
    this.Tag = tag;
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="caption">Заголовок кнопки</param>
  /// <param name="messageResult">Результат кнопки</param>
  /// <param name="tag">Дополнительные данные</param>
  public IMMessageBoxButton(string caption, DialogResult messageResult, object tag)
  {
    this.Caption = caption;
    this.MessageResult = messageResult;
    this.MessageResultAdv = DialogResultAdv.None;
    this.Tag = tag;
  }
}
