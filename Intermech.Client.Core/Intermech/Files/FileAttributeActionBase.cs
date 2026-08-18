
// Type: Intermech.Files.FileAttributeActionBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Files;

/// <summary>
/// Необязательный базовый класс для реализаций интерфейса <see cref="T:Intermech.Files.IFileAttributeAction" />
/// </summary>
public abstract class FileAttributeActionBase : IFileAttributeAction
{
  /// <inheritdoc />
  public void Perform(IDBAttribute dbFileAttribute, List<string> initialFileNames)
  {
    if (dbFileAttribute == null)
      throw new ArgumentNullException(nameof (dbFileAttribute));
    if (initialFileNames == null)
      throw new ArgumentNullException(nameof (initialFileNames));
    this.DoPerform(dbFileAttribute, initialFileNames);
  }

  /// <summary>
  /// Выполняет действие над значениями указанного файлового атрибута.
  /// </summary>
  /// <param name="dbFileAttribute">Открытый атрибут "Файл"</param>
  /// <param name="initialFileNames">Список имен файлов, находившихся в атрибуте на момент его открытия. Этот список может содержать пустые строки и null-значения</param>
  protected abstract void DoPerform(IDBAttribute dbFileAttribute, List<string> initialFileNames);
}
