// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.ImbaseAttributeArticleLocator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Helpers;
using System;

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Реализует поиск изделий в базе IPS по атрибуту записи Imbase.
/// </summary>
public sealed class ImbaseAttributeArticleLocator : IObjectLocator
{
  private readonly IImbaseAttributeLocatorData dataDecoder;

  /// <summary>Создает объект.</summary>
  /// <param name="dataDecoder">Декодер исходных данных, позволяющий прочитать из них атрибут Imbase</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект декодера не может быть null</exception>
  public ImbaseAttributeArticleLocator(IImbaseAttributeLocatorData dataDecoder)
  {
    this.dataDecoder = dataDecoder != null ? dataDecoder : throw new ArgumentNullException();
  }

  /// <summary>Ищет объект изделия в базе IPS.</summary>
  /// <returns>Описатель найденного изделия в базе IPS или null, если изделие не было найдено</returns>
  public ObjectLocatorResult LocateObject()
  {
    int objectTypeId = this.dataDecoder.ObjectTypeId;
    int imbaseAttributeId = this.dataDecoder.ImbaseAttributeId;
    string imbaseAttributeValue = this.dataDecoder.ImbaseAttributeValue;
    if (objectTypeId != -1 && imbaseAttributeId != 0 && imbaseAttributeValue != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IImbaseServer service1 = ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, false);
        IImbaseIndexingService service2 = ServiceUtils.GetService<IImbaseIndexingService>((object) sessionKeeper.Session, false);
        if (service1 != null)
        {
          if (service2 != null)
          {
            Tuple<long, long, long> record = new ImbaseSearchTool(sessionKeeper.Session, service1, service2).FindRecord(objectTypeId, true, imbaseAttributeId, imbaseAttributeValue);
            if (record != null)
            {
              long appropriateVersion = this.GetAppropriateVersion(service1.CreateObject(sessionKeeper.Session.SessionGUID, record.Item1, record.Item2, record.Item3, true, -1), VersionsRuleSources.GetEditorRule().OwnerId);
              return new ObjectLocatorResult(appropriateVersion, DBHelper.GetObjectType(appropriateVersion));
            }
          }
        }
      }
    }
    return (ObjectLocatorResult) null;
  }

  /// <summary>
  /// Определяет версию изделия, соответствующую нужному правилу подбора для редактирования.
  /// </summary>
  /// <param name="articleId">Идентификатор версии изделия, найденный сервисом Imbase</param>
  /// <param name="ruleOwnerId">Ключ владельца правила подбора</param>
  /// <returns>Подходящая версия изделия</returns>
  private long GetAppropriateVersion(long articleId, string ruleOwnerId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(articleId, true);
      return sessionKeeper.Session.GetObjectByVersionsRule(dbObject.GUID, ruleOwnerId, true).ObjectID;
    }
  }
}
