// Decompiled with JetBrains decompiler
// Type: Intermech.Files.ImportFileHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Делегат метода, используемого для импорта файла в базу IPS.
/// </summary>
/// <param name="fullPath">Абсолютный путь к импортируемому файлу</param>
/// <param name="importOptions">Опции импорта файла</param>
/// <returns>Результат импорта файла</returns>
public delegate FileImportResult ImportFileHandler(string fullPath, FileImportOptions importOptions);
