// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIArticleApiService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public class CIArticleApiService : IArticleCADApiService
{
  private readonly CICaptureChangesDriver driver;
  private readonly CaptureChangesDriverContext driverContext;
  private readonly ICADInterfaceService cadService;
  private readonly ICADSettingsService settingsService;

  public CIArticleApiService(
    CICaptureChangesDriver driver,
    CaptureChangesDriverContext driverContext)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (driverContext == null)
      throw new ArgumentNullException(nameof (driverContext));
    this.driver = driver;
    this.driverContext = driverContext;
    this.cadService = ServiceUtils.GetService<ICADInterfaceService>((object) driver.Integrator, true);
    this.settingsService = ServiceUtils.GetService<ICADSettingsService>((object) driver.Integrator, true);
  }

  protected CICaptureChangesDriver CIDriver
  {
    [DebuggerStepThrough] get => this.driver;
  }

  protected CaptureChangesDriverContext DriverContext
  {
    [DebuggerStepThrough] get => this.driverContext;
  }

  private ICollection<StringKey> GetArticleFileAttributes(SectionEntity articleItem)
  {
    return this.settingsService.SynchronizedArticleAttributes.GetAttributes(ObjectSection.TryGetObjectType(articleItem), false);
  }

  protected virtual IAttributeCodec GetArticleAttributeCodec(SectionEntity articleItem)
  {
    return this.cadService.GetArticleCodec(articleItem.Sections.Get<CIArticleData>().Configuration.Document);
  }

  protected virtual IValueBagContainer GetArticleAttributeContainer(SectionEntity articleItem)
  {
    return this.cadService.GetArticleAttributeContainer(articleItem.Sections.Get<CIArticleData>().Configuration);
  }

  public virtual ContainerValues ReadArticleProperties(SectionEntity articleItem)
  {
    return this.GetArticleAttributeCodec(articleItem).ReadFileProperties(this.GetArticleAttributeContainer(articleItem), this.GetArticleFileAttributes(articleItem));
  }

  public virtual bool WriteArticleProperties(
    SectionEntity articleItem,
    ContainerValues fileProperties)
  {
    return this.GetArticleAttributeCodec(articleItem).Formatter.Write(this.GetArticleAttributeContainer(articleItem), fileProperties);
  }

  public virtual ValueBag DecodeArticleAttributes(
    SectionEntity articleItem,
    ContainerValues fileProperties)
  {
    DecodeAttributesOptions decodeOptions = this.CIDriver.MechanicalOperations.Articles.GetDecodeOptions(articleItem);
    DecodeAttributesParams decodeParams = new DecodeAttributesParams(this.GetArticleAttributeContainer(articleItem), this.GetArticleFileAttributes(articleItem), fileProperties, decodeOptions);
    return this.GetArticleAttributeCodec(articleItem).Decode(decodeParams);
  }

  public virtual void EncodeArticleAttributes(
    SectionEntity articleItem,
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
    EncodeAttributesOptions encodeOptions = this.CIDriver.MechanicalOperations.Articles.GetEncodeOptions(articleItem);
    this.GetArticleAttributeCodec(articleItem).Encode(new EncodeAttributesParams(this.GetArticleAttributeContainer(articleItem), attributeKeys, attributes, fileProperties, encodeOptions)
    {
      ContainerDisplayName = DisplaySection.GetQualifiedName(articleItem)
    });
  }

  /// <summary>
  /// Возвращает список имен атрибутов, значения которых необходимо перенести из конфигурации документа в объект изделия IPS.
  /// В данный список можно не включать ряд атрибутов, копируемых всегда - обозначение, код ОКП, наименование и др.
  /// Если список атрибутов содержит атрибуты, которые не могут существовать у изделия данного типа, то такие атрибуты будут проигнорированы.
  /// </summary>
  /// <param name="articleItem">Сущность конфигурации документа</param>
  /// <returns>Список имен атрибутов</returns>
  public virtual ICollection<StringKey> GetArticleSyncAttributes(SectionEntity articleItem)
  {
    return this.GetArticleFileAttributes(articleItem);
  }
}
