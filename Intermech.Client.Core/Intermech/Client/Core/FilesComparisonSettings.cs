
// Type: Intermech.Client.Core.FilesComparisonSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Client.Core;

/// <summary>
/// Настройки сравнения файлов разных расширений через стороннее приложение
/// </summary>
[Serializable]
public class FilesComparisonSettings
{
  /// <summary>
  /// Наименование настройки (условное название программы, в которой будет происходить сравнение), которое задал пользователь
  /// </summary>
  public string Name { get; set; }

  /// <summary>Путь к исполняемому файлу программы</summary>
  public string ProgramExePath { get; set; }

  /// <summary>Аргументы командной строки</summary>
  public string Arguments { get; set; }

  /// <summary>
  /// Расширения через ";", которые настроены на данное приложение
  /// Без точки
  /// </summary>
  public string ExtensionsAsString { get; set; }

  public List<string> ExtensionsAsList { get; }

  public FilesComparisonSettings(
    string name,
    string programExePath,
    string arguments,
    string extensionsAsString)
  {
    this.Name = name;
    this.ProgramExePath = programExePath;
    this.Arguments = arguments;
    this.ExtensionsAsString = extensionsAsString;
    this.ExtensionsAsList = this.ParseExtensionStringToList();
  }

  /// <summary>Возвращает список расширений, созданный из строки</summary>
  /// <returns></returns>
  public List<string> ParseExtensionStringToList()
  {
    List<string> extensionStringToList = new List<string>();
    if (this.ExtensionsAsString == string.Empty)
      return extensionStringToList;
    return ((IEnumerable<string>) this.ExtensionsAsString.Replace(" ", "").Replace(".", "").Split(';')).ToList<string>();
  }

  public override string ToString()
  {
    return $"{this.Name}¦{this.ProgramExePath}¦{this.ExtensionsAsString}¦{this.Arguments}";
  }
}
