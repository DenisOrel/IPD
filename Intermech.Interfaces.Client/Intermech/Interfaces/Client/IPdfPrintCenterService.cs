// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPdfPrintCenterService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса подключения к центру печати через remoting
/// </summary>
public interface IPdfPrintCenterService
{
  /// <summary>
  /// Создаёт объект, реализующий интерфейс IPDMDocumentInfo
  /// </summary>
  /// <returns></returns>
  IPDMDocumentInfo CreateDocumentInfo(string objectName, List<string> filePaths);

  /// <summary>
  /// Осуществляет подключение к центру печати pdf с помощью remoting
  /// </summary>
  void LaunchPdfPrintCenter();

  /// <summary>
  /// Добавляет pdf-документы из выбранных объектов в центр печати pdf
  /// </summary>
  void AddFilesToPrintCenter(List<IPDMDocumentInfo> documents);
}
