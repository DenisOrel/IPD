// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SidecarObjectHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data.Actions;
using Intermech.Interfaces.Data.SidecarObjects;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class SidecarObjectHandler : DocumentHandlerBase
{
  private SectionEntity sourceDocumentEntity;
  private SidecarObjectsCaptureChangesExtension @extension;
  private SidecarObjectsIDCache extensionIDCache;
  private ObjectContentStatus? updatedContentStatus;
  private string updatedCaption;

  public SidecarObjectHandler(
    MechanicalDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity sidecarEntity,
    SectionEntity sourceDocumentEntity,
    SidecarObjectsCaptureChangesExtension @extension)
    : base((DocumentCaptureChangesDriver) driver, ctx, sidecarEntity)
  {
    if (sourceDocumentEntity == null)
      throw new ArgumentNullException(nameof (sourceDocumentEntity));
    if (@extension == null)
      throw new ArgumentNullException(nameof (@extension));
    this.sourceDocumentEntity = sourceDocumentEntity;
    this.@extension = @extension;
    this.extensionIDCache = @extension.SidecarIDCache;
  }

  private MechanicalDriver MechanicalDriver => (MechanicalDriver) this.Driver;

  protected override void ProcessDependencies()
  {
  }

  /// <summary>Читает значения свойств из файла документа.</summary>
  /// <returns>Контейнер со значениями свойств. Если у файла нет свойств, либо нет соответствующего API, то метод должен вернуть пустой контейнер</returns>
  protected override ContainerValues ReadFileProperties()
  {
    return new ContainerValues(new ValueBag(), false);
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
    throw new NotSupportedException();
  }

  /// <summary>
  /// Выполняет декодирование значений атрибутов документа из свойств файла.
  /// </summary>
  /// <param name="fileProperties">Контейнер со свойствами файла</param>
  /// <returns>Контейнер с значениями атрибутов</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на контейнер со свойствами файла не может быть null</exception>
  protected override ValueBag DecodeDocumentAttributes(ContainerValues fileProperties)
  {
    return new ValueBag();
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
  }

  /// <summary>
  /// Сохраняет измененные файлы документа на диск. Этот метод используется для сохранения на диск любых изменений в файле документа, а не только
  /// сделанных интегратором. Например, метод будет вызван, если пользователь не сохранил документ в приложении-редакторе перед вызовом команды IPS.
  /// </summary>
  protected override IEnumerable<CooperativeState> SaveModifiedDocumentFiles()
  {
    yield break;
  }

  protected override void CreateNewDBObject()
  {
    this.Driver.Operations.Db.CreateBlankObject(this.DriverContext, this.DocumentEntity, this.@extension.TryCreateBlankSidecarObjectAction(this.sourceDocumentEntity, this.DocumentEntity));
  }

  /// <summary>Позволяет обработать файлы документа.</summary>
  protected override void ProcessFiles()
  {
    this.CustomFilesBaseDirectory = this.@extension.GetSidecarFilesBaseDirectory();
    this.ProcessSidecarFile();
    base.ProcessFiles();
  }

  private void ProcessSidecarFile()
  {
    SidecarObjectUpdateMode objectUpdateMode = this.@extension.GetSidecarObjectUpdateMode(this.sourceDocumentEntity);
    switch (objectUpdateMode)
    {
      case SidecarObjectUpdateMode.KeepActual:
        if (this.IsSourceFileNewOrModified() || this.DocumentObject.NewObject || this.GetCurrentContentStatus() != ObjectContentStatus.Actual)
        {
          this.MakeSidecarFileActual();
          break;
        }
        this.ReadSidecarFileInfoFromDb();
        break;
      case SidecarObjectUpdateMode.SetOutdated:
        if (this.IsSourceFileNewOrModified() && this.GetCurrentContentStatus() == ObjectContentStatus.Actual)
          this.MakeSidecarFileOutdated();
        this.ReadSidecarFileInfoFromDb();
        break;
      default:
        throw new NotSupportedEnumException((Enum) objectUpdateMode);
    }
  }

  private void MakeSidecarFileActual()
  {
    SidecarFileResult updateSidecarFile = this.@extension.TryCreateOrUpdateSidecarFile(this.sourceDocumentEntity, this.FileVaultService.WorkArea.AreaPath);
    if (updateSidecarFile.IsSuccessful)
    {
      this.DocumentFiles.MasterFile = ((SidecarFileResult.Success) updateSidecarFile).FilePath;
      this.updatedContentStatus = new ObjectContentStatus?(ObjectContentStatus.Actual);
      this.updatedCaption = this.CreateSidecarObjectCaption();
      FilesProcessingOptionsSection processingOptionsSection = this.DocumentEntity.Sections.Get<FilesProcessingOptionsSection>();
      processingOptionsSection.EnableFilesProcessing = true;
      processingOptionsSection.EnableDependenciesProcessing = true;
    }
    else
    {
      SidecarFileResult.Error error = (SidecarFileResult.Error) updateSidecarFile;
      if (this.DocumentObject.NewObject)
      {
        this.updatedContentStatus = new ObjectContentStatus?(ObjectContentStatus.NotSet);
        this.updatedCaption = $"{this.extensionIDCache.SidecarInstanceName} без файла";
      }
      else if (this.GetCurrentContentStatus() == ObjectContentStatus.Actual)
        this.updatedContentStatus = new ObjectContentStatus?(ObjectContentStatus.Outdated);
      if (!UIReport.Enabled)
        return;
      UIReport.ReportEvent(this.@extension.CreateErrorWhenSidecarFileUpdateFailed(this.sourceDocumentEntity), TraceLevel.Error);
      UIReport.ReportEvent(error.Message.Replace(Environment.NewLine, " "), TraceLevel.Error);
    }
  }

  private void MakeSidecarFileOutdated()
  {
    this.updatedContentStatus = new ObjectContentStatus?(ObjectContentStatus.Outdated);
  }

  private void ReadSidecarFileInfoFromDb()
  {
    this.DocumentFiles.MasterFile = Path.Combine(this.DocumentFilesBaseDirectory, this.FileVaultService.DBFilesInfo.GetMasterFileName(this.DocumentObject.ObjectId, true));
  }

  private bool IsSourceFileNewOrModified()
  {
    if (ObjectSection.IsNewObject(this.sourceDocumentEntity))
      return true;
    ObjectActionsSection objectActionsSection = this.sourceDocumentEntity.Sections.Get<ObjectActionsSection>((ObjectActionsSection) null);
    return objectActionsSection != null && objectActionsSection.ObjectActions.ServerActions.Exist((Predicate<IAction>) (action => action is UploadFilesAction));
  }

  private ObjectContentStatus GetCurrentContentStatus()
  {
    return (ObjectContentStatus) this.DocumentAttributes.DatabaseSet.Read<long>((StringKey) this.extensionIDCache.ContentStatus.Text, 0L);
  }

  /// <summary>
  /// Позволяет обновить значения атрибутов, которые есть только у объекта документа в базе IPS. В файле документа такие атрибуты
  /// не сохраняются.
  /// </summary>
  protected override void UpdateDBOnlyAttributes()
  {
    base.UpdateDBOnlyAttributes();
    ValueBag databaseSet = this.DocumentAttributes.DatabaseSet;
    if (this.DocumentObject.NewObject)
    {
      long newValue = Math.Abs(ObjectSection.GetObjectId(this.sourceDocumentEntity));
      databaseSet.Update((StringKey) this.extensionIDCache.SourceDocumentReference.Text, (object) newValue);
    }
    if (this.updatedContentStatus.HasValue)
      databaseSet.Update((StringKey) this.extensionIDCache.ContentStatus.Text, (object) (long) this.updatedContentStatus.Value);
    if (this.updatedCaption == null)
      return;
    databaseSet.Update((StringKey) this.extensionIDCache.Caption.Text, (object) this.updatedCaption);
  }

  private string CreateSidecarObjectCaption()
  {
    long objectId = ObjectSection.GetObjectId(this.sourceDocumentEntity);
    int objectType = ObjectSection.GetObjectType(this.sourceDocumentEntity);
    AttributesSection attributesSection = this.sourceDocumentEntity.Sections.Get<AttributesSection>();
    ICollection<StringKey> identityKeys = this.Driver.Operations.Documents.GetIdentityKeys();
    ValueBag documentAttributeBag = new ValueBag(identityKeys.Count);
    foreach (StringKey key in (IEnumerable<StringKey>) identityKeys)
    {
      ValueRecord valueRecord = attributesSection.DatabaseSet.Find(key);
      if (valueRecord != null && !valueRecord.IsNull)
      {
        string str = DocumentDesignationHelper.RemoveDocCode(valueRecord.Read<string>(string.Empty), objectType);
        if (!string.IsNullOrEmpty(str))
          documentAttributeBag.Add(key, (object) str);
      }
    }
    string sidecarObjectCaption = this.@extension.TryCreateSidecarObjectCaption(objectId, objectType, documentAttributeBag, (IEnumerable<StringKey>) identityKeys);
    return !string.IsNullOrEmpty(sidecarObjectCaption) ? sidecarObjectCaption : string.Empty;
  }

  /// <summary>
  /// Позволяет выполнить тонкую настройку операции записи измененных файлов документа в базу данных.
  /// </summary>
  /// <param name="action">Действие записи файлов документа</param>
  protected override void SetupFilesUploadAction(UploadFilesAction action)
  {
    base.SetupFilesUploadAction(action);
    action.FullRewriteMode = true;
  }
}
