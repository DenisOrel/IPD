// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.SourceDocumentFromJTDocumentLocator
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

internal sealed class SourceDocumentFromJTDocumentLocator : IObjectLocator
{
  private readonly IDBObjectRef jtDocumentRef;

  public SourceDocumentFromJTDocumentLocator(IDBObjectRef jtDocumentRef)
  {
    this.jtDocumentRef = jtDocumentRef != null ? jtDocumentRef : throw new ArgumentNullException(nameof (jtDocumentRef));
  }

  public ObjectLocatorResult LocateObject()
  {
    long objectId = this.jtDocumentRef.GetObjectId();
    if (DBHelper.GetObjectType(objectId) != IDCache.Default.JTDocuments.Id)
      return (ObjectLocatorResult) null;
    long num;
    int objectType;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      num = (long) sessionKeeper.Session.GetObject(objectId).GetValuesByID(IDCache.Default.JTSourceDocumentReference.Id, true)[0];
      if (num > 0L && sessionKeeper.Session.HasMyWorkCopy(num))
        num = -num;
      objectType = DBHelper.GetObjectType(num);
    }
    return new ObjectLocatorResult(num, objectType);
  }
}
