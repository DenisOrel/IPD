
// Type: Intermech.Interfaces.Data.Metadata.MetadataNotificationMonitor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Memoization;
using System;


namespace Intermech.Interfaces.Data.Metadata;

/// <summary>
/// Предоставляет возможность для слежения за изменением метаданных с помощью INotificationService.
/// </summary>
public class MetadataNotificationMonitor : IStateMonitor
{
  private volatile int writerSeqNum;

  /// <summary>Создает объект.</summary>
  public MetadataNotificationMonitor(string eventName)
  {
    if (eventName == null)
      throw new ArgumentNullException(nameof (eventName));
    ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, true).Subscribe(eventName, new NotificationEventHandler(this.OnMetadataChanged));
  }

  private void OnMetadataChanged(object sender, NotificationEventArgs e) => ++this.writerSeqNum;

  /// <summary>
  /// Возвращает текущее значение счетчика изменений метаданных.
  /// </summary>
  public object WriterSeqNum => (object) this.writerSeqNum;

  /// <summary>
  /// Проверяет, были ли изменены метаданные с указанного момента времени.
  /// </summary>
  /// <param name="seqNum">Значение счетчика изменений.</param>
  /// <returns>true, если метаданные были изменены с того момента, когда было получено указанное
  /// значение счетчика. В другом случае метод вернет null.</returns>
  public bool AnyWritersSince(object seqNum) => seqNum == null || (int) seqNum < this.writerSeqNum;
}
