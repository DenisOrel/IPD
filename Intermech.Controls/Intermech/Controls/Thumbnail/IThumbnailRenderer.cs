
// Type: Intermech.Controls.Thumbnail.IThumbnailRenderer
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;


namespace Intermech.Controls.Thumbnail;

/// <summary>Summary description for IThumbnailRenderer.</summary>
public interface IThumbnailRenderer
{
  /// <summary>
  /// Указывает владельцу, что надо перерисовать содержимое  его окна
  /// </summary>
  event RedrawEventHandler RedrawRequired;

  /// <summary>Рисует данные в панели</summary>
  /// <param name="panelIndex">Индекс панели</param>
  /// <param name="g"></param>
  /// <param name="bounds">Область рисования</param>
  /// <param name="selected">Выбрана или нет</param>
  /// <param name="active">Активно окно или нет</param>
  void DrawPanel(int panelIndex, Graphics g, Rectangle bounds, bool selected, bool active);

  /// <summary>Минимальный размер элемента</summary>
  Size MinimumSize { get; }

  /// <summary>Максимальный размер элемента</summary>
  Size MaximumSize { get; }
}
