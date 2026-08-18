// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ArticleFromJTDocumentLocator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class ArticleFromJTDocumentLocator : IObjectLocator
{
  private readonly IDBObjectRef jtDocumentRef;

  public ArticleFromJTDocumentLocator(IDBObjectRef jtDocumentRef)
  {
    this.jtDocumentRef = jtDocumentRef != null ? jtDocumentRef : throw new ArgumentNullException(nameof (jtDocumentRef));
  }

  public ObjectLocatorResult LocateObject()
  {
    long objectId = this.jtDocumentRef.GetObjectId();
    if (DBHelper.GetObjectType(objectId) != IDCache.Default.JTDocuments.Id)
      return (ObjectLocatorResult) null;
    long num;
    string articleExternalKey;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId);
      num = (long) dbObject.GetValuesByID(IDCache.Default.JTSourceDocumentReference.Id, true)[0];
      if (num > 0L && sessionKeeper.Session.HasMyWorkCopy(num))
        num = -num;
      articleExternalKey = (string) dbObject.GetValuesByID(IDCache.Default.ObjectExternalKey.Id, true)[0];
    }
    return new ExternalKeyArticleLocator((IExternalKeyLocatorData) new SimpleExternalKeyLocatorData(num, articleExternalKey)).LocateObject();
  }
}
