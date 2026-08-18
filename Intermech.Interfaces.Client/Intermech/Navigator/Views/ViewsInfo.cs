// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.ViewsInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Контейнер сведений о допустимых закладках, а также о закладках других
/// провайдеров, появление которых должно быть подавлено.
/// </summary>
public class ViewsInfo : ProviderInfo
{
  /// <summary>
  /// Пустой контейнер сведений о закладках, который может возвращаться
  /// провайдерами в тех случаях, когда для некоторого контекста нет
  /// допустимых закладок.
  /// </summary>
  private static ViewsInfo _empty = new ViewsInfo();

  /// <summary>
  /// Добавляет в контейнер информацию о закладке, появление которой на экране
  /// допустимо для данного контекста.
  /// </summary>
  /// <param name="viewName">Имя закладки.</param>
  /// <param name="viewInfo">Информация о закладке.</param>
  public void Add(string viewName, ViewInfo viewInfo)
  {
    this.AddPossibleItem(viewName, (object) viewInfo);
  }

  /// <summary>
  /// Добавляет в контейнер сведения о закладке, предоставляемой другим провайдером,
  /// появление которой на экране должно быть подавлено.
  /// </summary>
  /// <param name="viewName">Имя закладки.</param>
  /// <param name="priority">Приоритет закладки.</param>
  public void Suppress(string viewName, int priority)
  {
    this.AddPossibleItem(viewName, (object) new ViewInfo(priority));
  }

  /// <summary>
  /// Удаляет из контейнера сведения о закладке с указанным именем.
  /// </summary>
  /// <param name="viewName">Имя закладки.</param>
  public void Remove(string viewName) => this.RemovePossibleItem(viewName);

  /// <summary>
  /// Возвращает массив имен закладок, сведения о которых находятся в контейнере.
  /// Если в контейнер не было добавлено ни одной закладки, то результатом будет null.
  /// </summary>
  public string[] ViewNames => this.PossibleItems;

  /// <summary>
  /// Возвращает сведения о закладке с указанным именем.
  /// с указанным именем.
  /// </summary>
  /// <param name="viewName">Имя закладки.</param>
  /// <returns>Контейнер сведений о закладке.</returns>
  public ViewInfo GetInfo(string viewName) => (ViewInfo) this.GetPossibleItem(viewName);

  /// <summary>
  /// Возвращает пустой контейнер сведений о закладках, который может
  /// возвращаться провайдерами в тех случаях, когда для некоторого
  /// контекста нет допустимых закладок.
  /// </summary>
  public static ViewsInfo Empty => ViewsInfo._empty;
}
