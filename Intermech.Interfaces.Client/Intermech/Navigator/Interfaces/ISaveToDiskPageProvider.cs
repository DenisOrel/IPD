// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISaveToDiskPageProvider
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс провайдера страницы дополнительных настроек для окна команды "Сохранить на диск"
/// </summary>
public interface ISaveToDiskPageProvider
{
  /// <summary>
  /// Проверка, выдаст ли интерфейс контрол по InitPage для выбранных элементов
  /// </summary>
  /// <param name="items"></param>
  /// <returns></returns>
  bool CheckItems(ISelectedItems items);

  /// <summary>
  /// Инициализировать страницу дополнительных настроек и вернуть интерфейс управления страницей.
  /// </summary>
  /// <param name="items"></param>
  /// <param name="options">интерфейс чтения основных настроек; после окончания редактирования (ISaveToDiskPage.Commit/Cancel) обращаться к ISaveToDiskOptions options не рекомендуется - соответствующий контрол может быть уже освобождён</param>
  /// <returns>null, если для указанных items нет поддержки</returns>
  ISaveToDiskPage InitPage(ISelectedItems items, ISaveToDiskOptions options);
}
