// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleTypesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>Создает объект.</summary>
/// <param name="driver">Драйвер захвата изменений</param>
/// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
/// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
public class ArticleTypesService(MechanicalDriver driver, CaptureChangesDriverContext driverContext) : 
  MechanicalDriverService(driver, driverContext),
  IArticleTypesService
{
  /// <summary>
  /// Возвращает имя виртуального атрибута в файле документа, в котором интегратор может хранить имя типа объекта IPS для изделия или материала.
  /// У новых изделий, импортируемых в IPS, этот атрибут может быть заполнен пользователем вручную.
  /// Если такого атрибута в файле нет, то метод может вернуть null или пустую строку.
  /// </summary>
  /// <param name="articleItem">Сущность изделия или материала</param>
  /// <returns>Имя виртуального атрибута в файле документа для хранения имени типа</returns>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public string GetArticleTypeAttributeName(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoGetArticleTypeAttributeName(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>
  /// Возвращает имя виртуального атрибута в файле документа, в котором интегратор может хранить имя типа объекта IPS для изделия или материала.
  /// У новых изделий, импортируемых в IPS, этот атрибут может быть заполнен пользователем вручную.
  /// Если такого атрибута в файле нет, то метод может вернуть null или пустую строку.
  /// </summary>
  /// <param name="articleItem">Сущность изделия или материала</param>
  /// <returns>Имя виртуального атрибута в файле документа для хранения имени типа</returns>
  protected virtual string DoGetArticleTypeAttributeName(SectionEntity articleItem) => string.Empty;

  public List<LocalId<int>> GetPossibleArticleTypes(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoGetPossibleArticleTypes(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  protected virtual List<LocalId<int>> DoGetPossibleArticleTypes(SectionEntity articleItem)
  {
    return this.Driver.MechanicalOperations.Articles.GetPossibleArticleTypes(articleItem);
  }

  public LocalId<int> DetectArticleType(SectionEntity articleItem)
  {
    return articleItem != null ? this.DoDetectArticleType(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  protected virtual LocalId<int> DoDetectArticleType(SectionEntity articleItem)
  {
    List<LocalId<int>> possibleArticleTypes = this.GetPossibleArticleTypes(articleItem);
    if (possibleArticleTypes.Count == 0)
      throw new FaultException(LocalizationHolder.rm.GetString("SR_565"));
    string typeAttributeName = this.GetArticleTypeAttributeName(articleItem);
    if (!string.IsNullOrEmpty(typeAttributeName))
    {
      int artType = DbOperations.ReadObjectTypeAttribute(articleItem, typeAttributeName);
      LocalId<int> localId = possibleArticleTypes.Find((Predicate<LocalId<int>>) (item => item.Id == artType));
      if (localId != null)
        return localId;
    }
    return possibleArticleTypes[0];
  }
}
