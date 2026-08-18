
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypeNodeOptions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Набор дополнительных свойств элемента пространства навигации для типов объектов
/// </summary>
[Flags]
[Serializable]
public enum ObjectTypeNodeOptions
{
  /// <summary>Никаких опций нет</summary>
  None = 0,
  /// <summary>Режим работы - "Показывать только типы объектов"</summary>
  OnlyTypesMode = 1,
  /// <summary>
  /// Показать закладку со списком шагов ЖЦ для текущего типа объекта
  /// </summary>
  ShowLCSteps = 2,
  /// <summary>
  /// Запрос в базу выполняться не будет
  /// (эмулируется вся цепочка Node - NonFolders - Part - Query, чтобы корректно работал
  /// механизм настройки отображения в Навигаторе, если у пользователя нет прав доступа
  /// для просмотра списка объектов указанного типа. В этом случае нет ни одного
  /// NonFolderPart, соответственно нет ни одной колонки в списке SupportColumns)
  /// </summary>
  EmptyQuery = 1073741824, // 0x40000000
  /// <summary>Значение свойств по умолчанию - "Никаких опций нет"</summary>
  Default = 0,
}
