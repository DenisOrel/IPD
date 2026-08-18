// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IFileAreas
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Позволяет перечислять файловые области в файловом хранилище пользователя, а также определять принадлежность файлов к различным файловым
/// областям.
/// </summary>
public interface IFileAreas : IEnumerable<IFileArea>, IEnumerable
{
  /// <summary>
  /// Позволяет определить область файлового хранилища, в которой находится указанный файл.
  /// </summary>
  /// <param name="fullPath">Абсолютный путь к файлу</param>
  /// <returns>Объект файловой области. Может быть null, если файл находится вне файлового хранилища</returns>
  /// <exception cref="T:System.ArgumentException">Не задан путь к файлу</exception>
  /// <exception cref="T:System.InvalidOperationException">Путь к файлу указан не в абсолютной форме</exception>
  IFileArea FindArea(string fullPath);

  /// <summary>Возвращает объект области для временных файлов.</summary>
  ITempArea TempArea { get; }

  /// <summary>Возвращает объект области для кэшируемых файлов.</summary>
  IFileArea CacheArea { get; }

  /// <summary>
  /// Возвращает объект рабочей области файлового хранилища.
  /// </summary>
  IWorkArea WorkArea { get; }

  /// <summary>
  /// Возвращает объект области просмотра файлового хранилища.
  /// </summary>
  IViewArea ViewArea { get; }
}
