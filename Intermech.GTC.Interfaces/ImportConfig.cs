// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Interfaces.ImportConfig
// Assembly: Intermech.GTC.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 767EAE12-F30F-454C-81D0-2862AEDD13C4
// Assembly location: D:\IPS\Client\Intermech.GTC.Interfaces.dll

using System;

#nullable disable
namespace Intermech.GTC.Interfaces;

[Serializable]
public class ImportConfig : IImportConfig
{
  private string _folderPath = string.Empty;
  private long _catalogId;

  public GtcVersion Version { get; set; }

  public string Path
  {
    get => this._folderPath;
    set => this._folderPath = value;
  }

  public long CatalogId
  {
    get => this._catalogId;
    set => this._catalogId = value;
  }

  public bool OnlyPlibAttributes { get; set; }
}
