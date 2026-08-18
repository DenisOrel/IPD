
// Type: Intermech.Client.Core.CompositionView.CVButtonEnabled
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.CompositionView;

/// <summary>
/// Структура для определения доступности (enabled/disabled) кнопок
/// </summary>
/// <summary>Конструктор</summary>
/// <param name="add">добавить</param>
/// <param name="insertBefore">вставить перед</param>
/// <param name="insertInto">вставить внутрь</param>
/// <param name="insertAfter">вставить после</param>
/// <param name="replace">заменить</param>
public struct CVButtonEnabled(
  bool add,
  bool insertBefore,
  bool insertInto,
  bool insertAfter,
  bool replace)
{
  /// <summary>Добавить</summary>
  public bool Add = add;
  /// <summary>Вставить перед</summary>
  public bool InsertBefore = insertBefore;
  /// <summary>Вставить внутрь</summary>
  public bool InsertInto = insertInto;
  /// <summary>Вставить после</summary>
  public bool InsertAfter = insertAfter;
  /// <summary>Заменить</summary>
  public bool Replace = replace;

  /// <summary>Пустое значение false (для всех)</summary>
  public static CVButtonEnabled Empty => new CVButtonEnabled(false, false, false, false, false);
}
