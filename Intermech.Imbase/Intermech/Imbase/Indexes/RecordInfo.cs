// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Indexes.RecordInfo
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Indexes;

internal class RecordInfo
{
  public long LinkId { get; set; }

  public long TableId { get; set; }

  public long RecordId { get; set; }

  public bool CreateNewMode { get; set; }

  public int ObjectType { get; set; }

  public List<long> ObjectIds { get; set; } = new List<long>();
}
