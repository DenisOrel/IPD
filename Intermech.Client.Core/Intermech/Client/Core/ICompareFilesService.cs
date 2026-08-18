
// Type: Intermech.Client.Core.ICompareFilesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Client.Core;

/// <summary>Клиентский сервис для сравнения файлов</summary>
public interface ICompareFilesService
{
  /// <summary>Зарегистрировать плагин на типы объектов</summary>
  /// <param name="plugin">Плагин</param>
  void AddPluginToCompareFilesService(ICanCompareObjectsFiles plugin);

  /// <summary>Удалить плагин</summary>
  /// <param name="plugin"></param>
  void DeletePluginFromCompareFilesService(ICanCompareObjectsFiles plugin);

  /// <summary>Сравнить файлы для объектов</summary>
  /// <param name="itemsForCompare">Ид объектов, файлы которых надо будет сравнить</param>
  void CompareTwoObjectsFiles(ISelectedItems itemsForCompare, FileTypes fileType);

  /// <summary>Получить настройки сравнения файлов через приложения</summary>
  /// <returns>Настройки сравнения файлов через приложения</returns>
  List<FilesComparisonSettings> GetAllFilesComparisonSettings();

  /// <summary>Cj[hfyb</summary>
  /// <param name="settings">Настройки сравнения файлов через приложения</param>
  void SaveFilesComparisonSettings(List<FilesComparisonSettings> settings);

  /// <summary>
  /// Сравнение файлов по общим правилам.
  /// Сначала проверяем на пдф, потом настройки системы, в крайнем случае показываем общую форму.
  /// </summary>
  /// <param name="object1">Объект для сравнеия</param>
  /// <param name="object2">Объект для сравнения</param>
  /// <param name="fileType">Тип файла для сравнения</param>
  void CompareFilesWithCommonRules(
    DBObjectToCompare object1,
    DBObjectToCompare object2,
    FileTypes fileType);
}
