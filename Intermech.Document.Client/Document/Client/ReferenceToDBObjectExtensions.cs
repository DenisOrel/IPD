// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ReferenceToDBObjectExtensions
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.Document.Client;

public static class ReferenceToDBObjectExtensions
{
  public static void UpdateDBObjectInfo(
    this ReferenceToDBObjectBase referenceToDBObject,
    string filtrationSettings = null)
  {
    if (referenceToDBObject == null)
      throw new ArgumentNullException(nameof (referenceToDBObject));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      referenceToDBObject.UpdateDBObjectInfo((object) sessionKeeper.Session, filtrationSettings);
  }
}
