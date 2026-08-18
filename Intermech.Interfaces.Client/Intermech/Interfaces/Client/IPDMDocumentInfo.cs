// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPDMDocumentInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс для изоляции класса PDMDocumentInfo, необходимого для передачи данных центру печати
/// </summary>
public interface IPDMDocumentInfo
{
  /// <summary>Возвращает заголовок документа в PDM-системе.</summary>
  string ObjectName { get; }

  /// <summary>Возвращает путь к файлу документа на локальном диске.</summary>
  List<string> FilePaths { get; }
}
