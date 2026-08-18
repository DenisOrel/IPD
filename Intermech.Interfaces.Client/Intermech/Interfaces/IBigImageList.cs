// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.IBigImageList
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces;

/// <summary>Сервис для работы с большими 48x48 картинками</summary>
public interface IBigImageList
{
  /// <summary>Добавляет картинку в список.</summary>
  /// <param name="image">Картинками для добавления. Копия не создается, image.Dispose() не нужен</param>
  /// <param name="name">Имя картинки</param>
  /// <returns>Индекс в списке</returns>
  int Add(Image image, string name);

  /// <summary>Возвращает индекс картинки по имени</summary>
  /// <param name="name">Требуемое имя</param>
  /// <returns>Индекс в списке или -1.</returns>
  int ImageIndex(string name);

  /// <summary>Объект ImageList для размера 48x48</summary>
  ImageList ImageList { get; }

  /// <summary>Возвращает имя картинки по ее индексу</summary>
  /// <param name="imageIndex">Индекс картинки</param>
  /// <returns></returns>
  string ImageName(int imageIndex);

  /// <summary>Добавляет группу изображений в список</summary>
  /// <param name="images">Группа изображений</param>
  /// <param name="names">Имена изображений</param>
  /// <returns>Индекс первого изображения в списке</returns>
  int AddStrip(Image images, string[] names);
}
