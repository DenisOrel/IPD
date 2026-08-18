
// Type: Intermech.Client.Core.Visualizers.IBackgroundPager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Visualizers;

/// <summary>Документ с фоновой разбивкой на страницы</summary>
public interface IBackgroundPager : IPager
{
  /// <summary>Событие возникает при добавлении новой страницы</summary>
  event PageEventHandler NewPageAdded;

  /// <summary>Останавливает процесс разбиения документа на страницы</summary>
  void Abort();
}
