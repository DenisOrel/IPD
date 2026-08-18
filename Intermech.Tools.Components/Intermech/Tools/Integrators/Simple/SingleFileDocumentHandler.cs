// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileDocumentHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.Data.Sync;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

/// <summary>
/// Реализует общий для всех интеграторов обработчик вспомогательных документов.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="driver">Стратегия анализа изменений</param>
/// <param name="ctx">Контекст обработки</param>
/// <param name="docItem">Рабочий элемент для обрабатываемого документа</param>
public class SingleFileDocumentHandler(
  DocumentCaptureChangesDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity docItem) : DocumentHandlerBase(driver, ctx, docItem)
{
  protected IServiceProvider integrator;
  protected IDocumentAttributesSettingsService settingsSvc;
  protected IDocumentApiService apiSvc;

  /// <summary>Возвращает или задает объект интегратора.</summary>
  public IServiceProvider Integrator
  {
    get => this.integrator;
    set => this.integrator = value;
  }

  /// <summary>
  /// Позволяет проверить корректность значений свойств, задающих поведение обработчика. Этот метод вызывается обработчиком перед началом работы.
  /// </summary>
  /// <exception cref="T:Intermech.Tools.DataExchange.DataExchangeConfigurationException">Свойства обработчика заполнены неверно</exception>
  protected override void ValidateProperties()
  {
    base.ValidateProperties();
    if (this.integrator == null)
      throw new DataExchangeConfigurationException("Integrator");
  }

  /// <summary>Выполняет инициализацию обработчика.</summary>
  protected override void InitializeHandler()
  {
    this.settingsSvc = ServiceUtils.GetService<IDocumentAttributesSettingsService>((object) this.integrator, true);
    this.apiSvc = ServiceUtils.GetService<IDocumentApiService>((object) this.integrator, true);
    base.InitializeHandler();
  }

  protected override void ProcessDependencies()
  {
  }

  protected virtual ICollection<StringKey> GetSynchronizedAttributes()
  {
    return this.settingsSvc.SynchronizedDocumentAttributes.GetAttributes(this.DocumentObject.ObjectType, false);
  }

  /// <summary>Читает значения свойств из файла документа.</summary>
  /// <returns>Контейнер со значениями свойств. Если у файла нет свойств, либо нет соответствующего API, то метод должен вернуть пустой контейнер</returns>
  protected sealed override ContainerValues ReadFileProperties()
  {
    IOpenDocument openDocument = this.DocumentEntity.Sections.Get<IOpenDocument>();
    return this.apiSvc.OpenDocuments.GetCodec(openDocument).ReadFileProperties(openDocument.Properties, this.GetSynchronizedAttributes());
  }

  /// <summary>
  /// Записывает измененные значения свойств в файл документа. Этот метод вызывается только при наличии изменений в свойствах.
  /// Если поддерживается только чтение свойств, то этот метод должен сбросить исключение.
  /// </summary>
  /// <param name="fileProperties">Контейнер со значениями свойств</param>
  /// <returns>true, если запись в файл была произведена</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на контейнер не может быть null</exception>
  /// <exception cref="T:System.NotSupportedException">Запись свойств в файл документа не поддерживается</exception>
  protected override bool WriteFileProperties(ContainerValues fileProperties)
  {
    IOpenDocument openDocument = this.DocumentEntity.Sections.Get<IOpenDocument>();
    return this.apiSvc.OpenDocuments.GetCodec(openDocument).Formatter.Write(openDocument.Properties, fileProperties);
  }

  /// <summary>
  /// Выполняет декодирование значений атрибутов документа из свойств файла.
  /// </summary>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <returns>Контейнер с значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на контейнер со свойствами файла не может быть null</exception>
  protected sealed override ValueBag DecodeDocumentAttributes(ContainerValues fileProperties)
  {
    if (fileProperties == null)
      throw new ArgumentNullException(nameof (fileProperties));
    IOpenDocument openDocument = this.DocumentEntity.Sections.Get<IOpenDocument>();
    DecodeAttributesOptions decodeOptions = this.Driver.Operations.Documents.GetDecodeOptions(this.DocumentEntity);
    DecodeAttributesParams decodeParams = new DecodeAttributesParams(openDocument.Properties, this.GetSynchronizedAttributes(), fileProperties, decodeOptions);
    return this.apiSvc.OpenDocuments.GetCodec(openDocument).Decode(decodeParams);
  }

  /// <summary>
  /// Выполняет обратное кодирование значений атрибутов документа в значения свойств файла. Если поддерживается
  /// только чтение свойств, но не запись, то этот метод может не выполнять кодирование. Исключение при этом сбрасываться не должно.
  /// </summary>
  /// <param name="attributeKeys">Список имен кодируемых атрибутов</param>
  /// <param name="attributes">Контейнер с значениями атрибутов</param>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на контейнеры не могут быть null</exception>
  protected sealed override void EncodeDocumentAttributes(
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
    IOpenDocument openDocument = this.DocumentEntity.Sections.Get<IOpenDocument>();
    EncodeAttributesOptions encodeOptions = this.Driver.Operations.Documents.GetEncodeOptions(this.DocumentEntity);
    this.apiSvc.OpenDocuments.GetCodec(openDocument).Encode(new EncodeAttributesParams(openDocument.Properties, attributeKeys, attributes, fileProperties, encodeOptions)
    {
      ContainerDisplayName = DisplaySection.GetQualifiedName(this.DocumentEntity)
    });
  }

  /// <summary>
  /// Возвращает список ключей атрибутов, значения которых должны быть перенесены из файла в объект документа IPS.
  /// Как правило, этот список задается в настройках интегратора.
  /// </summary>
  /// <returns>Список ключей атрибутов</returns>
  protected override ICollection<StringKey> GetTransferableAttributes()
  {
    ICollection<StringKey> transferableAttributes = base.GetTransferableAttributes();
    transferableAttributes.AddRange<StringKey>((IEnumerable<StringKey>) this.GetSynchronizedAttributes());
    return transferableAttributes;
  }

  /// <summary>
  /// Корректирует значения атрибутов, прочитанные из файла документа, перед переносом значений атрибутов в объект документа.
  /// </summary>
  protected override void CorrectAttributes()
  {
    base.CorrectAttributes();
    new FillEmptyDocumentIdentityHandler(this.Driver, this.DocumentEntity).Perform();
  }

  /// <summary>
  /// Выполняет перенос атрибутов из файла в объект документа.
  /// </summary>
  /// <param name="attributes">Список ключей атрибутов для переноса</param>
  protected override void TransferAttributes(ICollection<StringKey> attributes)
  {
    if (attributes == null)
      throw new ArgumentNullException(nameof (attributes));
    if (!this.DocumentObject.NewObject && !this.DocumentAttributes.DatabaseSet.Read<bool>((StringKey) IDCache.Default.OwnedByIntegrator.Text, false))
    {
      if (UIReport.Enabled)
        UIReport.ReportEvent(LocalizationHolder.rm.GetString("SR_538"));
      this.CopyDBAttributesToDocument(attributes);
    }
    base.TransferAttributes(attributes);
  }

  private void CopyDBAttributesToDocument(ICollection<StringKey> attributes)
  {
    DBToAppAttributeSyncTask attributeSyncTask = new DBToAppAttributeSyncTask();
    attributeSyncTask.EntityDisplayName = DisplaySection.GetQualifiedName(this.DocumentEntity);
    attributeSyncTask.SetDatabaseAttributes(this.DocumentAttributes.DatabaseSet, this.DocumentAttributesLayout);
    attributeSyncTask.SetApplicationAttributes(this.DocumentAttributes.WorkingSet, this.DocumentAttributes.EmbeddedSet.IsOpenMetadata);
    foreach (StringKey attribute in (IEnumerable<StringKey>) attributes)
    {
      ValueRecord valueRecord = this.DocumentAttributes.DatabaseSet.Find(attribute);
      if (valueRecord != null && !valueRecord.IsNull)
        attributeSyncTask.Attributes.Add(new AttributeSyncUnit(attribute, false));
    }
    attributeSyncTask.RunChecked(false);
    if (!this.DocumentAttributes.WorkingSet.HasChanges)
      return;
    List<StringKey> changedItemsKeys = this.DocumentAttributes.WorkingSet.GetChangedItemsKeys();
    changedItemsKeys.RemoveAll((Predicate<StringKey>) (attributeKey => !attributes.Contains(attributeKey)));
    if (changedItemsKeys.Count == 0)
      return;
    this.EncodeDocumentAttributes((ICollection<StringKey>) changedItemsKeys, this.DocumentAttributes.WorkingSet, this.DocumentAttributes.EmbeddedSet);
  }

  /// <summary>
  /// Позволяет обновить значения атрибутов, которые есть только у объекта документа в базе IPS. В файле документа такие атрибуты
  /// не сохраняются.
  /// </summary>
  protected override void UpdateDBOnlyAttributes()
  {
    base.UpdateDBOnlyAttributes();
    this.DocumentAttributes.DatabaseSet.Update((StringKey) IDCache.Default.OwnedByIntegrator.Text, (object) true);
    this.DocumentAttributes.DatabaseSet.SetFlag((StringKey) IDCache.Default.OwnedByIntegrator.Text, NamedFlags.ThrowSetException);
  }

  /// <summary>
  /// Сохраняет измененные файлы документа на диск. Этот метод используется для сохранения на диск любых изменений в файле документа, а не только
  /// сделанных интегратором. Например, метод будет вызван, если пользователь не сохранил документ в приложении-редакторе перед вызовом команды IPS.
  /// </summary>
  protected sealed override IEnumerable<CooperativeState> SaveModifiedDocumentFiles()
  {
    this.SaveModifiedDocument(this.DocumentEntity.Sections.Get<IOpenDocument>());
    yield break;
  }

  /// <summary>
  /// Сохраняет измененные файлы документа на диск. Этот метод используется для сохранения на диск любых изменений в файле документа, а не только
  /// сделанных интегратором. Например, метод будет вызван, если пользователь не сохранил документ в приложении-редакторе перед вызовом команды IPS.
  /// </summary>
  /// <param name="document">Объект документа, открытого в приложении</param>
  protected virtual void SaveModifiedDocument(IOpenDocument document)
  {
    this.apiSvc.OpenDocuments.Save(document);
  }
}
