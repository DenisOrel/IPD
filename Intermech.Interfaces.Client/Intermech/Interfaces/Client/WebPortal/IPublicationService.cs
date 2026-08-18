// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.WebPortal.IPublicationService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.WebPortal;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client.WebPortal;

/// <summary>Сервис клиентской части публикации</summary>
public interface IPublicationService
{
  /// <summary>
  /// Опубликовать объекты на портал с предварительным отображением окна публикации
  /// </summary>
  /// <param name="items">Коллекция выбранных объектов в Навигаторе</param>
  /// <returns></returns>
  bool PublishWithDialog(ISelectedItems items);

  /// <summary>
  /// Опубликовать объекты на портал с предварительным отображением окна публикации
  /// </summary>
  /// <param name="items">Коллекция объектов для публикации Tuple([идентификатор версии объекта],[тип объекта])</param>
  /// <returns></returns>
  bool PublishWithDialog(List<Tuple<long, int>> items);

  /// <summary>
  /// Показывает окно публикации в режиме изменения настроек и просмотра состава, без публикации
  /// </summary>
  /// <param name="items"></param>
  /// <param name="options"></param>
  /// <returns>True, если окно было закрыто по OK</returns>
  bool ShowPublishOptions(List<Tuple<long, int>> items, ExtendedPublishOptions options);
}
