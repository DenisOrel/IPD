// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IViewArea
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Область просмотра в файловом хранилище пользователя. Все методы интерфейса являются thread-safe.
/// </summary>
public interface IViewArea : IFileArea
{
  /// <summary>
  /// Публикует список объектов в области просмотра файлового хранилища.
  /// </summary>
  /// <param name="objectList">Список версий публикуемых объектов</param>
  /// <returns>Описатель головного объекта после публикации</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список версий объектов не может быть null</exception>
  PublishedObject Publish(IList<DBObjectState> objectList);

  /// <summary>Очищает область просмотра.</summary>
  void Cleanup();
}
