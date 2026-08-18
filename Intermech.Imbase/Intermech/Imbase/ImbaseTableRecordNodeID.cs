// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseTableRecordNodeID
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Navigator.Interfaces;
using System.Diagnostics;

#nullable disable
namespace Intermech.Imbase;

internal class ImbaseTableRecordNodeID : INodeID
{
  private object _cookie;
  private ImbaseTableRecordID _recordId;

  public ImbaseTableRecordNodeID(ImbaseTableRecordID recordId) => this._recordId = recordId;

  public ImbaseTableRecordID RecordId
  {
    [DebuggerStepThrough] get => this._recordId;
    set => this._recordId = value;
  }

  public int CategoryID => 1;

  public int TypeID => -1;

  public object Cookie
  {
    get => this._cookie;
    set => this._cookie = value;
  }
}
