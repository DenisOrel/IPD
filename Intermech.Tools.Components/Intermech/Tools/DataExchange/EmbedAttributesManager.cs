// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.EmbedAttributesManager
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.Data;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.DataExchange;

public sealed class EmbedAttributesManager
{
  private static readonly ICollection<string> noAncillaryFiles = (ICollection<string>) new string[0];
  private IEmbedAttributesDriver driver;

  /// <summary>
  /// Возвращает или задает стратегию для работы с атрибутами документа.
  /// Значение свойства должно быть задано до начала использования менеджера.
  /// </summary>
  public IEmbedAttributesDriver Driver
  {
    [DebuggerStepThrough] get => this.driver;
    [DebuggerStepThrough] set => this.driver = value;
  }

  /// <summary>
  /// Записывает в файловую копию объекта указанные значения атрибутов объекта.
  /// </summary>
  /// <param name="parameters">Параметры операции</param>
  /// <exception cref="T:ArgumentNullException">Параметр метода <paramref name="parameters" /> не должен быть равен null</exception>
  public void EmbedAttributes(EmbedAttributesActionParameters parameters)
  {
    if (parameters == null)
      throw new ArgumentNullException(nameof (parameters));
    parameters.ValidateProperties();
    this.ValidateProperties();
    this.PrepareEmbedAttributesParameters(parameters);
    int objectType = DBHelper.GetObjectType(parameters.ObjectId);
    try
    {
      this.Driver.BeginAction(parameters.ObjectId, objectType);
      ICollection<StringKey> embeddableAttributes = this.Driver.GetEmbeddableAttributes(parameters.ObjectId, objectType);
      if (embeddableAttributes == null || embeddableAttributes.Count == 0)
        return;
      ValueBag embeddableTable = this.CreateEmbeddableTable(objectType, (ICollection<AttributeValues>) parameters.AttributeValues, embeddableAttributes);
      if (embeddableTable.Count == 0)
        return;
      string masterFile = this.Driver.FindMasterFile(parameters.ObjectId);
      if (string.IsNullOrEmpty(masterFile))
        return;
      ICollection<string> strings = EmbedAttributesManager.noAncillaryFiles;
      if (this.Driver.HasAncillaryDocumentFiles(parameters.ObjectId))
        strings = this.Driver.GetAncillaryDocumentFiles(parameters.ObjectId);
      parameters.ProgressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_416"));
      IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
      string str = service.PublishTree(parameters.ObjectId, masterFile, VersionsRuleSources.GetEditorRule(), (IFileArea) service.WorkArea);
      parameters.ProgressSink.SetProgress(10.0);
      using (UIReport.CreateScope())
      {
        UIReportBuilder uiReportBuilder = new UIReportBuilder();
        uiReportBuilder.ReportStart($"Запись измененных атрибутов в файл объекта '{DBHelper.GetObjectCaption(parameters.ObjectId)}'");
        try
        {
          List<string> stringList = new List<string>(1 + strings.Count);
          stringList.Add(str);
          foreach (string path2 in (IEnumerable<string>) strings)
            stringList.Add(Path.Combine(service.WorkArea.AreaPath, path2));
          parameters.ProgressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_439"));
          IProgressUpdater progressUpdater = ProgressSinks.CreateProgressUpdater(parameters.ProgressSink.CreateNestedSink(90.0), stringList.Count);
          int num = 0;
          foreach (string documentFilePath in stringList)
          {
            if (this.Driver.EmbedAttributes(parameters.ObjectId, objectType, documentFilePath, embeddableTable))
              ++num;
            progressUpdater.AddCompletedTasks(1);
          }
          if (num != 0)
            this.Driver.FlushChanges();
          uiReportBuilder.ReportSuccess();
        }
        catch (Exception ex)
        {
          uiReportBuilder.ReportFail(ex);
          throw;
        }
      }
      parameters.ProgressSink.SetState(string.Empty);
      parameters.ProgressSink.SetProgress(100.0);
    }
    finally
    {
      this.Driver.EndAction();
    }
  }

  private void PrepareEmbedAttributesParameters(EmbedAttributesActionParameters parameters)
  {
    if (parameters.ProgressSink != null)
      return;
    parameters.ProgressSink = ProgressSinks.NullPercentageSink;
  }

  private ValueBag CreateEmbeddableTable(
    int objectType,
    ICollection<AttributeValues> allValues,
    ICollection<StringKey> embeddableAttrs)
  {
    ValueBag embeddableTable = new ValueBag(embeddableAttrs.Count);
    IDBAttributableTypeRef attrTypeRef = (IDBAttributableTypeRef) new DirectObjectAttributesRef(objectType);
    foreach (StringKey embeddableAttr1 in (IEnumerable<StringKey>) embeddableAttrs)
    {
      StringKey embeddableAttr = embeddableAttr1;
      AttributeValues rawValue = CollectionUtils.Find<AttributeValues>((IEnumerable<AttributeValues>) allValues, (Predicate<AttributeValues>) (value => embeddableAttr == (StringKey) value.AttributeName));
      if (rawValue != null)
      {
        ValueRecord valueRecord = DBAttributeHelper.TryReadEntityValue(attrTypeRef, rawValue);
        if (valueRecord != null)
        {
          valueRecord.Flags.Set(NamedFlags.ThrowSetException);
          embeddableTable.Add(valueRecord);
        }
      }
    }
    embeddableTable.AcceptChanges();
    return embeddableTable;
  }

  private void ValidateProperties()
  {
    if (this.Driver == null)
      throw new InvalidOperationException("Property 'Driver' must not be null.");
  }
}
