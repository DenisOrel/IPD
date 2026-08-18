// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.StandaloneView.StandaloneViewService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Runtime;
using Intermech.Tools.Data.Sync;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.StandaloneView;

/// <summary>
/// Реализует сервис интегратора, отвечающий за внедрение в файлы документов сведений, необходимых для режима автономного просмотра.
/// Эти сведения включают в себя информацию об актуальных подписях документа, контрольной сумме файла,
/// атрибутах документа, заполняемых после согласования документа, и др.
/// </summary>
/// <remarks>
/// Для записи информации в файлы документов используется сервис интегратора IDocumentApiService. Запись подписей в файлы документов, открытые в приложении, не поддерживается.
/// </remarks>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class StandaloneViewService(IIntegrator owner) : StandaloneViewServiceBase(owner)
{
  private IDocumentApiService documentApiService;

  /// <summary>
  /// Возвращает или задает ссылку на сервис для работы с API документов приложения. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IDocumentApiService DocumentApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.documentApiService;
    }
    set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.documentApiService = value;
      }
    }
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.DocumentApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "DocumentApiService");
  }

  /// <summary>
  /// Организует запись в файл документа информации для автономного просмотра.
  /// </summary>
  /// <param name="operation">Параметры выполняемой операции</param>
  protected override void DoInjectViewData(StandaloneViewDataInjectionOperation operation)
  {
    StandaloneViewService.OpenDocumentData openDocumentData = new StandaloneViewService.OpenDocumentData();
    operation.CustomData = (object) openDocumentData;
    this.CheckApplicationInstalled(operation.Parameters.FilePath);
    this.DocumentApiService.OpenApiSession();
    try
    {
      IOpenDocument openDocument = this.DocumentApiService.OpenDocuments.FindOpenDocument(operation.Parameters.FilePath);
      if (openDocument != null)
      {
        openDocumentData.Document = openDocument;
        this.DoInjectViewDataIntoAlreadyOpenFile(operation);
      }
      else
        base.DoInjectViewData(operation);
    }
    finally
    {
      this.DocumentApiService.CloseApiSession();
    }
  }

  private void CheckApplicationInstalled(string filePath)
  {
    IApplicationApiService service = ServiceUtils.GetService<IApplicationApiService>((object) this.Integrator, false);
    if (service != null && !service.IsApplicationInstalled)
      throw new ApplicationNotInstalledException(this.Integrator.DisplayName, $"Невозможно записать необходимые сведения в файл документа '{Path.GetFileName(filePath)}', так как приложение '{service.ApplicationName}' не установлено.");
  }

  protected override void DoWriteViewDataIntoTempFile(
    StandaloneViewDataInjectionOperation operation,
    string tempFilePath)
  {
    IOpenDocument openDocument = this.DocumentApiService.OpenDocuments.OpenDocument(tempFilePath);
    try
    {
      ((StandaloneViewService.OpenDocumentData) operation.CustomData).Document = openDocument;
      this.DoWriteViewDataIntoOpenFile(operation);
      this.DocumentApiService.OpenDocuments.Save(openDocument);
    }
    finally
    {
      this.DocumentApiService.OpenDocuments.Close(openDocument);
    }
  }

  /// <summary>
  /// Записывает информацию для просмотра в открытый файл документа. Объект открытого файла хранится в свойстве opParams.CustomData.
  /// </summary>
  /// <param name="operation">Параметры выполняемой операции</param>
  protected override void DoWriteViewDataIntoOpenFile(StandaloneViewDataInjectionOperation operation)
  {
    StandaloneViewService.OpenDocumentData customData = (StandaloneViewService.OpenDocumentData) operation.CustomData;
    if (customData.Codec == null)
      customData.Codec = this.DocumentApiService.OpenDocuments.GetCodec(customData.Document);
    base.DoWriteViewDataIntoOpenFile(operation);
  }

  protected sealed override void DoWriteAttributesIntoOpenFile(
    StandaloneViewDataInjectionOperation operation,
    List<ValueRecord> attributeValues)
  {
    StandaloneViewService.OpenDocumentData customData = (StandaloneViewService.OpenDocumentData) operation.CustomData;
    CollectionUtils.RemoveDuplicates<ValueRecord>(attributeValues, (IEqualityComparer<ValueRecord>) StandaloneViewService.ValueRecordKeyComparer.Instance);
    ValueBag table = new ValueBag((ICollection<ValueRecord>) attributeValues);
    ContainerValues values = customData.Codec.Formatter.Read(customData.Document.Properties, (ICollection<StringKey>) table.Keys);
    DBToAppAttributeSyncTask attributeSyncTask = new DBToAppAttributeSyncTask();
    attributeSyncTask.EntityDisplayName = $"Документ #{operation.Parameters.ObjectId}";
    attributeSyncTask.SetDatabaseAttributes(table, (IDBAttributableTypeRef) new DirectObjectAttributesRef(operation.ObjectTypeId));
    attributeSyncTask.SetApplicationAttributes(values.Bag, values.IsOpenMetadata);
    attributeSyncTask.AddAllAttributesToSync(false);
    attributeSyncTask.RunChecked(false);
    if (!values.Bag.HasChanges)
      return;
    customData.Codec.Formatter.Write(customData.Document.Properties, values);
  }

  /// <summary>
  /// Описывает открытый файл документа, в который производится запись.
  /// </summary>
  protected sealed class OpenDocumentData
  {
    public IOpenDocument Document { get; set; }

    public IAttributeCodec Codec { get; set; }
  }

  private sealed class ValueRecordKeyComparer : IEqualityComparer<ValueRecord>
  {
    public static readonly StandaloneViewService.ValueRecordKeyComparer Instance = new StandaloneViewService.ValueRecordKeyComparer();

    public bool Equals(ValueRecord x, ValueRecord y) => x.Key == y.Key;

    public int GetHashCode(ValueRecord obj) => obj.Key.GetHashCode();
  }
}
