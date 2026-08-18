// Decompiled with JetBrains decompiler
// Type: Intermech.Files.ImportFileCapabilities
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Files;

/// <summary>Флаги особенностей работы метода импорта файла</summary>
[Flags]
public enum ImportFileCapabilities
{
  /// <summary>Метод импорта файла не имеет особенностей работы</summary>
  None = 0,
  /// <summary>
  /// Метод импорта файла использует отложенный импорт ссылочных зависимостей
  /// </summary>
  DeferredImport = 1,
}
