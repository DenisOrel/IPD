// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.ReaderAPI.Common.SchemaCacheHolder
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using System.Collections.Generic;

#nullable disable
namespace XmlReaderAPI.ReaderAPI.Common;

public class SchemaCacheHolder
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IDictionary<string, SchemaCacheItem> _storage = (IDictionary<string, SchemaCacheItem>) new Dictionary<string, SchemaCacheItem>();

  public SchemaCacheItem GetCacheItem(string name)
  {
    SchemaCacheItem cacheItem1;
    if (this._storage.TryGetValue(name, out cacheItem1))
      return cacheItem1;
    SchemaCacheItem cacheItem2 = new SchemaCacheItem();
    this._storage[name] = cacheItem2;
    return cacheItem2;
  }
}
