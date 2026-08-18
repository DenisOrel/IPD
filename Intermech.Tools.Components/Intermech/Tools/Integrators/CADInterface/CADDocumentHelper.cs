// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADDocumentHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Tools.Components.Properties;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public static class CADDocumentHelper
{
  public static ContainerValues ReadAttributes(
    IServiceProvider integrator,
    CADDocumentProxy document,
    ICollection<StringKey> attributeKeys,
    DecodeAttributesOptions options)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (options == null)
      options = DecodeAttributesOptions.Empty;
    if (attributeKeys == null)
      attributeKeys = ServiceUtils.GetService<ICADSettingsService>((object) integrator, true).SynchronizedDocumentAttributes.GetAttributes();
    return ServiceUtils.GetService<ICADInterfaceService>((object) integrator, true).OpenDocuments.GetCodec((IOpenDocument) CADInterfaceAdapters.AsOpenDocument(document)).ReadAttributes((IValueBagContainer) CADInterfaceAdapters.AsValueBagContainer(document), attributeKeys, options);
  }

  public static int TryReadGlobalPDMFlag(IServiceProvider integrator, CADDocumentProxy document)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    ModelConfigurationProxy configuration = document != null ? document.DefaultConfiguration : throw new ArgumentNullException(nameof (document));
    return configuration == null ? 0 : CADDocumentHelper.ReadPDMFlag(integrator, configuration);
  }

  /// <summary>
  /// Позволяет определить, что указанный документ не должен быть импортирован в IPS как самостоятельный документ.
  /// Делается это с помощью атрибута PDMFlag.
  /// </summary>
  /// <param name="integrator">Интегратор</param>
  /// <param name="document">Объект документа</param>
  /// <returns>true, если документ нелья импортировать</returns>
  public static bool IsDocumentImportDenied(IServiceProvider integrator, CADDocumentProxy document)
  {
    return CADDocumentHelper.IsDocumentImportDenied(CADDocumentHelper.TryReadGlobalPDMFlag(integrator, document));
  }

  /// <summary>
  /// Позволяет определить, что указанный документ не должен быть импортирован в IPS как самостоятельный документ.
  /// Делается это с помощью атрибута PDMFlag.
  /// </summary>
  /// <param name="pdmFlag">Значение атрибута</param>
  /// <returns>true, если документ нелья импортировать</returns>
  public static bool IsDocumentImportDenied(int pdmFlag)
  {
    return pdmFlag == 1 || pdmFlag == 3 || pdmFlag == 4 || pdmFlag == 5;
  }

  public static ContainerValues ReadAttributes(
    IServiceProvider integrator,
    ModelConfigurationProxy configuration,
    ICollection<StringKey> attributeKeys,
    DecodeAttributesOptions options)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (configuration == null)
      throw new ArgumentNullException(nameof (configuration));
    if (options == null)
      options = DecodeAttributesOptions.Empty;
    if (attributeKeys == null)
      attributeKeys = ServiceUtils.GetService<ICADSettingsService>((object) integrator, true).SynchronizedArticleAttributes.GetAttributes();
    ICADInterfaceService service = ServiceUtils.GetService<ICADInterfaceService>((object) integrator, true);
    return service.GetArticleCodec(configuration.Document).ReadAttributes(service.GetArticleAttributeContainer(configuration), attributeKeys, options);
  }

  /// <summary>
  /// Позволяет определить, что указанная конфигурация 3D-модели исключена из процесса образования изделий.
  /// Делается это с помощью атрибута PDMFlag.
  /// </summary>
  /// <param name="pdmFlag">Значение атрибута</param>
  /// <returns>true, если эта конфигурация исключена</returns>
  public static bool IsArticleCreationDenied(int pdmFlag)
  {
    return pdmFlag == 2 || pdmFlag == 3 || pdmFlag == 6 || pdmFlag == 4;
  }

  /// <summary>
  /// Позволяет определить, что указанная конфигурация 3D-модели исключена из процесса образования изделий.
  /// Делается это с помощью атрибута PDMFlag.
  /// </summary>
  /// <param name="integrator">Интегратор</param>
  /// <param name="configuration">Конфигурация документа</param>
  /// <returns>true, если эта конфигурация исключена</returns>
  public static bool IsArticleCreationDenied(
    IServiceProvider integrator,
    ModelConfigurationProxy configuration)
  {
    return CADDocumentHelper.IsArticleCreationDenied(CADDocumentHelper.ReadPDMFlag(integrator, configuration));
  }

  public static int ReadPDMFlag(IServiceProvider integrator, ModelConfigurationProxy configuration)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    return configuration != null ? configuration.EvaluateCached<int>("PDMFlag", (Func<int>) (() => CADDocumentHelper.ReadPDMFlagCore(integrator, configuration))) : throw new ArgumentNullException(nameof (configuration));
  }

  private static int ReadPDMFlagCore(
    IServiceProvider integrator,
    ModelConfigurationProxy configuration)
  {
    StringKey[] attributeKeys = new StringKey[3]
    {
      (StringKey) CADDocumentResources.EMB_PDMFlagAttribute,
      (StringKey) CADDocumentResources.EMB_IgnoreConfiguration,
      (StringKey) CADDocumentResources.EMB_IgnoreConfigurationOld
    };
    ContainerValues containerValues = CADDocumentHelper.ReadAttributes(integrator, configuration, (ICollection<StringKey>) attributeKeys, (DecodeAttributesOptions) null);
    int num = containerValues.Bag.Read<int>(attributeKeys[0], 0);
    if (num != 0)
      return num;
    return containerValues.Bag.Read<bool>(attributeKeys[1], false) || containerValues.Bag.Read<bool>(attributeKeys[2], false) ? 6 : 0;
  }

  public static string TryGetReplacementName(
    IServiceProvider integrator,
    ModelConfigurationProxy configuration)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (configuration == null)
      throw new ArgumentNullException(nameof (configuration));
    StringKey[] attributeKeys = new StringKey[1]
    {
      (StringKey) CADDocumentResources.EMB_ReplaceWithAttribute
    };
    return CADDocumentHelper.ReadAttributes(integrator, configuration, (ICollection<StringKey>) attributeKeys, DecodeAttributesOptions.Empty).Bag.Read<string>(attributeKeys[0], (string) null);
  }
}
