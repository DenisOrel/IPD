
// Type: Intermech.Client.Core.IFindOrReplaceTextController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;


namespace Intermech.Client.Core;

/// <summary>
/// Интерфейс, который реализуют элементы пользовательского интерфейса,
/// отвечающие за настройку поиска или поиска с заменой текста
/// </summary>
public interface IFindOrReplaceTextController
{
  /// <summary> Признак того, что используется расширеная форма настройки поиска (с доп. параметрами поиска) </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  bool IsExpanded { get; set; }

  /// <summary> Строка поиска </summary>
  string FindWhat { get; set; }

  /// <summary> На что требуется заменять найденый текст </summary>
  string ReplaceWith { get; set; }

  /// <summary> Список доступных мест для поиска текста (например, поиск в [текущем документе], [на текущей странице] и т.п.) </summary>
  string[] PossibleSearchPlaces { get; set; }

  /// <summary> Индекс выбраного места для поиска в PossibleSearchPlaces </summary>
  int SelectedSearchPlace { get; set; }

  /// <summary> Направление сортировки </summary>
  SearchDirrection SearchDirrection { get; set; }

  /// <summary> Признак того, что поиск должен вестись с учётом регистра </summary>
  bool MatchCase { get; set; }

  /// <summary> Признак того, что ищется слово "целиком" </summary>
  bool MatchWholeWord { get; set; }

  /// <summary> Признак того, что при поиске должны быть использованы регулярные выражения </summary>
  bool UseRegularExpressions { get; set; }
}
