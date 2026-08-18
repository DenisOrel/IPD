
// Type: Intermech.Client.Core.Visualizers.IPager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Visualizers;

/// <summary>
/// Интерфейс для поддержки просмотра документов, содержащих страницы
/// </summary>
public interface IPager
{
  /// <summary>Переход на первую страницу</summary>
  void First();

  /// <summary>Переход на следующую страницу</summary>
  void Next();

  /// <summary>Переход на предыдущую страницу</summary>
  void Prev();

  /// <summary>Переход на последнюю страницу</summary>
  void Last();

  /// <summary>Список страниц</summary>
  object[] Pages { get; }

  /// <summary>Текущая страница</summary>
  object Current { get; set; }

  event EventHandler Refit;

  event EventHandler Refresh;

  /// <summary>Событие перехода на другую страницу</summary>
  event EventHandler PageChanged;
}
