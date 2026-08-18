
// Type: Intermech.Client.Core.ObjectCreator.Controls.PageSaveArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator.Controls;

/// <summary>
/// Информации для метода сохранения шага мастера создания объектов
/// </summary>
public class PageSaveArgs : PageArgs
{
  /// <summary>
  /// Индекс шага на который переходит мастер создания объектов
  /// </summary>
  public int NextPageIndex;
  /// <summary>
  /// Текущий контрол, чтобы контрол, получающий сообщение, хотя бы мог понять, он текущий или нет
  /// </summary>
  public UserControl currControl;
  /// <summary>тип произошедшей ошибки</summary>
  public ErrorType errorType;

  public PageSaveArgs(int nextIndex) => this.NextPageIndex = nextIndex;
}
