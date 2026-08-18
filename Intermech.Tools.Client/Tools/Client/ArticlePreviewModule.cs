// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.ArticlePreviewModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.Data;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class ArticlePreviewModule : InitializerModule
{
  private IPreviewExtender previewService;
  private ArticlePreviewModule instance;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.previewService = ServiceUtils.GetService<IPreviewExtender>((object) ServicesManager.ServiceContainer, true);
    this.instance = new ArticlePreviewModule();
    this.previewService.Extend += new ExtendEventHandler(this.instance.OnPreview);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.previewService == null)
      return;
    if (this.instance != null)
    {
      this.previewService.Extend -= new ExtendEventHandler(this.instance.OnPreview);
      this.instance = (ArticlePreviewModule) null;
    }
    this.previewService = (IPreviewExtender) null;
  }

  private void OnPreview(ExtendEventArgs e)
  {
    if (!this.IsArticleObject(e.ObjectID, e.ObjectType))
      return;
    VersionsRulePackage currentWindowRule = VersionsRuleSources.GetCurrentWindowRule();
    if (currentWindowRule == null)
      return;
    List<long> articleDocuments = DBDocumentHelper.FindArticleDocuments(e.ObjectID, true, currentWindowRule);
    long num1 = 0;
    int fileAttributeId = this.GetFileAttributeId();
    foreach (long num2 in articleDocuments)
    {
      List<ArticlePreviewModule.DocumentFile> documentFiles = this.GetDocumentFiles(num2, fileAttributeId);
      if (num1 == 0L)
      {
        string configurationFile = DBDocumentHelper.GetCADConfigurationFile(e.ObjectID, num2);
        if (!string.IsNullOrEmpty(configurationFile))
        {
          foreach (ArticlePreviewModule.DocumentFile documentFile in documentFiles)
          {
            if (PathUtils.IsSamePath(documentFile.FileName, configurationFile))
            {
              num1 = documentFile.BlobId;
              break;
            }
          }
        }
      }
      foreach (ArticlePreviewModule.DocumentFile documentFile in documentFiles)
        e.Items.Add(new FileBlobItem(num2, fileAttributeId, documentFile.FileIndex));
    }
    if (num1 == 0L)
      return;
    e.PreferedBlobID = num1;
  }

  private bool IsArticleObject(long objectId, int objectType)
  {
    return MetaDataHelper.IsObjectTypeChildOf(objectType, IDCache.Default.AllArticles.Id);
  }

  private int GetFileAttributeId()
  {
    return (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).FileAttributeID;
  }

  private List<ArticlePreviewModule.DocumentFile> GetDocumentFiles(
    long documentId,
    int fileAttributeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObject(documentId, true).GetAttributeByID(fileAttributeId);
      int valuesCount = attributeById != null ? attributeById.ValuesCount : 0;
      List<ArticlePreviewModule.DocumentFile> documentFiles = new List<ArticlePreviewModule.DocumentFile>(valuesCount);
      if (valuesCount > 0)
      {
        IBlobReader blobReader = attributeById as IBlobReader;
        for (int fileIndex = 0; fileIndex < valuesCount; ++fileIndex)
        {
          attributeById.Index = fileIndex;
          if (!attributeById.IsNull)
          {
            BlobInformation blobInformation = blobReader.OpenBlob(-1);
            documentFiles.Add(new ArticlePreviewModule.DocumentFile(fileIndex, blobInformation.FileName, blobInformation.BlobID));
          }
        }
      }
      return documentFiles;
    }
  }

  private class DocumentFile
  {
    public readonly int FileIndex;
    public readonly string FileName;
    public readonly long BlobId;

    public DocumentFile(int fileIndex, string fileName, long blobId)
    {
      this.FileIndex = fileIndex;
      this.FileName = fileName;
      this.BlobId = blobId;
    }
  }
}
