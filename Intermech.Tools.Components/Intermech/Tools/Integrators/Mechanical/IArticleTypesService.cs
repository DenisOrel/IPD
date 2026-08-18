// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IArticleTypesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>Сервис для работы с типами изделий.</summary>
public interface IArticleTypesService
{
  /// <summary>
  /// Возвращает имя виртуального атрибута в файле документа, в котором интегратор может хранить имя типа объекта IPS для изделия или материала.
  /// У новых изделий, импортируемых в IPS, этот атрибут может быть заполнен пользователем вручную.
  /// Если такого атрибута в файле нет, то метод может вернуть null или пустую строку.
  /// </summary>
  /// <param name="articleItem">Сущность изделия или материала</param>
  /// <returns>Имя виртуального атрибута в файле документа для хранения имени типа</returns>
  string GetArticleTypeAttributeName(SectionEntity articleItem);

  List<LocalId<int>> GetPossibleArticleTypes(SectionEntity articleItem);

  LocalId<int> DetectArticleType(SectionEntity articleItem);
}
