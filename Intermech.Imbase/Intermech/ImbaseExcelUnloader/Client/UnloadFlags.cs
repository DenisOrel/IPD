// Decompiled with JetBrains decompiler
// Type: Intermech.ImbaseExcelUnloader.Client.UnloadFlags
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace Intermech.ImbaseExcelUnloader.Client;

[Flags]
public enum UnloadFlags
{
  None = 0,
  Catalog = 1,
  Folder = 2,
  TableRef = 4,
  TableData = 8,
  CatalogRec = 16, // 0x00000010
  NameObjectReferences = 32, // 0x00000020
}
