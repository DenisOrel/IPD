// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseTableRecordID
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using System.Diagnostics;

#nullable disable
namespace Intermech.Imbase;

internal class ImbaseTableRecordID : IImbaseTableRecordID, IDBObjectID
{
  private long _value;

  public ImbaseTableRecordID(long value) => this._value = value;

  public long Value
  {
    [DebuggerStepThrough] get => this._value;
  }

  public long ID
  {
    [DebuggerStepThrough] get => 0;
  }

  public string Caption
  {
    [DebuggerStepThrough] get => string.Empty;
  }

  public long Owner
  {
    [DebuggerStepThrough] get => 0;
  }
}
