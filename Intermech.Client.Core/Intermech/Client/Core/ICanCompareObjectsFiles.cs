
// Type: Intermech.Client.Core.ICanCompareObjectsFiles
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using System.Collections.ObjectModel;


namespace Intermech.Client.Core;

/// <summary>
/// Интерфейс для плагинов, которые умеют сравнивать файлы
/// </summary>
public interface ICanCompareObjectsFiles
{
  /// <summary>Имя плагина</summary>
  string UniqueName { get; }

  /// <summary>Наименование плагина в системных сообщениях</summary>
  string NameInMessages { get; }

  /// <summary>Типы объектов, файлы которых плагин умеет сравнивать</summary>
  ReadOnlyCollection<int> TypeIds { get; }

  /// <summary>
  /// Удаляет из списка тип объектов
  /// Метод нужен для того, чтобы не было подписки двух разных плагинов на один и тот же тип.
  /// Кто первый подписался - тот и сравнивает. У других плагинов этот тип должен быть удален.
  /// </summary>
  /// <param name="typeId"></param>
  void RemoveTypeId(int typeId);

  /// <summary>Сравнение файлов объектов</summary>
  /// <param name="object1">Первый объект</param>
  /// <param name="object2">Второй объект</param>
  /// <param name="fileType">Тип файла</param>
  void CompareFilesFor(DBObjectToCompare object1, DBObjectToCompare object2, FileTypes fileType);
}
