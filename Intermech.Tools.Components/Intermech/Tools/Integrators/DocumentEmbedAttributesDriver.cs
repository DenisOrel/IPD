// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DocumentEmbedAttributesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Data;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data.Sync;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

public class DocumentEmbedAttributesDriver : EmbedAttributesDriver
{
  private IIntegrator integrator;
  private IApplicationFileTypes fileTypeService;
  private IDocumentAttributesSettingsService settingsService;
  private IDocumentApiService documentApiService;
  private IFileVault fileVaultService;

  /// <summary>Создает объект.</summary>
  /// <param name="integrator">Ссылка на объект интегратора</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект интегратора не может быть null</exception>
  public DocumentEmbedAttributesDriver(IIntegrator integrator)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  public IIntegrator Integrator
  {
    [DebuggerStepThrough] get => this.integrator;
  }

  public IApplicationFileTypes FileTypeService
  {
    [DebuggerStepThrough] get => this.fileTypeService;
  }

  public IDocumentAttributesSettingsService SettingsService
  {
    [DebuggerStepThrough] get => this.settingsService;
  }

  public IDocumentApiService DocumentApiService
  {
    [DebuggerStepThrough] get => this.documentApiService;
  }

  public IFileVault FileVaultService
  {
    [DebuggerStepThrough] get => this.fileVaultService;
  }

  protected override void InitializeDriver(long documentId, int documentTypeId)
  {
    base.InitializeDriver(documentId, documentTypeId);
    this.fileTypeService = ServiceUtils.GetService<IApplicationFileTypes>((object) this.Integrator, true);
    this.settingsService = ServiceUtils.GetService<IDocumentAttributesSettingsService>((object) this.Integrator, true);
    this.documentApiService = ServiceUtils.GetService<IDocumentApiService>((object) this.Integrator, true);
    this.fileVaultService = ClientContext.FileVault;
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.fileTypeService = (IApplicationFileTypes) null;
    this.settingsService = (IDocumentAttributesSettingsService) null;
    this.documentApiService = (IDocumentApiService) null;
    this.fileVaultService = (IFileVault) null;
  }

  protected override string DoFindMasterFile(long documentId)
  {
    string masterFileName = this.FileVaultService.DBFilesInfo.GetMasterFileName(documentId, false);
    return !string.IsNullOrEmpty(masterFileName) && this.FileTypeService.IsApplicationFile(masterFileName) ? masterFileName : (string) null;
  }

  protected override ICollection<StringKey> DoGetEmbeddableAttributes(
    long documentId,
    int documentType)
  {
    return this.SettingsService.SynchronizedDocumentAttributes.GetAttributes(documentType, true);
  }

  protected override bool DoEmbedAttributes(
    long documentId,
    int documentType,
    string documentFilePath,
    ValueBag documentAttributes)
  {
    this.DocumentApiService.OpenApiSession();
    try
    {
      bool flag = false;
      IOpenDocument openDocument = this.DocumentApiService.OpenDocuments.OpenDocument(documentFilePath);
      IAttributeCodec codec = this.DocumentApiService.OpenDocuments.GetCodec(openDocument);
      ContainerValues containerValues = codec.ReadFileProperties(openDocument.Properties, (ICollection<StringKey>) documentAttributes.Keys);
      DecodeAttributesParams decodeParams = new DecodeAttributesParams(openDocument.Properties, (ICollection<StringKey>) documentAttributes.Keys, containerValues, this.GetDecodeOptions(documentId, documentType));
      ValueBag valueBag = codec.Decode(decodeParams);
      DBToAppAttributeSyncTask attributeSyncTask = new DBToAppAttributeSyncTask();
      attributeSyncTask.EntityDisplayName = string.Join(", ", DBHelper.GetObjectNameInMessages(documentId), Path.GetFileName(documentFilePath));
      attributeSyncTask.SetDatabaseAttributes(documentAttributes, (IDBAttributableTypeRef) new DirectObjectAttributesRef(documentType));
      attributeSyncTask.SetApplicationAttributes(valueBag, containerValues.IsOpenMetadata);
      attributeSyncTask.AddAllAttributesToSync(false);
      attributeSyncTask.RunChecked(false);
      if (valueBag.HasChanges)
      {
        codec.Encode(new EncodeAttributesParams(openDocument.Properties, (ICollection<StringKey>) valueBag.GetChangedItemsKeys(), valueBag, containerValues, this.GetEncodeOptions(documentId, documentType))
        {
          ContainerDisplayName = Path.GetFileName(documentFilePath)
        });
        if (containerValues.Bag.HasChanges)
        {
          codec.Formatter.Write(openDocument.Properties, containerValues);
          this.DoSaveModifiedDocument(openDocument);
          flag = true;
        }
      }
      return flag;
    }
    finally
    {
      this.DocumentApiService.CloseApiSession();
    }
  }

  protected virtual void DoSaveModifiedDocument(IOpenDocument document)
  {
    this.DocumentApiService.OpenDocuments.Save(document);
  }

  protected virtual DecodeAttributesOptions GetDecodeOptions(long documentId, int documentType)
  {
    return DocumentAttributesOptions.GetDecodeOptions(documentType);
  }

  protected virtual EncodeAttributesOptions GetEncodeOptions(long documentId, int documentType)
  {
    EncodeAttributesOptions encodeOptions = DocumentAttributesOptions.GetEncodeOptions(documentType);
    encodeOptions.ReportErrorsOnly = false;
    return encodeOptions;
  }
}
