// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Interfaces.IGtcItemObject
// Assembly: Intermech.GTC.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 767EAE12-F30F-454C-81D0-2862AEDD13C4
// Assembly location: D:\IPS\Client\Intermech.GTC.Interfaces.dll

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.GTC.Interfaces;

public interface IGtcItemObject : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
{
  int AddPlibAttribute(
    string aAttBsuCode,
    long classFolderObjId,
    object[] aPropValues,
    out string errorMsg);

  int AddExternalLibAttribute(
    string aAttributeName,
    string libraryType,
    string libraryName,
    object[] aPropValues,
    out string errorMsg);

  Dictionary<int, string> AttributeCategoriesDictionary { get; set; }
}
