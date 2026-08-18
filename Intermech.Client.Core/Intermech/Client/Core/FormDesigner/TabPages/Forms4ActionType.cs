
// Type: Intermech.Client.Core.FormDesigner.TabPages.Forms4ActionType
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.FormDesigner.TabPages;

/// <summary>
/// 
/// </summary>
internal enum Forms4ActionType
{
  /// <summary>Нет действия.</summary>
  None,
  /// <summary>Добавить форму на тип объекта(связи).</summary>
  AddForm,
  /// <summary>Удалить форму у типа объекта(связи).</summary>
  DeleteForm,
  /// <summary>Редактировать форму.</summary>
  EditForm,
  /// <summary>
  /// Включить пользователя или группу на тип объекта(связи).
  /// </summary>
  Include,
  /// <summary>Исключить пользователя из типа объекта(связи).</summary>
  Exclude,
  /// <summary>Установить условие.</summary>
  SetCondition,
}
