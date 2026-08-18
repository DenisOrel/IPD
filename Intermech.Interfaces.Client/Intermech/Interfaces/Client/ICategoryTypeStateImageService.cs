// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICategoryTypeStateImageService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Сервис для хранения изображений элементов навигации, привязанных к
/// категориям, типам и состояниям элементов.
/// </summary>
public interface ICategoryTypeStateImageService
{
  /// <summary>
  /// Возвращает индекс зарегистрированного изображения или -1, если изображение не найдено.
  /// </summary>
  /// <param name="categoryId">Требуемая категория</param>
  /// <param name="typeId">Требуемый тип</param>
  /// <param name="data">Дополнительные данные</param>
  /// <param name="state">Состояние элемента навигации</param>
  /// <returns>Индекс изображение в ImageList</returns>
  int IndexOf(int categoryId, int typeId, object data, object state);

  /// <summary>
  /// Событие, которое возникает, если изображение для запрошенного элемента навигации
  /// еще не загружено.
  /// </summary>
  event FindStateImageEventHandler FindStateImage;
}
