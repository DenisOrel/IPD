// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.IDoubleNamedImageList
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces;

/// <summary>Сервис для работы с именоваными иконками</summary>
public interface IDoubleNamedImageList
{
  /// <summary>
  /// Добавляет иконку в список. Для двухразмерных списков иконка
  /// может содержать размеры 16х16 и 32х32
  /// </summary>
  /// <param name="icon">Иконка для добавления</param>
  /// <param name="name">Имя иконки</param>
  /// <returns>Индекс в списке</returns>
  /// <remarks>В зависимости от назначения списка, внутренний размер изображени может
  /// быть и не 16х16.</remarks>
  int Add(Icon icon, string name);

  /// <summary>
  /// Добавляет изображение в список. Для двухразмерных списков иконка
  /// увеличивается в размер 32х32 если она 16х16 или уменьшается до 16
  /// </summary>
  /// <param name="image">Изображение</param>
  /// <param name="name">Имя изображения</param>
  /// <returns>Индекс в списке</returns>
  int Add(Image image, string name);

  /// <summary>Добавляет изображения в список.</summary>
  /// <param name="image16">Изображение</param>
  /// <param name="image32">Увеличенное изображение</param>
  /// <param name="name">Имя изображения</param>
  /// <returns>Индекс в списке</returns>
  int Add(Image image16, Image image32, string name);

  /// <summary>Добавляет группу изображений в список</summary>
  /// <param name="images">Группа изображений</param>
  /// <param name="names">Имена изображений</param>
  /// <returns>Индекс первого изображения в списке</returns>
  int AddStrip(Image images, string[] names);

  /// <summary>Добавляет группу изображений в список</summary>
  /// <param name="images16">Группа изображений 16х16</param>
  /// <param name="images32">Группа изображений 32х32</param>
  /// <param name="names">Имена изображений</param>
  /// <returns>Индекс первого изображения в списке</returns>
  int AddStrip(Image images16, Image images32, string[] names);

  /// <summary>Возвращает индекс иконки по имени</summary>
  /// <param name="name">Требуемое имя</param>
  /// <returns>Индекс в списке или -1.</returns>
  int ImageIndex(string name);

  /// <summary>Объект ImageList для размера 16х16</summary>
  ImageList ImageList { get; }

  /// <summary>Объект ImageList для размера 32х32</summary>
  ImageList BigImageList { get; }

  Color TransparentColor { get; set; }
}
