// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientEventLog
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Класс поддержки с клиентской стороны записи в журнал событий
/// </summary>
public class ClientEventLog
{
  /// <summary>Записать в лог событие для объекта или связи</summary>
  /// <param name="id">id версии объекта или связи</param>
  /// <param name="attributableElement">объект или связь</param>
  /// <param name="element">тип объекта или связи</param>
  /// <param name="objectName">для объектов "имя_объекта_IDBObjectType.ObjectInstanceName 'caption'"; для связи ?</param>
  /// <param name="note">комментарий; Environment.NewLine для перевода строк</param>
  /// <param name="actionType">тип действия</param>
  /// <param name="recordType">тип записи</param>
  public static void AddEvent4Attributable(
    long id,
    AttributableElements attributableElement,
    int elementType,
    string objectName,
    string note,
    ActionType actionType,
    EventlogRecordType recordType)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.EventLog?.AddEvent(attributableElement == AttributableElements.Object ? id : 0L, attributableElement == AttributableElements.Relation ? id : 0L, attributableElement == AttributableElements.Object ? 1 : 5, id, objectName, note, actionType, recordType);
  }

  /// <summary>
  /// Записать в лог событие для объекта или связи с самостоятельным уточнением недостающей информации
  /// </summary>
  /// <param name="id">id версии объекта или связи</param>
  /// <param name="attributableElement">объект или связь</param>
  /// <param name="note">комментарий; Environment.NewLine для перевода строк</param>
  /// <param name="actionType">тип действия</param>
  /// <param name="recordType">тип записи</param>
  public static void AddEvent4Attributable(
    long id,
    AttributableElements attributableElement,
    string note,
    ActionType actionType,
    EventlogRecordType recordType)
  {
    int num = 0;
    string objectName = string.Empty;
    if (attributableElement == AttributableElements.Object)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(id);
      if (!objectInfo.Empty)
      {
        str2 = objectInfo.Caption;
        num = objectInfo.ObjectTypeID;
      }
      IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(num);
      if (objectType != null)
        str1 = objectType.ObjectInstanceName;
      objectName = $"{str1} '{str2}'";
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(id, false);
        if (relation != null)
        {
          num = relation.RelationType;
          IDBRelationTypeInfo relationType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetRelationType(num);
          if (relationType != null)
            objectName = $"Связь типа '{relationType.Description}'";
        }
      }
    }
    ClientEventLog.AddEvent4Attributable(id, attributableElement, num, objectName, note, actionType, recordType);
  }
}
