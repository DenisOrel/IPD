// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.IClientRxmlService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Redline;

/// <summary>
/// Интерфейс сервиса для операций, специфических для редактора замечаний "ИНТЕРМЕХ" (rxml).
/// </summary>
public interface IClientRxmlService
{
  /// <summary>Открывает в файл замечаний в редакторе.</summary>
  /// <param name="documentId">Идентификатор версии документа с файлом замечаний</param>
  /// <returns>Признак успешного/неуспешного выполнения</returns>
  bool TryOpenRxmlViewer(long documentId);
}
