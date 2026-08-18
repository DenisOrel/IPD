// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADModelDesignationHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Tools.Components.Properties;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Вспомогательный класс, обслуживающий режимы чтения/записи обозначений в моделях CAD-систем.
/// Поддерживаются два режима: режим общего обозначения с основным исполнением изделия и
/// режим независимого обозначения у модели.
/// </summary>
public class CADModelDesignationHelper
{
  /// <summary>
  /// Читает из файла документа и возвращает данные, необходимые для определения режим работы
  /// и сопутствующих параметров.
  /// </summary>
  /// <param name="document">Документ CAD-системы</param>
  /// <returns>Контейнер с необходимыми пользовательскими параметрами документа</returns>
  public ValueBag GetDetectionData(CADDocumentProxy document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    ValueBag detectionData = new ValueBag(2);
    ValueRecord parameter1 = document.TryGetParameter(CADDocumentResources.EMB_BasicArticleInstanceAttribute);
    if (parameter1 != null)
      detectionData.Add(parameter1);
    ValueRecord parameter2 = document.TryGetParameter(CADDocumentResources.EMB_IndependentDesignation);
    if (parameter2 != null)
      detectionData.Add(parameter2);
    detectionData.AcceptChanges();
    return detectionData;
  }

  /// <summary>
  /// Возвращает конфигурацию документа, которая соответствует основному исполнению изделия.
  /// Метод используется только в случае режима общего обозначения у документа и у основного исполнения изделия.
  /// </summary>
  /// <param name="document">Документ CAD-системы</param>
  /// <param name="documentProperties">Контейнер с параметрами документа, прочитанными из файла документа</param>
  /// <returns>Конфигурация документа, которая соответствует основному исполнению изделия</returns>
  public ModelConfigurationProxy GetBasicArticleInstance(
    CADDocumentProxy document,
    ValueBag documentProperties)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (documentProperties == null)
      throw new ArgumentNullException(nameof (documentProperties));
    string instanceAttribute = CADDocumentResources.EMB_BasicArticleInstanceAttribute;
    ValueBag target = new ValueBag();
    new DataTypeFilterAction((TransferValueRecordAction) new CopySourceValueAction(documentProperties, target, (StringKey) instanceAttribute), typeof (string), true).Perform();
    string name = target.Read<string>((StringKey) instanceAttribute, (string) null);
    if (!string.IsNullOrEmpty(name))
    {
      try
      {
        return document.GetConfiguration(name, false);
      }
      catch (FaultException ex)
      {
      }
    }
    return document.DefaultConfiguration;
  }

  /// <summary>
  /// Возвращает признак, что у документа включен режим независимого обозначения.
  /// Иначе, у документа действует режим совместного обозначения с основным исполнением изделия.
  /// </summary>
  /// <param name="document">Документ CAD-системы</param>
  /// <param name="documentProperties">Значения свойств документа</param>
  /// <returns>true - включен режим независимого обозначения документа</returns>
  public bool IsIndependentDesignationMode(CADDocumentProxy document, ValueBag documentProperties)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (documentProperties == null)
      throw new ArgumentNullException(nameof (documentProperties));
    string independentDesignation = CADDocumentResources.EMB_IndependentDesignation;
    ValueBag target = new ValueBag();
    new DataTypeFilterAction((TransferValueRecordAction) new CopySourceValueAction(documentProperties, target, (StringKey) independentDesignation), typeof (string), true).Perform();
    return !string.IsNullOrEmpty(target.Read<string>((StringKey) independentDesignation, (string) null));
  }
}
