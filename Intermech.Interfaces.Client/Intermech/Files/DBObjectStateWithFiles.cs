// Decompiled with JetBrains decompiler
// Type: Intermech.Files.DBObjectStateWithFiles
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Описывает состояние объекта IPS и состояния его файлов.
/// </summary>
public class DBObjectStateWithFiles : DBEntityWithFiles<DBObjectState, FileState>
{
  /// <summary>Создает объект.</summary>
  /// <param name="dbObjectState">Состояние объекта IPS</param>
  /// <param name="files">Список состояний файлов объекта IPS</param>
  /// <exception cref="T:ArgumentNullException">dbObjectState, files</exception>
  public DBObjectStateWithFiles(DBObjectState dbObjectState, List<FileState> files)
    : base(dbObjectState, files)
  {
    if (dbObjectState == null)
      throw new ArgumentNullException(nameof (dbObjectState));
  }
}
