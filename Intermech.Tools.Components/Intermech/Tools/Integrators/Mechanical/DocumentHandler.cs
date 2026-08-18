// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.DocumentHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Реализует базовый класс для всех обработчиков документов CAD-системы.
/// </summary>
public class DocumentHandler : DocumentHandlerBase
{
  private IDocumentCADApiService docApiService;

  /// <summary>Создает объект.</summary>
  /// <param name="ctx">Рабочий контекст анализатора</param>
  /// <param name="docItem">Объект документа в базе данных анализатора</param>
  protected DocumentHandler(
    MechanicalDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity docItem)
    : base((DocumentCaptureChangesDriver) driver, ctx, docItem)
  {
  }

  /// <summary>
  /// Возвращает анализатор изменений документов CAD-системы.
  /// </summary>
  protected MechanicalDriver Driver
  {
    [DebuggerStepThrough] get => (MechanicalDriver) base.Driver;
  }

  /// <summary>
  /// Возвращает фасад API документов со стороны интегрируемого приложения.
  /// Значение свойства доступно только после инициализации текущего объекта.
  /// </summary>
  protected IDocumentCADApiService DocumentApiService
  {
    [DebuggerStepThrough] get => this.docApiService;
  }

  protected override void ValidateProperties()
  {
    base.ValidateProperties();
    if (ObjectSection.TryGetObjectType(this.DocumentEntity) == -1)
      throw new InvalidOperationException("Тип документа уже должен быть известен.");
    if (!this.Driver.HasDocumentKind(this.DocumentEntity))
      throw new InvalidOperationException("Вид документа уже должен быть известен.");
  }

  /// <summary>Выполняет инициализацию обработчика.</summary>
  protected override void InitializeHandler()
  {
    base.InitializeHandler();
    this.docApiService = this.Driver.GetDocumentApiService(this.DocumentEntity);
  }

  /// <summary>
  /// Выполняет обработку файловых зависимостей документа. По каждой зависимости в базе данных анализатора создается объект и назначается обработчик.
  /// </summary>
  protected override void ProcessDependencies()
  {
    if (!this.Driver.Operations.Documents.GetDependenciesProcessingFlag(this.DocumentEntity))
      return;
    this.DocumentApiService.TryGetFileDependenciesHandler(this.DocumentEntity)?.Run(this.DocumentEntity);
  }

  /// <summary>Читает значения свойств из файла документа.</summary>
  /// <returns>Контейнер со значениями свойств. Если у файла нет свойств, либо нет соответствующего API, то метод должен вернуть пустой контейнер</returns>
  protected override ContainerValues ReadFileProperties()
  {
    return this.DocumentApiService.ReadDocumentProperties(this.DocumentEntity);
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
    return this.DocumentApiService.WriteDocumentProperties(this.DocumentEntity, fileProperties);
  }

  /// <summary>
  /// Выполняет декодирование значений атрибутов документа из свойств файла.
  /// </summary>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <returns>Контейнер с значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на контейнер со свойствами файла не может быть null</exception>
  protected override ValueBag DecodeDocumentAttributes(ContainerValues fileProperties)
  {
    return this.DocumentApiService.DecodeDocumentAttributes(this.DocumentEntity, fileProperties);
  }

  /// <summary>
  /// Выполняет обратное кодирование значений атрибутов документа в значения свойств файла. Если поддерживается
  /// только чтение свойств, но не запись, то этот метод может не выполнять кодирование. Исключение при этом сбрасываться не должно.
  /// </summary>
  /// <param name="attributeKeys">Список имен кодируемых атрибутов</param>
  /// <param name="attributes">Контейнер с значениями атрибутов</param>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на контейнеры не могут быть null</exception>
  protected override void EncodeDocumentAttributes(
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
    this.DocumentApiService.EncodeDocumentAttributes(this.DocumentEntity, attributeKeys, attributes, fileProperties);
  }

  /// <summary>
  /// Корректирует значения атрибутов, прочитанные из файла документа, перед переносом значений атрибутов в объект документа.
  /// </summary>
  protected override void CorrectAttributes()
  {
    base.CorrectAttributes();
    new FillEmptyDocumentIdentityHandler((DocumentCaptureChangesDriver) this.Driver, this.DocumentEntity).Perform();
    string typeAttributeName = this.DocumentApiService.GetDocumentTypeAttributeName(this.DocumentEntity);
    if (!string.IsNullOrEmpty(typeAttributeName))
      new FillObjectTypeAttributeHandler(this.DocumentEntity, typeAttributeName).Perform();
    new FillDocumentCodeHandler(this.DocumentEntity).Perform();
    if (this.DocumentObject.NewObject)
      new CheckIdentityOnUniquenessAction(this.Driver, this.DriverContext, this.DocumentEntity).Perform();
    this.DocumentApiService.ProcessDocumentAttributes(this.DocumentEntity, this.DocumentAttributes.WorkingSet, this.DocumentAttributes.DatabaseSet);
  }

  /// <summary>
  /// Возвращает список ключей атрибутов, значения которых должны быть перенесены из файла в объект документа IPS.
  /// Как правило, этот список задается в настройках интегратора.
  /// </summary>
  /// <returns>Список ключей атрибутов</returns>
  protected override ICollection<StringKey> GetTransferableAttributes()
  {
    ICollection<StringKey> transferableAttributes = base.GetTransferableAttributes();
    transferableAttributes.AddRange<StringKey>((IEnumerable<StringKey>) this.DocumentApiService.GetDocumentSyncAttributes(this.DocumentEntity));
    return transferableAttributes;
  }

  /// <summary>
  /// Возвращает true, если атрибут обязательно должен быть перенесен из файла в объект документа. Если это не удается сделать,
  /// то будет сброшено исключение и вся операция будет прервана. Ошибки переноса остальных атрибутов игнорируются с занесением информации о
  /// сбое в протокол выполнения.
  /// </summary>
  /// <param name="attributeKey">Ключ атрибута</param>
  /// <returns>Признак, что ошибки в процессе переноса этого атрибута из файла в объект документа недопустимы</returns>
  protected override bool IsTransferRequired(StringKey attributeKey)
  {
    return attributeKey == (StringKey) IDCache.Default.Designation.Text || attributeKey == (StringKey) IDCache.Default.Name.Text || base.IsTransferRequired(attributeKey);
  }

  /// <summary>
  /// Позволяет обновить значения атрибутов, которые есть только у объекта документа в базе IPS. В файле документа такие атрибуты
  /// не сохраняются.
  /// </summary>
  protected override void UpdateDBOnlyAttributes()
  {
    base.UpdateDBOnlyAttributes();
    this.UpdatePrivateFilesAttribute();
    this.ReportUnresolvedDependencies();
  }

  private void UpdatePrivateFilesAttribute()
  {
    List<string> privateFiles = this.DocumentApiService.GetPrivateFiles(this.DocumentEntity);
    if (privateFiles != null && privateFiles.Count > 0)
    {
      privateFiles.RemoveAll((Predicate<string>) (filePath => string.IsNullOrEmpty(filePath) || !PathUtils.IsPlacedIn(filePath, this.DocumentFilesBaseDirectory)));
      for (int index = 0; index < privateFiles.Count; ++index)
        privateFiles[index] = PathUtils.GetRelativePath(privateFiles[index], this.DocumentFilesBaseDirectory, RelativePathOptions.ThrowIfNotPossible);
    }
    if (privateFiles != null && privateFiles.Count > 0)
    {
      PathCollection documentPrivateFiles = this.GetDocumentPrivateFiles();
      bool flag = documentPrivateFiles == null || privateFiles.Count != documentPrivateFiles.Count;
      if (!flag)
      {
        foreach (string str in privateFiles)
        {
          if (!documentPrivateFiles.Contains(str))
          {
            flag = true;
            break;
          }
        }
      }
      if (!flag)
        return;
      object[] newValue = new object[privateFiles.Count];
      for (int index = 0; index < privateFiles.Count; ++index)
        newValue[index] = (object) privateFiles[index];
      this.DocumentAttributes.DatabaseSet.Update((StringKey) IDCache.Default.PrivateFiles.Text, (object) newValue);
      this.DocumentAttributes.DatabaseSet.SetFlag((StringKey) IDCache.Default.PrivateFiles.Text, NamedFlags.ThrowSetException);
    }
    else
    {
      PathCollection documentPrivateFiles = this.GetDocumentPrivateFiles();
      if (documentPrivateFiles == null || documentPrivateFiles.Count <= 0)
        return;
      this.DocumentAttributes.DatabaseSet.Update((StringKey) IDCache.Default.PrivateFiles.Text, (object) new object[1]
      {
        (object) DBNull.Value
      });
      this.DocumentAttributes.DatabaseSet.SetFlag((StringKey) IDCache.Default.PrivateFiles.Text, NamedFlags.ThrowSetException);
    }
  }

  private PathCollection GetDocumentPrivateFiles()
  {
    if (!this.DocumentObject.NewObject)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetObject(this.DocumentObject.ObjectId, true).GetAttributeByID(IDCache.Default.PrivateFiles.Id);
        if (attributeById != null)
        {
          if (!attributeById.IsNull)
          {
            PathCollection documentPrivateFiles = new PathCollection(attributeById.Values.Length);
            foreach (string str in attributeById.Values)
              documentPrivateFiles.Add(str);
            return documentPrivateFiles;
          }
        }
      }
    }
    return (PathCollection) null;
  }

  private void ReportUnresolvedDependencies()
  {
    UnresolvedFilesSection unresolvedFilesSection = this.DocumentEntity.Sections.Get<UnresolvedFilesSection>((UnresolvedFilesSection) null);
    if (unresolvedFilesSection != null && unresolvedFilesSection.Files.Count > 0)
    {
      string[] strArray = new string[unresolvedFilesSection.Files.Count];
      unresolvedFilesSection.Files.CopyTo(strArray, 0);
      this.DocumentAttributes.DatabaseSet.Update((StringKey) IDCache.Default.RequireFileCheck.Text, (object) strArray);
      if (!UIReport.Enabled)
        return;
      UIReport.ReportEvent(LocalizationHolder.rm.GetString("SR_561"), TraceLevel.Warning);
      UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_562"), (object) strArray.Length));
      UIReport.Indent();
      foreach (string text in strArray)
        UIReport.ReportEvent(text);
      UIReport.Unindent();
    }
    else
    {
      if (!this.HasDBUnresolvedDependencies())
        return;
      this.DocumentAttributes.DatabaseSet.Update((StringKey) IDCache.Default.RequireFileCheck.Text, (object) DeleteModesEnum.None);
    }
  }

  private bool HasDBUnresolvedDependencies()
  {
    if (!this.DocumentObject.NewObject)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObject(this.DocumentObject.ObjectId, true).GetAttributeByID(IDCache.Default.RequireFileCheck.Id) != null)
          return true;
      }
    }
    return false;
  }

  protected override void DeleteUnwantedAttributes()
  {
    base.DeleteUnwantedAttributes();
    this.Driver.Operations.Db.RemoveIntegrationStatusIfEmpty(this.DocumentEntity);
    this.Driver.Operations.Db.RemoveIntegrationErrorsIfEmpty(this.DocumentEntity);
  }

  protected override PathCollection CollectNewAncillaryFiles()
  {
    PathCollection collection = base.CollectNewAncillaryFiles();
    collection.AddRange<string>((IEnumerable<string>) this.DocumentApiService.GetSatelliteFiles(this.DocumentEntity));
    return collection;
  }

  /// <summary>
  /// Сохраняет измененные файлы документа на диск. Этот метод используется для сохранения на диск любых изменений в файле документа, а не только
  /// сделанных интегратором. Например, метод будет вызван, если пользователь не сохранил документ в приложении-редакторе перед вызовом команды IPS.
  /// </summary>
  protected override IEnumerable<CooperativeState> SaveModifiedDocumentFiles()
  {
    TopDownSaveFilesAction downSaveFilesAction = TopDownSaveFilesAction.GetOrCreate(this.DriverContext, true);
    downSaveFilesAction.RegisterDocument(this.DocumentEntity, (IAction) new MethodAction(new Action(this.SaveModifiedDocumentCallback)));
    yield return this.Wait(downSaveFilesAction.Complete);
  }

  private void SaveModifiedDocumentCallback()
  {
    this.DocumentApiService.SaveDocumentFile(this.DocumentEntity);
  }

  /// <summary>Выполняет анализ связей документа.</summary>
  protected override void ProcessRelations()
  {
    base.ProcessRelations();
    SyncDocumentStructureAction documentStructureAction = this.CreateDocumentStructureAction();
    if (this.Driver.Operations.Documents.GetDependenciesProcessingFlag(this.DocumentEntity))
    {
      documentStructureAction.Perform();
    }
    else
    {
      if (!this.DocumentObject.NewObject)
        return;
      documentStructureAction.SetEmptyDocumentStructureStatus();
    }
  }

  protected virtual SyncDocumentStructureAction CreateDocumentStructureAction()
  {
    SyncDocumentStructureAction documentStructureAction = new SyncDocumentStructureAction((DocumentCaptureChangesDriver) this.Driver, this.DriverContext, this.DocumentEntity);
    documentStructureAction.ReadLocalRelationAttributes += (EventHandler<RelationAttributesEventArgs>) ((sender, e) =>
    {
      ValueBag valueBag = this.DocumentApiService.TryReadDocumentRelationAttributes(e.Project, e.Part);
      if (valueBag == null)
        return;
      foreach (ValueRecord valueRecord in valueBag)
      {
        if (!e.RelationAttributes.Exists(valueRecord.Key))
        {
          e.RelationAttributes.Update(valueRecord.Key, valueRecord.Value);
          e.RelationAttributes.CopyFlag(valueRecord.Key, valueRecord.Flags, NamedFlags.ThrowSetException);
        }
      }
    });
    return documentStructureAction;
  }
}
