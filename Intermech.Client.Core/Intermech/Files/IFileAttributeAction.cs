
// Type: Intermech.Files.IFileAttributeAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Files;

/// <summary>
/// Позволяет реализовать действие, которое использует открытый файловый атрибут объекта.
/// </summary>
/// <remarks>
/// Этот интерфейс используется для реализации действий по извлечению файлов из объекта,
/// а также действий по обновлению файлов объекта в базе IPS.
/// </remarks>
public interface IFileAttributeAction
{
  /// <summary>
  /// Выполняет действие над значениями указанного файлового атрибута.
  /// </summary>
  /// <param name="dbFileAttribute">Открытый атрибут "Файл"</param>
  /// <param name="initialFileNames">Список имен файлов, находившихся в атрибуте на момент его открытия. Этот список может содержать пустые строки и null-значения</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="dbFileAttribute" /> не может быть null; параметр <paramref name="initialFileNames" /> не может быть null</exception>
  void Perform(IDBAttribute dbFileAttribute, List<string> initialFileNames);
}
