// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.ImbaseKeyArticleLocator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Imbase;
using System;

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>Реализует поиск изделий в базе IPS по ключу Imbase.</summary>
public sealed class ImbaseKeyArticleLocator : IObjectLocator
{
  private readonly IImbaseKeyLocatorData dataDecoder;

  /// <summary>Создает объект.</summary>
  /// <param name="dataDecoder">Декодер исходных данных, позволяющий прочитать из них ключ Imbase</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект декодера не может быть null</exception>
  public ImbaseKeyArticleLocator(IImbaseKeyLocatorData dataDecoder)
  {
    this.dataDecoder = dataDecoder != null ? dataDecoder : throw new ArgumentNullException();
  }

  /// <summary>Ищет объект изделия в базе IPS.</summary>
  /// <returns>Описатель найденного изделия в базе IPS или null, если изделие не было найдено</returns>
  public ObjectLocatorResult LocateObject()
  {
    string imbaseKey = this.dataDecoder.GetImbaseKey();
    if (!string.IsNullOrEmpty(imbaseKey))
    {
      long articleId = this.LocateArticleByImbaseKey(imbaseKey);
      switch (articleId)
      {
        case -1:
        case 0:
          break;
        default:
          long appropriateVersion = this.GetAppropriateVersion(articleId, VersionsRuleSources.GetEditorRule().OwnerId);
          return new ObjectLocatorResult(appropriateVersion, DBHelper.GetObjectType(appropriateVersion));
      }
    }
    return (ObjectLocatorResult) null;
  }

  private long LocateArticleByImbaseKey(string imbaseKey)
  {
    IImbaseSelector service = ServiceUtils.GetService<IImbaseSelector>((object) ServicesManager.ServiceContainer, true);
    try
    {
      return service.GetObjectIdByImbaseKey(imbaseKey, false);
    }
    catch (ArgumentException ex)
    {
      return 0;
    }
    catch (KernelExceptionID ex)
    {
      if (ex.ErrorID == 158)
        return 0;
      throw;
    }
    catch (ObjectNotFoundException ex)
    {
      return 0;
    }
    catch (KernelException ex)
    {
      if (ex.Source == "Intermech.Imbase.Server")
        return 0;
      throw;
    }
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
