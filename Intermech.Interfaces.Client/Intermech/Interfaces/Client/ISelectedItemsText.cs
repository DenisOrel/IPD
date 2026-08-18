// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ISelectedItemsText
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс реализуется элементами управления Навигатора,
/// которые позволяют вернуть содержимое своих выделенных элементов
/// в виде текста, с форматированием, разделителями, заголовками столбцов, т.п.
/// </summary>
public interface ISelectedItemsText
{
  /// <summary>
  /// Получить текст выделенных в элементе управления Навигатора данных
  /// </summary>
  /// <param name="options">Параметры получения текста</param>
  /// <param name="cellsSeparator">Разделитель между значениями отдельных ячеек</param>
  /// <param name="rowsSeparator">Разделитель между строками текста</param>
  /// <returns>Полученный текст</returns>
  string GetSelectedItemsText(
    SelectedItemsTextOptions options,
    string cellsSeparator,
    string rowsSeparator);
}
