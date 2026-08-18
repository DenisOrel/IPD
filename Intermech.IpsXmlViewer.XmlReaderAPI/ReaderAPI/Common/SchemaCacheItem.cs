// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.ReaderAPI.Common.SchemaCacheItem
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.Diagnostics;
using System.Data;
using System.Runtime.CompilerServices;

#nullable disable
namespace XmlReaderAPI.ReaderAPI.Common;

public class SchemaCacheItem
{
  /// <summary>
  /// 
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void InitFieldNames([NotNull] IDataReader reader)
  {
    if (this.FieldNames != null)
      return;
    this.FieldNames = new string[reader.FieldCount];
    for (int i = 0; i < reader.FieldCount; ++i)
      this.FieldNames[i] = reader.GetName(i);
  }

  public string[] FieldNames { get; private set; }
}
