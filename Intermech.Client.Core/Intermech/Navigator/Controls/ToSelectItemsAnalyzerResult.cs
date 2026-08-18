
// Type: Intermech.Navigator.Controls.ToSelectItemsAnalyzerResult
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Результат выполнения проверки очередного узла из коллекции выделенных элементов
/// в службе IToSelectItemsAnalyzer
/// </summary>
public enum ToSelectItemsAnalyzerResult
{
  /// <summary>Не выделять указанный элемент</summary>
  Skip = 0,
  /// <summary>
  /// Требуется выделить указанный элемент и продолжить анализ дальше
  /// (если в контроле поддерживается множественное выделение элементов)
  /// </summary>
  Select = 1,
  /// <summary>
  /// Требуется выделить указанный элемент и завершить работу анализатора
  /// (если в контроле не поддерживается множественное выделение элементов).
  /// Внимание! Элемент зарезервирован для будущих реализаций Навигатора
  /// </summary>
  SelectAndBreak = 2,
  /// <summary>
  /// Требуется выделить указанный элемент и продолжить работу анализатора
  /// уровнем ниже (если в контроле не поддерживается множественное выделение элементов,
  /// а сам контрол хранит древовидную структуру элементов).
  /// Внимание! Элемент зарезервирован для будущих реализаций Навигатора
  /// </summary>
  SelectAndMoveDown = 2,
}
