
// Type: Intermech.Navigator.EventLog.RecordEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System.Collections;


namespace Intermech.Navigator.EventLog;

/// <summary>
/// Предоставляет данные для событий службы обновления, связанных c удалением и очисткой
/// записей журнала.
/// </summary>
public class RecordEventArgs : NotificationEventArgs, IDataMergingSupport
{
  private IList _eventIDs;

  public RecordEventArgs(string eventName, IList eventIDs)
    : base(eventName)
  {
    this._eventIDs = eventIDs;
  }

  public IList EventIDs => this._eventIDs;

  /// <summary>
  /// Объединяет данные этого объекта с данными указанного объекта. После успешного объединения другой
  /// объект будет больше не нужен.
  /// </summary>
  /// <param name="obj">Объект, чьи данные должны быть объединены с данными этого объекта</param>
  /// <returns>true, если объединение было успешным, в противном случае - false</returns>
  public bool MergeWith(object obj)
  {
    if (!(obj is RecordEventArgs recordEventArgs))
      return false;
    for (int index = 0; index < recordEventArgs._eventIDs.Count; ++index)
    {
      object eventId = recordEventArgs._eventIDs[index];
      if (!this._eventIDs.Contains(eventId))
        this._eventIDs.Add(eventId);
    }
    return true;
  }
}
