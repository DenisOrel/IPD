
// Type: Intermech.Client.Core.IWindowWithFind
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core;

/// <summary> Интерфейс, который должны поддерживать все окна, которые поддерживают функциональность поиска в своём содержимом </summary>
public interface IWindowWithFind
{
  /// <summary> Позволяет сервису поиска определить тип класса окна, в котором должна осуществляться настройка поиска </summary>
  /// <returns> Тип класса окна, в котором должна осуществляться настройка поиска </returns>
  Type GetFindSetupFormClass();

  /// <summary> Вызывается, когда в диалоге поиска была нажата кнопка "Найти далее" </summary>
  /// <param name="findController"> Ссылка на интерфейс окна настройки поиска </param>
  void FindNext(IFindController findController);
}
