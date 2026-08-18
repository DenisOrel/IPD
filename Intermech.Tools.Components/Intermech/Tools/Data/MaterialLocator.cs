// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.MaterialLocator
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
/// Реализует поиск материалов в базе IPS по обозначению, коду ОКП и наименованию. Этот класс
/// используется при обработке неосновных материалов в изделиях.
/// </summary>
public sealed class MaterialLocator : IObjectLocator
{
  private IIdentityArticleLocatorData dataDecoder;

  /// <summary>Создает объект.</summary>
  /// <param name="dataDecoder">Декодер исходных данных, позволяющий прочитать из них обозначение, наименование и код ОКП материала</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект декодера не может быть null</exception>
  public MaterialLocator(IIdentityArticleLocatorData dataDecoder)
  {
    this.dataDecoder = dataDecoder != null ? dataDecoder : throw new ArgumentNullException();
  }

  /// <summary>Ищет объект материала в базе IPS.</summary>
  /// <returns>Описатель найденного материала в базе IPS или null, если материал не был найден</returns>
  public ObjectLocatorResult LocateObject()
  {
    string designation = this.dataDecoder.GetDesignation();
    string okpCode = this.dataDecoder.GetOKPCode();
    string name = this.dataDecoder.GetName();
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject material = ServiceUtils.GetService<IArticleService>((object) ServicesManager.ServiceContainer, true).FindMaterial(designation, okpCode, name, IDCache.Default.AllMaterials.Id, editorRule.OwnerId, (object) sessionKeeper.Session);
      return material != null ? new ObjectLocatorResult(material.ObjectID, material.ObjectType) : (ObjectLocatorResult) null;
    }
  }
}
