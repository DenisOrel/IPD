// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.EmbedAttributesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.DataExchange;

public abstract class EmbedAttributesDriver : IEmbedAttributesDriver
{
  private bool active;

  /// <summary>
  /// Подготавливает драйвер к обработке нового объекта. Этот метод следует использовать для контроля установки свойств объекта, а также
  /// создания вспомогательных объектов и сервисов.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <exception cref="T:Intermech.Tools.Integrators.DataExchange.CaptureChangesConfigurationException">Одно из свойств объекта не инициализировано должным образом</exception>
  public void BeginAction(long documentId, int documentTypeId)
  {
    if (this.active)
      throw new InvalidOperationException("Method BeginAction already called.");
    this.ValidateDriverProperties();
    try
    {
      this.InitializeDriver(documentId, documentTypeId);
      this.active = true;
    }
    catch
    {
      this.EndAction();
      throw;
    }
  }

  protected virtual void ValidateDriverProperties()
  {
  }

  protected virtual void InitializeDriver(long documentId, int documentTypeId)
  {
  }

  /// <summary>
  /// Очищает драйвер в конце обработки объекта. Этот метод следует использовать для освобождения ссылок на вспомогательные объекты и сервисы.
  /// Метод не должен сбрасывать исключений, так как он может являться частью обработчика уже возникшего исключения.
  /// </summary>
  public void EndAction()
  {
    try
    {
      this.ClearDriver();
    }
    catch
    {
    }
    finally
    {
      this.active = false;
    }
  }

  protected virtual void ClearDriver()
  {
  }

  /// <summary>
  /// Возвращает признак, что метод BeginAction был выполнен без ошибок.
  /// </summary>
  public bool Active => this.active;

  /// <summary>
  /// Позволяет убедиться, что метод BeginAction() был вызван.
  /// </summary>
  private void ValidateActive()
  {
    if (!this.active)
      throw new InvalidOperationException("Method BeginAction must be called first.");
  }

  public ICollection<StringKey> GetEmbeddableAttributes(long documentId, int documentType)
  {
    if (documentId == 0L)
      throw new ArgumentException();
    if (documentType == -1)
      throw new ArgumentException();
    this.ValidateActive();
    return this.DoGetEmbeddableAttributes(documentId, documentType);
  }

  public string FindMasterFile(long documentId)
  {
    if (documentId == 0L)
      throw new ArgumentException();
    this.ValidateActive();
    return this.DoFindMasterFile(documentId);
  }

  public bool HasAncillaryDocumentFiles(long documentId)
  {
    if (documentId == 0L)
      throw new ArgumentException();
    this.ValidateActive();
    return this.DoHasAncillaryDocumentFiles(documentId);
  }

  public ICollection<string> GetAncillaryDocumentFiles(long documentId)
  {
    if (documentId == 0L)
      throw new ArgumentException();
    this.ValidateActive();
    return this.DoGetAncillaryDocumentFiles(documentId);
  }

  public bool EmbedAttributes(
    long documentId,
    int documentType,
    string documentFilePath,
    ValueBag attributes)
  {
    if (documentId == 0L)
      throw new ArgumentException();
    if (documentType == -1)
      throw new ArgumentException();
    if (documentFilePath == null)
      throw new ArgumentNullException(nameof (documentFilePath));
    if (attributes == null)
      throw new ArgumentNullException(nameof (attributes));
    this.ValidateActive();
    return this.DoEmbedAttributes(documentId, documentType, documentFilePath, attributes);
  }

  public void FlushChanges()
  {
    this.ValidateActive();
    this.DoFlushChanges();
  }

  protected abstract ICollection<StringKey> DoGetEmbeddableAttributes(
    long documentId,
    int documentType);

  protected abstract string DoFindMasterFile(long documentId);

  protected virtual bool DoHasAncillaryDocumentFiles(long documentId) => false;

  protected virtual ICollection<string> DoGetAncillaryDocumentFiles(long documentId)
  {
    throw new NotSupportedException();
  }

  protected abstract bool DoEmbedAttributes(
    long documentId,
    int documentType,
    string documentFilePath,
    ValueBag attributes);

  protected virtual void DoFlushChanges()
  {
  }
}
