// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleOperations
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class ArticleOperations
{
  public ICollection<StringKey> GetIdentityKeys()
  {
    return (ICollection<StringKey>) new StringKey[3]
    {
      (StringKey) IDCache.Default.Designation.Text,
      (StringKey) IDCache.Default.OKPCode.Text,
      (StringKey) IDCache.Default.Name.Text
    };
  }

  public SectionEntity TryGetArticleInitialDocument(SectionEntity articleItem)
  {
    ArticleSection articleSection = articleItem != null ? articleItem.Sections.Get<ArticleSection>() : throw new ArgumentNullException(nameof (articleItem));
    return articleSection.InitialDocumentType == ArticleInitialDocumentType.Normal || articleSection.InitialDocumentType == ArticleInitialDocumentType.Hidden ? articleSection.InitialDocument : (SectionEntity) null;
  }

  public SectionEntity TryGetHiddenInitialDocument(SectionEntity articleItem)
  {
    ArticleSection articleSection = articleItem != null ? articleItem.Sections.Get<ArticleSection>() : throw new ArgumentNullException(nameof (articleItem));
    return articleSection.InitialDocumentType == ArticleInitialDocumentType.Hidden ? articleSection.InitialDocument : (SectionEntity) null;
  }

  public SectionEntity TryGetArticleMainDocument(SectionEntity articleItem)
  {
    ArticleSection articleSection = articleItem != null ? articleItem.Sections.Get<ArticleSection>() : throw new ArgumentNullException(nameof (articleItem));
    return articleSection.InitialDocumentType == ArticleInitialDocumentType.Normal ? articleSection.InitialDocument : (SectionEntity) null;
  }

  public SectionEntity GetArticleMainDocument(SectionEntity articleItem)
  {
    return (articleItem != null ? this.TryGetArticleMainDocument(articleItem) : throw new ArgumentNullException(nameof (articleItem))) ?? throw new InvalidOperationException($"У изделия '{DisplaySection.GetQualifiedName(articleItem)}' не задан документ, по которому оно выпускается.");
  }

  public List<LocalId<int>> GetPossibleArticleTypes(SectionEntity articleItem)
  {
    SectionEntity articleMainDocument = this.TryGetArticleMainDocument(articleItem);
    if (articleMainDocument != null)
      return this.GetPossibleArticleTypes(ObjectSection.GetObjectType(articleMainDocument));
    List<LocalId<int>> possibleArticleTypes = CollectionUtils.ConvertAsList<int, LocalId<int>>((ICollection<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(IDCache.Default.AllArticles.Id), (Converter<int, LocalId<int>>) (id => (LocalId<int>) DBHelper.CreateObjectTypeGID(id)));
    possibleArticleTypes.RemoveAt(0);
    return possibleArticleTypes;
  }

  public List<LocalId<int>> GetPossibleArticleTypes(int documentType)
  {
    if (documentType == -1)
      throw new ArgumentException();
    string str = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (ServiceUtils.GetService<IDocumentTypeSettingsService>((object) sessionKeeper.Session, true).InheritedFromDocuments(sessionKeeper.Session.SessionGUID, documentType))
        str = DocumentTypeSettingsCache.GetSettings(documentType).OutputObjectTypes;
    }
    string[] strArray = str.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
    List<LocalId<int>> possibleArticleTypes = new List<LocalId<int>>(strArray.Length);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (string g in strArray)
      {
        Guid anObjectTypeGuid = new Guid(g);
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(anObjectTypeGuid, false);
        if (objectType != null)
          possibleArticleTypes.Add(new LocalId<int>(objectType.ObjectType, objectType.ObjectTypeName));
      }
    }
    return possibleArticleTypes;
  }

  public DecodeAttributesOptions GetDecodeOptions(SectionEntity articleItem)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    DecodeAttributesOptions decodeOptions = new DecodeAttributesOptions();
    SectionEntity articleMainDocument = this.TryGetArticleMainDocument(articleItem);
    if (articleMainDocument != null)
      decodeOptions.Properties[(StringKey) "DocumentType"] = (object) ObjectSection.GetObjectType(articleMainDocument);
    return decodeOptions;
  }

  public EncodeAttributesOptions GetEncodeOptions(SectionEntity articleItem)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    EncodeAttributesOptions encodeOptions = new EncodeAttributesOptions();
    encodeOptions.ReportErrorsOnly = true;
    SectionEntity articleMainDocument = this.TryGetArticleMainDocument(articleItem);
    if (articleMainDocument != null)
      encodeOptions.Properties[(StringKey) "DocumentType"] = (object) ObjectSection.GetObjectType(articleMainDocument);
    return encodeOptions;
  }
}
