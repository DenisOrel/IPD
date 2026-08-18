// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.ContextInfo
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace Intermech.Imbase.API;

[Serializable]
internal struct ContextInfo
{
  public long CatalogId;
  public long LinkId;
  public long TableId;
  public string TableName;
  public string Description;
  public string User;
  public string IndexFields;
  public double Created;
  public double Modified;
}
