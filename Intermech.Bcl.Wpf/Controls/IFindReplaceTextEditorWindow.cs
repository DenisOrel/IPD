
// Type: Intermech.UI.Wpf.Controls.IFindReplaceTextEditorWindow
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System.Windows;


namespace Intermech.UI.Wpf.Controls;

/// <summary>
/// Интерфейс окна, в котором находится элемент редактирования текста, предоставляющий доступ к API для
/// поиска и замены фрагментов текста. Используется в <see cref="T:Intermech.UI.Wpf.Controls.FindReplaceManager" /> для
/// интеграции с UI.
/// </summary>
public interface IFindReplaceTextEditorWindow
{
  int Left { get; }

  int Top { get; }

  int Width { get; }

  int Height { get; }

  /// <summary>
  /// Устанавливает текущее окно в качестве владельца для диалога поиска и замены текста.
  /// </summary>
  /// <param name="findReplaceWindow">WPF-окно поиска и замены текста</param>
  /// <exception cref="!:ArgumentNullException">параметр <paramref name="findReplaceWindow" /> содержит null</exception>
  void SetOwnerWindow(Window findReplaceWindow);
}
