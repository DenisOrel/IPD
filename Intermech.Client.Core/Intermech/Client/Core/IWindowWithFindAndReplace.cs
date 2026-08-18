
// Type: Intermech.Client.Core.IWindowWithFindAndReplace
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core;

/// <summary> Интерфейс, который должны поддерживать все окна, которые поддерживают функциональность поиска с заменой в своём содержимом </summary>
public interface IWindowWithFindAndReplace : IWindowWithFind
{
  /// <summary> Вызывается при нажатии кнопки "Заменить" </summary>
  /// <param name="findController"> Ссылка на интерфейс окна настройки поиска и замены </param>
  void Replace(IFindController findController);

  /// <summary> Вызывается при нажатии кнопки "Заменить все" </summary>
  /// <param name="findController"> Ссылка на интерфейс окна настройки поиска и замены </param>
  void ReplaceAll(IFindController findController);
}
