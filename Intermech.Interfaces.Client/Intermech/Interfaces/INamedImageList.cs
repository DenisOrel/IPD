// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.INamedImageList
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces;

/// <summary>Сервис для работы с именоваными иконками</summary>
public interface INamedImageList
{
  /// <summary>Добавляет иконку в список.</summary>
  /// <param name="icon">Иконка для добавления. При добавлении создается копия, поэтому может потребоваться Dispose()</param>
  /// <param name="name">Имя иконки</param>
  /// <returns>Индекс в списке</returns>
  /// <remarks>В зависимости от назначения списка, внутренний размер изображени может
  /// быть и не 16х16.</remarks>
  int Add(Icon icon, string name);

  /// <summary>Добавляет изображение в список.</summary>
  /// <param name="image">Изображение. При добавлении создается копия, поэтому может потребоваться Dispose()</param>
  /// <param name="name">Имя изображения</param>
  /// <returns>Индекс в списке</returns>
  int Add(Image image, string name);

  /// <summary>Добавляет группу изображений в список</summary>
  /// <param name="images">Группа изображений.</param>
  /// <param name="names">Имена изображений</param>
  /// <returns>Индекс первого изображения в списке</returns>
  int AddStrip(Image images, string[] names);

  /// <summary>Возвращает индекс иконки по имени</summary>
  /// <param name="name">Требуемое имя</param>
  /// <returns>Индекс в списке или -1.</returns>
  int ImageIndex(string name);

  /// <summary>Объект ImageList для размера 16х16</summary>
  ImageList ImageList { get; }

  /// <summary>Возвращает имя иконки по ее индексу</summary>
  /// <param name="imageIndex">Индекс иконки</param>
  /// <returns>Имя иконки</returns>
  string ImageName(int imageIndex);
}
