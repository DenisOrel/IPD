
// Type: Intermech.UI.Wpf.Controls.IFindReplaceTextEditor
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml


namespace Intermech.UI.Wpf.Controls;

/// <summary>
/// Интерфейс элемента редактирования текста, предоставляющий доступ к API для
/// поиска и замены фрагментов текста. Используется в <see cref="T:Intermech.UI.Wpf.Controls.FindReplaceManager" /> для
/// интеграции с UI.
/// </summary>
public interface IFindReplaceTextEditor
{
  string Text { get; }

  int SelectionStart { get; }

  int SelectionLength { get; }

  /// <summary>
  /// Selects the specified portion of Text and scrolls that part into view.
  /// </summary>
  /// <param name="start"></param>
  /// <param name="length"></param>
  void Select(int start, int length);

  void Replace(int start, int length, string ReplaceWith);

  /// <summary>This method is called before a replace all operation.</summary>
  void BeginChange();

  /// <summary>This method is called after a replace all operation.</summary>
  void EndChange();
}
