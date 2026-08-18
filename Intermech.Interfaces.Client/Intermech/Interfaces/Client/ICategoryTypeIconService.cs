// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICategoryTypeIconService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Сервис для хранения икон, привязанных к категориям и(или) типам.
/// Иконы хранятся в двух параллельных ImageList размерами 16 и 32.
/// Индексы в этих списках для одной иконы одинаковые.
/// </summary>
public interface ICategoryTypeIconService
{
  /// <summary>
  /// Событие, которое возникает, если икона для запрошенного типа и
  /// категории еще не загружена.
  /// </summary>
  event FindIconEventHandler FindIcon;

  /// <summary>
  /// Добавляет иконку для категории.
  /// В сервисе создается клон иконки.
  /// !!! ПОСЛЕ ДОБАВЛЕНИЯ ИКОНА ДОЛЖНА БЫТЬ УНИЧТОЖЕНА !!!
  /// </summary>
  /// <param name="icon">Иконка. При добавлении создается копия, поэтому может потребоваться icon.Dispose()</param>
  /// <param name="category">Требуемая категория</param>
  /// <returns>Индекс в ImageList</returns>
  int AddIcon(Icon icon, int category);

  /// <summary>
  /// Добавляет иконку для категории и типа.
  /// В сервисе создается клон иконки.
  /// !!! ПОСЛЕ ДОБАВЛЕНИЯ ИКОНА ДОЛЖНА БЫТЬ УНИЧТОЖЕНА !!!
  /// </summary>
  /// <param name="icon">Иконка. При добавлении создается копия, поэтому может потребоваться icon.Dispose()</param>
  /// <param name="category">Требуемая категория</param>
  /// <param name="type">Требуемый тип</param>
  /// <returns>Индекс в ImageList</returns>
  int AddIcon(Icon icon, int category, int type);

  /// <summary>
  /// Добавляет иконку для категории и типа
  /// В сервисе создается клон иконки.
  /// !!! ПОСЛЕ ДОБАВЛЕНИЯ ИКОНА ДОЛЖНА БЫТЬ УНИЧТОЖЕНА !!!
  /// </summary>
  /// <param name="icon">Иконка. При добавлении создается копия, поэтому может потребоваться icon.Dispose()</param>
  /// <param name="category">Требуемая категория</param>
  /// <param name="type">Требуемый тип</param>
  /// <param name="data">Дополнительные данные</param>
  /// <returns>Индекс в ImageList</returns>
  int AddIcon(Icon icon, int category, int type, object data);

  /// <summary>
  /// Возвращает индекс зарегистрированной иконки для категории
  /// </summary>
  /// <param name="category">Требуемая категория</param>
  /// <returns>Индекс в ImageList</returns>
  int IndexOf(int category);

  /// <summary>
  /// Возвращает индекс зарегистрированной иконки для категории и типа
  /// </summary>
  /// <param name="category">Требуемая категория</param>
  /// <param name="type">Требуемый тип</param>
  /// <returns>Индекс в ImageList</returns>
  int IndexOf(int category, int type);

  /// <summary>
  /// Возвращает индекс зарегистрированной иконки для категории и типа
  /// </summary>
  /// <param name="category">Требуемая категория</param>
  /// <param name="type">Требуемый тип</param>
  /// <param name="data">Дополнительные данные</param>
  /// <returns>Индекс в ImageList</returns>
  int IndexOf(int category, int type, object data);

  /// <summary>ImageList 32x16</summary>
  ImageList ImageList { get; }

  /// <summary>ImageList 32x32</summary>
  ImageList BigImageList { get; }

  /// <summary>
  /// Возвращает иконку. Если  иконка не зарегистрирована,
  /// возвращает иконку по умолчанию.
  /// Передается внутренний объект, освобождать не требуется.
  /// </summary>
  /// <param name="category"></param>
  /// <returns></returns>
  Icon GetIcon(int category);

  /// <summary>
  /// Возвращает иконку. Если  иконка не зарегистрирована,
  /// возвращает иконку по умолчанию.
  /// Передается внутренний объект, освобождать не требуется.
  /// </summary>
  /// <param name="category"></param>
  /// <param name="type"></param>
  /// <returns></returns>
  Icon GetIcon(int category, int type);

  /// <summary>
  /// Возвращает иконку. Если  иконка не зарегистрирована,
  /// возвращает иконку по умолчанию.
  /// Передается внутренний объект, освобождать не требуется.
  /// </summary>
  /// <param name="category"></param>
  /// <param name="type"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  Icon GetIcon(int category, int type, object data);

  Icon GetIndexIcon(int index);

  /// <summary>
  /// Возвращает иконку. Если  иконка не зарегистрирована,
  /// возвращает null.
  /// Передается внутренний объект, освобождать не требуется.
  /// </summary>
  /// <param name="category"></param>
  /// <returns></returns>
  Icon GetIconEx(int category);

  /// <summary>
  /// Возвращает иконку. Если  иконка не зарегистрирована,
  /// возвращает null.
  /// Передается внутренний объект, освобождать не требуется.
  /// </summary>
  /// <param name="category"></param>
  /// <param name="type"></param>
  /// <returns></returns>
  Icon GetIconEx(int category, int type);

  /// <summary>
  /// Возвращает иконку. Если  иконка не зарегистрирована,
  /// возвращает null.
  /// Передается внутренний объект, освобождать не требуется.
  /// </summary>
  /// <param name="category"></param>
  /// <param name="type"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  Icon GetIconEx(int category, int type, object data);

  /// <summary>Включение режима обновления</summary>
  /// <remarks>Данный режим отключает отправку уведомлений контролам при добавлении / создании иконок</remarks>
  void BeginUpdate();

  /// <summary>Отключение режима обновления</summary>
  /// <remarks>Данный режим восстанавливает отправку уведомлений контролам при добавлении / создании иконок</remarks>
  void EndUpdate();
}
