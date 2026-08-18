// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.IdentityArticleLocator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Pdm;
using System;

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Реализует поиск изделий в базе IPS по обозначению, коду ОКП и наименованию.
/// </summary>
public sealed class IdentityArticleLocator : IObjectLocator
{
  private readonly IIdentityArticleLocatorData dataDecoder;

  /// <summary>Создает объект.</summary>
  /// <param name="dataDecoder">Декодер исходных данных, позволяющий прочитать из них обозначение, наименование и код ОКП изделия</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект декодера не может быть null</exception>
  public IdentityArticleLocator(IIdentityArticleLocatorData dataDecoder)
  {
    this.dataDecoder = dataDecoder != null ? dataDecoder : throw new ArgumentNullException();
  }

  /// <summary>Ищет объект изделия в базе IPS.</summary>
  /// <returns>Описатель найденного изделия в базе IPS или null, если изделие не было найдено</returns>
  public ObjectLocatorResult LocateObject()
  {
    string designation = this.dataDecoder.GetDesignation();
    string okpCode = this.dataDecoder.GetOKPCode();
    string name = this.dataDecoder.GetName();
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject articleObject = ServiceUtils.GetService<IArticleService>((object) ServicesManager.ServiceContainer, true).FindArticleObject(designation, okpCode, name, editorRule.OwnerId, (object) sessionKeeper.Session);
      return articleObject != null ? new ObjectLocatorResult(articleObject.ObjectID, articleObject.ObjectType) : (ObjectLocatorResult) null;
    }
  }
}
