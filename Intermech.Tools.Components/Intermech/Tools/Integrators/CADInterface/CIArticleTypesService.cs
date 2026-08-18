// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIArticleTypesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Создает объект.</summary>
/// <param name="driver">Драйвер захвата изменений</param>
/// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
/// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
internal sealed class CIArticleTypesService(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext) : ArticleTypesService(driver, driverContext)
{
  /// <summary>
  /// Возвращает имя виртуального атрибута в файле документа, в котором интегратор может хранить имя типа объекта IPS для изделия или материала.
  /// У новых изделий, импортируемых в IPS, этот атрибут может быть заполнен пользователем вручную.
  /// Если такого атрибута в файле нет, то метод может вернуть null или пустую строку.
  /// </summary>
  /// <param name="articleItem">Сущность изделия или материала</param>
  /// <returns>Имя виртуального атрибута в файле документа для хранения имени типа</returns>
  protected override string DoGetArticleTypeAttributeName(SectionEntity articleItem)
  {
    return CADDocumentResources.EMB_ArticleTypeAttribute;
  }
}
