
// Type: Intermech.Client.Core.FilesComparisonHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core;

/// <summary>Держатель констант для сравнения файлов</summary>
public class FilesComparisonHolder
{
  /// <summary>Модуль настроек для сравнения файлов</summary>
  public const string ModuleFilesComparison = "CLIENT";
  /// <summary>Секция настроек для сравнения файлов</summary>
  public const string SectionFilesComparison = "FILESCOMPARISON";
  /// <summary>
  /// Имена столбцов для таблицы, хранящей данные с настройками программ сравнения фалов
  /// </summary>
  public const string F_NAME = "F_NAME";
  public const string F_ARGS = "F_ARGS";
  public const string F_PATH = "F_PATH";
  public const string F_EXT = "F_EXT";
  public const string File1Key = "%file1";
  public const string File2Key = "%file2";
}
