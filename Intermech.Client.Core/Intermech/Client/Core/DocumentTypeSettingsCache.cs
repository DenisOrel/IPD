
// Type: Intermech.Client.Core.DocumentTypeSettingsCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Memoization;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core;

public static class DocumentTypeSettingsCache
{
  private static readonly object dtsSyncRoot = new object();
  private static IDictionary<int, DocumentTypeSettings> dtsCache;
  private static IStateMonitor dtsMonitor;
  private static object dtsSeqNumber;

  /// <summary>
  /// Возвращает настройки для указанного типа документов. Результаты обращения к серверу приложений кэшируются для ускорения последующих вызовов этого метода.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документов</param>
  /// <returns>Настройки для указанного типа документов</returns>
  public static DocumentTypeSettings GetSettings(LocalId<int> documentType)
  {
    return DocumentTypeSettingsCache.GetSettings(documentType.Id);
  }

  /// <summary>
  /// Возвращает настройки для указанного типа документов. Результаты обращения к серверу приложений кэшируются для ускорения последующих вызовов этого метода.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документов</param>
  /// <returns>Настройки для указанного типа документов</returns>
  public static DocumentTypeSettings GetSettings(int documentType)
  {
    if (documentType == -1)
      throw new ArgumentException();
    lock (DocumentTypeSettingsCache.dtsSyncRoot)
    {
      if (DocumentTypeSettingsCache.dtsCache == null)
      {
        DocumentTypeSettingsCache.dtsCache = (IDictionary<int, DocumentTypeSettings>) new Dictionary<int, DocumentTypeSettings>();
        DocumentTypeSettingsCache.dtsMonitor = (IStateMonitor) new MetadataNotificationMonitor("DocumentTypeSettingsChanged");
        DocumentTypeSettingsCache.dtsSeqNumber = DocumentTypeSettingsCache.dtsMonitor.WriterSeqNum;
      }
      else if (DocumentTypeSettingsCache.dtsMonitor.AnyWritersSince(DocumentTypeSettingsCache.dtsSeqNumber))
      {
        DocumentTypeSettingsCache.dtsCache.Clear();
        DocumentTypeSettingsCache.dtsSeqNumber = DocumentTypeSettingsCache.dtsMonitor.WriterSeqNum;
      }
      DocumentTypeSettings settings;
      if (!DocumentTypeSettingsCache.dtsCache.TryGetValue(documentType, out settings))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          settings = ServiceUtils.GetService<IDocumentTypeSettingsService>((object) sessionKeeper.Session, true).GetSettings(sessionKeeper.Session.SessionGUID, documentType);
        DocumentTypeSettingsCache.dtsCache.Add(documentType, settings);
      }
      return settings;
    }
  }
}
