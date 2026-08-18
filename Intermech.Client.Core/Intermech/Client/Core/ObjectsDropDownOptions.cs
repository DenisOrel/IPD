
// Type: Intermech.Client.Core.ObjectsDropDownOptions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core;

/// <summary>
/// Параметры для "обёртки" над элементом Intermech.Bars.DropDownMenuItem
/// </summary>
[Flags]
[Serializable]
public enum ObjectsDropDownOptions
{
  /// <summary>
  /// Стандартные параметры (ShowText, ShowItemsImages, MoveSelectedOnTop, WithGroupItem, AutoAppendNewObjects)
  /// </summary>
  Default = 310, // 0x00000136
  /// <summary>Никаких опций не требуется</summary>
  None = 0,
  /// <summary>
  /// Элемент занимает всё свободное пространство на панели инструментов.
  /// </summary>
  Stretch = 1,
  /// <summary>
  /// Кроме изображения должен отображаться текст выбранного элемента.
  /// Если задана опция SelectOnly, то в качестве текста будет использоваться
  /// отдельное поле, а не выбранный элемент
  /// </summary>
  ShowText = 2,
  /// <summary>Отображать изображения у элементов списка</summary>
  ShowItemsImages = 4,
  /// <summary>
  /// Перемещать отмеченный элемент в начало списка (в начало группы, если задана опция WithGroupItem)
  /// </summary>
  MoveSelectedOnTop = 16, // 0x00000010
  /// <summary>
  /// Первый элемент списка - группирующий или отменяющий выбор, отделён от остальных
  /// элементов разделителем. Параметр игнорируется, если задана опция SelectOnly
  /// </summary>
  WithGroupItem = 32, // 0x00000020
  /// <summary>
  /// Автоматически добавлять созданные объекты отслеживаемых типов в коллекцию.
  /// Если задана опция MoveSelectedOnTop, объекты будут размещены в начале списка
  /// </summary>
  AutoAppendNewObjects = 256, // 0x00000100
  /// <summary>
  /// Выбор любого элемента генерирует событие, но не выполняет
  /// замену содержимого главной кнопки. Пример: список "Недавние объекты"
  /// </summary>
  SelectOnly = 268435456, // 0x10000000
}
