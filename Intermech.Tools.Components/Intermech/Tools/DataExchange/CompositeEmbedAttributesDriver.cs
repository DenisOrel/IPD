// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CompositeEmbedAttributesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Data;
using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Позволяет реализовать драйвер внедрения атрибутов в файл документа, чье поведение меняется
/// в зависимости от схемы обработки документа.
/// </summary>
public abstract class CompositeEmbedAttributesDriver : EmbedAttributesDriver
{
  private IEmbedAttributesDriver activeDriver;

  protected override void InitializeDriver(long documentId, int documentTypeId)
  {
    base.InitializeDriver(documentId, documentTypeId);
    this.activeDriver = this.SelectDriver(documentId, DBHelper.GetObjectTypeLID(documentId));
    this.activeDriver.BeginAction(documentId, documentTypeId);
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    if (this.activeDriver == null)
      return;
    this.activeDriver.EndAction();
    this.activeDriver = (IEmbedAttributesDriver) null;
  }

  /// <summary>
  /// Выбирает и возвращает драйвер, который должен использоваться для захвата изменений в документе указанного типа.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Объект драйвера, которому следует передать управление для продолжения анализа документа</returns>
  protected abstract IEmbedAttributesDriver SelectDriver(long documentId, LocalId<int> documentType);

  protected override ICollection<StringKey> DoGetEmbeddableAttributes(
    long documentId,
    int documentType)
  {
    return this.activeDriver.GetEmbeddableAttributes(documentId, documentType);
  }

  protected override string DoFindMasterFile(long documentId)
  {
    return this.activeDriver.FindMasterFile(documentId);
  }

  protected override bool DoHasAncillaryDocumentFiles(long documentId)
  {
    return this.activeDriver.HasAncillaryDocumentFiles(documentId);
  }

  protected override ICollection<string> DoGetAncillaryDocumentFiles(long documentId)
  {
    return this.activeDriver.GetAncillaryDocumentFiles(documentId);
  }

  protected override bool DoEmbedAttributes(
    long documentId,
    int documentType,
    string documentFilePath,
    ValueBag attributes)
  {
    return this.activeDriver.EmbedAttributes(documentId, documentType, documentFilePath, attributes);
  }
}
